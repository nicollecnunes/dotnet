using RestWithASPNET.Model;

namespace RestWithASPNET.Repository
{
    public interface IPersonRepository : irepository<Person>
    {
        Person Disable(long id);
    }
}