namespace CompanyAPIs.Services.UsersService
{
    public class UsersService : IUsersServices
    {

        private static List<Users> users = new List<Users>
        {
                    new Users
                    { Id = 1,
                    FirstName = "Test",
                    LastName = "Test1",
                    Username = "Test123",
                    Email = "Test@gmail.com"
                    },
                    new Users
                    { Id = 2,
                    FirstName = "Test2",
                    LastName = "Test2",
                    Username = "Test22",
                    Email = "Test2@gmail.com"
                    }
        };

        public List<Users> AddUser(Users user)
        {
            users.Add(user);
            return users;
        }

        public List<Users>? DeleteUser(int id)
        {
            var User = users.Find(x => x.Id == id);
            if (User == null)
            {
                return null;

            }
            users.Remove(User);
            return users;
        }

        public List<Users> GetAllUsers()
        {
            return users;
        }

        public Users? GetSingleUsers(int id)
        {
            var User = users.Find(x => x.Id == id);
            if (User is null)
            
                return null;

            
            return User;
        }

        public List<Users>? UpdateUser(int id, Users request)
        {
            var User = users.Find(x => x.Id == id);
            if (User == null)
            {
                return null;

            }

            User.Username = request.Username;
            User.Email = request.Email;
            User.FirstName = request.FirstName;
            User.LastName = request.LastName;
            users.Add(User);

            return users;
        }
    }
}
