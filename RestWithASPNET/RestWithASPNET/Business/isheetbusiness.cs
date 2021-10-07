using System.Collections.Generic;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Business
{
    public interface isheetbusiness
    {
        void findbyrow(long row);
        void findall();     
        //string getSheetTitle(string id); 
        bool findStatusUser(int HorasTotais, int HorasLancadas, int HorasFaltantes);
    }
}
