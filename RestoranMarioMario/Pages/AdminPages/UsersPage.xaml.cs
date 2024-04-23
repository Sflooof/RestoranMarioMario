using System;
using System.Collections.Generic;
using System.Linq;
using RestoranMarioMario.Entities;
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
using RestoranMarioMario.Pages.PageEdit;

namespace RestoranMarioMario.Pages.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        public Users()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.Users.ToString();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void TbFind_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteItem = (sender as Button).DataContext as Entities.Users;
            if (MessageBox.Show("Вы действительно хотите удалить этого пользователя?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Users.Remove(deleteItem);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            var updateItem = App.db.Users.ToList();
            updateItem = updateItem.Where(item => item.Surname.ToLower().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(x => x.Surname).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(x => x.Surname).ToList();
            }
            if (CbFilter.SelectedIndex == 1)
            {
                updateItem = updateItem.Where(item => item.Role == 1).ToList();
            }
            if (CbFilter.SelectedIndex == 2)
            {
                updateItem = updateItem.Where(item => item.Role == 2).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.Users.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as Entities.Users;
            NavigationService.Navigate(new UsersEditPage(edit));
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UsersEditPage());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
            var type = App.db.Roles.OrderBy(x => x.IdRole).Select(x => x.Name).ToArray();
            for (int i = 0; i < type.Length; i++)
            {
                CbFilter.Items.Add(type[i]);
            }
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TbFind_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void CbFilter_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CbSort_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtDelete_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtEdit_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtAdd_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void BtBack_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
