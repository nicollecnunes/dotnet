using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;

namespace RestWithASPNET.Business.Implementations
{
    public class sheetBusinessImplementation : isheetbusiness
    {
        static readonly string[] scopes = {SheetsService.Scope.Spreadsheets};
        static readonly string appname = "teste de planilha"; // qualquer coisa
        static readonly string sheet_id = "1N6lFgvx0GYTf_rj06Ifd3EKSRQTP8Rqdc_o-1crdl0c"; //vem do link
        static readonly string sheet = "Planilha2";
        static SheetsService service;

        public void setting()
        {
            GoogleCredential credential;

            //vai abrir o arquivo baixado
            using(var stream = new FileStream("arquivo.json", FileMode.Open, FileAccess.Read)){
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(scopes);
            }

            Console.WriteLine("Lido.");

            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer(){
                HttpClientInitializer = credential, 
                ApplicationName = appname,
            });

        }

        public void findall()
        {
            setting();
            var range = $"{sheet}!A2:H117";
            var request = service.Spreadsheets.Values.Get(sheet_id, range);

            var response = request.Execute(); //faz a request e recebe o retorno
            var values = response.Values; //pega os valores obtidos

            if (values != null && values.Count>0) //checando se obtivemos valores validos
            {
                foreach (var item in values) //vai percorrer as linhas dos valores
                {
                    Console.WriteLine("Funcionario : {0}\nData Inicial{1}\nData Final{2}\nHoras Totais{3}\nHoras Lancadas{4}\nHoras Faltantes{5}\nE-mail{6}\nTribo{7}\n", item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7]); //[x] = coluna x
                    if(findStatusUser(Convert.ToInt32(item[3]),Convert.ToInt32(item[4]),Convert.ToInt32(item[5])))
                    {
                        Console.WriteLine("O usuário {0} recebeu 8 pontos!", item[0]);
                    }
                }
                
            }
        }

        public void findbyrow(long row)
        {
            
            setting();
            var range = $"{sheet}!{row}:{row}";
            var request = service.Spreadsheets.Values.Get(sheet_id, range);

            var response = request.Execute(); //faz a request e recebe o retorno
            var values = response.Values; //pega os valores obtidos

            if (values != null && values.Count>0) //checando se obtivemos valores validos
            {
                foreach (var item in values) //vai percorrer as linhas dos valores
                {
                    Console.WriteLine("Funcionario : {0}\nData Inicial{1}\nData Final{2}\nHoras Totais{3}\nHoras Lancadas{4}\nHoras Faltantes{5}\nE-mail{6}\nTribo{7}\n", item[0], item[1], item[2], item[3], item[4], item[5], item[6], item[7]); //[x] = coluna x
                    if(findStatusUser(Convert.ToInt32(item[3]),Convert.ToInt32(item[4]),Convert.ToInt32(item[5])))
                    {
                        Console.WriteLine("O usuário {0} recebeu 8 pontos!", item[0]);
                    }
                }
                
            }
        }

        public bool findStatusUser(int HorasTotais, int HorasLancadas, int HorasFaltantes)
        {
            if (HorasFaltantes == 126 || HorasFaltantes == 0){
                return true;
            }
            return false;
        }
    }
}