using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;
namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]

    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert){
            try
            {
                var user = HttpContext.User.Identity as ClaimsIdentity;
                var em = user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var res = _repository.Add(bookingInsert, em!);
                return Created("", res);
            }
            catch (Exception e)
            {

                return BadRequest(new { message = e.Message });
            }
        }


        [HttpGet("{Bookingid}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid){
            try
            {
                var u = HttpContext.User.Identity as ClaimsIdentity;
                var em = u?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var res = _repository.GetBooking(Bookingid, em!);
                return Ok(res);
            }
            catch (Exception e)
            {

                return Unauthorized(new { message = e.Message });
            }
        }
    }
}