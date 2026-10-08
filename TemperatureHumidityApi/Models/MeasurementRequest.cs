namespace TemperatureHumidityApi.Models;

public class MeasurementRequest
{
    public DateTimeOffset? Time { get; set; }

    public double? Temperature { get; set; }

    public double? Humidity { get; set; }

    public string? Id { get; set; }
}