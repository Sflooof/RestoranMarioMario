using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для UsersEditPage.xaml
    /// </summary>
    public partial class UsersEditPage : Page
    {
        private Entities.Users users = null;
        Regex regexName = new Regex(@"^[А-ЯЁ][а-яё]+$");
        Regex regexPassword = new Regex(@"^\w{4,50}$");
        MatchCollection match;
        public UsersEditPage()
        {
            InitializeComponent();
        }
        public UsersEditPage(Entities.Users corUsers)
        {
            InitializeComponent();
            if (corUsers != null)
            {
                users = corUsers;
                TbSurname.Text = corUsers.Surname.ToString();
                TbName.Text = corUsers.Name.ToString();
                TbPatronymic.Text = corUsers.Patronymic.ToString();
                CbRole.SelectedIndex = corUsers.Role - 1;
                TbLogin.Text = corUsers.Login.ToString();
                TbPassword.Password = corUsers.Password.ToString();
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
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var roles = App.db.Roles.Where(c => c.Name == CbRole.SelectedItem.ToString()).FirstOrDefault();
                if (users == null)
                {
                    var correstUsers = new Entities.Users { };
                    if (TbPatronymic.Text == "")
                    {
                        correstUsers = new Entities.Users
                        {
                            Surname = TbSurname.Text,
                            Name = TbName.Text,
                            Patronymic = "NULL",
                            Role = roles.IdRole,
                            Login = TbLogin.Text,
                            Password = TbPassword.Password,
                        };
                    }
                    else
                    {
                        correstUsers = new Entities.Users
                        {
                            Surname = TbSurname.Text,
                            Name = TbName.Text,
                            Patronymic = TbPatronymic.Text,
                            Role = roles.IdRole,
                            Login = TbLogin.Text,
                            Password = TbPassword.Password,
                        };
                    }
                    App.db.Users.Add(correstUsers);
                    App.db.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    users.Surname = TbSurname.Text;
                    users.Name = TbName.Text;
                    users.Patronymic = TbPatronymic.Text;
                    users.Role = roles.IdRole;
                    users.Login = TbLogin.Text;
                    users.Password = TbPassword.Password;
                    App.db.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbSurname.Text))
                errorBuilder.AppendLine("Поле Фамилия обязательно для заполнения.");
            match = regexName.Matches(TbSurname.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена фамилия");
            if (string.IsNullOrWhiteSpace(TbName.Text))
                errorBuilder.AppendLine("Поле Имя обязательно для заполнения.");
            match = regexName.Matches(TbName.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введено имя.");
            if (CbRole.SelectedItem == null)
                errorBuilder.AppendLine("Поле Роль обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbLogin.Text))
                errorBuilder.AppendLine("Поле Логин обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbPassword.Password))
                errorBuilder.AppendLine("Поле Пароль обязательно для заполнения.");
            match = regexPassword.Matches(TbPassword.Password);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введен пароль.");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbUser = App.db.Roles.OrderBy(p => p.IdRole).Select(p => p.Name).ToArray();

            for (int i = 0; i < cbUser.Length; i++)
                CbRole.Items.Add(cbUser[i]);
        }
    }
}
