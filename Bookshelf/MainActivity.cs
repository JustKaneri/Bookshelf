using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;
using Bookshelf.Model;
using System;
using static Android.Provider.MediaStore;

namespace Bookshelf
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme",MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private Android.Support.V4.App.Fragment fragment = null;

        public static UserControler _userControler { get; set; }

        public static ApplicationController _appController { get; set; }

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

            _userControler.StartRepostBook += _userControler_StartRepostBook;
        }

        //private void CheckAppPermissions()
        //{
        //    if ((int)Build.VERSION.SdkInt < 23)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
        //            && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted)
        //        {
        //            var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage };
        //            RequestPermissions(permissions, 1);
        //        }
        //    }
        //}

        /// <summary>
        /// Поделиться книгой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartRepostBook(object sender, EventArgs e)
        {
            var Book = MainActivity._userControler.GetBooks()[int.Parse((sender).ToString())];

            try
            {
                String url = Images.Media.InsertImage(this.ContentResolver, Book.Photo, "title", null);

                Intent intentRep = new Intent();
                intentRep.SetAction(Intent.ActionSend);
                intentRep.SetFlags(ActivityFlags.NewTask);
                intentRep.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse(url));
                intentRep.PutExtra(Intent.ExtraText, "Хочу поделиться книгой " + Book.Name + " из читательского дневника Bookshelf.");
                intentRep.SetType("image/png");
                StartActivity(Intent.CreateChooser(intentRep, "Share with Friends"));
            }
            catch 
            {
                Toast.MakeText(this, "Не удалось поделиться записью",ToastLength.Short).Show();
            }          
        }

        /// <summary>
        /// Открытие страницы с цитатами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _userControler_StartOpenQuotes(object sender, EventArgs e)
        {
            Intent quot = new Intent(this, typeof(PageQuotes));
            quot.PutExtra("id", _userControler.GetBooks()[int.Parse(sender.ToString())].ID);
            quot.PutExtra("pos", int.Parse(sender.ToString()));
            quot.PutExtra("name", _userControler.GetBooks()[int.Parse(sender.ToString())].Name);
            StartActivity(quot);
        }

        /// <summary>
        /// Открытие окна с превью
        /// </summary>
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
        private void _userControler_StartCopy(object sender, EventArgs e)
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
        private void _userControler_StartLaterDelete(object sender, EventArgs e)
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
        private void _userControler_StartLaterUpdate(object sender, EventArgs e)
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
        private void _userControler_StartReadDelete(object sender, EventArgs e)
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
        private void _userControler_StartReadUpdate(object sender, EventArgs e)
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

