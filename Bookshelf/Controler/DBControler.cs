using System.Collections.Generic;
using System.IO;
using Android.Graphics;
using Bookshelf.Model;
using SQLite;
using static Android.Graphics.Bitmap;
using System.Threading.Tasks;
using System;

namespace Bookshelf.Controler
{
    public class DBControler
    {
        private readonly static string filePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),"bookshelf.db3");

        [Table("ReadBook")]
        public class ReadBook
        {
            [PrimaryKey, AutoIncrement,Unique, Column("_id")]
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

        public static void CreatDB()
        {
            var db = new SQLiteConnection(filePath);
            db.CreateTable<ReadBook>();
            db.CreateTable<PendibgBook>();

        }

        public static (List<Model.ReadBook>,List<Model.PendingBook>) GetTables()
        {
            var db = new SQLiteConnection(filePath);
            var tableRead = db.Table<ReadBook>();

            List<Model.ReadBook> rb = new List<Model.ReadBook>();

            foreach (var item in tableRead)
            {
                Model.ReadBook read = new Model.ReadBook(item.Name, item.Autor,
                    BitmapFactory.DecodeByteArray(item.Photo, 0, item.Photo.Length),
                    item.CountPage, item.Discript, item.Mark, item.Categori);
                read.ID = item.Id;

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

        public static void AddBook(Book book,bool type)
        {
            var db = new SQLiteConnection(filePath);

            if(type)
            {
                var newBok = book as Model.ReadBook;
                using (MemoryStream ms = new MemoryStream())
                {
                    newBok.Photo.Compress(CompressFormat.Png, 100, ms);
                    db.Query<ReadBook>($"INSERT INTO ReadBook Values({new Random().Next(0,int.MaxValue)},'{newBok.Name}','{newBok.Autor}',?," +
                        $"{newBok.CountPage},'{newBok.Discript}',{newBok.Categori},{newBok.Mark})", ms.ToArray());
                }                
            }
            else
            {
                PendibgBook pendibg = new PendibgBook();

                var newBok = book as Model.PendingBook;
                using (MemoryStream ms = new MemoryStream())
                {
                    newBok.Photo.Compress(CompressFormat.Png, 100, ms);
                    db.Query<ReadBook>($"INSERT INTO ReadBook Values({new Random().Next(0, int.MaxValue)},'{newBok.Name}','{newBok.Autor}',?," +
                      $"{newBok.CountPage},'{newBok.Discript}',{newBok.Categori})", ms.ToArray());
                }
            }
        }

        public static void UpdateBook(Book book,bool type)
        {

        }
    }
}