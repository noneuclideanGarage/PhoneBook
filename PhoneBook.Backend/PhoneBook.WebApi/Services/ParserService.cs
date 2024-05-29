using Newtonsoft.Json.Linq;
using PhoneBook.WebApi.DTOs.PhonebookRecord;
using PhoneBook.WebApi.Helpers.Mapping;
using Serilog;
namespace PhoneBook.WebApi.Services;

public class ParserService
{
    public List<PhonebookDto> ParseJsonText(string rawJson)
    {
        try
        {
            string json = rawJson.Replace("\n", "");
            List<PhonebookDto> result = [];

            var jsonTextAsSubstring = DefineJsonAsSubstring(json);

            result.AddRange(jsonTextAsSubstring.Select(DeserializeToPhonebookDto));
            // JsonMetaObject для каждого текстового объекта
            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return [];
        }
    }

    private static List<string> DefineJsonAsSubstring(string source)
    {
        var positions = DefineJsonPositions(source);

        if (positions.Count == 0) throw new Exception("There's no json-object in text");

        return positions
            .Select(
                position =>
                    source.Substring(position.Key, position.Value - position.Key + 1)
            ).ToList();
    }

    private static Dictionary<int, int> DefineJsonPositions(string source)
    {
        Stack<(char Symbol, int Position)> specialSymbols = new Stack<(char Symbol, int Position)>();

        Dictionary<int, int> positions = [];

        for (int i = 0; i < source.Length; i++)
        {
            if (source[i] == '{' && specialSymbols.Count == 0)
            {
                positions.Add(i, 0);
                specialSymbols.Push((source[i], i));
                continue;
            }

            if (source[i] == '{' && specialSymbols.Peek().Symbol == '{')
            {
                specialSymbols.Push((source[i], i));
                continue;
            }

            if (source[i] == '}')
            {
                var (_, start) = specialSymbols.Pop();

                if (specialSymbols.Count == 0)
                {
                    positions[start] = i;
                }
            }
        }

        return positions;
    }

