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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для BarCardDetail.xaml
    /// </summary>
    public partial class BarCardDetail : Page
    {

        public Order currentOrder = null;
        public BarCard currentBarCard = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");
        public BarCardDetail(Entities.BarCard barCardDetail)
        {
            InitializeComponent();
            if (barCardDetail != null)
            {
                if (barCardDetail.PhotoBarCard != null)
                {
                    ImgPhoto.Source = new ImageSourceConverter()
                        .ConvertFrom(barCardDetail.PhotoBarCard) as ImageSource;
                }
                TbName.Text = barCardDetail.Name;
                var curCategory = App.db.CategoryBarCard.Where(m => m.IdBarCard == barCardDetail.Category).FirstOrDefault();
                TbCategory.Text = curCategory.Name;
                TbSum.Text = barCardDetail.Sum.ToString();
                TbVolume.Text = barCardDetail.Volume.ToString();
                currentBarCard = barCardDetail;
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtBasket_Click(object sender, RoutedEventArgs e)
        {
            //var button = sender as Button;
            //var currentTool = button.DataContext as Entities.BarCard;
            var random = new Random();
            //if (App.CurrentUser == null)
            //{
            //    if (App.CurrentOrder == null)
            //    {
            //        var order = new Order
            //        {
            //            TableNumber = 1,
            //            Waiter = 1,
            //            Date = DateTime.Today,
            //            NumberOrder = random.Next().ToString(),
            //        };
            //        App.CurrentOrder = order;
            //    }
            //    if (App.CurrentOrderProducts == null)
            //    {
            //        App.CurrentOrderProducts = new List<OrderMenuBarCard>();
            //    }
            //    bool fl = false;
            //    foreach (var product in App.CurrentOrderProducts)
            //    {
            //        if (product.NameBarCard == currentBarCard.IdBarCard)
            //        {
            //            //if ((product.Quantity + 1) > 1000)
            //            //{
            //            //    MessageBox.Show("Превышен лимит товара на складе");
            //            //    return;
            //            //}
            //            product.Quantity++;
            //            fl = true;
            //        }
            //    }
            //    if (!fl)
            //    {
            //        var orderProduct = new OrderMenuBarCard
            //        {
            //            NumberOrder = App.CurrentOrder.IdOrder,
            //            NameBarCard = currentBarCard.IdBarCard,
            //            Quantity = 1
            //        };
            //        App.CurrentOrderProducts.Add(orderProduct);
            //    }
            //}
            //else
            //{
            if (App.CurrentOrder == null)
            {
                var order = new Order
                {
                    TableNumber = 1,
                    Waiter = 1,
                    Date = DateTime.Today,
                    NumberOrder = random.Next().ToString(),
                };
                App.CurrentOrder = order;
            }
            //App.db.Order.Add(order);
            //App.db.SaveChanges();
            //}
            //var currentOrderProducts = App.db.OrderMenuBarCard.Where(op => op.NumberOrder == currentOrder.IdOrder).ToList();
            bool flag = false;
            if (App.CurrentOrder != null)
            {
                App.CurrentOrderProducts = new List<OrderMenuBarCard>();
            }
            foreach (var product in App.CurrentOrderProducts)
            {
                if (product.NameBarCard == currentBarCard.IdBarCard)
                {
                    product.Quantity++;
                    flag = true;
                }
            }
            if (!flag)
            {
                var orderProduct = new OrderMenuBarCard
                {
                    NumberOrder = int.Parse(App.CurrentOrder.NumberOrder),
                    NameBarCard = currentBarCard.IdBarCard,
                    Quantity = 1
                };
                App.CurrentOrderProducts.Add(orderProduct);
            }
        }
    }
}
