using Microsoft.UI.Xaml.Controls;

namespace LibVLCSharp.Maui.Platforms.Windows;
public class VideoView : Control, IVideoView
{
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

    void Attach()
    {
        if (MediaPlayer == null || MediaPlayer.NativeReference == IntPtr.Zero)
            return;

        //MediaPlayer.Hwnd = Han ;
    }

    void Detach()
    {
        if (MediaPlayer == null || MediaPlayer.NativeReference == IntPtr.Zero)
            return;

        MediaPlayer.Hwnd = IntPtr.Zero;
    }

}
