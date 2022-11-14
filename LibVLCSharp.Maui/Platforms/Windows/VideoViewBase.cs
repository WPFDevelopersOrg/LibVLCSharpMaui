using LibVLCSharp.Maui.Platforms.Windows.Winui_dxinterop;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SharpDX;
using SharpDX.Mathematics.Interop;
using System.Runtime.InteropServices;
using MicrosoftuixamlControls = Microsoft.UI.Xaml.Controls;
using SharpDxdxgi = SharpDX.DXGI;

namespace LibVLCSharp.Maui.Platforms.Windows;

//[TemplatePart(Name = PARTSwapChainPanelName, Type = typeof(SwapChainPanel))]
public abstract class VideoViewBase : MicrosoftuixamlControls.Grid
{
    public VideoViewBase()
    {
        _SwapChainPanel = new SwapChainPanel();
        Children.Add(_SwapChainPanel);

        _SwapChainPanel.SizeChanged += SwapChainPanel_SizeChanged;
        _SwapChainPanel.CompositionScaleChanged += SwapChainPanel_CompositionScaleChanged;
        _SwapChainPanel.Unloaded += VideoViewBase_Unloaded;
    }

    const string PARTSwapChainPanelName = "PARTSwapChainPanel";
    readonly Guid SWAPCHAIN_WIDTH = new Guid(0xf1b59347, 0x1643, 0x411a, 0xad, 0x6b, 0xc7, 0x80, 0x17, 0x7a, 0x6, 0xb6);
    readonly Guid SWAPCHAIN_HEIGHT = new Guid(0x6ea976a0, 0x9d60, 0x4bb7, 0xa5, 0xa9, 0x7d, 0xd1, 0x18, 0x7f, 0xc9, 0xbd);

    readonly SwapChainPanel _SwapChainPanel;
    SharpDX.Direct3D11.Device? _SharpDxD31Device;
    SharpDxdxgi.Device1? _SharpDxDevice1;
    SharpDxdxgi.Device3? _SharpDxDevice3;

    SharpDxdxgi.SwapChain1? _SwapChain1;
    SharpDxdxgi.SwapChain2? _SwapChain2;

    bool _IsLoaded = false;

    public string[] SwapChainOptions
    {
        get
        {
            if (!_IsLoaded)
                throw new InvalidOperationException("You must wait for the VideoView to be loaded before calling GetSwapChainOptions()");

            return new string[]
            {
                    $"--winrt-d3dcontext=0x{_SharpDxD31Device!.ImmediateContext.NativePointer.ToString("x")}",
                    $"--winrt-swapchain=0x{_SwapChain1!.NativePointer.ToString("x")}"
            };
        }
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        //_SwapChainPanel = GetTemplateChild(PARTSwapChainPanelName) as SwapChainPanel;
        //ArgumentNullException.ThrowIfNull(_SwapChainPanel, "the swapchainpanel is not in the template!");

        if (DesignMode.IsDesignModeEnabled)
            return;

        //DestroySwapChain();

        //_SwapChainPanel.SizeChanged += SwapChainPanel_SizeChanged;
        //_SwapChainPanel.CompositionScaleChanged += SwapChainPanel_CompositionScaleChanged;
    }

    void VideoViewBase_Unloaded(object sender, RoutedEventArgs e)
    {
        DestroySwapChain();
    }

    void SwapChainPanel_CompositionScaleChanged(SwapChainPanel sender, object args)
    {
        if (_IsLoaded)
            ScalingChangedCallBack();
        else
            CreateSwapChain();
    }

