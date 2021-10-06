namespace Softozor.HasuraHandling.Interfaces;

public interface ISyncActionHandler<TInputType, TOutputType>
{
    TOutputType Handle(TInputType input);
}