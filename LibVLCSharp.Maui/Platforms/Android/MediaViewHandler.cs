using LibVLCSharp.Maui.Platforms.Android;
using LibVLCSharp.Maui.Shared;
using Microsoft.Maui.Handlers;
using PlatformApplication = Android.App.Application;

namespace LibVLCSharp.Maui;

// All the code in this file is only included on Android.
public partial class MediaViewHandler : ViewHandler<IMediaView, VideoViewX>
{

    protected override VideoViewX CreatePlatformView()
    {
        return new VideoViewX(Context);
    }

    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view)
    {
        handler.PlatformView.MediaPlayer = view.MediaPlayer;
    }
}