using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RestWithASPNET.Repository{
    public class UserRepository : IUserRepository
    { //implementa isuerrep

        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context) //construtor
        {
            _context = context;
        }

        public User validateCredentials(UserVO user)
        {
            var pass = ComputeHash(user.password, new SHA256CryptoServiceProvider());
            return _context.User.FirstOrDefault(u => (u.username == user.username) && (u.password == pass));
            // a senha que veio na requisição foi criptografada em pass e deve ser igual a do banco
            //que tambem esta criptografada
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm) //encriptar a senha
        {
            byte[] inputbytes = Encoding.UTF8.GetBytes(input);
            byte[] hashed = algorithm.ComputeHash(inputbytes);

            return BitConverter.ToString(hashed);
        }
    }
}