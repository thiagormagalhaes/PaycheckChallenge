# PaycheckChallenge

O serviço responsável por criar o extrato da folha salarial dos funcionários. Esse extrato expõe o salário líquido do funcionário e todos os seus descontos discriminados.

### Endpoints
http://localhost:5000/swagger/index.html

### Requisitos
- .NET 7
- SQL Server 2022
- Docker
- Docker Compose

### Como usar:
Execute os seguintes comandos no prompt de comando:
1. `docker build -t web_api .`
2. `docker-compose up`

### Pipeline
É possível acompanhar as execuções da pipeline pelo seguinte link:
https://dev.azure.com/CI-PaycheckChallenge/PaycheckChallenge/_build?definitionId=1

### Planos de teste
É possível ver os resultados dos testes pelo seguinte link:
https://dev.azure.com/CI-PaycheckChallenge/PaycheckChallenge/_testManagement/runs?_a=runQuery
