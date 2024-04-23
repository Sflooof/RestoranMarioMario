using RestoranMarioMario.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Логика взаимодействия для MenuIngredientEditPage.xaml
    /// </summary>
    public partial class MenuIngredientEditPage : Page
    {
        private Entities.MenuIngredient menuIngredient = null;
        public MenuIngredientEditPage()
        {
            InitializeComponent();
        }
        public MenuIngredientEditPage(Entities.MenuIngredient corMenuIngredient)
        {
            InitializeComponent();
            if (corMenuIngredient != null)
            {
                menuIngredient = corMenuIngredient;
                CbMenu.SelectedIndex = corMenuIngredient.IdMenu - 1;
                CbIngredient.SelectedIndex = corMenuIngredient.IdIngredient - 1;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbMenu = App.db.Menu.OrderBy(p => p.IdMenu).Select(p => p.Name).ToArray();
            for (int i = 0; i < cbMenu.Length; i++)
            {
                CbMenu.Items.Add(cbMenu[i]);
            }
            var cbIngredient = App.db.Ingredient.OrderBy(p => p.IdIngredient).Select(p => p.Name).ToArray();
            for (int i = 0; i < cbIngredient.Length; i++)
            {
                CbIngredient.Items.Add(cbIngredient[i]);
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
                var menu = App.db.Menu.Where(c => c.Name == CbMenu.SelectedItem.ToString()).FirstOrDefault();
                var ingredient = App.db.Ingredient.Where(c => c.Name == CbIngredient.SelectedItem.ToString()).FirstOrDefault();
                if (menuIngredient == null)
                {
                    var correctMenuIngredient = new Entities.MenuIngredient { };
                    if (CbMenu.Text == "")
                    {
                        correctMenuIngredient = new Entities.MenuIngredient
                        {
                            IdMenu = menu.IdMenu,
                            IdIngredient = ingredient.IdIngredient,
                        };
                    }
                    else
                    {
                        correctMenuIngredient = new Entities.MenuIngredient
                        {
                            IdMenu = menu.IdMenu,
                            IdIngredient = ingredient.IdIngredient,
                        };
                    }
                    App.db.MenuIngredient.Add(correctMenuIngredient);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    menuIngredient.IdMenu = menu.IdMenu;
                    menuIngredient.IdIngredient = ingredient.IdIngredient;
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (CbMenu.SelectedItem == null)
                errorBuilder.AppendLine("Поле Блюдо обязательно для заполнения.");
            if (CbIngredient.SelectedItem == null)
                errorBuilder.AppendLine("Поле Ингредиент обязательно для заполнения.");


            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }
    }
}
