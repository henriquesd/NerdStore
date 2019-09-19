using System.Threading.Tasks;
using MediatR;
using NerdStore.Core.Messages;

namespace NerdStore.Core.Bus
{
    // Ele não é bem um bus, ele é um "bus em memória"; o Bus é um pouco além disso; O Mediatr pode ser uma interface para o seu bus;
    // O Mediatr ele pode entregar a sua mensagem para um service bus, ele pode ser o seu bus em memória enquanto você não tem o bus, então você
    // só substitui o uso do Mediator por um bus; Ele não é exatamente um bus mas ele faz o mesmo papel;
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Esse evento "<T>" ele tem que ser filho de Event; Ele vai aceitar um DomainEvent porque ele é filho de Event;
        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            await _mediator.Publish(evento);
        }
    }
}