
using Blog_Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.Json.Serialization;

namespace Blog_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private IConfiguration _configuration;

        private BlogContext _context;

        public FoodController(IConfiguration configuration, BlogContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        [HttpGet]
        [Route("GetFood")]
        public async Task<IActionResult> GetFoods()
        {
            try
            {
                string query = "SELECT * FROM Food";
                string sqlDataSource = _configuration.GetConnectionString("Blog");
                using (SqlConnection connection = new SqlConnection(sqlDataSource))
                {
                    await connection.OpenAsync();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using SqlDataReader reader = await command.ExecuteReaderAsync();
                        DataTable table = new DataTable();
                        table.Load(reader);
                        return new JsonResult(table);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("GetFoodImage/{id}")]
        public async Task<IActionResult> GetFoodImage(long id)
        {
            string imgPath = _context.Foods.Find(id)?.PhotoUrl;
            //  byte[] imgByte = System.IO.File.ReadAllBytes(imgPath); */ 
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(imgPath);
                response.EnsureSuccessStatusCode();
                var bytes = await response.Content.ReadAsByteArrayAsync();
                return File(bytes, "image/jpeg");

            }
        }
        [HttpPost]
        [Route("AddFoods")]
        public void AddFood(Food food)
        {
            _context.Foods.Add(food);
            _context.SaveChanges();
        }
        [HttpDelete]
        [Route("DeleteFoods")]
        public void DeleteFood(long id)
        {
            var food = _context.Foods.Find(id);
            if (food != null)
            {
                _context.Foods.Remove(food);
                _context.SaveChanges();
            }
        }
        [HttpPut]
        [Route("UpdateFoods")]
        public IActionResult UpdateFood(long id, [FromBody] Food food)
        {
            var existingFood = _context.Foods.ToList().FirstOrDefault(a => a.Foodid == id);
            if (existingFood == null) return NotFound();
            existingFood.FoodName = food.FoodName;
            existingFood.Price = food.Price;
            existingFood.PhotoUrl = food.PhotoUrl;
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Route("GetAllNews")]
        public List<News> GetNews()
        {
            return _context.News.ToList();
        }
        [HttpGet]
        [Route("GetNewsImage/{id}")]
        public async Task<IActionResult> getNewsImage(long id)
        {
            string imgPath = _context.News.Find(id)?.PhotoUrl;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(imgPath);
                responseMessage.EnsureSuccessStatusCode();
                var bytes = await responseMessage.Content.ReadAsByteArrayAsync();
                return File(bytes, "image/jpeg");
            }
        }
        [HttpGet]
        [Route("GetNews")]
        public IQueryable<News> getNews(string searchTerm)
        {
            return _context.News.Where(u => u.Newsid.ToString().Contains(searchTerm) ||
                                       u.Title.Contains(searchTerm) ||
                                       u.DateUpload.ToString().Contains(searchTerm) || 
                                       u.Author.Contains(searchTerm) ||
                                       u.Description.Contains(searchTerm));
       
        }

      


    }
}

    
