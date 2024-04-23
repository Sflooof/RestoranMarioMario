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
    /// Логика взаимодействия для RoleEditPage.xaml
    /// </summary>
    public partial class RoleEditPage : Page
    {
        private Entities.Roles role = null;
        Regex regex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        MatchCollection match;
        public RoleEditPage()
        {
            InitializeComponent();
        }
        public RoleEditPage(Entities.Roles corRoles)
        {
            InitializeComponent();
            if (corRoles != null)
            {
                role = corRoles;
                TbRole.Text = corRoles.Name.ToString();
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
                if (role == null)
                {
                    var numRole = new Entities.Roles { };
                    if (TbRole.Text == "")
                    {
                        numRole = new Entities.Roles
                        {
                            Name = TbRole.Text
                        };
                    }
                    else
                    {
                        numRole = new Entities.Roles
                        {
                            Name = TbRole.Text
                        };
                    }

                    App.db.Roles.Add(numRole);
                    App.db.SaveChanges();
                    MessageBox.Show("Роль пользователя успешно создана", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    role.Name = TbRole.Text;
                    App.db.SaveChanges();
                    MessageBox.Show("Роль пользователя успешно обновлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TbRole.Text))
            {
                errorBuilder.AppendLine("Поле обязательно для заполнения!");
            }
            match = regex.Matches(TbRole.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена роль");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
