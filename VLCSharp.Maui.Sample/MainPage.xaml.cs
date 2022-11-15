using LibVLCSharp.Shared;
using System.IO;
using MediaPlayerX = LibVLCSharp.Shared.MediaPlayer;

namespace VLCSharp.Maui.Sample;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        Source.Text = "https://tb-video.bdstatic.com/tieba-smallvideo-transcode-cae/14_85ebc1dea846ccb3c6dca9ffe3a88363_0_0_cae3.mp4?vt=0&pt=3&ver=&cr=2&cd=0&sid=&ft=2&tbau=2022-11-15_d532423a1e5fa98b5381181c3a51fdd90e71d0e0383e6f4b744d6dfce63e6809&ptid=8047416825";
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Source.Text))
        {
            Media.MediaPlayer?.Stop();

            LibVLC lib = new();
            Media media = new(lib, new Uri(Source.Text));
            Media.MediaPlayer = new MediaPlayerX(media);
            Media.MediaPlayer.Play();
        }
    }
}

