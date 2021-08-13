using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bookshelf.Model;
using Bookshelf.Controler;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bookshelf.Controler
{
    public class UserControler
    {
        public Shelf _shelf;

        private readonly string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data.dat";

        public UserControler()
        {
            if (File.Exists(filePath))
            {
                BinaryFormatter bf = new BinaryFormatter();

                using (FileStream fs = File.OpenRead(filePath))
                {
                    _shelf = bf.Deserialize(fs) as Shelf;
                }
            }
            else
            {
                _shelf = new Shelf(new List<ReadBook>(), new List<PendingBook>());
            }
        }


        public void AddBook(Book book, bool type)
        {
            if (type)
                _shelf.readBooksArray.Add(book as ReadBook);
            else
                _shelf.pendingBooksArray.Add(book as PendingBook);
        }

        public List<ReadBook> GetBooks()
        {
            return _shelf.readBooksArray;
        }
        
        public List<PendingBook> GetPendingBooks()
        {
            return _shelf.pendingBooksArray;
        }

      
    }
}