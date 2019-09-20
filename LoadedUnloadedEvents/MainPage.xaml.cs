using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace LoadedUnloadedEvents
{
    public sealed partial class MainPage : Page
    {
        private Rectangle _yellowRect;
        public MainPage()
        {
            InitializeComponent();

            _yellowRect = new Rectangle
            {
                Width = 100,
                Height = 100,
                Fill = new SolidColorBrush
                {
                    Color = Colors.Yellow
                }
            };
            
            _yellowRect.Loaded += YellowRectLoaded;
            _yellowRect.Unloaded += YellowRectUnloaded;

            Loaded += MainPageLoaded;

            // Wire up the loading event extensions
            _yellowRect.ExtendLoadingEvents();
        }

        private void MainPageLoaded(object sender, RoutedEventArgs e)
        {
            _redRect.Children.Add(_yellowRect);
        }

        private void YellowRectUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                var lastLoadedArgs = element.GetLastLoadedEventArgs();
                if (lastLoadedArgs != null)
                {
                    _textBlock.Text = "LoadedUnloaded";
                }
            }
            else
            {
                _textBlock.Text += "Unloaded";
            }
        }

        private void YellowRectLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element)
            {
                var lastUnloadedArgs = element.GetLastUnloadedEventArgs();

                if (lastUnloadedArgs != null)
                {
                    _textBlock.Text = "LoadedUnloaded";
                }
            }
            else
            {
                _textBlock.Text += "Loaded";
            }
        }

        private void ButtonTapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            _textBlock.Text = "";

            if(_redRect.Children.Contains(_yellowRect))
            {
                _redRect.Children.Remove(_yellowRect);
                _greenRect.Children.Add(_yellowRect);
            }
            else
            {
                _greenRect.Children.Remove(_yellowRect);
                _redRect.Children.Add(_yellowRect);
            }
        }
    }
}
