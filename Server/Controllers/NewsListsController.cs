using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Server.Repositories.Interfaces;
using News.Shared.DTOs;
using News.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class NewsListsController : ControllerBase
    {
        private readonly GenericInterface<NewsList> _newslist;
        private readonly IWebHostEnvironment _webHostEnvironment;//for news image

        public NewsListsController(GenericInterface<NewsList> newslist, IWebHostEnvironment webHostEnvironment)
        {
            _newslist = newslist;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/<NewsListsController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllNews()
        {
            return Ok(_newslist.GetAllData("category").OrderByDescending(n => n.date));
        }

        // GET api/<NewsListsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetNewsById(int id)
        {
            return Ok(_newslist.GetAllData("category").Where(nl => nl.id == id).SingleOrDefault());
        }

        // GET api/<NewsListsController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetNewsByCategory(int id)
        {
            return Ok(_newslist.GetAllData("category").Where(nl => nl.category!.id == id));
        }

        // GET api/<NewsListsController>
        [HttpGet]
        public IActionResult GetNewsByTitle(string title)
        {
            return Ok(_newslist.GetAllData("category").Where(nl => nl.category!.name!.Contains(title)));
        }

        // POST api/<NewsListsController>
        [HttpPost]
        [Authorize(Roles ="Admin, Content Creator")]
        public async Task<IActionResult> AddNews([FromBody] NewsList_DTO value)
        {
            string imageName = "";

            if (value.newImage != null)
            {
                string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "NewsImages");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                imageName = Guid.NewGuid() + "_" + value.image;
                string imagePath = Path.Combine(fullPath, imageName);

                await using var stream = new FileStream(imagePath, FileMode.Create);
                stream.Write(value.newImage, 0, value.newImage.Length);
                
            }
            NewsList newsList = new NewsList
            {
                title = value.title,
                subTitle = value.subTitle,
                details = value.details,
                shortDetails = value.shortDetails,
                categoryid = value.categoryid,
                image = imageName
            };
            _newslist.AddData(newsList);
            return Ok(newsList);
        }

        // PUT api/<NewsListsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateNews([FromBody] NewsList value)
        {
            try
            {
                _newslist.UpdateData(value);
                return Ok(value);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<NewsListsController>/5
        [HttpDelete("{id}")]
        public void DeleteNews(int id)
        {
            _newslist.DeleteData(id);
        }


    }
}
