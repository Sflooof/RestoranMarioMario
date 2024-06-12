using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
            var currentOrderMenu = App.CurrentOrderMenu.SingleOrDefault(om => om.MenuBarCard == currentMenu.IdMenu && om.Modification == modificationText);

            if (currentOrderMenu == null)
            {
                currentOrderMenu = new Entities.OrderMenu
                {
                    MenuBarCard = currentMenu.IdMenu,
                    MenuTitle = currentMenu.Name,
                    Quantity = int.Parse(TbCount.Text),
                    Sum = currentMenu.Sum,
                    Modification = modificationText,
                    DateAdd = DateTime.Now,
                };
                App.CurrentOrderMenu.Add(currentOrderMenu);
            }
            else
            {
                currentOrderMenu.Quantity++;
            }
            App.CurrentOrder.OrderSum += currentMenu.Sum;
            MessageBox.Show("Товар добавлен в корзину!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MenuPage());

        }

        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(TbCount.Text);
            if (count == 100)
            {
                MessageBox.Show("Это миниммальное количество порций!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else count--;
            TbCount.Text = count.ToString();
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            int count = int.Parse(TbCount.Text);
            if (count == 10)
            {
                MessageBox.Show("Это максимальное количество порций!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else count++;
            TbCount.Text = count.ToString();
        }
    }
}
