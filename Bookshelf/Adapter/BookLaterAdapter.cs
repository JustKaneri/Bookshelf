using System.Collections.Generic;
using Android.Content.Res;
using Android.Support.V7.Widget;
using Android.Views;
using Bookshelf.Controler;
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
            View itemView = null;

            if (MainActivity._appController.GetTypeView(Controler.UserControler.TypeBook.PendingBook) == Controler.ApplicationController.TypeView.MinInfo)
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListLaterItem ,parent, false);
            else
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListLaterItemSecond, parent, false);

            BookLaterViewHolder bv = new BookLaterViewHolder(itemView);
            return bv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookLaterViewHolder book = holder as BookLaterViewHolder;

            book.Image.SetImageBitmap(books[position].Photo);
            book.Caption.Text = books[position].Name;
            book.BtnMove.Tag = position.ToString();


            if (MainActivity._appController.GetTypeView(Controler.UserControler.TypeBook.PendingBook) == Controler.ApplicationController.TypeView.MaxInfo)
            {
                book.TxtAutor.Text = "Автор: " + books[position].Autor;
                book.TxtCategori.Text = "Жанр: " + UserControler.categories[books[position].Categori];
            }
            else
            {
                if (books[position].Name.Length > 12)
                    book.Caption.Text = books[position].Name.Substring(0, 12) + "...";

                int Height = GetSize();

                book.ImageFon.SetImageBitmap(books[position].Photo);
                var param = book.ImageFon.LayoutParameters;
                var paramLayot = book.Layout.LayoutParameters;
                var paramView = book.Image.LayoutParameters;

                paramLayot.Height = (int)(Height / 2);
                param.Height = (int)(Height / 2) - 20;
                paramView.Height = (int)(Height / 2.8);

                book.ImageFon.LayoutParameters = param;
                book.Layout.LayoutParameters = paramLayot;
                book.Image.LayoutParameters = paramView;
            }

        }

        private int GetSize()
        {
            var pixels = Resources.System.DisplayMetrics.HeightPixels;
            //var scale = Resources.System.DisplayMetrics.Density;
            //var dps = (double)((pixels - 0.5f) / scale);
            //return (int)(dps);
            return pixels - 200;
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