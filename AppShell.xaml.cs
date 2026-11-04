namespace Projekt_Wlasny;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            nameof(ElementPage),
            typeof(ElementPage));
    }
}