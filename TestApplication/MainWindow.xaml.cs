using System.Windows;

namespace TestApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new AddWindow(){Owner = this};
            window.ShowDialog();

        }
    }
}
