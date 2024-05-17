using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
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
using System.Data.Entity;

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
                MessageBox.Show("Не все поля заполнены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (login == null)
            {
                MessageBox.Show("Пользователь не зарегистрирован", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
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
            //var query = App.db.Order.AsNoTracking();

            //// Подписаться на уведомления об изменениях
            //query.RegisterNotification(entity =>
            //{
            //    // Выполнить код, когда в таблице Order появятся новые данные
            //    if (entity.EntityState == EntityState.Added)
            //    {
            //        // Вывести уведомление менеджеру
            //        MessageBox.Show("Появился новый заказ!");
            //    }
            //});

            //// Запустить асинхронную операцию для отслеживания изменений
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        // Бесконечный цикл для ожидания уведомлений об изменениях
            //        // Время ожидания равно 100 миллисекундам
            //        query.Load();
            //        Thread.Sleep(100);
            //    }
            //});
        }

        private void BtLoginTable_Click(object sender, RoutedEventArgs e)
        {
            var loginTable = App.db.Table.FirstOrDefault(x => x.TableNumber.ToString() == TbLoginTable.Text && x.TablePassword == PbLoginTable.Password);
            if (TbLoginTable.Text == "" || PbLoginTable.Password == "")
            {
                MessageBox.Show("Не все поля заполнены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (loginTable == null)
            {
                MessageBox.Show("Пользователь не зарегистрирован", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                App.CurrentTable = loginTable;
                App.CurrentOrder = new Entities.Order()
                {
                    TableNumber = loginTable.IdTable,
                    OrderSum = 0,
                    Date = DateTime.Now,
                    NumberOrder = "1231"
                };
                App.CurrentOrderMenu = new List<Entities.OrderMenu>();
                NavigationService.Navigate(new MainPage());
                
            }
        }
    }
}
