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

namespace Bookshelf.ViewHolder
{
    class QuoteViewHolder : RecyclerView.ViewHolder
    {
        public TextView Quot { get; private set; }
        public TextView Caption { get; private set; }
        public ImageButton BtnEdit { get; private set; }
        public ImageButton BtnDele { get; private set; }

        public QuoteViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Quot = itemView.FindViewById<TextView>(Resource.Id.TxtQuot);
            Caption = itemView.FindViewById<TextView>(Resource.Id.TxtAutorQ);
            BtnEdit = itemView.FindViewById<ImageButton>(Resource.Id.BtnEditQ);
            BtnDele = itemView.FindViewById<ImageButton>(Resource.Id.BtnDelQ);

            BtnEdit.Click += delegate
            {
                MainActivity._userControler.BegingUpdate(int.Parse(BtnEdit.Tag.ToString()), true);
            };

            BtnDele.Click += delegate
            {
                MainActivity._userControler.BeginDelete(int.Parse(BtnDele.Tag.ToString()), true);
            };

        }
    }
}