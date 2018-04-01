using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StockExchangeMarket
{
    

    public partial class StockSecuritiesExchange : Form
    {
        

        TcpClient client;
        RealTimedata Subject;
        static Random rnd = new Random();
        int sessionNum;
        int CSeq;
        public StockSecuritiesExchange()
        {
            InitializeComponent();
            
        }

        public void listentoServer()
        {
            while (true)
            {
                NetworkStream nwstream = client.GetStream();
                byte[] read = new byte[client.ReceiveBufferSize];
                int bytesread = nwstream.Read(read, 0, client.ReceiveBufferSize);
                string decode = Encoding.ASCII.GetString(read, 0, bytesread);
                string[] message = decode.Split(' ');
                if (message[0] == "notify")
                {

                    string []data= decode.Split(new string[] { "Data: " }, StringSplitOptions.None);

                    JObject jObject = JObject.Parse(data[1]);
                    
                    int size = Convert.ToInt32( (string)jObject.SelectToken("size"));
                    double price = Convert.ToDouble((string)jObject.SelectToken("price"));
                    DateTime dt = DateTime.Now;
                    //DateTime dt = Convert.ToDateTime((string)jObject.SelectToken("timestamp")); 



                    if (message[1] == "Buy")
                    {
                        foreach(Company company in Subject.getCompanies())
                        {
                            if (message[2] == company.Symbol)
                            {
                                company.addBuyOrder(price, size, dt);
                            }
                        }
                    }
                    else
                    {
                        foreach (Company company in Subject.getCompanies())
                        {
                            if (message[2] == company.Symbol)
                            {
                                company.addSellOrder(price, size, dt);
                            }
                        }
                    }
                }

                else if (message[0] == "exit")
                {
                    break;
                }
            }
        }


        private void beginTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //connect to server
            //if its already opened print error
            if (client == null)
            {
                client = new TcpClient();
                //IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                //IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, Convert.ToInt32(this.clientPortToolStripMenuItem.Text));
                //client = new TcpClient(ipLocalEndPoint);
                try
                {//try to connect
                
                    
                    client.Connect(this.serverIPToolStripMenuItem.Text, Convert.ToInt32(this.serverPortToolStripMenuItem.Text));                 
                }
                catch(Exception err)//catch error
                {
                    client = null;
                    Console.Write(err.Message);
                }
            }
            //send register 
            CSeq = rnd.Next();
            string msg = "register SME/TCP-1.0\nID: " + this.userNameToolStripMenuItem.Text.ToString() + " CSeq: " + CSeq++ + " Notification Port: ";
            NetworkStream nwstream = client.GetStream();
            byte[] tosend = ASCIIEncoding.ASCII.GetBytes(msg);
            nwstream.Write(tosend, 0, tosend.Length);
            
            //read msg
            byte[] read = new byte[client.ReceiveBufferSize];
            int bytesread = nwstream.Read(read, 0, client.ReceiveBufferSize);
            string decode = Encoding.ASCII.GetString(read, 0, bytesread);
            string[] message = decode.Split(' ');
            sessionNum = Convert.ToInt32(message[5]);

            //byte[] read1 = new byte[client.ReceiveBufferSize];
            //int bytesread1 = nwstream.Read(read1, 0, client.ReceiveBufferSize);
            //string decode1 = Encoding.ASCII.GetString(read1, 0, bytesread1);
            //string[] message2 = decode1.Split(' ');
            
            //sanaty check
            if (message[1] != "OK")
                client.Close();
            else    //no error
            {
                
                this.beginTradingToolStripMenuItem.Enabled = false;
                this.stopTradingToolStripMenuItem.Enabled = true;

                string companymsg = "listCompanies SME/TCP-1.0\nCSeq: " + CSeq++ + " Session: " + sessionNum;        
                tosend = ASCIIEncoding.ASCII.GetBytes(companymsg);
                nwstream.Write(tosend, 0, tosend.Length);


                bytesread = nwstream.Read(read, 0, client.ReceiveBufferSize);
                decode = Encoding.ASCII.GetString(read, 0, bytesread);
                message = decode.Split(new string[] { "Data: " }, StringSplitOptions.None);

                // Create three stocks and add them to the market
                Subject = new RealTimedata(message[1], client, ref CSeq, sessionNum);

              
                

                this.watchToolStripMenuItem.Visible = true;
                this.ordersToolStripMenuItem.Visible = true;
                this.beginTradingToolStripMenuItem.Enabled = false;
                this.marketToolStripMenuItem.Text = "Join <<Connected>>";
                this.userNameToolStripMenuItem.Enabled = false;
                this.clientIPToolStripMenuItem.Enabled = false;
                this.clientPortToolStripMenuItem.Enabled = false;
                this.serverIPToolStripMenuItem.Enabled = false;
                this.serverPortToolStripMenuItem.Enabled = false;


                MarketDepthSubMenu(this.marketByOrderToolStripMenuItem1);
                MarketDepthSubMenu(this.marketByPriceToolStripMenuItem1);


                //get sellorder, buyorder from the company

                companymsg = "listBuyOrders SME/TCP-1.0\nCSeq: " + CSeq++ + " Session: " + sessionNum;
                tosend = ASCIIEncoding.ASCII.GetBytes(companymsg);
                nwstream.Write(tosend, 0, tosend.Length);

                bytesread = nwstream.Read(read, 0, client.ReceiveBufferSize);
                decode = Encoding.ASCII.GetString(read, 0, bytesread);
                message = decode.Split(new string[] { "Data: " }, StringSplitOptions.None);

                JObject jObject = JObject.Parse(message[1]);
                IList<JToken> BMSFT = jObject["MSFT"].Children().ToList();
                IList<JToken> BFB = jObject["FB"].Children().ToList();
                IList<JToken> BAAPL = jObject["AAPL"].Children().ToList();


                foreach (Company company in Subject.getCompanies())
                {
                    if (company.Symbol == "MSFT")
                    {
                        foreach (JToken result in BMSFT)
                        {
                            int q = Convert.ToInt32(((string)result["size"]));
                            double p = Convert.ToDouble(((string)result["price"]));
                            DateTime dt = Convert.ToDateTime(((string)result["closedPrice"]));
                            company.addBuyOrder(p, q, dt);
                        }
                    }
                    if (company.Symbol == "FB")
                    {
                        foreach (JToken result in BFB)
                        {
                            int q = Convert.ToInt32(((string)result["size"]));
                            double p = Convert.ToDouble(((string)result["price"]));
                            DateTime dt = Convert.ToDateTime(((string)result["closedPrice"]));
                            company.addBuyOrder(p, q, dt);
                        }
                    }
                    if (company.Symbol == "AAPL")
                    {
                        foreach (JToken result in BAAPL)
                        {
                            int q = Convert.ToInt32(((string)result["size"]));
                            double p = Convert.ToDouble(((string)result["price"]));
                            DateTime dt = Convert.ToDateTime(((string)result["closedPrice"]));
                            company.addBuyOrder(p, q, dt);
                        }
                    }
                }




                companymsg = "listSellOrders SME/TCP-1.0\nCSeq: " + CSeq++ + " Session: " + sessionNum;
                tosend = ASCIIEncoding.ASCII.GetBytes(companymsg);
                nwstream.Write(tosend, 0, tosend.Length);

                bytesread = nwstream.Read(read, 0, client.ReceiveBufferSize);
                decode = Encoding.ASCII.GetString(read, 0, bytesread);
                message = decode.Split(new string[] { "Data: " }, StringSplitOptions.None);

                jObject = JObject.Parse(message[1]);
                IList<JToken> SMSFT = jObject["MSFT"].Children().ToList();
                IList<JToken> SFB = jObject["FB"].Children().ToList();
                IList<JToken> SAAPL = jObject["AAPL"].Children().ToList();


                foreach (Company company in Subject.getCompanies())
                {
                    if (company.Symbol == "MSFT")
                    {
                        foreach (JToken result in SMSFT)
                        {
                            int q = Convert.ToInt32(((string)result["size"]));
                            double p= Convert.ToDouble(((string)result["price"]));
                            DateTime dt= Convert.ToDateTime(((string)result["closedPrice"]));
                            company.addSellOrder(p, q,dt);
                        }
                    }
                    if (company.Symbol == "FB")
                    {
                        foreach (JToken result in SFB)
                        {
                            int q = Convert.ToInt32(((string)result["size"]));
                            double p = Convert.ToDouble(((string)result["price"]));
                            DateTime dt = Convert.ToDateTime(((string)result["closedPrice"]));
                            company.addSellOrder(p, q,dt);
                        }
                    }
                    if (company.Symbol == "AAPL")
                    {
                        foreach (JToken result in SAAPL)
                        {
                            int q = Convert.ToInt32(((string)result["size"]));
                            double p = Convert.ToDouble(((string)result["price"]));
                            DateTime dt = Convert.ToDateTime(((string)result["closedPrice"]));
                            company.addSellOrder(p, q, dt);
                        }
                    }
                }

                // thread out to recieve update from other user
                Thread listener = new Thread(new ThreadStart(listentoServer));
                listener.IsBackground = true;
                listener.Start();
               

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (client != null)
                client.Close();
            client = null;
            this.Close();
        }

        private void StockStateSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockStateSummary summaryObserver = new StockStateSummary(Subject);
            summaryObserver.MdiParent = this;

            // Display the new form.
            summaryObserver.Show();



        }
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cascade all MDI child windows.
            this.LayoutMdi(MdiLayout.Cascade);
        }



        private void arrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tile all child forms vertically.
            this.LayoutMdi(MdiLayout.ArrangeIcons);

        }

        private void horizontalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tile all child forms horizontally.
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void verticalTileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tile all child forms vertically.
            this.LayoutMdi(MdiLayout.TileVertical);

        }

        private void stopTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //close connection
            if (client != null)
                client.Close();
            client = null;
            this.beginTradingToolStripMenuItem.Enabled = true;
            this.stopTradingToolStripMenuItem.Enabled = false;

            this.watchToolStripMenuItem.Visible = false;
            this.ordersToolStripMenuItem.Visible = false;     
            this.marketToolStripMenuItem.Text = "Join <<Disconnected>>";

            foreach (Form frm in this.MdiChildren)
            { 
                frm.Dispose();
                frm.Close();
            }

            this.userNameToolStripMenuItem.Enabled = true;
            this.clientIPToolStripMenuItem.Enabled = true;
            this.clientPortToolStripMenuItem.Enabled = true;
            this.serverIPToolStripMenuItem.Enabled = true;
            this.serverPortToolStripMenuItem.Enabled = true;
        }



        public void MarketDepthSubMenu(ToolStripMenuItem MnuItems)
        {
            ToolStripMenuItem SSSMenu;
            List<Company> StockCompanies = Subject.getCompanies();
            foreach (Company company in StockCompanies)
            {
                if (MnuItems.Name == "marketByPriceToolStripMenuItem1")
                    SSSMenu = new ToolStripMenuItem(company.Name, null, marketByPriceToolStripMenuItem_Click);
                else
                    SSSMenu = new ToolStripMenuItem(company.Name, null, marketByOrderToolStripMenuItem_Click);
                MnuItems.DropDownItems.Add(SSSMenu);
            }
        }

        public void marketByOrderToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
           
            MarketByOrder newMDIChild = new MarketByOrder(Subject, sender.ToString());
            // Set the parent form of the child window.
            newMDIChild.Text = "Market Depth By Order (" + sender.ToString() + ")";
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }
        private void marketByPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarketByPrice newMDIChild = new MarketByPrice(Subject, sender.ToString());
            // Set the parent form of the child window.

            newMDIChild.Text = "Market Depth By Price (" + sender.ToString() + ")";

            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void bidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceBidOrder newMDIChild = new PlaceBidOrder(Subject, client, ref CSeq, sessionNum);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void askToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceSellOrder newMDIChild = new PlaceSellOrder(Subject, client, ref CSeq, sessionNum);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }
    }
}
