using LibVLCSharp.Maui.Shared;
using LibVLCSharp.Platforms.Android;
using Microsoft.Maui.Handlers;

namespace LibVLCSharp.Maui;

// All the code in this file is only included on Android.
public partial class MediaViewHandler : ViewHandler<IMediaView, VideoView>
{

    protected override VideoView CreatePlatformView()
    {
        throw new NotImplementedException();
    }

    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view)
    {

    }
}