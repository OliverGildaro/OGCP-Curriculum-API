using Newtonsoft.Json;
using System;
using System.Linq;

public class FieldsConverter : JsonConverter<string[]>
{
    public override string[] ReadJson(JsonReader reader, Type objectType, string[] existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var value = reader.Value?.ToString(); // Obtener el valor del parámetro como string
        return string.IsNullOrWhiteSpace(value)
            ? Array.Empty<string>() // Si el valor está vacío, devolvemos un array vacío
            : value.Split(',') // Dividir el string en base a las comas
                    .Select(f => f.Trim()) // Eliminar los espacios en blanco de cada campo
                    .ToArray(); // Convertir a un array
    }

    public override void WriteJson(JsonWriter writer, string[] value, JsonSerializer serializer)
    {
        writer.WriteValue(value == null ? string.Empty : string.Join(",", value)); // Escribir el array como un string con comas
    }
}
