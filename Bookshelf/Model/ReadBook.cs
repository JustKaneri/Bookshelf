using Android.Graphics;
using System;

namespace Bookshelf.Model
{
    public class ReadBook:Book
    {
        public int Mark { get; set; }
        public bool Favorite { get; internal set; } = false;
        public string DateReading { get; set; }

        public ReadBook(string name,string autor,Bitmap photo,int cntPage,string discript,int mark,int categori,string date)
        {
            Name = name;
            Autor = autor;
            Photo = photo;
            CountPage = cntPage;
            Discript = discript;
            Mark = mark;
            Categori = categori;
            DateReading = date;
        }

        
    }
}