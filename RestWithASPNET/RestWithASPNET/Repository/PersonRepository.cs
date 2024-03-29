﻿using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Repository.Generic;
using System;
using System.Linq;

namespace RestWithASPNET.Repository
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository (MySQLContext context) : base (context)
        {

        }

        public Person Disable(long id)
        {
            if (!_context.People.Any(p => p.id.Equals(id)))
            {
                return null;
            }
            var user = _context.People.SingleOrDefault(p => p.id.Equals(id));
            if (user != null)
            {
                user.Enabled = false;
                try
                {
                    _context.Entry(user).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return user;
        }
    }
}