    void SwapChainPanel_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (_IsLoaded)
            SizeChangedCallBack();
        else
            CreateSwapChain();
    }

    bool CreateSwapChain()
    {
        if (_SwapChainPanel == null || _SwapChainPanel.ActualHeight == 0)
            return false;

        if (double.IsNaN(_SwapChainPanel.ActualHeight) ||
            double.IsNegative(_SwapChainPanel.ActualHeight) ||
            double.IsPositiveInfinity(_SwapChainPanel.ActualHeight))
            return false;

        SharpDxdxgi.Factory2? dxgFactory = default;

        var deviceCreationFlags = SharpDX.Direct3D11.DeviceCreationFlags.BgraSupport | SharpDX.Direct3D11.DeviceCreationFlags.VideoSupport;
        try
        {
#if DEBUG

            try
            {
                dxgFactory = new SharpDxdxgi.Factory2(true);
            }
            catch
            {
                dxgFactory = new SharpDxdxgi.Factory2(false);
            }

#else
            dxgFactory = new SharpDxdxgi.Factory2(false);
#endif

            foreach (var adapter in dxgFactory.Adapters)
            {
                try
                {
                    _SharpDxD31Device = new SharpDX.Direct3D11.Device(adapter, deviceCreationFlags);
                    break;
                }
                catch
                {

                }
            }

            ArgumentNullException.ThrowIfNull(_SharpDxD31Device, nameof(SharpDX.Direct3D11.Device));

            _SharpDxDevice1 = _SharpDxD31Device.QueryInterface<SharpDxdxgi.Device1>();
            var swapChainDescription = new SharpDxdxgi.SwapChainDescription1()
            {
                Width = (int)(_SwapChainPanel.ActualWidth * _SwapChainPanel.CompositionScaleX),
                Height = (int)(_SwapChainPanel.ActualHeight * _SwapChainPanel.CompositionScaleY),
                Format = SharpDxdxgi.Format.B8G8R8A8_UNorm,
                Stereo = false,
                SampleDescription =
                {
                    Count = 1,
                    Quality = 0,
                },
                Usage = SharpDxdxgi.Usage.RenderTargetOutput,
                BufferCount = 2,
                SwapEffect = SharpDxdxgi.SwapEffect.FlipSequential,
                Flags = SharpDxdxgi.SwapChainFlags.None,
                AlphaMode = SharpDxdxgi.AlphaMode.Unspecified,
            };

            _SwapChain1 = new SharpDxdxgi.SwapChain1(dxgFactory, _SharpDxD31Device, ref swapChainDescription);
            _SharpDxDevice1.MaximumFrameLatency = 1;

            //IObjectReference nativeObject = ((IWinRTObject)_SwapChainPanel).NativeObject;
            //var iSwapChainPanelNative = nativeObject.As<SharpDxdxgi.ISwapChainPanelNative>();
            //var comObject = ComObject.As<SharpDxdxgi.ISwapChainPanelNative>(nativeObject.ThisPtr.ToPointer());
            //var iInspectable =  _SwapChainPanel.As<IInspectable>();
            //var iSwapChainPanelNative = iInspectable.As<SharpDxdxgi.ISwapChainPanelNative>();

            //using var nativePanel = SharpDX.ComObject.As<SharpDxdxgi.ISwapChainPanelNative>(_SwapChainPanel);

            {
                var comObject = new ComObject(_SwapChainPanel);
                using var nativePanel = comObject.QueryInterfaceOrNull<Winui_ISwapChainPanelNative>();
                nativePanel.SwapChain = _SwapChain1;
            }
           
            _SharpDxDevice3 = _SharpDxDevice1.QueryInterface<SharpDxdxgi.Device3>();
            ArgumentNullException.ThrowIfNull(_SharpDxDevice3, nameof(SharpDxdxgi.Device3));

            _SwapChain2 = _SwapChain1.QueryInterface<SharpDxdxgi.SwapChain2>();
            ArgumentNullException.ThrowIfNull(_SharpDxDevice3, nameof(SharpDxdxgi.Device3));

            ScalingChangedCallBack();
            SizeChangedCallBack();
            _IsLoaded = true;
            OnInitialized();
        }
        catch
        {
            DestroySwapChain();
            throw;
        }
        finally
        {
            dxgFactory?.Dispose();
        }

        return true;
    }

    protected abstract void OnInitialized();

    bool DestroySwapChain()
    {
        _SwapChain2?.Dispose();
        _SwapChain2 = null;

        _SharpDxDevice3?.Dispose();
        _SharpDxDevice3 = null;

        _SwapChain1?.Dispose();
        _SwapChain1 = null;

        _SharpDxDevice1?.Dispose();
        _SharpDxDevice1 = null;

        _SharpDxD31Device?.Dispose();
        _SharpDxD31Device = null;

        _IsLoaded = false;
        return true;
    }

    bool ScalingChangedCallBack()
    {
        if (_SwapChainPanel is null)
            return false;

        _SwapChain2!.MatrixTransform = new RawMatrix3x2 { M11 = 1.0f / _SwapChainPanel.CompositionScaleX, M22 = 1.0f / _SwapChainPanel.CompositionScaleY };
        return true;
    }

    bool SizeChangedCallBack()
    {
        if (_SwapChainPanel is null)
            return false;

        if (_SwapChain1 is null || _SwapChain1.IsDisposed)
            return false;

        var width = IntPtr.Zero;
        var height = IntPtr.Zero;

        try
        {
            width = Marshal.AllocHGlobal(sizeof(int));
            height = Marshal.AllocHGlobal(sizeof(int));
            var w = (int)(_SwapChainPanel.ActualWidth * _SwapChainPanel.CompositionScaleX);
            var h = (int)(_SwapChainPanel.ActualHeight * _SwapChainPanel.CompositionScaleY);

            Marshal.WriteInt32(width, w);
            Marshal.WriteInt32(height, h);

            _SwapChain1.SetPrivateData(SWAPCHAIN_WIDTH, sizeof(int), width);
            _SwapChain1.SetPrivateData(SWAPCHAIN_HEIGHT, sizeof(int), height);
        }
        finally
        {
            Marshal.FreeHGlobal(width);
            Marshal.FreeHGlobal(height);
        }

        return true;
    }
}
