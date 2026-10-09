# Storage Temperature & Humidity Monitor

A small demo project that simulates temperature and humidity values, saves them to a file, sends them to an API, and classifies the result.

## How it works

```text
Wokwi
  ↓
SimulatedDataCapture
  ↓
data/simulated-readings.jsonl
  ↓
MeasurementSender
  ↓
TemperatureHumidityApi
  ↓
data/classification-results.jsonl
```

## Flow Diagram

```txt
+-----------------------------+
| Wokwi ESP32 test data       |
| temperature / humidity / id |
+-------------+---------------+
              |
              v
+-----------------------------+
| SimulatedDataCapture        |
| reads serial data           |
+-------------+---------------+
              |
              v
+-----------------------------+
| simulated-readings.jsonl    |
+-------------+---------------+
              |
              v
+-----------------------------+
| MeasurementSender           |
| reads one line at a time    |
| sends HTTP POST             |
+-------------+---------------+
              |
              v
+-----------------------------+
| TemperatureHumidityApi      |
| receives measurement        |
+-------------+---------------+
              |
              v
+-----------------------------+
| Classify temperature        |
| OK / Warning / Risk /       |
| Critical / Unknown          |
+-------------+---------------+
              |
              v
+-----------------------------+
| Classify humidity           |
| OK / Warning / Risk /       |
| Critical / Unknown          |
+-------------+---------------+
              |
              v
+-----------------------------+
| Calculate overall level     |
| most serious level wins     |
+-------------+---------------+
              |
              v
+-----------------------------+
| API response                |
| temperatureLevel            |
| humidityLevel               |
| overallLevel                |
+-------------+---------------+
              |
              v
+-----------------------------+
| classification-results.jsonl|
+-----------------------------+
```

## Projects

- `firmware/wokwi`  
  Generates test values for temperature and humidity.

- `SimulatedDataCapture`  
  Reads data from Wokwi and saves it as JSONL.

- `MeasurementSender`  
  Reads the saved measurements and sends them to the API.

- `TemperatureHumidityApi`  
  Classifies temperature, humidity, and overall status.

- `TemperatureHumidityApi.Tests`  
  Tests the classification rules.

## Example input

```json
{
  "time": null,
  "temperature": 5,
  "humidity": 65,
  "id": "case-050"
}
```

## Example result

```json
{
  "id": "case-050",
  "time": null,
  "temperature": 5,
  "humidity": 65,
  "temperatureLevel": "OK",
  "humidityLevel": "Warning",
  "overallLevel": "Warning"
}
```

## Classification rules

### Temperature

| Temperature | Level |
|---|---|
| `< 2` | Critical |
| `2 - 6` | OK |
| `> 6 - 8` | Warning |
| `> 8 - 10` | Risk |
| `> 10` | Critical |
| `null` | Unknown |

### Humidity

| Humidity | Level |
|---|---|
| `< 0` or `> 100` | Unknown |
| `0 - <20` | Critical |
| `20 - <30` | Risk |
| `30 - <40` | Warning |
| `40 - 60` | OK |
| `>60 - 70` | Warning |
| `>70 - 80` | Risk |
| `>80 - 100` | Critical |
| `null` | Unknown |

The overall level is based on the most serious result from temperature and humidity.

These rules are only used for this demo.

## Run

### 1. Build the firmware

Build the PlatformIO project in:

```text
firmware/wokwi
```

### 2. Start Wokwi

Start the Wokwi simulation.

### 3. Capture the data

From the repo root:

```bash
dotnet run --project SimulatedDataCapture
```

This creates:

```text
data/simulated-readings.jsonl
```

### 4. Start the API

```bash
dotnet run --project TemperatureHumidityApi
```

The API runs on:

```text
http://localhost:5152
```

Endpoint:

```text
POST /api/measurements
```

### 5. Send the measurements

In another terminal:

```bash
dotnet run --project MeasurementSender
```

The results are saved to:

```text
data/classification-results.jsonl
```

## Tests

Run:

```bash
dotnet test TemperatureHumidityApi.Tests/TemperatureHumidityApi.Tests.csproj
```

## Note

The current values are generated test cases. They are not readings from a real sensor.

The test cases are fixed so the same values can be used every time.
