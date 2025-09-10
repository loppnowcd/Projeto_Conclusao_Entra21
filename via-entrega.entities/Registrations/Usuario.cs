namespace via_entrega.entities.Registrations
{
    public class Usuario : EntityBase
    {
        public string Senha { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public Guid? PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

    }
}