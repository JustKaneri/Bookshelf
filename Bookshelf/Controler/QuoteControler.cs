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
        private int posBook;
        private List<Quotes> listQuot;

        public event EventHandler BeginUpdate;
        public event EventHandler BeginDelete;

        public QuoteControler(int id,int pos)
        {
            idBook = id;
            posBook = pos;

            if (MainActivity._userControler.GetBooks()[posBook].list != null)
            {
                listQuot = MainActivity._userControler.GetBooks()[posBook].list;
            }
            else
            {
                listQuot = new List<Quotes>();
                MainActivity._userControler.GetBooks()[posBook].list = listQuot;
            }
                
        }

        public void StartUpdate(int pos)
        {
            BeginUpdate?.Invoke(pos, null);
        }

        public void StartDelet(int pos)
        {
            BeginDelete?.Invoke(pos, null);
        }

        internal List<Quotes> GetQuoteList()
        {
            return listQuot;
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