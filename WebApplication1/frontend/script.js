const apiUrl = "http://localhost:5032/api/produto"; // ajuste conforme sua API

const produtoForm = document.getElementById("produtoForm");
const produtosTableBody = document.querySelector("#produtosTable tbody");
const cancelEditButton = document.getElementById("cancelEdit");

let editMode = false;
let editId = null;

// Função para listar produtos
async function listarProdutos() {
    const res = await fetch(apiUrl);
    const produtos = await res.json();

    produtosTableBody.innerHTML = "";

    produtos.forEach(produto => {
        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td data-label="Foto"><img class="produto-foto" src="${produto.fotoUrl || 'https://via.placeholder.com/60'}" alt="${produto.nome}"></td>
            <td data-label="Nome">${produto.nome}</td>
            <td data-label="Descrição">${produto.descricao || '-'}</td>
            <td data-label="Quantidade">${produto.quantidade}</td>
            <td data-label="Estoque Mínimo">${produto.estoqueMinimo}</td>
            <td data-label="Ações" class="action-buttons">
                <button onclick="editarProduto(${produto.id})">Editar</button>
                <button onclick="excluirProduto(${produto.id})">Excluir</button>
                <button onclick="entradaEstoque(${produto.id})">+ Estoque</button>
                <button onclick="saidaEstoque(${produto.id})">- Estoque</button>
            </td>
        `;

        produtosTableBody.appendChild(tr);
    });
}

// Função para salvar produto (POST ou PUT)
produtoForm.addEventListener("submit", async (e) => {
    e.preventDefault();

    const formData = new FormData();
    formData.append("Nome", document.getElementById("nome").value);
    formData.append("Descricao", document.getElementById("descricao").value);
    formData.append("Quantidade", document.getElementById("quantidade").value);
    formData.append("EstoqueMinimo", document.getElementById("estoqueMinimo").value);

    const fotoFile = document.getElementById("foto").files[0];
    if (fotoFile) formData.append("foto", fotoFile);

    if (editMode) {
        const res = await fetch(`${apiUrl}/${editId}`, {
            method: "PUT",
            body: formData
        });
        if (!res.ok) alert("Erro ao atualizar produto!");
    } else {
        const res = await fetch(apiUrl, {
            method: "POST",
            body: formData
        });
        if (!res.ok) alert("Erro ao criar produto!");
    }

    produtoForm.reset();
    editMode = false;
    editId = null;
    listarProdutos();
});

// Cancelar edição
cancelEditButton.addEventListener("click", () => {
    produtoForm.reset();
    editMode = false;
    editId = null;
});

// Editar produto
async function editarProduto(id) {
    const res = await fetch(`${apiUrl}/${id}`);
    const produto = await res.json();

    document.getElementById("produtoId").value = produto.id;
    document.getElementById("nome").value = produto.nome;
    document.getElementById("descricao").value = produto.descricao;
    document.getElementById("quantidade").value = produto.quantidade;
    document.getElementById("estoqueMinimo").value = produto.estoqueMinimo;

    editMode = true;
    editId = id;
}

// Excluir produto
async function excluirProduto(id) {
    if (confirm("Deseja realmente excluir este produto?")) {
        await fetch(`${apiUrl}/${id}`, { method: "DELETE" });
        listarProdutos();
    }
}

// Entrada de estoque
async function entradaEstoque(id) {
    const quantidade = prompt("Quantidade a adicionar:");
    if (!quantidade) return;

    await fetch(`${apiUrl}/${id}/entrada?quantidade=${quantidade}`, { method: "POST" });
    listarProdutos();
}

// Saída de estoque
async function saidaEstoque(id) {
    const quantidade = prompt("Quantidade a remover:");
    if (!quantidade) return;

    const res = await fetch(`${apiUrl}/${id}/saida?quantidade=${quantidade}`, { method: "POST" });
    if (!res.ok) {
        const msg = await res.text();
        alert(msg);
    }
    listarProdutos();
}

// Inicializa a lista
listarProdutos();
