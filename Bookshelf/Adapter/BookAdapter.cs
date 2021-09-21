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
            View itemView = null;

            if (MainActivity._appController.GetTypeView(UserControler.TypeBook.ReadBook) == ApplicationController.TypeView.MinInfo)
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItem, parent, false);
            else
                itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItemSecond, parent, false);

            BookViewHolder bv = new BookViewHolder(itemView);
            return bv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            BookViewHolder book = holder as BookViewHolder;

            book.Image.SetImageBitmap(bookArray[position].Photo);
            book.Caption.Text = bookArray[position].Name;
            //book.BtnEdit.Tag = position.ToString();
            //book.BtnDele.Tag = position.ToString();
            //book.BtnFavorite.Tag = position.ToString();
            book.Image.Tag = position.ToString();

            if (MainActivity._userControler._shelf.readBooksArray[position].Favorite)
                book.BtnFavorite.SetBackgroundResource(Resource.Drawable.Favorite);
            else
                book.BtnFavorite.SetBackgroundResource(Resource.Drawable.NoFavorite);

            if(MainActivity._appController.GetTypeView(UserControler.TypeBook.ReadBook) == ApplicationController.TypeView.MaxInfo)
            {
                book.TxtAutor.Text = "Автор: "+ bookArray[position].Autor;
                book.TxtCategori.Text = "Жанр: "+UserControler.categories[bookArray[position].Categori];
                book.TxtDate.Text = "Дата прочтения: " + bookArray[position].DateReading;

                switch (bookArray[position].Mark)
                {
                    case 1:
                        book.ImvMark.SetImageResource(Resource.Drawable.MarkOne);
                        break;
                    case 2:
                        book.ImvMark.SetImageResource(Resource.Drawable.MarkTwo);
                        break;
                    case 3:
                        book.ImvMark.SetImageResource(Resource.Drawable.MarkThree);
                        break;
                    case 4:
                        book.ImvMark.SetImageResource(Resource.Drawable.MarkFour);
                        break;
                    case 5:
                        book.ImvMark.SetImageResource(Resource.Drawable.MarkFive);
                        break;
                }
            }
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