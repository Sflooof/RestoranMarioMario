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
using Rectangle = iTextSharp.text.Rectangle;
using System.Windows.Markup;

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
            orderProductsListView.ItemsSource = App.CurrentOrderMenu;
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
                orderProductsListView.ItemsSource = App.CurrentOrderMenu;
                double sum = 0;
                foreach (var product in App.CurrentOrderMenu)
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
            App.db.Order.Add(App.CurrentOrder);
            App.db.SaveChanges();
            Entities.Order createdOrder = App.db.Order.ToList().Last();
            foreach (var orderMenu in App.CurrentOrderMenu)
            {
                orderMenu.OrderId = createdOrder.IdOrder;
                App.db.OrderMenu.Add(orderMenu);
            }
            App.db.SaveChanges();
            MessageBox.Show("Заказ создан", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
            App.CurrentOrder = new Entities.Order()
            {
                TableNumber = createdOrder.TableNumber,
                Waiter = 1,
                OrderSum = 0,
                Date = DateTime.Now,
                NumberOrder = "1231"
            };
            App.CurrentOrderMenu = new List<Entities.OrderMenu>();
            //Здесь будем формировать чек, пока что нихера
            NavigationService.Navigate(new MainPage());

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
                        App.CurrentOrderMenu.Remove(currentOrderProduct);
                        if (App.CurrentOrderMenu.Count == 0)
                        {
                            App.CurrentOrder = null;
                            App.CurrentOrderMenu = null;
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
                App.CurrentOrderMenu.Remove(currentOrderProduct);
                if (App.CurrentOrderMenu.Count == 0)
                {
                    App.CurrentOrder = null;
                    App.CurrentOrderMenu = null;
                    NavigationService.Navigate(new MainPage());
                    return;
                }
            }
            Update();
        }

        private void BtPDF_Click(object sender, RoutedEventArgs e)
        {
            Document doc = new Document();
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            int numberCheque = 1;
            int numverTable = App.CurrentTable.TableNumber;
            var waiterOrder = App.db.Order.Where(x => x.Waiter == 5).First();
            var waiterInfo = App.db.Waiter.Where(w => w.IdWaiter == waiterOrder.Waiter).Select(w => new { w.Surname, w.Name, w.Patronymic }).First();
            string fullName = $"{waiterInfo.Surname} {waiterInfo.Name} {waiterInfo.Patronymic}";
            var orderMenu = App.db.OrderMenu.GroupBy(x => x.DateAdd).OrderBy(x => x.Min(y => y.DateAdd)).Select(x => x.Key) .First();
            try
            {
                PdfWriter.GetInstance(doc, new FileStream($"..\\..\\Сheque\\cheque_{timestamp}.pdf", FileMode.Create));
                doc.Open();
                BaseFont baseFont = BaseFont.CreateFont("C:\\Windows\\Fonts\\times.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font font = new Font(baseFont, 14);
                Font font1 = new Font(baseFont, 24, 1, BaseColor.BLACK);
                Paragraph prechek = new Paragraph($"Пречек №        {numberCheque}  {timestamp}", font);
                prechek.Alignment = Element.ALIGN_LEFT;
                doc.Add(prechek);
                Paragraph bill = new Paragraph($"Счет №            {numberCheque}", font);
                bill.Alignment = Element.ALIGN_LEFT;
                doc.Add(bill);
                numberCheque++;
                Paragraph table = new Paragraph($"Стол №            {numverTable}", font);
                table.Alignment = Element.ALIGN_LEFT;
                doc.Add(table);
                Paragraph waiter = new Paragraph($"Официант       {fullName}", font);
                waiter.Alignment = Element.ALIGN_LEFT;
                doc.Add(waiter);
                Paragraph openDate = new Paragraph($"Счет открыт    {orderMenu}", font);
                openDate.Alignment = Element.ALIGN_LEFT;
                doc.Add(openDate);
                Paragraph closeDate = new Paragraph($"Счет открыт    {timestamp}", font);
                closeDate.Alignment = Element.ALIGN_LEFT;
                doc.Add(closeDate);
                Paragraph dash = new Paragraph("----------------------------------------------------------------------------------------------------------------", font);
                dash.Alignment = Element.ALIGN_LEFT;
                doc.Add(dash);
                Paragraph header = new Paragraph($"Наименование           Цена (руб.)    Кол-во (шт.)   Сумма", font);
                header.Alignment = Element.ALIGN_LEFT;
                doc.Add(header);
                Paragraph dash2 = new Paragraph("----------------------------------------------------------------------------------------------------------------", font);
                dash2.Alignment = Element.ALIGN_LEFT;
                doc.Add(dash2);
                decimal? totalCost = 0;
                var tableOrder = new PdfPTable(4);
                float[] columnWidths = new float[] { 30f, 20f, 20f, 30f };
                tableOrder.SetWidths(columnWidths);
                //tableOrder.DefaultCell.Border = Rectangle.NO_BORDER;
                //tableOrder.DefaultCell.BorderColor = BaseColor.WHITE;
                //tableOrder.DefaultCell.BorderWidth = 0;
                foreach (var item in App.CurrentOrderMenu)
                {
                    OrderMenu data = (OrderMenu)item;
                    if (data.Modification != null)
                        tableOrder.AddCell(new PdfPCell(new Phrase(data.MenuBarCard.ToString() + $"({data.Modification})", font)));
                    else
                        tableOrder.AddCell(new PdfPCell(new Phrase(data.MenuBarCard.ToString(), font)));
                    tableOrder.AddCell(new PdfPCell(new Phrase(data.Sum.ToString())));
                    tableOrder.AddCell(new PdfPCell(new Phrase(data.Quantity.ToString())));
                    decimal? totalSum = data.Sum * data.Quantity;
                    tableOrder.AddCell(new PdfPCell(new Phrase(totalSum.ToString())));
                    totalCost += totalSum;
                }
                tableOrder.HorizontalAlignment = Element.ALIGN_LEFT;
                doc.Add(tableOrder);
                Paragraph dash3 = new Paragraph("----------------------------------------------------------------------------------------------------------------", font);
                dash3.Alignment = Element.ALIGN_LEFT;
                doc.Add(dash3);
                Paragraph sumOrder = new Paragraph("Сумма заказа " + totalCost.ToString(), font);
                sumOrder.Alignment = Element.ALIGN_RIGHT;
                doc.Add(sumOrder);
                Paragraph sumPaid = new Paragraph("Сумма к оплате " + totalCost.ToString(), font1);
                sumPaid.Alignment = Element.ALIGN_CENTER;
                doc.Add(sumPaid);
                Paragraph dash4 = new Paragraph("----------------------------------------------------------------------------------------------------------------", font);
                dash4.Alignment = Element.ALIGN_LEFT;
                doc.Add(dash4);
                Paragraph datePrint = new Paragraph($"Дата печати {timestamp}", font);
                datePrint.Alignment = Element.ALIGN_LEFT;
                doc.Add(datePrint);
                Paragraph final = new Paragraph($"Благодарим за визит!", font);
                final.Alignment = Element.ALIGN_CENTER;
                doc.Add(final);
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
        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
