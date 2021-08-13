using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Bookshelf
{
    public class FragmentLater : Android.Support.V4.App.Fragment
    {
        public static FragmentLater NewInstance()
        {
            var bundle = new Bundle();

            return new FragmentLater { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            return inflater.Inflate(Resource.Layout.LaterPage, container, false);
        }
    }
}