using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
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
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItem, parent, false);

            BookViewHolder bv = new BookViewHolder(itemView);
            return bv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookViewHolder book = holder as BookViewHolder;

            book.Image.SetImageBitmap(bookArray[position].Photo);
            book.Caption.Text = bookArray[position].Name;
            book.BtnEdit.Tag = position.ToString();
            book.BtnDele.Tag = position.ToString();
            book.BtnFavorite.Tag = position.ToString();
            if (MainActivity._userControler._shelf.readBooksArray[position].Favorite)
                book.BtnFavorite.SetBackgroundResource(Resource.Drawable.Favorite);
            else
                book.BtnFavorite.SetBackgroundResource(Resource.Drawable.NoFavorite);
        }

        public override int ItemCount
        {
            get
            {
                return bookArray.Count;
            }
        }
    }
}