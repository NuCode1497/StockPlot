//**************************************************************
//   (c) Copyright 2010 Cody Neuburger
//          ALL RIGHTS RESERVED.
//**************************************************************
#region using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
#endregion

namespace StockPlot
{
    public partial class Form1 : Form
    {
        #region variables

        Settings settings;
        string activeTicker;
        DateTime to;
        DateTime from;

        #region realtime variables
        List<Ticker> tickers = new List<Ticker>();
        List<string> recentSymbols = new List<string>();
        bool realtime;
        #endregion

        #region grid variables
        List<PointF> orgpointList;
        List<PointF> pointList;
        List<PointF> spointList;
        float rminX, rmaxX;
        float rminY, rmaxY;
        float gminX, gmaxX;
        float gminY, gmaxY;
        float sminX, smaxX;
        float sminY, smaxY;
        float xInterval;
        float yInterval;
        List<candlestick> candlesticks = new List<candlestick>();
        List<float> yLabels = new List<float>();
        List<float> xLabels = new List<float>();
        Color penColor;

        //selection
        RectangleF selection;
        bool selectionDefault = true;
        bool moveMode = false;
        bool resizeMode = false;
        bool mouseIsDown;
        PointF mouseStart;
        PointF mouseEnd;
        PointF relativeToGraph;
        PointF orgRelativeToGraph;
        PointF orgGraphLocation;
        PointF orgRectLocation;
        SizeF graphSize;
        RectangleF rect = Rectangle.Empty;
        float zoomStrength = 1.00f;
        float maxZoomStrength = 1000;
        #endregion

        #endregion
        #region Form1 stuff

        public Form1()
        {
            InitializeComponent();
            
            timer1.Start();
            settings = new Settings();

            to = DateTime.Now;
            from = to.AddMonths(-1);

            gridPanel.MouseWheel += new MouseEventHandler(gridPanel_MouseWheel);

            penColor = btnColor.BackColor;
            selection = gridPanel.ClientRectangle;
            GetPanelMaxMin(gridPanel);
            loadArray(false);
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            gridPanel.Focus();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.StartPosition = FormStartPosition.CenterParent;
            about.Show();
        }
        private void topicsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void toolStripDotStyle_TextChanged(object sender, EventArgs e)
        {
            if (toolStripDotStyle.Text == "Candlesticks")
            {
                toolStripLabel2.Enabled = false;
                btnColor.Enabled = false;
            }
            else
            {
                toolStripLabel2.Enabled = true;
                btnColor.Enabled = true;
            }
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoritesAdd adder = new FavoritesAdd();
            adder.StartPosition = FormStartPosition.CenterParent;
            if (adder.ShowDialog() == DialogResult.OK)
            {
                adder.result = adder.result.ToUpper();

                //find existing favorite
                bool exists = false;
                foreach(ToolStripItem tsi in favoritesToolStripMenuItem.DropDownItems)
                {
                    if (tsi.Text == adder.result) exists = true;
                }

                if (!exists)
                {
                    try
                    {
                        realtime = false;
                        activeTicker = adder.result;
                        String fileName = "http://ichart.finance.yahoo.com/table.csv?s=" + adder.result +
                                          "&a=" + (settings.startMonth - 1) +
                                          "&b=" + settings.startDay +
                                          "&c=" + settings.startYear +
                                          "&d=" + (settings.endMonth - 1) +
                                          "&e=" + settings.endDay +
                                          "&f=" + settings.endYear +
                                          "&g=d&ignore=.csv";
                        loadCSVFromURL(fileName, false);
                        //force refresh on to and from boxes
                        toolStripTextBoxFROM.Focus();
                        toolStripTextBoxTO.Focus();
                        gridPanel.Focus();

                        //save Favorites            
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Favorites.txt", true))
                        {
                            file.WriteLine(adder.result);
                        }
                    }
                    catch
                    {
                        Error err = new Error();
                        err.label1.Text = "Couldn't find the symbol '" + adder.result + "'.";
                        err.label2.Text = "Check that your internet connection is active \nand that the symbol exists on ";
                        err.linkLabel1.Text = "http://finance.yahoo.com/";
                        err.ShowDialog();
                    }
                }
                else
                {
                    Error err = new Error();
                    err.label1.Text = "Symbol already in Favorites.";
                    err.label2.Text = "";
                    err.ShowDialog();
                }
            }
        }
        void tsmi_Click(object sender, EventArgs e)
        {
            activeTicker = ((ToolStripMenuItem)sender).Text;
            try
            {
                if (realtime)
                {
                    loadCSVFromURL("http://download.finance.yahoo.com/d/quotes.csv?s=" + activeTicker + "&f=sl1d1t1c1ohgv&e=.csv", false);
                }
                else
                {
                    String fileName = "http://ichart.finance.yahoo.com/table.csv?s=" + activeTicker +
                                      "&a=" + (settings.startMonth - 1) +
                                      "&b=" + settings.startDay +
                                      "&c=" + settings.startYear +
                                      "&d=" + (settings.endMonth - 1) +
                                      "&e=" + settings.endDay +
                                      "&f=" + settings.endYear +
                                      "&g=d&ignore=.csv";
                    loadCSVFromURL(fileName, false);
                    //force refresh on to and from boxes
                    toolStripTextBoxFROM.Focus();
                    toolStripTextBoxTO.Focus();
                    gridPanel.Focus();
                }
                gridPanel.Invalidate();
            }
            catch
            {
                Error err = new Error();
                err.label1.Text = "Couldn't find the symbol '" + activeTicker + "'.";
                err.label2.Text = "Check that your internet connection is active and that the symbol exists on http://finance.yahoo.com/";
                err.ShowDialog();
            }
        }
        private void favoritesToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            //removing items on close will prevent showing items that were removed by the organize dialog
            List<string> symbols = new List<string>();

            try
            {
                using (StreamReader file = new StreamReader(@"Favorites.txt"))
                {
                    while (!file.EndOfStream)
                    {
                        symbols.Add(file.ReadLine());
                    }
                }

                foreach (string symbol in symbols)
                {
                    favoritesToolStripMenuItem.DropDownItems.RemoveByKey(symbol);
                }
            }
            catch
            {
                return;
            }
        }
        private void favoritesToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            List<string> symbols = new List<string>();

