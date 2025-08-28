# Desafio iPrazos

Este projeto consiste em um **crawler** desenvolvido em **C#** que busca dados do **Tribunal de Justiça de São Paulo (TJSP)** e os armazena em um banco de dados, facilitando a consulta e análise dessas informações.

```
OBS: Como o site de origem possui proteção por reCAPTCHA e bloqueia tentativas de acesso automatizado, não é possível realizar chamadas diretas via robô ou crawler durante o desenvolvimento.
Dessa forma, para viabilizar os testes e a integração, estou utilizando dados mockados que simulam as respostas esperadas do sistema real. Essa abordagem permite:
	•	Continuar o desenvolvimento sem depender da instabilidade ou restrição do ambiente externo.
	•	Testar regras de negócio e fluxos internos de forma controlada.
	•	Garantir que, quando o acesso legítimo ao sistema for possível (ex.: via credenciais oficiais, API homologada ou liberação de whitelisting), a integração real possa ser validada sem impacto no cronograma.
```

## Funcionalidades

- **Rastreamento de dados**: Coleta informações diretamente do site do TJSP.
- **Armazenamento eficiente**: Salva os dados em um banco de dados relacional.
- **API RESTful**: Disponibiliza os dados coletados por meio de uma API para fácil acesso e integração.

## Tecnologias Utilizadas

- **C# / .NET 6**: Linguagem e framework principais do projeto.
- **Entity Framework Core**: ORM para interação com o banco de dados.
- **SQL Server / Oracle**: Bancos de dados suportados.
- **Docker**: Contêinerização para facilitar o desenvolvimento e implantação.
- **API REST**: Interface para consumo dos dados coletados.

## Como Executar o Projeto

### 1. Clonar o Repositório

```bash
git clone https://github.com/Vinicius19910101/Desafio_iPrazos.git
cd Desafio_iPrazos
```

### 2. Configurar o Banco de Dados

- **SQL Server**: Utilize o Docker para rodar o SQL Server localmente.

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=SenhaForte123" -p 1433:1433 --name sql1 -d mcr.microsoft.com/mssql/server:2025-latest
```

-Criando o banco via **migrations** 

```bash
cd "API Rest"
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 3. Configurar a String de Conexão

No arquivo `appsettings.json`, configure a string de conexão para o banco de dados escolhido:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SeuBanco;User Id=sa;Password=SenhaForte123;"
  }
}
```

### 4. Executar o Projeto

- **API REST**:

```bash
dotnet run --project ApiRest/ApiRest.csproj
```

- **Aplicação Console (Crawler) - executar no diretório onde está o arquivo TJSPCrawler.csproj**:

```bash
dotnet run
```

## Contribuições

Contribuições são bem-vindas! Para sugerir melhorias ou relatar problemas, por favor, abra uma **issue** ou envie um **pull request**.

## Licença

Este projeto está licenciado sob a licença **MIT**. Consulte o arquivo [LICENSE](LICENSE) para mais detalhes.

