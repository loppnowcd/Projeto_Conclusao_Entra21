function openModal() {
    document.getElementById("entregaModalLabel").innerText = "Nova Entrega";
    loadForm("/Entrega/Criar");
}

function editEntrega(id) {
    document.getElementById("entregaModalLabel").innerText = "Editar Entrega #" + id;
    loadForm("/Entrega/Editar/" + id);
}

function loadForm(url) {
    const modalBody = document.getElementById("modalBodyContent");
    modalBody.innerHTML = '<div class="text-center p-5">Carregando formulário...</div>';
    fetch(url)
        .then(res => res.text())
        .then(html => {
            modalBody.innerHTML = html;
            const modal = new bootstrap.Modal(document.getElementById("entregaModal"));
            modal.show();
        })
        .catch(err => {
            modalBody.innerHTML = `<div class="text-danger">Erro ao carregar formulário: ${err}</div>`;
        });
}

function filterGrid() {
    const search = document.getElementById("searchInput").value.toLowerCase();
    const rows = document.querySelectorAll("#entregasTable tr");

    rows.forEach(row => {
        const cells = row.querySelectorAll("td");
        const match = [...cells].some(td => td.textContent.toLowerCase().includes(search));
        row.style.display = match ? "" : "none";
    });
}


