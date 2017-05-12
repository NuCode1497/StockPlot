using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace StockPlot
{
    public partial class OrganizeFavorites : Form
    {
        public OrganizeFavorites()
        {
            InitializeComponent();
            load();
        }

        private void load()
        {
            List<string> notchecked = new List<string>();

            if (!File.Exists(@"Favorites.txt")) File.Create(@"Favorites.txt");
            using (StreamReader file = new StreamReader(@"Favorites.txt"))
            {
                while (!file.EndOfStream)
                {
                    notchecked.Add(file.ReadLine());
                }
            }

            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(notchecked.ToArray());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FavoritesAdd adder = new FavoritesAdd();
            adder.StartPosition = FormStartPosition.CenterParent;
            if (adder.ShowDialog() == DialogResult.OK)
            {
                adder.result = adder.result.ToUpper();

                //find existing favorite
                bool exists = false;
                if (!File.Exists(@"Favorites.txt")) File.Create(@"Favorites.txt");
                using (StreamReader file = new StreamReader(@"Favorites.txt"))
                {
                    while (!file.EndOfStream)
                    {
                        if (adder.result == file.ReadLine())
                        {
                            exists = true;
                            break;
                        }
                    }
                }

                if (!exists)
                {
                    Settings settings = new Settings();
                    settings.loadSettings();

                    try
                    {
                        String fileName = "http://ichart.finance.yahoo.com/table.csv?s=" + adder.result +
                                          "&a=" + settings.startMonth +
                                          "&b=" + settings.startDay +
                                          "&c=" + settings.startYear +
                                          "&d=" + settings.endMonth +
                                          "&e=" + settings.endDay +
                                          "&f=" + settings.endYear +
                                          "&g=d&ignore=.csv";
                        System.Net.WebClient client = new WebClient();
                        StreamReader reader = new StreamReader(client.OpenRead(fileName)); //try finding the symbol
                        reader.Close();

                        //save Favorites            
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Favorites.txt", true)) //append
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
                    err.label1.Text = "Symbol already exists in Favorites.";
                    err.label2.Text = "";
                    err.ShowDialog();
                }
                load();
            }
        }

        private void btnDelChkd_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                List<string> notchecked = new List<string>();
                foreach (string symbol in checkedListBox1.Items)
                {
                    if (checkedListBox1.CheckedItems.Contains(symbol)) continue; //skip
                    notchecked.Add(symbol);
                }
                File.WriteAllLines(@"Favorites.txt", notchecked);
                checkedListBox1.Items.Clear();
                checkedListBox1.Items.AddRange(notchecked.ToArray());
            }
            load();
        }
    }
}
