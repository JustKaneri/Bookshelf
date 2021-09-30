using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Bookshelf.Controler;
using Bookshelf.Model;
using System;

namespace Bookshelf
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private Android.Support.V4.App.Fragment fragment = null;

        public static UserControler _userControler;

        public static ApplicationController _appController;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;

            OpenPreviewScreen();

            SetContentView(Resource.Layout.activity_main);
           
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

            _appController = new ApplicationController();

        }

        private void ConnectEvent()
        {
            _userControler.StartReadUpdate += _userControler_StartReadUpdate;
            _userControler.StartReadDelete += _userControler_StartReadDelete;
            _userControler.StartOpenQuotes += _userControler_StartOpenQuotes;

            _userControler.StartLaterUpdate += _userControler_StartLaterUpdate;
            _userControler.StartLaterDelete += _userControler_StartLaterDelete;
            _userControler.StartCopy += _userControler_StartCopy;
        }

        private void _userControler_StartOpenQuotes(object sender, EventArgs e)
        {
            Intent quot = new Intent(this, typeof(PageQuotes));
            quot.PutExtra("id", _userControler.GetBooks()[int.Parse(sender.ToString())].ID);
            quot.PutExtra("pos", int.Parse(sender.ToString()));
            quot.PutExtra("name", _userControler.GetBooks()[int.Parse(sender.ToString())].Name);
            StartActivity(quot);
        }

        private void OpenPreviewScreen()
        {
            Intent intent = new Intent(this,typeof(PagePreview));
            StartActivityForResult(intent, 2);
        } 

        /// <summary>
        /// Перемещение из отложенного в прочитанное
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartCopy(object sender, System.EventArgs e)
        {
            var mas = sender as int[];

            var pend = _userControler.GetPendingBooks()[mas[0]];

            var readBook = new ReadBook(pend.Name, pend.Autor, pend.Photo, pend.CountPage, pend.Discript, mas[1], pend.Categori,DateTime.Now.ToShortDateString());

            _userControler.ReadingBook(readBook, mas[0]);

            fragment = FragmentLater.NewInstance();
            LoadFragment(fragment);
        }

        /// <summary>
        /// Удаление отложенной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartLaterDelete(object sender, System.EventArgs e)
        {
            new Android.App.AlertDialog.Builder(this)
                .SetTitle("Удаление")
                .SetIcon(Resource.Drawable.help)
                .SetMessage("Удалить выбранную книгу?")
                .SetPositiveButton("Да", delegate
                {
                    _userControler.Delete(int.Parse(sender.ToString()), UserControler.TypeBook.PendingBook);
                    fragment = FragmentLater.NewInstance();
                    LoadFragment(fragment);
                })
                .SetNegativeButton("Нет", delegate { }).Show();
        }

        /// <summary>
        /// Обновление отложенной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartLaterUpdate(object sender, System.EventArgs e)
        {
            Intent edt = new Intent(this, typeof(ActivityAdding));
            edt.PutExtra("status", "edit_later");
            edt.PutExtra("id", int.Parse(sender.ToString()));
            StartActivityForResult(edt, 1);
        }

        /// <summary>
        /// Удаление прочитанной книги
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartReadDelete(object sender, System.EventArgs e)
        {
            new Android.App.AlertDialog.Builder(this)
                .SetIcon(Resource.Drawable.help)
                .SetTitle("Удаление")
                .SetMessage("Удалить выбранную книгу?")
                .SetPositiveButton("Да", delegate
                {
                    _userControler.Delete(int.Parse(sender.ToString()), UserControler.TypeBook.ReadBook);
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

            if (requestCode == 2)
            {
                if (resultCode == 0)
                {
                    this.Finish();
                }
                else
                {
                    fragment = FragmentRead.NewInstance();
                    LoadFragment(fragment);
                    ConnectEvent();
                }
            }
                

            if (requestCode == 0)
                fragment = FragmentRead.NewInstance();

            if (requestCode == 1)
                fragment = FragmentLater.NewInstance();

            LoadFragment(fragment);
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

