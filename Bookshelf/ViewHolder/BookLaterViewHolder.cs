﻿using Android.App;
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
        public ImageButton BtnEdit { get; private set; }
        public ImageButton BtnDele { get; private set; }
        public ImageButton BtnMove { get; private set; }

        public TextView TxtAutor { get; private set; }
        public TextView TxtCategori { get; private set; }

        public BookLaterViewHolder(View itemView) : base(itemView)
        {
            // Locate and cache view references:
            Image = itemView.FindViewById<ImageView>(Resource.Id.ImgLater);
            Caption = itemView.FindViewById<TextView>(Resource.Id.TxtLater);
            BtnEdit = itemView.FindViewById<ImageButton>(Resource.Id.BtnEditLater);
            BtnDele = itemView.FindViewById<ImageButton>(Resource.Id.BtnDelLater);
            BtnMove = itemView.FindViewById<ImageButton>(Resource.Id.BtnMovLater);

            TxtAutor = itemView.FindViewById<TextView>(Resource.Id.TvAutor);
            TxtCategori = itemView.FindViewById<TextView>(Resource.Id.TvCategori);

            BtnEdit.Click += delegate
            {
                MainActivity._userControler.BegingUpdate(int.Parse(BtnEdit.Tag.ToString()),UserControler.TypeBook.PendingBook);
            };

            BtnDele.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Удалить книгу", ToastLength.Short).Show();
            };

            BtnDele.Click += delegate
            {
                MainActivity._userControler.BeginDelete(int.Parse(BtnDele.Tag.ToString()), UserControler.TypeBook.PendingBook);
            };

            BtnEdit.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Редактировать книгу", ToastLength.Short).Show();
            };

            BtnMove.Click += delegate
            {
                EditText editText = new EditText(itemView.Context);
                editText.Hint = "Введите оценку";
                editText.TextAlignment = TextAlignment.Center;
                editText.TextChanged += EditText_TextChanged;


                new Android.App.AlertDialog.Builder(itemView.Context)
                .SetTitle("Перемещение").SetMessage("Переместить данную книгу в раздел прочитанное?\nДля этого укажите оценку данной книги.")
                .SetView(editText)
                .SetPositiveButton("Переместить", delegate
                {
                    if (editText.Text == "")
                        Toast.MakeText(itemView.Context, "Вы не указали оценку", ToastLength.Long).Show();
                    else
                        MainActivity._userControler.StartMoved(int.Parse(BtnMove.Tag.ToString()),int.Parse(editText.Text));
                })
                .SetNegativeButton("Отмена", delegate { })
                .Show();

              
            };

            BtnMove.LongClick += delegate
            {
                Toast.MakeText(Application.Context, "Переместить книгу в прочитанное", ToastLength.Short).Show();
            };

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