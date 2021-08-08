using System.Collections.Generic;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;


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
            List<string> ls = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                ls.Add(i.ToString());
            }

            lst.Adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, ls);

            return v;
        }

        private void Fb_Click(object sender, System.EventArgs e)
        {
            Toast.MakeText(Activity, "Hello", ToastLength.Long).Show();
        }
    }
}