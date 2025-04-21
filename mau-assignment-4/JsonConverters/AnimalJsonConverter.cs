namespace mau_assignment_4.JsonConverters;

public class AnimalJsonConverter : JsonConverter<Animal>
{
	public override Animal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			reader.Read();

		using (var doc = JsonDocument.ParseValue(ref reader))
		{
			var root = doc.RootElement;
			var typeName = root.GetProperty("Type").GetString();

			return typeName switch
			{
				"Eagle" => JsonSerializer.Deserialize<Eagle>(root.GetRawText(), options),
				"Parrot" => JsonSerializer.Deserialize<Parrot>(root.GetRawText(), options),
				"Penguin" => JsonSerializer.Deserialize<Penguin>(root.GetRawText(), options),
				"Bat" => JsonSerializer.Deserialize<Bat>(root.GetRawText(), options),
				"Dolphin" => JsonSerializer.Deserialize<Dolphin>(root.GetRawText(), options),
				"Gorilla" => JsonSerializer.Deserialize<Gorilla>(root.GetRawText(), options),
				"Alligator" => JsonSerializer.Deserialize<Alligator>(root.GetRawText(), options),
				"Chameleon" => JsonSerializer.Deserialize<Chameleon>(root.GetRawText(), options),
				"Komodo" => JsonSerializer.Deserialize<Komodo>(root.GetRawText(), options),
				_ => JsonSerializer.Deserialize<Animal>(root.GetRawText(), options)
			};
		}
	}

	public override void Write(Utf8JsonWriter writer, Animal value, JsonSerializerOptions options)
	{
		var type = value.GetType();
		writer.WriteStartObject();
		writer.WriteString("Type", type.Name);
		using var doc = JsonDocument.Parse(JsonSerializer.Serialize(value, type, options));

		foreach (var prop in doc.RootElement.EnumerateObject())
		{
			if (prop.Name != "Type")
				prop.WriteTo(writer);
		}

		writer.WriteEndObject();
	}
}
