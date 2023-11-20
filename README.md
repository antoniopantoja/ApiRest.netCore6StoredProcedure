# ApiRest.netCore6StoredProcedure

Scenario
Back must be a REST API in C#. Requirements • It must be possible to create, update, view and remove Customer o The customer record must only contain the following fields:  Name;  email  Logo;  Public place; • A client can contain several public places;  A customer cannot register twice with the same email address;

The API will have a large volume of requests so keep in mind that performance concerns are something we are constantly concerned about.
.Net Core 6.0 framework
Merge the use of an ORM of your choice for queries and for other operations use Stored Procedures

Cenário 
O Back deve ser uma API REST em C#. Requisitos • Deve ser possível criar, atualizar, visualizar e remover Cliente o O cadastro dos clientes deve conter apenas os seguintes campos:  Nome;  e-mail  Logotipo;  Logradouro; • Um cliente pode conter vários logradouros;  Um cliente não pode se registrar duas vezes com o mesmo endereço de e-mail;

A API terá um grande volume de requisições então tenha em mente que a preocupação com performance é algo que temos constantemente preocupação.
framework .Net Core 6.0 
Mesclar a utilização de um ORM a sua escolha para consultas e para outras operações utilizar Stored Procedures

## Especificação tecnica

.NET Core 6 Web API

SQL Server 2016

## package 
Swashbuckle.AspNetCore versão: 6.5.0 nuget 

Swagger APIs built on ASP.NET Core

![image](https://github.com/antoniopantoja/ApiRest.netCore6StoredProcedure/assets/138262828/74f28773-00ed-4b65-b8fe-d78d3135f1cd)


### Configuração

Open the appsettings.json file and change the database information, the authentication key and the network path where we save the files
Here you will configure your authentication key "Key", "Issuer", "Audience" here in this project I made an example.

Abra o arquivo appsettings.json e vamos altera as informações de banco de dados a chave de autenticação e caminho de rede aonde vamos salva os aquivos
Aqui você vai configura sua chave de autenticação "Key", "Issuer", "Audience" aqui nesse projeto fiz um exemplo. 

        "Jwt":{
              "Key": "UklGRiZxAgBXQVZFZm10IBIAAAABAA",
              "Issuer": "APIDesafioNet",
              "Audience": "DesafioNETViews"
              }
              
I created this username and password for authentication

criei esse usuario e senha para autenticação.          
![image](https://github.com/antoniopantoja/ApiRest.netCore6StoredProcedure/assets/138262828/8ac41ccb-5f1b-4513-ba27-a51667168c43)



        {
        "username": "admin",
        "password": "555236"
        }
              
Here you will enter the database connection string information.

Aqui você vai coloca as informações da string de conexão com banco de dados. 

        "ConnectionStrings": {
        "Connection": "server=*;User Id=username;password=password;Persist Security Info=True;database=DesafioNET"
        }

Here you will enter the Local network path where the Files will be saved the important item if published your API is the URL information in QAS or PROD

Aqui você vai coloca o caminho de rede Local onde os Arquivos seram salvos o item importate caso publicado sua API é a informação da URL em QAS ou PROD

        "File": {
        "Local": "C:\\inetpub\\wwwroot\\DesafioNet\\Imagens\\",
        "Servidor": "https://www.DesafioNet/"
        }

### Preview

<img align="center" alt="React and React Native" src="https://github.com/antoniopantoja/antoniopantoja/blob/main/assets/DESAFIO.NET-API.gif"/>

### Banco de Dados

#### Scrpts

Nome da base de dados DesafioNET as tabelas e views Stored Procedures  


