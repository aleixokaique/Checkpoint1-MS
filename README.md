# API ASP.NET Core com Cache Redis

Este projeto apresenta um exemplo de como utilizar o **Redis** para
implementar cache em uma API ASP.NET Core, reduzindo o número de
consultas diretas ao banco de dados e aumentando a performance da
aplicação.\
O banco principal adotado é o **MySQL**, mas a solução pode ser
facilmente ajustada para **MongoDB** ou outros bancos relacionais/não
relacionais.

------------------------------------------------------------------------

## Recursos Implementados

-   Consulta de usuários diretamente no banco de dados.\
-   Uso de **Redis** para armazenar resultados em cache e evitar
    chamadas repetitivas.\
-   Definição de expiração dos itens no cache (15 minutos).\
-   Resposta mais rápida quando os dados já estão disponíveis no Redis.\
-   Tratamento de erros para maior estabilidade da aplicação.

------------------------------------------------------------------------

## Modelo de Usuário

A entidade `Users` representa os dados do usuário recuperados do banco:

``` csharp
public class Users
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime UltimoAcesso { get; set; }
}
```

------------------------------------------------------------------------

## Integrações

### Banco de Dados

-   **MySQL**: consultas realizadas com **Dapper** e **MySqlConnector**.

### Cache Redis

-   Biblioteca utilizada: **StackExchange.Redis**.\
-   Os resultados são armazenados em JSON no Redis.\
-   Cada entrada no cache expira após **15 minutos**.

------------------------------------------------------------------------

## Funcionamento do Cache

1.  Verifica se a chave já está presente no Redis.\
2.  Se encontrada, retorna os dados diretamente do cache.\
3.  Caso contrário, busca os registros no banco de dados.\
4.  O resultado é salvo no Redis com tempo de expiração configurado.


