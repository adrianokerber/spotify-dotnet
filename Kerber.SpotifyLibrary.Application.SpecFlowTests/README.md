# Projeto de testes utilizando BDD através do SpecFlow

Este é o projeto de testes para o *Spotify dotnet CRUD* e tem o objetivo de testar as regras de negócio das controllers da REST API.

## Estruturando testes

O SpecFlow utiliza escopo global, sendo assim todos os passos (A.K.A.: Steps) são compartilhados entre os cenários e funcionalidades.
Para criar novos testes criamos um arquivo `.feature`.

Temos duas bases gerais para as classes de teste, sendo:
1. `BaseSteps.cs` a classe base geral que deve agregar todos os métodos comuns aos demais testes, porém nenhum destes métodos pode ser um step. Os steps devem ficar na classe filha ou na classe de steps compartilhados chamada `CommonSteps.cs`
2. Todos os passos de teste que devem ser compartilhados precisam ficar definidos na classe `CommonSteps.cs` pois ela é o centralizador de passos comuns a todos os testes.

Utilize a classe `ParameterNameGuide.cs` para armazenar a chave para cada objeto que será salvo nos contextos do SpecFlow e que devem ser compartilhados entre os steps.

**DICA**: Sempre crie testes verbosos e bem definidos para deixar as intenções claras

>Não conheçe o SpecFlow e quer entender um pouco mais sobre esse framework que usamos? Acesse https://specflow.org/

## Estrutura de testes atual

O projeto de testes está dividido em dois tipos, primeiro testes de Controllers sem mockar o servidor, depois na pasta `ServerMigration` estão os testes utilizando mock no servidor também, tornando mais confiáveis as respostas por passar por todo o fluxo do projeto.