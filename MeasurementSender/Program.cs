using System.Text;

using var client = new HttpClient();

const string endpoint =
    "http://localhost:5152/api/measurements";

string repoRoot = FindRepoRoot();
string inputFile = Path.Combine(
    repoRoot,
    "data",
    "simulated-readings.jsonl");

foreach (string line in File.ReadLines(inputFile))
{
    if (string.IsNullOrWhiteSpace(line))
        continue;

    using var content = new StringContent(
        line,
        Encoding.UTF8,
        "application/json");

    using var response =
        await client.PostAsync(endpoint, content);

    string body =
        await response.Content.ReadAsStringAsync();

    Console.WriteLine(
        $"{(int)response.StatusCode} | {body}");
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