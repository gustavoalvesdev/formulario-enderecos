using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serialization.Json;

namespace ApiViaCep2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (mTxtCep.TextLength == 9)
            {
                try
                {
                    RestClient restClient = new RestClient(string.Format("https://viacep.com.br/ws/{0}/json/", mTxtCep.Text));
                    RestRequest restRequest = new RestRequest(Method.GET);

                    IRestResponse restResponse = restClient.Execute(restRequest);

                    if (restResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        MessageBox.Show("Houve um problema com sua requisição: " + restResponse.Content, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        DadosRetorno dadosRetorno = new JsonDeserializer().Deserialize<DadosRetorno>(restResponse);

                        txtCep.Text = dadosRetorno.Cep;
                        txtLogradouro.Text = dadosRetorno.Logradouro;
                        txtComplemento.Text = dadosRetorno.Complemento;
                        txtBairro.Text = dadosRetorno.Bairro;
                        txtLocalidade.Text = dadosRetorno.Localidade;
                        txtUf.Text = dadosRetorno.Uf;
                        txtUnidade.Text = dadosRetorno.Unidade;
                        txtIbge.Text = dadosRetorno.Ibge;
                        txtGia.Text = dadosRetorno.Gia;
                    }
                }
                catch(Exception erro)
                {
                    MessageBox.Show("Erro Geral ao Consultar a API: " + erro.Message, "Erro Geral!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                

            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja sair da aplicação?", "Sair", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }

    class DadosRetorno
    {
        public String Cep { get; set; }
        public String Logradouro { get; set; }
        public String Complemento { get; set; }
        public String Bairro { get; set; }
        public String Localidade { get; set; }
        public String Uf { get; set; }
        public String Unidade { get; set; }
        public String Ibge { get; set; }
        public String Gia { get; set; }
    }
}
