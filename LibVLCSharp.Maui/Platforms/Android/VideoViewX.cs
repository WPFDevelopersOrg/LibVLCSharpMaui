using Android.Content;
using Android.Runtime;
using Android.Util;
using LibVLCSharp.Maui.Events;
using LibVLCSharp.Maui.Shared;
using LibVLCSharp.Platforms.Android;

namespace LibVLCSharp.Maui.Platforms.Android;
public class VideoViewX : VideoView, IVideoViewBase<VLCInitilizedeventArgs>
{
    public VideoViewX(IntPtr javaReference, JniHandleOwnership transfer)
    : base(javaReference, transfer)
    {
    }

    public VideoViewX(Context context)
          : base(context)
    {
    }

    public VideoViewX(Context context, IAttributeSet attrs)
           : base(context, attrs)
    {
    }

    public VideoViewX(Context context, IAttributeSet attrs, int defStyleAttr)
           : base(context, attrs, defStyleAttr)
    {
    }

    public VideoViewX(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
    {
    }

    public event EventHandler<VLCInitilizedeventArgs>? Initialized;
}
