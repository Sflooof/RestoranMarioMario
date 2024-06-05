using RestoranMarioMario.Entities;
using RestoranMarioMario.Pages.PageEdit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для MenuIngredientPage.xaml
    /// </summary>
    public partial class MenuIngredientPage : Page
    {
        public MenuIngredientPage()
        {
            InitializeComponent();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuIngredientEditPage());
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as MenuIngredient;
            NavigationService.Navigate(new PageEdit.MenuIngredientEditPage(edit));
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = (sender as Button).DataContext as MenuIngredient;
            if (MessageBox.Show("Вы действительно хотите удалить эту сточку?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.MenuIngredient.Remove(delete);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            var updateItem = App.db.MenuIngredient.ToList();
            updateItem = updateItem.Where(item => item.Menu1.Name.ToLower().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(item => item.IdMenu).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(item => item.IdMenu).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.MenuIngredient.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }
        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void TbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
    }
}
