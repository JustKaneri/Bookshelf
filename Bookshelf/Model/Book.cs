using System;
using Android.Graphics;
using Android.Media;

namespace Bookshelf.Model
{
    [Serializable]
    public class Book
    {
        internal string Name { get; set; }
        protected string Autor { get; set; }
        internal Bitmap Photo { get; set; }
        protected int CountPage { get; set; }
        protected string Discript { get; set;}

    }
}