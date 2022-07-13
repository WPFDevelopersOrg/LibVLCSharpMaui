using UIKit;

namespace LibVLCSharp.Maui.Platforms.iOS;
public class VideoView : UIView, IVideoView
{

    MediaPlayerX? _mediaPlayer;
    public MediaPlayerX? MediaPlayer
    {
        get => _mediaPlayer;
        set
        {
            if (_mediaPlayer != value)
            {
                Detach();
                _mediaPlayer = value;

                if (_mediaPlayer != null)
                    Attach();
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    void Attach()
    {
        if (MediaPlayer != null && MediaPlayer.NativeReference != IntPtr.Zero)
            MediaPlayer.NsObject = Handle;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    void Detach()
    {
        if (MediaPlayer != null && MediaPlayer.NativeReference != IntPtr.Zero)
            MediaPlayer.NsObject = IntPtr.Zero;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Detach();
    }
}
