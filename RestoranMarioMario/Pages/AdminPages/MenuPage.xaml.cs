﻿using RestoranMarioMario.Pages.PageEdit;
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
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
            ListViewCatalog.ItemsSource = App.db.Menu.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
            var type = App.db.CategoryMenu.OrderBy(x => x.IdCategoryMenu).Select(x => x.Name).ToArray();
            for (int i = 0; i < type.Length; i++)
            {
                CbFilter.Items.Add(type[i]);
            }
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
            var updateItem = App.db.Menu.ToList();
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
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.BarCard.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }
        private void ListViewCatalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuEditPage());
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteItem = (sender as Button).DataContext as Entities.Menu;
            if (MessageBox.Show("Вы действительно хотите удалить это блюдо?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Menu.Remove(deleteItem);
                App.db.SaveChanges();
                UpdateData();
            }
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit =(sender as Button).DataContext as Entities.Menu;
            NavigationService.Navigate(new MenuEditPage(edit));
        }
    }
}