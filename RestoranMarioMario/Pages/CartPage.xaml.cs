using iTextSharp.text.pdf;
using iTextSharp.text;
using RestoranMarioMario.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Paragraph = iTextSharp.text.Paragraph;

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
            orderProductsListView.ItemsSource = App.db.OrderMenu.ToList();
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
                var currentOrderProducts = App.db.OrderMenu.Where(op => op.MenuBarCard == currentOrder.IdOrder).ToList();
                orderProductsListView.ItemsSource = currentOrderProducts;
                double sum = 0;
                foreach (var product in currentOrderProducts)
                {
                    var currentProduct = App.db.Menu.Where(p => p.IdMenu == product.MenuBarCard).First();
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
                    var currentProduct = App.db.Menu.Where(p => p.IdMenu == product.MenuBarCard).First();
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
            //NavigationService.Navigate(new MainPage());

        }

        private void BtMinus_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var currentOrderProduct = button.DataContext as OrderMenu;
            if ((currentOrderProduct.Quantity - 1) == 0)
            {
                if (App.CurrentUser == null)
                {
                    if (MessageBox.Show($"Вы уверены, что хотите удалить из заказа товар: {currentOrderProduct.MenuBarCard}", "Внимание",
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
                    if (MessageBox.Show($"Вы уверены, что хотите удалить из заказа товар: {currentOrderProduct.MenuBarCard}" +
                        $"{currentOrderProduct.MenuBarCard}", "Внимание",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        App.db.OrderMenu.Remove(currentOrderProduct);
                        App.db.SaveChanges();
                        //if (App.db.OrderMenu.Where(op => op.NumberOrder == currentOrder.IdOrder).ToList().Count == 0)
                        //{
                        //    App.db.Order.Remove(currentOrder);
                        //    App.db.SaveChanges();
                        //    NavigationService.Navigate(new MainPage());
                        //    return;
                        //}
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
            var currentOrderProduct = button.DataContext as OrderMenu;
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
            var currentOrderProduct = button.DataContext as OrderMenu;
            if (MessageBox.Show($"Вы уверены, что хотите удалить из заказа товар: {currentOrderProduct.MenuBarCard}", "Внимание",
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
                    //App.db.OrderMenu.Remove(currentOrderProduct);
                    //App.db.SaveChanges();
                    //if (App.db.OrderMenu.Where(op => op.NumberOrder == currentOrder.IdOrder).ToList().Count == 0)
                    //{
                    //    App.db.Order.Remove(currentOrder);
                    //    App.db.SaveChanges();
                    //    NavigationService.Navigate(new MainPage());
                    //    return;
                    //}
                }
            }
            Update();
        }

        private void BtPDF_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            int numberCheque = 1;
            OrderMenu orderMenu = App.db.OrderMenu.First();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream($"..\\..\\Сheque\\cheque_{timestamp}.pdf", FileMode.Create));
                doc.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\times.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 14);
                Font font1 = new Font(baseFont, 14, 1, BaseColor.BLACK);
                Paragraph prechek = new Paragraph($"Пречек №      {numberCheque}  {timestamp}", font1);
                prechek.Alignment = Element.ALIGN_CENTER;
                doc.Add(prechek);
                Paragraph bill = new Paragraph($"Счет №        {numberCheque}", font1);
                bill.Alignment = Element.ALIGN_CENTER;
                doc.Add(bill);
                numberCheque++;
                Paragraph table = new Paragraph($"Стол №       {App.CurrentTable.TableNumber}", font1);
                table.Alignment = Element.ALIGN_CENTER;
                doc.Add(table);
                Paragraph waiter = new Paragraph($"Официант    {App.db.Waiter}", font1);///////sos
                waiter.Alignment = Element.ALIGN_CENTER;
                doc.Add(waiter);
                Paragraph openDate = new Paragraph($"Счет открыт    {orderMenu}", font1);
                openDate.Alignment = Element.ALIGN_CENTER;
                doc.Add(openDate);
                Paragraph closeDate = new Paragraph($"Счет открыт    {timestamp}", font1);
                closeDate.Alignment = Element.ALIGN_CENTER;
                doc.Add(closeDate);
                Paragraph paragraph = new Paragraph("Список товаров", font1);
                paragraph.Alignment = Element.YMARK;
                doc.Add(paragraph);
                decimal? totalCost = 0;
                foreach (var item in App.db.OrderMenu.ToList())
                {
                    if (item is OrderMenu)
                    {
                        OrderMenu data = (OrderMenu)item;
                        //Image img = Image.GetInstance(data.Foto);
                        //img.ScaleAbsolute(100f, 100f); doc.Add(img);
                        doc.Add(new Paragraph("Название:" + data.MenuBarCard, font));
                        doc.Add(new Paragraph("Название:" + data.Sum.ToString() + "руб.", font));
                        doc.Add(new Paragraph("Название:" + data.Quantity.ToString() + "шт.", font));
                        doc.Add(new Paragraph("Название:" + data.Modification, font));
                        totalCost += data.Sum;
                    }
                }
                Paragraph paragraph1 = new Paragraph("Сумма = " + totalCost.ToString(), font);
                paragraph1.Alignment = Element.ALIGN_RIGHT; doc.Add(paragraph1);
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            finally
            {
                doc.Close();
            }
        }
    }
}
