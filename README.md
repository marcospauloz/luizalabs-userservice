# Bem-vindo ao LuizaLabs User Service!

Um projeto simples em .Net 8 como um serviço de cadastro e login de usuário para a entrevista na Luizalabs

## Tecnologias utilizadas:
- **RESTfull asyncronous** back-end;
 - Dotnet core 8;
 - MongoDB;
 - JWT Authenticator;
 - Mocked Integrated tests with MongoDb database;

 Ferramentas utilizadas:
  - Visual studio 2022 community edition;
  - Docker desktop for windows;

Este projeto inclui Dockerfiles para construir a aplicação

## Requisitos
### Tecnologias
- .NET 6 ou 8 para backend;
- Framework de frontend de escolha;
- Banco de dados entre SQL Server, MySQL e MongoDB;
- Controle de versão pelo GitHub;
- Desenvolvimento de aplicações API e Web de alta performance;
- Arquiteturas baseadas em APIs e microsserviços;
- Testes automatizados (unitário, integração, E2E, stress - não precisa ser todos, mas
utilizar um ou mais e explicar posteriormente);
- Paradigmas de programação, padrões de projeto e boas práticas;
- Técnicas como TDD, DDD, Clean Code, Event-driven architecture (não precisa ser
todos, mas utilizar um ou mais e explicar posteriormente);

### Funcionalidades
#### Login
- Permitir que o usuário insira seu nome de usuário e senha;
- Realizar validações de entrada para garantir dados corretos;
- Autenticar o usuário no sistema;
- Apresentar mensagens de erro ou sucesso de acordo com o resultado da
autenticação;

#### Cadastro
- Permitir que o usuário se cadastre fornecendo nome, e-mail e senha;
- Realizar validações de entrada para garantir dados corretos;
- Avaliar a segurança da senha e apresentar um indicador de nível de segurança;
- Persistir as informações do usuário no banco de dados;
- Utilizar criptografia para armazenar a senha;
- Enviar e-mail de confirmação de cadastro. O usuário só poderá fazer login após a
confirmação;

#### Logs e Exceções
- Implementar um sistema de controle de logs para registrar eventos importantes na
aplicação;
- Adicionar tratamento de exceções para lidar com situações inesperadas de forma
adequada;
- Os logs devem conter informações relevantes para identificação e resolução de
problemas;

#### Interações Frontend - Backend
- As comunicações entre o frontend e o backend devem ser feitas de forma RESTful;

## Como executar a aplicação
### Requisitos
- .Net 8
- Visual Studio 2022 ou IDE de sua preferencia
- Docker
- Docker-compose
- Git
- NodeJs

### Executando o projeto local em modo debug
Para a database, basta executar o comando na raiz do projeto:

```sh
docker-compose up -d
```
Credenciais para acesso ao banco de dados:
- username: admin
- password: admin

Para o projeto em .Net basta executar em sua IDE de preferencia.

Para o Frontend, apenas execute o comando abaixo na pasta `/luizalabs.UI/`
* Garantir que as dependencias estão instaladas:
```sh
npm install
```
*  Executar a aplicação
```sh
npm start
```