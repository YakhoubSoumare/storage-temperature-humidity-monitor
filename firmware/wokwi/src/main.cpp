#include <Arduino.h>

#include <math.h>

struct TestReading {
  const char* id;
  float temperature;
  float humidity;
};

const TestReading testReadings[] = {
  {"case-001", 5.0, 50.0},

  {"case-002", 1.99, 50.0},
  {"case-003", 2.0, 50.0},
  {"case-004", 2.01, 50.0},

  {"case-005", 5.99, 50.0},
  {"case-006", 6.0, 50.0},
  {"case-007", 6.01, 50.0},

  {"case-008", 7.99, 50.0},
  {"case-009", 8.0, 50.0},
  {"case-010", 8.01, 50.0},

  {"case-011", 9.99, 50.0},
  {"case-012", 10.0, 50.0},
  {"case-013", 10.01, 50.0},

  {"case-014", 0.0, 50.0},
  {"case-015", -100.0, 50.0},
  {"case-016", 150.0, 50.0},

  {"case-017", 5.0, -0.01},
  {"case-018", 5.0, 0.0},
  {"case-019", 5.0, 0.01},

  {"case-020", 5.0, 19.99},
  {"case-021", 5.0, 20.0},
  {"case-022", 5.0, 20.01},

  {"case-023", 5.0, 29.99},
  {"case-024", 5.0, 30.0},
  {"case-025", 5.0, 30.01},

  {"case-026", 5.0, 39.99},
  {"case-027", 5.0, 40.0},
  {"case-028", 5.0, 40.01},

  {"case-029", 5.0, 59.99},
  {"case-030", 5.0, 60.0},
  {"case-031", 5.0, 60.01},

  {"case-032", 5.0, 69.99},
  {"case-033", 5.0, 70.0},
  {"case-034", 5.0, 70.01},

  {"case-035", 5.0, 79.99},
  {"case-036", 5.0, 80.0},
  {"case-037", 5.0, 80.01},

  {"case-038", 5.0, 99.99},
  {"case-039", 5.0, 100.0},
  {"case-040", 5.0, 100.01},

  {"case-041", 5.0, -50.0},
  {"case-042", 5.0, 150.0},

  {"case-043", NAN, 50.0},
  {"case-044", INFINITY, 50.0},
  {"case-045", -INFINITY, 50.0},

  {"case-046", 5.0, NAN},
  {"case-047", 5.0, INFINITY},
  {"case-048", 5.0, -INFINITY},

  {"case-049", 5.0, 50.0},
  {"case-050", 5.0, 65.0},
  {"case-051", 5.0, 75.0},
  {"case-052", 5.0, 85.0},
  {"case-053", 5.0, NAN},

  {"case-054", 7.0, 50.0},
  {"case-055", 7.0, 65.0},
  {"case-056", 7.0, 75.0},
  {"case-057", 7.0, 85.0},
  {"case-058", 7.0, NAN},

  {"case-059", 9.0, 50.0},
  {"case-060", 9.0, 65.0},
  {"case-061", 9.0, 75.0},
  {"case-062", 9.0, 85.0},
  {"case-063", 9.0, NAN},

  {"case-064", 11.0, 50.0},
  {"case-065", 11.0, 65.0},
  {"case-066", 11.0, 75.0},
  {"case-067", 11.0, 85.0},
  {"case-068", 11.0, NAN},

  {"case-069", NAN, 50.0},
  {"case-070", NAN, 65.0},
  {"case-071", NAN, 75.0},
  {"case-072", NAN, 85.0},
  {"case-073", NAN, NAN},

  {"case-074", 1.0, 15.0},
  {"case-075", 7.0, 25.0},
  {"case-076", 9.0, 35.0},

  {"case-077", 11.0, -1.0},
  {"case-078", 11.0, 101.0},

  {"case-079", 5.0, 50.0},
  {"case-080", 7.0, 65.0},
  {"case-081", 9.0, 75.0},
  {"case-082", 11.0, 85.0},
  {"case-083", NAN, NAN},
  {"case-084", 11.0, 85.0},
  {"case-085", 9.0, 75.0},
  {"case-086", 7.0, 65.0},
  {"case-087", 5.0, 50.0},

  {"case-088", 6.0, 60.0},
  {"case-089", 6.01, 60.01},
  {"case-090", 6.0, 60.0},
  {"case-091", 6.01, 60.01},
  {"case-092", 5.0, 50.0}
};

const size_t testReadingCount =
  sizeof(testReadings) / sizeof(testReadings[0]);

size_t testIndex = 0;

void printJsonNumber(float value) {
  if (isfinite(value)) {
    Serial.print(value, 2);
  } else {
    Serial.print("null");
  }
}

void setup() {
  Serial.begin(115200);
}

void loop() {
  if (testIndex >= testReadingCount) {
    return;
  }

  const TestReading& reading = testReadings[testIndex];

  Serial.print("{\"time\":null");

  Serial.print(",\"temperature\":");
  printJsonNumber(reading.temperature);

  Serial.print(",\"humidity\":");
  printJsonNumber(reading.humidity);

  Serial.print(",\"id\":\"");
  Serial.print(reading.id);
  Serial.print("\"");

  Serial.println("}");

  testIndex++;

  delay(500);
}