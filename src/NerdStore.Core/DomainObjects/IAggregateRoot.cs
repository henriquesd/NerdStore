namespace NerdStore.Core.DomainObjects
{
    // IAggregateRoot - isso se chama interface de marcação (várias aplicações e ferramentas utilizam);
    // ele utiliza para saber se essa classe realmente é uma raiz de agregação, porque ele implementa uma interface;
    // para distinguir qual classe é uma entidade e qual classe é uma raiz de agregação para tratar ela de tal forma;
    public interface IAggregateRoot { }
}
