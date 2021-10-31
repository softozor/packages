namespace Softozor.HasuraHandling.Interfaces;

using System.Threading.Tasks;

public interface IActionHandler<TInput, TOutput>
{
    Task<TOutput> Handle(TInput input);
}