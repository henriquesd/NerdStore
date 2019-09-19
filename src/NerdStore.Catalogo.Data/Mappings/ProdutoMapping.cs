using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            // aqui é mapeado tudo aquilo que eu tenho para a tabela de Produtos;

            // definindo a chave primária;
            builder.HasKey(c => c.Id);

            // mapeando as colunas;
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Imagem)
                .IsRequired()
                .HasColumnType("varchar(250)");

            // OwnsOne - significa que eu estou transformando o meu objeto de valor "Dimensoes" em colunas na minha tabela de Produto;
            // ele mapeia, são duas classes (classes aninhadas nesse caso), "Dimensoes" é uma classe utilizada pelo produto, mas na hora de
            // ser representado ele está no mesmo nível da tabela; então o Entity Framework faz esse de-para;
            builder.OwnsOne(c => c.Dimensoes, cm =>
            {
                cm.Property(c => c.Altura)
                    .HasColumnName("Altura")
                    .HasColumnType("int");

                cm.Property(c => c.Largura)
                    .HasColumnName("Largura")
                    .HasColumnType("int");

                cm.Property(c => c.Profundidade)
                    .HasColumnName("Profundidade")
                    .HasColumnType("int");
            });

            // o nome da tabela vai ser "Produtos";
            builder.ToTable("Produtos");
        }
    }
}
