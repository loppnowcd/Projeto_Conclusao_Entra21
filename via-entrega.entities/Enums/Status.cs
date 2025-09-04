using System.ComponentModel;

namespace via_entrega.entities.Enums
{
	public  enum Status
	{
		[Description("Pendente")]
		Entregue = 1,
		 Cancelado = 2,
		 Em_transito = 3,
		 Devolvido = 4,
		AguardandoColeta = 5,
	}
}
