namespace LibVLCSharp.Maui.Shared
{
    // All the code in this file is included in all platforms.
    public interface IMediaView : IView
    {
        MediaPlayerX? MediaPlayer { get; set; }
    }
}