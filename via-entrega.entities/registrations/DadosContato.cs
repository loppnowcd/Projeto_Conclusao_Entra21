using System.ComponentModel.DataAnnotations;

namespace via_entrega.entities.Registrations
{
    public class DadosContato : EntityBase 
	{
		public string Celular { get; set; }
		public string? Telefone { get; set; }		      
        public string Email { get; set; }
		public Guid? PessoaId { get; set; }
		public virtual Pessoa Pessoa { get; set; }
	}
}
