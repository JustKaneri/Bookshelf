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

namespace Bookshelf.Model
{
    class BookLaterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public ImageButton BtnEdit { get; private set; }
        public ImageButton BtnDele { get; private set; }
        public ImageButton BtnMove { get; private set; }

        public BookLaterViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.ImgLater);
            Caption = itemView.FindViewById<TextView>(Resource.Id.TxtLater);
            BtnEdit = itemView.FindViewById<ImageButton>(Resource.Id.BtnEditLater);
            BtnDele = itemView.FindViewById<ImageButton>(Resource.Id.BtnDelLater);
            BtnMove = itemView.FindViewById<ImageButton>(Resource.Id.BtnMovLater);

            BtnEdit.Click += delegate
            {
                MainActivity._userControler.BegingUpdate(int.Parse(BtnEdit.Tag.ToString()), false);
            };

            BtnDele.Click += delegate
            {
                MainActivity._userControler.BeginDelete(int.Parse(BtnDele.Tag.ToString()), false);
            };

            BtnMove.Click += delegate
            {
                MainActivity._userControler.StartMoved(int.Parse(BtnMove.Tag.ToString()));

            };

        }
    }

}