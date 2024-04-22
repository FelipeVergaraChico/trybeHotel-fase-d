using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        //  5. Refatore o endpoint GET /hotel
        public IEnumerable<HotelDto> GetHotels()
        {
            try
            {
                var res = _context.Hotels.Select(h => new HotelDto()
                {
                    hotelId = h.HotelId,
                    name = h.Name,
                    address = h.Address,
                    cityId = h.CityId,
                    cityName = _context.Cities.First(c => c.CityId == h.CityId).Name,
                    state = _context.Cities.First(c => c.CityId == h.CityId).State
                }).ToList();
                return res;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 6. Refatore o endpoint POST /hotel
        public HotelDto AddHotel(Hotel hotel)
        {
            try
            {
                _context.Hotels.Add(hotel);
                _context.SaveChanges();
                var city = _context.Cities.First(c => c.CityId == hotel.CityId);
                if (city != null)
                {
                    return new HotelDto()
                    {
                        hotelId = hotel.HotelId,
                        name = hotel.Name,
                        address = hotel.Address,
                        cityId = hotel.CityId,
                        cityName = city.Name,
                        state = city.State
                    };
                }
                else
                {
                    throw new Exception("City Not Found");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}