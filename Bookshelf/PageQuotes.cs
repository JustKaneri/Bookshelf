using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.Provider;
using Android.Support.V7.Widget;
using Android.Widget;
using Bookshelf.Adapter;
using Bookshelf.Controler;
using Bookshelf.Model;
using Android.Views;
using static Android.Provider.MediaStore;

namespace Bookshelf
{
    [Activity(Label = "PageQuotes")]
    public class PageQuotes : Activity
    {
        public static QuoteControler _quoteControler;
        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private TextView txtName;
        private FloatingActionButton fb;

        private int IdBook;
        private string Url;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuotesPage);
            RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
            // Create your application here

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.RecQuot);
            txtName = FindViewById<TextView>(Resource.Id.TxtNameQ);
            fb = FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddQuot);

            fb.Click += Fb_Click;

            int id = Intent.GetIntExtra("id", -1);
            int pos = Intent.GetIntExtra("pos", -1);

            IdBook = pos;

            txtName.Text = "Цитаты из книги \"" + Intent.GetStringExtra("name") + "\"";

            _quoteControler = new QuoteControler(id, pos);

            _quoteControler.BeginDelete += _quoteControler_BeginDelete;
            _quoteControler.BeginUpdate += _quoteControler_BeginUpdate;
            _quoteControler.BeginRepost += _quoteControler_BeginRepost;

            FillRecylerView();
        }

        private void _quoteControler_BeginRepost(object sender, EventArgs e)
        {
            var Book = MainActivity._userControler.GetBooks()[IdBook];
            var quote = _quoteControler.GetQuoteList()[int.Parse((sender).ToString())];


            string TxtMessage = $"«{quote.Quot}»\n© {quote.Autor}.\n Цатата из книги {Book.Name}.\nЧитательский дневник Bookshelf.";

            try
            {
                String url = Images.Media.InsertImage(this.ContentResolver, Book.Photo, "title", null);
                Url = url;

                Intent intentRep = new Intent();
                intentRep.SetAction(Intent.ActionSend);
                intentRep.SetFlags(ActivityFlags.NewTask);
                intentRep.PutExtra(Intent.ExtraStream, Android.Net.Uri.Parse(url));
                intentRep.PutExtra(Intent.ExtraText, TxtMessage);
                intentRep.SetType("image/png");
                StartActivity(Intent.CreateChooser(intentRep, "Share with Friends"));
            }
            catch
            {
                Toast.MakeText(this, "Не удалось поделиться записью", ToastLength.Short).Show();
            }
            
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            View view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.WindowQuotes, null, false);

            EditText edtQut = view.FindViewById<EditText>(Resource.Id.EdtTextMin);
            edtQut.Hint = "Цитата";

            EditText edtAutor = view.FindViewById<EditText>(Resource.Id.EdtAutorMin);
            edtAutor.Hint = "Автор Цитаты";

            TextView txtV = view.FindViewById<TextView>(Resource.Id.TxtMinQuot);
            txtV.Text = "Добавление цитаты";

            Button btnSave = view.FindViewById<Button>(Resource.Id.BtnOkQuot);
            Button btnCancel = view.FindViewById<Button>(Resource.Id.BtnCancelQuot);


            var Window = new AlertDialog.Builder(this).SetView(view).Show();

            btnSave.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(edtQut.Text))
                {
                    Toast.MakeText(this, "Укажите цитату", ToastLength.Short).Show();
                    return;
                }

                if (string.IsNullOrWhiteSpace(edtAutor.Text))
                    edtAutor.Text = "Неизвестен";

                Quotes quot = new Quotes(edtQut.Text.Trim(),edtAutor.Text.Trim());
                _quoteControler.AddQuot(quot);
                FillRecylerView();

                Window.Cancel();
            };

            btnCancel.Click += delegate
            {
                Window.Cancel();
            };
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
            int pos = int.Parse(sender.ToString());
            var tmp = _quoteControler.GetQuoteList()[pos];

            View view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.WindowQuotes, null, false);

            EditText edtQut = view.FindViewById<EditText>(Resource.Id.EdtTextMin);
            edtQut.Text = tmp.Quot;
            edtQut.Hint = "Цитата";

            EditText edtAutor = view.FindViewById<EditText>(Resource.Id.EdtAutorMin);
            edtAutor.Text = tmp.Autor;
            edtAutor.Hint = "Автор Цитаты";

            TextView txtV = view.FindViewById<TextView>(Resource.Id.TxtMinQuot);
            txtV.Text = "Редактирование цитаты";

            Button btnSave = view.FindViewById<Button>(Resource.Id.BtnOkQuot);
            Button btnCancel = view.FindViewById<Button>(Resource.Id.BtnCancelQuot);


            var Window = new AlertDialog.Builder(this).SetView(view).Show();

            btnSave.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(edtQut.Text))
                {
                    Toast.MakeText(this, "Введите цитату", ToastLength.Short).Show();
                    return;
                }

                if(string.IsNullOrWhiteSpace(edtAutor.Text))
                {
                    edtAutor.Text = "Неизвестен";
                }

                Quotes quot = new Quotes(edtQut.Text.Trim(), edtAutor.Text.Trim());
                _quoteControler.EditQuot(quot, pos);
                FillRecylerView();

                Window.Cancel();
            };

            btnCancel.Click += delegate
            {
                Window.Cancel();
            };
        }

        private void _quoteControler_BeginDelete(object sender, EventArgs e)
        {
            new Android.App.AlertDialog.Builder(this)
                .SetTitle("Удаление")
                .SetMessage("Удалить выбранную цитату ?")
                .SetIcon(Resource.Drawable.help)
                .SetPositiveButton("Удалить", delegate
                {
                    _quoteControler.DeleteQuot(int.Parse(sender.ToString()));
                    FillRecylerView();

                    Toast.MakeText(this, "Цитата удалена", ToastLength.Short).Show();
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();
        }

    }
}