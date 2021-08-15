using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bookshelf.Model;
using System.Drawing;

namespace Bookshelf
{
    [Activity(Label = "ActivityAdding")]
    public class ActivityAdding : Activity
    {
        private EditText edtName;
        private EditText edtAutor;
        private ImageView imvBook;
        private EditText edtMark;
        private EditText edtStr;
        private EditText edtDiscript;
        private Button btnAdd;

        private Bitmap bmp;

        private string status;
        private PendingBook pendingBook;
        private ReadBook readBook;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddingPage);
            // Create your application here

            edtName = FindViewById<EditText>(Resource.Id.EdtName);
            edtAutor = FindViewById<EditText>(Resource.Id.EdtAutor);
            imvBook = FindViewById<ImageView>(Resource.Id.ImvBookAdd);
            edtMark = FindViewById<EditText>(Resource.Id.EdtMark);
            edtStr = FindViewById<EditText>(Resource.Id.EdtStr);
            edtDiscript = FindViewById<EditText>(Resource.Id.EdtDiscript);
            btnAdd = FindViewById<Button>(Resource.Id.BtnAdd);

            imvBook.Click += ImvBook_Click;
            edtMark.TextChanged += EdtMark_TextChanged;
            btnAdd.Click += BtnAdd_Click;


            status = Intent.GetStringExtra("status");
            int id = Intent.GetIntExtra("id", -1);


            if(status == "add_read")
            {
                
            }

            if(status == "edit_read")
            {
                readBook = MainActivity._userControler.GetBooks()[id];

                edtName.Text = readBook.Name;
                edtAutor.Text = readBook.Autor;
                imvBook.SetImageBitmap(readBook.Photo);
                edtMark.Text = readBook.Mark.ToString();
                edtStr.Text = readBook.CountPage.ToString();
                edtDiscript.Text = readBook.Discript;
            }

            if (status == "add_later")
                EditLater();

            if(status == "edit_later")
            {
                EditLater();
                pendingBook = MainActivity._userControler.GetPendingBooks()[id];
            }
        }

        private void EditLater()
        {
            edtMark.Visibility = ViewStates.Invisible;

            if(pendingBook != null)
            {
                edtName.Text = pendingBook.Name;
                edtAutor.Text = pendingBook.Autor;
                imvBook.SetImageBitmap(pendingBook.Photo);
                edtStr.Text = pendingBook.CountPage.ToString();
                edtDiscript.Text = pendingBook.Discript;

            }
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

        private void AddReadBook()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);

            ReadBook read = new ReadBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text, int.Parse(edtMark.Text));

            MainActivity._userControler.AddBook(read, true);

            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(0, intent);
            Finish();
        }

        private void AddLaterBook()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);

            pendingBook = new PendingBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text);

            MainActivity._userControler.AddBook(pendingBook,false);

            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(0, intent);
            Finish();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(edtName.Text))
            {
                Toast.MakeText(this, "Укажите название", ToastLength.Short).Show();
                return;
            }

            if (string.IsNullOrWhiteSpace(edtAutor.Text))
            {
                Toast.MakeText(this, "Укажите автора", ToastLength.Short).Show();
                return;
            }

            if (status.Contains("read") && string.IsNullOrWhiteSpace(edtMark.Text))
            {
                Toast.MakeText(this, "Укажите оценку", ToastLength.Short).Show();
                return;
            }

            if (string.IsNullOrWhiteSpace(edtStr.Text))
            {
                Toast.MakeText(this, "Укажите кол-во страниц", ToastLength.Short).Show();
                return;
            }


            if (status == "add_read")
            {
                AddReadBook();
            }

            if (status == "edit_read")
            {

            }

            if (status == "add_later")
            {
                AddLaterBook();
            }

            if (status == "edit_later")
            {

            }

        }


        private void ImvBook_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 0);

        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0 && data != null)
            {
                Android.Net.Uri uri = data.Data;
                Stream stream = ContentResolver.OpenInputStream(uri);
                imvBook.SetImageURI(uri);

                bmp = BitmapFactory.DecodeStream(stream);
                
            }
        }
    }
}