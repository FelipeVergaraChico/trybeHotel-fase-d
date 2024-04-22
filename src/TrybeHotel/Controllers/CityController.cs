using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCities(){
            var res = _repository.GetCities();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult PostCity([FromBody] City city){
            var res = _repository.AddCity(city);
            return CreatedAtAction(nameof(GetCities), new { id = res.cityId }, res);
        }

        // 3. Desenvolva o endpoint PUT /city
        [HttpPut]
        public IActionResult PutCity([FromBody] City city){
            try
            {
                var res = _repository.UpdateCity(city);
                return Ok(res);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message });
            }
        }
    }
}