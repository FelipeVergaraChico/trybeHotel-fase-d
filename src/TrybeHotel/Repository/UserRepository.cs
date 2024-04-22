using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
                if (user != null){
                    return new UserDto(){
                        UserId = user.UserId,
                        Name = user.Name,
                        Email = user.Email,
                        UserType = user.UserType
                    };
                } else {
                    throw new Exception("Incorrect e-mail or password");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
        public UserDto Add(UserDtoInsert user)
        {
            try
            {
                var userEx = _context.Users.FirstOrDefault(u => u.Email == user.Email);
                if (userEx != null){
                    throw new Exception("User email already exists");
                }
                var userNew = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    UserType = "client"
                };
                _context.Users.Add(userNew);
                _context.SaveChanges();
                return new UserDto
                {
                    UserId = userNew.UserId,
                    Name = userNew.Name,
                    Email = userNew.Email,
                    UserType = userNew.UserType
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            try
            {
                var res = _context.Users.Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    UserType = u.UserType
                });
                return res;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

    }
}