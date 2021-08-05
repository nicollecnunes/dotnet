using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Repository
{
    public interface ipersonrepository
    {
        Person create(Person person);
        Person findbyid(long id);
        List<Person> findall();
        Person update(Person person);
        void delete(long id);

        public bool exists(long id);
        
    }
}
