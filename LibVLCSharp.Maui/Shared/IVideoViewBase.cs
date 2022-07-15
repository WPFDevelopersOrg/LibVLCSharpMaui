namespace LibVLCSharp.Maui.Shared;
public interface IVideoViewBase<TEventArgs> where TEventArgs : EventArgs
{
    event EventHandler<TEventArgs>? Initialized;
}
