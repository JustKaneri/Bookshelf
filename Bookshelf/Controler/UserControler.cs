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

        public static string[] categories = {"Афоризмы,фольклор и мифы","Астрология","Детективы","Домоводство","Исторические романы","На иностранном языке",
            "Комиксы и манга","Любовные романы","Поэзия","Приключенческая литература","Проза","Триллер","Фантастика или фэнтези",
            "Юмор и сатира"};

        private readonly string filePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "/data.dat";

        public event EventHandler StartReadUpdate;
        public event EventHandler StartReadDelete;

        public event EventHandler StartLaterUpdate;
        public event EventHandler StartLaterDelete;
        public event EventHandler StartCopy;

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

        /// <summary>
        /// Обработчик события нажатия на кнопку редактирования
        /// </summary>
        /// <param name="i">позиция</param>
        /// <param name="type">
        /// True - прочитанная книга
        /// False - отложенная книга
        /// </param>
        public void BegingUpdate(int i,bool type)
        {
            if (type)
                StartReadUpdate?.Invoke(i, null);
            else
                StartLaterUpdate?.Invoke(i, null);
        }

        /// <summary>
        /// Обработчик событя нажатия на кнопку удаления
        /// </summary>
        /// <param name="i">Позиция</param>
        /// <param name="type">
        /// True - прочитанная книга
        /// False - отложенная книга
        /// </param>
        public void BeginDelete(int i,bool type)
        {
            if (type)
                StartReadDelete?.Invoke(i, null);
            else
                StartLaterDelete?.Invoke(i, null);
        }

        public void StartMoved(int i)
        {
            StartCopy?.Invoke(i,null);
        }
      
        public void Update(Book book,int id,bool type)
        {
            if(type)
            {
                _shelf.readBooksArray[id] = book as ReadBook;
            }
            else
            {
                _shelf.pendingBooksArray[id] = book as PendingBook;
            }
        }

        public void Delete(int id,bool type)
        {
            if (type)
                _shelf.readBooksArray.RemoveAt(id);
            else
                _shelf.pendingBooksArray.RemoveAt(id);
        }

        public void ReadingBook(ReadBook book,int id)
        {
            Delete(id, false);

            AddBook(book, true);
        }
        

    }
}