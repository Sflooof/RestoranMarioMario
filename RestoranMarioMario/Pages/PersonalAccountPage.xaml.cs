using RestoranMarioMario.Pages.AdminPages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountPage.xaml
    /// </summary>
    public partial class PersonalAccountPage : Page
    {
        public PersonalAccountPage()
        {
            InitializeComponent();
            FullUserName();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtTable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TablePage());
        }

        private void BtUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Users());
        }

        private void BtRole_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RolePage());
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.MenuPage());
        }

        private void BtCategoryMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.CategoryMenuPage());
        }

        private void BtIngredient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.IngredientPage());
        }

        private void BtOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.OrderPage());
        }
        private void FullUserName()
        {
            var userName = "Гость";
            if (App.CurrentUser != null)
            {
                userName = $"{App.CurrentUser.Surname} {App.CurrentUser.Name} {App.CurrentUser.Patronymic}";
            }
            TbFullName.Text = userName;
        }

        private void BtWaiter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WaiterPage());
        }

        private void BtMenuIngredient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuIngredientPage());
        }

        private void BtOrderMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderMenuPage());
        }

        private void BtOrderBarCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderMenuPage());
        }
    }
}
