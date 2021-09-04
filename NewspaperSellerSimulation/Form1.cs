using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        public string h = null;

        public readfile ff = new readfile(@"E:\1_ST 4YEAR\SIMULATION\newsption_Students\NewspaperSellerSimulation_Students\NewspaperSellerSimulation\TestCases\TestCase2.txt");
        SimulationCase w = new SimulationCase();
        SimulationSystem d = new SimulationSystem();
        public Form1( )

        {
            
            InitializeComponent();
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {

            DataGridView view = dataGridView1;
            view.Rows.Clear();
            view.ColumnCount = 9;
            view.Columns[0].Name = "Day";
            view.Columns[1].Name = "Random digits for type of newsday";
            view.Columns[2].Name = "type of newsday";
            view.Columns[3].Name = "Random digits for demand";
            view.Columns[4].Name = "demand";
            view.Columns[5].Name = "revenue from sales";
            view.Columns[6].Name = "lost profit";
            view.Columns[7].Name = "salvage";
            view.Columns[8].Name = "daily profit";

            ff.Format_File_Data();
            ff.cummofdemand();
            ff.rangeofdemond();
            ff.cummdaytype();
            ff.rangedaytype();
            ff.Generate_Random();
            ff.generandnewsday();
            ff.generandemand();
            ff.tofnewsday();
            ff.tofdemond();
            ff.calsells();
            ff.calostprofit();
            ff.caltscrip();
            ff.caldaprofit();
            decimal c = 0;
            int i = 0;
            while (i < ff.ss.NumOfRecords)
            {

                c = ff.ss.NumOfNewspapers * ff.ss.PurchasePrice;
                d.SimulationTable.Add(new SimulationCase
                {
                    DayNo = i + 1,
                    RandomNewsDayType = ff.randnews[i],
                    NewsDayType = ff.typenewday[i],
                    RandomDemand = ff.randdemand[i]
                ,
                    Demand = ff.demondvalue[i],
                    DailyCost = c,
                    SalesProfit = ff.salles[i],
                    LostProfit = ff.lostpro[i],
                    ScrapProfit = ff.lostscrip[i],
                    DailyNetProfit = ff.daprofit[i]
                });


                if (d.SimulationTable[i].Demand > ff.ss.NumOfNewspapers)
                {
                    d.PerformanceMeasures.DaysWithMoreDemand++;
                }
                else if (d.SimulationTable[i].Demand < ff.ss.NumOfNewspapers)
                {

                    d.PerformanceMeasures.DaysWithUnsoldPapers++;
                }

                string[] rows = new string[] { (i+1) .ToString(), ff.randnews[i].ToString(),  ff.typenewday[i].ToString(),ff.randdemand[i].ToString(),ff.demondvalue[i].ToString(),
                ff.salles[i].ToString(),ff.lostpro[i].ToString(),ff.lostscrip[i].ToString(),ff.daprofit[i].ToString()

                };


                view.Rows.Add(rows);

                i++;
            }

            string[] r = new string[] { "0", "0", "0", "0", "0", ff.pm.TotalSalesProfit.ToString(),ff.pm.TotalLostProfit.ToString(),ff.pm.TotalScrapProfit.ToString(),
            ff.pm.TotalNetProfit.ToString()
            };
            view.Rows.Add(r);
            d.PerformanceMeasures.TotalSalesProfit = ff.pm.TotalSalesProfit;
            d.PerformanceMeasures.TotalScrapProfit = ff.pm.TotalScrapProfit;
            d.PerformanceMeasures.TotalCost = ff.pm.TotalCost;
            d.PerformanceMeasures.TotalLostProfit = (decimal)ff.pm.TotalLostProfit;
            d.PerformanceMeasures.TotalNetProfit = ff.pm.TotalNetProfit;
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string x = TestingManager.Test(d, Constants.FileNames.TestCase2);
            MessageBox.Show(x);
        }
        
       

    }
}
