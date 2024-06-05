using RestoranMarioMario.Entities;
using RestoranMarioMario.Pages.PageEdit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для RolePage.xaml
    /// </summary>
    public partial class RolePage : Page
    {
        public RolePage()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.Roles.ToList();
            UpdateData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoleEditPage());
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteItem = (sender as Button).DataContext as Roles;
            if (MessageBox.Show("Вы действительно хотите удалить эту роль?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Roles.Remove(deleteItem);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            var updateItem = App.db.Roles.ToList();
            updateItem = updateItem.Where(item => item.Name.ToLower().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(x => x.Name).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(x => x.Name).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.Roles.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as Roles;
            NavigationService.Navigate(new RoleEditPage(edit));
        }

        private void TbFind_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbSort_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void TbFind_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
