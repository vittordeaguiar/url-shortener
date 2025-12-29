> **[🇺🇸 Read in English](README.en.md)**

# Encurtador de URLs

Esse é um projeto que eu criei pra estudar e colocar em prática algumas coisas do ecossistema .NET.

## O que tem aqui dentro?

Aproveitei esse projeto pra mexer com umas tecnologias atuais e reforçar alguns conceitos de arquitetura:

*   **.NET 10**: Nova versão do .NET
*   **Minimal APIs**: Sem Controllers. Usei os endpoints direto no builder, que deixa o código mais limpo e rápido.
*   **Clean Architecture (Simplificada)**: Separei o projeto em diretórios lógicos pra não misturar as coisas:
    *   `Domain`: Entidades e interfaces
    *   `Application`: Regra de encurtamento
    *   `Infrastructure`: Conexão com DB
    *   `Endpoints`: Onde as rotas da API estão definidas
*   **EF Core + PostgreSQL**
*   **Docker & Docker Compose**

## Como rodar

A forma mais fácil de rodar isso aqui é usando o Docker, porque ele já sobe o banco de dados e a aplicação prontos.

1.  Certifique-se de ter o **Docker** e o **Docker Compose** instalados.
2.  Clone o repositório e entre na pasta raiz.
3.  Rode o comando:

```bash
docker-compose up --build
```

Isso vai subir a API e o Postgres. O sistema já está configurado pra rodar as migrações do banco automaticamente assim que a aplicação inicia, então não precisa se preocupar em criar tabelas na mão.

Se quiser rodar localmente sem Docker, vai precisar ter um Postgres rodando e ajustar a `ConnectionStrings` no `appsettings.Development.json` antes de dar um `dotnet run`.

### Encurtar uma URL

Realize um POST para `/shorten` com um JSON no corpo:

```json
{
  "url": "https://www.google.com/search?q=dotnet+10"
}
```

A resposta vai ser algo tipo:

```json
{
  "code": "Xy7z9A",
  "shortUrl": "http://localhost:5000/Xy7z9A"
}
```

### Acessar a URL

É só colar a `shortUrl` no navegador ou fazer um GET para `/{code}` (ex: `http://localhost:5000/Xy7z9A`). A API vai te redirecionar pro link original.
