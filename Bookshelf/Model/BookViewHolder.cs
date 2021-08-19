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

            BtnDele.Click += delegate
            {
                MainActivity._userControler.BeginDelete(int.Parse(BtnDele.Tag.ToString()), true);
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
