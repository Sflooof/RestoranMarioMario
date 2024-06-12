using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            TableNumberLogin();
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void BtBasket_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartPage());
        }

        private void TableNumberLogin()
        {
            var tableNumber = $"{App.CurrentTable.TableNumber}";
            TbTableNumber.Text = tableNumber;
        }

        private void BtWaiter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Официант скоро к вам подойдет.", "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtPlay_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GamePage());
        }
    }
}
