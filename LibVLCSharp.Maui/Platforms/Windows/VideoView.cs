using LibVLCSharp.Maui.Events;
using LibVLCSharp.Maui.Shared;
using Microsoft.Maui.Platform;
using WinRT;
using Microsoftuixaml = Microsoft.UI.Xaml;
using MicrosoftuixamlControls= Microsoft.UI.Xaml.Controls;

namespace LibVLCSharp.Maui.Platforms.Windows;
public class VideoView : MicrosoftuixamlControls.ContentControl, IVideoView, IVideoViewBase<VLCInitilizedeventArgs>
{
    public VideoView()
    {
        Content = "123123123232323";
    }

    MediaPlayerX? _mediaPlayer;
    public MediaPlayerX? MediaPlayer
    {
        get => _mediaPlayer;
        set
        {
            if (ReferenceEquals(_mediaPlayer, value))
            {
                return;
            }

            Detach();
            _mediaPlayer = value;
            Attach();
        }
    }


    public event EventHandler<VLCInitilizedeventArgs>? Initialized;


    void Attach()
    {
        if (MediaPlayer == null || MediaPlayer.NativeReference == IntPtr.Zero)
            return;

        Microsoftuixaml.Window window = new Microsoftuixaml.Window();
        var handle = window.GetWindowHandle();
        var appWindow = window.GetAppWindow();
        appWindow?.Show();
        //Microsoftuixaml.Application.Current.


        if (this is not IWinRTObject iWinRTObject)
            return;
 

        MediaPlayer.Hwnd = handle;
    }

    void Detach()
    {
        if (MediaPlayer == null || MediaPlayer.NativeReference == IntPtr.Zero)
            return;

        MediaPlayer.Hwnd = IntPtr.Zero;
    }

}
