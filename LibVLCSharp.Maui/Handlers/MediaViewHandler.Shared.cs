using LibVLCSharp.Maui.Shared;
using Microsoft.Maui.Handlers;

namespace LibVLCSharp.Maui;
public partial class MediaViewHandler : ViewHandler<IMediaView, object>
{
    protected override object CreatePlatformView() => throw new NotImplementedException();

    public static void MapMediaPlayer(MediaViewHandler handler, IMediaView view);
}
