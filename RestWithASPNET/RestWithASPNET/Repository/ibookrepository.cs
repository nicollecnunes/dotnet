using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Repository
{
    public interface ibookrepository
    {
        Book create(Book book);
        Book findbyid(long id);
        List<Book> findall();
        Book update(Book book);
        void delete(long id);

        public bool exists(long id);
        
    }
}
