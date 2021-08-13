using Android.Graphics;
using System;

namespace Bookshelf.Model
{
    [Serializable]
    public class PendingBook:Book
    {
        public PendingBook(string name, string autor, Bitmap photo, int cntPage, string discript)
        {
            Name = name;
            Autor = autor;
            Photo = photo;
            CountPage = cntPage;
            Discript = discript;
        }
    }
}