using PhoneBook.WebApi.DTOs.PhonebookRecord;

namespace PhoneBook.WebApi.Helpers.JsonParser;

public class CustomParser
{
    public async Task<List<PhonebookDto>> ParseJsonText(string rawJson, CancellationToken ctk)
    {
        string json = rawJson.Replace("\n", "");
        List<PhonebookDto> result = [];

        var jsonTextObjects = JsonTextSplitter(json);

        

        return result;
    }


    private List<string> JsonTextSplitter(string json)
    {
        return DetermineJsonObjectsInText(json)
            .Select(
                scope => json.Substring(scope.start, scope.end - scope.start)
            )
            .ToList();
    }


    private List<(int start, int end)> DetermineJsonObjectsInText(string json)
    {
        List<(int start, int end)> scopesOfJson = [];
        Stack<(char symbol, int position)> jsonBrackets = new();
        Stack<char> innerJsonBrackets = new();
        for (int i = 0; i < json.Length; i++)
        {
            if (json[i] == '{'
                && (jsonBrackets.Count == 0
                    || (jsonBrackets.Peek().symbol == '}' && innerJsonBrackets.Count == 0)))
            {
                jsonBrackets.Push((json[i], i));
                scopesOfJson.Add((i, 0));
                continue;
            }

            if (json[i] == '{' && jsonBrackets.Peek().symbol == '{')
            {
                innerJsonBrackets.Push(json[i]);
                continue;
            }

            if (json[i] == '}'
                && jsonBrackets.Peek().symbol == '{'
                && innerJsonBrackets.Peek() == '{')
            {
                innerJsonBrackets.Pop();
                continue;
            }

            if (json[i] == '}'
                && innerJsonBrackets.Count == 0
                && jsonBrackets.Peek().symbol == '{')
            {
                var (_, startPosition) = jsonBrackets.Pop();
                scopesOfJson.Add((startPosition, i));
            }
        }

        return scopesOfJson;
    }
}