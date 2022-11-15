using LibVLCSharp.Maui.Platforms.Windows;
using LibVLCSharp.Maui.Shared;
using Microsoft.Maui.Handlers;

namespace LibVLCSharp.Maui;

// All the code in this file is only included on Windows.
public partial class MediaViewHandler : ViewHandler<IMediaView, VideoViewFrame>
{
    protected override VideoViewFrame CreatePlatformView()
    {
        var view = new VideoViewFrame();
        view.Initialized += View_Initialized;
        return view;
    }

    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view)
    {
        handler.PlatformView.MediaPlayer = view.MediaPlayer;
    }

    private void View_Initialized(object? sender, Events.VLCInitilizedeventArgs e)
    {
        VirtualView.TiggerEvent(e);
    }
}