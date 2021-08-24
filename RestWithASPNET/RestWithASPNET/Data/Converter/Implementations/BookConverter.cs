using System.Collections.Generic;
using System.Linq;
using RestWithASPNET.Data.Converter.Contract;
using RestWithASPNET.Model;

namespace RestWithASPNET.Data.Converter.Implementations{
    public class BookConverter : Iparser<BookVO, Book>, Iparser<Book, BookVO>
    {
        public Book parse(BookVO o) // vo  -> Book
        {
            if (o == null){
                return null;
            }else{
                return new Book{
                    id = o.id,
                    author = o.author,
                    launch_date = o.launch_date,
                    price = o.price,
                    title = o.title
                };
            }
        }

        public List<Book> parse(List<BookVO> o)
        {
            if (o == null){
                return null;
            }else{
                return o.Select(item => parse(item)).ToList();
            }
        }

        public BookVO parse(Book o) // Book -> vo
        {
            if (o == null){
                return null;
            }else{
                return new BookVO{ //mapeia o objeto
                    id = o.id,
                    author = o.author,
                    launch_date = o.launch_date,
                    price = o.price,
                    title = o.title
                };
            }
        }

        public List<BookVO> parse(List<Book> o)
        {
            if (o == null){
                return null;
            }else{
                return o.Select(item => parse(item)).ToList(); //for each em cada e chama o parse
            }
        }
    }
}