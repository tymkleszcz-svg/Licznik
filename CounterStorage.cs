using System.Collections.ObjectModel;
using System.Text.Json;

namespace Projekt_Wlasny;

public class CounterStorage
{
    string filePath = Path.Combine(
        FileSystem.AppDataDirectory,
        "counters.json"
    );

    public CounterStorage()
    {
        System.Diagnostics.Debug.WriteLine("ŒCIE¯KA PLIKU: " + filePath);
    }

    public void SaveCounters(ObservableCollection<Counter> counters)
    {
        string json = JsonSerializer.Serialize(counters);
        File.WriteAllText(filePath, json);
    }

    public ObservableCollection<Counter> LoadCounters()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var counters = JsonSerializer.Deserialize<ObservableCollection<Counter>>(json);

            if (counters != null)
            {
                return counters;
            }
        }
        return new ObservableCollection<Counter>();
    }
}