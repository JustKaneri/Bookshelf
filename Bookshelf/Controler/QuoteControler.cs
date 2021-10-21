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

        public event EventHandler BeginRepost;

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
                listQuot = DBControler.GetQuotes(idBook);
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

        public void StartRepost(int pos)
        {
            BeginRepost?.Invoke(pos, null);
        }

        internal List<Quotes> GetQuoteList()
        {
            return listQuot;
        }

        public void AddQuot(Quotes quot)
        {
            quot.Id =  DBControler.AddQuot(quot, idBook);
            listQuot.Add(quot);
        }

        public void DeleteQuot(int pos)
        {
            DBControler.DeleteQuites(listQuot[pos].Id);
            listQuot.RemoveAt(pos);
        }

        public void EditQuot(Quotes newQuot, int pos)
        {
            newQuot.Id = listQuot[pos].Id;
            DBControler.UpadteQuote(newQuot);
            listQuot[pos] = newQuot;
        }
    }
}