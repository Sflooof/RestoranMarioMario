using RestoranMarioMario.Entities;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для CategoryMenuEditPage.xaml
    /// </summary>
    public partial class CategoryMenuEditPage : Page
    {
        private Entities.CategoryMenu menu = null;
        Regex regex = new Regex(@"^[a-zA-ZА-ЯЁ][a-zA-Zа-яё ]+$");
        MatchCollection match;
        public CategoryMenuEditPage()
        {
            InitializeComponent();
        }
        public CategoryMenuEditPage(Entities.CategoryMenu corMenu)
        {
            InitializeComponent();
            if (corMenu != null)
            {
                menu = corMenu;
                TbCategory.Text = corMenu.Name.ToString();
            }
        }
        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var error = CheckErrors();
            if (error.Length > 0)
            {
                MessageBox.Show(error, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (menu == null)
                {
                    var numCategory = new CategoryMenu { };
                    if (TbCategory.Text == "")
                    {
                        numCategory = new CategoryMenu
                        {
                            Name = TbCategory.Text
                        };
                    }
                    else
                    {
                        numCategory = new CategoryMenu
                        {
                            Name = TbCategory.Text
                        };
                    }

                    App.db.CategoryMenu.Add(numCategory);
                    App.db.SaveChanges();
                    MessageBox.Show("Категория меню успешно создана.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    menu.Name = TbCategory.Text;
                    App.db.SaveChanges();
                    MessageBox.Show("Категория меню успешно обновлена.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();

            if (string.IsNullOrWhiteSpace(TbCategory.Text))
            {
                errorBuilder.AppendLine("Поле обязательно для заполнения!");
            }
            match = regex.Matches(TbCategory.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена категория меню!");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
