using System.Collections.Generic;
using System.Linq;
using RestWithASPNET.Data.Converter.Contract;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;

namespace RestWithASPNET.Data.Converter.Implementations{
    public class PersonConverter : Iparser<PersonVO, Person>, Iparser<Person, PersonVO>
    {
        public Person parse(PersonVO o) // vo  -> person
        {
            if (o == null){
                return null;
            }else{
                return new Person{
                    id = o.Id,
                    Fname = o.Fname,
                    Lname = o.Lname,
                    Address = o.Address,
                    Gender = o.Gender
                };
            }
        }

        public List<Person> parse(List<PersonVO> o)
        {
            if (o == null){
                return null;
            }else{
                return o.Select(item => parse(item)).ToList();
            }
        }

        public PersonVO parse(Person o) // person -> vo
        {
            if (o == null){
                return null;
            }else{
                return new PersonVO{ //mapeia o objeto
                    Id = o.id,
                    Fname = o.Fname,
                    Lname = o.Lname,
                    Address = o.Address,
                    Gender = o.Gender
                };
            }
        }

        public List<PersonVO> parse(List<Person> o)
        {
            if (o == null){
                return null;
            }else{
                return o.Select(item => parse(item)).ToList(); //for each em cada e chama o parse
            }
        }
    }
}