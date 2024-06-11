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
    /// Логика взаимодействия для OrderEditPage.xaml
    /// </summary>
    public partial class OrderEditPage : Page
    {
        private Entities.Order order = null;
        Regex regexSum = new Regex(@"^\w{2,4}$");
        MatchCollection match;

        public OrderEditPage()
        {
            InitializeComponent();
        }
        public OrderEditPage(Entities.Order corOrder)
        {
            InitializeComponent();
            if (corOrder != null)
            {
                order = corOrder;
                TbNumberOrder.Text = corOrder.NumberOrder.ToString();
                CbNumberTable.SelectedIndex = corOrder.TableNumber - 1;
                DpDate.SelectedDate = corOrder.Date;
                TbSum.Text = corOrder.OrderSum.ToString();

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbTable = App.db.Table.OrderBy(p => p.IdTable).Select(p => p.TableNumber).ToArray();
            for (int i = 0; i < cbTable.Length; i++)
                CbNumberTable.Items.Add(cbTable[i]);
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

                var table = App.db.Table.Where(c => c.TableNumber.ToString() == CbNumberTable.SelectedItem.ToString()).FirstOrDefault();
                if (order == null)
                {
                    var correctOrder = new Entities.Order { };
                    if (CbNumberTable.Text == "")
                    {
                        correctOrder = new Entities.Order
                        {
                            TableNumber = table.IdTable,
                            OrderSum = int.Parse(TbSum.Text),
                            Date = (DateTime)DpDate.SelectedDate,
                            NumberOrder = TbNumberOrder.Text,
                        };
                    }
                    else
                    {
                        correctOrder = new Entities.Order
                        {
                            TableNumber = table.IdTable,
                            OrderSum = int.Parse(TbSum.Text),
                            Date = (DateTime)DpDate.SelectedDate,
                            NumberOrder = TbNumberOrder.Text,
                        };
                    }
                    App.db.Order.Add(correctOrder);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    order.TableNumber = table.IdTable;
                    order.OrderSum = int.Parse(TbSum.Text);
                    order.Date = (DateTime)DpDate.SelectedDate;
                    order.NumberOrder = TbNumberOrder.Text;
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbNumberOrder.Text))
                errorBuilder.AppendLine("Поле Номер заказа обязательно для заполнения.");
            if (CbNumberTable.SelectedItem == null)
                errorBuilder.AppendLine("Поле Номер стола обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(DpDate.Text))
                errorBuilder.AppendLine("Поле Дата обязательно для заполнения.");
            match = regexSum.Matches(TbSum.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Сумма должна быть целым числом.");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
