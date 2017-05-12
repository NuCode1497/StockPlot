using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace StockPlot
{
    class Settings
    {
        public int startMonth;
        public int startDay;
        public int startYear;
        public int endMonth;
        public int endDay;
        public int endYear;

        public Settings()
        {
            endMonth = DateTime.Now.Month;
            endDay = DateTime.Now.Day;
            endYear = DateTime.Now.Year;

            DateTime sdt = DateTime.Now.AddMonths(-1);
            startMonth = sdt.Month;
            startDay = sdt.Day;
            startYear = sdt.Year;

            //startMonth = DateTime.Now.Month - 1;
            //startDay = 1;
            //startYear = DateTime.Now.Year;
        }

        
            //endMonth = DateTime.Now.Month;
            //endDay = DateTime.Now.Day;
            //endYear = DateTime.Now.Year;




        public void saveSettings()
        {
            if (!File.Exists(@"Settings.txt")) File.Create(@"Settings.txt");
            //save Settings
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Settings.txt", false)) //overwrite
            {
                file.WriteLine(startMonth);
                file.WriteLine(startDay);
                file.WriteLine(startYear);
                file.WriteLine(endMonth);
                file.WriteLine(endDay);
                file.WriteLine(endYear);
            }
        }

        public void loadSettings()
        {
            if (File.Exists(@"Settings.txt"))
            {
                //load Settings
                using (System.IO.StreamReader file = new System.IO.StreamReader(@"Settings.txt", false))
                {
                    try
                    {
                        startMonth = Convert.ToInt32(file.ReadLine());
                        startDay = Convert.ToInt32(file.ReadLine());
                        startYear = Convert.ToInt32(file.ReadLine());
                        endMonth = Convert.ToInt32(file.ReadLine());
                        endDay = Convert.ToInt32(file.ReadLine());
                        endYear = Convert.ToInt32(file.ReadLine());
                    }
                    catch
                    {
                        Error err = new Error();
                        err.label1.Text = "Invalid Settings";
                        err.label2.Text = "";
                        err.ShowDialog();
                    }
                }
            }
        }
    }
}
