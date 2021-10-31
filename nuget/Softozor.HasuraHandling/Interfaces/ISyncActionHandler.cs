namespace Softozor.HasuraHandling.Interfaces;

public interface ISyncActionHandler<TInput, TOutput>
{
    TOutput Handle(TInput input);
}