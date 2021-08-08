using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Bookshelf
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private FloatingActionButton fb;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    fragment = FragmentRead.NewInstance();
                    LoadFragment(fragment);
                    return true;
                case Resource.Id.navigation_dashboard:
                   
                    return true;
                case Resource.Id.navigation_statistic:
                    fragment = FragmentStatistic.NewInstance();
                    LoadFragment(fragment);
                    return true;
            }


            return false;
        }

        private void LoadFragment(Android.Support.V4.App.Fragment fragment)
        {
            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.fragment_container, fragment).Commit();
        }

    }
}

