namespace StockPlot
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.screenCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.realCoordinatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onCursorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.showGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimalGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.favoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizeFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFILLER = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelTIME = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelOPEN = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveAndResize = new System.Windows.Forms.ToolStripButton();
            this.btnHorizontalShrink = new System.Windows.Forms.ToolStripButton();
            this.btnHorizontalStretch = new System.Windows.Forms.ToolStripButton();
            this.btnVerticalShrink = new System.Windows.Forms.ToolStripButton();
            this.btnVerticalStretch = new System.Windows.Forms.ToolStripButton();
            this.btnFullGraph = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripZoom = new System.Windows.Forms.ToolStripLabel();
            this.toolStripZoomText = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDotStyle = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.btnColor = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxSYMBOL = new System.Windows.Forms.ToolStripTextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripComboBoxShowTime = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabelFROM = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxFROM = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabelTO = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBoxTO = new System.Windows.Forms.ToolStripTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.doubleBufferedPanel1 = new StockPlot.DoubleBufferedPanel();
            this.gridXLabelPanel = new StockPlot.DoubleBufferedPanel();
            this.gridYLabelPanel = new StockPlot.DoubleBufferedPanel();
            this.gridPanel = new StockPlot.DoubleBufferedPanel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.recentToolStripMenuItem,
            this.favoritesToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(838, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFileLoad,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // MenuFileLoad
            // 
            this.MenuFileLoad.Name = "MenuFileLoad";
            this.MenuFileLoad.Size = new System.Drawing.Size(134, 22);
            this.MenuFileLoad.Text = "Load";
            this.MenuFileLoad.Click += new System.EventHandler(this.MenuFileLoad_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fullGraphToolStripMenuItem,
            this.toolStripSeparator5,
            this.screenCoordinatesToolStripMenuItem,
            this.realCoordinatesToolStripMenuItem,
            this.onCursorToolStripMenuItem,
            this.toolStripSeparator6,
            this.showGridToolStripMenuItem,
            this.minimalGridToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // fullGraphToolStripMenuItem
            // 
            this.fullGraphToolStripMenuItem.Name = "fullGraphToolStripMenuItem";
            this.fullGraphToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.fullGraphToolStripMenuItem.Text = "Full Graph";
            this.fullGraphToolStripMenuItem.Click += new System.EventHandler(this.toolStripButtonFullGraph);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(172, 6);
            // 
            // screenCoordinatesToolStripMenuItem
            // 
            this.screenCoordinatesToolStripMenuItem.Name = "screenCoordinatesToolStripMenuItem";
            this.screenCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.screenCoordinatesToolStripMenuItem.Text = "Actual Coordinates";
            this.screenCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.screenCoordinatesToolStripMenuItem_Click);
            // 
            // realCoordinatesToolStripMenuItem
            // 
            this.realCoordinatesToolStripMenuItem.Checked = true;
            this.realCoordinatesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.realCoordinatesToolStripMenuItem.Name = "realCoordinatesToolStripMenuItem";
            this.realCoordinatesToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.realCoordinatesToolStripMenuItem.Text = "Date Coordinates";
            this.realCoordinatesToolStripMenuItem.Click += new System.EventHandler(this.realCoordinatesToolStripMenuItem_Click);
            // 
            // onCursorToolStripMenuItem
            // 
            this.onCursorToolStripMenuItem.Checked = true;
            this.onCursorToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.onCursorToolStripMenuItem.Name = "onCursorToolStripMenuItem";
            this.onCursorToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.onCursorToolStripMenuItem.Text = "On Cursor";
            this.onCursorToolStripMenuItem.Click += new System.EventHandler(this.onCursorToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(172, 6);
            // 
            // showGridToolStripMenuItem
            // 
            this.showGridToolStripMenuItem.Checked = true;
            this.showGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGridToolStripMenuItem.Name = "showGridToolStripMenuItem";
            this.showGridToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.showGridToolStripMenuItem.Text = "Show Grid";
            this.showGridToolStripMenuItem.Click += new System.EventHandler(this.showGridToolStripMenuItem_Click);
            // 
            // minimalGridToolStripMenuItem
            // 
            this.minimalGridToolStripMenuItem.Name = "minimalGridToolStripMenuItem";
            this.minimalGridToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.minimalGridToolStripMenuItem.Text = "Minimal Grid";
            this.minimalGridToolStripMenuItem.Click += new System.EventHandler(this.minimalGridToolStripMenuItem_Click);
            // 
            // recentToolStripMenuItem
            // 
            this.recentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8});
            this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
            this.recentToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.recentToolStripMenuItem.Text = "Recent";
            this.recentToolStripMenuItem.DropDownClosed += new System.EventHandler(this.recentToolStripMenuItem_DropDownClosed);
            this.recentToolStripMenuItem.DropDownOpening += new System.EventHandler(this.recentToolStripMenuItem_DropDownOpening);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(57, 6);
            // 
            // favoritesToolStripMenuItem
            // 
            this.favoritesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.organizeFavoritesToolStripMenuItem,
            this.toolStripSeparator7});
            this.favoritesToolStripMenuItem.Name = "favoritesToolStripMenuItem";
            this.favoritesToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.favoritesToolStripMenuItem.Text = "Favorites";
            this.favoritesToolStripMenuItem.DropDownClosed += new System.EventHandler(this.favoritesToolStripMenuItem_DropDownClosed);
            this.favoritesToolStripMenuItem.DropDownOpening += new System.EventHandler(this.favoritesToolStripMenuItem_DropDownOpening);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.addToolStripMenuItem.Text = "Add...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // organizeFavoritesToolStripMenuItem
            // 
            this.organizeFavoritesToolStripMenuItem.Name = "organizeFavoritesToolStripMenuItem";
            this.organizeFavoritesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.organizeFavoritesToolStripMenuItem.Text = "Organize Favorites";
            this.organizeFavoritesToolStripMenuItem.Click += new System.EventHandler(this.organizeFavoritesToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(168, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topicsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // topicsToolStripMenuItem
            // 
            this.topicsToolStripMenuItem.Name = "topicsToolStripMenuItem";
            this.topicsToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.topicsToolStripMenuItem.Text = "View Help";
            this.topicsToolStripMenuItem.Click += new System.EventHandler(this.topicsToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabelFILLER,
            this.toolStripStatusLabelTIME,
            this.toolStripStatusLabelOPEN});
            this.statusStrip1.Location = new System.Drawing.Point(0, 548);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(838, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // toolStripStatusLabelFILLER
            // 
            this.toolStripStatusLabelFILLER.Name = "toolStripStatusLabelFILLER";
            this.toolStripStatusLabelFILLER.Size = new System.Drawing.Size(716, 17);
            this.toolStripStatusLabelFILLER.Spring = true;
            // 
            // toolStripStatusLabelTIME
            // 
            this.toolStripStatusLabelTIME.Name = "toolStripStatusLabelTIME";
            this.toolStripStatusLabelTIME.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabelTIME.Text = "Time";
            // 
            // toolStripStatusLabelOPEN
            // 
            this.toolStripStatusLabelOPEN.Name = "toolStripStatusLabelOPEN";
            this.toolStripStatusLabelOPEN.Size = new System.Drawing.Size(34, 17);
            this.toolStripStatusLabelOPEN.Text = "open";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.toolStripSeparator4,
            this.btnMoveAndResize,
            this.btnHorizontalShrink,
            this.btnHorizontalStretch,
            this.btnVerticalShrink,
            this.btnVerticalStretch,
            this.btnFullGraph,
            this.toolStripSeparator2,
            this.toolStripZoom,
            this.toolStripZoomText,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.toolStripDotStyle,
            this.toolStripLabel2,
            this.btnColor,
            this.toolStripSeparator3,
            this.btnRefresh,
            this.toolStripLabel5,
            this.toolStripTextBoxSYMBOL});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(838, 32);
            this.toolStrip1.TabIndex = 6;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 29);
            this.btnOpen.Text = "toolStripButton1";
            this.btnOpen.Click += new System.EventHandler(this.MenuFileLoad_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // btnMoveAndResize
            // 
            this.btnMoveAndResize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveAndResize.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveAndResize.Image")));
            this.btnMoveAndResize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveAndResize.Name = "btnMoveAndResize";
            this.btnMoveAndResize.Size = new System.Drawing.Size(23, 29);
            this.btnMoveAndResize.Text = "Move and Resize";
            this.btnMoveAndResize.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // btnHorizontalShrink
            // 
            this.btnHorizontalShrink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHorizontalShrink.Image = ((System.Drawing.Image)(resources.GetObject("btnHorizontalShrink.Image")));
            this.btnHorizontalShrink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHorizontalShrink.Name = "btnHorizontalShrink";
            this.btnHorizontalShrink.Size = new System.Drawing.Size(23, 29);
            this.btnHorizontalShrink.Text = "Shrink Horizontally";
            this.btnHorizontalShrink.Click += new System.EventHandler(this.btnHorizontalShrink_Click);
            // 
            // btnHorizontalStretch
            // 
            this.btnHorizontalStretch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHorizontalStretch.Image = ((System.Drawing.Image)(resources.GetObject("btnHorizontalStretch.Image")));
            this.btnHorizontalStretch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHorizontalStretch.Name = "btnHorizontalStretch";
            this.btnHorizontalStretch.Size = new System.Drawing.Size(23, 29);
            this.btnHorizontalStretch.Text = "Stretch Horizontally";
            this.btnHorizontalStretch.Click += new System.EventHandler(this.btnHorizontalStretch_Click);
            // 
            // btnVerticalShrink
            // 
            this.btnVerticalShrink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVerticalShrink.Image = ((System.Drawing.Image)(resources.GetObject("btnVerticalShrink.Image")));
            this.btnVerticalShrink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVerticalShrink.Name = "btnVerticalShrink";
            this.btnVerticalShrink.Size = new System.Drawing.Size(23, 29);
            this.btnVerticalShrink.Text = "Shrink Vertically";
            this.btnVerticalShrink.Click += new System.EventHandler(this.btnVerticalShrink_Click);
            // 
            // btnVerticalStretch
            // 
            this.btnVerticalStretch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnVerticalStretch.Image = ((System.Drawing.Image)(resources.GetObject("btnVerticalStretch.Image")));
            this.btnVerticalStretch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVerticalStretch.Name = "btnVerticalStretch";
            this.btnVerticalStretch.Size = new System.Drawing.Size(23, 29);
            this.btnVerticalStretch.Text = "Stretch Vertically";
            this.btnVerticalStretch.Click += new System.EventHandler(this.btnVerticalStretch_Click);
            // 
            // btnFullGraph
            // 
            this.btnFullGraph.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullGraph.Image = ((System.Drawing.Image)(resources.GetObject("btnFullGraph.Image")));
            this.btnFullGraph.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullGraph.Name = "btnFullGraph";
            this.btnFullGraph.Size = new System.Drawing.Size(23, 29);
            this.btnFullGraph.Text = "Full Graph";
            this.btnFullGraph.Click += new System.EventHandler(this.toolStripButtonFullGraph);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripZoom
            // 
            this.toolStripZoom.Name = "toolStripZoom";
            this.toolStripZoom.Size = new System.Drawing.Size(42, 29);
            this.toolStripZoom.Text = "Zoom:";
            // 
            // toolStripZoomText
            // 
            this.toolStripZoomText.Name = "toolStripZoomText";
            this.toolStripZoomText.Size = new System.Drawing.Size(35, 29);
            this.toolStripZoomText.Text = "100%";
            this.toolStripZoomText.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStripZoomText_Paint);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(57, 29);
            this.toolStripLabel1.Text = "Dot Style:";
            // 
            // toolStripDotStyle
            // 
            this.toolStripDotStyle.Items.AddRange(new object[] {
            "Dots and Lines",
            "Candlesticks"});
            this.toolStripDotStyle.Name = "toolStripDotStyle";
            this.toolStripDotStyle.Size = new System.Drawing.Size(121, 32);
            this.toolStripDotStyle.Text = "Choose...";
            this.toolStripDotStyle.TextChanged += new System.EventHandler(this.toolStripDotStyle_TextChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(42, 29);
            this.toolStripLabel2.Text = "Color: ";
            // 
            // btnColor
            // 
            this.btnColor.BackColor = System.Drawing.Color.Green;
            this.btnColor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnColor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(23, 29);
            this.btnColor.Text = "Choose Color";
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Enabled = false;
            this.btnRefresh.Image = global::StockPlot.Properties.Resources.refresh1;
            this.btnRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(29, 29);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(53, 29);
            this.toolStripLabel5.Text = "Symbol: ";
            // 
            // toolStripTextBoxSYMBOL
            // 
            this.toolStripTextBoxSYMBOL.Name = "toolStripTextBoxSYMBOL";
            this.toolStripTextBoxSYMBOL.Size = new System.Drawing.Size(100, 32);
            this.toolStripTextBoxSYMBOL.Leave += new System.EventHandler(this.toolStripTextBoxSYMBOL_Leave);
            this.toolStripTextBoxSYMBOL.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSYMBOL_KeyPress);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxShowTime,
            this.toolStripLabelFROM,
            this.toolStripTextBoxFROM,
            this.toolStripLabelTO,
            this.toolStripTextBoxTO});
            this.toolStrip2.Location = new System.Drawing.Point(0, 56);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(838, 25);
            this.toolStrip2.TabIndex = 8;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripComboBoxShowTime
            // 
            this.toolStripComboBoxShowTime.Enabled = false;
            this.toolStripComboBoxShowTime.Items.AddRange(new object[] {
            "5 days",
            "15 days",
            "1 Month",
            "2 Months",
            "3 Months",
            "6 Months",
            "1 Year",
            "2 Years",
            "5 Years"});
            this.toolStripComboBoxShowTime.Name = "toolStripComboBoxShowTime";
            this.toolStripComboBoxShowTime.Size = new System.Drawing.Size(80, 25);
            this.toolStripComboBoxShowTime.Text = "1 Month";
            this.toolStripComboBoxShowTime.TextChanged += new System.EventHandler(this.toolStripComboBoxShowTime_TextChanged);
            // 
            // toolStripLabelFROM
            // 
            this.toolStripLabelFROM.Enabled = false;
            this.toolStripLabelFROM.Name = "toolStripLabelFROM";
            this.toolStripLabelFROM.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabelFROM.Text = "FROM:";
            // 
            // toolStripTextBoxFROM
            // 
            this.toolStripTextBoxFROM.Enabled = false;
            this.toolStripTextBoxFROM.Name = "toolStripTextBoxFROM";
            this.toolStripTextBoxFROM.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxFROM.Enter += new System.EventHandler(this.toolStripTextBoxFROM_Enter);
            this.toolStripTextBoxFROM.Leave += new System.EventHandler(this.toolStripTextBoxFROM_Leave);
            this.toolStripTextBoxFROM.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxFROM_KeyPress);
            // 
            // toolStripLabelTO
            // 
            this.toolStripLabelTO.Enabled = false;
            this.toolStripLabelTO.Name = "toolStripLabelTO";
            this.toolStripLabelTO.Size = new System.Drawing.Size(26, 22);
            this.toolStripLabelTO.Text = "TO:";
            // 
            // toolStripTextBoxTO
            // 
            this.toolStripTextBoxTO.Enabled = false;
            this.toolStripTextBoxTO.Name = "toolStripTextBoxTO";
            this.toolStripTextBoxTO.Size = new System.Drawing.Size(100, 25);
            this.toolStripTextBoxTO.Enter += new System.EventHandler(this.toolStripTextBoxTO_Enter);
            this.toolStripTextBoxTO.Leave += new System.EventHandler(this.toolStripTextBoxTO_Leave);
            this.toolStripTextBoxTO.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxTO_KeyPress);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // doubleBufferedPanel1
            // 
            this.doubleBufferedPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.doubleBufferedPanel1.BackColor = System.Drawing.Color.Black;
            this.doubleBufferedPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.doubleBufferedPanel1.Location = new System.Drawing.Point(0, 520);
            this.doubleBufferedPanel1.Name = "doubleBufferedPanel1";
            this.doubleBufferedPanel1.Size = new System.Drawing.Size(838, 28);
            this.doubleBufferedPanel1.TabIndex = 8;
            // 
            // gridXLabelPanel
            // 
            this.gridXLabelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridXLabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridXLabelPanel.Location = new System.Drawing.Point(75, 441);
            this.gridXLabelPanel.Name = "gridXLabelPanel";
            this.gridXLabelPanel.Size = new System.Drawing.Size(751, 79);
            this.gridXLabelPanel.TabIndex = 7;
            this.gridXLabelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gridXLabelPanel_Paint);
            this.gridXLabelPanel.Resize += new System.EventHandler(this.gridXLabelPanel_Resize);
            // 
            // gridYLabelPanel
            // 
            this.gridYLabelPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridYLabelPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridYLabelPanel.Location = new System.Drawing.Point(12, 84);
            this.gridYLabelPanel.Name = "gridYLabelPanel";
            this.gridYLabelPanel.Size = new System.Drawing.Size(64, 358);
            this.gridYLabelPanel.TabIndex = 5;
            this.gridYLabelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gridYLabelPanel_Paint);
            this.gridYLabelPanel.Resize += new System.EventHandler(this.gridYLabelPanel_Resize);
            // 
            // gridPanel
            // 
            this.gridPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridPanel.AutoScroll = true;
            this.gridPanel.AutoSize = true;
            this.gridPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(61)))), ((int)(((byte)(73)))));
            this.gridPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridPanel.Location = new System.Drawing.Point(75, 84);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(751, 358);
            this.gridPanel.TabIndex = 4;
            this.gridPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gridPanel_Paint);
            this.gridPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gridPanel_MouseDown);
            this.gridPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridPanel_MouseMove);
            this.gridPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gridPanel_MouseUp);
            this.gridPanel.Resize += new System.EventHandler(this.gridPanel_Resize);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 570);
            this.Controls.Add(this.doubleBufferedPanel1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.gridXLabelPanel);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gridYLabelPanel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.gridPanel);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "Form1";
            this.Text = "Stock Plotter";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private DoubleBufferedPanel gridPanel;
        private DoubleBufferedPanel gridYLabelPanel;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnMoveAndResize;
        private System.Windows.Forms.ToolStripButton btnFullGraph;
        private DoubleBufferedPanel gridXLabelPanel;
        private System.Windows.Forms.ToolStripMenuItem MenuFileLoad;
        private System.Windows.Forms.ToolStripLabel toolStripZoom;
        private System.Windows.Forms.ToolStripLabel toolStripZoomText;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripDotStyle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem screenCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem realCoordinatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnHorizontalStretch;
        private System.Windows.Forms.ToolStripButton btnHorizontalShrink;
        private System.Windows.Forms.ToolStripButton btnVerticalStretch;
        private System.Windows.Forms.ToolStripButton btnVerticalShrink;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton btnColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem fullGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem showGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimalGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem organizeFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem onCursorToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxShowTime;
        private System.Windows.Forms.ToolStripLabel toolStripLabelFROM;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxFROM;
        private System.Windows.Forms.ToolStripLabel toolStripLabelTO;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxTO;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSYMBOL;
        private DoubleBufferedPanel doubleBufferedPanel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTIME;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOPEN;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFILLER;
    }
}

