using System.Text.Json.Serialization;
using System.Text.Json;

namespace UsersService.Api.JsonConverters;

public sealed class DateOnlyNullableJsonConverter : JsonConverter<DateOnly?>
{
    public override DateOnly? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TryGetDateTime(out DateTime dateTime))
            return DateOnly.FromDateTime(dateTime);
        else
            return null;
    }

    public override void Write(Utf8JsonWriter writer, DateOnly? value, JsonSerializerOptions options)
    {
        if (value == null)
            writer.WriteNullValue();
        else
            writer.WriteStringValue(((DateOnly)value).ToString("O"));
    }
}
