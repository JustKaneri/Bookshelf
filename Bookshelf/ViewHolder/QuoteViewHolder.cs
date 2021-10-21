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
        public Button BtnPopMenu { get; set; }

        public QuoteViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Quot = itemView.FindViewById<TextView>(Resource.Id.TxtQuot);
            Caption = itemView.FindViewById<TextView>(Resource.Id.TxtAutorQ);
            BtnPopMenu = itemView.FindViewById<Button>(Resource.Id.BtnOpenPopMenu);

            BtnPopMenu.Click += (s, arg) =>
            {
                Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu(Application.Context, BtnPopMenu);
                menu.Inflate(Resource.Menu.PopupMenuQuotes);

                menu.MenuItemClick += (s1, arg1) =>
                {
                    switch (arg1.Item.ItemId)
                    {
                        
                        case Resource.Id.menu_Edit:
                            PageQuotes._quoteControler.StartUpdate(int.Parse(Caption.Tag.ToString()));
                            break;
                        case Resource.Id.menu_Rep:
                            PageQuotes._quoteControler.StartRepost(int.Parse(Caption.Tag.ToString()));
                            break;
                        case Resource.Id.menu_Del:
                            PageQuotes._quoteControler.StartDelet(int.Parse(Caption.Tag.ToString()));
                            break;
                    }
                };

                menu.Show();
            };

        }
    }
}