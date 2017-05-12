using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockPlot
{
    class candlestick
    {
        public DateTime date;
        public double open;
        public double high;
        public double low;
        public double close;

        public candlestick()
        {
            date = new DateTime(1,1,1);
            open = -1;
            high = -1;
            low = -1;
            close = -1;
        }

        public candlestick(DateTime d, double o, double h, double l, double c)
        {
            date = d;
            open = o;
            high = h;
            low = l;
            close = c;
        }
    }
}
