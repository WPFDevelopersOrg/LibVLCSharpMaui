using LibVLCSharp.Maui.Platforms.iOS;
using LibVLCSharp.Maui.Shared;
using Microsoft.Maui.Handlers;

namespace LibVLCSharp.Maui;

// All the code in this file is only included on iOS.
public partial class MediaViewHandler : ViewHandler<IMediaView, VideoView>
{
    protected override VideoView CreatePlatformView()
    {
        var view = new VideoView();
        view.Initialized += View_Initialized;
        return view;
    }

    private void View_Initialized(object? sender, Events.VLCInitilizedeventArgs e)
    {
        VirtualView.TiggerEvent(e);
    }

    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view)
    {
        handler.PlatformView.MediaPlayer = view.MediaPlayer;
    }
}