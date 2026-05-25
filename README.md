

# 💳 DebtQuerySystem

Sistema robusto e escalável para consulta de débitos e parcelamentos de clientes. O ecossistema é composto por um front-end moderno em Angular 21, um back-end resiliente em .NET 10 utilizando Domain-Driven Design (DDD) e um fluxo automatizado de carga de dados via planilhas.

---

## 🏗️ Arquitetura do Ecossistema Local

O projeto foi totalmente containerizado seguindo os princípios do *Twelve-Factor App*, isolando as responsabilidades e garantindo que o ambiente local suba de forma idêntica ao comportamento em nuvem (Azure).

> 🗄️ **1. DATABASE** *(Postgres)*
> └─ Aguarda o script de `init-scripts` e o `healthcheck` confirmar que o banco aceita conexões.
>
> 🔻 *(Status: service_healthy)*
>
> 🚀 **2. DATA SEEDER** *(DebtQuerySystem.DataSeeder)*
> └─ Job Efêmero: Sobe isolado, lê a planilha Excel via ClosedXML, popula o banco e encerra com sucesso.
>
> 🔻 *(Status: service_completed_successfully)*
>
> 💻 **3. ECOSSISTEMA ONLINE** *(API .NET 10, UI Angular e Keycloak)*
> └─ Os serviços iniciam em sincronia com a base de dados 100% estruturada e carregada.

## 💎 Diferenciais Técnicos Implementados

Abaixo estão os principais pilares arquiteturais que elevam a maturidade técnica, a performance e a segurança deste ecossistema:

### 🐳 Arquitetura Multi-Stage Build & Imagens Imutáveis
* **Containers Otimizados:** Tanto a API .NET quanto o Frontend Angular utilizam Dockerfiles estruturados em múltiplos estágios (*multi-stage build*). O SDK do .NET e o ambiente do Node.js são isolados estritamente na etapa de compilação.
* **Segurança e Leveza em Produção:** As imagens finais de runtime utilizam bases extremamente leves (`aspnet:10.0` e `nginx:alpine`), reduzindo drasticamente a superfície de ataque e o tamanho dos artefatos.

### ⚙️ Injeção Dinâmica de Variáveis no Frontend (Runtime Config)
* **Desacoplamento do Build:** A aplicação Angular foi totalmente desvinculada dos arquivos estáticos `environment.ts` (padrão de *compile-time*). 
* **O Motor do `envsubst`:** Ao iniciar o container, o Nginx intercepta as variáveis de ambiente reais do Docker e, através do utilitário `envsubst`, as injeta dinamicamente em um arquivo `config.json`. Isso permite que **a mesma imagem Docker imutável** seja promovida por múltiplos ambientes (Dev, Homolog, Prod) sem necessidade de recompilação do código fonte.

### 🚀 Carga de Dados Resiliente (Short-lived Seeder Job)
* **Isolamento de Responsabilidade:** O processo de leitura e carga da planilha matriz (`BASE CLIENTES_DIVIDAS.xlsx`) não está acoplado à API principal. Ele roda como um *Short-lived Container* (Job Efêmero) via Console Application.
* **Compatibilidade Linux (ClosedXML):** O container do seeder foi blindado com suporte a bibliotecas gráficas nativas (`libgdiplus` e `libc6-dev`), garantindo que o processamento de fontes, estilos e moedas do Excel funcione perfeitamente dentro do ecossistema Linux do Docker.
* **Ciclo de Vida Curto:** Assim que os dados são processados e validados contra o banco de dados, o container encerra sua execução com sucesso (`code 0`) e libera 100% dos recursos de memória da máquina.

### 🔐 Centralização de Identidade com Keycloak SSO (OIDC)
* **Blindagem de Segurança e Identidade:** A integração do **Keycloak** como provedor de identidade centralizado eleva o nível de segurança da aplicação para o padrão corporativo, isolando e blindando o ecossistema contra vulnerabilidades comuns de autenticação.
* **Controle de Acesso Baseado em Papéis (RBAC):** Toda a segurança foi desenhada utilizando o protocolo OpenID Connect (OIDC). O backend valida os tokens JWT localmente de forma rápida, e o frontend protege as rotas reativamente com base nas *roles* de perfil do usuário.
* **Experiência Corporativa:** Inclusão do tema de login `keywind` customizado, garantindo uma interface profissional de autenticação unificada.

