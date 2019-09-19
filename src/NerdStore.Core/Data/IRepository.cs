using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Core.Data
{
    // Um Repositório genérico, só que sem implementar os métodos genérico;
    // É apenas um Repositório onde ele vai implementar o IDisposable para poder fazer o Dispose;
    // E onde esse "T" é do tipo "IAggregateRoot", desta forma obrigatoriamente vai ter que passar um agregado, para atender aquela regra de um repositório por agregação;
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
