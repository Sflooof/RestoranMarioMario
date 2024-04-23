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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            ListViewMenu.ItemsSource = App.db.Menu.ToList();
        }

        private void BtPizza_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 1).ToList();
        }

        private void BtFocaccia_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 2).ToList();
        }

        private void BtPizzaSauce_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 3).ToList();
        }

        private void BtPastaAndRisotto_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 4).ToList();
        }

        private void BtSoup_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 5).ToList();
        }

        private void BtHotFishDishes_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 6).ToList();
        }

        private void BtotMeatDishes_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 7).ToList();
        }

        private void BtSideDish_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 8).ToList();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var currentProduct = button.DataContext as Entities.Menu;
            NavigationService.Navigate(new MenuDetail(currentProduct));
        }
    }
}
