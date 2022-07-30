using LibVLCSharp.Maui.Events;
using LibVLCSharp.Maui.Shared;

namespace LibVLCSharp.Maui.Platforms.Tizen;

public class VideoView : IVideoView, IVideoViewBase<VLCInitilizedeventArgs>
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

    public event EventHandler<VLCInitilizedeventArgs>? Initialized;

    void Attach()
    {
         
    }

    void Detach()
    {
         
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        Detach();
    }
}
