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

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для BarCardIngredientEditPage.xaml
    /// </summary>
    public partial class BarCardIngredientEditPage : Page
    {
        private Entities.BarCardIngredient cardIngredient = null;
        
        public BarCardIngredientEditPage()
        {
            InitializeComponent();
        }
        public BarCardIngredientEditPage(Entities.BarCardIngredient corBarCardIngredient)
        {
            InitializeComponent();
            if (corBarCardIngredient != null)
            {
                cardIngredient = corBarCardIngredient;
                CbBarCard.SelectedIndex = corBarCardIngredient.IdBarCard - 1;
                CbIngredient.SelectedIndex = corBarCardIngredient.IdIngredient - 1;
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
                var card = App.db.BarCard.Where(c => c.Name == CbBarCard.SelectedItem.ToString()).FirstOrDefault();
                var ingredient = App.db.Ingredient.Where(c => c.Name == CbIngredient.SelectedItem.ToString()).FirstOrDefault();
                if (cardIngredient == null)
                {
                    var correctBarIngredient = new Entities.BarCardIngredient { };
                    if (CbBarCard.Text == "")
                    {
                        correctBarIngredient = new Entities.BarCardIngredient
                        {
                            IdBarCard = card.IdBarCard,
                            IdIngredient = ingredient.IdIngredient,
                        };
                    }
                    else
                    {
                        correctBarIngredient = new Entities.BarCardIngredient
                        {
                            IdBarCard = card.IdBarCard,
                            IdIngredient = ingredient.IdIngredient,
                        };
                    }
                    App.db.BarCardIngredient.Add(correctBarIngredient);
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно добавлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    cardIngredient.IdBarCard = card.IdBarCard;
                    cardIngredient.IdIngredient = ingredient.IdIngredient;
                    App.db.SaveChanges();
                    MessageBox.Show("Данные успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            if (CbBarCard.SelectedItem == null)
                errorBuilder.AppendLine("Поле Напиток обязательно для заполнения.");
            if (CbIngredient.SelectedItem == null)
                errorBuilder.AppendLine("Поле Ингредиент обязательно для заполнения.");


            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbBarCard = App.db.BarCard.OrderBy(p => p.IdBarCard).Select(p => p.Name).ToArray();

            for (int i = 0; i < cbBarCard.Length; i++)
                CbBarCard.Items.Add(cbBarCard[i]);
            var cbIngredient = App.db.Ingredient.OrderBy(p => p.IdIngredient).Select(p => p.Name).ToArray();

            for (int i = 0; i < cbIngredient.Length; i++)
                CbIngredient.Items.Add(cbIngredient[i]);
        }
    }
}
