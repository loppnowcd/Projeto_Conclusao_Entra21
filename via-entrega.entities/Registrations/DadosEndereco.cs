
namespace via_entrega.entities.Registrations
{
    public class DadosEndereco : EntityBase
	{
        public Guid? PessoaId { get; set; }
		public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; } 
        public virtual Pessoa Pessoa { get; set; }
	}
}
