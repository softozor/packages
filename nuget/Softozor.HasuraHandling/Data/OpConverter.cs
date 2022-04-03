namespace Softozor.HasuraHandling.Data;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class OpConverter : JsonConverter<Op>
{
    public override Op Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (Op)Enum.Parse(typeof(Op), reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, Op value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}