namespace LibVLCSharp.Maui.Platforms.Windows;
public abstract class VideoViewBase<TEventArgs> : VideoViewBase where TEventArgs : EventArgs
{
    public event EventHandler<TEventArgs>? Initialized;

    protected abstract TEventArgs CreateInitializedEventArgs();

    protected override void OnInitialized()
    {
        Initialized?.Invoke(this, CreateInitializedEventArgs());
    }
}
