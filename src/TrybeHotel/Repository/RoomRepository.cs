using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 7. Refatore o endpoint GET /room
        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            try
            {
                var r = from room in _context.Rooms join hotel in _context.Hotels on room.HotelId equals hotel.HotelId where room.HotelId == HotelId
                            select new RoomDto()
                            {
                                roomId = room.RoomId,
                                name = room.Name,
                                capacity = room.Capacity,
                                image = room.Image,
                                hotel = new HotelDto()
                                {
                                    name = hotel.Name,
                                    address = hotel.Address,
                                    cityId = hotel.CityId,
                                    cityName = hotel.City!.Name,
                                    state = hotel.City.State
                                }
                            };
            return r;
            }
            catch (Exception e)
            {

            throw new Exception(e.Message);
            }
        }

        // 8. Refatore o endpoint POST /room
        public RoomDto AddRoom(Room room) {
            try
            {
                var r = _context.Rooms.Add(room);
                _context.SaveChanges();
                var newHotel = _context.Hotels.Find(room.HotelId);
                var newCity = _context.Cities.FirstOrDefault(c => c.CityId == newHotel.CityId);
                return new RoomDto {
                    roomId = r.Entity.RoomId,
                    name = r.Entity.Name,
                    image = r.Entity.Image,
                    capacity = r.Entity.Capacity,
                    hotel = new HotelDto {
                        hotelId = newHotel.HotelId,
                        name = newHotel.Name,
                        address = newHotel.Address,
                        cityId = newHotel.CityId,
                        cityName = newCity.Name,
                        state = newCity.State
                    }
                };
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public void DeleteRoom(int RoomId) {
            try
            {
                var r = _context.Rooms.Find(RoomId);
                _context.Rooms.Remove(r);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}