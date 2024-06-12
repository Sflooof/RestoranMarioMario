using RestoranMarioMario.Pages.PageEdit;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.Order.ToList();
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

        private void UpdateData()
        {
            var updateItem = App.db.Order.ToList();
            updateItem = updateItem.Where(item => item.NumberOrder.ToString().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderByDescending(item => item.Date).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderBy(item => item.Date).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.Order.Count().ToString();
            if (countFind < 0)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено.";
            }
        }

        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = (sender as Button).DataContext as Entities.Order;
            if (MessageBox.Show("Вы действительно хотите удалить этот заказ?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Order.Remove(delete);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as Entities.Order;
            NavigationService.Navigate(new OrderEditPage(edit));
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderEditPage());
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }
    }
}
