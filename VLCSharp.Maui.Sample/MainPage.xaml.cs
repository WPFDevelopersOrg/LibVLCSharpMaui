using LibVlcshared = LibVLCSharp.Shared;

namespace VLCSharp.Maui.Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

    LibVlcshared.LibVLC? _LibVLC;
    LibVlcshared.MediaPlayer? _MediaPlayer;

    string[]? SwapChainOptions;

    [SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    private void OnCounterClicked(object sender, EventArgs e)
	{
#if WINDOWS
        if(SwapChainOptions is not null)
            _LibVLC = new(enableDebugLogs: true, SwapChainOptions);
        else
            _LibVLC = new(enableDebugLogs: true);
#else
        _LibVLC = new(enableDebugLogs: true);
#endif

        using var media = new LibVlcshared.Media(_LibVLC, new Uri("https://www.bilibili.com/video/BV1kF411K7Dh?t=0.0"));
        _MediaPlayer = new LibVlcshared.MediaPlayer(_LibVLC);
        VLC_MediaView.MediaPlayer = _MediaPlayer;
       _MediaPlayer.Play(media);
    }

    private void VLC_MediaView_Initialized(object sender, LibVLCSharp.Maui.Events.VLCInitilizedeventArgs e)
    {
        if (e is null)
            return;

        SwapChainOptions = e.SwapChainOptions;
    }
}

