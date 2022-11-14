using SharpDX;
using SharpDX.DXGI;
using System.Runtime.InteropServices;

namespace LibVLCSharp.Maui.Platforms.Windows.Winui_dxinterop;

[Guid("63aad0b8-7c24-40ff-85a8-640d944cc325")]
public class Winui_ISwapChainPanelNative : ComObject
{
    public Winui_ISwapChainPanelNative(IntPtr nativePtr)
           : base(nativePtr)
    {
    }

    public SwapChain SwapChain
    {
        set => SetSwapChain(value);
    }

    public static explicit operator Winui_ISwapChainPanelNative?(IntPtr nativePtr)
    {
        if (nativePtr != IntPtr.Zero)
            return new Winui_ISwapChainPanelNative(nativePtr);

        return default;
    }

    internal unsafe void SetSwapChain(SwapChain swapChain)
    {
        IntPtr zero = IntPtr.Zero;
        zero = CppObject.ToCallbackPtr<SwapChain>(swapChain);
        void* nativePointer = _nativePointer;
        void* intPtr = (void*)zero;
        ((Result)((delegate* unmanaged[Stdcall]<void*, void*, int>)(*(IntPtr*)((nint)(*(IntPtr*)_nativePointer) + (nint)3 * (nint)sizeof(void*))))(nativePointer, intPtr)).CheckError();
    }
}
