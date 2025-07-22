using System.ComponentModel.DataAnnotations;
using ProjetoEntra21.Enums;

namespace ProjetoEntra21.Models
{
    public class Veiculo
    {
        public DadosMarcaFipe MarcaFipe { get; set; }
        public DadosModelo Modelo { get; set; }
        [Display(Name = "Ano de Fabricação")]
        public int AnoFabricacao { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }

    }
}
