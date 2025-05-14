using System.Text.Json.Serialization;
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
            if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8 || (cep) )
            {
                await DisplayAlert("Erro", "Digite um CEP!", "Ok");
                return;
            }
            try
            {
                using var client = new HttpClient();
                string url = $"{VIA_CEP_URL}/{cep}/json";

                var response = await client.GetStringAsync(url);
                if (response.Contains("\"erro\""))
                    resultadoLbl.Text = "CEP não encontrado!";
                    //Console.WriteLine()
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
