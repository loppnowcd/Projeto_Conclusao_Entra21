// Variáveis globais
let produtos = [];
let produtoEditandoIndex = -1;

// Inicialização
document.addEventListener('DOMContentLoaded', function () {
    // Aplicar máscaras
    aplicarMascaras();

    // Carregar produtos existentes se for edição
    carregarProdutosExistentes();

    // Event listeners
    document.getElementById('entregaForm').addEventListener('submit', handleSubmit);

    // Atualizar visualização inicial
    atualizarVisualizacao();
});

// Carregar produtos existentes (para edição)
function carregarProdutosExistentes() {
    const produtosJson = document.getElementById('produtosJsonField').value;
    if (produtosJson && produtosJson !== '[]') {
        try {
            produtos = JSON.parse(produtosJson);
            atualizarVisualizacao();
        } catch (e) {
            console.error('Erro ao carregar produtos:', e);
            produtos = [];
        }
    }
}

// Aplicar máscaras nos campos
function aplicarMascaras() {
    // Máscara de telefone
    const telefoneInput = document.querySelector('[data-mask="(00) 00000-0000"]');
    if (telefoneInput) {
        telefoneInput.addEventListener('input', function (e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length <= 11) {
                value = value.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
                if (value.length < 14) {
                    value = value.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
                }
            }
            e.target.value = value;
        });
    }

    // Máscara de documento
    const documentoInput = document.querySelector('[data-mask="000.000.000-00"]');
    if (documentoInput) {
        documentoInput.addEventListener('input', function (e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length <= 11) {
                // CPF
                value = value.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
            } else if (value.length <= 14) {
                // CNPJ
                value = value.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/, '$1.$2.$3/$4-$5');
            }
            e.target.value = value;
        });
    }
}

// Toggle formulário de produto
function toggleFormularioProduto() {
    const form = document.getElementById('formularioProduto');
    const isVisible = !form.classList.contains('d-none');

    if (isVisible) {
        form.classList.add('d-none');
        cancelarEdicaoProduto();
    } else {
        form.classList.remove('d-none');
        document.getElementById('produtoDescricao').focus();
    }
}

// Salvar produto
function salvarProduto() {
    if (!validarProduto()) return;

    const produto = obterDadosProduto();

    if (produtoEditandoIndex >= 0) {
        produtos[produtoEditandoIndex] = produto;
        produtoEditandoIndex = -1;
    } else {
        produtos.push(produto);
    }

    limparFormularioProduto();
    toggleFormularioProduto();
    atualizarVisualizacao();
}

// Validar produto
function validarProduto() {
    const campos = [
        { id: 'produtoDescricao', erro: 'erroDescricao', mensagem: 'Descrição é obrigatória' },
        { id: 'produtoAltura', erro: 'erroAltura', mensagem: 'Altura deve ser maior que 0' },
        { id: 'produtoLargura', erro: 'erroLargura', mensagem: 'Largura deve ser maior que 0' },
        { id: 'produtoComprimento', erro: 'erroComprimento', mensagem: 'Comprimento deve ser maior que 0' },
        { id: 'produtoPeso', erro: 'erroPeso', mensagem: 'Peso deve ser maior que 0' },
        { id: 'produtoValor', erro: 'erroValor', mensagem: 'Valor deve ser maior que 0' },
        { id: 'produtoQuantidade', erro: 'erroQuantidade', mensagem: 'Quantidade deve ser maior que 0' }
    ];

    let valido = true;

    // Limpar erros anteriores
    campos.forEach(campo => {
        const elemento = document.getElementById(campo.id);
        const erro = document.getElementById(campo.erro);
        elemento.classList.remove('is-invalid');
        erro.textContent = '';
    });

    // Validar cada campo
    campos.forEach(campo => {
        const elemento = document.getElementById(campo.id);
        const erro = document.getElementById(campo.erro);
        const valor = elemento.value.trim();

        if (!valor || (campo.id !== 'produtoDescricao' && parseFloat(valor) <= 0)) {
            elemento.classList.add('is-invalid');
            erro.textContent = campo.mensagem;
            valido = false;
        }
    });

    // Validações específicas
    const peso = parseFloat(document.getElementById('produtoPeso').value);
    if (peso > 50) {
        const elemento = document.getElementById('produtoPeso');
        const erro = document.getElementById('erroPeso');
        elemento.classList.add('is-invalid');
        erro.textContent = 'Peso não pode exceder 50kg';
        valido = false;
    }

    return valido;
}

