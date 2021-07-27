using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Services
{
    public interface ipersonservice
    {
        Person create(Person person);
        Person findbyid(long id);
        List<Person> findall();
        Person update(Person person);
        void delete(long id);
        
    }
}
