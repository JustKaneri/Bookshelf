using System.Collections.Generic;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;
using Android.Content.Res;

namespace Bookshelf
{
    public class FragmentStatistic : Android.Support.V4.App.Fragment
    {
        

        public static FragmentStatistic NewInstance()
        {
            var bundle = new Bundle();
            
            return new FragmentStatistic { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.StatisticPage, container, false);

            ListView lst = v.FindViewById<ListView>(Resource.Id.LstStat);

            lst.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, StatisticControler.GetList(MainActivity._userControler.GetBooks(),MainActivity._userControler.GetPendingBooks()));
                


            return v;
        }

    }
}