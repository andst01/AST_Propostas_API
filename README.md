Here's the improved `README.md` file, incorporating the new content while maintaining the existing structure and coherence:

# Nome do Projeto

Descrição breve do projeto e seu propósito.

## Especificação técnica

Esta seção descreve a arquitetura, componentes e instruções de execução da solução.

### Visão geral
- **Plataforma**: .NET 8
- **Linguagem**: C# (versão 12)
- **Arquitetura**: aplicação em camadas (API, Application, Domain, Infra/CrossCutting)
- **Banco de dados**: SQL Server (conexão configurada em `appsettings.Development.json`)

### Projetos principais
- `src/Propostas.Api` — API Web (entrada HTTP, controllers, filtros e configuração de middleware)
- `src/Propostas.Application` — Camada de aplicação (serviços, interfaces, ViewModels/DTOs)
- `src/Propostas.Domain` — Entidades, enums e contratos de domínio
- `src/Propostas.Infra.Data` — Implementação de persistência (EF Core, mapeamentos, Repositórios, DbContext)
- `src/Propostas.Infra.CrossCutting` — Configurações transversais (Injeção de dependências, AutoMapper, Swagger, configs)
- `tests/*` — Projetos de teste (testes unitários e de integração)

### Principais dependências e frameworks
- **AutoMapper** — mapeamentos entre Domain, DTOs e ViewModels
- **Entity Framework Core** — acesso a dados e migrations
- **NUnit, Moq, AutoFixture** — bibliotecas usadas nos testes (ver projetos `tests`)
- **Swagger** — documentação da API

### Perfis do AutoMapper
A solução contém profiles para conversões comuns:
- `DomainToViewModelMappingProfile` (mapeia `Proposta` ? `PropostaViewModel`)
- `ViewModelToDomainMappingProfile` (mapeia `PropostaViewModel` ? `Proposta`)
- `DomainToDTOMappingProfile` / `DTOToDomainMappingProfile` — mapeamentos entre Domain ? DTO

Os perfis têm testes automatizados que validam a configuração do AutoMapper.

### Convenções e padrões
- Projetos visam separação de responsabilidades e inversão de dependência (DI)
- Seguir regras definidas em `.editorconfig` e `CONTRIBUTING.md` (formatos e padrões de contribuição)
- **Targets de build**: `.NET 8`

### Configuração local e execução
1. Ajuste a string de conexão em `src/Propostas.Api/appsettings.Development.json` ou use variáveis de ambiente.
2. Por conveniência, a configuração de desenvolvimento aponta para um SQL Server em Docker (`host.docker.internal`).
3. Para aplicar migrations e atualizar o banco execute:
   - `dotnet ef database update` (executar a partir do projeto de migrations ou da solution)
4. Executar a API:
   - Via Visual Studio: executar o projeto `Propostas.Api` (debug/run)
   - Via CLI: `dotnet run --project src/Propostas.Api`

### Testes
- Executar todos os testes:
  - `dotnet test` na raiz da solution
- Os testes cobrem: App services, repositório/DbContext (com provider de teste), configuração do AutoMapper e controllers.

### Observações importantes
- Verifique o arquivo `appsettings.Development.json` antes de rodar localmente; há uma string de conexão de exemplo para desenvolvimento em Docker.
- Os perfis do AutoMapper são validados em tempo de teste usando `AssertConfigurationIsValid()` para evitar mapeamentos faltantes.

---

Se desejar, posso gerar um exemplo de seção com badges (build, coverage) ou um checklist de revisão para o `CONTRIBUTING.md`.

This version maintains the original structure while integrating the new technical specifications, ensuring clarity and coherence throughout the document.