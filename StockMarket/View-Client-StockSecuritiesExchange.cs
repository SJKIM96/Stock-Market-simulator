using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace StockExchangeMarket
{

    public partial class StockSecuritiesExchange : Form
    {
        
        TcpClient client;
        RealTimedata Subject;
        static Random rnd = new Random();
        public StockSecuritiesExchange()
        {
            InitializeComponent();
            
        }
        

        private void beginTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //connect to server
            //if its already opened print error
            if (client == null)
            {
                //client = new TcpClient();
                IPAddress ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
                IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, Convert.ToInt32(this.clientPortToolStripMenuItem.Text));
                client = new TcpClient(ipLocalEndPoint);
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

            NetworkStream nwstream = client.GetStream();
            byte[] tosend = ASCIIEncoding.ASCII.GetBytes("testing");
            nwstream.Write(tosend, 0, tosend.Length);
           
            //client information
            int CSeq = rnd.Next();


            this.beginTradingToolStripMenuItem.Enabled = false;
            this.stopTradingToolStripMenuItem.Enabled = true;



            // Create three stocks and add them to the market
            Subject = new RealTimedata();
            

            // In this lab assignment we will add three companies only using the following format:
            // Company symbol , Company name , Open price
            Subject.addCompany("MSFT", "Microsoft Corporation", 46.13);
            Subject.addCompany("AAPL", "Apple Inc.", 105.22);
            Subject.addCompany("FB", "Facebook, Inc.", 80.67);

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
            PlaceBidOrder newMDIChild = new PlaceBidOrder(Subject);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }

        private void askToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlaceSellOrder newMDIChild = new PlaceSellOrder(Subject);
            // Set the parent form of the child window.
            newMDIChild.MdiParent = this;
            // Display the new form.
            newMDIChild.Show();
        }
    }
}
