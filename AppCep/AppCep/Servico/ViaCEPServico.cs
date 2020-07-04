using AppCep.Sevico.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AppCep.Sevico
{
    public class ViaCEPServico
    {
        //string com o endereço do serviço WEB
        private static string EnderecoURL = "https://viacep.com.br/ws/{0}/json/";

        public static Endereco BuscarEnderecoViaCEP(string cep)
        {
            //propriedade statica que recebe o endereço
            string NovoEnderecoURL = string.Format(EnderecoURL, cep);
            //objeto que acessa o resultado do endereço web passado como parâmetro
            WebClient wc = new WebClient();
            string Conteudo = wc.DownloadString(NovoEnderecoURL);
            //objeto do tipo da Classe endereço que recebe os dados des-serializados
            Endereco end = JsonConvert.DeserializeObject<Endereco>(Conteudo);
            //caso haja retorno do serviço WEB
            if (end.cep == null) return null;

            return end;
        }
    }
}
