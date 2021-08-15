﻿using System;
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
    class BookViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public ImageButton BtnEdit { get; private set; }
        public ImageButton BtnDele { get; private set; }

        public BookViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.imageView);
            Caption = itemView.FindViewById<TextView>(Resource.Id.textView);
            BtnEdit = itemView.FindViewById<ImageButton>(Resource.Id.BtnEdit);
            BtnDele = itemView.FindViewById<ImageButton>(Resource.Id.BtnDel);

            BtnEdit.Click += delegate
            {
                MainActivity._userControler.BegingUpdate(int.Parse(BtnEdit.Tag.ToString()),true);
            };

            BtnDele.Click += delegate
            {
                MainActivity._userControler.BeginDelete(int.Parse(BtnDele.Tag.ToString()), true);
            };
        }

    }

}