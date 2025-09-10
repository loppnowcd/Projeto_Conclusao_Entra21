// entregaLista.js - Script para gerenciamento da lista de entregas

class EntregaListaManager {
    constructor() {
        this.entregaModal = null;
        this.detalhesModal = null;
        this.successToast = null;
        this.errorToast = null;
        this.currentEditId = null;

        this.init();
    }

    init() {
        // Aguardar DOM estar pronto
        if (document.readyState === 'loading') {
            document.addEventListener('DOMContentLoaded', () => this.initializeComponents());
        } else {
            this.initializeComponents();
        }
    }

    initializeComponents() {
        // Inicializar modais
        const entregaModalElement = document.getElementById('entregaModal');
        const detalhesModalElement = document.getElementById('detalhesModal');

        if (entregaModalElement) {
            this.entregaModal = new bootstrap.Modal(entregaModalElement);

            // Event listener para limpar modal quando fechar
            entregaModalElement.addEventListener('hidden.bs.modal', () => {
                this.limparModalContent();
            });
        }

        if (detalhesModalElement) {
            this.detalhesModal = new bootstrap.Modal(detalhesModalElement);
        }

        // Inicializar toasts
        const successToastElement = document.getElementById('successToast');
        const errorToastElement = document.getElementById('errorToast');

        if (successToastElement) {
            this.successToast = new bootstrap.Toast(successToastElement);
        }

        if (errorToastElement) {
            this.errorToast = new bootstrap.Toast(errorToastElement);
        }

        // Configurar eventos de busca
        this.setupSearchEvents();
    }

    setupSearchEvents() {
        const searchInput = document.getElementById('searchInput');
        if (searchInput) {
            // Debounce para melhor performance
            let timeoutId;
            searchInput.addEventListener('input', () => {
                clearTimeout(timeoutId);
                timeoutId = setTimeout(() => this.filtrarEntregas(), 300);
            });
        }
    }

    // Função para nova entrega
    novaEntrega() {
        this.currentEditId = null;
        this.atualizarTituloModal('Nova Entrega', 'fas fa-truck');
        this.carregarFormulario('/Entrega/Criar');
    }

    // Função para editar entrega
    editarEntrega(id) {
        this.currentEditId = id;
        this.atualizarTituloModal('Editar Entrega', 'fas fa-edit');
        this.carregarFormulario(`/Entrega/Editar?id=${id}`);
    }

