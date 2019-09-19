using AutoMapper;
using NerdStore.Catalogo.Application.ViewModels;
using NerdStore.Catalogo.Domain;

namespace NerdStore.Catalogo.Application.AutoMapper
{
    // Perfil de mapeamento de domínio para view model do AutoMapper;
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            // O Produto possui as Dimensoes dentro de uma outra classe, e na ProdutoViewModel todas as propriedades estão dentro da própria classe ProdutoViewModel,
            // então para isso é necessário criar um mapeamento adicional para cada campo;
            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(d => d.Largura, o => o.MapFrom(s => s.Dimensoes.Largura))
                .ForMember(d => d.Altura, o => o.MapFrom(s => s.Dimensoes.Altura))
                .ForMember(d => d.Profundidade, o => o.MapFrom(s => s.Dimensoes.Profundidade));

            // Mapeia Categoria para CategoriaViewModel; então irá copiar os campos de Categoria para CategoriaViewModel;
            CreateMap<Categoria, CategoriaViewModel>();
        }
    }
}