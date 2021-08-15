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
using Bookshelf.Adapter;
using Bookshelf.Controler;

namespace Bookshelf
{
    public class FragmentLater : Android.Support.V4.App.Fragment
    {
        public static FragmentLater NewInstance()
        {
            var bundle = new Bundle();

            return new FragmentLater { Arguments = bundle };
        }

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private View v;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            v = inflater.Inflate(Resource.Layout.LaterPage, container, false);

            FloatingActionButton fb = v.FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddLater);
            fb.Click += Fb_Click;

            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.RecLater);
            FillRecylerView();

            return v;
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            Intent add = new Intent(Activity, typeof(ActivityAdding));
            add.PutExtra("status", "add_later");
            StartActivityForResult(add, 0);
        }

        private void FillRecylerView()
        {
            BookLaterAdapter adapter = new BookLaterAdapter(MainActivity._userControler.GetPendingBooks());

            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == 0)
            {
                FillRecylerView();
            }
        }
    }
}