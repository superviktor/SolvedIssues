using Authentiocation_and_Authorization.Contract;

namespace Authentiocation_and_Authorization
{
    public interface IUserService
    {
        AuthResponse Authenticate(AuthRequest request);
        User GetById(int userId);
    }
}