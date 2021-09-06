using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business{
    public interface iloginbusiness{
        TokenVO validateCredentials(UserVO user);

        TokenVO validateCredentials(TokenVO token);

        bool RevokeToken(string username);
    }
}