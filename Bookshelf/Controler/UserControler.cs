using System;
using System.Collections.Generic;
using Bookshelf.Model;
using System.IO;

namespace Bookshelf.Controler
{
    public class UserControler
    {
        public enum TypeBook
        {
            ReadBook,
            PendingBook
        }

        public enum TypeSort
        {
            ByName = 0,
            ByAutor,
            ByDate,
            ByMark,
            ByFavorite
        }

        public Shelf _shelf;

        public static string[] categories = {"Афоризмы,фольклор и мифы","Астрология","Детективы",
                                            "Исторические романы","На иностранном языке","Психология",
                                            "Комиксы и манга","Классика","Любовные романы","Мистика","Поэзия","Приключения",
                                            "Проза","Триллер","Сказки","Учебная литература","Ужасы","Фантастика или фэнтези",
                                            "Юмор и сатира"};

        public int SortRead = -1;
        public int SortPen = -1;

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

        public void AddBook(Book book, TypeBook type)
        {
            if (type == TypeBook.ReadBook)
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
        public void BegingUpdate(int id,TypeBook type)
        {
            if (type == TypeBook.ReadBook)
                StartReadUpdate?.Invoke(id, null);
            else
                StartLaterUpdate?.Invoke(id, null);
        }

        /// <summary>
        /// Обработчик событя нажатия на кнопку удаления
        /// </summary>
        /// <param name="i">Позиция</param>
        /// <param name="type">
        /// True - прочитанная книга
        /// False - отложенная книга
        /// </param>
        public void BeginDelete(int id,TypeBook type)
        {
            if (type == TypeBook.ReadBook)
                StartReadDelete?.Invoke(id, null);
            else
                StartLaterDelete?.Invoke(id, null);
        }

        public void StartMoved(int id,int mark)
        {
            StartCopy?.Invoke(new int[] {id,mark},null);
        }

        public void BeginOpenQuotes(int pos)
        {
            StartOpenQuotes?.Invoke(pos, null);
        }
      
        public void Update(Book book,int id,TypeBook type)
        {
            if(type == TypeBook.ReadBook)
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

        public void Delete(int id,TypeBook type)
        {
            if (type == TypeBook.ReadBook)
            {
                DBControler.DeleteBook(_shelf.readBooksArray[id].ID, type);
                _shelf.readBooksArray.RemoveAt(id);
            }          
            else
            {
                DBControler.DeleteBook(_shelf.pendingBooksArray[id].ID, type);
                _shelf.pendingBooksArray.RemoveAt(id);
            }
               
        }

        public void ReadingBook(ReadBook book,int id)
        {
            Delete(id, TypeBook.PendingBook);
            AddBook(book,TypeBook.ReadBook);
        }
        
        public void SortBook(TypeSort typeSort,TypeBook typeBook)
        {
            if (typeBook == TypeBook.ReadBook)
                SortReadBook(typeSort);
            else
                SortPendingBook(typeSort);
        }

        private void SortReadBook(TypeSort type)
        {
            switch (type)
            {
                case TypeSort.ByName:
                    _shelf.readBooksArray.SortByName();
                    break;
                case TypeSort.ByAutor:
                    _shelf.readBooksArray.SortByAutor();
                    break;
                case TypeSort.ByDate:
                    _shelf.readBooksArray.SortByDate();
                    break;
                case TypeSort.ByMark:
                    _shelf.readBooksArray.SortByMark();
                    break;
                case TypeSort.ByFavorite:
                    _shelf.readBooksArray.SortByFavorite();
                    break;
            }
        }

        private void SortPendingBook(TypeSort type)
        {
            switch (type)
            {
                case TypeSort.ByName:
                    _shelf.pendingBooksArray.SortByName();
                    break;
                case TypeSort.ByAutor:
                    _shelf.pendingBooksArray.SortByAutor();
                    break;
            }
        }
    }
}