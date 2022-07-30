using LibVLCSharp.Maui.Events;

namespace LibVLCSharp.Maui.Platforms.Windows;
public class VideoViewx : VideoViewBase<VLCInitilizedeventArgs>, IVideoView
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

    protected override VLCInitilizedeventArgs CreateInitializedEventArgs()
    {
        return new VLCInitilizedeventArgs(SwapChainOptions);
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

        //MediaPlayer.Hwnd = IntPtr.Zero;
    }

}