            if (!File.Exists(@"Favorites.txt"))
            {
                File.Create(@"Favorites.txt");
            }
            try
            {
                using (StreamReader file = new StreamReader(@"Favorites.txt"))
                {
                    while (!file.EndOfStream)
                    {
                        symbols.Add(file.ReadLine());
                    }
                }

                foreach (string symbol in symbols)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(symbol);
                    tsmi.Click += new EventHandler(tsmi_Click);
                    tsmi.Name = symbol;
                    favoritesToolStripMenuItem.DropDownItems.Add(tsmi);
                }
            }
            catch
            {
                return;
            }
        }
        private void organizeFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrganizeFavorites organizer = new OrganizeFavorites();
            organizer.StartPosition = FormStartPosition.CenterParent;
            organizer.ShowDialog();
        }
        private void onCursorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onCursorToolStripMenuItem.Checked = !onCursorToolStripMenuItem.Checked;
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshSymbol();
        }
        private void refreshSymbol()
        {

            try
            {
                if (realtime)
                {
                    loadCSVFromURL("http://download.finance.yahoo.com/d/quotes.csv?s=" + activeTicker + "&f=sl1d1t1c1ohgv&e=.csv", false);
                }
                else
                {
                    String fileName = "http://ichart.finance.yahoo.com/table.csv?s=" + activeTicker +
                                      "&a=" + (settings.startMonth - 1) +
                                      "&b=" + settings.startDay +
                                      "&c=" + settings.startYear +
                                      "&d=" + (settings.endMonth - 1) +
                                      "&e=" + settings.endDay +
                                      "&f=" + settings.endYear +
                                      "&g=d&ignore=.csv";
                    loadCSVFromURL(fileName, false);
                }
                toolStripTextBoxFROM.Text = from.ToString("MMM d yyyy");
                toolStripTextBoxTO.Text = to.ToString("MMM d yyyy");
                gridPanel.Invalidate();
            }
            catch
            {
                Error err = new Error();
                err.label1.Text = "Couldn't find the symbol '" + activeTicker + "'.";
                err.label2.Text = "Check that your internet connection is active \nand that the symbol exists on ";
                err.linkLabel1.Text = "http://finance.yahoo.com/";
                err.ShowDialog();
            }
        }
        private void recentToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            foreach (string symbol in recentSymbols)
            {
                ToolStripMenuItem tsmi = new ToolStripMenuItem(symbol);
                tsmi.Click += new EventHandler(tsmi_Click);
                tsmi.Name = symbol;
                recentToolStripMenuItem.DropDownItems.Add(tsmi);
            }
        }
        private void recentToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            foreach (string symbol in recentSymbols)
            {
                recentToolStripMenuItem.DropDownItems.RemoveByKey(symbol);
            }
        }
        private void toolStripTextBoxSYMBOL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gridPanel.Focus();
            }
        }
        private void toolStripTextBoxSYMBOL_Leave(object sender, EventArgs e)
        {
            toolStripTextBoxSYMBOL.Text = toolStripTextBoxSYMBOL.Text.ToUpper();
            try
            {
                activeTicker = toolStripTextBoxSYMBOL.Text;
                realtime = false;
                String fileName = "http://ichart.finance.yahoo.com/table.csv?s=" + activeTicker +
                                  "&a=" + (settings.startMonth - 1) +
                                  "&b=" + settings.startDay +
                                  "&c=" + settings.startYear +
                                  "&d=" + (settings.endMonth - 1) +
                                  "&e=" + settings.endDay +
                                  "&f=" + settings.endYear +
                                  "&g=d&ignore=.csv";
                loadCSVFromURL(fileName, false);
            }
            catch
            {
                Error err = new Error();
                err.label1.Text = "Couldn't find the symbol '" + toolStripTextBoxSYMBOL.Text + "'.";
                err.label2.Text = "Check that your internet connection is active \nand that the symbol exists on ";
                err.linkLabel1.Text = "http://finance.yahoo.com/";
                err.ShowDialog();
            }
        }

        #endregion
        #region loading stuff

        private void MenuFileLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "csv files (*.csv) | *.csv";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                realtime = false;
                loadCSV(ofd.FileName, false);
            }
        }
        private void loadCSV(string fileName, bool refresh)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!refresh) candlesticks = new List<candlestick>();
                load(fileName);
                loadArray(true);
                gridPanel.Invalidate();
                online(false);
            }
            catch
            {
                Cursor = Cursors.Default;
                Error err = new Error();
                err.label1.Text = "Could not load file";
                err.label2.Text = "Check that the file is in the \ncorrect CSV format.";
                err.ShowDialog();
            }
            Cursor = Cursors.Default;
        }
        private void loadCSVFromURL(string fileName, bool refresh)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (!refresh) candlesticks = new List<candlestick>();
                loadFromURL(fileName);
                loadArray(true);
                gridPanel.Invalidate();
                if (!recentSymbols.Contains(activeTicker)) recentSymbols.Add(activeTicker);
                online(true);
            }
            catch(Exception e)
            {
                Cursor = Cursors.Default;
                throw e;
            }
            Cursor = Cursors.Default;
        }
        private void online(bool b)
        {
            btnRefresh.Enabled = b;
            toolStripLabelFROM.Enabled = b;
            toolStripLabelTO.Enabled = b;
            toolStripTextBoxFROM.Enabled = b;
            toolStripTextBoxTO.Enabled = b;
            toolStripComboBoxShowTime.Enabled = b;
        }
        public void loadFromURL(string fileName)
        {
            try
            {
                string inline;
                System.Net.WebClient client = new WebClient();
                using (StreamReader reader = new StreamReader(client.OpenRead(fileName)))
                {
                    inline = reader.ReadLine();
                    while (inline != null)
                    {
                        inline = reader.ReadLine();
                        if (inline != null) update(inline);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public void load(string filename)
        {
            string inline;
            using (TextReader tr = new StreamReader(filename))
            {
                inline = tr.ReadLine();
                while (inline != null)
                {
                    inline = tr.ReadLine();
                    if (inline != null) update(inline);
                }
            }
        }
        private void update(string s)
        {
            if (realtime)
            {
                List<string> tokens;
                parse(out tokens, s);
                tokens[0] = tokens[0].Substring(1, tokens[0].Length - 1); //remove quotes
                tokens[2] = tokens[2].Substring(1, tokens[2].Length - 1); //remove quotes
                tokens[3] = tokens[3].Substring(1, tokens[3].Length - 1); //remove quotes

                Ticker ticker = new Ticker();
                ticker.symbol = tokens[0];
                ticker.last = Convert.ToDouble(tokens[1]);
                String[] datePieces = tokens[2].Split('/');
                tokens[3].Insert(tokens[3].Length - 3, ":");
                String[] timePieces = tokens[3].Split(':');
                if(timePieces[2] == "pm") timePieces[0] = Convert.ToString(Convert.ToInt32(timePieces[0]) + 12);
                ticker.time = new DateTime(Convert.ToInt32(datePieces[2]),Convert.ToInt32(datePieces[0]),Convert.ToInt32(datePieces[1]),Convert.ToInt32(timePieces[0]),Convert.ToInt32(timePieces[1]),0);
                ticker.change = Convert.ToInt32(tokens[4]);
                ticker.perChange = Convert.ToInt32(tokens[4]) / Convert.ToInt32(tokens[1]);

                candlestick cs = new candlestick();
                cs.date = ticker.time;
                cs.close = ticker.last;
                candlesticks.Add(cs);
            }
            else
            {
                List<string> tokens;
                parse(out tokens, s);
                String[] datePieces = tokens[0].Split('-');
                candlestick cs = new candlestick();
                cs.date = new DateTime(Convert.ToInt32(datePieces[0]), Convert.ToInt32(datePieces[1]), Convert.ToInt32(datePieces[2]), 16, 0, 0);
                cs.open = Convert.ToDouble(tokens[1]);
                cs.high = Convert.ToDouble(tokens[2]);
                cs.low = Convert.ToDouble(tokens[3]);
                cs.close = Convert.ToDouble(tokens[4]);
                candlesticks.Add(cs);
            }
        }
        static private void parse(out List<string> tokens, string s)
        {
            String[] t = s.Split(',');
            tokens = new List<string>();
            tokens = t.ToList();
        }

        #endregion
        #region grid stuff

        PointF transformToScreenPoint(PointF realPoint)
        {
            PointF screenPoint = new PointF();

            float rRangeX = rmaxX - rminX;
            float sRangeX = smaxX - sminX;
            screenPoint.X = (((realPoint.X - rminX) / rRangeX) * sRangeX) + sminX;

            float rRangeY = rmaxY - rminY;
            float sRangeY = smaxY - sminY;
            screenPoint.Y = (((realPoint.Y - rminY) / rRangeY) * sRangeY) + sminY;
            return screenPoint;
        }
        PointF transformToRealPoint(PointF screenPoint)
        {
            PointF realPoint = new PointF();

            float rRangeX = rmaxX - rminX;
            float sRangeX = smaxX - sminX;
            realPoint.X = (((screenPoint.X - sminX) / sRangeX) * rRangeX) + rminX;

            float rRangeY = rmaxY - rminY;
            float sRangeY = smaxY - sminY;
            realPoint.Y = (((screenPoint.Y - sminY) / sRangeY) * rRangeY) + rminY;
            return realPoint;
        }
        private void setLimits(PointF point)
        {
            if (point.X > rmaxX) rmaxX = point.X;
            if (point.X < rminX) rminX = point.X;
            if (point.Y > rmaxY) rmaxY = point.Y;
            if (point.Y < rminY) rminY = point.Y;
        }
        static void adjustLimits(ref float max, ref float min)
        {
            float numax, numin;
            float range = max - min;
            float interval = (float)Math.Pow(10.0, (Math.Floor(Math.Log10((double)range))) - 1.0f);
            if (max >= 0f)
            {
                for (numax = 0f; numax < max; numax += interval) { }
            }
            else
            {
                for (numax = 0f; numax > max; numax -= interval) { }
                numax += interval;
            }

            if (min <= 0f)
            {
                for (numin = 0f; numin > min; numin -= interval) { }
            }
            else
            {
                for (numin = 0f; numin < min; numin += interval) { }
                numin -= interval;
            }
            max = numax;
            min = numin;
        }
        private void loadArray(bool usingCSV)
        {
            int NumberOfPoints = 400;
            rmaxX = -10000000.0f;
            rminX = 10000000.0f;
            rmaxY = -10000000.0f;
            rminY = 10000000.0f;
            
            //initialize
            orgpointList = new List<PointF>();
            pointList = new List<PointF>();
            spointList = new List<PointF>();

            //initialize members
            //orgpointList
            PointF close = new PointF();
            PointF open = new PointF();
            PointF high = new PointF();
            PointF low = new PointF();
            if (!usingCSV)
            {
                candlesticks = new List<candlestick>();
                DateTime century = new DateTime(1900,1,1);
                long day = new DateTime(1900,1,2).Ticks - century.Ticks;
                for (int i = 0; i < NumberOfPoints; i++)
                {
                    close.X = i;
                    close.Y = (float)(Math.Sin(Math.Sqrt((double)close.X))) * i * i;
                    orgpointList.Add(close);
                    candlesticks.Add(new candlestick(new DateTime(century.Ticks + day * i),-1,-1,-1,close.Y));
                    setLimits(close);
                }
            }
            else
            {
                if (realtime)
                {
                    foreach (candlestick cs in candlesticks)
                    {
                        close.X = (float)cs.date.ToOADate();
                        close.Y = (float)cs.close;
                        orgpointList.Add(close);
                        setLimits(close);
                    }

                    //dummy points that will tell the grid to focus the entire day
                    PointF start = new PointF();
                    start.X = (float)(new DateTime(candlesticks.Last().date.Year, candlesticks.Last().date.Month, candlesticks.Last().date.Day, 8, 30, 0)).ToOADate();
                    start.Y = (float)candlesticks.Last().close;
                    PointF end = new PointF();
                    end.X = (float)(new DateTime(candlesticks.Last().date.Year, candlesticks.Last().date.Month, candlesticks.Last().date.Day, 17, 0, 0)).ToOADate();
                    end.Y = (float)candlesticks.Last().close;
                }
                else
                {
                    foreach (candlestick cs in candlesticks)
                    {
                        close.X = (float)cs.date.ToOADate();
                        close.Y = (float)cs.close;
                        orgpointList.Add(close);
                        setLimits(close);

                        open.X = (float)cs.date.ToOADate();
                        open.Y = (float)cs.open;
                        orgpointList.Add(open);
                        setLimits(open);

                        high.X = (float)cs.date.ToOADate();
                        high.Y = (float)cs.high;
                        orgpointList.Add(high);
                        setLimits(high);

                        low.X = (float)cs.date.ToOADate();
                        low.Y = (float)cs.low;
                        orgpointList.Add(low);
                        setLimits(low);
                    }
                }
            }
            adjustLimits(ref rmaxX, ref rminX);
            adjustLimits(ref rmaxY, ref rminY);

            //pointList
            foreach (PointF point in orgpointList)
            {
                pointList.Add(point);
            }

            gmaxX = rmaxX;
            gmaxY = rmaxY;
            gminX = rminX;
            gminY = rminY;

            zoomStrength = 1.00f;
            toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
            selection = gridPanel.ClientRectangle;
            selectionDefault = true;
            moveAndResizeMode = false;
        }
        private void cropArray()
        {
            gmaxX = -10000000.0f;
            gminX = 10000000.0f;

            gmaxY = -10000000.0f;
            gminY = 10000000.0f;
            
            foreach (PointF point in pointList)
            {
                if (point.X > gmaxX) gmaxX = point.X;
                if (point.X < gminX) gminX = point.X;
                if (point.Y > gmaxY) gmaxY = point.Y;
                if (point.Y < gminY) gminY = point.Y;
            }
            adjustLimits(ref gmaxX, ref gminX);
            adjustLimits(ref gmaxY, ref gminY);
        }
        private void LabelHorizantalGridLine(Graphics g, float y)
        {
            Font font = new Font("Arial", 8);
            PointF rPoint = new PointF();
            PointF sPoint;
            rPoint.Y = y;
            rPoint.X = 0;
            sPoint = transformToScreenPoint(rPoint);
            sPoint.X = 65;
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Far;
            strFormat.LineAlignment = StringAlignment.Center;
            String s = y.ToString() + " ―";
            while (s.Length < 10) s = " " + s;
            g.DrawString(s, font, Brushes.Black, sPoint, strFormat);
        } 
        private void LabelVerticalGridLine(Graphics g, float x)
        {
            Font font = new Font("Arial", 9);
            Pen pen = new Pen(new SolidBrush(Color.Black));
            PointF rPoint = new PointF();
            PointF sPoint;
            rPoint.Y = 0;
            rPoint.X = x;
            sPoint = transformToScreenPoint(rPoint);
            sPoint.Y = sPoint.X;
            sPoint.X = -5;
            PointF offset = new PointF(sPoint.X + 20, sPoint.Y);
            StringFormat strFormat = new StringFormat();
            //strFormat.FormatFlags = StringFormatFlags.DirectionVertical;
            strFormat.Alignment = StringAlignment.Far;
            strFormat.LineAlignment = StringAlignment.Near;
            DateTime dt = DateTime.FromOADate(x);
            String s;
            if (realtime)
            {
                s = dt.ToString("MMM d h:mmtt");
            }
            else
            {
                s = dt.ToString("yyyy MMM d");
            }
            while (s.Length < 10) s = " " + s;
            g.RotateTransform(-90);
            g.DrawString(s, font, Brushes.Black, sPoint, strFormat);
            g.DrawLine(pen, offset, sPoint);
            g.RotateTransform(90);
        }
        private void DrawCoordinates(Graphics g, PointF p, Color c)
        {
            PointF pReal = transformToRealPoint(p);
            PointF offset = new PointF(p.X + 20, p.Y + 20);
            Font font = new Font("Times New Roman", 10, FontStyle.Bold);
            StringFormat strFormat = new StringFormat();
            strFormat.Alignment = StringAlignment.Near;
            strFormat.LineAlignment = StringAlignment.Center;
            DateTime dt = DateTime.FromOADate(pReal.X);
            String X;
            if (realtime)
            {
                X = dt.ToString("yyyy MMM d h:mmtt");
            }
            else
            {
                X = dt.ToString("yyyy MMM d");
            }
            String s;
            if (realCoordinatesToolStripMenuItem.Checked)
            {
                s = "(" + X + ", " + pReal.Y + ")";
            }
            else
            {
                s = "(" + pReal.X + ", " + pReal.Y + ")";
            }
            while (s.Length < 10) s = " " + s;

            if (onCursorToolStripMenuItem.Checked)
            {
                g.DrawString(s, font, new SolidBrush(c), offset, strFormat);
                g.DrawLine(new Pen(new SolidBrush(c)), offset, p);
            }
            toolStripStatusLabel1.Text = s;

        }
        private void DrawOneGridLine(Graphics g, Pen pen, float pos, int axis)
        {
            PointF start = new PointF();
            PointF end = new PointF();
            switch (axis)
            {
                case 1:
                    start.X = gminX;
                    start.Y = pos;
                    end.X = gmaxX;
                    end.Y = pos;
                    break;
                case 2:
                    start.Y = gminY;
                    start.X = pos;
                    end.Y = gmaxY;
                    end.X = pos;
                    break;
                default:
                    return;
            }
            PointF sStart = transformToScreenPoint(start);
            PointF sEnd = transformToScreenPoint(end);
            g.DrawLine(pen, sStart, sEnd);
        }
        private void drawHorizantalGridLines(Graphics g, bool MajorGridLines, Color col)
        {
            Pen pen = new Pen(col, 1.0f);
            Graphics gYLabel = gridYLabelPanel.CreateGraphics();
            float Yrange = rmaxY - rminY;
            float sYrange = sminY - smaxY;
            float interval;
            float high;
            float low;
            float labelMod;

            if (MajorGridLines)
            {
                interval = (float)Math.Pow(10.0, (Math.Floor(Math.Log10((double)Yrange))));
                high = gmaxY;
                low = gminY;

                //dynamic grid
                //shrinking
                while ((sYrange / (Yrange / interval)) < 100)
                {
                    interval *= 2;
                }
                //expanding
                while ((sYrange / (Yrange / interval)) > 200)
                {
                    interval /= 2;
                }

                //draw lines
                for (float y = 0.0f; y <= high; y += interval)
                {
                    if (y >= low)
                    {
                        DrawOneGridLine(g, pen, y, 1);
                    }
                }
                for (float y = 0.0f; y >= low; y -= interval)
                {
                    if (y <= high)
                    {
                        DrawOneGridLine(g, pen, y, 1);
                    }
                }
            }
            else
            {
                interval = (float)Math.Pow(10.0, (Math.Floor(Math.Log10((double)Yrange))) - 1.0f);
                yInterval = interval;
                high = gmaxY;
                low = gminY;
                labelMod = 2; //place a label every x lines

                //dynamic grid
                //shrinking
                while ((sYrange / (Yrange / interval)) < 10)
                {
                    interval *= 2;
                }
                //expanding
                while ((sYrange / (Yrange / interval)) > 20)
                {
                    interval /= 2;
                }

                //draw lines
                float labelPlacer = labelMod;
                yLabels.Clear();
                for (float y = 0.0f; y <= high; y += interval)
                {
                    if (y >= low)
                    {
                        if (labelPlacer == labelMod) yLabels.Add(y);
                        if (!minimalGridToolStripMenuItem.Checked) DrawOneGridLine(g, pen, y, 1);
                    }
                    labelPlacer--;
                    if (labelPlacer == 0) labelPlacer = labelMod;
                }
                labelPlacer = labelMod;
                for (float y = 0.0f; y >= low; y -= interval)
                {
                    if (y <= high)
                    {
                        if (labelPlacer == labelMod) yLabels.Add(y);
                        if (!minimalGridToolStripMenuItem.Checked) DrawOneGridLine(g, pen, y, 1);
                    }
                    labelPlacer--;
                    if (labelPlacer == 0) labelPlacer = labelMod;
                }
                gridYLabelPanel.Invalidate();
            }
        }
        private void drawVerticalGridLines(Graphics g, bool MajorGridLines, Color color)
        {
            Pen pen = new Pen(color, 1.0f);
            Graphics gXLabel = gridXLabelPanel.CreateGraphics();
            float Xrange = rmaxX - rminX;
            float sXrange = smaxX - sminX;
            float interval;
            float high;
            float low;
            float labelMod;

            if (MajorGridLines)
            {
                interval = (float)Math.Pow(10.0, (Math.Floor(Math.Log10((double)Xrange))));
                high = gmaxX;
                low = gminX;

                //dynamic grid
                //shrinking
                while ((sXrange / (Xrange / interval)) < 100)
                {
                    interval *= 2;
                }
                //expanding
                while ((sXrange / (Xrange / interval)) > 200)
                {
                    interval /= 2;
                }

                //draw lines
                for (float x = 0.0f; x <= high; x += interval)
                {
                    if (x >= low)
                    {
                        DrawOneGridLine(g, pen, x, 2);
                    }
                }
                for (float x = 0.0f; x >= low; x -= interval)
                {
                    if (x <= high)
                    {
                        DrawOneGridLine(g, pen, x, 2);
                    }
                }
            }
            else
            {
                interval = (float)Math.Pow(10.0, (Math.Floor(Math.Log10((double)Xrange))) - 1.0f);
                xInterval = interval;
                high = gmaxX;
                low = gminX;
                labelMod = 2; //place a label every x lines

                //dynamic grid
                //shrinking
                while ((sXrange / (Xrange / interval)) < 10)
                {
                    interval *= 2;
                }
                //expanding
                while ((sXrange / (Xrange / interval)) > 20)
                {
                    interval /= 2;
                }

                //draw lines
                float labelPlacer = labelMod;
                xLabels.Clear();
                for (float x = 0.0f; x <= high; x += interval)
                {
                    if (x >= low)
                    {
                        if (labelPlacer == labelMod) xLabels.Add(x);
                        if (!minimalGridToolStripMenuItem.Checked) DrawOneGridLine(g, pen, x, 2);
                    }
                    labelPlacer--;
                    if (labelPlacer == 0) labelPlacer = labelMod;
                }
                labelPlacer = labelMod;
                for (float x = 0.0f; x >= low; x -= interval)
                {
                    if (x <= high)
                    {
                        if (labelPlacer == labelMod) xLabels.Add(x);
                        if (!minimalGridToolStripMenuItem.Checked) DrawOneGridLine(g, pen, x, 2);
                    }
                    labelPlacer--;
                    if (labelPlacer == 0) labelPlacer = labelMod;
                }
                gridXLabelPanel.Invalidate();
            }
        }
        private void drawGridLines(Graphics g)
        {
            drawHorizantalGridLines(g, false, Color.LightBlue);
            drawHorizantalGridLines(g, true, Color.LightCoral);
            drawVerticalGridLines(g, false, Color.LightBlue);
            drawVerticalGridLines(g, true, Color.LightCoral);
        }
        private void DrawGraph(Graphics g)
        {
            Brush brush = new SolidBrush(penColor);
            Pen pen = new Pen(brush, 2.0f);
            Rectangle relevant = gridPanel.ClientRectangle;
            relevant.X -= 10;
            relevant.Y -= 10;
            relevant.Width += 20;
            relevant.Height += 20;
            g.DrawRectangle(pen, relevant);

            //cut out parts that arent shown
            pointList.Clear();
            spointList.Clear();
            foreach (PointF point in orgpointList)
            {
                PointF spoint = transformToScreenPoint(point);
                if (relevant.Contains(Point.Round(spoint)) && !(pointList.Exists(p => p == point)))
                {
                    pointList.Add(point);
                    spointList.Add(spoint);
                }
            }

            //add points so the grid shows on the whole panel
            Point topRight = new Point(relevant.X + relevant.Width, relevant.Y);
            Point topLeft = new Point(relevant.X, relevant.Y);
            Point bottomRight = new Point(relevant.X + relevant.Width, relevant.Y + relevant.Height);
            Point bottomLeft = new Point(relevant.X, relevant.Y + relevant.Height);
            pointList.Add(transformToRealPoint(topRight));
            pointList.Add(transformToRealPoint(topLeft));
            pointList.Add(transformToRealPoint(bottomRight));
            pointList.Add(transformToRealPoint(bottomLeft));

            //prepare for drawing graph
            cropArray();
            if (showGridToolStripMenuItem.Checked) drawGridLines(g);
            PointF[] spoints = spointList.ToArray();

            switch (toolStripDotStyle.Text)
            {
                case "Candlesticks":
                    //candlesticks
                    List<candlestick> onscreencandlesticks = new List<candlestick>();
                    foreach (PointF point in pointList)
                    {
                        foreach (candlestick cs in candlesticks)
                        {
                            if (point.X == (float)cs.date.ToOADate()) onscreencandlesticks.Add(cs);
                        }
                    }

                    //remove duplicates
                    onscreencandlesticks.Union(onscreencandlesticks);

                    //set max zoom when 1 candlestick is shown
                    maxZoomStrength = 1000;
                    if (onscreencandlesticks.Count <= 1)
                    {
                        maxZoomStrength = zoomStrength;
                    }

                    int numberOfPoints = orgpointList.Count;
                    SolidBrush csbrush = new SolidBrush(penColor);
                    int penwidth = 2;
                    if (zoomStrength > 2) penwidth = (int)((gridPanel.Width / 8) * (selection.Width / gridPanel.Width)) / numberOfPoints;
                    if (penwidth < 2) penwidth = 2;
                    Pen cspen = new Pen(csbrush, penwidth);
                    foreach (candlestick cs in onscreencandlesticks)
                    {
                        numberOfPoints = orgpointList.Count;
                        float x = (float)cs.date.ToOADate();
                        PointF close = transformToScreenPoint(new PointF(x, (float)cs.close));
                        spointList.Add(close);

                        if (cs.open >= 0)
                        {//correct format
                            double candleWidth = ((gridPanel.Width / .7) * (selection.Width / gridPanel.Width)) / numberOfPoints;
                            PointF open = transformToScreenPoint(new PointF(x, (float)cs.open));
                            spointList.Add(open);
                            PointF high = transformToScreenPoint(new PointF(x, (float)cs.high));
                            spointList.Add(high);
                            PointF low = transformToScreenPoint(new PointF(x, (float)cs.low));
                            spointList.Add(low);
                            spoints = spointList.ToArray();

                            if (Point.Round(open) == Point.Round(close))
                            {
                                csbrush.Color = Color.ForestGreen;
                                cspen.Brush = csbrush;
                                g.DrawLine(cspen, high, low);
                                if (candleWidth > 4) g.DrawLine(cspen, new PointF((float)(close.X - candleWidth / 2), close.Y), new PointF((float)(close.X + candleWidth / 2), close.Y));
                            }
                            else
                            {
                                if (cs.close == Math.Max(cs.close, cs.open))
                                {//gained money
                                    csbrush.Color = Color.ForestGreen;
                                    cspen.Brush = csbrush;
                                    g.DrawLine(cspen, high, low);
                                    if (candleWidth > 4) g.FillRectangle(csbrush, (float)(close.X - candleWidth / 2), close.Y, (float)candleWidth, (open.Y - close.Y));
                                }
                                else
                                {//lost money
                                    csbrush.Color = Color.IndianRed;
                                    cspen.Brush = csbrush;
                                    g.DrawLine(cspen, high, low);
                                    if (candleWidth > 4) g.FillRectangle(csbrush, (float)(close.X - candleWidth / 2), open.Y, (float)candleWidth, (close.Y - open.Y));
                                }
                            }
                        }
                        else
                        {//incorrect format, just use dots at close
                            //dots
                            float dotSize = 2;
                            if (zoomStrength > 3)
                                dotSize = 6;
                            if (zoomStrength > 2 && zoomStrength < 3)
                                dotSize = 4;
                            if (zoomStrength > 1 && zoomStrength < 2)
                                dotSize = 3;
                            g.FillEllipse(brush, (float)(close.X - dotSize / 2), (float)(close.Y - dotSize / 2), dotSize, dotSize);
                        }
                    }
                    break;
                default:
                case "Dots and Lines":
                    //dots and lines
                    //lines
                    //use full graph to draw lines correctly
                    List<PointF> tempspointsList = new List<PointF>();
                    PointF[] tempspoints;
                    foreach (candlestick cs in candlesticks)
                    {
                        tempspointsList.Add(transformToScreenPoint(new PointF((float)cs.date.ToOADate(), (float)cs.close)));
                    }
                    tempspoints = tempspointsList.ToArray();

                    //set max zoom when 1 candlestick is shown
                    maxZoomStrength = 1000;
                    if (spointList.Count <= 1)
                    {
                        maxZoomStrength = zoomStrength;
                    }
                    
                    g.DrawLines(pen, tempspoints);
                    //dots
                    foreach (PointF point in tempspoints)
                    {
                        float dotSize = 0;
                        if (zoomStrength > 3)
                            dotSize = 6;
                        if (zoomStrength > 2 && zoomStrength < 3)
                            dotSize = 4;
                        if (zoomStrength > 1 && zoomStrength < 2)
                            dotSize = 3;
                        g.FillEllipse(brush, (float)(point.X - dotSize / 2), (float)(point.Y - dotSize / 2), dotSize, dotSize);
                    }
                    break;
            }
        }
        private void GetPanelMaxMin(Panel p)
        {
            if (selectionDefault)
            {
                selection = p.ClientRectangle;
            }
            float edgeSize = 0.01f;
            sminX = selection.X;
            smaxY = selection.Y;
            smaxX = selection.X + selection.Width;
            sminY = selection.Y + selection.Height;
            if (smaxX < 100) smaxX = 100;
            if (sminY < 100) sminY = 100;
            float Xedge = (smaxX - sminX) * edgeSize;
            float Yedge = (sminY - smaxY) * edgeSize;
            sminX += Xedge;
            smaxX -= Xedge;
            sminY -= Yedge;
            smaxY += Yedge;
        }
        private void gridPanel_Paint(object sender, PaintEventArgs e)
        {
            GetPanelMaxMin(gridPanel);
            Graphics g = e.Graphics;
            PointF relativeToPanel = new PointF(gridPanel.PointToClient(MousePosition).X, gridPanel.PointToClient(MousePosition).Y);

            g.FillRectangle(new SolidBrush(Color.AliceBlue), selection);
            if (candlesticks.Count > 0)DrawGraph(g);
            Rectangle border = new Rectangle((int)selection.X - 1, (int)selection.Y - 1, (int)selection.Width + 2, (int)selection.Height + 2);
            g.DrawRectangle(new Pen(new SolidBrush(Color.BlueViolet), 1f), border);
            DrawCoordinates(g, relativeToPanel, Color.Black);
            g.FillRectangle(new SolidBrush(gridPanel.BackColor), 0, 0, border.Location.X, border.Location.Y + border.Height);
            g.FillRectangle(new SolidBrush(gridPanel.BackColor), 0, 0, border.Location.X + border.Width, border.Location.Y);
            g.FillRectangle(new SolidBrush(gridPanel.BackColor), border.Location.X + border.Width, 0, gridPanel.Width - (border.Location.X + border.Width), gridPanel.Height);
            g.FillRectangle(new SolidBrush(gridPanel.BackColor), 0, border.Location.Y + border.Height, gridPanel.Width, gridPanel.Height - (border.Y + border.Height));
            
            toolStripZoomText.Invalidate();
        }
        private void gridPanel_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void gridYLabelPanel_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void gridYLabelPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (float y in yLabels)
            {
                LabelHorizantalGridLine(g, y);
            }
        }
        private void gridXLabelPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (float x in xLabels)
            {
                LabelVerticalGridLine(g, x);
            }
        }
        private void gridPanel_MouseDown(object sender, MouseEventArgs e)
        {
            gridPanel.Focus();
            mouseStart = MousePosition;
            mouseIsDown = true;
            orgRectLocation = rect.Location;
            orgGraphLocation = selection.Location;

            //editing rectangle
            graphSize = new SizeF(selection.Width, selection.Height);
            PointF pt = PointToScreen(Rectangle.Round(selection).Location);
            rect = new RectangleF(pt, graphSize);

            if (moveAndResizeMode)
            {
                selectionDefault = false;
                if (e.Button == MouseButtons.Left)
                {
                    moveMode = true;
                    Cursor = Cursors.SizeAll;
                }
                if (e.Button == MouseButtons.Right)
                {
                    resizeMode = true;
                    Cursor = Cursors.NoMove2D;
                }
            }
        }
        private void gridPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                if (moveMode)
                {
                    mouseEnd = MousePosition;
                    rect.X = orgRectLocation.X + (mouseEnd.X - mouseStart.X);
                    rect.Y = orgRectLocation.Y + (mouseEnd.Y - mouseStart.Y);
                    PointF newPoint = new PointF();
                    newPoint.X = orgGraphLocation.X + (mouseEnd.X - mouseStart.X);
                    newPoint.Y = orgGraphLocation.Y + (mouseEnd.Y - mouseStart.Y);
                    selection.Location = newPoint;
                    gridPanel.Invalidate();
                }
                if (resizeMode)
                {
                    mouseEnd = MousePosition;
                    rect.Width = (mouseEnd.X - mouseStart.X)* 2 + graphSize.Width;
                    rect.Height = (mouseEnd.Y - mouseStart.Y)* 2 + graphSize.Height;
                    if (rect.Size.Width < 100) rect.Width = 100;
                    if (rect.Size.Height < 100) rect.Height = 100;
                    selection.Size = rect.Size;
                    selection.X = orgGraphLocation.X - (mouseEnd.X - mouseStart.X);
                    selection.Y = orgGraphLocation.Y - (mouseEnd.Y - mouseStart.Y);

                    //set zoom
                    zoomStrength = selection.Width / gridPanel.Width;
                    toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                }
            }
            gridPanel.Invalidate();
        }
        private void gridPanel_MouseUp(object sender, MouseEventArgs e)
        {
            mouseEnd = MousePosition;
            if (moveMode)
            {
                PointF newPoint = new PointF();
                newPoint.X = orgGraphLocation.X + (mouseEnd.X - mouseStart.X);
                newPoint.Y = orgGraphLocation.Y + (mouseEnd.Y - mouseStart.Y);
                selection.Location = newPoint;
                moveMode = false;
            }
            if (resizeMode)
            {
                if (rect.Width > 0 && rect.Height > 0)
                {
                    selection.Size = rect.Size;
                }
                //set zoom
                zoomStrength = selection.Width / gridPanel.Width;
                toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";

                resizeMode = false;
            }
            Cursor = Cursors.Default;
            mouseIsDown = false;
        }
        void gridPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            //dont allow multiple actions at once
            if (!mouseIsDown)
            {
                //editing rectangle
                graphSize = new SizeF(selection.Width, selection.Height);
                PointF pt = PointToClient(PointToScreen(Rectangle.Round(selection).Location));
                rect = new RectangleF(pt, graphSize);

                // Update the drawing based upon the mouse wheel scrolling.
                int numberOfTextLinesToMove = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
                float numberOfPixelsToMove = (numberOfTextLinesToMove * 20) * zoomStrength;
                float aspectRatio = (float)selection.Width / (float)selection.Height;

                orgGraphLocation = selection.Location;
                selectionDefault = false;

                mouseStart = gridPanel.PointToClient(MousePosition);
                orgRelativeToGraph = new PointF(mouseStart.X - orgGraphLocation.X, mouseStart.Y - orgGraphLocation.Y);

                rect.Width += numberOfPixelsToMove;
                rect.Height = (int)((float)rect.Width / aspectRatio);

                //keep aspect ratio and stay above 100px
                if (Math.Min(rect.Width, rect.Height) < 100)
                {
                    if (Math.Min(rect.Width / aspectRatio, rect.Height / aspectRatio) == rect.Width)
                    {
                        rect.Width = 100;
                        rect.Height = (int)(100 / aspectRatio);

                        //check if zoom is valid before drawing
                        //set zoom
                        zoomStrength = rect.Width / gridPanel.Width;
                        if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                        {
                            toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                            selection = rect;
                        }
                    }
                    else
                    {
                        rect.Height = 100;
                        rect.Width = (int)(100 * aspectRatio);

                        //check if zoom is valid before drawing
                        //set zoom
                        zoomStrength = rect.Width / gridPanel.Width;
                        if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                        {
                            toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                            selection = rect;
                        }
                    }
                }
                else
                {
                    //find % stretched
                    float xStretched = ((float)rect.Width) / ((float)selection.Width);
                    float yStretched = ((float)rect.Height) / ((float)selection.Height);
                    //stretch mouse point
                    relativeToGraph.X = orgRelativeToGraph.X * xStretched;
                    relativeToGraph.Y = orgRelativeToGraph.Y * yStretched;
                    //adjust graph to focus mouse point
                    rect.X -= relativeToGraph.X - orgRelativeToGraph.X;
                    rect.Y -= relativeToGraph.Y - orgRelativeToGraph.Y;

                    //check if zoom is valid before drawing
                    //set zoom
                    zoomStrength = rect.Width / gridPanel.Width;
                    if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                    {
                        toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                        selection = rect;
                    }

                }
                gridPanel.Invalidate();
            }
        }
        private void toolStripButtonFullGraph(object sender, EventArgs e)
        {
            zoomStrength = 1.00f;
            toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
            selection = gridPanel.ClientRectangle;
            selectionDefault = true;
            moveAndResizeMode = false;
            gridPanel.Invalidate();
        }
        bool moveAndResizeMode = false;
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            moveAndResizeMode = true;
        }
        private void toolStripZoomText_Paint(object sender, PaintEventArgs e)
        {
        }
        private void realCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            screenCoordinatesToolStripMenuItem.Checked = false;
            realCoordinatesToolStripMenuItem.Checked = true;
        }
        private void screenCoordinatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            screenCoordinatesToolStripMenuItem.Checked = true;
            realCoordinatesToolStripMenuItem.Checked = false;
        }
        private void btnHorizontalStretch_Click(object sender, EventArgs e)
        {
            //editing rectangle
            graphSize = new SizeF(selection.Width, selection.Height);
            PointF pt = PointToClient(PointToScreen(Rectangle.Round(selection).Location));
            rect = new RectangleF(pt, graphSize);

            float numberOfPixelsToMove = 40 * zoomStrength;

            orgGraphLocation = selection.Location;
            selectionDefault = false;

            mouseStart = new Point(gridPanel.Width / 2, gridPanel.Height / 2);
            orgRelativeToGraph = new PointF(mouseStart.X - orgGraphLocation.X, mouseStart.Y - orgGraphLocation.Y);

            rect.Width += numberOfPixelsToMove;

            //stay above 100px
            if (Math.Min(rect.Width, rect.Height) < 100)
            {
                rect.Width = 100;

                //check if zoom is valid before drawing
                //set zoom
                zoomStrength = rect.Width / gridPanel.Width;
                if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                {
                    toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                    selection = rect;
                }
            }
            else
            {
                //find % stretched
                float xStretched = ((float)rect.Width) / ((float)selection.Width);
                //stretch mouse point
                relativeToGraph.X = orgRelativeToGraph.X * xStretched;
                relativeToGraph.Y = orgRelativeToGraph.Y;
                //adjust graph to focus mouse point
                rect.X -= relativeToGraph.X - orgRelativeToGraph.X;
                rect.Y -= relativeToGraph.Y - orgRelativeToGraph.Y;

                //check if zoom is valid before drawing
                //set zoom
                zoomStrength = rect.Width / gridPanel.Width;
                if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                {
                    toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                    selection = rect;
                }

            }
            gridPanel.Invalidate();
        }
        private void btnHorizontalShrink_Click(object sender, EventArgs e)
        {
            //editing rectangle
            graphSize = new SizeF(selection.Width, selection.Height);
            PointF pt = PointToClient(PointToScreen(Rectangle.Round(selection).Location));
            rect = new RectangleF(pt, graphSize);

            float numberOfPixelsToMove = -40 * zoomStrength;

            orgGraphLocation = selection.Location;
            selectionDefault = false;

            mouseStart = new Point(gridPanel.Width / 2, gridPanel.Height / 2);
            orgRelativeToGraph = new PointF(mouseStart.X - orgGraphLocation.X, mouseStart.Y - orgGraphLocation.Y);

            rect.Width += numberOfPixelsToMove;

            //stay above 100px
            if (rect.Width < 100)
            {
                rect.Width = 100;

                //check if zoom is valid before drawing
                //set zoom
                zoomStrength = rect.Width / gridPanel.Width;
                if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                {
                    toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                    selection = rect;
                }
            }
            else
            {
                //find % stretched
                float xStretched = ((float)rect.Width) / ((float)selection.Width);
                //stretch mouse point
                relativeToGraph.X = orgRelativeToGraph.X * xStretched;
                relativeToGraph.Y = orgRelativeToGraph.Y;
                //adjust graph to focus mouse point
                rect.X -= relativeToGraph.X - orgRelativeToGraph.X;
                rect.Y -= relativeToGraph.Y - orgRelativeToGraph.Y;

                //check if zoom is valid before drawing
                //set zoom
                zoomStrength = rect.Width / gridPanel.Width;
                if (zoomStrength <= maxZoomStrength && zoomStrength >= .001)
                {
                    toolStripZoomText.Text = (int)(100 * zoomStrength) + "%";
                    selection = rect;
                }

            }
            gridPanel.Invalidate();
        }
        private void btnVerticalStretch_Click(object sender, EventArgs e)
        {
            //editing rectangle
            graphSize = new SizeF(selection.Width, selection.Height);
            PointF pt = PointToClient(PointToScreen(Rectangle.Round(selection).Location));
            rect = new RectangleF(pt, graphSize);

            float numberOfPixelsToMove = 40 * zoomStrength;

            orgGraphLocation = selection.Location;
            selectionDefault = false;

            mouseStart = new Point(gridPanel.Width / 2, gridPanel.Height / 2);
            orgRelativeToGraph = new PointF(mouseStart.X - orgGraphLocation.X, mouseStart.Y - orgGraphLocation.Y);

            rect.Height += numberOfPixelsToMove;

            //stay above 100px
            if (rect.Height < 100)
            {
                rect.Height = 100;
                selection = rect;
            }
            else
            {
                //find % stretched
                float yStretched = ((float)rect.Height) / ((float)selection.Height);
                //stretch mouse point
                relativeToGraph.X = orgRelativeToGraph.X;
                relativeToGraph.Y = orgRelativeToGraph.Y * yStretched;
                //adjust graph to focus mouse point
                rect.X -= relativeToGraph.X - orgRelativeToGraph.X;
                rect.Y -= relativeToGraph.Y - orgRelativeToGraph.Y;
                selection = rect;

            }
            gridPanel.Invalidate();
        }
        private void btnVerticalShrink_Click(object sender, EventArgs e)
        {
            //editing rectangle
            graphSize = new SizeF(selection.Width, selection.Height);
            PointF pt = PointToClient(PointToScreen(Rectangle.Round(selection).Location));
            rect = new RectangleF(pt, graphSize);

            float numberOfPixelsToMove = -40 * zoomStrength;

            orgGraphLocation = selection.Location;
            selectionDefault = false;

            mouseStart = new Point(gridPanel.Width / 2, gridPanel.Height / 2);
            orgRelativeToGraph = new PointF(mouseStart.X - orgGraphLocation.X, mouseStart.Y - orgGraphLocation.Y);

            rect.Height += numberOfPixelsToMove;

            //stay above 100px
            if (rect.Height < 100)
            {
                rect.Height = 100;
                selection = rect;
            }
            else
            {
                //find % stretched
                float yStretched = ((float)rect.Height) / ((float)selection.Height);
                //stretch mouse point
                relativeToGraph.X = orgRelativeToGraph.X;
                relativeToGraph.Y = orgRelativeToGraph.Y * yStretched;
                //adjust graph to focus mouse point
                rect.X -= relativeToGraph.X - orgRelativeToGraph.X;
                rect.Y -= relativeToGraph.Y - orgRelativeToGraph.Y;
                selection = rect;

            }
            gridPanel.Invalidate();
        }
        private void gridXLabelPanel_Resize(object sender, EventArgs e)
        {
            gridXLabelPanel.Invalidate();
        }
        private void btnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                btnColor.BackColor = colorDialog1.Color;
                penColor = colorDialog1.Color;
            }
        }
        private void showGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showGridToolStripMenuItem.Checked = !showGridToolStripMenuItem.Checked;
        }
        private void minimalGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minimalGridToolStripMenuItem.Checked = !minimalGridToolStripMenuItem.Checked;
        }

        #endregion
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelTIME.Text = DateTime.Now.ToString("yyyy MMM d h:mmtt");

            if ((DateTime.Now.DayOfWeek == DayOfWeek.Monday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Tuesday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Wednesday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Thursday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Friday) &&
                (DateTime.Now.Hour > 9 && DateTime.Now.Minute > 30) &&
                DateTime.Now.Hour < 16)
            {
                toolStripStatusLabelOPEN.Text = "U.S. Markets open";
            }
            else
            {
                toolStripStatusLabelOPEN.Text = "U.S. Markets closed";
            }
        }
        private void toolStripComboBoxShowTime_TextChanged(object sender, EventArgs e)
        {
            switch (toolStripComboBoxShowTime.Text)
            {
                case "5 days":
                    from = to.AddDays(-5);
                    break;
                case "15 days":
                    from = to.AddDays(-15);
                    break;
                case "1 Month":
                    from = to.AddMonths(-1);
                    break;
                case "2 Months":
                    from = to.AddMonths(-2);
                    break;
                case "3 Months":
                    from = to.AddMonths(-3);
                    break;
                case "6 Months":
                    from = to.AddMonths(-6);
                    break;
                case "1 Year":
                    from = to.AddYears(-1);
                    break;
                case "2 Years":
                    from = to.AddYears(-2);
                    break;
                case "5 Years":
                    from = to.AddYears(-5);
                    break;
                default:
                    break;
            }
            settings.startYear = from.Year;
            settings.startMonth = from.Month;
            settings.startDay = from.Day;
            refreshSymbol();
        }
        private void toolStripTextBoxFROM_Enter(object sender, EventArgs e)
        {//suggest a format
            toolStripTextBoxFROM.Text = from.ToString("M/d/yyyy");
        }
        private void toolStripTextBoxFROM_Leave(object sender, EventArgs e)
        {
            DateTime lastGoodFrom = new DateTime(from.Year, from.Month, from.Day);
            try
            {
                string[] datePieces = toolStripTextBoxFROM.Text.Split('/');
                from = new DateTime(Convert.ToInt32(datePieces[2]), Convert.ToInt32(datePieces[0]), Convert.ToInt32(datePieces[1]));
                settings.startDay = from.Day;
                settings.startMonth = from.Month;
                settings.startYear = from.Year;
                refreshSymbol();
            }
            catch
            {
                toolStripTextBoxFROM.Text = lastGoodFrom.ToString("MMM d yyyy");
                from = lastGoodFrom;
                settings.startDay = from.Day;
                settings.startMonth = from.Month;
                settings.startYear = from.Year;
                refreshSymbol();
            }

            toolStripTextBoxFROM.Text = from.ToString("MMM d yyyy");
        }
        private void toolStripTextBoxTO_Enter(object sender, EventArgs e)
        {//suggest a format
            toolStripTextBoxTO.Text = to.ToString("M/d/yyyy");
        }
        private void toolStripTextBoxTO_Leave(object sender, EventArgs e)
        {
            DateTime lastGoodTo = new DateTime(to.Year, to.Month, to.Day);
            try
            {
                string[] datePieces = toolStripTextBoxTO.Text.Split('/');
                to = new DateTime(Convert.ToInt32(datePieces[2]), Convert.ToInt32(datePieces[0]), Convert.ToInt32(datePieces[1]));
                settings.endDay = to.Day;
                settings.endMonth = to.Month;
                settings.endYear = to.Year;
                refreshSymbol();
            }
            catch
            {
                toolStripTextBoxFROM.Text = lastGoodTo.ToString("MMM d yyyy");
                to = lastGoodTo;
                settings.endDay = to.Day;
                settings.endMonth = to.Month;
                settings.endYear = to.Year;
                refreshSymbol();
            }
            toolStripTextBoxTO.Text = to.ToString("MMM d yyyy");
        }
        private void toolStripTextBoxFROM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gridPanel.Focus();
            }
        }
        private void toolStripTextBoxTO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                gridPanel.Focus();
            }
        }
    }
}
