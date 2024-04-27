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
        private OrderMenu orderMenu = null;
        //private Entities.Menu menu = null;
        public AddProductCartPage(OrderMenu corOrderMenu)
        {
            InitializeComponent();
            orderMenu = corOrderMenu;
            //TbSum.Text = menu.Sum.ToString();
            DataContext = orderMenu;
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            var currentMenu = (sender as Button).DataContext as Entities.Menu;
            var order = App.db.Order.Where(o => o.TableNumber == App.CurrentTable.IdTable).FirstOrDefault();
            var orderMenu = new Entities.OrderMenu
            {
                MenuBarCard = currentMenu.IdMenu,
                Quantity = 1,
                Sum = currentMenu.Sum,
                Modification = TbModification.Text,
            };
            App.db.OrderMenu.Add(orderMenu);
            order.OrderSum += currentMenu.Sum;
            App.db.SaveChanges();
        }
    }
}
