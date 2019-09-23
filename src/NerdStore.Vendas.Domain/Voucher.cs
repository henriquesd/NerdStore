using System;
using System.Collections.Generic;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain
{
    public class Voucher : Entity
    {
        public string Codigo { get; private set; }
        public decimal? Percentual { get; private set; }
        public decimal? ValorDesconto { get; private set; }
        public int Quantidade { get; private set; }
        public TipoDescontoVoucher TipoDescontoVoucher { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataUtilizacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public bool Ativo { get; private set; }
        public bool Utilizado { get; private set; }

        // EF Rel. - Um Voucher pode ser aplicado a vários pedidos, por isso que tem um ICollection aqui, para o EF entender que isso é uma relação de 1 para N, é um voucher para N pedidos;
        public ICollection<Pedido> Pedidos { get; set; }

    }
}
