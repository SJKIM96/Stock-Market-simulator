using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockExchangeMarket
{

    public partial class StockSecuritiesExchange : Form
    {
        RealTimedata Subject;
        public StockSecuritiesExchange()
        {
            InitializeComponent();
        }

        private void beginTradingToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            this.marketToolStripMenuItem.Text = "&Market <<Open>>";

            MarketDepthSubMenu(this.marketByOrderToolStripMenuItem1);
            MarketDepthSubMenu(this.marketByPriceToolStripMenuItem1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            this.Close();
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
