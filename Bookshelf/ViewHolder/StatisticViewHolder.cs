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
    class StatisticViewHolder : RecyclerView.ViewHolder
    {
        public TextView TxtName { get; private set; }
        public TextView TxtStat { get; private set; }

        public StatisticViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            TxtName = itemView.FindViewById<TextView>(Resource.Id.TxtNameSt);
            TxtStat = itemView.FindViewById<TextView>(Resource.Id.TxtResStat);
        }
    }
}