using RestoranMarioMario.Entities;
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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public Entities.Order currentOrder = null;
        public CartPage()
        {
            InitializeComponent();
            //if (App.CurrentUser != null)
            //{
            //    currentOrder = App.Context.Orders.Where(o => o.UserID == App.CurrentUser.ID && o.OrderStatusID == 3).FirstOrDefault();
            //}
            //Update();
        }
        private void Update()
        {
            orderProductsListView.ItemsSource = null;
            if (App.CurrentUser != null)
            {
                var currentOrderProducts = App.db.OrderMenuBarCard.Where(op => op.NumberOrder == currentOrder.IdOrder).ToList();
                orderProductsListView.ItemsSource = currentOrderProducts;
                double sum = 0;
                foreach (var product in currentOrderProducts)
                {
                    var currentProduct = App.db.Menu.Where(p => p.IdMenu == product.NameMenu).First();
                    double productPrice = (double)currentProduct.Sum;
                    sum += productPrice * (int)product.Quantity;
                }
                OrderPriceBox.Text = $"Стоимость заказа: {sum} руб.";
            }
            else
            {
                orderProductsListView.ItemsSource = App.CurrentOrderProducts;
                double sum = 0;
                foreach (var product in App.CurrentOrderProducts)
                {
                    var currentProduct = App.db.Menu.Where(p => p.IdMenu == product.NameMenu).First();
                    double productPrice = (double)currentProduct.Sum;
                    sum += productPrice * (int)product.Quantity;
                }
                OrderPriceBox.Text = $"Стоимость заказа: {sum} руб.";
            }
        }
        private void BtCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            App.db.SaveChanges();
            MessageBox.Show("Заказ создан", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage());
        }

        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentOrderProduct = button.DataContext as OrderMenuBarCard;
            if ((currentOrderProduct.Quantity - 1) == 0)
            {
                if (App.CurrentUser == null)
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить из заказа товар: {currentOrderProduct.NameMenu}" +
                        $" {currentOrderProduct.NameBarCard}", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        App.CurrentOrderProducts.Remove(currentOrderProduct);
                        if (App.CurrentOrderProducts.Count == 0)
                        {
                            App.CurrentOrder = null;
                            App.CurrentOrderProducts = null;
                            NavigationService.Navigate(new MainPage());
                            return;
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить из заказа товар: {currentOrderProduct.NameMenu}" +
                        $"{currentOrderProduct.NameBarCard}", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        App.db.OrderMenuBarCard.Remove(currentOrderProduct);
                        App.db.SaveChanges();
                        if (App.db.OrderMenuBarCard.Where(op => op.NumberOrder == currentOrder.IdOrder).ToList().Count == 0)
                        {
                            App.db.Order.Remove(currentOrder);
                            App.db.SaveChanges();
                            NavigationService.Navigate(new MainPage());
                            return;
                        }
                    }
                }
            }
            else
            {
                currentOrderProduct.Quantity--;
                App.db.SaveChanges();
            }
            Update();
        }

        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentOrderProduct = button.DataContext as OrderMenuBarCard;
            if ((currentOrderProduct.Quantity + 1) > 1000)
            {
                MessageBox.Show("Превышен лимит");
            }
            else
            {
                currentOrderProduct.Quantity++;
                if (App.CurrentUser != null)
                {
                    App.db.SaveChanges();
                }
            }
            Update();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentOrderProduct = button.DataContext as OrderMenuBarCard;
            if (MessageBox.Show($"Вы уверены, что хотите удалить из заказа товар: {currentOrderProduct.NameMenu} {currentOrderProduct.NameBarCard}", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                if (App.CurrentUser == null)
                {
                    App.CurrentOrderProducts.Remove(currentOrderProduct);
                    if (App.CurrentOrderProducts.Count == 0)
                    {
                        App.CurrentOrder = null;
                        App.CurrentOrderProducts = null;
                        NavigationService.Navigate(new MainPage());
                        return;
                    }
                }
                else
                {
                    App.db.OrderMenuBarCard.Remove(currentOrderProduct);
                    App.db.SaveChanges();
                    if (App.db.OrderMenuBarCard.Where(op => op.NumberOrder == currentOrder.IdOrder).ToList().Count == 0)
                    {
                        App.db.Order.Remove(currentOrder);
                        App.db.SaveChanges();
                        NavigationService.Navigate(new MainPage());
                        return;
                    }
                }
            }
            Update();
        }
    }
}
