using LibVLCSharp.Maui.Shared;
using LibVLCSharp.Platforms.Android;
using Microsoft.Maui.Handlers;
using PlatformApplication = Android.App.Application;

namespace LibVLCSharp.Maui;

// All the code in this file is only included on Android.
public partial class MediaViewHandler : ViewHandler<IMediaView, VideoView>
{

    protected override VideoView CreatePlatformView()
    {
        return new VideoView(PlatformApplication.Context);
    }

    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view)
    {

    }
}