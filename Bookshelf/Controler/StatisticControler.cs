using System;
using System.Collections.Generic;
using Bookshelf.Model;
using Microcharts;
using System.Threading;
using System.Threading.Tasks;

namespace Bookshelf.Controler
{
    public class StatisticControler
    {
        private StatisticControler() { }

        private static int RPage;
        private static int PPage;
        private static (string[], int[]) Categori;
        private static (string[], int[]) Date; 

        public static List<Statistic> GetList(List<ReadBook> readBooks, List<PendingBook> pendingBooks)
        {
            List<Statistic> res = new List<Statistic>();

            GetDataForStatistic(readBooks,pendingBooks);


            res.Add(GetCountBookStatistic(readBooks.Count, pendingBooks.Count));
            res.Add(LikeAutor(readBooks));
            res.Add(GetCountPageStatistic(RPage, PPage));
            res.Add(new Statistic()
            {
                Value = DBControler.CountQuotes().ToString(),
                Name = "Цитат"
            });

            res.Add(GetCategoriStatisc(Categori.Item1, Categori.Item2));

            res.Add(new Statistic()
            {
                Value = GetFavoriteCount(readBooks).ToString(),
                Name = "Книг в избранном"
            });

            res.Add(GetDateStatistic(Date.Item1, Date.Item2));

            return res;
        }

        private static void GetDataForStatistic(List<ReadBook> readBooks, List<PendingBook> pendingBooks)
        {
            Thread ThCountRPage = new Thread(CountReadPage);
            ThCountRPage.Start(new List<ReadBook>(readBooks));

            Thread ThCountPPage = new Thread(CountLaterPage);
            ThCountPPage.Start(new List<PendingBook>(pendingBooks));

            Thread ThCategBook = new Thread(GetTypeBook);
            ThCategBook.Start(new List<ReadBook>(readBooks));

            Thread ThDateBook = new Thread(GetDateBook);
            ThDateBook.Start(new List<ReadBook>(readBooks));

            ThCountRPage.Join();
            ThCountPPage.Join();
            ThCategBook.Join();
            ThDateBook.Join();
        }

        #region Элементы статитстики
        private static Statistic GetCountPageStatistic(int red, int pendin)
        {
            Statistic statistic = new Statistic();
            var Elem = new[]
            {
                new Entry(red)
                {
                    ValueLabel = red.ToString(),
                    Label = "Страниц прочитано",
                    Color = SkiaSharp.SKColor.Parse("#fc6e51")
                },
                new Entry(pendin)
                {
                    ValueLabel = pendin.ToString(),
                    Label = "Страниц предстоит прочитать",
                    Color = SkiaSharp.SKColor.Parse("#5d9cec")
                }
            };

            var chart = new BarChart() { Entries = Elem };
            chart.LabelTextSize = 28;

            statistic.ChartStat = chart;
            statistic.Name = "Страницы";

            return statistic;
        }

        private static Statistic GetCountBookStatistic(int red, int pendin)
        {
            Statistic statistic = new Statistic();
            var Elem = new[]
            {
                new Entry(red)
                {
                    ValueLabel = red.ToString(),
                    Label = "Книг прочитано",
                    Color = SkiaSharp.SKColor.Parse("#fc6e51")
                },
                new Entry(pendin)
                {
                    ValueLabel = pendin.ToString(),
                    Label = "Книг в планах",
                    Color = SkiaSharp.SKColor.Parse("#5d9cec")
                }
            };

            var chart = new LineChart() { Entries = Elem };
            chart.LabelTextSize = 28;

            statistic.ChartStat = chart;
            statistic.Name = "Кол-во книг";

            return statistic;
        }

        private static Statistic GetCategoriStatisc(String[] Name,int[] Count )
        {
            Statistic statistic = new Statistic();

            var Elem = new[]
            {
                new Entry(Count[0])
                {
                    ValueLabel = Count[0].ToString(),
                    Label = Name[0].Length>=10?Name[0].Substring(0,10)+"...":Name[0],
                    Color = SkiaSharp.SKColor.Parse("#fc6e51")
                },

                new Entry(Count[1])
                {
                    ValueLabel = Count[1].ToString(),
                    Label = Name[1].Length>=10?Name[1].Substring(0,10)+"...":Name[1],
                    Color = SkiaSharp.SKColor.Parse("#5d9cec")
                },

                new Entry(Count[2])
                {
                    ValueLabel = Count[2].ToString(),
                    Label = Name[2].Length>=10?Name[2].Substring(0,10)+"...":Name[2],
                    Color = SkiaSharp.SKColor.Parse("#fc6e51")
                },

                new Entry(Count[3])
                {
                    ValueLabel = Count[3].ToString(),
                    Label = Name[3].Length>=10?Name[3].Substring(0,10)+"...":Name[3],
                    Color = SkiaSharp.SKColor.Parse("#5d9cec")
                },
            };

            var chart = new RadarChart() { Entries = Elem };
            chart.LabelTextSize = 26;
            chart.Margin = 50;

            statistic.ChartStat = chart;
            statistic.Name = "Жанры";

            return statistic;
        }

