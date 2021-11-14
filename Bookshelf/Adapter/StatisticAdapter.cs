using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Bookshelf.Model;
using Bookshelf.ViewHolder;

namespace Bookshelf.Adapter
{
    public class StatisticAdapter : RecyclerView.Adapter
    {
        private List<Statistic> AllStatisc = new List<Statistic>();

        public StatisticAdapter(List<Statistic> stat)
        {
            AllStatisc = stat;
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

            statistic.TxtName.Text = AllStatisc[position].Name;
            //statistic.TxtStat.Text = lstResult[position];
            if(AllStatisc[position].ChartStat == null)
            {
                statistic.ImvChart.Visibility = ViewStates.Gone;
                statistic.TxtStat.Text = AllStatisc[position].Value;
            }
            else
            {
                statistic.ImvChart.Chart = AllStatisc[position].ChartStat;
                statistic.TxtStat.Visibility = ViewStates.Gone;
            }
            

        }

        public override int ItemCount
        {
            get
            {
                return AllStatisc.Count;
            }
        }
    }
}