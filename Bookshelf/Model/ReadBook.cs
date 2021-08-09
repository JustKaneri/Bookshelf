using Android.Media;
using System;

namespace Bookshelf.Model
{
    [Serializable]
    public class ReadBook:Book
    {
        private int Mark;

        public ReadBook(string name,string autor,Image photo,int cntPage,string discript,int mark)
        {
            Name = name;
            Autor = autor;
            Photo = photo;
            CountPage = cntPage;
            Discript = discript;
            Mark = mark;
        }
    }
}