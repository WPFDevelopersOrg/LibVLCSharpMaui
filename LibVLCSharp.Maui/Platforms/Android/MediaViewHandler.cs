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
        var view = new VideoViewX(Context);
        view.Initialized += View_Initialized;
        return view;
    }

    private void View_Initialized(object? sender, Events.VLCInitilizedeventArgs e)
    {
        VirtualView.TiggerEvent(e);
    }

    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view)
    {
        handler.PlatformView.MediaPlayer = view.MediaPlayer;
    }
}