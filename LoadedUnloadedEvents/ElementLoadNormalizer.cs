using Windows.UI.Xaml;

namespace LoadedUnloadedEvents
{
    public static class FrameworkElementExtensions
    {
        public static RoutedEventArgs GetLastLoadedEventArgs(this FrameworkElement element)
        {
            return GetLastLoadedEventArgs(element as DependencyObject);
        }

        public static RoutedEventArgs GetLastUnloadedEventArgs(this FrameworkElement element)
        {
            return GetLastUnloadedEventArgs(element as DependencyObject);
        }

        public static void ExtendLoadingEvents(this FrameworkElement element)
        {
            element.Loading += Element_Loading;
            element.Loaded += Element_Loaded;
            element.Unloaded += Element_Unloaded;
        }

        private static void Element_Loading(FrameworkElement sender, object args)
        {
            // Clear the data to prepare for new Load/Unload events
            SetLastLoadedEventArgs(sender as FrameworkElement, null);
            SetLastUnloadedEventArgs(sender as FrameworkElement, null);
        }

        private static void Element_Loaded(object sender, RoutedEventArgs e)
        {
            SetLastLoadedEventArgs(sender as FrameworkElement, e);
        }

        private static void Element_Unloaded(object sender, RoutedEventArgs e)
        {
            SetLastUnloadedEventArgs(sender as FrameworkElement, e);
        }

        #region LastLoaded
        private const string LastLoadedEventArgsPropertyName = "LastLoadedEventArgs";
        private const RoutedEventArgs LastLoadedEventArgsDefaultValue = null;

        private static readonly DependencyProperty LastLoadedEventArgsProperty = DependencyProperty.RegisterAttached(
            name: LastLoadedEventArgsPropertyName,
            propertyType: typeof(RoutedEventArgs),
            ownerType: typeof(DependencyObject),
            defaultMetadata: new PropertyMetadata(LastLoadedEventArgsDefaultValue)
            );

        private static void SetLastLoadedEventArgs(DependencyObject element, RoutedEventArgs value)
        {
            (element as FrameworkElement).SetValue(LastLoadedEventArgsProperty, value);
        }

        private static RoutedEventArgs GetLastLoadedEventArgs(DependencyObject element)
        {
            return (RoutedEventArgs)(element as FrameworkElement).GetValue(LastLoadedEventArgsProperty);
        }
        #endregion

        #region LastUnloaded
        private const string LastUnloadedEventArgsPropertyName = "LastUnloadedEventArgs";
        private const RoutedEventArgs LastUnloadedEventArgsDefaultValue = null;

        private static readonly DependencyProperty LastUnloadedEventArgsProperty = DependencyProperty.RegisterAttached(
            name: LastUnloadedEventArgsPropertyName,
            propertyType: typeof(RoutedEventArgs),
            ownerType: typeof(DependencyObject),
            defaultMetadata: new PropertyMetadata(LastUnloadedEventArgsDefaultValue)
            );

        private static void SetLastUnloadedEventArgs(DependencyObject element, RoutedEventArgs value)
        {
            (element as FrameworkElement).SetValue(LastUnloadedEventArgsProperty, value);
        }

        private static RoutedEventArgs GetLastUnloadedEventArgs(DependencyObject element)
        {
            return (RoutedEventArgs)(element as FrameworkElement).GetValue(LastUnloadedEventArgsProperty);
        }
        #endregion
    }
}
