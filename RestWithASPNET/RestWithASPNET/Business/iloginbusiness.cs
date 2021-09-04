using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business{
    public interface iloginbusiness{
        TokenVO ValidateCredentials(UserVO user);
    }
}