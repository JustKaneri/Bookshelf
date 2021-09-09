using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Bookshelf.Adapter;
using Bookshelf.Controler;

namespace Bookshelf
{
    public class FragmentStatistic : Android.Support.V4.App.Fragment
    {
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;

        public static FragmentStatistic NewInstance()
        {
            var bundle = new Bundle();
            
            return new FragmentStatistic { Arguments = bundle };
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View v = inflater.Inflate(Resource.Layout.StatisticPage, container, false);

            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.LstStat);

            FillRecylerView();

            return v;
        }

        private void FillRecylerView()
        {
            var stat = StatisticControler.GetList(MainActivity._userControler.GetBooks(), MainActivity._userControler.GetPendingBooks());

            StatisticAdapter adapter = new StatisticAdapter(stat.Item1,stat.Item2);

            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

    }
}