using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;

namespace Bookshelf
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private Android.Support.V4.App.Fragment fragment = null;

        public static UserControler _userControler;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            _userControler = new UserControler();

            fragment = FragmentRead.NewInstance();
            LoadFragment(fragment);

            MainActivity._userControler.StartReadUpdate += _userControler_StartReadUpdate;
            MainActivity._userControler.StartReadDelete += _userControler_StartReadDelete;

            MainActivity._userControler.StartLaterUpdate += _userControler_StartLaterUpdate;
            MainActivity._userControler.StartLaterDelete += _userControler_StartLaterDelete;
            MainActivity._userControler.StartCopy += _userControler_StartCopy;
        }

        /// <summary>
        /// Перемещение из отложенного в прочитанное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartCopy(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Удаление отложенной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartLaterDelete(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Обновление отложенной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartLaterUpdate(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Удаление прочитанной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartReadDelete(object sender, System.EventArgs e)
        {
            new Android.App.AlertDialog.Builder(this)
                .SetTitle("Удаление")
                .SetMessage("Удалить выбранную книгу?")
                .SetPositiveButton("Да", delegate
                {
                    MainActivity._userControler.Delete(int.Parse(sender.ToString()), true);
                    fragment = FragmentRead.NewInstance();
                    LoadFragment(fragment);
                })
                .SetNegativeButton("Нет", delegate { }).Show();
        }

        /// <summary>
        /// Редактирование прочитанной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartReadUpdate(object sender, System.EventArgs e)
        {
            Intent edt = new Intent(this, typeof(ActivityAdding));
            edt.PutExtra("status", "edit_read");
            edt.PutExtra("id", int.Parse(sender.ToString()));
            StartActivityForResult(edt, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == 0)
            {
                fragment = FragmentRead.NewInstance();
                LoadFragment(fragment);
            }
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    fragment = FragmentRead.NewInstance();
                    LoadFragment(fragment);
                    return true;
                case Resource.Id.navigation_dashboard:
                    fragment = FragmentLater.NewInstance();
                    LoadFragment(fragment);
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

