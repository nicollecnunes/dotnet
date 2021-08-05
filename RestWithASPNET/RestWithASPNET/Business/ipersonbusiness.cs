using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Business
{
    public interface ipersonbusiness
    {
        Person create(Person person);
        Person findbyid(long id);
        List<Person> findall();
        Person update(Person person);
        void delete(long id);
        
    }
}
