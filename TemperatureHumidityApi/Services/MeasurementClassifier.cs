namespace TemperatureHumidityApi.Services;

public static class MeasurementClassifier
{
    public static string ClassifyTemperature(double? temperature)
    {
        if (temperature is null)
            return "Unknown";

        if (temperature < 2)
            return "Critical";

        if (temperature <= 6)
            return "OK";

        if (temperature <= 8)
            return "Warning";

        if (temperature <= 10)
            return "Risk";

        return "Critical";
    }

    public static string ClassifyHumidity(double? humidity)
    {
        if (humidity is null || humidity < 0 || humidity > 100)
            return "Unknown";

        if (humidity < 20)
            return "Critical";

        if (humidity < 30)
            return "Risk";

        if (humidity < 40)
            return "Warning";

        if (humidity <= 60)
            return "OK";

        if (humidity <= 70)
            return "Warning";

        if (humidity <= 80)
            return "Risk";

        return "Critical";
    }
}