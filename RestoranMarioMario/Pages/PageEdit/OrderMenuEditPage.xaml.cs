using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для OrderMenuEditPage.xaml
    /// </summary>
    public partial class OrderMenuEditPage : Page
    {
        private Entities.OrderMenu orderMenu = null;
        Regex regexSum = new Regex(@"^\w{1,4}$");
        MatchCollection match;
        public OrderMenuEditPage()
        {
            InitializeComponent();
        }
        public OrderMenuEditPage(Entities.OrderMenu corOrderMenu)
        {
            InitializeComponent();
            if (corOrderMenu != null)
            {
                orderMenu = corOrderMenu;
                CbNameMenu.SelectedIndex = (int)(corOrderMenu.MenuBarCard - 1);
                TbQuantity.Text = corOrderMenu.Quantity.ToString();
                TbSum.Text = corOrderMenu.Sum.ToString();
                TbModification.Text = corOrderMenu.Modification.ToString();
                CbOrderId.SelectedIndex = corOrderMenu.OrderId;
                DpDate.SelectedDate = corOrderMenu.DateAdd;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbMenu = App.db.Menu.OrderBy(p => p.IdMenu).Select(p => p.Name).ToArray();
            for (int i = 0; i < cbMenu.Length; i++)
                CbNameMenu.Items.Add(cbMenu[i]);
            var cbOrder = App.db.Order.OrderBy(p => p.IdOrder).Select(p => p.NumberOrder).ToArray();
            for (int i = 0; i < cbOrder.Length; i++)
                CbOrderId.Items.Add(cbOrder[i]);
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
                var order = App.db.Order.Where(c => c.NumberOrder == CbOrderId.SelectedItem.ToString()).FirstOrDefault();
                if (orderMenu == null)
                {
                    var correctMenu = new Entities.OrderMenu { };
                    if (CbNameMenu.Text == "")
                    {
                        correctMenu = new Entities.OrderMenu
                        {
                            MenuBarCard = menu.IdMenu,
                            Quantity = int.Parse(TbQuantity.Text),
                            Sum = decimal.Parse(TbSum.Text),
                            Modification = "NULL",
                            OrderId = order.IdOrder,
                            DateAdd = (DateTime)DpDate.SelectedDate,
                        };
                    }
                    else
                    {
                        correctMenu = new Entities.OrderMenu
                        {
                            MenuBarCard = menu.IdMenu,
                            Quantity = int.Parse(TbQuantity.Text),
                            Sum = decimal.Parse(TbSum.Text),
                            Modification = TbModification.Text,
                            OrderId = order.IdOrder,
                            DateAdd = (DateTime)DpDate.SelectedDate,
                        };
                    }
                    App.db.OrderMenu.Add(correctMenu);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    orderMenu.MenuBarCard = menu.IdMenu;
                    orderMenu.Quantity = int.Parse(TbQuantity.Text);
                    orderMenu.Sum = decimal.Parse(TbSum.Text);
                    orderMenu.Modification = TbModification.Text;
                    orderMenu.OrderId = order.IdOrder;
                    orderMenu.DateAdd = (DateTime)DpDate.SelectedDate;
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
                errorBuilder.AppendLine("Поле Позиция меню обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbQuantity.Text))
                errorBuilder.AppendLine("Поле Количество обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbSum.Text))
                errorBuilder.AppendLine("Поле Сумма обязательно для заполнения.");
            match = regexSum.Matches(TbQuantity.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена количество.");
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
