namespace PhoneBook.WebApi.Helpers.JsonParser;

public class JsonMetaObject
{
    public required int Id { get; set; }

    public bool HasArrays { get; set; } = false;

    public bool HasObjects { get; set; } = false;

    public Dictionary<string, (JsonMetaType type, string textValue)> Properties { get; set; } = [];
}

public enum JsonMetaType
{
    String,
    Number,
    Array,
    JsObject
}