    private static PhonebookDto DeserializeToPhonebookDto(string jsonText)
    {
        var syncDto = new PhonebookSyncDto();

        Log.Information("Start parsing Json with JObject");
        var parsedJson = JObject.Parse(jsonText);
        Log.Information("End parsing Json with JObject");

        Log.Information("Start fetching values into DTO");
        foreach (var prop in parsedJson.Properties())
        {
            Log.Information($"Current property name: {prop.Name}");
            var typeOfProp = prop.Value.Type;
            Log.Information("Current property's value type: {@type}", typeOfProp);

            if (prop.Name.ToLower().Contains("id") 
                && !prop.Name.ToLower().Contains("sub")
                && !prop.Name.ToLower().Contains("dept")
                && !prop.Name.ToLower().Contains("name"))
            {
                if (typeOfProp is JTokenType.Integer or JTokenType.String)
                {
                    syncDto.Id = prop.Value.Value<int>();
                    Log.Information($"parse id = {syncDto.Id}");
                    continue;
                }
            }

            if (prop.Name.ToLower().Contains("name") && typeOfProp == JTokenType.String)
            {
                if (prop.Name.ToLower() == "name" || prop.Name.ToLower().Contains("full"))
                {
                    var names = prop.Value.Value<string>()?.Split(" ");

                    if (names == null)
                    {
                        Log.Error("There's no names values in this json");
                        continue;
                    }

                    syncDto.Lastname = names[0];
                    syncDto.Firstname = names[1];
                    syncDto.Middlename = names[2];
                    Log.Information("Fetched all names properties");
                    continue;
                }

                if (prop.Name.ToLower() == "middlename")
                {
                    var middlenameFromJson = prop.Value.Value<string>();
                    if (middlenameFromJson is null or "" or " ")
                    {
                        Log.Error("Middlename property has no data");
                        continue;
                    }

                    syncDto.Middlename = middlenameFromJson;
                    Log.Information($"Middlename has data: {syncDto.Middlename}");
                    continue;
                }

                if (prop.Name.ToLower() == "lastname")
                {
                    var lastnameFromJson = prop.Value.Value<string>();
                    if (lastnameFromJson is null or "" or " ")
                    {
                        Log.Error("Lastname property has no data");
                        continue;
                    }

                    syncDto.Lastname = lastnameFromJson;
                    Log.Information($"Lastname has data: {syncDto.Lastname}");
                    continue;
                }

                if (prop.Name.ToLower() == "firstname")
                {
                    var firstFromJson = prop.Value.Value<string>();
                    if (firstFromJson is null or "" or " ")
                    {
                        Log.Error("Firstname property has no data");
                        continue;
                    }

                    syncDto.Firstname = firstFromJson;
                    Log.Information($"Firstname has data: {syncDto.Firstname}");
                    continue;
                }
            }

            if (prop.Name.ToLower().Contains("phone"))
            {
                if (prop.Name.ToLower() == "phonenumbers")
                {
                    var phoneNumberFromJson = prop.Value.ToObject<List<PhoneNumberDto>>();

                    if (phoneNumberFromJson is null)
                    {
                        Log.Error("There's no phone numbers in json");
                        continue;
                    }

                    syncDto.PhoneNumbers = phoneNumberFromJson;
                    Log.Information($"Fetching {phoneNumberFromJson.Count} phone numbers from json");
                    continue;
                }

                if (prop.Name.ToLower().Contains("gas"))
                {
                    var gasNumberFromJson = prop.Value.Value<string>();

                    if (gasNumberFromJson is null or "" or " ")
                    {
                        Log.Error("Gas-phonenumber has no data");
                        continue;
                    }

                    syncDto.PhoneNumbers.Add(new PhoneNumberDto
                    {
                        Type = "Газ. номер",
                        Number = gasNumberFromJson
                    });

                    Log.Information("Adding into 'phonumbers' 1 gas-phone number");
                    continue;
                }

                if (prop.Name.ToLower().Contains("gor")
                    || prop.Name.ToLower().Contains("urban")
                    || prop.Name.ToLower().Contains("city"))
                {
                    var urbanNumberFromJson = prop.Value.Value<string>();

                    if (urbanNumberFromJson is null or "" or " ")
                    {
                        Log.Error("Urban-phonenumber has no data");
                        continue;
                    }

                    syncDto.PhoneNumbers.Add(new PhoneNumberDto
                    {
                        Type = "Городской номер",
                        Number = urbanNumberFromJson
                    });

                    Log.Information("Adding into 'phonumbers' 1 urban-phone number");
                    continue;
                }
            }

            if (prop.Name.ToLower().Contains("email"))
            {
                var emailFromJson = prop.Value.Value<string>();

                if (emailFromJson is null or "" or " ")
                {
                    Log.Error("Email has no data");
                    continue;
                }

                syncDto.Email = emailFromJson;
                Log.Information("Added email into syncDto");
                continue;
            }

            if (prop.Name.ToLower().Contains("post"))
            {
                var postFromJson = prop.Value.Value<string>();

                if (postFromJson is null or "" or " ")
                {
                    Log.Error("Post has no data");
                    continue;
                }

                syncDto.Post = postFromJson;
                Log.Information("Added post into syncDto");
                continue;
            }

            if (prop.Name.ToLower().Contains("address"))
            {
                var addressFromJson = prop.Value.Value<string>();

                if (addressFromJson is null or "" or " ")
                {
                    Log.Error("Address has no data");
                    continue;
                }

                syncDto.Address = addressFromJson;
                Log.Information("Added address into syncDto");
                continue;
            }

            if (prop.Name.ToLower().Contains("organization"))
            {
                var orgFromJson = prop.Value.Value<string>();

                if (orgFromJson is null or "" or " ")
                {
                    Log.Error("Organization has no data");
                    continue;
                }

                syncDto.Organization = orgFromJson;
                Log.Information("Added organization into syncDto");
                continue;
            }

            if (prop.Name.ToLower().Contains("subdivision")
                && prop.Name.ToLower().Contains("department")
                && prop.Name.ToLower().Contains("dept")
                && !prop.Name.ToLower().Contains("id"))
            {
                var subFromJson = prop.Value.Value<string>();

                if (subFromJson is null or "" or " ")
                {
                    Log.Error("Subdivision has no data");
                    continue;
                }

                syncDto.Subdivision = subFromJson;
                Log.Information("Added subdivision into syncDto");
            }
        }

        return syncDto.FromSyncToDto();
    }
}