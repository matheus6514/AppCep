using AppCep.Sevico;
using AppCep.Sevico.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppCep
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ImgLogo.Source = ImageSource.FromResource("AppCEP.Img.Logo.png");
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            //retiro os espaços vazios
            String cep = txtCEP.Text.Trim();

            //Chama o método para validar o cep se o retorno igual a TRUE faz a busca no WebService
            if (isValidCEP(cep))
            {
                try
                {
                    /*faz a busca no webservice e retorna os dados do CEP
                     para o obejto end*/
                    Endereco end = ViaCEPServico.BuscarEnderecoViaCEP(cep);
                    //teste para ver se o end não é null (não achou o endereço do cep)
                    if (end != null)
                    {
                        RESULTADO.Text = String.Format("Endereço: {2} \nBairro: {3} \nCidade: {0}-{1} ", end.localidade, end.uf, end.logradouro, end.bairro);
                    }
                    else
                    {
                        DisplayAlert("ERRO", "O endereço não foi encontrado para o CEP informado: " + cep, "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("ERRO CRÍTICO", ex.Message, "OK");
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            //variavel de controle de validação
            bool valido = true;
            //verifica se não possui 8 caracteres
            if (cep.Length != 8)
            {
                DisplayAlert("ERRO", "CEP inválido! o CEP deve conter 8 caracteres", "OK");

                valido = false;
            }
            //verifica se possui caracteres
            int NovoCEP = 0;
            if (int.TryParse(cep, out NovoCEP))
            {
                DisplayAlert("ERRO", "CEP inválido ! O CEP deve ser composto apenas por números.", "OK");

                valido = false;
            }
            return valido;
        }

    }
}
