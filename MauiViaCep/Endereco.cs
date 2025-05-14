using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiViaCep
{
    internal class Endereco
    {
        public string Cep { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Unidade { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Estado { get; set; }
        public string Regiao { get; set; }
        public int? Ibge { get; set; }
        public int? Gia { get; set; }
        public int? Ddd { get; set; }
        public int? Siafi { get; set; }
    }
}
