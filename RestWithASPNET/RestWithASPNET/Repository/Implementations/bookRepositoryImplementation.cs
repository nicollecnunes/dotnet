using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RestWithASPNET.Repository.Implementations{
    public class bookRepositoryImplementation : ibookrepository{
        private MySQLContext _context; 

        // construtor. recebe injecao do context
        public bookRepositoryImplementation(MySQLContext context){ 
            _context = context;

        }


        //metodo create. recebe o objeto pessoa
        public Book create(Book book){
            try {
                _context.Add(book);
                _context.SaveChanges();

            }catch(Exception){
                throw;
            }
            return book;
        }


        //deleta a pessoa pelo id, nao pelo objeto
        public void delete(long id){
            var result = _context.Book.SingleOrDefault(p => p.id.Equals(id));
            if (result != null){
                try{
                    _context.Book.Remove(result);
                    _context.SaveChanges();

                }catch (Exception){
                    throw;
                }
            }
        }


        //lista todas as pessoas cadastradas
        public List<Book> findall(){
            return _context.Book.ToList();
        }

       
        //encontra uma pessoa especifica pela PK
        public Book findbyid(long id){
            return _context.Book.SingleOrDefault(p => p.id.Equals(id)); //retorna um p que tenha id == id recebido
        }


        //atualiza informacoes de uma pessoa, recebe o objeto todo e substitui no bd
        public Book update(Book book){
            if (!exists(book.id)){
                return null; //se nao existe, q pena
            }
            var result = _context.Book.SingleOrDefault(p => p.id.Equals(book.id));
            if (result != null){
                try{
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();

                }catch (Exception){
                    throw;
                }
            } 
            return book; 
        }


        //verifica se uma pessoa existe pelo seu id
        public bool exists(long id){
            return _context.Book.Any(p => p.id.Equals(id));
        }
    }
}
