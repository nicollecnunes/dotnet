using System.Collections.Generic;
using RestWithASPNET.Model;

namespace RestWithASPNET.Business
{
    public interface ibookbusiness
    {
        BookVO create(BookVO book);
        BookVO findbyid(long id);
        List<BookVO> findall();
        BookVO update(BookVO book);
        void delete(long id);
        
    }
}
