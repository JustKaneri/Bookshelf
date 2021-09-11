using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Adapter;
using Bookshelf.Controler;

namespace Bookshelf
{
    public class FragmentLater : Android.Support.V4.App.Fragment
    {
        public static FragmentLater NewInstance()
        {
            var bundle = new Bundle();

            return new FragmentLater { Arguments = bundle };
        }

        private RecyclerView mRecyclerView;
        private RecyclerView.LayoutManager mLayoutManager;
        private View v;
        private FloatingActionButton fb;
        private ImageButton BtnSort;
        private ImageButton BtnView;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            v = inflater.Inflate(Resource.Layout.LaterPage, container, false);

            fb = v.FindViewById<FloatingActionButton>(Resource.Id.fltBtnAddLater);
            fb.Click += Fb_Click;

            BtnSort = v.FindViewById<ImageButton>(Resource.Id.ImbSortL);
            BtnSort.Click += BtnSort_Click;

            BtnView = v.FindViewById<ImageButton>(Resource.Id.ImgBtnView);
            BtnView.Click += BtnView_Click;

            mRecyclerView = v.FindViewById<RecyclerView>(Resource.Id.RecLater);
            FillRecylerView();

            return v;
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            if (MainActivity._appController.GetTypeView(UserControler.TypeBook.PendingBook) == ApplicationController.TypeView.MinInfo)
            {
                MainActivity._appController.SetTypeView(ApplicationController.TypeView.MaxInfo,UserControler.TypeBook.PendingBook);
                BtnView.SetBackgroundResource(Resource.Drawable.ViewOne);
            }
            else
            {
                MainActivity._appController.SetTypeView(ApplicationController.TypeView.MinInfo,UserControler.TypeBook.PendingBook);
                BtnView.SetBackgroundResource(Resource.Drawable.ViewTwo);
            }

            FillRecylerView();
        }

        private void BtnSort_Click(object sender, EventArgs e)
        {

            RadioGroup rg = new RadioGroup(Application.Context);

            RadioButton rbName = new RadioButton(Application.Context);
            rbName.Text = "По названию";

            RadioButton rbAutor = new RadioButton(Application.Context);
            rbAutor.Text = "По автору";

            rg.AddView(rbName);
            rg.AddView(rbAutor);

            switch (MainActivity._userControler.SortPen)
            {
                case 0:
                    rbName.Checked = true;
                    break;
                case 1:
                    rbAutor.Checked = true;
                    break;
            }


            new Android.App.AlertDialog.Builder(v.Context)
                .SetTitle("Сортировка")
                .SetIcon(Resource.Drawable.Sort)
                .SetView(rg)
                .SetPositiveButton("Сортировать", delegate
                {
                    int index = -1;

                    if (rbName.Checked)
                        index = 0;
                    if (rbAutor.Checked)
                        index = 1;

                    if (index != -1)
                        MainActivity._userControler.SortBook((UserControler.TypeSort)index, UserControler.TypeBook.PendingBook);

                    MainActivity._userControler.SortPen = index;
                    FillRecylerView();
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();
        }

        private void Fb_Click(object sender, EventArgs e)
        {
            fb.Enabled = false;
            Intent add = new Intent(Activity, typeof(ActivityAdding));
            add.PutExtra("status", "add_later");
            StartActivityForResult(add, 0);
        }

        private void FillRecylerView()
        {
            BookLaterAdapter adapter = new BookLaterAdapter(MainActivity._userControler.GetPendingBooks());

            mRecyclerView.SetAdapter(adapter);

            mLayoutManager = new LinearLayoutManager(Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
        }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (resultCode == 0)
            {
                fb.Enabled = true;
                FillRecylerView();
            }
        }
    }
}