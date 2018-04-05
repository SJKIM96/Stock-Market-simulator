using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace StockExchangeMarket
{
    public class tempObj
    {
        public int size;
        public double price;
        DateTime timestamp;

        public tempObj(double p, int s)
        {
            this.size = s;
            this.price = p;
            this.timestamp = DateTime.Now;
        }
    }


    public partial class PlaceBidOrder : Form
    {
        RealTimedata Subject;
        Company selectedCompany;
        TcpClient client;
        int sessionNum;
        int CSeq;
        public PlaceBidOrder(Object _subject)
        {
            Subject = (RealTimedata)_subject;

            InitializeComponent();
            //this.comboBox1.Items.Add("");
            foreach (Company company in Subject.getCompanies())
            {
                this.comboBox1.Items.Add(company.Name);

            }
            comboBox1.SelectedIndex = 0;
        }
        public PlaceBidOrder(Object _subject, TcpClient client, ref int  CSeq, int SessionNum)
        {
            Subject = (RealTimedata)_subject;
            this.client = client;
            this.CSeq = CSeq;
            this.sessionNum = SessionNum;

            InitializeComponent();
            //this.comboBox1.Items.Add("");
            foreach (Company company in Subject.getCompanies())
            {
                this.comboBox1.Items.Add(company.Name);

            }
            comboBox1.SelectedIndex = 0;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
             // Check to see if both validation checks return true
            if (ValidShareSize() && ValidSharePrice())
            {
                selectedCompany.addBuyOrder(Convert.ToDouble(textBox2.Text), Convert.ToInt32(textBox1.Text));
                tempObj tO;
                NetworkStream nwstream = client.GetStream();
                string companymsg = "buyOrder SME/TCP-1.0\nCSeq: " + CSeq++ + " Session: " + sessionNum + " Data: ";
                tO = new tempObj(Convert.ToDouble(textBox2.Text), Convert.ToInt32(textBox1.Text));
                string orders;
                if (selectedCompany.Symbol == "MSFT")
                {
                    orders = JsonConvert.SerializeObject(new { MSFT = tO });
                }
                else if (selectedCompany.Symbol == "AAPL")
                {
                    orders = JsonConvert.SerializeObject(new { AAPL = tO });
                }
                else
                {
                    orders = JsonConvert.SerializeObject(new { FB = tO });
                }
            
                companymsg += orders;
                //send to server
                byte[] tosend = ASCIIEncoding.ASCII.GetBytes(companymsg);
                nwstream.Write(tosend, 0, tosend.Length);

                //wait for response
                //byte[] read = new byte[client.ReceiveBufferSize];
                //int bytesread = nwstream.Read(read, 0, client.ReceiveBufferSize);
                //string decode = Encoding.ASCII.GetString(read, 0, bytesread);


                foreach (Control control in this.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        textBox.Text = null;
                    }

                    if (control is ComboBox)
                    {
                        ComboBox comboBox = (ComboBox)control;
                        if (comboBox.Items.Count > 0)
                            comboBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int i = 0;
            foreach (Company company in Subject.getCompanies())
            {
                if (comboBox1.SelectedIndex == i)
                {
                    selectedCompany = company;
                    break;
                }
                else i++;
            }
        }

        // Method that validates the shares size
        // The method returns true if the textbox passes the validation checks
        private bool ValidShareSize()
        {

            int num;
            bool isNum = Int32.TryParse(textBox1.Text.Trim(), out num);

            if (!isNum)
            {
                // If not numeric, set the error
                epErrorProvider.SetError(textBox1, "The # of Shares is invalid");
                return false;
            }
            else
            {
                // If it has a value, clear the error
                epErrorProvider.SetError(textBox1, "");
                return true;
            }

        }

        // Method that validates the shares Price
        // The method returns true if the textbox passes the validation checks
        private bool ValidSharePrice()
        {

            Double Doub;
            bool isDoub = Double.TryParse(textBox2.Text.Trim(), out Doub);
            // Check the Name text
            if (!isDoub)
            {
                // If empty, set the error
                epErrorProvider.SetError(textBox2, "The Price of Shares is invalid");
                return false;
            }
            else
            {
                // If it has a value, clear the error
                epErrorProvider.SetError(textBox2, "");
                return true;
            }
        }
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            ValidShareSize();
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            ValidSharePrice();
        }
    }
}
