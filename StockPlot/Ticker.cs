using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockPlot
{
    class Ticker
    {
        public string symbol;
        public double last;
        public double change;
        public double perChange;
        public double volume;
        public DateTime time;

        public Ticker()
        {
            last = 0;
            change = 0;
            perChange = 0;
            volume = 0;
            time = new DateTime();
        }
    }
}
