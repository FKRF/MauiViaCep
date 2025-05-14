using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MauiViaCep
{
    public partial class MainPage : ContentPage
    {
        private const string VIA_CEP_URL = "http://viacep.com.br/ws/";
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnConsultarClicked(object sender, EventArgs e)
        {
            string cep = cepEntry.Text.Trim().Replace("-", "");
            int n;
            if (string.IsNullOrWhiteSpace(cep) || !Regex.IsMatch(cep, @"\d{8}$") )
            {
                await DisplayAlert("Erro", "Digite um CEP válido!", "Ok");
                return;
            }
            try
            {
                using var client = new HttpClient();
                string url = $"{VIA_CEP_URL}/{cep}/json";

                var response = await client.GetStringAsync(url);
                if (response.Contains("\"erro\""))
                    resultadoLbl.Text = "CEP não encontrado!";
                else
                {
                    var endereco = JsonConvert.DeserializeObject<Endereco>(response);
                    resultadoLbl.Text = $"Logradouro: {endereco.Logradouro}\n"
                                        + $"Numero: {endereco.Unidade}\n"
                                        + $"Bairro: {endereco.Bairro}\n"
                                        + $"Cidade: {endereco.Localidade}\n"
                                        + $"Bairro: {endereco.Uf}\n";
                    
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao consultar cep {cep}: {ex.Message}", "Ok");
            }
        }
    }

}
