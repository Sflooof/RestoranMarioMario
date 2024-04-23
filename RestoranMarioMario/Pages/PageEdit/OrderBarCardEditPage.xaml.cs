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
    /// Логика взаимодействия для OrderBarCardEditPage.xaml
    /// </summary>
    public partial class OrderBarCardEditPage : Page
    {
        private Entities.OrderMenuBarCard orderBarCard = null;
        Regex regexSum = new Regex(@"^\w{2,4}$");
        MatchCollection match;
        public OrderBarCardEditPage()
        {
            InitializeComponent();
        }

        public OrderBarCardEditPage(Entities.OrderMenuBarCard corOrderBarCard)
        {
            InitializeComponent();
            if (corOrderBarCard != null)
            {
                orderBarCard = corOrderBarCard;
                CbNameBarCard.SelectedIndex = (int)corOrderBarCard.NameBarCard - 1;
                CbNameMenu.SelectedIndex = (int)corOrderBarCard.NameMenu - 1;
                CbNumberOrder.SelectedIndex = corOrderBarCard.NumberOrder - 1;
                TbQuantity.Text = corOrderBarCard.Quantity.ToString();
            }
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
                var barCard = App.db.BarCard.Where(c => c.Name == CbNameBarCard.SelectedItem.ToString()).FirstOrDefault();
                var orderMenu = App.db.Menu.Where(c => c.Name == CbNameMenu.SelectedItem.ToString()).FirstOrDefault();
                var order = App.db.Order.Where(c => c.NumberOrder == CbNumberOrder.SelectedItem.ToString()).FirstOrDefault();
                if (orderBarCard == null)
                {
                    var correctOrderBar = new Entities.OrderMenuBarCard { };
                    if (CbNameBarCard.Text == "")
                    {
                        correctOrderBar = new Entities.OrderMenuBarCard
                        {
                            NameBarCard = barCard.IdBarCard,
                            NameMenu = orderMenu.IdMenu,
                            NumberOrder = order.IdOrder,
                            Quantity = int.Parse(TbQuantity.Text),
                        };
                    }
                    else
                    {
                        correctOrderBar = new Entities.OrderMenuBarCard
                        {
                            NameBarCard = barCard.IdBarCard,
                            NameMenu = orderMenu.IdMenu,
                            NumberOrder = order.IdOrder,
                            Quantity = int.Parse(TbQuantity.Text),
                        };
                    }
                    App.db.OrderMenuBarCard.Add(correctOrderBar);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    orderBarCard.NameBarCard = barCard.IdBarCard;
                    orderBarCard.NameMenu = orderMenu.IdMenu;
                    orderBarCard.NumberOrder = order.IdOrder;
                    orderBarCard.Quantity = int.Parse(TbQuantity.Text);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (CbNameBarCard.SelectedItem == null)
                errorBuilder.AppendLine("Поле Напиток обязательно для заполнения.");
            if (CbNameMenu.SelectedItem == null)
                errorBuilder.AppendLine("Поле Блюдо обязательно для заполнения.");
            if (CbNumberOrder.SelectedItem == null)
                errorBuilder.AppendLine("Поле Номер заказа обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbQuantity.Text))
                errorBuilder.AppendLine("Поле Количество обязательно для заполнения.");
            match = regexSum.Matches(TbQuantity.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена количество.");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbBarCard = App.db.BarCard.OrderBy(p => p.IdBarCard).Select(p => p.Name).ToArray();
            var cbOrder = App.db.Order.OrderBy(p => p.IdOrder).Select(p => p.NumberOrder).ToArray();
            for (int i = 0; i < cbBarCard.Length; i++)
                CbNameBarCard.Items.Add(cbBarCard[i]);
            for (int i = 0; i < cbOrder.Length; i++)
                CbNumberOrder.Items.Add(cbOrder[i]);
        }
    }
}
