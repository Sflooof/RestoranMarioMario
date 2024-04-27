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
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 26).ToList();
        }

        private void BtFocaccia_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 27).ToList();
        }

        private void BtPizzaSauce_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 28).ToList();
        }

        private void BtPastaAndRisotto_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 29).ToList();
        }

        private void BtSoup_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 30).ToList();
        }

        private void BtHotFishDishes_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 31).ToList();
        }

        private void BtotMeatDishes_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 32).ToList();
        }

        private void BtSideDish_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 33).ToList();
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

        private void BtCtrtificate_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 34).ToList();
        }

        private void BtBeerOnTap_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 2).ToList();
        }

        private void BtVodka_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 4).ToList();
        }

        private void BtTequila_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 5).ToList();
        }

        private void BtIceShot_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 6).ToList();
        }

        private void BtWhiskey_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 7).ToList();
        }

        private void BtCognacBrandy_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 8).ToList();
        }

        private void BtRum_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 9).ToList();
        }

        private void BtGin_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 10).ToList();
        }

        private void BtVermouth_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 11).ToList();
        }

        private void BtAlcoholicCocktails_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 12).ToList();
        }

        private void BtTinctureCocktails_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 13).ToList();
        }

        private void BtNonAlcoholicCocktails_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 14).ToList();
        }

        private void BtMilkShakes_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 15).ToList();
        }

        private void BtSmoothies_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 16).ToList();
        }

        private void BtCocktailsWithJuice_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 17).ToList();
        }

        private void BtFresh_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 18).ToList();
        }

        private void BtLemonade_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 19).ToList();
        }

        private void BtMojito_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 20).ToList();
        }

        private void BtJuice_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 21).ToList();
        }

        private void BtClassicTea_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 22).ToList();
        }

        private void BtHomemadeTea_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 23).ToList();
        }

        private void BtCoffee_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 24).ToList();
        }

        private void BtDrink_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 25).ToList();
        }

        private void BtHomemadeTinctures_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 1).ToList();
        }

        private void BtBottledBeer_Click(object sender, RoutedEventArgs e)
        {
            ListViewMenu.ItemsSource = App.db.Menu.Where(item => item.Category == 3).ToList();
        }
    }
}
