using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");


            // mapeando o relacionamento de 1 para N, ou seja, 1 Produto pode estar relacionado com Categorias;
            // Uma categoria possui N produtos; Uma categoria pode estar relacionada a muitos produtos; e um produto só possui uma categoria;
            // 1 : N => Categorias : Produtos
            builder.HasMany(c => c.Produtos) // categoria tem uma lista de produtos;
                .WithOne(p => p.Categoria) // produto possui uma categoria;
                .HasForeignKey(p => p.CategoriaId);

            builder.ToTable("Categorias");
        }
    }
}