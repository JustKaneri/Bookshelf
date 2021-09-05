using System;
using System.Collections.Generic;
using Bookshelf.Model;

namespace Bookshelf.Controler
{
    public static class Sorting
    {
        public static void SortByName(this List<ReadBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count-i; j++)
                {
                    if (rbb[j].Name[0] > rbb[j + 1].Name[0])
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }                      
                    else
                    if(rbb[j].Name[0] == rbb[j + 1].Name[0])
                    {
                        if (rbb[j].Name[1] > rbb[j + 1].Name[1])
                        {
                            (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                        }
                    }
                }
            }
        }

        public static void SortByAutor(this List<ReadBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count - i; j++)
                {
                    if (rbb[j].Autor[0] > rbb[j + 1].Autor[0])
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }
                    else
                    if (rbb[j].Autor[0] == rbb[j + 1].Autor[0])
                    {
                        if (rbb[j].Autor[1] > rbb[j + 1].Autor[1])
                        {
                            (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                        }
                    }
                }
            }
        }

        public static void SortByDate(this List<ReadBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count - i; j++)
                {
                    if (DateTime.Parse(rbb[j].DateReading) < DateTime.Parse(rbb[j + 1].DateReading))
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }
                }
            }
        }

        public static void SortByMark(this List<ReadBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count - i; j++)
                {
                    if (rbb[j].Mark < rbb[j + 1].Mark)
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }
                }

            }
        }

        public static void SortByFavorite(this List<ReadBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count - i; j++)
                {
                    if (rbb[j].Favorite == false && rbb[j + 1].Favorite == true)
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }
                }
            }
        }

        public static void SortByName(this List<PendingBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count - i; j++)
                {
                    if (rbb[j].Name[0] > rbb[j + 1].Name[0])
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }
                    else
                    if (rbb[j].Name[0] == rbb[j + 1].Name[0])
                    {
                        if (rbb[j].Name[1] > rbb[j + 1].Name[1])
                        {
                            (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                        }
                    }
                }
            }
        }

        public static void SortByAutor(this List<PendingBook> rbb)
        {
            for (int i = 1; i < rbb.Count; i++)
            {
                for (int j = 0; j < rbb.Count - i; j++)
                {
                    if (rbb[j].Autor[0] > rbb[j + 1].Autor[0])
                    {
                        (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                    }
                    else
                    if (rbb[j].Autor[0] == rbb[j + 1].Autor[0])
                    {
                        if (rbb[j].Autor[1] > rbb[j + 1].Autor[1])
                        {
                            (rbb[j], rbb[j + 1]) = (rbb[j + 1], rbb[j]);
                        }
                    }
                }
            }
        }
    }
}