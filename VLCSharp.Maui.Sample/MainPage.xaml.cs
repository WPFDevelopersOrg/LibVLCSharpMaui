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
        _LibVLC = new(SwapChainOptions);

        VLC_MediaView.MediaPlayer = new LibVlcshared.MediaPlayer(_LibVLC);
        var media = new LibVlcshared.Media(_LibVLC, new Uri("https://tb-video.bdstatic.com/tieba-smallvideo-transcode-cae/14_85ebc1dea846ccb3c6dca9ffe3a88363_0_0_cae3.mp4?vt=0&pt=3&ver=&cr=2&cd=0&sid=&ft=2&tbau=2022-11-15_d532423a1e5fa98b5381181c3a51fdd90e71d0e0383e6f4b744d6dfce63e6809&ptid=8047416825"));
        VLC_MediaView.MediaPlayer.Play(media);
    }

    private void VLC_MediaView_Initialized(object sender, LibVLCSharp.Maui.Events.VLCInitilizedeventArgs e)
    {
        if (e is null)
            return;

        SwapChainOptions = e.SwapChainOptions;
    }
}

