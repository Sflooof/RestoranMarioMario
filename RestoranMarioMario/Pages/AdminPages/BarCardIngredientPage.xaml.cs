using RestoranMarioMario.Entities;
using RestoranMarioMario.Pages.PageEdit;
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

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для BarCardIngredientPage.xaml
    /// </summary>
    public partial class BarCardIngredientPage : Page
    {
        public BarCardIngredientPage()
        {
            InitializeComponent();
        }

        private void TbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = (sender as Button).DataContext as BarCardIngredient;
            if (MessageBox.Show("Вы действительно хотите удалить эту сточку?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.BarCardIngredient.Remove(delete);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            var updateItem = App.db.BarCardIngredient.ToList();
            updateItem = updateItem.Where(item => item.IdBarCard.ToString().ToLower().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(item => item.IdBarCard).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(item => item.IdIngredient).ToList();
            }
            if (CbFilter.SelectedIndex > 0)
            {
                var selectedCategory = CbFilter.SelectedIndex;
                updateItem = updateItem.Where(item => item.IdIngredient == selectedCategory).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.BarCardIngredient.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as Entities.BarCardIngredient;
            NavigationService.Navigate(new BarCardIngredientEditPage(edit));
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BarCardIngredientEditPage());
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
            var type = App.db.Ingredient.OrderBy(x => x.IdIngredient).Select(x => x.Name).ToArray();
            for (int i = 0; i < type.Length; i++)
            {
                CbFilter.Items.Add(type[i]);
            }
        }

        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
