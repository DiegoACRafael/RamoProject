# *[RamosProject] - Aplicação de Cadastro de pessoas e produtos com API RESTful*

## *1. Apresentação*

Bem-vindo ao repositório do projeto *[RamosProject]*. 

Este é um teste prático exemplificando minhas skills em NET.

O objetivo principal desenvolver uma aplicação que permita aos usuários autenticados criar, editar, visualizar e excluir pessoas e produtos.

### *Autor*
•⁠  ⁠*Diego Augusto Coelho Rafael*

## *2. Proposta do Projeto*

O projeto consiste em:

•⁠  ⁠*Frontend Angular:* Interface web para interação com produto.

•⁠  ⁠*API RESTful:* Exposição dos recursos  para integração com outras aplicações ou desenvolvimento de front-ends alternativos.

•⁠  ⁠*Autenticação e Autorização:* Implementação de controle de acesso utilizando o Identity.

•⁠  ⁠*Acesso a Dados:* Implementação de acesso ao banco de dados através de ORM.

## *3. Tecnologias Utilizadas*

•⁠  ⁠*Linguagem de Programação:* C#
  - ASP.NET Core Web API
  - Entity Framework Core
•⁠  ⁠*Banco de Dados:* SQLite
•⁠  ⁠*Autenticação e Autorização:*
  - ASP.NET Core Identity
  - JWT (JSON Web Token) para autenticação na API
•⁠  ⁠*Front-end:*
  - Angular 17+
•⁠  ⁠*Documentação da API:* Swagger

## *4. Estrutura do Projeto*

A estrutura do projeto é organizada da seguinte forma:

•⁠  ⁠src/
  - RamoProject/Frontend/src/app - Projeto Angular
  - RamoProject/Frontend/src/Api - API RESTful
  - RamoProject/Frontend/src/Infra.EF/ - Modelos de Dados e Configuração do EF Core
•⁠  ⁠README.md - Arquivo de Documentação do Projeto
•⁠  ⁠.gitignore - Arquivo de Ignoração do Git

## *5. Funcionalidades Implementadas*

•⁠  ⁠*CRUD para Pessoas e produtos:* Permite criar, editar, visualizar e excluir pessoas e produtos.
•⁠  ⁠*Autenticação e Autorização:* Utilização do Identity
•⁠  ⁠*API RESTful:* Exposição de endpoints para operações CRUD via API.
•⁠  ⁠*Documentação da API:* Documentação automática dos endpoints da API utilizando Swagger.

## *6. Como Executar o Projeto*

### *Pré-requisitos*

•⁠  ⁠.NET SDK 8.0 ou superior
•⁠  ⁠SQLite
•⁠  ⁠Visual Studio Code
•⁠  ⁠Git

### *Passos para Execução*

1.⁠ ⁠*Clone o Repositório:*
   - ⁠ git clone https://github.com/DiegoACRafael/RamoProject.git
   - ⁠ cd Backend/src/Api ⁠
        - $ dotnet run
        - Acesse a aplicação em: http://localhost:5267/
        - Acesse a documentação da API em: http://localhost:5267/swagger

2.⁠ ⁠*Configuração do Banco de Dados:*
   - No arquivo ⁠ appsettings.json ⁠, está configurado a utilização do SQLite para a realização dos testes

3.⁠ ⁠*Executar a Aplicação Angular:*
   - ⁠ cd Frontend/src/app
   - ⁠ npm install --force
   - ng serve
   - Acesse a aplicação em: http://localhost:4200

## *7. Instruções de Configuração*

•⁠  ⁠*JWT para API:* As chaves de configuração do JWT estão no ⁠ appsettings.json ⁠.

•⁠  ⁠*Migrações do Banco de Dados:* As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

## *8. Documentação da API*

A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:

http://localhost:5267/swagger
