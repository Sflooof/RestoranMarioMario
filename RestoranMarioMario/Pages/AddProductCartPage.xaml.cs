using RestoranMarioMario.Entities;
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
    /// Логика взаимодействия для AddProductCartPage.xaml
    /// </summary>
    public partial class AddProductCartPage : Page
    {
        private Entities.Menu curMenu = null;
        public AddProductCartPage(Entities.Menu corOrderMenu)
        {
            InitializeComponent();
            curMenu = corOrderMenu;
            DataContext = curMenu;
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            var currentMenu = (sender as Button).DataContext as Entities.Menu;
            var modificationText = TbModification.Text;
            if (modificationText == "")
            {
                modificationText = null;
            }
            var currentOrderMenu = App.CurrentOrderMenu.SingleOrDefault(om => om.MenuBarCard == currentMenu.IdMenu && om.Modification == modificationText);
            if (currentOrderMenu == null)
            {
                var orderMenu = new Entities.OrderMenu
                {
                    MenuBarCard = currentMenu.IdMenu,
                    Quantity = 1,
                    Sum = currentMenu.Sum,
                    Modification = modificationText,
                    DateAdd = DateTime.Now
                };
                App.CurrentOrderMenu.Add(orderMenu);
            }
            else
            {
                currentOrderMenu.Quantity++;
            }
            App.CurrentOrder.OrderSum += currentMenu.Sum;
            MessageBox.Show("Товар добавлен к корзину", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MenuPage());
        }

        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            var currentMenu = (sender as Button).DataContext as Entities.Menu;
            var modificationText = TbQuantity.Text;
            if (modificationText == "")
            {
                modificationText = null;
            }

            var currentOrderMenu = App.CurrentOrderMenu.SingleOrDefault(om => om.MenuBarCard == currentMenu.IdMenu && om.Modification == modificationText);
            if (currentOrderMenu != null)
            {
                if (currentOrderMenu.Quantity > 1)
                {
                    currentOrderMenu.Quantity--;
                    App.CurrentOrder.OrderSum -= currentMenu.Sum;
                }
                else
                {
                    App.CurrentOrderMenu.Remove(currentOrderMenu);
                    App.CurrentOrder.OrderSum -= currentMenu.Sum;
                }
            }
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            var currentMenu = (sender as Button).DataContext as Entities.Menu;
            var modificationText = TbQuantity.Text;
            if (modificationText == "")
            {
                modificationText = null;
            }

            var currentOrderMenu = App.CurrentOrderMenu.SingleOrDefault(om => om.MenuBarCard == currentMenu.IdMenu && om.Modification == modificationText);
            if (currentOrderMenu == null)
            {
                var orderMenu = new Entities.OrderMenu
                {
                    MenuBarCard = currentMenu.IdMenu,
                    Quantity = 1,
                    Sum = currentMenu.Sum,
                    Modification = modificationText,
                    DateAdd = DateTime.Now
                };
                App.CurrentOrderMenu.Add(orderMenu);
            }
            else
            {
                currentOrderMenu.Quantity++;
            }

            App.CurrentOrder.OrderSum += currentMenu.Sum;
        }
    }
}
