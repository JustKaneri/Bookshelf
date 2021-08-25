using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Adapter;
using Bookshelf.Controler;
using Bookshelf.Model;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.QuotesPage);
            // Create your application here

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.RecQuot);
            txtName = FindViewById<TextView>(Resource.Id.TxtNameQ);
            fb = FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddQuot);

            fb.Click += Fb_Click;

            int id = Intent.GetIntExtra("id", -1);
            int pos = Intent.GetIntExtra("pos", -1);

            txtName.Text = "Цитаты из \"" + Intent.GetStringExtra("name") + "\"";

            _quoteControler = new QuoteControler(id, pos);

            _quoteControler.BeginDelete += _quoteControler_BeginDelete;
            _quoteControler.BeginUpdate += _quoteControler_BeginUpdate;

            FillRecylerView();
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            EditText edtQut = new EditText(this);
            edtQut.Hint =  "Цитата";
            EditText edtAutor = new EditText(this);
            edtAutor.Hint = "Автор Цитаты";

            LinearLayout ln = new LinearLayout(this);
            ln.Orientation = Orientation.Vertical;

            ln.AddView(edtQut);
            ln.AddView(edtAutor);

            new Android.App.AlertDialog.Builder(this)
                .SetTitle("Добавление цитаты")
                .SetView(ln)
                .SetPositiveButton("Добавить", delegate 
                {
                    if (string.IsNullOrWhiteSpace(edtQut.Text))
                    {
                        Toast.MakeText(this, "Укажите цитату", ToastLength.Short).Show();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(edtAutor.Text))
                        edtAutor.Text = "Неизвестен";

                    Quotes quot = new Quotes();
                    quot.Autor = edtAutor.Text;
                    quot.Quot = edtQut.Text;
                    _quoteControler.AddQuot(quot);
                    FillRecylerView();
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();
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

            EditText edtQut = new EditText(this);
            edtQut.Text = tmp.Quot;
            edtQut.Hint = "Цитата";
            EditText edtAutor = new EditText(this);
            edtAutor.Text = tmp.Autor;
            edtAutor.Hint = "Автор Цитаты";

            LinearLayout ln = new LinearLayout(this);
            ln.Orientation = Orientation.Vertical;

            ln.AddView(edtQut);
            ln.AddView(edtAutor);

            new Android.App.AlertDialog.Builder(this)
                .SetTitle("Редактирование цитаты")
                .SetView(ln)
                .SetPositiveButton("Сохранить", delegate
                {
                    if(string.IsNullOrWhiteSpace(edtAutor.Text) || string.IsNullOrWhiteSpace(edtQut.Text))
                    {
                        Toast.MakeText(this, "Заполните все поля", ToastLength.Short).Show();
                        return;
                    }

                    Quotes quot = new Quotes();
                    quot.Autor = edtAutor.Text;
                    quot.Quot = edtQut.Text;
                    _quoteControler.EditQuot(quot,pos);
                    FillRecylerView();
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();
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