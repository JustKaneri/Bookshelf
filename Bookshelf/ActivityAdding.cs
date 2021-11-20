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
        private TextView TxtStatus;
        private LinearLayout layout;

        private Bitmap ImageBook { get; set; }
        private string StatusPage { get; set; }
        private PendingBook BookPending { get; set; }
        private ReadBook BookRead { get; set; }
        private int IdBook { get; set; }

        private AlertDialog Alert;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddPage);
            // Create your application here

            FindIdElements();

            imvdate.Click += Imvdate_Click;
            imvBook.Click += ImvBook_Click;
            edtMark.TextChanged += EdtMark_TextChanged;
            btnAdd.Click += BtnAdd_Click;

            edtDate.Text = DateTime.Now.ToShortDateString();
            imvBook.SetImageResource(Resource.Drawable.nophoto);
            spType.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, UserControler.categories);

            StatusPage = Intent.GetStringExtra("status");
            IdBook = Intent.GetIntExtra("id", -1);

            TxtStatus.Text = StatusPage.Contains("add") ? "Добавление" : "Редактирование";

            if (StatusPage == "edit_read")
            {
                BookRead = MainActivity._userControler.GetBooks()[IdBook];

                edtName.Text = BookRead.Name;
                edtAutor.Text = BookRead.Autor;
                imvBook.SetImageBitmap(BookRead.Photo);
                ImageBook = BookRead.Photo;
                edtDate.Text = BookRead.DateReading;
                edtMark.Text = BookRead.Mark.ToString();
                edtStr.Text = BookRead.CountPage.ToString();
                edtDiscript.Text = BookRead.Discript;
                spType.SetSelection(BookRead.Categori);
            }

            if (StatusPage == "add_later")
            {
                InvisibilityPartUI();
            }
            
            if (StatusPage == "edit_later")
            {
                InvisibilityPartUI();
                BookPending = MainActivity._userControler.GetPendingBooks()[IdBook];
                FillLater();
            }

            Toast.MakeText(this, "Нажмите на изображение что бы добавить фотографию.", ToastLength.Short).Show();
        }

        private void FindIdElements()
        {
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
            layout = FindViewById<LinearLayout>(Resource.Id.lLayout2);
            TxtStatus = FindViewById<TextView>(Resource.Id.TxtStatus);
        }

        private void InvisibilityPartUI()
        {
            edtMark.Visibility = ViewStates.Gone;
            layout.Visibility = ViewStates.Gone;
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
            edtName.Text = BookPending.Name;
            edtAutor.Text = BookPending.Autor;
            imvBook.SetImageBitmap(BookPending.Photo);
            edtStr.Text = BookPending.CountPage.ToString();
            ImageBook = BookPending.Photo;
            edtDiscript.Text = BookPending.Discript;
            spType.SetSelection(BookPending.Categori);
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
            if (ImageBook == null)
                ImageBook = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);
        }

        private void AddReadBook()
        {
            SetBitmap();

            string nameBook = edtName.Text;
            string autorBook = edtAutor.Text;
            int countPage = int.Parse(edtStr.Text);
            string discriptBook = edtDiscript.Text;
            int markBook = int.Parse(edtMark.Text);
            int categoriBook = spType.SelectedItemPosition;
            string dateBook = edtDate.Text;

            ReadBook read = new ReadBook(nameBook, autorBook, ImageBook, countPage, edtDiscript.Text, markBook , categoriBook ,dateBook);

            MainActivity._userControler.AddBook(read, UserControler.TypeBook.ReadBook);

            FinishAddingOrEdit();
        }

        private void UpdateReadBook()
        {
            SetBitmap();

            int idB = BookRead.ID;
            bool IsFavorite = BookRead.Favorite;

            string nameBook = edtName.Text;
            string autorBook = edtAutor.Text;
            int countPage = int.Parse(edtStr.Text);
            string discriptBook = edtDiscript.Text;
            int markBook = int.Parse(edtMark.Text);
            int categoriBook = spType.SelectedItemPosition;
            string dateBook = edtDate.Text;

            BookRead = new ReadBook(nameBook, autorBook, ImageBook, countPage, edtDiscript.Text, markBook, categoriBook, dateBook);
            BookRead.ID = idB;
            BookRead.Favorite = IsFavorite;

            MainActivity._userControler.Update(BookRead, IdBook, UserControler.TypeBook.ReadBook);

            FinishAddingOrEdit();
        }

        private void AddLaterBook()
        {
            SetBitmap();

            string nameBook = edtName.Text;
            string autorBook = edtAutor.Text;
            int countPage = int.Parse(edtStr.Text);
            string discriptBook = edtDiscript.Text;
            int categoriBook = spType.SelectedItemPosition;

            BookPending = new PendingBook(nameBook, autorBook, ImageBook, countPage,discriptBook,categoriBook);

            MainActivity._userControler.AddBook(BookPending, UserControler.TypeBook.PendingBook);

            FinishAddingOrEdit();
        }

        private void UpdateLaterBook()
        {
            SetBitmap();

            int idBook = BookPending.ID;
            string nameBook = edtName.Text;
            string autorBook = edtAutor.Text;
            int countPage = int.Parse(edtStr.Text);
            string discriptBook = edtDiscript.Text;
            int categoriBook = spType.SelectedItemPosition;

            BookPending = new PendingBook(nameBook, autorBook, ImageBook, countPage, discriptBook, categoriBook);
            BookPending.ID = idBook;

            MainActivity._userControler.Update(BookPending, IdBook, UserControler.TypeBook.PendingBook);

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

            if (StatusPage.Contains("read") && string.IsNullOrWhiteSpace(edtMark.Text))
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

            switch (StatusPage)
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

            Alert =  new AlertDialog.Builder(this).SetTitle("Изображение").SetView(linearLayout).Show();

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
                   ImageBook = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);
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
                ImageBook = BitmapFactory.DecodeStream(stream);

                if(ImageBook.Width > 3840 || ImageBook.Height > 2160)
                    ImageBook = Bitmap.CreateScaledBitmap(ImageBook, ImageBook.Width / 2, ImageBook.Height / 2 , false);

                imvBook.SetImageBitmap(ImageBook);
            }

            if (requestCode == 1 && data != null)
            {
                ImageBook = (Bitmap)data.Extras.Get("data");

                if (ImageBook.Width > 3840 || ImageBook.Height > 2160)
                    ImageBook = Bitmap.CreateScaledBitmap(ImageBook, ImageBook.Width / 2, ImageBook.Height / 2, false);

                imvBook.SetImageBitmap(ImageBook);
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