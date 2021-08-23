using RestWithASPNET.Model;
using RestWithASPNET.Repository;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Implementations{
    public class bookBusinessImplementation : ibookbusiness{
        private readonly irepository<Book> _repository; //irepository do tipo book

        // construtor. recebe injecao do context
        public bookBusinessImplementation(irepository<Book> repository){ 
            _repository = repository;

        }

        //metodo create. recebe o objeto pessoa
        public Book create(Book book){
            return _repository.create(book);
        }


        //deleta a pessoa pelo id, nao pelo objeto
        public void delete(long id){
            _repository.delete(id);
        }


        //lista todas as pessoas cadastradas
        public List<Book> findall(){
            return _repository.findall();
        }

       
        //encontra uma pessoa especifica pela PK
        public Book findbyid(long id){
            return _repository.findbyid(id); //retorna um p que tenha id == id recebido
        }


        //atualiza informacoes de uma pessoa, recebe o objeto todo e substitui no bd
        public Book update(Book book){
            return _repository.update(book);
        }

    }
}
