namespace LibVLCSharp.Maui.Platforms.Tizen;
public class VideoView : IVideoView
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
