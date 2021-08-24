using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using RestWithASPNET.Repository;
using System.Collections.Generic;


namespace RestWithASPNET.Business.Implementations{
    public class personBusinessImplementation : ipersonbusiness{
        private readonly irepository<Person> _repository; 
        private readonly PersonConverter _converter;

        // construtor. recebe injecao do context
        public personBusinessImplementation(irepository<Person> repository){ 
            _repository = repository;
            _converter = new PersonConverter();

        }

        //metodo create. recebe o objeto pessoa
        public PersonVO create(PersonVO person){ //chega um VO
            //antes de persistir, precisa converter pra entidade
            //pq o repositorio trabalha com entidade, nao com VOs
            var personEntity = _converter.parse(person); //o VO vira entidade
            personEntity = _repository.create(personEntity); //agr pode criar

            return _converter.parse(personEntity); //entidade -> VO e devolve
        }


        //deleta a pessoa pelo id, nao pelo objeto
        public void delete(long id){
            _repository.delete(id);
        }


        //lista todas as pessoas cadastradas
        public List<PersonVO> findall(){
            return _converter.parse(_repository.findall());
        }

       
        //encontra uma pessoa especifica pela PK
        public PersonVO findbyid(long id){
            return _converter.parse(_repository.findbyid(id)); //converte essa entidade para VO
        }


        //atualiza informacoes de uma pessoa, recebe o objeto todo e substitui no bd
        public PersonVO update(PersonVO person){
            //antes de persistir, precisa converter pra entidade
            //pq o repositorio trabalha com entidade, nao com VOs
            var personEntity = _converter.parse(person); //o VO vira entidade
            personEntity = _repository.update(personEntity); //agr pode criar

            return _converter.parse(personEntity); //entidade -> VO e devolve
        }

    }
}