    // Função para ver detalhes
    verDetalhes(id) {
        if (!this.detalhesModal) {
            this.mostrarErro('Modal de detalhes não encontrado');
            return;
        }

        const content = document.getElementById('detalhesModalContent');
        if (content) {
            content.innerHTML = this.getLoadingHtml('Carregando detalhes...');
        }

        this.detalhesModal.show();

        fetch(`/Entrega/Detalhes?id=${id}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.text();
            })
            .then(html => {
                if (content) {
                    content.innerHTML = html;
                }
            })
            .catch(error => {
                console.error('Erro ao carregar detalhes:', error);
                if (content) {
                    content.innerHTML = this.getErrorHtml('Erro ao carregar detalhes da entrega');
                }
                this.mostrarErro('Erro ao carregar detalhes da entrega');
            });
    }

    // Função para excluir entrega
    excluirEntrega(id) {
        // Usar SweetAlert2 se disponível, senão usar confirm nativo
        if (typeof Swal !== 'undefined') {
            Swal.fire({
                title: 'Confirmar Exclusão',
                text: 'Tem certeza que deseja excluir esta entrega? Esta ação não pode ser desfeita.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#dc3545',
                cancelButtonColor: '#6c757d',
                confirmButtonText: 'Sim, excluir',
                cancelButtonText: 'Cancelar',
                reverseButtons: true
            }).then((result) => {
                if (result.isConfirmed) {
                    this.excluirEntregaConfirmado(id);
                }
            });
        } else {
            if (confirm('Tem certeza que deseja excluir esta entrega? Esta ação não pode ser desfeita.')) {
                this.excluirEntregaConfirmado(id);
            }
        }
    }

    // Função para excluir confirmado
    excluirEntregaConfirmado(id) {
        const formData = new FormData();
        formData.append('id', id);

        // Adicionar token anti-forgery se disponível
        const token = document.querySelector('input[name="__RequestVerificationToken"]');
        if (token) {
            formData.append('__RequestVerificationToken', token.value);
        }

        fetch('/Entrega/Excluir', {
            method: 'POST',
            body: formData
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    this.mostrarSucesso(data.message || 'Entrega excluída com sucesso');
                    this.removerLinhaTabela(id);
                    this.atualizarContador();
                } else {
                    this.mostrarErro(data.message || 'Erro ao excluir entrega');
                }
            })
            .catch(error => {
                console.error('Erro ao excluir entrega:', error);
                this.mostrarErro('Erro de conexão ao excluir entrega');
            });
    }

    // Carregar formulário via AJAX
    carregarFormulario(url) {
        if (!this.entregaModal) {
            this.mostrarErro('Modal não encontrado');
            return;
        }

        const content = document.getElementById('modalBodyContent');
        if (content) {
            content.innerHTML = this.getLoadingHtml('Carregando formulário...');
        }

        this.entregaModal.show();

        fetch(url)
            .then(response => {
                if (!response.ok) {
                    throw new Error(`HTTP ${response.status}: ${response.statusText}`);
                }
                return response.text();
            })
            .then(html => {
                if (content) {
                    content.innerHTML = html;
                }
            })
            .catch(error => {
                console.error('Erro ao carregar formulário:', error);
                if (content) {
                    content.innerHTML = this.getErrorHtml('Erro ao carregar formulário');
                }
                this.mostrarErro('Erro ao carregar formulário');
            });
    }

    // Atualizar título do modal
    atualizarTituloModal(titulo, icone = 'fas fa-truck') {
        const modalLabel = document.getElementById('entregaModalLabel');
        if (modalLabel) {
            modalLabel.innerHTML = `<i class="${icone} me-2"></i>${titulo}`;
        }
    }

    // Filtrar entregas
    filtrarEntregas() {
        const searchInput = document.getElementById('searchInput');
        const statusFilter = document.getElementById('filtroStatus');

        if (!searchInput) return;

        const searchTerm = searchInput.value.toLowerCase().trim();
        const statusValue = statusFilter ? statusFilter.value.toLowerCase() : '';

        const rows = document.querySelectorAll('#entregasTable tbody tr');
        let visibleCount = 0;

        rows.forEach(row => {
            // Pular linhas de estado vazio
            if (row.cells.length <= 1) return;

            const destinatario = row.cells[1]?.textContent?.toLowerCase() || '';
            const endereco = row.cells[2]?.textContent?.toLowerCase() || '';

            const matchesSearch = !searchTerm ||
                destinatario.includes(searchTerm) ||
                endereco.includes(searchTerm);

            // Por enquanto, não filtrar por status até implementar
            const matchesStatus = true; // !statusValue || row.dataset.status === statusValue;

            if (matchesSearch && matchesStatus) {
                row.style.display = '';
                visibleCount++;
                // Adicionar animação suave
                row.classList.add('fade-in');
            } else {
                row.style.display = 'none';
                row.classList.remove('fade-in');
            }
        });

        // Atualizar contador
        this.atualizarContadorFiltrado(visibleCount);
    }

    // Limpar filtros
    limparFiltros() {
        const searchInput = document.getElementById('searchInput');
        const statusFilter = document.getElementById('filtroStatus');

        if (searchInput) searchInput.value = '';
        if (statusFilter) statusFilter.value = '';

        this.filtrarEntregas();
    }

    // Remover linha da tabela
    removerLinhaTabela(id) {
        const row = document.querySelector(`tr[data-id="${id}"]`);
        if (row) {
            // Adicionar animação de saída
            row.style.transition = 'opacity 0.3s ease';
            row.style.opacity = '0';

            setTimeout(() => {
                row.remove();
                this.verificarEstadoVazio();
            }, 300);
        }
    }

    // Verificar se a tabela está vazia
    verificarEstadoVazio() {
        const tbody = document.querySelector('#entregasTable tbody');
        const visibleRows = tbody?.querySelectorAll('tr:not([style*="display: none"])');

        if (!visibleRows || visibleRows.length === 0) {
            // Adicionar linha de estado vazio se necessário
            this.adicionarEstadoVazio();
        }
    }

    // Adicionar estado vazio à tabela
    adicionarEstadoVazio() {
        const tbody = document.querySelector('#entregasTable tbody');
        if (!tbody) return;

        const emptyRow = document.createElement('tr');
        emptyRow.innerHTML = `
            <td colspan="7" class="text-center py-5">
                <div class="text-muted">
                    <i class="fas fa-inbox fa-3x mb-3 opacity-50"></i>
                    <h5>Nenhuma entrega encontrada</h5>
                    <p>Clique em "Nova Entrega" para criar sua primeira solicitação</p>
                </div>
            </td>
        `;
        tbody.appendChild(emptyRow);
    }

    // Atualizar contador
    atualizarContador() {
        const rows = document.querySelectorAll('#entregasTable tbody tr:not([style*="display: none"])');
        const count = rows.length;
        this.atualizarContadorDisplay(count);
    }

    // Atualizar contador filtrado
    atualizarContadorFiltrado(count) {
        this.atualizarContadorDisplay(count);
    }

    // Atualizar display do contador
    atualizarContadorDisplay(count) {
        const counter = document.getElementById('totalEntregas');
        if (counter) {
            counter.textContent = count;
        }
    }

    // Limpar conteúdo do modal
    limparModalContent() {
        const content = document.getElementById('modalBodyContent');
        if (content) {
            content.innerHTML = this.getLoadingHtml('Carregando formulário...');
        }
    }

    // Mostrar mensagem de sucesso
    mostrarSucesso(message) {
        if (this.successToast) {
            const messageElement = document.getElementById('successMessage');
            if (messageElement) {
                messageElement.textContent = message;
            }
            this.successToast.show();
        } else {
            // Fallback para alert se toast não estiver disponível
            alert(message);
        }
    }

    // Mostrar mensagem de erro
    mostrarErro(message) {
        if (this.errorToast) {
            const messageElement = document.getElementById('errorMessage');
            if (messageElement) {
                messageElement.textContent = message;
            }
            this.errorToast.show();
        } else {
            // Fallback para alert se toast não estiver disponível
            alert(message);
        }
    }

    // HTML de loading
    getLoadingHtml(message = 'Carregando...') {
        return `
            <div class="text-center py-5">
                <div class="spinner-border text-primary" role="status">
                    <span class="visually-hidden">Carregando...</span>
                </div>
                <p class="mt-2 text-muted">${message}</p>
            </div>
        `;
    }

    // HTML de erro
    getErrorHtml(message = 'Erro ao carregar conteúdo') {
        return `
            <div class="text-center py-5">
                <i class="fas fa-exclamation-triangle fa-3x text-danger mb-3"></i>
                <h5 class="text-danger">${message}</h5>
                <button class="btn btn-outline-secondary mt-2" onclick="location.reload()">
                    <i class="fas fa-redo me-1"></i>Tentar Novamente
                </button>
            </div>
        `;
    }

    // Callback para quando uma entrega é salva com sucesso
    entregaSalva(message) {
        if (this.entregaModal) {
            this.entregaModal.hide();
        }

        this.mostrarSucesso(message);

        // Recarregar página após delay
        setTimeout(() => {
            window.location.reload();
        }, 1500);
    }

    // Callback para erro na entrega
    entregaErro(message) {
        this.mostrarErro(message);
    }
}

// Instanciar manager globalmente
let entregaManager;

// Inicializar quando DOM estiver pronto
document.addEventListener('DOMContentLoaded', function () {
    entregaManager = new EntregaListaManager();

    // Disponibilizar funções globalmente para compatibilidade
    window.novaEntrega = () => entregaManager.novaEntrega();
    window.editarEntrega = (id) => entregaManager.editarEntrega(id);
    window.verDetalhes = (id) => entregaManager.verDetalhes(id);
    window.excluirEntrega = (id) => entregaManager.excluirEntrega(id);
    window.filtrarEntregas = () => entregaManager.filtrarEntregas();
    window.limparFiltros = () => entregaManager.limparFiltros();
    window.entregaSalva = (message) => entregaManager.entregaSalva(message);
    window.entregaErro = (message) => entregaManager.entregaErro(message);
});