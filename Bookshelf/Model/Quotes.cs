namespace Bookshelf.Model
{
    public class Quotes
    {
        public int Id { get; set; }

        public string Quot { get; set; }

        public string Autor { get; set; }

        public Quotes()
        {

        }

        public Quotes(string quot,string autor)
        {
            if (quot.Contains("\""))
                quot = quot.Replace("\"", "");

            if (quot.Contains("'"))
                quot = quot.Replace("'", "`");

            if (autor.Contains("\""))
                autor = autor.Replace("\"", "");

            if (autor.Contains("'"))
                autor = autor.Replace("'", "`");

            Quot = quot;
            Autor = autor;
        }
    }
}