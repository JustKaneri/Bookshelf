using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Model;

namespace Bookshelf.Controler
{
    class BookAdapter: RecyclerView.Adapter
    {
        private List<ReadBook> bookArray { get;set; }
        
        public BookAdapter(List<ReadBook> books)
        {
            bookArray = books;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItem, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            BookViewHolder bv = new BookViewHolder(itemView);
            return bv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookViewHolder book = holder as BookViewHolder;

            book.Image.SetImageBitmap(bookArray[position].Photo);
            book.Caption.Text = bookArray[position].Name;
            book.BtnEdit.Tag = position.ToString();
        }

       

        public override int ItemCount
        {
            get { return bookArray.Count; }
        }
    }
}