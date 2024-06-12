using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для TablePage.xaml
    /// </summary>
    public partial class TablePage : Page
    {
        public TablePage()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.Table.ToList();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteItem = (sender as Button).DataContext as Entities.Table;
            if (MessageBox.Show("Вы действительно хотите удалить этот стол?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Table.Remove(deleteItem);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            var updateItem = App.db.Table.ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(x => x.TableNumber).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(x => x.TableNumber).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.Table.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено.";
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var table = (sender as Button).DataContext as Entities.Table;
            NavigationService.Navigate(new TableEditPage(table));
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TableEditPage());
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }


        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
        }

        private void TbFind_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
    }
}
