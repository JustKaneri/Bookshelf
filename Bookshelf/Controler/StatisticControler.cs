using System;
using System.Collections.Generic;
using Bookshelf.Model;

namespace Bookshelf.Controler
{
    public class StatisticControler
    {
        private StatisticControler() { }

        public static (List<string>,List<string>) GetList (List<ReadBook> readBooks, List<PendingBook> pendingBooks)
        {
            List<string> res = new List<string>();

            res.Add(readBooks.Count.ToString());
            res.Add(CountReadPage(readBooks));
            res.Add(LikeTypeBook(readBooks));
            res.Add(LikeAutor(readBooks));
            res.Add(GetFavoriteCount(readBooks));

            res.Add(GetCountBookThisMonth(readBooks));
            res.Add(GetCountBookThisYear(readBooks));
            res.Add(DBControler.CountQuotes());

            res.Add(pendingBooks.Count.ToString());
            res.Add(CountLaterPage(pendingBooks));


            List<string> name = new List<string>();
            name.Add("Прочитанно книг:");
            name.Add("Прочитанно страниц:");
            name.Add("Любимый жанр:");
            name.Add("Любимый автор:");
            name.Add("Избранных книг:");
            name.Add("Прочитано за этот месяц:");
            name.Add("Прочитано за этот год:");
            name.Add("Цитат: ");
            name.Add("Отложенно книг::");
            name.Add("Страниц предстоит прочитать:");

            return (name,res);
        }

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

        private static string GetCountBookThisMonth(List<ReadBook> readBooks)
        {
            int count = 0;

            foreach (var item in readBooks)
            {
                var dt = DateTime.Parse(item.DateReading);

                if (dt.Month == DateTime.Now.Month && dt.Year == DateTime.Now.Year)
                    count++;  
            }

            return count.ToString();
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

        private static string LikeAutor(List<ReadBook> readBooks)
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

            return key == "" ? "Отсутствует" : key;
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