using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;

namespace Bookshelf
{
    public class FragmentRead : Android.Support.V4.App.Fragment
    {
        public static FragmentRead NewInstance()
        {
            var bundle = new Bundle();

            return new FragmentRead { Arguments = bundle };
        }

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private View v;
        private FloatingActionButton fb;
        private Activity activ;
        private ImageButton BtnSort;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            v = inflater.Inflate(Resource.Layout.ReadPage, container, false);
            fb = v.FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddRead);
            fb.Click += Fb_Click;

            BtnSort = v.FindViewById<ImageButton>(Resource.Id.ImbSortR);
            BtnSort.Click += BtnSort_Click;

            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.RecRead);
            FillRecylerView();

            activ = Activity;

            return v;
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {
            RadioGroup rg = new RadioGroup(Application.Context);

            RadioButton rbName = new RadioButton(Application.Context);
            rbName.Text = "По названию";

            RadioButton rbAutor = new RadioButton(Application.Context);
            rbAutor.Text = "По автору";

            RadioButton rbDate = new RadioButton(Application.Context);
            rbDate.Text = "По дате";

            RadioButton rbMark = new RadioButton(Application.Context);
            rbMark.Text = "По оценке";

            RadioButton rbFavorite = new RadioButton(Application.Context);
            rbFavorite.Text = "Сначало избранные";

            rg.AddView(rbName);
            rg.AddView(rbAutor);
            rg.AddView(rbDate);
            rg.AddView(rbMark);
            rg.AddView(rbFavorite);

            switch (MainActivity._userControler.SortRead)
            {
                case 0:
                    rbName.Checked = true;
                    break;
                case 1:
                    rbAutor.Checked = true;
                    break;
                case 2:
                    rbDate.Checked = true;
                    break;
                case 3:
                    rbMark.Checked = true;
                    break;
                case 4:
                    rbFavorite.Checked = true;
                    break;
            }


            new Android.App.AlertDialog.Builder(v.Context)
                .SetTitle("Сортировка")
                .SetIcon(Resource.Drawable.Sort)
                .SetView(rg)
                .SetPositiveButton("Сортировать",delegate 
                {
                    int index = -1;

                    if (rbName.Checked)
                        index = 0;
                    if (rbAutor.Checked)
                        index = 1;
                    if (rbDate.Checked)
                        index = 2;
                    if (rbMark.Checked)
                        index = 3;
                    if (rbFavorite.Checked)
                        index = 4;

                    if (index != -1)
                        MainActivity._userControler.SortBook((UserControler.TypeSort)index, UserControler.TypeBook.ReadBook);

                    MainActivity._userControler.SortRead = index;
                    FillRecylerView();
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();
        }

        private void FillRecylerView()
        {
            BookAdapter adapter = new BookAdapter(MainActivity._userControler.GetBooks());
            
            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            fb.Enabled = false;
            Intent add = new Intent(Activity, typeof(ActivityAdding));
            add.PutExtra("status", "add_read");
            StartActivityForResult(add, 0);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if(requestCode == 0)
            {
                FillRecylerView();
                fb.Enabled = true;
            }
        }
    }
}