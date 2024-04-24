namespace CompanyAPIs.Services.UsersService
{
    public interface IUsersServices
    {
        List<Users> GetAllUsers();
        Users? GetSingleUsers(int id);

        List<Users> AddUser(Users user);

        List<Users>? UpdateUser(int id, Users request);

        List<Users>? DeleteUser(int id);
    }
}
