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
using Bookshelf.Adapter;
using Bookshelf.Controler;

namespace Bookshelf
{
    [Activity(Label = "PageQuotes")]
    public class PageQuotes : Activity
    {
        public static QuoteControler _quoteControler;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuotesPage);
            // Create your application here

            int id = Intent.GetIntExtra("id", -1);
            int pos = Intent.GetIntExtra("pos", -1);

            _quoteControler = new QuoteControler(id, pos);

            _quoteControler.BeginDelete += _quoteControler_BeginDelete;
            _quoteControler.BeginUpdate += _quoteControler_BeginUpdate;
        }

        private void FillRecylerView()
        {
            QuotesAdapter adapter = new QuotesAdapter(_quoteControler.GetQuoteList());

            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

        private void _quoteControler_BeginUpdate(object sender, EventArgs e)
        {
            
        }

        private void _quoteControler_BeginDelete(object sender, EventArgs e)
        {
            
        }
    }
}