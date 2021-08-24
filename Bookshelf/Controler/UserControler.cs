using System;
using System.Collections.Generic;
using Bookshelf.Model;
using System.IO;

namespace Bookshelf.Controler
{
    public class UserControler
    {
        public Shelf _shelf;

        public static string[] categories = {"Афоризмы,фольклор и мифы","Астрология","Детективы","Домоводство","Исторические романы","На иностранном языке",
            "Комиксы и манга","Любовные романы","Поэзия","Приключенческая литература","Проза","Триллер","Фантастика или фэнтези",
            "Юмор и сатира"};

        private readonly string filePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bookshelf.db3");


        public event EventHandler StartReadUpdate;
        public event EventHandler StartReadDelete;
        public event EventHandler StartOpenQuotes;

        public event EventHandler StartLaterUpdate;
        public event EventHandler StartLaterDelete;
        public event EventHandler StartCopy;

        public UserControler()
        {
            if(File.Exists(filePath))
            {
                var res = DBControler.GetTables();
                _shelf = new Shelf(res.Item1, res.Item2);
            }
            else
            {
                DBControler.CreatDB();
                _shelf = new Shelf(new List<ReadBook>(), new List<PendingBook>());
            }
            
        }

        public void AddBook(Book book, bool type)
        {
            if (type)
            {
                 book.ID = DBControler.AddBook(book, type);
                _shelf.readBooksArray.Add(book as ReadBook);
                
            }
            else
            {
                 book.ID = DBControler.AddBook(book, type);
                _shelf.pendingBooksArray.Add(book as PendingBook);
            }
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

        public void StartMoved(int i,int mark)
        {
            StartCopy?.Invoke(new int[] {i,mark},null);
        }

        public void BeginOpenQuotes(int pos)
        {
            StartOpenQuotes?.Invoke(pos, null);
        }
      
        public void Update(Book book,int id,bool type)
        {
            if(type)
            {
                DBControler.UpdateBook(book, type);
                _shelf.readBooksArray[id] = book as ReadBook;
            }
            else
            {
                DBControler.UpdateBook(book, type);
                _shelf.pendingBooksArray[id] = book as PendingBook;
            }
        }

        public void Delete(int id,bool type)
        {
            if (type)
            {
                DBControler.DeleteBook(_shelf.readBooksArray[id].ID, true);
                _shelf.readBooksArray.RemoveAt(id);
            }          
            else
            {
                DBControler.DeleteBook(_shelf.pendingBooksArray[id].ID, false);
                _shelf.pendingBooksArray.RemoveAt(id);
            }
               
        }

        public void ReadingBook(ReadBook book,int id)
        {
            Delete(id, false);
            AddBook(book, true);
        }
        
    }
}