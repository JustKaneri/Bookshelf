using System;
using Android.Graphics;
using Android.Media;

namespace Bookshelf.Model
{
    [Serializable]
    public class Book
    {
        internal string Name { get; set; }
        internal string Autor { get; set; }
        internal Bitmap Photo { get; set; }
        internal int CountPage { get; set; }
        internal string Discript { get; set;}
        internal int Categori { get; set; }
    }
}