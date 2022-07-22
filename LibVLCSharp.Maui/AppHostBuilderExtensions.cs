using LibVLCSharp.Maui.Controls;

namespace LibVLCSharp.Maui;
public static class AppHostBuilderExtensions
{
    public static MauiAppBuilder UseVLCSharp(this MauiAppBuilder builder, bool useCompatibilityRenderers = false)
    {
        builder.ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddLibraryHandlers();
                });

        return builder;
    }

    public static IMauiHandlersCollection? AddLibraryHandlers(this IMauiHandlersCollection handlers)
    {
        if (handlers is not null)
            handlers.AddTransient(typeof(MediaView), h => new MediaViewHandler());

        return handlers;
    }
}
