using RoleBasedAuth.Contract;

namespace RoleBasedAuth
{
    public interface IUserService
    {
        User Authenticate(AuthRequest request);
        User GetById(int userId);
    }
}