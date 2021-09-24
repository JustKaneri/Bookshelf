using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bookshelf.Controler;
using Bookshelf.Model;

namespace Bookshelf
{
    [Activity(Label = "ActivityAdding")]
    public class ActivityAdding : Activity
    {
        private EditText edtName;
        private EditText edtAutor;
        private ImageView imvBook;
        private Spinner spType;
        private EditText edtDate;
        private ImageButton imvdate;
        private EditText edtMark;
        private EditText edtStr;
        private EditText edtDiscript;
        private Button btnAdd;

        private LinearLayout layout;

        private Bitmap bmp;

        private string status;
        private PendingBook pendingBook;
        private ReadBook readBook;
        private int id;

        private AlertDialog Alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddingPage);
            // Create your application here

            edtName = FindViewById<EditText>(Resource.Id.EdtName);
            edtAutor = FindViewById<EditText>(Resource.Id.EdtAutor);
            imvBook = FindViewById<ImageView>(Resource.Id.ImvBookAdd);
            spType = FindViewById<Spinner>(Resource.Id.SpnCatecogr);
            edtDate = FindViewById<EditText>(Resource.Id.EdtDate);
            imvdate = FindViewById<ImageButton>(Resource.Id.ImvDate);
            edtMark = FindViewById<EditText>(Resource.Id.EdtMark);
            edtStr = FindViewById<EditText>(Resource.Id.EdtStr);
            edtDiscript = FindViewById<EditText>(Resource.Id.EdtDiscript);
            btnAdd = FindViewById<Button>(Resource.Id.BtnAdd);
            layout = FindViewById<LinearLayout>(Resource.Id.linearLayout2);

            imvdate.Click += Imvdate_Click;
            imvBook.Click += ImvBook_Click;
            edtMark.TextChanged += EdtMark_TextChanged;
            btnAdd.Click += BtnAdd_Click;

