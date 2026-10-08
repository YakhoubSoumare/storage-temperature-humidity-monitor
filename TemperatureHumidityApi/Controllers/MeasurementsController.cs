using Microsoft.AspNetCore.Mvc;
using TemperatureHumidityApi.Models;
using TemperatureHumidityApi.Services;

namespace TemperatureHumidityApi.Controllers;

[ApiController]
[Route("api/measurements")]
public class MeasurementsController : ControllerBase
{

    [HttpPost]
    public IActionResult Post(MeasurementRequest request)
    {
        string temperatureLevel =
            MeasurementClassifier.ClassifyTemperature(
                request.Temperature);

        string humidityLevel =
            MeasurementClassifier.ClassifyHumidity(
                request.Humidity);

        string overallLevel =
            MeasurementClassifier.ClassifyOverall(
                temperatureLevel,
                humidityLevel);

        return Ok(new
        {
            request.Id,
            request.Time,
            request.Temperature,
            request.Humidity,
            temperatureLevel,
            humidityLevel,
            overallLevel
        });
    }
}