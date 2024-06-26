﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = App.db.Users.FirstOrDefault(x => x.Login == TbLogin.Text && x.Password == PbLogin.Password);
            if (TbLogin.Text == "" || PbLogin.Password == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (login == null)
            {
                MessageBox.Show("Пользователь не зарегистрирован!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                App.CurrentUser = login;
                if (login.Role == 1)
                {
                    NavigationService.Navigate(new PersonalAccountPage());
                }
                else if (login.Role == 2)
                {
                    NavigationService.Navigate(new PersonalAccountMemegerPage());
                }
            }
        }

        private void BtLoginTable_Click(object sender, RoutedEventArgs e)
        {
            var loginTable = App.db.Table.FirstOrDefault(x => x.TableNumber.ToString() == TbLoginTable.Text && x.TablePassword == PbLoginTable.Password);
            if (TbLoginTable.Text == "" || PbLoginTable.Password == "")
            {
                MessageBox.Show("Не все поля заполнены!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (loginTable == null)
            {
                MessageBox.Show("Пользователь не зарегистрирован!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                App.CurrentTable = loginTable;
                App.CurrentOrder = new Entities.Order()
                {
                    TableNumber = loginTable.IdTable,
                    OrderSum = 0,
                    Date = DateTime.Now,
                    NumberOrder = loginTable.IdTable.ToString(),
                };
                App.CurrentOrderMenu = new List<Entities.OrderMenu>();
                NavigationService.Navigate(new MainPage());
            }
        }
    }
}
