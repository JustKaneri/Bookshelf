using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;

namespace Bookshelf.Model
{
    class BookLaterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; private set; }
        public TextView Caption { get; private set; }
        public ImageButton BtnMove { get; private set; }

        public TextView TxtAutor { get; private set; }
        public TextView TxtCategori { get; private set; }

        public Button BtnPopMenu { get; set; }

        private View viewMain { get; set; }

        public ImageView ImageFon { get; private set; }
        public LinearLayout Layout { get; private set; }

        public BookLaterViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.ImgLater);
            Caption = itemView.FindViewById<TextView>(Resource.Id.TxtLater);
            BtnMove = itemView.FindViewById<ImageButton>(Resource.Id.BtnMovLater);
            BtnPopMenu = itemView.FindViewById<Button>(Resource.Id.BtnOpenPopMenu);
            TxtAutor = itemView.FindViewById<TextView>(Resource.Id.TvAutor);
            TxtCategori = itemView.FindViewById<TextView>(Resource.Id.TvCategori);
            ImageFon = itemView.FindViewById<ImageView>(Resource.Id.imageView2);
            Layout = itemView.FindViewById<LinearLayout>(Resource.Id.LnlMain);

            viewMain = itemView;

            BtnPopMenu.Click += (s, arg) =>
            {
                Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu(Application.Context, BtnPopMenu);
                menu.Inflate(Resource.Menu.PopupMenuPending);

                menu.MenuItemClick += (s1, arg1) =>
                {
                    switch (arg1.Item.ItemId)
                    {
                        case Resource.Id.menu_PreShow:
                            Preview(int.Parse(BtnMove.Tag.ToString()));
                            break;
                        case Resource.Id.menu_Edit:
                            MainActivity._userControler.BegingUpdate(int.Parse(BtnMove.Tag.ToString()), UserControler.TypeBook.PendingBook);
                            break;
                        case Resource.Id.menu_Del:
                            MainActivity._userControler.BeginDelete(int.Parse(BtnMove.Tag.ToString()), UserControler.TypeBook.PendingBook);
                            break;
                    }
                };

                menu.Show();
            };

            BtnMove.Click += BtnMove_Click;


            BtnMove.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Переместить книгу в прочитанное", ToastLength.Short).Show();
            };

        }

        private void BtnMove_Click(object sender, System.EventArgs e)
        {

            View view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.WindowMark, null, false);
            EditText editText = view.FindViewById<EditText>(Resource.Id.EdtMark);
            editText.Hint = "Введите оценку";
            editText.InputType = Android.Text.InputTypes.ClassNumber;
            editText.TextChanged += EditText_TextChanged;

            Button btnOk = view.FindViewById<Button>(Resource.Id.BtnOkMark);
            Button btnCns = view.FindViewById<Button>(Resource.Id.BtnCancelMark);

            var window = new AlertDialog.Builder(viewMain.Context).SetView(view).Show();

            btnOk.Click += delegate
            {
                if(string.IsNullOrEmpty(editText.Text))
                {
                    Toast.MakeText(viewMain.Context,"Вы не указали оценку",ToastLength.Short).Show();
                }
                else
                {
                    window.Cancel();
                    MainActivity._userControler.StartMoved(int.Parse(BtnMove.Tag.ToString()), int.Parse(editText.Text));
                }
            };
            btnCns.Click += delegate
            {
                window.Cancel();
            };

            view.Dispose();

        }

        private void Preview(int pos)
        {
            View view = LayoutInflater.From(Application.Context).Inflate(Resource.Layout.PreviewPendingPage, null, false);
            var book = MainActivity._userControler.GetPendingBooks()[pos];

            view.FindViewById<ImageView>(Resource.Id.ImvPhotoPrev).SetImageBitmap(book.Photo);
            view.FindViewById<TextView>(Resource.Id.TxtNamePrev).Text = book.Name;
            view.FindViewById<ImageView>(Resource.Id.ImvFon).SetImageBitmap(book.Photo);
            view.FindViewById<TextView>(Resource.Id.TxtAutorPrev).Text = book.Autor;
            view.FindViewById<TextView>(Resource.Id.TxtCategoriPrev).Text = UserControler.categories[book.Categori];
            view.FindViewById<TextView>(Resource.Id.TxtStrPrev).Text = book.CountPage.ToString();
            view.FindViewById<TextView>(Resource.Id.TxtDiscriptPrev).Text = book.Discript == "" ? "Описание отсутствует" : book.Discript;

            new AlertDialog.Builder(BtnPopMenu.Context).SetView(view).Show();
        }

        private void EditText_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            int res = 0;
            if (!int.TryParse(((EditText)sender).Text, out res))
                return;

            int mark = int.Parse(((EditText)sender).Text);

            if (mark == 0 || mark > 5)
                ((EditText)sender).Text = "";
        }
    }

}