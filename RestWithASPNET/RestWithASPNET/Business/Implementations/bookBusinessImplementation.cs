using RestWithASPNET.Data.Converter.Implementations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Model;
using RestWithASPNET.Repository;
using System.Collections.Generic;

namespace RestWithASPNET.Business.Implementations{
    public class bookBusinessImplementation : ibookbusiness{
        private readonly irepository<Book> _repository; //irepository do tipo book
        private readonly BookConverter _converter;

        // construtor. recebe injecao do context
        public bookBusinessImplementation(irepository<Book> repository){ 
            _repository = repository;
            _converter = new BookConverter(); //used on update and create

        }

        //metodo create. recebe o objeto pessoa
        public BookVO create(BookVO book){
            var bookEntity = _converter.parse(book);
            bookEntity = _repository.create(bookEntity);

            return _converter.parse(bookEntity);
        }


        //deleta a pessoa pelo id, nao pelo objeto
        public void delete(long id){
            _repository.delete(id);
        }


        //lista todas as pessoas cadastradas
        public List<BookVO> findall(){
            return _converter.parse(_repository.findall());
        }

       
        //encontra uma pessoa especifica pela PK
        public BookVO findbyid(long id){
            return _converter.parse(_repository.findbyid(id)); //retorna um p que tenha id == id recebido
        }


        //atualiza informacoes de uma pessoa, recebe o objeto todo e substitui no bd
        public BookVO update(BookVO book){
            var bookEntity = _converter.parse(book);
            bookEntity = _repository.create(bookEntity);
            return _converter.parse(bookEntity);
        }

    }
}
