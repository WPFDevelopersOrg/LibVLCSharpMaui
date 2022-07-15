using LibVLCSharp.Maui.Shared;

namespace LibVLCSharp.Maui.Platforms.Windows;
public abstract class VideoViewBase<TEventArgs> : VideoViewBase, IVideoViewBase<TEventArgs> where TEventArgs : EventArgs
{
    public event EventHandler<TEventArgs>? Initialized;

    protected abstract TEventArgs CreateInitializedEventArgs();

    protected override void OnInitialized()
    {
        Initialized?.Invoke(this, CreateInitializedEventArgs());
    }
}