            edtDate.Text = DateTime.Now.ToShortDateString();
            imvBook.SetImageResource(Resource.Drawable.nophoto);
            spType.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, UserControler.categories);

            status = Intent.GetStringExtra("status");
            id = Intent.GetIntExtra("id", -1);

            if (status == "edit_read")
            {
                readBook = MainActivity._userControler.GetBooks()[id];

                edtName.Text = readBook.Name;
                edtAutor.Text = readBook.Autor;
                imvBook.SetImageBitmap(readBook.Photo);
                bmp = readBook.Photo;
                edtDate.Text = readBook.DateReading;
                edtMark.Text = readBook.Mark.ToString();
                edtStr.Text = readBook.CountPage.ToString();
                edtDiscript.Text = readBook.Discript;
                spType.SetSelection(readBook.Categori);
            }

            if (status == "add_later")
            {
                edtMark.Visibility = ViewStates.Gone;
                layout.Visibility = ViewStates.Gone;
            }
            
            if (status == "edit_later")
            {
                edtMark.Visibility = ViewStates.Gone;
                layout.Visibility = ViewStates.Gone;
                pendingBook = MainActivity._userControler.GetPendingBooks()[id];
                FillLater();
            }

            Toast.MakeText(this, "Нажмите на изображение что бы добавить фотографию.", ToastLength.Short).Show();
        }

        private void Imvdate_Click(object sender, EventArgs e)
        {
            DatePicker dt = new DatePicker(this);

            if (edtDate.Text != "")
                dt.DateTime = DateTime.Parse(edtDate.Text);

            new AlertDialog.Builder(this)
                .SetTitle("Дата прочтения")
                .SetView(dt)
                .SetPositiveButton("Ок", delegate 
                {
                    edtDate.Text = dt.DateTime.ToShortDateString();
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();
        }

        private void FillLater()
        {
            edtName.Text = pendingBook.Name;
            edtAutor.Text = pendingBook.Autor;
            imvBook.SetImageBitmap(pendingBook.Photo);
            edtStr.Text = pendingBook.CountPage.ToString();
            bmp = pendingBook.Photo;
            edtDiscript.Text = pendingBook.Discript;
            spType.SetSelection(pendingBook.Categori);
        }

        private void EdtMark_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            int res = 0;
            if (!int.TryParse(edtMark.Text, out res))
                return;

            int mark = int.Parse(edtMark.Text);

            if (mark == 0 || mark > 5)
                edtMark.Text = "";
        }

        private void FinishAddingOrEdit()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(0, intent);
            Finish();
        }

        private void SetBitmap()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);
        }

        private void AddReadBook()
        {
            SetBitmap();

            ReadBook read = new ReadBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text, int.Parse(edtMark.Text),spType.SelectedItemPosition,edtDate.Text);

            MainActivity._userControler.AddBook(read, UserControler.TypeBook.ReadBook);

            FinishAddingOrEdit();
        }

        private void UpdateReadBook()
        {
            SetBitmap();

            int idB = readBook.ID;
            bool favorite = readBook.Favorite;
            readBook = new ReadBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text, int.Parse(edtMark.Text),spType.SelectedItemPosition,edtDate.Text);
            readBook.ID = idB;
            readBook.Favorite = favorite;

            MainActivity._userControler.Update(readBook, id, UserControler.TypeBook.ReadBook);

            FinishAddingOrEdit();
        }

        private void AddLaterBook()
        {
            SetBitmap();

            pendingBook = new PendingBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text,spType.SelectedItemPosition);

            MainActivity._userControler.AddBook(pendingBook, UserControler.TypeBook.PendingBook);

            FinishAddingOrEdit();
        }

        private void UpdateLaterBook()
        {
            SetBitmap();

            int idBook = pendingBook.ID;
            pendingBook = new PendingBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text,spType.SelectedItemPosition);
            pendingBook.ID = idBook;

            MainActivity._userControler.Update(pendingBook, id, UserControler.TypeBook.PendingBook);

            FinishAddingOrEdit();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            btnAdd.Enabled = false;

            if (string.IsNullOrWhiteSpace(edtName.Text))
            {
                Toast.MakeText(this, "Укажите название", ToastLength.Short).Show();
                btnAdd.Enabled = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(edtAutor.Text))
            {
                edtAutor.Text = "Неизвестен";
            }

            if (status.Contains("read") && string.IsNullOrWhiteSpace(edtMark.Text))
            {
                Toast.MakeText(this, "Укажите оценку", ToastLength.Short).Show();
                btnAdd.Enabled = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(edtStr.Text))
            {
                Toast.MakeText(this, "Укажите кол-во страниц", ToastLength.Short).Show();
                btnAdd.Enabled = true;
                return;
            }

            switch (status)
            {
                case "add_read":
                    AddReadBook();
                    break;
                case "edit_read":
                    UpdateReadBook();
                    break;
                case "add_later":
                    AddLaterBook();
                    break;
                case "edit_later":
                    UpdateLaterBook();
                    break;
            }
                
        }

        private void ImvBook_Click(object sender, EventArgs e)
        {
            LinearLayout linearLayout = new LinearLayout(this);
            linearLayout.Orientation = Orientation.Vertical;

            Button btnCamera = new Button(this);
            btnCamera.Text = "Открыть камеру";
            btnCamera.SetBackgroundColor(Color.White);
            btnCamera.SetTextColor(Color.Black);
            


            Button btnGalery = new Button(this);
            btnGalery.Text = "Открыть галерую";
            btnGalery.SetBackgroundColor(Color.White);
            btnGalery.SetTextColor(Color.Black);
            


            Button btnClear = new Button(this);
            btnClear.Text = "Очистить";
            btnClear.SetBackgroundColor(Color.White);
            btnClear.SetTextColor(Color.Black);
            


            linearLayout.AddView(btnCamera);
            linearLayout.AddView(btnGalery);
            linearLayout.AddView(btnClear);

            Alert =  new AlertDialog.Builder(this).SetView(linearLayout).Show();

            btnCamera.Click += BtnCamera_Click;
            btnGalery.Click += BtnGalery_Click;
            btnClear.Click += BtnClear_Click;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            new AlertDialog.Builder(this)
               .SetTitle("Удаление")
               .SetMessage("Очистить изображение ?")
               .SetPositiveButton("Да", delegate
               {
                   imvBook.SetImageResource(Resource.Drawable.nophoto);
                   bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);
               })
               .SetNegativeButton("Нет", delegate { }).Show();

            Alert.Cancel();
        }

        private void BtnGalery_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 0);
            Alert.Cancel();
        }

        private void BtnCamera_Click(object sender, EventArgs e)
        {
            Intent ope = new Intent(MediaStore.ActionImageCapture);
            StartActivityForResult(ope, 1);
            Alert.Cancel();
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0 && data != null)
            {
                Android.Net.Uri uri = data.Data;
                Stream stream = ContentResolver.OpenInputStream(uri);
                bmp = BitmapFactory.DecodeStream(stream);

                if(bmp.Width > 3840 || bmp.Height > 2160)
                    bmp = Bitmap.CreateScaledBitmap(bmp, bmp.Width / 2, bmp.Height / 2 , false);

                imvBook.SetImageBitmap(bmp);
            }

            if (requestCode == 1 && data != null)
            {
                bmp = (Bitmap)data.Extras.Get("data");
                imvBook.SetImageBitmap(bmp);
            }
        }

        public override void OnBackPressed()
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(Result.Canceled, intent);
            Finish();
        }
    }
}