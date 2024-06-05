using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для TableEditPage.xaml
    /// </summary>
    public partial class TableEditPage : Page
    {
        private Entities.Table table = null;
        Regex regex = new Regex(@"^\w{1,50}$");
        MatchCollection match;
        public TableEditPage()
        {
            InitializeComponent();
        }
        public TableEditPage(Entities.Table curTable)
        {
            InitializeComponent();
            if (curTable != null)
            {
                table = curTable;
                TbNumber.Text = curTable.TableNumber.ToString();
                TbPassword.Text = curTable.TablePassword.ToString();
                CbWaiter.SelectedIndex = (int)curTable.TableWaiter - 1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var error = CheckErrors();
            if (error.Length > 0)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var waiter = App.db.Waiter.Where(c => c.Surname == CbWaiter.SelectedItem.ToString()).FirstOrDefault();
                if (table == null)
                {
                    var numTable = new Entities.Table { };

                    if (TbNumber.Text == "")
                    {
                        numTable = new Entities.Table
                        {
                            TableNumber = int.Parse(TbNumber.Text),
                            TablePassword = TbPassword.Text,
                            TableWaiter = waiter.IdWaiter
                        };
                    }
                    else
                    {
                        numTable = new Entities.Table
                        {
                            TableNumber = int.Parse(TbNumber.Text),
                            TablePassword = TbPassword.Text,
                            TableWaiter = waiter.IdWaiter
                        };
                    }

                    App.db.Table.Add(numTable);
                    App.db.SaveChanges();
                    MessageBox.Show("Стол успешно создан", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    table.TableNumber = int.Parse(TbNumber.Text);
                    table.TablePassword = TbPassword.Text;
                    table.TableWaiter = waiter.IdWaiter;
                    App.db.SaveChanges();
                    MessageBox.Show("Стол успешно обновлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TbNumber.Text))
            {
                errorBuilder.AppendLine("Поле обязательно для заполнения!");
            }
            match = regex.Matches(TbNumber.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введен номер стола");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }
            if (CbWaiter.SelectedItem == null)
            {
                errorBuilder.AppendLine("Поле Категория обязательно для заполнения.");
            }

            return errorBuilder.ToString();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var waiter = App.db.Waiter.OrderBy(x => x.IdWaiter).Select(x => x.Surname).ToArray();
            for (int i = 0; i < waiter.Length; i++)
            {
                CbWaiter.Items.Add(waiter[i]);
            }
        }
    }
}
