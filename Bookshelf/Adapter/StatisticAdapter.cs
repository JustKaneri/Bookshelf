using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Bookshelf.ViewHolder;

namespace Bookshelf.Adapter
{
    public class StatisticAdapter : RecyclerView.Adapter
    {
        private List<string> lstName = new List<string>();
        private List<string> lstResult = new List<string>();

        public StatisticAdapter(List<string> name,List<string> res)
        {
            lstName = name;
            lstResult = res;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ListItemStatistic, parent, false);

            StatisticViewHolder sv = new StatisticViewHolder(itemView);
            
            return sv;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            //BookViewHolder book = holder as BookViewHolder;
            StatisticViewHolder statistic = holder as StatisticViewHolder;

            statistic.TxtName.Text = lstName[position];
            statistic.TxtStat.Text = lstResult[position];
        }

        public override int ItemCount
        {
            get
            {
                return lstName.Count;
            }
        }
    }
}