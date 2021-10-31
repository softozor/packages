namespace Softozor.HasuraHandling.Interfaces;

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

[SuppressMessage(
    "Naming",
    "CA1711:Identifiers should not have incorrect suffix",
    Justification = "This is the domain name")]
public interface IEventHandler<TInput, TOutput>
{
    Task<TOutput> Handle(TInput oldRow, TInput newRow);
}