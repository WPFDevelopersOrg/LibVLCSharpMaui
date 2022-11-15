using LibVLCSharp.Maui.Events;
using LibVLCSharp.Maui.Shared;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using System.Runtime.InteropServices;
using System.Text;
using Color = Windows.UI.Color;
using DirectXPixelFormat = Windows.Graphics.DirectX.DirectXPixelFormat;
using Grid = Microsoft.UI.Xaml.Controls.Grid;
using Rect = Windows.Foundation.Rect;

namespace LibVLCSharp.Maui.Platforms.Windows
{
    public class VideoViewFrame : Grid, IVideoView, IVideoViewBase<VLCInitilizedeventArgs>
    {
        public event EventHandler<VLCInitilizedeventArgs>? Initialized;

        private readonly CanvasControl? canvasControl;

        private int videoWidth;
        private int videoHeight;
        private IntPtr plane;
        private byte[]? buffer;
        private CanvasBitmap? canvasBitmap;
        private bool initialize;

        #region 属性
        private MediaPlayerX? mediaPlayer;

        public MediaPlayerX? MediaPlayer
        {
            get => mediaPlayer;
            set
            {
                if (mediaPlayer != value)
                {
                    Detach();
                    mediaPlayer = value;
                    if (mediaPlayer != null)
                    {
                        Attach();
                    }
                }
            }
        }
        #endregion

        public VideoViewFrame()
        {
            canvasControl = new();
            canvasControl.Draw += Canvas_Draw;
            Children.Add(canvasControl);
        }

        private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (initialize)
            {
                args.DrawingSession.Clear(Color.FromArgb(255, 0, 0, 0));

                if (canvasBitmap == null)
                {
                    canvasBitmap = CanvasBitmap.CreateFromBytes(sender, buffer, videoWidth, videoHeight, DirectXPixelFormat.B8G8R8A8UIntNormalized);
                }
                canvasBitmap.SetPixelBytes(buffer);

                float left, top, right, bottom, sum;
                double sumW = sender.ActualWidth / videoWidth;
                double sumH = sender.ActualHeight / videoHeight;

                sum = Convert.ToSingle(sumW < sumH ? sumW : sumH);
                right = videoWidth * sum;
                bottom = videoHeight * sum;
                left = Convert.ToSingle(sender.ActualWidth - right) / 2;
                top = Convert.ToSingle(sender.ActualHeight - bottom) / 2;
                args.DrawingSession.DrawImage(canvasBitmap, new Rect(left, top, right, bottom));
            }
        }

        private uint VideoFormat(ref IntPtr opaque, IntPtr chroma, ref uint width, ref uint height, ref uint pitches, ref uint lines)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("RV32");
            for (int i = 0; i < bytes.Length; i++)
            {
                Marshal.WriteByte(chroma, i, bytes[i]);
            }

            pitches = width * 4;
            lines = height;

            buffer = new byte[pitches * lines];

            plane = Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0);

            if (mediaPlayer != null && mediaPlayer.Media is Media media)
            {
                foreach (MediaTrack track in media.Tracks)
                {
                    if (track.TrackType == TrackType.Video)
                    {
                        VideoTrack trackInfo = track.Data.Video;
                        if (trackInfo.Width > 0 && trackInfo.Height > 0)
                        {
                            width = trackInfo.Width;
                            height = trackInfo.Height;
                        }

                        break;
                    }
                }
            }

            videoWidth = (int)width;
            videoHeight = (int)height;

            return 1;
        }

        private IntPtr LockVideo(IntPtr opaque, IntPtr planes)
        {
            Marshal.WriteIntPtr(planes, plane);
            return plane;
        }

        private void DisplayVideo(IntPtr opaque, IntPtr picture)
        {
            initialize = true;
            canvasControl?.Invalidate();
        }

        private void Attach()
        {
            if (mediaPlayer != null && mediaPlayer.NativeReference != IntPtr.Zero)
            {
                mediaPlayer.EnableHardwareDecoding = true;
                mediaPlayer.SetVideoFormatCallbacks(VideoFormat, null);
                mediaPlayer.SetVideoCallbacks(LockVideo, null, DisplayVideo);

                Initialized?.Invoke(this, new(Array.Empty<string>()));
            }
        }

        private void Detach()
        {
            if (mediaPlayer != null && mediaPlayer.NativeReference != IntPtr.Zero)
            {
                mediaPlayer.SetVideoFormatCallbacks(delegate { return 1; }, null);
                mediaPlayer.SetVideoCallbacks(delegate { return IntPtr.Zero; }, null, null);
            }

            InitializeData();
        }

        private void InitializeData()
        {
            videoWidth = 0;
            videoHeight = 0;
            plane = IntPtr.Zero;
            buffer = null;
            canvasBitmap = null;
            initialize = false;
        }
    }
}