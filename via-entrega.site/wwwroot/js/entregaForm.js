let productList = [];
let editIndex = -1;

function toggleProducts() {
    document.getElementById("productList").classList.toggle("d-none");
}

function updateProductCount() {
    document.getElementById("productCount").textContent = productList.length;
}

function getProductInputs() {
    return {
        descricao: document.getElementById("descricao").value.trim(),
        altura: document.getElementById("altura").value.trim(),
        largura: document.getElementById("largura").value.trim(),
        comprimento: document.getElementById("comprimento").value.trim(),
        peso: document.getElementById("peso").value.trim(),
        valor: document.getElementById("valor").value.trim(),
        quantidade: document.getElementById("quantidade").value.trim(),
        imagem: document.getElementById("imagem").files[0]
    };
}

function clearProductInputs() {
    document.querySelectorAll('#productForm input').forEach(input => input.value = "");
}

function buildProductCard(p, index) {
    return `
				<div class="col-md-4 mb-3">
					<div class="card product-card">
						<div class="card-body">
							<h6>${p.descricao} (x${p.quantidade})</h6>
							<p>Dimensões: ${p.altura}x${p.largura}x${p.comprimento} cm</p>
							<p>Peso: ${p.peso} kg</p>
							<p>Valor: R$ ${p.valor}</p>
							${p.imagem ? `<img src="${p.imagem}" class="img-thumbnail mt-2" />` : ""}
							<div class="mt-3 d-flex gap-2">
								<button class="btn btn-sm btn-outline-primary" type="button" onclick="editProduct(${index})">Editar</button>
								<button class="btn btn-sm btn-outline-danger" type="button" onclick="deleteProduct(${index})">Excluir</button>
							</div>
						</div>
					</div>
				</div>`;
}

function renderProducts() {
    const container = document.getElementById("productList");
    container.innerHTML = "";
    productList.forEach((p, index) => {
        container.innerHTML += buildProductCard(p, index);
    });
    updateProductCount();
}

function handleProductSubmit() {

    debugger;

    const inputs = getProductInputs();
    const required = [inputs.descricao, inputs.altura, inputs.largura, inputs.comprimento, inputs.peso, inputs.valor, inputs.quantidade];

    if (required.some(v => !v || parseFloat(v) <= 0)) {
        Swal.fire({ icon: "warning", title: "Campos inválidos", text: "Preencha todos os campos corretamente!" });
        return;
    }

    const reader = new FileReader();
    reader.onload = function (e) {
        const produto = {
            ...inputs,
            imagem: e.target.result
        };

        if (editIndex === -1) {
            productList.push(produto);
        } else {
            productList[editIndex] = produto;
            editIndex = -1;
            document.getElementById("btnAddProduct").textContent = "Adicionar Produto";
        }

        renderProducts();
        clearProductInputs();
    };

    if (inputs.imagem) {
        reader.readAsDataURL(inputs.imagem);
    } else {
        reader.onload({ target: { result: "" } });
    }
}

function editProduct(index) {
    const p = productList[index];
    document.getElementById("descricao").value = p.descricao;
    document.getElementById("altura").value = p.altura;
    document.getElementById("largura").value = p.largura;
    document.getElementById("comprimento").value = p.comprimento;
    document.getElementById("peso").value = p.peso;
    document.getElementById("valor").value = p.valor;
    document.getElementById("quantidade").value = p.quantidade;
    document.getElementById("imagem").value = "";
    editIndex = index;
    document.getElementById("btnAddProduct").textContent = "Salvar Edição";
}

function deleteProduct(index) {
    Swal.fire({
        title: 'Tem certeza?',
        text: "Este produto será removido.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#ff7300',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sim, excluir',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            productList.splice(index, 1);
            renderProducts();
            Swal.fire({ icon: 'success', title: 'Produto excluído!', confirmButtonColor: '#ff7300' });
        }
    });
}

document.getElementById("btnAddProduct").addEventListener("click", handleProductSubmit);

document.getElementById("deliveryForm").addEventListener("submit", function (e) {
    e.preventDefault();
    const form = e.target;

    // Adiciona produtos no campo hidden
    document.getElementById("ProdutosJson").value = JSON.stringify(productList);

    const formData = new FormData(form);

    fetch(form.action, {
        method: "POST",
        body: formData
    })
        .then(res => res.json())
        .then(data => {
            if (data.success) {
                location.reload(); // Recarrega a listagem
            }
        })
        .catch(err => console.error("Erro:", err));
});

