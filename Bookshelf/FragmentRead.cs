using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;
using Bookshelf.Model;

namespace Bookshelf
{
    public class FragmentRead : Android.Support.V4.App.Fragment
    {
        public static FragmentRead NewInstance()
        {
            var bundle = new Bundle();

            return new FragmentRead { Arguments = bundle };
        }

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private View v;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

            v = inflater.Inflate(Resource.Layout.ReadPage, container, false);
            FloatingActionButton fb = v.FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddRead);
            fb.Click += Fb_Click;

            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.RecRead);
            FillRecylerView();

            return v;
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (sender != null)
                Toast.MakeText(Activity, ((Button)sender).Text, ToastLength.Short).Show();
        }

        private void FillRecylerView()
        {
            BookAdapter adapter = new BookAdapter(MainActivity._userControler.GetBooks());
            

            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);

        }

        public void GetId(int id)
        {
            Toast.MakeText(Activity, id.ToString(), ToastLength.Short);
        }


        private void Fb_Click(object sender, EventArgs e)
        {
            Intent add = new Intent(Activity, typeof(ActivityAdding));
            add.PutExtra("status", "add_read");
            StartActivityForResult(add, 0);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(resultCode == 0)
            {
                FillRecylerView();
            }
        }
    }
}