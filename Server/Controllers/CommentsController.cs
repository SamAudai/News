using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Server.Repositories.Interfaces;
using News.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly GenericInterface<Comment> _comment;

        public CommentsController(GenericInterface<Comment> comment)
        {
            _comment = comment;
        }

        // GET: api/<CommentsController>
        [HttpGet]
        public IActionResult GetAllComments()
        {
            return Ok(_comment.GetAllData("newsList"));
        }

        // GET: api/<CommentsController>
        [HttpGet("{id}")]
        public IActionResult GetAllComments(string id)
        {
            return Ok(_comment.GetAllData("newsList").Where(c => c.newsListid == int.Parse(id)));
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public IActionResult GetCommentById(int id)
        {
            return Ok(_comment.GetAllData("newsList").Where(c => c.id == id));
        }

        // POST api/<CommentsController>
        [HttpPost]
        [Authorize]
        public IActionResult AddComment([FromBody] Comment value)
        {
            _comment.AddData(value);
            return Ok(value);
        }

        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateComment([FromBody] Comment value)
        {
            try
            {
                _comment.UpdateData(value);
                return Ok(value);
            }
            catch
            {
                return BadRequest();
            }
        }

        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public void DeleteComment(int id)
        {
            _comment.DeleteData(id);
        }
    }
}
