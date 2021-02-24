# webapi-EF-mysql-NLog
webapi-EF-mysql-NLog

Dependências:

    dotnet add package Microsoft.EntityFrameworkCore --version 3.1.3

    dotnet add package Microsoft.EntityFrameworkCore.Design --version 3.1.1

    dotnet add package Pomelo.EntityFrameworkCore.MySql --version 3.1.1

    dotnet add package NLog --version 4.6.7

    dotnet add package NLog.Web.AspNetCore --version 4.9.0
    
Projeto de aprendizado para uma webapi com Entity Framework usando MySql, C#, .NET e NLog baseado nos seguintes tuturiais:

parte 1: https://medium.com/@gedanmagalhaes/criando-uma-api-rest-com-asp-net-core-3-1-entity-framework-mysql-423c00e3b58e

parte 2: https://medium.com/@gedanmagalhaes/criando-uma-api-rest-com-asp-net-core-3-1-entity-framework-mysql-parte-2-e969e82e5d2f

HttpPost baseado neste código: https://github.com/GedanMagal/Api-Ef/blob/master/Controllers/ProductController.cs

EXEMPLO DELET E PUT: https://www.entityframeworktutorial.net/efcore/delete-data-in-entity-framework-core.aspx

Geração de Logs: https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-3


### PASSOS para ativar Entity Framework Core:

para puxar os registros do banco de dados: ainda não temos nenhuma tabela criada. abrir o terminal do vscode e gerar nossa primeira Migration rodando o comando:

(se já estiver com as migrations prontas basta pular esse passo)

    dotnet ef migrations add PrimeiraMigration

qualquer nome pode ser adicionado após o add. este comando gerara uma tabela no seu schema com o nome dado no DataContext, no caso a primeira tabela que criará será a de Pessoas pois foi a unica que mapeamos até o momento.

depois insira o comando para criar as tabelas e também o banco de dados:

    dotnet ef database update

caso queira ver o processo de execução, basta rodar com o -v no final do comando

