using RestoranMarioMario.Pages.PageEdit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для WaiterPage.xaml
    /// </summary>
    public partial class WaiterPage : Page
    {
        public WaiterPage()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.Waiter.ToList();
        }

        private void TbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            var updateItem = App.db.Waiter.ToList();
            updateItem = updateItem.Where(item => item.Name.ToLower().Contains(TbFind.Text.ToLower())).ToList();
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.Waiter.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено.";
            }
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = (sender as Button).DataContext as Entities.Waiter;
            if (MessageBox.Show("Вы действительно хотите удалить этого официанта?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Waiter.Remove(delete);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as Entities.Waiter;
            NavigationService.Navigate(new WaiterEditPage(edit));
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WaiterEditPage());
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
    }
}
