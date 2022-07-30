using LibVLCSharp.Maui.Events;
using LibVLCSharp.Maui.Shared;

namespace LibVLCSharp.Maui.Controls;
public class MediaView : View, IMediaView
{
    public static readonly BindableProperty MediaPlayerProperty = BindableProperty.Create(nameof(MediaPlayer), typeof(MediaPlayerX), typeof(MediaView), default, propertyChanged: (bindable, oldValue, newValue) =>
    {
        if (bindable is not MediaView mediaView)
            return;

        MediaPlayerX? oldValuePlayer = default;
        if (oldValue is MediaPlayerX oldPlayer)
            oldValuePlayer = oldPlayer;

        MediaPlayerX? newValuePlayer = default;
        if (newValue is MediaPlayerX newPlayer)
            newValuePlayer = newPlayer;

        mediaView.MediaPlayerChanged?.Invoke(bindable, new MediaPlayerChangedEventArgs(oldValuePlayer, newValuePlayer));
    }, defaultBindingMode: BindingMode.TwoWay);

    public MediaPlayerX? MediaPlayer 
    { 
        get => (MediaPlayerX?)GetValue(MediaPlayerProperty); 
        set => SetValue(MediaPlayerProperty, value); 
    }

    public event EventHandler<VLCInitilizedeventArgs>? Initialized;

    public event EventHandler<MediaPlayerChangedEventArgs>? MediaPlayerChanged;

    bool IMediaView.TiggerEvent(VLCInitilizedeventArgs arg)
    {
        Initialized?.Invoke(this, arg);
        return true;
    }
}