        private static Statistic GetDateStatistic(String[] Name, int[] Count)
        {
            Statistic statistic = new Statistic();

            var Elem = new[]
            {
                new Entry(Count[0])
                {
                    ValueLabel = Count[0].ToString(),
                    Label = Name[0],
                    Color = SkiaSharp.SKColor.Parse("#fc6e51")
                },

                new Entry(Count[1])
                {
                    ValueLabel = Count[1].ToString(),
                    Label = Name[1],
                    Color = SkiaSharp.SKColor.Parse("#5d9cec")
                },

                new Entry(Count[2])
                {
                    ValueLabel = Count[2].ToString(),
                    Label = Name[2],
                    Color = SkiaSharp.SKColor.Parse("#fc6e51")
                },

                new Entry(Count[3])
                {
                    ValueLabel = Count[3].ToString(),
                    Label = Name[3],
                    Color = SkiaSharp.SKColor.Parse("#5d9cec")
                }
            };

            var chart = new LineChart() { Entries = Elem };
            chart.LabelTextSize = 26;
            chart.Margin = 50;

            statistic.ChartStat = chart;
            statistic.Name = "Время";

            return statistic;
        }

        private static Statistic LikeAutor(List<ReadBook> readBooks)
        {
            Dictionary<string, int> autor = new Dictionary<string, int>();

            foreach (var item in readBooks)
            {
                if (item.Mark > 3)
                {
                    if (autor.ContainsKey(item.Autor))
                        autor[item.Autor] += 1;
                    else
                        autor.Add(item.Autor, 1);
                }
            }

            int max = 0;
            string key = "";

            foreach (var item in autor)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    key = item.Key;
                }

            }

            return new Statistic() { Value = key == "" ? "Отсутствует" : key, Name = "Любимый автор" };
        }
        #endregion

        private static string GetCountBookThisYear(List<ReadBook> readBooks)
        {
            int count = 0;

            foreach (var item in readBooks)
            {
                var dt = DateTime.Parse(item.DateReading);

                if (dt.Year == DateTime.Now.Year)
                    count++;
            }

            return count.ToString();
        }

        private static void GetDateBook(object listRead)
        {
            List<ReadBook> readBooks = listRead as List<ReadBook>;

            DateTime dt = DateTime.Now;

            String[] Name = 
            {
                dt.AddMonths(-3).ToString("MM.yyyy"),
                dt.AddMonths(-2).ToString("MM.yyyy"),
                dt.AddMonths(-1).ToString("MM.yyyy"),
                dt.ToString("MM.yyyy")
               
            };

            int[] Count = new int[Name.Length];

            foreach (var item in readBooks)
            {
                var dtTmp = DateTime.Parse(item.DateReading);

                if(dtTmp.Year == dt.Year)
                {
                    for (int i = 0; i < Name.Length; i++)
                    {
                        DateTime DtName = DateTime.Parse(Name[i]);

                        if (DtName.Month == dtTmp.Month)
                        {
                            Count[i] += 1;
                            break;
                        }
                            
                    }
                }
            }

            Date = (Name, Count);
        }

        private static string GetFavoriteCount(List<ReadBook> readBooks)
        {
            int count = 0;

            foreach (var item in readBooks)
            {
                if (item.Favorite)
                    count++;
            }

            return count.ToString();
        }

        private static void GetTypeBook(object listRead)
        {
            List<ReadBook> readBooks = listRead as List<ReadBook>;

            int CountCategori = UserControler.categories.Length;

            string[] Name = new string[CountCategori];
            UserControler.categories.CopyTo(Name, 0);
            int[] Count = new int[CountCategori];

            foreach (var item in readBooks)
            {
                Count[item.Categori] += 1;
            }

            for (int i = 1; i < Count.Length; i++)
            {
                for (int j = 0; j < Count.Length - i; j++)
                {
                    if (Count[j] < Count[j + 1])
                    {
                        (Count[j], Count[j + 1]) = (Count[j + 1], Count[j]);
                        (Name[j], Name[j + 1]) = (Name[j + 1], Name[j]);
                    }
                }
            }

            Array.Resize<string>(ref Name, 4);
            Array.Resize<int>(ref Count, 4);

            Categori = (Name, Count);
        }

        private static void CountLaterPage(object listPendig)
        {
            List<PendingBook> pendingBooks = listPendig as List<PendingBook>;

            int count = 0;

            foreach (var item in pendingBooks)
            {
                count += item.CountPage;
            }

            PPage =  count;
        }

        private static void CountReadPage(object listRead)
        {
            List<ReadBook> readBooks = (List<ReadBook>)listRead;

            int count = 0;

            foreach (var item in readBooks)
            {
                count += item.CountPage;
            }

            RPage = count;
        }

    }
}