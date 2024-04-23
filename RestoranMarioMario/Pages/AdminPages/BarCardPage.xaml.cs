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
    /// Логика взаимодействия для BarCardPage.xaml
    /// </summary>
    public partial class BarCardPage : Page
    {
        public BarCardPage()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.BarCard.ToString();
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
            var deleteItem = (sender as Button).DataContext as Entities.BarCard;
            if (MessageBox.Show("Вы действительно хотите удалить этот напиток?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.BarCard.Remove(deleteItem);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void UpdateData()
        {
            var updateItem = App.db.BarCard.ToList();
            updateItem = updateItem.Where(item => item.Name.ToLower().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(x => x.Sum).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(x => x.Sum).ToList();
            }
            if (CbFilter.SelectedIndex == 1)
            {
                updateItem = updateItem.Where(item => item.Category == 1).ToList();
            }
            if (CbFilter.SelectedIndex == 2)
            {
                updateItem = updateItem.Where(item => item.Category == 2).ToList();
            }
            if (CbFilter.SelectedIndex == 3)
            {
                updateItem = updateItem.Where(item => item.Category == 3).ToList();
            }
            if (CbFilter.SelectedIndex == 4)
            {
                updateItem = updateItem.Where(item => item.Category == 4).ToList();
            }
            if (CbFilter.SelectedIndex == 5)
            {
                updateItem = updateItem.Where(item => item.Category == 5).ToList();
            }
            if (CbFilter.SelectedIndex == 6)
            {
                updateItem = updateItem.Where(item => item.Category == 6).ToList();
            }
            if (CbFilter.SelectedIndex == 7)
            {
                updateItem = updateItem.Where(item => item.Category == 7).ToList();
            }
            if (CbFilter.SelectedIndex == 8)
            {
                updateItem = updateItem.Where(item => item.Category == 8).ToList();
            }
            if (CbFilter.SelectedIndex == 9)
            {
                updateItem = updateItem.Where(item => item.Category == 9).ToList();
            }
            if (CbFilter.SelectedIndex == 10)
            {
                updateItem = updateItem.Where(item => item.Category == 10).ToList();
            }
            if (CbFilter.SelectedIndex == 11)
            {
                updateItem = updateItem.Where(item => item.Category == 11).ToList();
            }
            if (CbFilter.SelectedIndex == 12)
            {
                updateItem = updateItem.Where(item => item.Category == 12).ToList();
            }
            if (CbFilter.SelectedIndex == 13)
            {
                updateItem = updateItem.Where(item => item.Category == 13).ToList();
            }
            if (CbFilter.SelectedIndex == 14)
            {
                updateItem = updateItem.Where(item => item.Category == 14).ToList();
            }
            if (CbFilter.SelectedIndex == 15)
            {
                updateItem = updateItem.Where(item => item.Category == 15).ToList();
            }
            if (CbFilter.SelectedIndex == 16)
            {
                updateItem = updateItem.Where(item => item.Category == 16).ToList();
            }
            if (CbFilter.SelectedIndex == 17)
            {
                updateItem = updateItem.Where(item => item.Category == 17).ToList();
            }
            if (CbFilter.SelectedIndex == 18)
            {
                updateItem = updateItem.Where(item => item.Category == 18).ToList();
            }
            if (CbFilter.SelectedIndex == 19)
            {
                updateItem = updateItem.Where(item => item.Category == 19).ToList();
            }
            if (CbFilter.SelectedIndex == 20)
            {
                updateItem = updateItem.Where(item => item.Category == 20).ToList();
            }
            if (CbFilter.SelectedIndex == 21)
            {
                updateItem = updateItem.Where(item => item.Category == 22).ToList();
            }
            if (CbFilter.SelectedIndex == 23)
            {
                updateItem = updateItem.Where(item => item.Category == 23).ToList();
            }
            if (CbFilter.SelectedIndex == 24)
            {
                updateItem = updateItem.Where(item => item.Category == 24).ToList();
            }
            if (CbFilter.SelectedIndex == 25)
            {
                updateItem = updateItem.Where(item => item.Category == 25).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.BarCard.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit =(sender as Button).DataContext as Entities.BarCard;
            NavigationService.Navigate(new BarCardEditPage(edit));
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BarCardEditPage());
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
            var type = App.db.CategoryBarCard.OrderBy(x => x.IdBarCard).Select(x => x.Name).ToArray();
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
