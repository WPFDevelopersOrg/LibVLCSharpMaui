using LibVLCSharp.Maui.Shared;

namespace LibVLCSharp.Maui;
public partial class MediaViewHandler
{
    public static IPropertyMapper<IMediaView, MediaViewHandler> Mapper = new PropertyMapper<IMediaView, MediaViewHandler>(ViewMapper)
    {
        [nameof(IMediaView.MediaPlayer)] = MapMediaPlayer,
    };

    public MediaViewHandler() : base(Mapper)
    {

    }

    public MediaViewHandler(IPropertyMapper mapper) : base(mapper ?? Mapper)
    {

    }
    public MediaViewHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }
}
