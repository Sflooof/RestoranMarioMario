﻿using RestoranMarioMario.Entities;
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
    /// Логика взаимодействия для OrderMenuPage.xaml
    /// </summary>
    public partial class OrderMenuPage : Page
    {
        public OrderMenuPage()
        {
            InitializeComponent();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderMenuEditPage());
        }

        private void BtEdit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as OrderMenuBarCard;
            NavigationService.Navigate(new OrderMenuEditPage(edit));
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var delete = (sender as Button).DataContext as OrderMenuBarCard;
            if (MessageBox.Show("Вы действительно хотите удалить эту сточку?", "Внимание!", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.OrderMenuBarCard.Remove(delete);
                App.db.SaveChanges();
                UpdateData();
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

        private void UpdateData()
        {
            var updateItem = App.db.OrderMenuBarCard.ToList();
            updateItem = updateItem.Where(item => item.NameMenu.ToString().ToLower().Contains(TbFind.Text.ToLower())
            || item.NameBarCard.ToString().ToLower().Contains(TbFind.Text.ToLower())).ToList();
            if (CbSort.SelectedIndex == 0)
            {
                updateItem = updateItem.OrderBy(item => item.NameBarCard).ToList();
            }
            else if (CbSort.SelectedIndex == 1)
            {
                updateItem = updateItem.OrderByDescending(item => item.NameBarCard).ToList();
            }
            if (CbFilter.SelectedIndex > 0)
            {
                var selectedCategory = CbFilter.SelectedIndex;
                updateItem = updateItem.Where(item => item.NameMenu == selectedCategory).ToList();
            }
            ListViewCatalog.ItemsSource = updateItem;
            int countFind = ListViewCatalog.Items.Count;
            TbCountFind.Text = countFind.ToString() + " из " + App.db.OrderMenuBarCard.Count().ToString();
            if (countFind < 1)
            {
                TbCountFind.Text += " по вашему запросу ничего не найдено";
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateData();
            var type = App.db.Menu.OrderBy(x => x.IdMenu).Select(x => x.Name).ToArray();
            for (int i = 0; i < type.Length; i++)
            {
                CbFilter.Items.Add(type[i]);
            }
        }
    }
}
