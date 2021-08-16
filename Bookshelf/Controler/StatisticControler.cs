using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bookshelf.Model;

namespace Bookshelf.Controler
{
    public class StatisticControler
    {
        private StatisticControler() { }

        public static List<string> GetList(List<ReadBook> readBooks, List<PendingBook> pendingBooks)
        {
            List<string> statistic = new List<string>();
            statistic.Add("Прочитанно книг: " +readBooks.Count.ToString());
            statistic.Add("Прочитанно страниц: " + CountReadPage(readBooks));
            statistic.Add("Любимый жанр: " + LikeTypeBook(readBooks));

            statistic.Add("Отложенно книг: " +pendingBooks.Count.ToString());
            statistic.Add("Страниц предстоит прочитать: " +CountLaterPage(pendingBooks));

            return statistic;
        }

        private static string CountLaterPage(List<PendingBook> pendingBooks)
        {
            int count = 0;

            foreach (var item in pendingBooks)
            {
                count += item.CountPage;
            }

            return count.ToString();
        }

        private static string LikeTypeBook(List<ReadBook> readBooks)
        {
            Dictionary<int, int> type = new Dictionary<int, int>();

            foreach (var item in readBooks)
            {
                if (item.Mark > 3)
                {
                    if (type.ContainsKey(item.Categori))
                        type[item.Categori] += 1;
                    else
                        type.Add(item.Categori, 1);
                }
            }

            int max = 0;
            int key = -1;

            foreach (var item in type)
            {
                if(item.Value > max)
                {
                    max = item.Value;
                    key = item.Key;
                }
               
            }

            return key == -1 ? "Отсутствует": UserControler.categories[key];
        }

        private static string CountReadPage(List<ReadBook> readBooks)
        {
            int count = 0;

            foreach (var item in readBooks)
            {
                count += item.CountPage;
            }

            return count.ToString();
        }

    }
}