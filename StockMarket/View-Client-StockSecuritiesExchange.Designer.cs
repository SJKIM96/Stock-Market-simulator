namespace StockExchangeMarket
{
    partial class StockSecuritiesExchange
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StockSecuritiesExchange));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.marketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginTradingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopTradingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockStateSummaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marketByOrderToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.marketByPriceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.askToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalTileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.userNameToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.aToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientIPToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.clientPortToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.serverIPToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.serverPortToolStripMenuItem = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.userNameToolStripMenuItem,
            this.marketToolStripMenuItem,
            this.watchToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.serverPortToolStripMenuItem,
            this.sPortToolStripMenuItem,
            this.serverIPToolStripMenuItem,
            this.sPToolStripMenuItem,
            this.clientPortToolStripMenuItem,
            this.dsToolStripMenuItem,
            this.clientIPToolStripMenuItem,
            this.aToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.windowsToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1309, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // marketToolStripMenuItem
            // 
            this.marketToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginTradingToolStripMenuItem,
            this.stopTradingToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.marketToolStripMenuItem.Name = "marketToolStripMenuItem";
            this.marketToolStripMenuItem.Size = new System.Drawing.Size(147, 23);
            this.marketToolStripMenuItem.Text = "&Join <<Disconnected>>";
            // 
            // beginTradingToolStripMenuItem
            // 
            this.beginTradingToolStripMenuItem.Name = "beginTradingToolStripMenuItem";
            this.beginTradingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.beginTradingToolStripMenuItem.Text = "&Register";
            this.beginTradingToolStripMenuItem.Click += new System.EventHandler(this.beginTradingToolStripMenuItem_Click);
            // 
            // stopTradingToolStripMenuItem
            // 
            this.stopTradingToolStripMenuItem.Enabled = false;
            this.stopTradingToolStripMenuItem.Name = "stopTradingToolStripMenuItem";
            this.stopTradingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopTradingToolStripMenuItem.Text = "Unregister";
            this.stopTradingToolStripMenuItem.Click += new System.EventHandler(this.stopTradingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // watchToolStripMenuItem
            // 
            this.watchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stockStateSummaryToolStripMenuItem,
            this.marketByOrderToolStripMenuItem1,
            this.marketByPriceToolStripMenuItem1});
            this.watchToolStripMenuItem.Name = "watchToolStripMenuItem";
            this.watchToolStripMenuItem.Size = new System.Drawing.Size(53, 23);
            this.watchToolStripMenuItem.Text = "Watc&h";
            this.watchToolStripMenuItem.Visible = false;
            // 
            // stockStateSummaryToolStripMenuItem
            // 
            this.stockStateSummaryToolStripMenuItem.Name = "stockStateSummaryToolStripMenuItem";
            this.stockStateSummaryToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.stockStateSummaryToolStripMenuItem.Text = "&Stock State Summary";
            this.stockStateSummaryToolStripMenuItem.Click += new System.EventHandler(this.StockStateSummaryToolStripMenuItem_Click);
            // 
            // marketByOrderToolStripMenuItem1
            // 
            this.marketByOrderToolStripMenuItem1.Name = "marketByOrderToolStripMenuItem1";
            this.marketByOrderToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.marketByOrderToolStripMenuItem1.Text = "Market By &Order";
            // 
            // marketByPriceToolStripMenuItem1
            // 
            this.marketByPriceToolStripMenuItem1.Name = "marketByPriceToolStripMenuItem1";
            this.marketByPriceToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.marketByPriceToolStripMenuItem1.Text = "Market By &Price";
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bidToolStripMenuItem,
            this.askToolStripMenuItem});
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(54, 23);
            this.ordersToolStripMenuItem.Text = "&Orders";
            this.ordersToolStripMenuItem.Visible = false;
            // 
            // bidToolStripMenuItem
            // 
            this.bidToolStripMenuItem.Name = "bidToolStripMenuItem";
            this.bidToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.bidToolStripMenuItem.Text = "&Bid";
            this.bidToolStripMenuItem.Click += new System.EventHandler(this.bidToolStripMenuItem_Click);
            // 
            // askToolStripMenuItem
            // 
            this.askToolStripMenuItem.Name = "askToolStripMenuItem";
            this.askToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.askToolStripMenuItem.Text = "&Ask";
            this.askToolStripMenuItem.Click += new System.EventHandler(this.askToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.horizontalTileToolStripMenuItem,
            this.verticalTileToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(68, 23);
            this.windowsToolStripMenuItem.Text = "&Windows";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.cascadeToolStripMenuItem.Text = "Cascade";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
            // 
            // horizontalTileToolStripMenuItem
            // 
            this.horizontalTileToolStripMenuItem.Name = "horizontalTileToolStripMenuItem";
            this.horizontalTileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.horizontalTileToolStripMenuItem.Text = "Horizontal Tile";
            this.horizontalTileToolStripMenuItem.Click += new System.EventHandler(this.horizontalTileToolStripMenuItem_Click);
            // 
            // verticalTileToolStripMenuItem
            // 
            this.verticalTileToolStripMenuItem.Name = "verticalTileToolStripMenuItem";
            this.verticalTileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.verticalTileToolStripMenuItem.Text = "Vertical Tile ";
            this.verticalTileToolStripMenuItem.Click += new System.EventHandler(this.verticalTileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(77, 23);
            this.toolStripMenuItem1.Text = "User Name";
            // 
            // userNameToolStripMenuItem
            // 
            this.userNameToolStripMenuItem.Name = "userNameToolStripMenuItem";
            this.userNameToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            // 
            // aToolStripMenuItem
            // 
            this.aToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.aToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.aToolStripMenuItem.Name = "aToolStripMenuItem";
            this.aToolStripMenuItem.Size = new System.Drawing.Size(63, 23);
            this.aToolStripMenuItem.Text = "Client IP";
            // 
            // dsToolStripMenuItem
            // 
            this.dsToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.dsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.dsToolStripMenuItem.Name = "dsToolStripMenuItem";
            this.dsToolStripMenuItem.Size = new System.Drawing.Size(75, 23);
            this.dsToolStripMenuItem.Text = "Client Port";
            // 
            // sPToolStripMenuItem
            // 
            this.sPToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sPToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.sPToolStripMenuItem.Name = "sPToolStripMenuItem";
            this.sPToolStripMenuItem.Size = new System.Drawing.Size(64, 23);
            this.sPToolStripMenuItem.Text = "Server IP";
            // 
            // sPortToolStripMenuItem
            // 
            this.sPortToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.sPortToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Highlight;
            this.sPortToolStripMenuItem.Name = "sPortToolStripMenuItem";
            this.sPortToolStripMenuItem.Size = new System.Drawing.Size(76, 23);
            this.sPortToolStripMenuItem.Text = "Server Port";
            // 
            // clientIPToolStripMenuItem
            // 
            this.clientIPToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clientIPToolStripMenuItem.Name = "clientIPToolStripMenuItem";
            this.clientIPToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.clientIPToolStripMenuItem.Text = "127.0.0.1";
            // 
            // clientPortToolStripMenuItem
            // 
            this.clientPortToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.clientPortToolStripMenuItem.Name = "clientPortToolStripMenuItem";
            this.clientPortToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.clientPortToolStripMenuItem.Text = "8001";
            // 
            // serverIPToolStripMenuItem
            // 
            this.serverIPToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.serverIPToolStripMenuItem.Name = "serverIPToolStripMenuItem";
            this.serverIPToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.serverIPToolStripMenuItem.Text = "127.0.0.1";
            // 
            // serverPortToolStripMenuItem
            // 
            this.serverPortToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.serverPortToolStripMenuItem.Name = "serverPortToolStripMenuItem";
            this.serverPortToolStripMenuItem.Size = new System.Drawing.Size(90, 23);
            this.serverPortToolStripMenuItem.Text = "8000";
            // 
            // StockSecuritiesExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1309, 525);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "StockSecuritiesExchange";
            this.Text = "Stock Security Exchange";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem marketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem windowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalTileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalTileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginTradingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopTradingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem watchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockStateSummaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marketByOrderToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem marketByPriceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem askToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox userNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox serverPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox serverIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sPToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox clientPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dsToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox clientIPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aToolStripMenuItem;
    }
}