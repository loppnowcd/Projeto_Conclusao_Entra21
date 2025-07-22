using System.ComponentModel.DataAnnotations;
using ProjetoEntra21.Enums;

namespace ProjetoEntra21.Models
{
    public class PessoaF
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DadosDocumentos Documento { get; set; }
        public DadosEstadoCivil EstadoCivil { get; set; }
        public DadosEndereco Endereco { get; set; }
        public DadosContato Contato { get; set; }
        public string SenhaGovBR { get; set; }
        public bool Status { get; set; }

    }
}