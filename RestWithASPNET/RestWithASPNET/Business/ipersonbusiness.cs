﻿using System.Collections.Generic;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business
{
    public interface ipersonbusiness
    {
        PersonVO create(PersonVO person);
        PersonVO findbyid(long id);
        List<PersonVO> findall();
        PersonVO update(PersonVO person);
        void delete(long id);
        
    }
}
