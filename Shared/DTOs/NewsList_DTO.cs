using Microsoft.AspNetCore.Http;

namespace News.Shared.DTOs
{
    public class NewsList_DTO
    {
        public int id { get; set; }
        public string? title { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string? subTitle { get; set; }
        public string? shortDetails { get; set; }
        public string? details { get; set; }
        //public IFormFile? newImage { get; set; }
        public byte[]? newImage { get; set; }
        public string? image { get; set; }
        public int categoryid { get; set; }
    }
}
