using System.Collections.Generic;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Base;

namespace RestWithASPNET.Repository
{
    public interface irepository<T> where T : BaseEntity //t extende a entidade base
    {
        T create(T item);
        T findbyid(long id);
        List<T> findall();
        T update(T item);
        void delete(long id);

        public bool exists(long id);
        
    }
}
