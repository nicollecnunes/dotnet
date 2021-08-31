using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Repository{
    public interface IUserRepository{
        User validateCredentials(UserVO user);
    }
}