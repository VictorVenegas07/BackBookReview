namespace EmailSend.Models
{
    public class EmailModel
    {
        public string Username { get; set; }
        public List<Article> Articles { get; set; }
        public int TotalArticles { get; set; }
    }

    public class Article
    {
        public string NumeroRadicado { get; set; }
        public string Titulo { get; set; }
        public string Estado { get; set; }
    }
}
