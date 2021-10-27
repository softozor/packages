namespace Softozor.HasuraHandling.Data;

using Newtonsoft.Json;

public class ActionRequestPayload<TInputType>
    where TInputType : class
{
    [JsonConstructor]
    public ActionRequestPayload(HasuraAction action, TInputType input, HasuraSessionVariables sessionVariables)
    {
        this.Action = action;
        this.Input = input;
        this.SessionVariables = sessionVariables;
    }

    [JsonProperty("action")]
    public HasuraAction Action { get; }

    [JsonProperty("input")]
    public TInputType Input { get; }

    [JsonProperty("session_variables")]
    public HasuraSessionVariables SessionVariables { get; }
}