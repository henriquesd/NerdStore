using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTO;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalogo.Domain
{
    // O serviço de domínio é um serviço cross-aggregate , ou seja, ele trabalha com duas ou mais entidades;
    // ou uma outra necessidade de você ter um serviço de domínio, é quando a sua regra de negócio não cabe nem em uma camada
    // de aplicação, nem em uma entidade;

    // Temos aqui um EstoqueService, que é um Serviço de Domínio, que representa uma ação conhecida da linguage ubíqua, que é DebitarEstoque e ReporEstoque;
    // se você tivesse mais ações como adquirir produto do fornecedor, etc (qualquer coisa que seja relativa ao seu negócio), talvez aqui seja o lugar certo para você trabalhar isso;

    // Muitas vezes o Serviço de Domínio é utilizado como uma ferramenta de persistência das entidades, mas falando puramente em DDD, no sentido da coisa levada a sério mesmo,
    // o EstoqueService ele só existe porque ele realmente é um item da linguagem ubíqua, ele representa ações que estão previstas na  linguagem ubíqua;
    // Salvar produto, atualizar produto, isso é CRUD, então não representa uma ideia da linguagem ubíqua, isso pode ser facilmente em outro lugar com menos esforço;
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public EstoqueService(IProdutoRepository produtoRepository,
                              IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
        }

        //public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        //{
        //    var produto = await _produtoRepository.ObterPorId(produtoId);

        //    if (produto == null) return false;

        //    if (!produto.PossuiEstoque(quantidade)) return false;

        //    produto.DebitarEstoque(quantidade);

        //    // TODO: Parametrizar a quantidade de estoque baixo;
        //    if (produto.QuantidadeEstoque < 10)
        //    {
        //        // avisar, mandar email, abrir chamado, realizar nova compra, etc;
        //        // trabalhando com eventos;
        //        await _bus.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
        //    }

        //    _produtoRepository.Atualizar(produto);
        //    return await _produtoRepository.UnitOfWork.Commit();
        //}

        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitarListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                if (!await DebitarItemEstoque(item.Id, item.Quantidade)) return false;
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (produto.QuantidadeEstoque < 10)
            {
                await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return true;
        }

        public async Task<bool> ReporListaProdutosPedido(ListaProdutosPedido lista)
        {
            foreach (var item in lista.Itens)
            {
                await ReporItemEstoque(item.Id, item.Quantidade);
            }

            return await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var sucesso = await ReporItemEstoque(produtoId, quantidade);

            if (!sucesso) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> ReporItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;
            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return true;
        }

        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
