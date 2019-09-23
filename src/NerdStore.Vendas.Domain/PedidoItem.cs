using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; private set; }

        // Produto é um item de Catalogo, então poderia dentro do domínio de vendas criar um produto e relacionar aqui ao invés, 
        // de ter o Id e o Nome, mas como são apenas duas propriedades, o Id e o Nome estão nesta própria classe.
        public Guid ProdutoId { get; private set; }
        public string ProdutoNome { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorUnitario { get; private set; }

        // EF Rel.
        public Pedido Pedido { get; set; }

        public PedidoItem(Guid produtoId, string produtoNome, int quantidade, decimal valorUnitario)
        {
            ProdutoId = produtoId;
            ProdutoNome = produtoNome;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        // Para fazer uso através do EF;
        protected PedidoItem() { }

        internal void AssociarPedido(Guid pedidoId)
        {
            PedidoId = pedidoId;
        }

        public decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }

        internal void AdicionarUnidades(int unidades)
        {
            Quantidade += unidades;
        }

        internal void AtualizarUnidades(int unidades)
        {
            Quantidade = unidades;
        }

        public override bool EhValido()
        {
            return true;
        }
    }
}
