using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using RestWithASPNET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASPNET.Business.Implementations{
    public class personBusinessImplementation : ipersonbusiness{
        private readonly ipersonrepository _repository; 

        // construtor. recebe injecao do context
        public personBusinessImplementation(ipersonrepository repository){ 
            _repository = repository;

        }

        //metodo create. recebe o objeto pessoa
        public Person create(Person person){
            return _repository.create(person);
        }


        //deleta a pessoa pelo id, nao pelo objeto
        public void delete(long id){
            _repository.delete(id);
        }


        //lista todas as pessoas cadastradas
        public List<Person> findall(){
            return _repository.findall();
        }

       
        //encontra uma pessoa especifica pela PK
        public Person findbyid(long id){
            return _repository.findbyid(id); //retorna um p que tenha id == id recebido
        }


        //atualiza informacoes de uma pessoa, recebe o objeto todo e substitui no bd
        public Person update(Person person){
            return _repository.update(person);
        }

    }
}
