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

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            
        }


        private void ImvBook_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), 0);

            //Intent ope = new Intent(MediaStore.ExtraShowActionIcons);
            //StartActivityForResult(ope, 0);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            if (requestCode == 0 && data != null)
            {
                Android.Net.Uri uri = data.Data;
                Stream stream = ContentResolver.OpenInputStream(uri);
                imvBook.SetImageURI(uri);

                //var bmp = (Bitmap)data.Extras.Get("data");
                //imvBook.SetImageBitmap(bmp);

                //MemoryStream ms = new MemoryStream();
                //bmp.Compress(CompressFormat.Png, 100, ms);

                //ArrayImgByte = ms.ToArray();
            }
        }
    }
}