using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Implementations{
    public class personRepositoryImplementation : ipersonrepository{
        private MySQLContext _context; 

        // construtor. recebe injecao do context
        public personRepositoryImplementation(MySQLContext context){ 
            _context = context;

        }


        //metodo create. recebe o objeto pessoa
        public Person create(Person person){
            try {
                _context.Add(person);
                _context.SaveChanges();

            }catch(Exception){
                throw;
            }
            return person;
        }


        //deleta a pessoa pelo id, nao pelo objeto
        public void delete(long id){
            var result = _context.People.SingleOrDefault(p => p.id.Equals(id));
            if (result != null){
                try{
                    _context.People.Remove(result);
                    _context.SaveChanges();

                }catch (Exception){
                    throw;
                }
            }
        }


        //lista todas as pessoas cadastradas
        public List<Person> findall(){
            return _context.People.ToList();
        }

       
        //encontra uma pessoa especifica pela PK
        public Person findbyid(long id){
            return _context.People.SingleOrDefault(p => p.id.Equals(id)); //retorna um p que tenha id == id recebido
        }


        //atualiza informacoes de uma pessoa, recebe o objeto todo e substitui no bd
        public Person update(Person person){
            if (!exists(person.id)){
                return new Person(); //se nao existe, cria
            }
            var result = _context.People.SingleOrDefault(p => p.id.Equals(person.id));
            if (result != null){
                try{
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();

                }catch (Exception){
                    throw;
                }
            } 
            return person; //simula acesso. retorna a mesma pessoa
        }


        //verifica se uma pessoa existe pelo seu id
        public bool exists(long id){
            return _context.People.Any(p => p.id.Equals(id));
        }
    }
}
