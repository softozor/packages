namespace Softozor.HasuraHandling.Interfaces;

using System.Threading.Tasks;

public interface IActionHandler<TInputType, TOutputType>
{
    Task<TOutputType> Handle(TInputType input);
}