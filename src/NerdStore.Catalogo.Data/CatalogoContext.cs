using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NerdStore.Catalogo.Domain;
using NerdStore.Core.Data;

namespace NerdStore.Catalogo.Data
{
    public class CatalogoContext : DbContext, IUnitOfWork
    {
        // O DbContextOptions é o parâmetro necessário para configurar o contexto na classe Startup da aplicação;
        // (isso é uma prática principalmente para ASP.NET Core ou EF Core; precisa utilizar o DbContextOptions que é uma espécie de Factory que vai fazer a configuração dele no Startup project);
        public CatalogoContext(DbContextOptions<CatalogoContext> options)
          : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // pega todas as entidades mapeadas, verifica quais as propriedades são do tipo string, e mapeia automaticamente o tipo da coluna
            // como varchar(100); se não for varchar(100) manipule para ser de outra forma, caso não seja declarado, tudo que é string vai virar
            // varchar(100), justamente para ele evitar de criar uma coluna NVarchar(max);
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.Relational().ColumnType = "varchar(100)";

            // ele vai buscar todas as entidades que estão declaradas acima, buscar os mappings dela via reflection (uma única vez) e configurar
            // para que ele siga as configurações feitas (na CategoriaMapping e no ProdutoMapping por exemplo);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            // Na hora de fazer um commit, na hora de salvar no banco, irá pegar o Tracker do EF, que é o mapeador de mudanças, e irá buscar
            // pror propriedades que possuem o nome "DataCadastro", se "DataCadastro" existe, e se estiver adicionando esta entidade, o "DataCadastro"
            // vai possuir o valor DateTime.Now; caso esteja modificando, ou seja, atualizando a entidade, irá ignorar qualquer dado no campo "DataCadastro"
            // justamente para não salvar, atualizando a DataCadastro sem querer; e isto fica diretamente no commit porque é ali que vai valer salvar no banco;

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }
    }
}
