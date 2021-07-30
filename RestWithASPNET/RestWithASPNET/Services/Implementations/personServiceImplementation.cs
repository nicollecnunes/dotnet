using RestWithASPNET.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASPNET.Services.Implementations
{
    public class personServiceImplementation : ipersonservice
    {
        private volatile int count; //mock id
        public Person create(Person person)
        {
            return person; //simula acesso. retorna a mesma pessoa
        }

        public void delete(long id)
        {
            
        }

        public List<Person> findall()
        {
            List<Person> people = new List<Person>();
            for(int i = 0; i < 8; i++)
            {
                Person person = mockPerson(i);
                people.Add(person);
            }
            return people;
        }

       

        public Person findbyid(long id)
        {
            return new Person
            {
                id = incrementAndGet(),
                fname = "Nicolle",
                lname = "Nunes",
                adress = "Ouro Preto MG",
                gender = "Female",
            };
        }

        public Person update(Person person)
        {
            return person;
        }
        
        private Person mockPerson(int i)
        {
            string dynamicGender = "";
            string dynamicFname = "";
            string dynamicLname = "";

            if (i % 2 == 0)
            {
                dynamicGender = "even male";
                dynamicFname = "even name";
                dynamicLname = "not odd last name";
            }
            else
            {
                dynamicGender = "odd female";
                dynamicFname = "odd name";
                dynamicLname = "not even last name";
            }

            return new Person
            {
                id = incrementAndGet(),
                fname = dynamicFname + i,
                lname = dynamicLname + i,
                adress = "Adress" + i,
                gender = dynamicGender,
            };
        }

        private long incrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}
