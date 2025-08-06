using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities.Registrations
{
    public class Veiculo : EntityBase
	{   
        
        public string MarcaFipe { get; set; }
        public string Modelo { get; set; }        
        public int AnoFabricacao { get; set; }
        public string Cor { get; set; }
        public string Placa { get; set; }
        public virtual ICollection<PessoaFisica> Pessoas { get; set; }
	}
}
