# 📦 Sistema de Gestão de Estoque (Full Stack)

Um sistema para gerenciamento de produtos e controle de fluxo de estoque, desenvolvido com arquitetura **REST API** no back-end e interface web responsiva.

![Status do Projeto](https://img.shields.io/badge/Status-Concluído-brightgreen)
![Tech](https://img.shields.io/badge/.NET-6.0%2F8.0-purple)
![Tech](https://img.shields.io/badge/C%23-Advanced-blue)
![Tech](https://img.shields.io/badge/MySQL-Database-orange)


## 🚀 Sobre o Projeto

Este projeto foi desenvolvido para atender uma demanda real da Baademaq Assistência Técnica, visando solucionar problemas com o controle de estoque.

Diferente de CRUDS simples, este sistema implementa **Regras de Negócio** para garantir a integridade do estoque (ex: validação de saldo negativo) e manipulação de arquivos estáticos.

## 🛠 Tecnologias Utilizadas

**Back-end (API):**
* **C# / .NET Core:** Desenvolvimento da API RESTful.
* **Entity Framework Core:** ORM para manipulação do banco de dados.
* **MySQL:** Persistência dos dados.
* **Swagger:** Documentação automática dos Endpoints.
* **System.IO:** Manipulação e armazenamento de imagens (Upload).

**Front-end (Client):**
* **HTML5 & CSS3:** Estrutura e estilização responsiva.
* **JavaScript:** Consumo assíncrono da API (Fetch API) e manipulação dinâmica do DOM.

## ✨ Funcionalidades Principais

1.  **CRUD Completo de Produtos:** Criação, Leitura, Atualização e Remoção.
2.  **Upload de Imagens:** Gerenciamento de arquivos enviados pelo usuário (`IFormFile`), salvamento em diretório estático (`wwwroot`) e referência no banco de dados.
3.  **Controle Transacional de Estoque:**
    * Endpoints específicos para `Entrada` e `Saída` de mercadorias.
    * Validação de regras de negócio (impede saída se o estoque for insuficiente).
4.  **Feedback Visual:** Interface reativa que atualiza a lista de produtos sem necessidade de recarregar a página.

## ⚙️ Como Executar o Projeto

### Pré-requisitos
* .NET SDK Instalado
* MySQL Server rodando
* Visual Studio ou VS Code

### Passo a Passo

1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/brulindner/NOME-DO-SEU-REPO.git](https://github.com/brulindner/NOME-DO-SEU-REPO.git)
    ```

2.  **Configuração do Banco de Dados:**
    * No arquivo `appsettings.json`, configure sua string de conexão com o MySQL.
    * Execute as Migrations para criar o banco:
    ```bash
    dotnet ef database update
    ```

3.  **Executar a API:**
    * Abra a solução no Visual Studio e execute (F5).
    * Anote a porta onde a API está rodando (ex: `localhost:7123`).

4.  **Executar o Front-end:**
    * Abra o arquivo `script.js` e atualize a variável `const apiUrl` com a porta correta da sua API.
    * Abra o `index.html` no seu navegador.

## 👩‍💻 Autora

**Bruna Laís Lindner**
* [LinkedIn](https://www.linkedin.com/in/brulindner/)
* Analista Comercial em transição para Engenharia de Software.
