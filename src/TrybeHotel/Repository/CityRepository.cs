using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 4. Refatore o endpoint GET /city
        public IEnumerable<CityDto> GetCities()
        {
            try
            {
                var res = _context.Cities.Select(c => new CityDto()
                {
                    cityId = c.CityId,
                    name = c.Name,
                    state = c.State
                }).ToList();
                return res;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 2. Refatore o endpoint POST /city
        public CityDto AddCity(City city)
        {
            try
            {
                _context.Cities.Add(city);
                _context.SaveChanges();
                var nCity = _context.Cities.First(c => c.CityId == city.CityId);
                var res = new CityDto()
                {
                    cityId = nCity.CityId,
                    name = nCity.Name,
                    state = nCity.State
                };
                return res;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 3. Desenvolva o endpoint PUT /city
        public CityDto UpdateCity(City city)
        {
            try
            {
                var lc = _context.Cities.Find(city.CityId);
                if (lc == null) {
                    throw new Exception("City not found");
                }
                lc.Name = city.Name;
                lc.State = city.State;
                _context.SaveChanges();
                return new CityDto
                {
                    cityId = lc.CityId,
                    name = lc.Name,
                    state = lc.State
                };
                }
            catch (Exception e)
            {

            throw new Exception(e.Message);
            }
        }

    }
}