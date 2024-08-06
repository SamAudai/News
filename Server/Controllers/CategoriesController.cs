using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using News.Server.Repositories.Interfaces;
using News.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace News.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly GenericInterface<Category> _category;

        public CategoriesController(GenericInterface<Category> category)
        {
            _category = category;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAllCategories()
        {
            return Ok(_category.GetAllData());
        }
        [HttpGet]
        public IActionResult GetCategoriesError()
        {
            return StatusCode(500, "Something wrong!");
        }
        [HttpGet]
        public IActionResult GetCategoriesUnauthorized()
        {
            return StatusCode(401, "Not Allowed");
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _category.GetDataById(id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult AddCategory([FromBody] Category value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                    var categoryName = _category.GetAllData().Where(c => c.name == value.name).FirstOrDefault();
                    if (categoryName != null)
                    {
                        ModelState.AddModelError("Error", "Category name already exists");
                        //return BadRequest(ModelState);
                        return BadRequest(ModelState.First().Value.Errors.First().ErrorMessage);
                    }
                    _category.AddData(value);
                    return Ok(value);
                }
                return BadRequest(ModelState);
            }
            catch
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromBody] Category value)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Category category = new Category
                    {
                        id = value.id,
                        name = value.name,
                    };
                    _category.UpdateData(category);
                    return Ok(category);
                }
                return BadRequest(ModelState);
            }
            catch(IndexOutOfRangeException ex)
            {
                return StatusCode(500, ex.Message);
            }


        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            _category.DeleteData(id);
            return StatusCode(200, "Delete category successfully");
        }
    }
}