// Obter dados do produto
function obterDadosProduto() {
    const imagemFile = document.getElementById('produtoImagem').files[0];

    const produto = {
        descricao: document.getElementById('produtoDescricao').value.trim(),
        altura: parseFloat(document.getElementById('produtoAltura').value),
        largura: parseFloat(document.getElementById('produtoLargura').value),
        comprimento: parseFloat(document.getElementById('produtoComprimento').value),
        peso: parseFloat(document.getElementById('produtoPeso').value),
        valor: parseFloat(document.getElementById('produtoValor').value),
        quantidade: parseInt(document.getElementById('produtoQuantidade').value),
        imagem: null
    };

    // Processar imagem se existir
    if (imagemFile) {
        return new Promise((resolve) => {
            const reader = new FileReader();
            reader.onload = function (e) {
                produto.imagem = e.target.result;
                resolve(produto);
            };
            reader.readAsDataURL(imagemFile);
        });
    }

    return Promise.resolve(produto);
}

// Limpar formulário de produto
function limparFormularioProduto() {
    document.getElementById('produtoDescricao').value = '';
    document.getElementById('produtoAltura').value = '';
    document.getElementById('produtoLargura').value = '';
    document.getElementById('produtoComprimento').value = '';
    document.getElementById('produtoPeso').value = '';
    document.getElementById('produtoValor').value = '';
    document.getElementById('produtoQuantidade').value = '1';
    document.getElementById('produtoImagem').value = '';

    // Limpar erros
    document.querySelectorAll('.is-invalid').forEach(el => el.classList.remove('is-invalid'));
    document.querySelectorAll('.invalid-feedback').forEach(el => el.textContent = '');

    // Resetar botão
    document.getElementById('tituloFormProduto').textContent = 'Adicionar Produto';
    document.getElementById('textoBotaoSalvar').textContent = 'Salvar Produto';
}

// Cancelar edição de produto
function cancelarEdicaoProduto() {
    produtoEditandoIndex = -1;
    limparFormularioProduto();
}

// Editar produto
function editarProduto(index) {
    const produto = produtos[index];
    produtoEditandoIndex = index;

    document.getElementById('produtoDescricao').value = produto.descricao;
    document.getElementById('produtoAltura').value = produto.altura;
    document.getElementById('produtoLargura').value = produto.largura;
    document.getElementById('produtoComprimento').value = produto.comprimento;
    document.getElementById('produtoPeso').value = produto.peso;
    document.getElementById('produtoValor').value = produto.valor;
    document.getElementById('produtoQuantidade').value = produto.quantidade;

    // Atualizar labels
    document.getElementById('tituloFormProduto').textContent = 'Editar Produto';
    document.getElementById('textoBotaoSalvar').textContent = 'Atualizar Produto';

    // Mostrar formulário se estiver oculto
    document.getElementById('formularioProduto').classList.remove('d-none');
    document.getElementById('produtoDescricao').focus();
}

// Excluir produto
function excluirProduto(index) {
    if (confirm('Deseja realmente excluir este produto?')) {
        produtos.splice(index, 1);
        atualizarVisualizacao();
    }
}

// Atualizar visualização
function atualizarVisualizacao() {
    const listaProdutos = document.getElementById('listaProdutos');
    const estadoVazio = document.getElementById('estadoVazio');
    const resumoProdutos = document.getElementById('resumoProdutos');
    const contadorProdutos = document.getElementById('contadorProdutos');

    // Atualizar contador
    contadorProdutos.textContent = `${produtos.length} produto${produtos.length !== 1 ? 's' : ''}`;

    if (produtos.length === 0) {
        listaProdutos.innerHTML = '';
        estadoVazio.classList.remove('d-none');
        resumoProdutos.classList.add('d-none');
    } else {
        estadoVazio.classList.add('d-none');
        resumoProdutos.classList.remove('d-none');

        // Renderizar produtos
        listaProdutos.innerHTML = produtos.map((produto, index) => `
                <div class="card produto-card mb-3 border-0 shadow-sm">
                    <div class="card-body">
                        <div class="row align-items-center">
                            <div class="col-md-1">
                                ${produto.imagem ? `<img src="${produto.imagem}" class="produto-imagem" alt="Produto">` : '<div class="produto-imagem bg-light d-flex align-items-center justify-content-center"><i class="fas fa-box text-muted"></i></div>'}
                            </div>
                            <div class="col-md-7">
                                <h6 class="mb-1">${produto.descricao}</h6>
                                <div class="d-flex flex-wrap gap-1 mb-2">
                                    <span class="badge bg-secondary badge-dimension">${produto.altura}×${produto.largura}×${produto.comprimento} cm</span>
                                    <span class="badge bg-secondary badge-dimension">${produto.peso} kg</span>
                                    <span class="badge bg-secondary badge-dimension">Qtd: ${produto.quantidade}</span>
                                </div>
                                <small class="text-muted">
                                    Volume: ${(produto.altura * produto.largura * produto.comprimento / 1000).toFixed(2)} dm³
                                </small>
                            </div>
                            <div class="col-md-2 text-end">
                                <h6 class="text-success mb-0">R$ ${(produto.valor * produto.quantidade).toFixed(2)}</h6>
                                <small class="text-muted">Unit: R$ ${produto.valor.toFixed(2)}</small>
                            </div>
                            <div class="col-md-2 text-end">
                                <div class="btn-group">
                                    <button type="button" class="btn btn-sm btn-outline-primary btn-action" onclick="editarProduto(${index})" title="Editar">
                                        <i class="fas fa-edit"></i>
                                    </button>
                                    <button type="button" class="btn btn-sm btn-outline-danger btn-action" onclick="excluirProduto(${index})" title="Excluir">
                                        <i class="fas fa-trash"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            `).join('');

        // Atualizar resumo
        atualizarResumo();
    }

    // Atualizar campo hidden
    document.getElementById('produtosJsonField').value = JSON.stringify(produtos);
}

