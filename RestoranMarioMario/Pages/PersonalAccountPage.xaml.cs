﻿using RestoranMarioMario.Pages.AdminPages;
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

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для PersonalAccountPage.xaml
    /// </summary>
    public partial class PersonalAccountPage : Page
    {
        public PersonalAccountPage()
        {
            InitializeComponent();
            FullUserName();
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtTable_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TablePage());
        }

        private void BtUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Users());
        }

        private void BtRole_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RolePage());
        }

        private void BtBarCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.BarCardPage());
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.MenuPage());
        }

        private void BtCategoryMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.CategoryMenuPage());
        }

        private void BtCategoryBarCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.CategoryBarCard());
        }

        private void BtIngredient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.IngredientPage());
        }

        private void BtOrder_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.OrderPage());
        }
        private void FullUserName()
        {
            var userName = "Гость";
            if (App.CurrentUser != null)
            {
                userName = $"{App.CurrentUser.Surname} {App.CurrentUser.Name} {App.CurrentUser.Patronymic}";
            }
            TbFullName.Text = userName;
        }

        private void BtWaiter_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WaiterPage());
        }

        private void BtBarCardInrgedient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BarCardIngredientPage());
        }

        private void BtMenuIngredient_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuIngredientPage());
        }

        private void BtOrderMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate( new OrderMenuPage());
        }

        private void BtOrderBarCard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminPages.OrderMenuPage());
        }
    }
}