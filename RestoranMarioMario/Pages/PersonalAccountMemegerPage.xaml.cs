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
    /// Логика взаимодействия для PersonalAccountMemegerPage.xaml
    /// </summary>
    public partial class PersonalAccountMemegerPage : Page
    {
        public PersonalAccountMemegerPage()
        {
            InitializeComponent();
            FullUserName();
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

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtTable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.TablePage());
        }

        private void BtOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.OrderPage());
        }

        private void BtIngredient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.IngredientPage());
        }

        private void BtWaiter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.WaiterPage());
        }

    }
}
