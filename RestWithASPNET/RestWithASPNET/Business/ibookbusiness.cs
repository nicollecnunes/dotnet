using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Business
{
    public interface ibookbusiness
    {
        Book create(Book book);
        Book findbyid(long id);
        List<Book> findall();
        Book update(Book book);
        void delete(long id);
        
    }
}
