using Projekt_Wlasny.Models;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Text.Json;
using Projekt_Wlasny.Models;

namespace Projekt_Wlasny
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Counter> counters = new ObservableCollection<Counter>();

        CounterStorage storage = new CounterStorage();

        public MainPage()
        {
            InitializeComponent();
            countersCollection.ItemsSource = counters;

            var savedCounters = storage.LoadCounters();
            foreach (Counter counter in savedCounters)
            {
                counters.Add(counter);
            }
        }

        private async void AddCounter(object sender, EventArgs e)
        {
            int initialValue = 0;

            if (!int.TryParse(initialValueEntry.Text, out initialValue))
            {
                await DisplayAlert(
                    "Błąd",
                    "Podaj poprawną wartość początkową licznika (liczbę całkowitą).",
                    "OK");

                return;
            }

            Counter newCounter = new Counter
            {
                name = string.IsNullOrWhiteSpace(nameEntry.Text) ? "Nowy licznik" : nameEntry.Text,
                clickCount = initialValue
            };

            counters.Add(newCounter);
            storage.SaveCounters(counters);

            nameEntry.Text = "";
            initialValueEntry.Text = "";
        }

        private async void OpenCounter(object sender, SelectionChangedEventArgs e)
        {
            Counter selectedCounter = e.CurrentSelection.FirstOrDefault() as Counter;

            if (selectedCounter != null)
            {
                await Shell.Current.GoToAsync(
                    nameof(ElementPage),
                    new Dictionary<string, object>
                    {
                        { "SelectedCounter", selectedCounter },
                        { "Counters", counters }
                    });
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            countersCollection.ItemsSource = null;
            countersCollection.ItemsSource = counters;

            countersCollection.SelectedItem = null;
        }
    }
}
