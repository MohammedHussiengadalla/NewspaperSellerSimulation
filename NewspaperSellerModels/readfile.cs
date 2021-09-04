using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NewspaperSellerModels
{
    public class readfile
    {

       public SimulationSystem ss = new SimulationSystem();
        private String FileName { set; get; }
        public StreamReader SR;
        private List<String> KeyWords;
        public readfile(String FileName)
        {
            SR = new StreamReader(FileName);
            this.FileName = FileName;
            this.KeyWords = new List<String> { "NumOfNewspapers", "NumOfRecords", "PurchasePrice", "ScrapPrice", "SellingPrice", "DayTypeDistributions", "DemandDistributions" };
        }

       

        public void Format_File_Data()
        {
            String Line="";

            List<String> ret = new List<string>();
            int lk = 0;
            Line = SR.ReadLine();
            while (Line != null)
            {

                 

                if (Line == "NumOfNewspapers")
                    lk = 1;
                else if (Line == "NumOfRecords") lk = 2;
                else if (Line == "PurchasePrice") lk = 3;
                else if (Line == "ScrapPrice") lk = 4;
                else if (Line == "SellingPrice") lk = 5;
                else if (Line == "DayTypeDistributions") lk = 6;
                else if (Line == "DemandDistributions") lk = 7;

                else if (Line != "")
                {
                    if (lk == 1)
                        ss.NumOfNewspapers = int.Parse(Line);
                    else if (lk == 2)
                        ss.NumOfRecords = int.Parse(Line);

                    else if (lk == 3)
                        ss.PurchasePrice = decimal.Parse(Line);

                    else if (lk == 4)
                        ss.ScrapPrice = decimal.Parse(Line);

                    else if (lk == 5)
                        ss.SellingPrice = decimal.Parse(Line);
                    else if (lk == 6)
                    {

                        String[] sperated_line = Line.Split(',');
                        for (int i = 0; i < sperated_line.Length; i++)
                        {
                            DayTypeDistribution day = new DayTypeDistribution();
                            day.DayType = (Enums.DayType)i;
                            day.Probability = decimal.Parse(sperated_line[i]);
                            ss.DayTypeDistributions.Add(day); 
                           
                        }


                    }
                    else
                    {
                        
                       
                        DayTypeDistribution day = new DayTypeDistribution();
                        DayTypeDistribution day1 = new DayTypeDistribution();
                        DayTypeDistribution day2 = new DayTypeDistribution();

                        String[] sperated_line = Line.Split(',');
                        
                            DemandDistribution demandnew = new DemandDistribution();
                            demandnew.Demand = int.Parse(sperated_line[0]);
                           


                            day.DayType = (Enums.DayType)0;
                            day.Probability = decimal.Parse(sperated_line[1]);
                            demandnew.DayTypeDistributions.Add(day);

                            day1.DayType = (Enums.DayType)1;
                            day1.Probability = decimal.Parse(sperated_line[2]);
                            demandnew.DayTypeDistributions.Add(day1);

                            day2.DayType = (Enums.DayType)2;
                            day2.Probability = decimal.Parse(sperated_line[3]);
                            demandnew.DayTypeDistributions.Add(day2);


                            ss.DemandDistributions.Add(demandnew);
                        
                    }
                }
                Line = SR.ReadLine();
            }
            SR.Close();
        }
        /// cummulative of demand
        public void cummofdemand()
        {
            for (int i = 0; i < ss.DemandDistributions.Count; i++)
            {


                if (i == 0)
                {
                    ss.DemandDistributions[i].DayTypeDistributions[0].CummProbability = ss.DemandDistributions[i].DayTypeDistributions[0].Probability;
                    ss.DemandDistributions[i].DayTypeDistributions[1].CummProbability = ss.DemandDistributions[i].DayTypeDistributions[1].Probability;
                    ss.DemandDistributions[i].DayTypeDistributions[2].CummProbability = ss.DemandDistributions[i].DayTypeDistributions[2].Probability;
                }
                else
                {
                    ss.DemandDistributions[i].DayTypeDistributions[0].CummProbability = ss.DemandDistributions[i].DayTypeDistributions[0].Probability
                        + ss.DemandDistributions[i - 1].DayTypeDistributions[0].CummProbability;
                    ss.DemandDistributions[i].DayTypeDistributions[1].CummProbability = ss.DemandDistributions[i].DayTypeDistributions[1].Probability
                                                        + ss.DemandDistributions[i - 1].DayTypeDistributions[1].CummProbability;
                    ss.DemandDistributions[i].DayTypeDistributions[2].CummProbability = ss.DemandDistributions[i].DayTypeDistributions[2].Probability
                        + ss.DemandDistributions[i - 1].DayTypeDistributions[2].CummProbability;
                }
            }

        }
        /// range of demond
        public void rangeofdemond()
        {
            for (int i = 0; i < ss.DemandDistributions.Count; i++)
            {
                

                    if (i == 0)
                    {
                        ss.DemandDistributions[i].DayTypeDistributions[0].MinRange = 1;
                        ss.DemandDistributions[i].DayTypeDistributions[1].MinRange = 1;
                        ss.DemandDistributions[i].DayTypeDistributions[2].MinRange = 1;
                        ss.DemandDistributions[i].DayTypeDistributions[0].MaxRange = (int)(ss.DemandDistributions[i].DayTypeDistributions[0].CummProbability * 100);
                        ss.DemandDistributions[i].DayTypeDistributions[1].MaxRange = (int)(ss.DemandDistributions[i].DayTypeDistributions[1].CummProbability * 100);
                        ss.DemandDistributions[i].DayTypeDistributions[2].MaxRange = (int)(ss.DemandDistributions[i].DayTypeDistributions[2].CummProbability * 100);
                    }
                    else
                    {
                        ss.DemandDistributions[i].DayTypeDistributions[0].MaxRange = (int)(ss.DemandDistributions[i].DayTypeDistributions[0].CummProbability * 100);
                        ss.DemandDistributions[i].DayTypeDistributions[1].MaxRange = (int)(ss.DemandDistributions[i].DayTypeDistributions[1].CummProbability * 100);
                        ss.DemandDistributions[i].DayTypeDistributions[2].MaxRange = (int)(ss.DemandDistributions[i].DayTypeDistributions[2].CummProbability * 100);
                        ss.DemandDistributions[i].DayTypeDistributions[0].MinRange = (int)(ss.DemandDistributions[i - 1].DayTypeDistributions[0].MaxRange + 1);
                        ss.DemandDistributions[i].DayTypeDistributions[1].MinRange = (int)(ss.DemandDistributions[i - 1].DayTypeDistributions[1].MaxRange + 1);
                        ss.DemandDistributions[i].DayTypeDistributions[2].MinRange = (int)(ss.DemandDistributions[i - 1].DayTypeDistributions[2].MaxRange + 1);

                    }
                

            }


        }
        ///cummulative of day type 
        public void cummdaytype()
        {
            ss.DayTypeDistributions[0].CummProbability = ss.DayTypeDistributions[0].Probability;
            for (int i = 1; i < ss.DayTypeDistributions.Count; i++)
            {
                ss.DayTypeDistributions[i].CummProbability = ss.DayTypeDistributions[i].Probability + ss.DayTypeDistributions[i - 1].CummProbability;
            }
        }
        /// range of day type 
        public void rangedaytype()
        {
            ss.DayTypeDistributions[0].MinRange = 1;
            ss.DayTypeDistributions[0].MaxRange = (int)(ss.DayTypeDistributions[0].CummProbability * 100);
            for (int i = 1; i < ss.DayTypeDistributions.Count; i++)
            {
                ss.DayTypeDistributions[i].MinRange = ss.DayTypeDistributions[i - 1].MaxRange + 1;
                ss.DayTypeDistributions[i].MaxRange = (int)(ss.DayTypeDistributions[i].CummProbability * 100);
            }
        }
        public Random random = new Random();

        public List<int> randdemand = new List<int>();
        public List<int> randnews = new List<int>();
        public int Generate_Random()
        {

            return random.Next(1, 101);
        }

        //generate random of news day
        public void generandnewsday()
        {
            for (int i = 1; i < ss.NumOfRecords+1; i++)
                randnews.Add(Generate_Random());

        }
        public void generandemand()
        {
            for (int i = 0; i < ss.NumOfRecords; i++)
                randdemand.Add(Generate_Random());

        }
        public List<Enums.DayType> typenewday = new List<Enums.DayType>();
        public void tofnewsday()
        {
            for (int i = 0; i < ss.NumOfRecords; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (randnews[i] >= ss.DayTypeDistributions[j].MinRange && randnews[i] <= ss.DayTypeDistributions[j].MaxRange)
                    {
                        typenewday.Add( ss.DayTypeDistributions[j].DayType);
                    }
                }
            }
        }
        public List<int> demondvalue = new List<int>();
        DemandDistribution v = new DemandDistribution();
        public void tofdemond()
        {
            int j = 0;
            for ( int i = 0; i < ss.NumOfRecords; i++)
            {
                j = 0;
                while (j < ss.DemandDistributions.Count) { 
                    if (randdemand[i] >= ss.DemandDistributions[j].DayTypeDistributions[(int)typenewday[i]].MinRange && randdemand[i] <=
                       ss.DemandDistributions[j].DayTypeDistributions[(int)typenewday[i]].MaxRange)
                    {
                        demondvalue.Add(ss.DemandDistributions[j].Demand);
                        break;
                    }

                    j ++;
                }
            }


        }
        public PerformanceMeasures pm = new PerformanceMeasures();
        public List<decimal> salles = new List<decimal>();
        public void calsells()
        {
            for (int i = 0; i < ss.NumOfRecords; i++)
            {
                if (demondvalue[i] >= ss.NumOfNewspapers)
                {
                    salles.Add( ss.NumOfNewspapers * ss.SellingPrice);
                }
                else
                {
                    salles.Add( demondvalue[i] * ss.SellingPrice);

                }
                pm.TotalSalesProfit += salles[i];

            }

        }
        public List<decimal> lostpro = new List<decimal>();
        public void calostprofit()
        {
            for (int i = 0; i < ss.NumOfRecords; i++)
            {
                if (demondvalue[i] > ss.NumOfNewspapers)
                {
                    lostpro.Add((demondvalue[i] - ss.NumOfNewspapers) * (ss.SellingPrice - ss.PurchasePrice));

                }
                else lostpro.Add( 0);
                pm.TotalLostProfit += lostpro[i];

            }
        }


        public List<decimal> lostscrip = new List<decimal>();
        public void caltscrip()
        {
            for (int i = 0; i < ss.NumOfRecords; i++)
            {
                if (demondvalue[i] <= ss.NumOfNewspapers)
                {
                    lostscrip.Add((ss.NumOfNewspapers - demondvalue[i]) * (ss.ScrapPrice));

                }
                else
                    lostscrip.Add(0);


                    pm.TotalScrapProfit += lostscrip[i];

            }
        }
        public List<decimal> daprofit = new List<decimal>();
        public object SimulationTable;

        public void caldaprofit()
        {
            for (int i = 0; i < ss.NumOfRecords; i++)
            {
                pm.TotalCost += (ss.NumOfNewspapers * ss.PurchasePrice);
                daprofit.Add( salles[i] - (ss.NumOfNewspapers * ss.PurchasePrice)-lostpro[i] + lostscrip[i]);
                pm.TotalNetProfit += daprofit[i];
            }
        }
    }
}
