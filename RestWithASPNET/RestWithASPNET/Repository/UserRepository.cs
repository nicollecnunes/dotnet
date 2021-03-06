using Microsoft.EntityFrameworkCore;
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

        public User RefreshUserInfo(User user){
            if(!_context.User.Any(u => u.id.Equals(user.id))){ //se nao existir
                return null;
            }
            var result = _context.User.SingleOrDefault(p => p.id.Equals(user.id));

            if (result != null){
                try{
                     _context.Entry(result).CurrentValues.SetValues(user);
                     _context.SaveChanges();
                     return(result);
                }
                catch(Exception)
                {
                    throw;
                }
            }
                return result;
        }

        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm) //encriptar a senha
        {
            byte[] inputbytes = Encoding.UTF8.GetBytes(input);
            byte[] hashed = algorithm.ComputeHash(inputbytes);

            return BitConverter.ToString(hashed);
        }

        public User validateCredentials(string username)
        {
            return _context.User.SingleOrDefault(u => (u.username == username));
        }

        public bool RevokeToken(string username)
        {
            var user = _context.User.SingleOrDefault( u => (u.username == username));

            if (user is null){
                return false;
            }
            user.refreshtoken = null;
            _context.SaveChanges();
            return true;
        }
    }
}