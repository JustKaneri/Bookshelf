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

namespace Bookshelf.Adapter
{
    class BookLaterAdapter : RecyclerView.Adapter
    {
        private List<PendingBook> books;

        public BookLaterAdapter(List<PendingBook> books)
        {
            this.books = books;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListLaterItem ,parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            BookLaterViewHolder bv = new BookLaterViewHolder(itemView);
            return bv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookLaterViewHolder book = holder as BookLaterViewHolder;

            book.Image.SetImageBitmap(books[position].Photo);
            book.Caption.Text = books[position].Name;
            book.BtnEdit.Tag = position.ToString();
            book.BtnDele.Tag = position.ToString();
            book.BtnMove.Tag = position.ToString();


        }

        public override int ItemCount
        {
            get
            {
                return books.Count;
            }
        }
    }
}