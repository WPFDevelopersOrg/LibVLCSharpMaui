using LibVLCSharp.Shared;

namespace VLCSharp.Maui.Sample;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();

        Core.Initialize();
    }
}
