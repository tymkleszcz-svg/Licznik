using System.Collections.ObjectModel;

namespace Projekt_Wlasny;

[QueryProperty(nameof(SelectedCounter), nameof(SelectedCounter))]
[QueryProperty(nameof(Counters), nameof(Counters))]
public partial class ElementPage : ContentPage
{
    Counter counter;
    int count = 0;

    ObservableCollection<Counter> counters;

    public ElementPage()
    {
        InitializeComponent();
    }

    public Counter SelectedCounter
    {
        set
        {
            counter = value;
            Title = counter.name;
            nameEntry.Text = counter.name;
            count = counter.clickCount;
            counterLabel.Text = $"Wartosc: {count}";
        }
    }

    public ObservableCollection<Counter> Counters
    {
        set
        {
            counters = value;
        }
    }

    private async void GoBack(object sender, EventArgs e)
    {
        counter.name = nameEntry.Text;

        CounterStorage storage = new CounterStorage();
        storage.SaveCounters(counters);

        await Shell.Current.GoToAsync("..");
    }

    private void IncrementCounter(object sender, EventArgs e)
    {
        count++;
        counterLabel.Text = $"Wartosc: {count}";
        AutoSave();
        SemanticScreenReader.Announce(counterLabel.Text);
    }

    private void DecrementCounter(object sender, EventArgs e)
    {
        count--;
        counterLabel.Text = $"Wartosc: {count}";
        AutoSave();
        SemanticScreenReader.Announce(counterLabel.Text);
    }

    private void AutoSave()
    {
        counter.clickCount = count;

        CounterStorage storage = new CounterStorage();
        storage.SaveCounters(counters);
    }
}