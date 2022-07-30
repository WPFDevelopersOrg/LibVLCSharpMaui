using LibVlcshared = LibVLCSharp.Shared;

namespace VLCSharp.Maui.Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    LibVlcshared.LibVLC? _LibVLC;
    //LibVlcshared.MediaPlayer? _MediaPlayer;

    string[]? SwapChainOptions;

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    private void OnCounterClicked(object sender, EventArgs e)
	{
        _LibVLC = new();

        VLC_MediaView.MediaPlayer = new LibVlcshared.MediaPlayer(_LibVLC);
        var media = new LibVlcshared.Media(_LibVLC, new Uri("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4"));
        VLC_MediaView.MediaPlayer.Play(media);
    }

    private void VLC_MediaView_Initialized(object sender, LibVLCSharp.Maui.Events.VLCInitilizedeventArgs e)
    {
        if (e is null)
            return;

        SwapChainOptions = e.SwapChainOptions;
    }
}

