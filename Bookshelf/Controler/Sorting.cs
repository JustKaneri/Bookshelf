using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
    }
}