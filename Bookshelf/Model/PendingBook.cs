using Android.Graphics;
using System;

namespace Bookshelf.Model
{
    public class PendingBook:Book
    {
        public PendingBook(string name, string autor, Bitmap photo, int cntPage, string discript,int categori)
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
            Categori = categori;
        }
    }
}