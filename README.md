# LoadedUnloadedEvents

In UWP XAML, it is possible for the Loaded and Unloaded events of a FrameworkElement to propagate in a different order than they occurred.
A common instance is when an element is disconnected from the VisualTree and re-inserted somewhere else.
Without additional checks, any code that relies on those events bubbling in a consistent order will be at risk of breaking.

This solution offers a simple workaround, in the form of the `ElementLoadNormalizer.cs`

Simply add the `ElementLoadNormalizer` to your application and adopt the pattern below to ensure the correct processing order:

```
public MainPage()
{
    InitializeComponent();

    myElement.Loaded += OnLoaded;
    myElement.Unloaded += OnUnloaded;

    // Wire up the loading event extensions
    myElement.ExtendLoadingEvents();
}

private void OnUnloaded(object sender, RoutedEventArgs e)
{
    if (sender is FrameworkElement element)
    {
        var lastLoadedArgs = element.GetLastLoadedEventArgs();
        if (lastLoadedArgs != null)
        {
            HandleLoaded(sender, lastLoadedArgs);
        }
        
        HandleUnloaded(sender, e);
    }
}

private void OnLoaded(object sender, RoutedEventArgs e)
{
    if (sender is FrameworkElement element)
    {
        HandleLoaded(sender, e);
    
        var lastUnloadedArgs = element.GetLastUnloadedEventArgs();
        if (lastUnloadedArgs != null)
        {
            HandleUnloaded(sender, lastUnloadedArgs);
        }
    }
}

private void HandleUnloaded(object sender, RoutedEventArgs e) { ... }

private void HandleLoaded(object sender, RoutedEventArgs e) { ... }
```
