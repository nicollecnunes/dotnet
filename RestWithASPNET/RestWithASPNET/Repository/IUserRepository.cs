using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using System.Security.Cryptography;

namespace RestWithASPNET.Repository{
    public interface IUserRepository{
        User validateCredentials(UserVO user);
        User validateCredentials(string username);
        bool RevokeToken(string username);
        User RefreshUserInfo(User user);
    }
}