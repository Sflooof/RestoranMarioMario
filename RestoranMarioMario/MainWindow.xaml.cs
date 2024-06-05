using RestoranMarioMario.Pages;
using System.Windows;

namespace RestoranMarioMario
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LoginPage());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Официант скоро подойдет", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
