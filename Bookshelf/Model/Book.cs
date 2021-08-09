using System;
using Android.Media;

namespace Bookshelf.Model
{
    [Serializable]
    public class Book
    {
        protected string Name { get; set; }
        protected string Autor { get; set; }
        protected Image Photo { get; set; }
        protected int CountPage { get; set; }
        protected string Discript { get; set;}

    }
}