function validate(){
	$('#cadastroForm').validate({
		rules: {
			Email: {
				required: true,
				email: true
			},
			Senha: {
				required: true,
				minlength: 6
			},
			ConfirmarSenha: {
				required: true,
				equalTo: '[name="Senha"]'
			}
		},
		messages: {
			ConfirmarSenha: {
				equalTo: "As senhas não coincidem."
			}
		},
		//submitHandler: function (form) {
		//	debugger;
		//	Swal.fire({
		//		icon: 'success',
		//		title: 'Cadastro realizado!',
		//		confirmButtonColor: '#ff7300'
		//	});
		//	form.submit();
		//}
	});

}