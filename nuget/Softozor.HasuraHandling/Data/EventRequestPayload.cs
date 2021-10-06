namespace Softozor.HasuraHandling.Data;

using System;
using Newtonsoft.Json;

public class EventRequestPayload<TInputType>
    where TInputType : class
{
    [JsonConstructor]
    public EventRequestPayload(Guid id, DateTime createdAt, Trigger trigger, Table table, HasuraEvent<TInputType> e)
    {
        this.Id = id;
        this.CreatedAt = createdAt;
        this.Trigger = trigger;
        this.Table = table;
        this.Event = e;
    }

    [JsonProperty("id")]
    public Guid Id { get; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; }

    [JsonProperty("trigger")]
    public Trigger Trigger { get; }

    [JsonProperty("table")]
    public Table Table { get; }

    [JsonProperty("event")]
    public HasuraEvent<TInputType> Event { get; }
}

public class Trigger
{
    [JsonConstructor]
    public Trigger(string name)
    {
        this.Name = name;
    }

    [JsonProperty("name")]
    public string Name { get; }
}

public class Table
{
    [JsonConstructor]
    public Table(string schema, string name)
    {
        this.Schema = schema;
        this.Name = name;
    }

    [JsonProperty("schema")]
    public string Schema { get; }

    [JsonProperty("name")]
    public string Name { get; }
}

public class HasuraEvent<TInputType>
    where TInputType : class
{
    private readonly Op op;

    [JsonConstructor]
    public HasuraEvent(HasuraSessionVariables sessionVariables, string op, EventData<TInputType> data)
    {
        this.SessionVariables = sessionVariables;
        this.op = (Op)Enum.Parse(typeof(Op), op);
        this.Data = data;
    }

    [JsonProperty("session_variables")]
    public HasuraSessionVariables SessionVariables { get; }

    [JsonProperty("op")]
    public string Op => this.op.ToString();

    [JsonProperty("data")]
    public EventData<TInputType> Data { get; }
}

public enum Op
{
    INSERT,

    UPDATE,

    DELETE,

    MANUAL
}

public class EventData<TInputType>
    where TInputType : class
{
    [JsonConstructor]
    public EventData(TInputType o, TInputType n)
    {
        this.Old = o;
        this.New = n;
    }

    [JsonProperty("old")]
    public TInputType Old { get; }

    [JsonProperty("new")]
    public TInputType New { get; }
}