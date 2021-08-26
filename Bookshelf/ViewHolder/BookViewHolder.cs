using System;
using Android.App;
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
        public ImageButton BtnEdit { get; private set; }
        public ImageButton BtnDele { get; private set; }
        public ImageButton BtnFavorite { get; set; }

        public BookViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            BtnEdit = itemView.FindViewById<ImageButton>(Resource.Id.BtnEdit);
            BtnDele = itemView.FindViewById<ImageButton>(Resource.Id.BtnDel);
            BtnFavorite = itemView.FindViewById<ImageButton>(Resource.Id.BtnFavorite);

            BtnEdit.Click += delegate
            {
                MainActivity._userControler.BegingUpdate(int.Parse(BtnEdit.Tag.ToString()),true);
            };

            BtnEdit.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Редактировать книгу", ToastLength.Short).Show();
            };


            BtnDele.Click += delegate
            {
                MainActivity._userControler.BeginDelete(int.Parse(BtnDele.Tag.ToString()), true);
            };

            BtnDele.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Удалить книгу", ToastLength.Short).Show();
            };

            Image.Click += delegate
            {
                MainActivity._userControler.BeginOpenQuotes(int.Parse(Image.Tag.ToString()));
            };

            Image.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Открыть страницу с цитатами из этой книги", ToastLength.Short).Show();
            };


            BtnFavorite.Click += BtnFavorite_Click;
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
