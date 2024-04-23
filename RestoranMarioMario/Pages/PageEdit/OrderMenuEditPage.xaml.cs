using RestoranMarioMario.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для OrderMenuEditPage.xaml
    /// </summary>
    public partial class OrderMenuEditPage : Page
    {
        private Entities.OrderMenuBarCard orderMenu = null;
        Regex regexSum = new Regex(@"^\w{1,4}$");
        MatchCollection match;
        public OrderMenuEditPage()
        {
            InitializeComponent();
        }
        public OrderMenuEditPage(Entities.OrderMenuBarCard corOrderMenu)
        {
            InitializeComponent();
            if (corOrderMenu != null)
            {
                orderMenu = corOrderMenu;
                CbNameMenu.SelectedIndex = (int)(corOrderMenu.NameMenu - 1);
                CbNameBarCard.SelectedIndex = (int)(corOrderMenu.NameBarCard - 1);
                CbNumberOrder.SelectedIndex = corOrderMenu.NumberOrder - 1;
                TbQuantity.Text = corOrderMenu.Quantity.ToString();
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbMenu = App.db.Menu.OrderBy(p => p.IdMenu).Select(p => p.Name).ToArray();
            var cbBarCard = App.db.BarCard.OrderBy(p => p.IdBarCard).ToArray();
            var cbOrder = App.db.Order.OrderBy(p => p.IdOrder).Select(p => p.NumberOrder).ToArray();
            for (int i = 0; i < cbMenu.Length; i++)
                CbNameMenu.Items.Add(cbMenu[i]);
            for (int i = 0; i < cbOrder.Length; i++)
                CbNumberOrder.Items.Add(cbOrder[i]);
            for (int i = 0; i < cbBarCard.Length; i++)
                CbNameBarCard.Items.Add(cbBarCard[i]);
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            var error = CheckErrors();
            if (error.Length > 0)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var menu = App.db.Menu.Where(c => c.Name == CbNameMenu.SelectedItem.ToString()).FirstOrDefault();
                var bar = App.db.BarCard.Where(c => c.Name == CbNameBarCard.SelectedItem.ToString()).FirstOrDefault();
                var order = App.db.Order.Where(c => c.NumberOrder == CbNumberOrder.SelectedItem.ToString()).FirstOrDefault();
                if (orderMenu == null)
                {
                    var correctMenu = new Entities.OrderMenuBarCard { };
                    if (CbNameMenu.Text == "")
                    {
                        correctMenu = new Entities.OrderMenuBarCard
                        {
                            NameMenu = menu.IdMenu,
                            NameBarCard = bar.IdBarCard,
                            NumberOrder = order.IdOrder,
                            Quantity = int.Parse(TbQuantity.Text),
                        };
                    }
                    else
                    {
                        correctMenu = new Entities.OrderMenuBarCard
                        {
                            NameMenu = menu.IdMenu,
                            NameBarCard = bar.IdBarCard,
                            NumberOrder = order.IdOrder,
                            Quantity = int.Parse(TbQuantity.Text),
                        };
                    }
                    App.db.OrderMenuBarCard.Add(correctMenu);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    orderMenu.NameMenu = menu.IdMenu;
                    orderMenu.NameBarCard = bar.IdBarCard;
                    orderMenu.NumberOrder = order.IdOrder;
                    orderMenu.Quantity = int.Parse(TbQuantity.Text);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (CbNameMenu.SelectedItem == null)
                errorBuilder.AppendLine("Поле Блюдо обязательно для заполнения.");
            if (CbNumberOrder.SelectedItem == null)
                errorBuilder.AppendLine("Поле Номер заказа обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbQuantity.Text))
                errorBuilder.AppendLine("Поле Количество обязательно для заполнения.");
            match = regexSum.Matches(TbQuantity.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена количество.");
            if (CbNameBarCard.Text != null && CbNameMenu.Text != null)
            {
                errorBuilder.AppendLine("Введмите значение только для меня или только для барной крты.");
            }
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
