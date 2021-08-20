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
        private FloatingActionButton fb;
        private Activity activ;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //MainActivity._userControler.StartReadUpdate += _userControler_StartUpdate;
            //MainActivity._userControler.StartReadDelete += _userControler_StartReadDelete;

            v = inflater.Inflate(Resource.Layout.ReadPage, container, false);
            fb = v.FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddRead);
            fb.Click += Fb_Click;

            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.RecRead);
            FillRecylerView();

            activ = Activity;

            return v;
        }

        private void FillRecylerView()
        {
            BookAdapter adapter = new BookAdapter(MainActivity._userControler.GetBooks());
            
            // Plug the adapter into the RecyclerView:
            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            fb.Enabled = false;
            Intent add = new Intent(Activity, typeof(ActivityAdding));
            add.PutExtra("status", "add_read");
            StartActivityForResult(add, 0);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == 0)
            {
                FillRecylerView();
                fb.Enabled = true;
            }
        }
    }
}