// Atualizar resumo
function atualizarResumo() {
    const totalItens = produtos.reduce((sum, p) => sum + p.quantidade, 0);
    const pesoTotal = produtos.reduce((sum, p) => sum + (p.peso * p.quantidade), 0);
    const valorTotal = produtos.reduce((sum, p) => sum + (p.valor * p.quantidade), 0);
    const volumeTotal = produtos.reduce((sum, p) => sum + (p.altura * p.largura * p.comprimento * p.quantidade), 0);

    document.getElementById('totalItens').textContent = totalItens;
    document.getElementById('pesoTotal').textContent = `${pesoTotal.toFixed(1)} kg`;
    document.getElementById('valorTotal').textContent = `R$ ${valorTotal.toFixed(2)}`;
    document.getElementById('volumeTotal').textContent = `${(volumeTotal / 1000).toFixed(2)} dm³`;
}

// Handle submit do formulário
function handleSubmit(e) {
    e.preventDefault();

    // Validar se tem produtos
    if (produtos.length === 0) {
        alert('É necessário adicionar pelo menos um produto.');
        return;
    }

    // Mostrar loading no botão
    const btnSubmit = document.getElementById('btnSubmit');
    const originalText = btnSubmit.innerHTML;
    btnSubmit.innerHTML = '<i class="fas fa-spinner fa-spin me-1"></i>Salvando...';
    btnSubmit.disabled = true;

    // Preparar dados
    const formData = new FormData(e.target);

    // Fazer submit via AJAX
    fetch(e.target.action, {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.headers.get('content-type')?.includes('application/json')) {
                return response.json();
            } else {
                return response.text();
            }
        })
        .then(data => {
            if (typeof data === 'object' && data.success) {
                // Sucesso
                if (window.entregaSalva) {
                    window.entregaSalva(data.message);
                } else {
                    alert(data.message);
                    window.location.reload();
                }
            } else if (typeof data === 'string') {
                // Retornou HTML (erro de validação)
                document.querySelector('.modal-body').innerHTML = data;
            } else {
                // Erro
                if (window.entregaErro) {
                    window.entregaErro(data.message || 'Erro ao salvar entrega');
                } else {
                    alert(data.message || 'Erro ao salvar entrega');
                }
            }
        })
        .catch(error => {
            console.error('Erro:', error);
            if (window.entregaErro) {
                window.entregaErro('Erro de conexão');
            } else {
                alert('Erro de conexão');
            }
        })
        .finally(() => {
            // Restaurar botão
            btnSubmit.innerHTML = originalText;
            btnSubmit.disabled = false;
        });
}

// Funções auxiliares para processar produtos com imagem
async function salvarProdutoAsync() {
    if (!validarProduto()) return;

    try {
        const produto = await obterDadosProduto();

        if (produtoEditandoIndex >= 0) {
            produtos[produtoEditandoIndex] = produto;
            produtoEditandoIndex = -1;
        } else {
            produtos.push(produto);
        }

        limparFormularioProduto();
        toggleFormularioProduto();
        atualizarVisualizacao();
    } catch (error) {
        console.error('Erro ao processar produto:', error);
        alert('Erro ao processar imagem do produto');
    }
}

// Substituir a função salvarProduto original
function salvarProduto() {
    salvarProdutoAsync();
}
