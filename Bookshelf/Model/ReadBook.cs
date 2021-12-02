using Android.Graphics;
using System;
using System.Collections.Generic;
using Bookshelf.Model;

namespace Bookshelf.Model
{
    public class ReadBook:Book
    {
        public int Mark { get; set; }
        public bool Favorite { get; set; } = false;
        public string DateReading { get; set; }

        public List<Quotes> list  = null;

        public ReadBook(string name,string autor,Bitmap photo,int cntPage,string discript,int mark,int categori,string date)
        {

            if (name.Contains("\""))
                name = name.Replace("\"", "");

            if (name.Contains("'"))
                name = name.Replace("'", "`");

            if (autor.Contains("\""))
                autor = autor.Replace("\"", "");

            if (autor.Contains("'"))
                autor = autor.Replace("'", "`");

            if (discript.Contains("\""))
                discript = discript.Replace("\"", "");

            if (discript.Contains("'"))
                discript = discript.Replace("'", "`");

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