namespace Shop.Helpers;

public interface ICommandHandler<TCommand>
{
    Task Handle(TCommand command);
}
