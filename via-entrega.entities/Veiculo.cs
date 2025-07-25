using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities
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
