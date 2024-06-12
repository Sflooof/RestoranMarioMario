using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для WaiterEditPage.xaml
    /// </summary>
    public partial class WaiterEditPage : Page
    {
        private Entities.Waiter waiter = null;
        Regex regex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        MatchCollection match;
        public WaiterEditPage()
        {
            InitializeComponent();
        }

        public WaiterEditPage(Entities.Waiter corWaiter)
        {
            InitializeComponent();
            if (corWaiter != null)
            {
                waiter = corWaiter;
                TbSurname.Text = corWaiter.Surname.ToString();
                TbName.Text = corWaiter.Name.ToString();
                TbPatronymic.Text = corWaiter.Patronymic.ToString();
            }
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
                MessageBox.Show(error, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (waiter == null)
                {
                    var correstWaiter = new Entities.Waiter { };
                    if (TbPatronymic.Text == "")
                    {
                        correstWaiter = new Entities.Waiter
                        {
                            Surname = TbSurname.Text,
                            Name = TbName.Text,
                            Patronymic = "",
                        };
                    }
                    else
                    {
                        correstWaiter = new Entities.Waiter
                        {
                            Surname = TbSurname.Text,
                            Name = TbName.Text,
                            Patronymic = TbPatronymic.Text,
                        };
                    }
                    App.db.Waiter.Add(correstWaiter);
                    App.db.SaveChanges();
                    MessageBox.Show("Официант успешно добавлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    waiter.Surname = TbSurname.Text;
                    waiter.Name = TbName.Text;
                    waiter.Patronymic = TbPatronymic.Text;
                    App.db.SaveChanges();
                    MessageBox.Show("Официант успешно обновлен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }

        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbSurname.Text))
                errorBuilder.AppendLine("Поле Фамилия обязательно для заполнения!");
            match = regex.Matches(TbSurname.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена фамилия!");
            if (string.IsNullOrWhiteSpace(TbName.Text))
                errorBuilder.AppendLine("Поле Имя обязательно для заполнения!");
            match = regex.Matches(TbName.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введено имя!");
            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
