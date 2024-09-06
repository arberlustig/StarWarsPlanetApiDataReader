
//var weatherData = new WeatherDatastruc(65, 25.1m);

//Console.WriteLine(weatherData);
//Console.ReadKey();

public record WeatherDatastruc(int Humidity, decimal Temperature);

public class WeatherData : IEquatable<WeatherData?>
{
    public decimal Temperature { get; }
    public int Humidity { get; }

    public WeatherData(int humidity, decimal temperature)
    {
        Humidity = humidity;
        Temperature = temperature;
    }

    public override string ToString()
    {
        return $"Temperature: {Temperature}, Humidity: {Humidity}";
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as WeatherData);
    }

    public bool Equals(WeatherData? other)
    {
        return other is not null &&
               Temperature == other.Temperature &&
               Humidity == other.Humidity;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Temperature, Humidity);
    }

    public static bool operator ==(WeatherData? left, WeatherData? right)
    {
        return EqualityComparer<WeatherData>.Default.Equals(left, right);
    }

    public static bool operator !=(WeatherData? left, WeatherData? right)
    {
        return !(left == right);
    }
}