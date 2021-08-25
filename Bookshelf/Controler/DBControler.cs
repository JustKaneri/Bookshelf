using System;
using System.Collections.Generic;
using System.IO;
using Android.Graphics;
using Bookshelf.Model;
using SQLite;
using static Android.Graphics.Bitmap;

namespace Bookshelf.Controler
{
    public class DBControler
    {
        private readonly static string filePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "bookshelf.db3");

        [Table("ReadBook")]
        public class ReadBook
        {
            [PrimaryKey, AutoIncrement, Unique, Column("_id")]
            public int Id { get; set; }
            [Column("Name")]
            public string Name { get; set; }
            [Column("Autor")]
            public string Autor { get; set; }
            [Column("Photo")]
            public byte[] Photo { get; set; }
            [Column("CountPage")]
            public int CountPage { get; set; }
            [Column("Discript")]
            public string Discript { get; set; }
            [Column("Categori")]
            public int Categori { get; set; }
            [Column("Mark")]
            public int Mark { get; set; }
            [Column("Favorite")]
            public bool Favorite { get; set; }
            [Column("Date")]
            public string DateRead { get; set; }
        }


        [Table("PendibgBook")]
        public class PendibgBook
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [Column("Name")]
            public string Name { get; set; }
            [Column("Autor")]
            public string Autor { get; set; }
            [Column("Photo")]
            public byte[] Photo { get; set; }
            [Column("CountPage")]
            public int CountPage { get; set; }
            [Column("Discript")]
            public string Discript { get; set; }
            [Column("Categori")]
            public int Categori { get; set; }
        }

        [Table("Quotes")]
        public class Quotes
        {
            [PrimaryKey, AutoIncrement, Column("_id")]
            public int Id { get; set; }
            [Column("Id_Book")]
            public int Id_Book { get; set; }
            [Column("Text")]
            public string Name { get; set; }
            [Column("Autor")]
            public string Autor { get; set; }
        }

        public static void CreatDB()
        {
            var db = new SQLiteConnection(filePath);
            db.CreateTable<ReadBook>();
            db.CreateTable<PendibgBook>();
            db.CreateTable<Quotes>();
        }

        public static (List<Model.ReadBook>, List<Model.PendingBook>) GetTables()
        {
            var db = new SQLiteConnection(filePath);
            var tableRead = db.Table<ReadBook>();

            List<Model.ReadBook> rb = new List<Model.ReadBook>();

            foreach (var item in tableRead)
            {
                Model.ReadBook read = new Model.ReadBook(item.Name, item.Autor,
                    BitmapFactory.DecodeByteArray(item.Photo, 0, item.Photo.Length),
                    item.CountPage, item.Discript, item.Mark, item.Categori, item.DateRead);
                read.ID = item.Id;
                read.Favorite = item.Favorite;

                rb.Add(read);

                read = null;
            }

            List<Model.PendingBook> pb = new List<PendingBook>();

            var tableLeter = db.Table<PendibgBook>();
            foreach (var item in tableLeter)
            {
                Model.PendingBook later = new Model.PendingBook(item.Name, item.Autor,
                    BitmapFactory.DecodeByteArray(item.Photo, 0, item.Photo.Length),
                    item.CountPage, item.Discript, item.Categori);

                later.ID = item.Id;

                pb.Add(later);

                later = null;
            }


            return (rb, pb);
        }

        public static int AddBook(Book book, bool type)
        {
            var db = new SQLiteConnection(filePath);

            if (type)
            {
                var newBok = book as Model.ReadBook;
                using (MemoryStream ms = new MemoryStream())
                {
                    newBok.Photo.Compress(CompressFormat.Png, 100, ms);
                    db.Execute($"INSERT INTO ReadBook (Name,Autor,Photo,CountPage,Discript,Categori,Mark,Favorite,Date) Values('{newBok.Name}','{newBok.Autor}',?," +
                        $"{newBok.CountPage},'{newBok.Discript}',{newBok.Categori},{newBok.Mark},{newBok.Favorite},'{newBok.DateReading}')", ms.ToArray());
                }

                var res = db.Query<ReadBook>("SELECT * FROM ReadBook ORDER BY _id DESC LIMIT 1;");

                return res[0].Id;
            }
            else
            {
                PendibgBook pendibg = new PendibgBook();

                var newBok = book as Model.PendingBook;
                using (MemoryStream ms = new MemoryStream())
                {
                    newBok.Photo.Compress(CompressFormat.Png, 100, ms);
                    db.Execute($"INSERT INTO PendibgBook (Name,Autor,Photo,CountPage,Discript,Categori) Values('{newBok.Name}','{newBok.Autor}',?," +
                      $"{newBok.CountPage},'{newBok.Discript}',{newBok.Categori})", ms.ToArray());
                }

                var res = db.Query<PendibgBook>("SELECT * FROM PendibgBook ORDER BY _id DESC LIMIT 1;");

                return res[0].Id;
            }
        }

        public static void UpdateBook(Book book, bool type)
        {
            var db = new SQLiteConnection(filePath);

            if (type)
            {
                var newBok = book as Model.ReadBook;

                using (MemoryStream ms = new MemoryStream())
                {
                    newBok.Photo.Compress(CompressFormat.Png, 100, ms);

                    db.Execute($"UPDATE ReadBook SET Name = '{newBok.Name}',Autor = '{newBok.Autor}'," +
                        $"Photo  = ?, CountPage = {newBok.CountPage}, " +
                        $"Discript = '{newBok.Discript}',Categori = {newBok.Categori}, " +
                        $"Mark  = {newBok.Mark} , Date = '{newBok.DateReading}'" +
                        $"WHERE _id = {newBok.ID}", ms.ToArray());
                }
            }
            else
            {
                var newBok = book as Model.PendingBook;

                using (MemoryStream ms = new MemoryStream())
                {
                    newBok.Photo.Compress(CompressFormat.Png, 100, ms);

                    db.Execute($"UPDATE PendibgBook SET Name = '{newBok.Name}',Autor = '{newBok.Autor}'," +
                        $"Photo  = ?, CountPage = {newBok.CountPage}, " +
                        $"Discript = '{newBok.Discript}',Categori = {newBok.Categori} " +
                        $"WHERE _id = {newBok.ID}", ms.ToArray());
                }
            }
        }

        public static void UpdateFavoriteStatus(int id, bool IsFavorite)
        {
            var db = new SQLiteConnection(filePath);

            db.Execute($"UPDATE ReadBook SET Favorite = ? Where _id = {id}", IsFavorite);
        }

        public static void DeleteBook(int id, bool type)
        {
            var db = new SQLiteConnection(filePath);

            if (type)
            {
                ClearQuites(id);
                db.Delete<ReadBook>(id);
            }
            else
            {
                db.Delete<PendibgBook>(id);
            }

        }

        public static List<Model.Quotes> GetQuotes(int idBook)
        {
            List<Model.Quotes> quotes = new List<Model.Quotes>();
            var db = new SQLiteConnection(filePath);

            var res = db.Query<Quotes>("Select * From Quotes Where Id_Book = ?", idBook);


            foreach (var item in res)
            {
                Model.Quotes quot = new Model.Quotes();
                quot.Id = item.Id;
                quot.Autor = item.Autor;
                quot.Quot = item.Name;

                quotes.Add(quot);

                quot = null;
            }

            return quotes;

        }

        public static int AddQuot(Model.Quotes quot, int idBook)
        {
            var db = new SQLiteConnection(filePath);

            db.Execute($"INSERT INTO Quotes (Id_Book,Text,Autor) Values({idBook},'{quot.Quot}','{quot.Autor}')");

            var res = db.Query<Quotes>("SELECT * FROM Quotes ORDER BY _id DESC LIMIT 1;");

            return res[0].Id;
        }

        public static void UpadteQuote(Model.Quotes quot)
        {
            var db = new SQLiteConnection(filePath);

            db.Execute($"UPDATE Quotes SET Text = '{quot.Quot}',Autor = '{quot.Autor}' WHERE _id = {quot.Id}");
        }

        private static void ClearQuites(int id)
        {
            var db = new SQLiteConnection(filePath);
            db.Execute("Delete From Quotes where Id_Book = ?", id); ;
        }

        public static void DeleteQuites(int id)
        {
            var db = new SQLiteConnection(filePath);
            db.Execute("Delete From Quotes where _id = ?", id); ;
        }


        public static string CountQuotes()
        {
            var db = new SQLiteConnection(filePath);
            return db.Table<Quotes>().Count().ToString();
        }


    }
}