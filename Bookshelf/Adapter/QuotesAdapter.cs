using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Bookshelf.Model;
using Bookshelf.ViewHolder;

namespace Bookshelf.Adapter
{
    class QuotesAdapter : RecyclerView.Adapter
    {
        private List<Quotes> quotes;

        public QuotesAdapter(List<Quotes> quotes)
        {
            this.quotes = quotes;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListQuotItem, parent, false);

            QuoteViewHolder bv = new QuoteViewHolder(itemView);
            return bv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            QuoteViewHolder quot = holder as QuoteViewHolder;

            quot.Caption.Tag = position;
            quot.Caption.Text = quotes[position].Autor;
            quot.Quot.Text = "\" " + quotes[position].Quot + " \" ";
        }

        public override int ItemCount
        {
            get
            {
                return quotes.Count;
            }
        }
    }
}