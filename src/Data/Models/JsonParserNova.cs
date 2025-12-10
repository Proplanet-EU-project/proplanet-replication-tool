using System.Text.Json;

public class JsonParserNova
{
    public List<JsonElementWrapper> Results { get; private set; } = new List<JsonElementWrapper>();

    public void ParseJson(string jsonString)
    {
        using var doc = JsonDocument.Parse(jsonString);
        var root = doc.RootElement.GetProperty("results");

        foreach (var level0Property in root.EnumerateObject())
        {
            var element = ParseElement(level0Property);
            Results.Add(new JsonElementWrapper { Name = level0Property.Name, Elements = element });
        }
    }

    private List<JsonElementWrapper> ParseElement(JsonProperty property)
    {
        var elements = new List<JsonElementWrapper>();

        if (property.Value.ValueKind == JsonValueKind.Object)
        {
            foreach (var child in property.Value.EnumerateObject())
            {
                var childElements = ParseElement(child);
                elements.Add(new JsonElementWrapper { Name = child.Name, Elements = childElements });
            }
        }
        else
        {
            elements.Add(new JsonElementWrapper { Name = property.Name, Value = property.Value.ToString() });
        }

        return elements;
    }
}

public class JsonElementWrapper
{
    public string Name { get; set; }
    public List<JsonElementWrapper> Elements { get; set; } = new List<JsonElementWrapper>();
    public string Value { get; set; }
}