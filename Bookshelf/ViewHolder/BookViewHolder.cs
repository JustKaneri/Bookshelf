using System;
using Android.App;
using Android.Content;
using Android.Provider;
using Android.Support.V7.View.Menu;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;

namespace Bookshelf.Model
{
    class BookViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public ImageButton BtnFavorite { get;private set; }
        public TextView TxtAutor { get;private set; }
        public TextView TxtCategori { get; private set; }
        public TextView TxtDate { get; private set; }
        public ImageView ImvMark { get; private set; }

        public Button BtnPopMenu { get; set; }

        public BookViewHolder(View itemView) : base(itemView)
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            BtnPopMenu = itemView.FindViewById<Button>(Resource.Id.BtnOpenPopMenu);
            BtnFavorite = itemView.FindViewById<ImageButton>(Resource.Id.BtnFavorite);

            TxtAutor = itemView.FindViewById<TextView>(Resource.Id.TvAutor);
            TxtCategori = itemView.FindViewById<TextView>(Resource.Id.TvCategori);
            TxtDate = itemView.FindViewById<TextView>(Resource.Id.TvDate);
            ImvMark = itemView.FindViewById<ImageView>(Resource.Id.ImvMark);

            BtnPopMenu.Click += (s, arg) =>
            {
                Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu(Application.Context, BtnPopMenu);
                menu.Inflate(Resource.Menu.popupmenu);

                menu.MenuItemClick += (s1, arg1) =>
                {
                    switch (arg1.Item.ItemId)
                    {
                        case Resource.Id.menu_PreShow:
                            Preview(int.Parse(Image.Tag.ToString()));
                            break;
                        case Resource.Id.menu_Repost:
                            MainActivity._userControler.BeginRepost(int.Parse(Image.Tag.ToString()));
                            break;
                        case Resource.Id.menu_Quotes:
                            MainActivity._userControler.BeginOpenQuotes(int.Parse(Image.Tag.ToString()));
                            break;
                        case Resource.Id.menu_Edit:
                            MainActivity._userControler.BegingUpdate(int.Parse(Image.Tag.ToString()), UserControler.TypeBook.ReadBook);
                            break;
                        case Resource.Id.menu_Del:
                            MainActivity._userControler.BeginDelete(int.Parse(Image.Tag.ToString()), UserControler.TypeBook.ReadBook);
                            break;
                    }
                };
                
                menu.Show();
            };

            BtnFavorite.Click += BtnFavorite_Click;
        }

        private void Preview(int pos)
        {
            View view  = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.PreviewReadPage, null , false);
            var book = MainActivity._userControler.GetBooks()[pos];

            view.FindViewById<TextView>(Resource.Id.TxtMarkPrev).Text = book.Mark+"/5";
            view.FindViewById<TextView>(Resource.Id.TxtDatePrev).Text = book.DateReading;
            view.FindViewById<ImageView>(Resource.Id.ImvPhotoPrev).SetImageBitmap(book.Photo);
            view.FindViewById<ImageView>(Resource.Id.ImvFon).SetImageBitmap(book.Photo);
            view.FindViewById<TextView>(Resource.Id.TxtNamePrev).Text = book.Name;
            view.FindViewById<TextView>(Resource.Id.TxtAutorPrev).Text = book.Autor;
            view.FindViewById<TextView>(Resource.Id.TxtCategoriPrev).Text = UserControler.categories[book.Categori];
            view.FindViewById<TextView>(Resource.Id.TxtStrPrev).Text = book.CountPage.ToString();
            view.FindViewById<TextView>(Resource.Id.TxtDiscriptPrev).Text = book.Discript==""?"Описание отсутствует": book.Discript;

            new AlertDialog.Builder(BtnPopMenu.Context).SetView(view).Show();
           
        }


        private void BtnFavorite_Click(object sender, EventArgs e)
        {
            int id = int.Parse(BtnFavorite.Tag.ToString());

            MainActivity._userControler._shelf.readBooksArray[id].Favorite = !MainActivity._userControler._shelf.readBooksArray[id].Favorite;

            bool res = MainActivity._userControler._shelf.readBooksArray[id].Favorite;

            DBControler.UpdateFavoriteStatus(MainActivity._userControler._shelf.readBooksArray[id].ID, res);

            if (res)
            {
                ((ImageButton)sender).SetBackgroundResource(Resource.Drawable.Favorite);
                Toast.MakeText(((ImageButton)sender).Context,MainActivity._userControler._shelf.readBooksArray[id].Name + "- добавлено в избранное.", ToastLength.Short).Show();
            }              
            else
            {
                ((ImageButton)sender).SetBackgroundResource(Resource.Drawable.NoFavorite);
                Toast.MakeText(((ImageButton)sender).Context, MainActivity._userControler._shelf.readBooksArray[id].Name + "- удаленно из избранного.", ToastLength.Short).Show();
            }
                
        }
    }
}
