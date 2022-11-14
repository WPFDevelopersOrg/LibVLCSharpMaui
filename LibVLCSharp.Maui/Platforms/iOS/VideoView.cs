using LibVLCSharp.Maui.Events;
using LibVLCSharp.Maui.Shared;
using UIKit;

namespace LibVLCSharp.Maui.Platforms.iOS;
public class VideoView : UIView, IVideoView, IVideoViewBase<VLCInitilizedeventArgs>
{

    MediaPlayerX? _mediaPlayer;

    public event EventHandler<VLCInitilizedeventArgs>? Initialized;

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

        Initialized?.Invoke(this, new(Array.Empty<string>()));
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
