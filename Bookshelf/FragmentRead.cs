using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Bookshelf
{
    public class FragmentRead : Android.Support.V4.App.Fragment
    {
        public static FragmentRead NewInstance()
        {
            var bundle = new Bundle();

            return new FragmentRead { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            var v = inflater.Inflate(Resource.Layout.ReadPage, container, false);
            FloatingActionButton fb = v.FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddRead);
            fb.Click += Fb_Click;

            return v;
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            Intent add = new Intent(Activity, typeof(ActivityAdding));
            StartActivity(add);
        }
    }
}