using RestoranMarioMario.Entities;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

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
                if (table == null)
                {
                    var numTable = new Entities.Table { };
                    if (TbNumber.Text == "")
                    {
                        numTable = new Entities.Table
                        {
                            TableNumber = int.Parse(TbNumber.Text)
                        };
                    }
                    else
                    {
                        numTable = new Entities.Table
                        {
                            TableNumber = int.Parse(TbNumber.Text)
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

            return errorBuilder.ToString();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
