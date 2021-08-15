﻿using System;
using System.IO;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bookshelf.Model;

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
        private int id;

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
            id = Intent.GetIntExtra("id", -1);


            if (status == "edit_read")
            {
                readBook = MainActivity._userControler.GetBooks()[id];

                edtName.Text = readBook.Name;
                edtAutor.Text = readBook.Autor;
                imvBook.SetImageBitmap(readBook.Photo);
                bmp = readBook.Photo;
                edtMark.Text = readBook.Mark.ToString();
                edtStr.Text = readBook.CountPage.ToString();
                edtDiscript.Text = readBook.Discript;
            }

            if (status == "add_later")
                edtMark.Visibility = ViewStates.Invisible;

            if (status == "edit_later")
            {
                edtMark.Visibility = ViewStates.Invisible;
                pendingBook = MainActivity._userControler.GetPendingBooks()[id];
                FillLater();
            }

            if (status == "read_book")
            {
                pendingBook = MainActivity._userControler.GetPendingBooks()[id];
                FillLater();
            }
                

        }

        private void FillLater()
        {
            edtName.Text = pendingBook.Name;
            edtAutor.Text = pendingBook.Autor;
            imvBook.SetImageBitmap(pendingBook.Photo);
            edtStr.Text = pendingBook.CountPage.ToString();
            bmp = pendingBook.Photo;
            edtDiscript.Text = pendingBook.Discript;
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

        private void UpdateReadBook()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);

            readBook = new ReadBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text, int.Parse(edtMark.Text));

            MainActivity._userControler.Update(readBook, id, true);

            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(0, intent);
            Finish();
        }

        private void AddLaterBook()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);

            pendingBook = new PendingBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text);

            MainActivity._userControler.AddBook(pendingBook, false);

            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(0, intent);
            Finish();
        }

        private void UpdateLaterBook()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);

            pendingBook = new PendingBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text);

            MainActivity._userControler.Update(pendingBook, id, false);

            Intent intent = new Intent(this, typeof(MainActivity));
            SetResult(Result.Ok, intent);
            Finish();
        }

        private void ReadingBook()
        {
            if (bmp == null)
                bmp = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.NotBook);

            ReadBook read = new ReadBook(edtName.Text, edtAutor.Text, bmp, int.Parse(edtStr.Text), edtDiscript.Text, int.Parse(edtMark.Text));

            MainActivity._userControler.ReadingBook(read,id);

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
                case "read_book":
                    ReadingBook();
                    break;
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