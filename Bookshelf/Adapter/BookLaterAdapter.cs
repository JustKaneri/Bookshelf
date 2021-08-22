using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
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
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListLaterItem ,parent, false);

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