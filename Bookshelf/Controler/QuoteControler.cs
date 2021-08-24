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
    public class QuoteControler
    {
        private int idBook;
        private List<Quotes> listQuot;

        public QuoteControler()
        {
            if (MainActivity._userControler.GetBooks()[idBook].list != null)
            {
                listQuot = MainActivity._userControler.GetBooks()[idBook].list;
            }
            else
            {
                listQuot = new List<Quotes>();
                MainActivity._userControler.GetBooks()[idBook].list = listQuot;
            }
                
        }

        public void AddQuot(Quotes quot)
        {
            listQuot.Add(quot);
        }

        public void DeleteQuot(int pos)
        {
            listQuot.RemoveAt(pos);
        }

        public void EditQuot(Quotes newQuot, int pos)
        {
            listQuot[pos] = newQuot;
        }
    }
}