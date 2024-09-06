
using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

string baseAddress = "https://swapi.dev/api/";
string requestUri = "planets";

IStarWarsReader starWarsReader = new StarWarsReader();
var json = await starWarsReader.Read(baseAddress, requestUri);
Root root = JsonSerializer.Deserialize<Root>(json);

Builder builder = new Builder(root);
builder.Build();

Console.WriteLine(
@"The statistics of which property would you like to see?

- [P]opulation
- [D]iameter 
- [S]urface water");
var userInput = Console.ReadLine();
builder.Abfrage(userInput);



Console.ReadKey();




public interface IStarWarsReader
{
    Task<string> Read(string baseAddress, string requestUri);
}

public class StarWarsReader : IStarWarsReader
{
    public async Task<string> Read(string baseAddress, string requestUri)
    {
        using var client = new HttpClient();
        client.BaseAddress = new Uri(baseAddress);
        HttpResponseMessage response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}

public class Builder
{
    public readonly Root root;

    public Builder(Root root)
    {
        this.root = root;
    }



    public void Build()
    {
        Console.WriteLine("{0,-20} {1,-10} {2,-15} {3,-10}", "Name", "Diameter", "SurfaceWater", "Population");
        Console.WriteLine(new string('-', 60));
        foreach (var item in root.results)
        {
            Console.WriteLine("{0,-20} {1,-10} {2,-15} {3,-10}", item.name, item.diameter, item.surface_water, item.population);
        }

    }

    public void Abfrage(string userInput)
    {
        string[] validBuchstaben = new string[]
        {
            "P",
            "D",
            "S"
        };


        try
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                throw new ArgumentException("Falscher Buchstabe");
            }
            else if (!validBuchstaben.Contains(userInput))

            {
                throw new ArgumentOutOfRangeException();
            }

            switch (userInput)
            {
                case "P":
                    var smth = root.results.Where(x => int.TryParse(x.population, out _)).OrderByDescending(x => int.Parse(x.population)).First();
                    var smth1 = root.results.Where(x => int.TryParse(x.population, out _)).OrderByDescending(x => int.Parse(x.population)).Last();
                    Console.WriteLine($"Maximal population: {smth.name},{smth.population}");
                    Console.WriteLine($"Maximal population: {smth1.name},{smth1.population}");
                    break;
                case "D":
                    var smth2 = root.results.Where(x => int.TryParse(x.diameter, out _)).OrderByDescending(x => int.Parse(x.diameter)).First();
                    var smth3 = root.results.Where(x => int.TryParse(x.diameter, out _)).OrderByDescending(x => int.Parse(x.diameter)).Last();
                    Console.WriteLine($"Maximal diameter: {smth2.name},{smth2.diameter}");
                    Console.WriteLine($"Maximal diameter: {smth3.name},{smth3.diameter}");
                    break;
                case "S":
                    var smth4 = root.results.Where(x => int.TryParse(x.diameter, out _)).OrderByDescending(x => int.Parse(x.diameter)).First();
                    var smth5 = root.results.Where(x => int.TryParse(x.diameter, out _)).OrderByDescending(x => int.Parse(x.diameter)).Last();
                    Console.WriteLine($"Maximal surfacewater: {smth4.name},{smth4.surface_water}");
                    Console.WriteLine($"Minimal surfacewater: {smth5.name},{smth5.surface_water}");
                    break;
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Buchstabe außerhalb des Bereichs: {userInput}");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalider Buchstabe: {userInput}");
            Console.ResetColor();
        }





    }
}

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public record Result(
    [property: JsonPropertyName("name")] string name,
    [property: JsonPropertyName("rotation_period")] string rotation_period,
    [property: JsonPropertyName("orbital_period")] string orbital_period,
    [property: JsonPropertyName("diameter")] string diameter,
    [property: JsonPropertyName("climate")] string climate,
    [property: JsonPropertyName("gravity")] string gravity,
    [property: JsonPropertyName("terrain")] string terrain,
    [property: JsonPropertyName("surface_water")] string surface_water,
    [property: JsonPropertyName("population")] string population,
    [property: JsonPropertyName("residents")] IReadOnlyList<string> residents,
    [property: JsonPropertyName("films")] IReadOnlyList<string> films,
    [property: JsonPropertyName("created")] DateTime created,
    [property: JsonPropertyName("edited")] DateTime edited,
    [property: JsonPropertyName("url")] string url
);

public record Root(
    [property: JsonPropertyName("count")] int count,
    [property: JsonPropertyName("next")] string next,
    [property: JsonPropertyName("previous")] object previous,
    [property: JsonPropertyName("results")] IReadOnlyList<Result> results
);

