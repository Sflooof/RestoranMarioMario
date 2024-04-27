using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            MessageBox.Show("Официант скоро к вам подойдет.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtPlay_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
