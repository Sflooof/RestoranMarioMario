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
    /// Логика взаимодействия для IngredientsEditPage.xaml
    /// </summary>
    public partial class IngredientsEditPage : Page
    {
        private Entities.Ingredient ingredient = null;
        Regex regex = new Regex(@"^[А-ЯЁ][а-яё]+$");
        MatchCollection match;
        public IngredientsEditPage()
        {
            InitializeComponent();
        }

        public IngredientsEditPage(Entities.Ingredient corIngredient)
        {
            InitializeComponent();
            if (corIngredient != null)
            {
                ingredient = corIngredient;
                TbIngredient.Text = corIngredient.Name.ToString();
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
                if (ingredient == null)
                {
                    var correctIngredient = new Entities.Ingredient { };
                    if (TbIngredient.Text == "")
                    {
                        correctIngredient = new Entities.Ingredient
                        {
                            Name = TbIngredient.Text
                        };
                    }
                    else
                    {
                        correctIngredient = new Entities.Ingredient
                        {
                            Name = TbIngredient.Text
                        };
                    }
                    App.db.Ingredient.Add(correctIngredient);
                    App.db.SaveChanges();
                    MessageBox.Show("Игредиент успешно создан", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    ingredient.Name = TbIngredient.Text;
                    App.db.SaveChanges();
                    MessageBox.Show("Игредиент успешно обновлен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(TbIngredient.Text))
            {
                errorBuilder.AppendLine("Поле обязательно для заполнения!");
            }
            match = regex.Matches(TbIngredient.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введен ингредиент!");

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
