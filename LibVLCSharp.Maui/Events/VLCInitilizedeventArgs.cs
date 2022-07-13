namespace LibVLCSharp.Maui.Events;
public class VLCInitilizedeventArgs : EventArgs
{
    public VLCInitilizedeventArgs(string[] swapChainOptions) :base()
    {
        SwapChainOptions = swapChainOptions;
    }

    public string[] SwapChainOptions { get; }
}