### ⚡ Performance, Cache Distribuído e DDD
* **Domínio Rico:** O core do sistema (`DebtQuerySystem.Domain`) adota o padrão Domain-Driven Design, isolando as regras de negócio de clientes, dívidas e parcelas em entidades ricas e protegidas por validações, livres de acoplamento com frameworks.
* **Estratégia de Cache:** Implementação de uma camada de cache distribuído reusável (`DistributedCacheService`), blindando o banco de dados contra consultas repetitivas de payloads idênticos.
* **Documentação Moderna com Scalar:** Substituição da interface legada do Swagger pelo **Scalar**, oferecendo um portal de testes de API moderno, interativo e fluido.

---

## 🛠️ Tecnologias Utilizadas

### Frontend
* **Angular 21** (Componentes autônomos, fluxo de controle nativo `@if`/`@for`)
* **Bootstrap 5** (Interface responsiva e limpa)
* **Nginx Alpine** (Servidor web leve para produção)

### Backend
* **.NET 10 Web API** (C#)
* **Domain-Driven Design (DDD)**
* **ClosedXML** (Processamento de planilhas de alta performance)
* **PostgreSQL** (Persistência de dados)
* **Redis** (Cache distribuido)

---

## 📁 Estrutura do Repositório

```text
├── 📄 .gitignore
├── 📄 README.md                          # Documentação principal do projeto
│
├── 📁 .github/
│   ├── 📄 pull_request_template.md       # Modelo de padronização para PRs
│   └── 📁 workflows/                     # Pipelines de CI/CD (GitHub Actions)
│
├── 📁 backend/
│   └── 📁 DebtQuerySystem/
│       ├── 📄 DebtQuerySystem.slnx       # Nova estrutura de solução simplificada do .NET
│       │
│       ├── 📁 DebtQuerySystem.Api/       # Camada de Apresentação (Minimal APIs)
│       │   ├── 📄 appsettings.json.example
│       │   ├── 📄 Dockerfile              # Multi-stage build (ASP.NET Runtime)
│       │   ├── 📄 Program.cs             # Bootstrapper da API e pipeline HTTP
│       │   ├── 📁 Configuration/         # Classes de setup (Auth, CORS, Scalar, Serilog)
│       │   ├── 📁 Endpoints/             # Mapeamento de rotas desacopladas (Dividas)
│       │
│       ├── 📁 DebtQuerySystem.Application/ # Camada de Aplicação (Regras de Caso de Uso)
│       │   ├── 📁 Queries/               # Handlers de consulta isolados por CPF
│       │   └── 📁 Transformers/          # Mappers de performance para DTOs
│       │
│       ├── 📁 DebtQuerySystem.DataSeeder/ # Console Application (Job de Carga Efêmero)
│       │   ├── 📄 appsettings.json.example
│       │   ├── 📄 Dockerfile              # Container com suporte gráfico (libgdiplus)
│       │   ├── 📄 Program.cs             # Ponto de entrada do executável do Seeder
│       │   ├── 📁 Builder/               # Fábrica de objetos de Domínio
│       │   ├── 📁 Data/Seed/             # Origem da planilha BASE CLIENTES_DIVIDAS.xlsx
│       │   ├── 📁 Reader/                # Motor de parsing do Excel (ClosedXML)
│       │   └── 📁 Runner/                # Orquestrador da carga de dados
│       │
│       ├── 📁 DebtQuerySystem.Domain/    # Núcleo da Aplicação (Enterprise Business Rules)
│       │   ├── 📁 Entities/              # Entidades Ricas (Cliente, Divida, Parcela)
│       │   └── 📁 Interfaces/            # Abstrações de I/O (Contratos de Repositories)
│       │
│       ├── 📁 DebtQuerySystem.Infrastructure/ # Camada de Dados, Cache e outras configurações
│       │   ├── 📄 InfrastructureDependencyInjection.cs # Registro de IoC da Infra
│       │   ├── 📁 Database/              # DbContext e Repositories (EF Core)
│       │   ├── 📁 Migrations/            # Versionamento estrutural do Banco de Dados
│       │   ├── 📁 Services/              # Cache Distribuído (Redis)
│       │   └── 📁 Settings/              # Strongly-typed settings (Records de config)
│       │
│       └── 📁 DebtQuerySystem.Tests/     # Camada de Testes de Unidade
│           └── 📁 Domain/                # Validações das regras de negócio (Parcela)
│
├── 📁 docker/                            # Infraestrutura como Código (IaC Local)
│   ├── 📄 docker-compose.yml             # Orquestração de todo o ambiente local
│   ├── 📁 init-scripts/                  # Scripts SQL executados na criação do banco
│   └── 📁 keycloak/                      # Dump do Realm (demo-realm) e Temas (keywind)
│
└── 📁 frontend/
    └── 📁 debt-query-ui/                 # Aplicação SPA Angular 21
        ├── 📄 .editorconfig
        ├── 📄 .gitignore
        ├── 📄 .prettierrc
        ├── 📄 angular.json
        ├── 📄 Dockerfile                 # Multi-stage build com Nginx + envsubst
        ├── 📄 nginx.conf                 # Configuração do proxy reverso com fallback SPA
        ├── 📄 package.json
        ├── 📄 tsconfig.app.json
        ├── 📄 tsconfig.json
        ├── 📄 tsconfig.spec.json
        │
        ├── 📁 .vscode/                   # Configurações de workspace do editor
        ├── 📁 public/                    # Arquivos públicos e templates de configuração
        │   ├── 📄 favicon.ico
        │   └── 📁 assets/                # Configurações dinâmicas e Silent Check do SSO
        │
        └── 📁 src/                       # Código-fonte do Frontend
            ├── 📄 index.html
            ├── 📄 main.ts
            ├── 📄 styles.scss
            └── 📁 app/                   # Módulos e Componentes da Aplicação
                ├── 📄 app.config.ts      # Configurações globais do Angular Core
                ├── 📄 app.routes.ts      # Matriz de rotas da aplicação
                ├── 📄 app.ts             # Componente raiz (Shell)
                ├── 📄 keycloak.config.ts # Inicializador do fluxo OIDC
                ├── 📁 core/              # Config Service dinâmico e Guardas de Roles
                ├── 📁 features/debitos/  # Fluxo de negócio (Consulta e Detalhes)
                ├── 📁 home/              # Home reformulada com Helpers de Dev
                ├── 📁 pages/             # Páginas globais (404 Not Found, 403 Forbidden)
                └── 📁 shared/components/ # Componentes reutilizáveis (Layout, Spinners)
```

🏁 Como Rodar o Projeto Localmente
Pró-requisitos
Docker e Docker Compose instalados.

A planilha `BASE CLIENTES_DIVIDAS.xlsx` deve estar presente no diretório `DebtQuerySystem.DataSeeder/Data/`.

Passo a Passo
Clone o repositório:

```bash
git clone https://github.com/dantecvip/debt-query-system.git
cd debt-query-system
```

Suba todo o ecossistema com um único comando:

```bash
cd docker
docker compose up -d --build
```

O que vai acontecer no seu terminal?

O container do banco de dados será iniciado.

O Docker Compose aguardará o `healthcheck` do banco confirmar que ele está aceitando conexões.

O container `debt_query_seeder` subirá, processará as linhas da planilha Excel e fará a carga na base.

Assim que o seeder fechar com `code 0`, a API e o Frontend ficarão disponíveis.

URLs de Acesso Local
Frontend (Angular): `http://localhost:4200`

Backend API (Scalar): `http://localhost:4555/scalar/v1` (ou caminho correspondente)

🔒 Segurança & Autenticação
A aplicação está preparada para integração corporativa de identidade utilizando Keycloak.
As rotas do backend são protegidas por tokens JWT emitidos pelo Keycloak, e o frontend realiza o fluxo de login via SSO.

📝 Padrões de Código e Git
Este projeto adota convenções rigorosas de mercado para manter a saúde e rastreabilidade do histórico:

Conventional Commits: Mensagens de commit padronizadas (ex: `feat(debitos): ..., refactor(debitos): ..., chore(infra): ...`).

Git Flow simplificado: Desenvolvimento baseado em branches de features com validação obrigatória via Pull Request.

