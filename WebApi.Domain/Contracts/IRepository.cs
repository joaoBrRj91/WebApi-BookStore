
using System;
using System.Collections.Generic;


/*
    Como um serviço irá ficar hospedado para qualquer aplicação cliente
    consumir é interresante limitar a quantidade de acesso a dados(principalmente
    o "GET"; pois temos limitação de dados do local/servidor o qual nosso serviço
    está hospedado.Se não limitarmos podemos ter nosso serviço imterrompido pq
    ultrapassou a quantidade de dados permitidos para serem trafegados)

    LIMITE SEMPRE A QUANTIDADE DE REGISTROS QUE UMA APLIUCAÇÃO CLIENTE IRÁ RECEBER]
    DO NOSSO SERVIÇO
    
 */
namespace WebApi.Domain.Contracts
{
    public interface IRepository<T> : IDisposable
    {
        IEnumerable<T> Get(int Skip = 0, int take = 25);
        T Get(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
