namespace News.Shared.Models
{
    public class NewsList
    {
        public int id { get; set; }
        public string? title { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string? subTitle { get; set; }
        public string? shortDetails { get; set; }
        public string? details { get; set; }
        public string? image { get; set; }
        public int categoryid { get; set; }
        public Category? category { get; set; }
    }
}
