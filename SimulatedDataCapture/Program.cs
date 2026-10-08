using System.Net.Sockets;
using System.Text;
using System.Text.Json;

const string host = "localhost";
const int port = 4000;

string repoRoot = FindRepoRoot();
string dataDirectory = Path.Combine(repoRoot, "data");
string outputFile = Path.Combine(dataDirectory, "simulated-readings.jsonl");

Directory.CreateDirectory(dataDirectory);

using var client = new TcpClient();

Console.WriteLine($"Connecting to Wokwi at {host}:{port}...");
await client.ConnectAsync(host, port);

Console.WriteLine("Connected.");
Console.WriteLine($"Writing to: {outputFile}");

await using var writer = new StreamWriter(
    outputFile,
    append: false,
    Encoding.UTF8);

using NetworkStream stream = client.GetStream();

var buffer = new byte[1024];
var lineBuffer = new StringBuilder();

while (true)
{
    using var timeout = new CancellationTokenSource(
        TimeSpan.FromSeconds(3));

    int bytesRead;

    try
    {
        bytesRead = await stream.ReadAsync(
            buffer,
            timeout.Token);
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("No more data. Capture complete.");
        break;
    }

    if (bytesRead == 0)
        break;

    for (int i = 0; i < bytesRead; i++)
    {
        char character = (char)buffer[i];

        if (character == '\n')
        {
            string line = lineBuffer
                .ToString()
                .Trim();

            lineBuffer.Clear();

            if (!IsJson(line))
                continue;

            await writer.WriteLineAsync(line);
            await writer.FlushAsync();

            Console.WriteLine(line);
        }
        else if (character != '\r')
        {
            lineBuffer.Append(character);
        }
    }
}

Console.WriteLine("Done.");

static bool IsJson(string line)
{
    if (!line.StartsWith('{'))
        return false;

    try
    {
        using JsonDocument _ = JsonDocument.Parse(line);
        return true;
    }
    catch (JsonException)
    {
        return false;
    }
}

static string FindRepoRoot()
{
    var directory = new DirectoryInfo(
        Directory.GetCurrentDirectory());

    while (directory is not null)
    {
        if (Directory.Exists(
            Path.Combine(directory.FullName, ".git")))
        {
            return directory.FullName;
        }

        directory = directory.Parent;
    }

    throw new InvalidOperationException(
        "Git repository root could not be found.");
}