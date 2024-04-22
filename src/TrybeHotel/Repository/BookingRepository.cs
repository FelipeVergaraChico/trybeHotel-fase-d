using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        // 9. Refatore o endpoint POST /booking
        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            try
            {
                var r = _context.Rooms.Find(booking.RoomId);
                var u = _context.Users.First(u => u.Email == email);
                var h = _context.Hotels.Find(r.HotelId);
                var c = _context.Cities.FirstOrDefault(c => c.CityId == h.CityId);

                if (booking.GuestQuant <= r.Capacity){
                    var register = new Booking{
                        RoomId = booking.RoomId,
                        UserId = u.UserId,
                        CheckIn = booking.CheckIn,
                        CheckOut = booking.CheckOut,
                        GuestQuant = booking.GuestQuant
                    };
                    var newBoking = _context.Bookings.Add(register);
                    _context.SaveChanges();
                    return new BookingResponse{
                        BookingId = newBoking.Entity.BookingId,
                        GuestQuant = newBoking.Entity.GuestQuant,
                        CheckIn = newBoking.Entity.CheckIn,
                        CheckOut = newBoking.Entity.CheckOut,
                        Room = new RoomDto{
                            roomId = r.RoomId,
                            name = r.Name,
                            image = r.Image,
                            capacity = r.Capacity,
                            hotel = new HotelDto{
                                hotelId = h.HotelId,
                                name = h.Name,
                                address = h.Address,
                                cityId = c.CityId,
                                cityName = c.Name,
                                state = c.State
                            }
                        }
                    };
                }
                else{
                    throw new Exception("Guest quantity over room capacity");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        // 10. Refatore o endpoint GET /booking
        public BookingResponse GetBooking(int bookingId, string email)
        {
             try
            {
                var b = _context.Bookings.Find(bookingId);
                var u = _context.Users.First(u => u.Email == email);
                if (b.UserId != u.UserId){
                    throw new Exception("Unauthorized access");
                }
                var r = _context.Rooms.Find(b.RoomId);
                var h = _context.Hotels.Find(r.HotelId);
                var c = _context.Cities.FirstOrDefault(c => c.CityId == h.CityId);
                return new BookingResponse{
                    BookingId = b.BookingId,
                    GuestQuant = b.GuestQuant,
                    CheckIn = b.CheckIn,
                    CheckOut = b.CheckOut,
                    Room = new RoomDto{
                        roomId = r.RoomId,
                        name = r.Name,
                        image = r.Image,
                        capacity = r.Capacity,
                        hotel = new HotelDto{
                            hotelId = h.HotelId,
                            name = h.Name,
                            address = h.Address,
                            cityId = c.CityId,
                            cityName = c.Name,
                            state = c.State
                        }
                    }
                };
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public Room GetRoomById(int RoomId)
        {
            throw new NotImplementedException();
        }

    }

}