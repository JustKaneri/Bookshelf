using System;
using System.Collections.Generic;


namespace Bookshelf.Model
{
    public class Shelf
    {
        public List<ReadBook> readBooksArray;
        public List<PendingBook> pendingBooksArray;

        public Shelf(List<ReadBook> readBooks, List<PendingBook> pendingBooks)
        {
            readBooksArray = readBooks;
            pendingBooksArray = pendingBooks;
        }
    }
}