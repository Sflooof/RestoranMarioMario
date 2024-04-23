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
    /// Логика взаимодействия для CategoryBarCardEditPage.xaml
    /// </summary>
    public partial class CategoryBarCardEditPage : Page
    {
        private CategoryBarCard barCard = null;
        Regex regex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        MatchCollection match;
        public CategoryBarCardEditPage()
        {
            InitializeComponent();
        }
        public CategoryBarCardEditPage(CategoryBarCard corBarCard)
        {
            InitializeComponent();
            if (corBarCard != null)
            {
                barCard = corBarCard;
                TbCategory.Text = corBarCard.Name.ToString();
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
                if (barCard == null)
                {
                    var catBar = new CategoryBarCard { };
                    if (TbCategory.Text == "")
                    {
                        catBar = new CategoryBarCard
                        {
                            Name = TbCategory.Text
                        };
                    }
                    else
                    {
                        catBar = new CategoryBarCard
                        {
                            Name = TbCategory.Text
                        };
                    }
                    App.db.CategoryBarCard.Add(catBar);
                    App.db.SaveChanges();
                    MessageBox.Show("Категория барной карты успешно создана", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    barCard.Name = TbCategory.Text;
                    App.db.SaveChanges();
                    MessageBox.Show("Категория барной карты  успешно обновлена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
                errorBuilder.AppendLine("Некорректно введена категория барной карты!");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
