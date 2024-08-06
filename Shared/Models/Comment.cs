namespace News.Shared.Models
{
    public class Comment
    {
        public int id { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string? details { get; set; }
        public int newsListid { get; set; }
        public NewsList? newsList { get; set; }
    }
}
