using TemperatureHumidityApi.Services;

namespace TemperatureHumidityApi.Tests;

public class MeasurementClassifierTests
{
    [Theory]
    [InlineData(null, "Unknown")]
    [InlineData(-100.0, "Critical")]
    [InlineData(1.99, "Critical")]
    [InlineData(2.0, "OK")]
    [InlineData(2.01, "OK")]
    [InlineData(5.99, "OK")]
    [InlineData(6.0, "OK")]
    [InlineData(6.01, "Warning")]
    [InlineData(7.99, "Warning")]
    [InlineData(8.0, "Warning")]
    [InlineData(8.01, "Risk")]
    [InlineData(9.99, "Risk")]
    [InlineData(10.0, "Risk")]
    [InlineData(10.01, "Critical")]
    [InlineData(150.0, "Critical")]
    public void ClassifyTemperature_ReturnsExpectedLevel(
        double? temperature,
        string expected)
    {
        string result =
            MeasurementClassifier.ClassifyTemperature(
                temperature);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(null, "Unknown")]
    [InlineData(-50.0, "Unknown")]
    [InlineData(-0.01, "Unknown")]
    [InlineData(0.0, "Critical")]
    [InlineData(0.01, "Critical")]
    [InlineData(19.99, "Critical")]
    [InlineData(20.0, "Risk")]
    [InlineData(20.01, "Risk")]
    [InlineData(29.99, "Risk")]
    [InlineData(30.0, "Warning")]
    [InlineData(30.01, "Warning")]
    [InlineData(39.99, "Warning")]
    [InlineData(40.0, "OK")]
    [InlineData(40.01, "OK")]
    [InlineData(59.99, "OK")]
    [InlineData(60.0, "OK")]
    [InlineData(60.01, "Warning")]
    [InlineData(69.99, "Warning")]
    [InlineData(70.0, "Warning")]
    [InlineData(70.01, "Risk")]
    [InlineData(79.99, "Risk")]
    [InlineData(80.0, "Risk")]
    [InlineData(80.01, "Critical")]
    [InlineData(99.99, "Critical")]
    [InlineData(100.0, "Critical")]
    [InlineData(100.01, "Unknown")]
    [InlineData(150.0, "Unknown")]
    public void ClassifyHumidity_ReturnsExpectedLevel(
        double? humidity,
        string expected)
    {
        string result =
            MeasurementClassifier.ClassifyHumidity(
                humidity);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("OK", "OK", "OK")]
    [InlineData("OK", "Warning", "Warning")]
    [InlineData("Warning", "OK", "Warning")]
    [InlineData("Risk", "OK", "Risk")]
    [InlineData("OK", "Risk", "Risk")]
    [InlineData("Critical", "OK", "Critical")]
    [InlineData("OK", "Critical", "Critical")]
    [InlineData("Risk", "Warning", "Risk")]
    [InlineData("Critical", "Risk", "Critical")]
    [InlineData("Unknown", "OK", "Unknown")]
    [InlineData("OK", "Unknown", "Unknown")]
    [InlineData("Unknown", "Critical", "Critical")]
    public void ClassifyOverall_ReturnsExpectedLevel(
        string temperatureLevel,
        string humidityLevel,
        string expected)
    {
        string result =
            MeasurementClassifier.ClassifyOverall(
                temperatureLevel,
                humidityLevel);

        Assert.Equal(expected, result);
    }
}