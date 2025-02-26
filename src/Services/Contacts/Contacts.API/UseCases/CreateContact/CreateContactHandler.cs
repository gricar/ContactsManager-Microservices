using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using MediatR;

namespace Contacts.API.UseCases.CreateContact;

public record CreateContactCommand : IRequest<CreateContactResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }
}

public record CreateContactResponse(bool IsSuccess);

public class CreateContactCommandHandler(IPublishEndpoint publishEndpoint)
    : IRequestHandler<CreateContactCommand, CreateContactResponse>
{
    public async Task<CreateContactResponse> Handle(CreateContactCommand command, CancellationToken cancellationToken)
    {
        var eventMessage = command.Adapt<CreateContactEvent>();

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        return new CreateContactResponse(true);
    }
}
