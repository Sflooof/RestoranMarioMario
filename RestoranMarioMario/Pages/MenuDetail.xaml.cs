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
using System.Xml.Linq;
using Path = System.IO.Path;

namespace RestoranMarioMario.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuDetail.xaml
    /// </summary>
    public partial class MenuDetail : Page
    {
        //private Entities.OrderMenu viewModel = null;
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");
        public MenuDetail(Entities.Menu menuDetail)
        {
            InitializeComponent();
            if (menuDetail != null)
            {
                if (menuDetail.PhotoMenu != null)
                {
                    ImgPhoto.Source = new ImageSourceConverter()
                        .ConvertFrom(menuDetail.PhotoMenu) as ImageSource;
                }
                TbName.Text = menuDetail.Name;
                var curCategory = App.db.CategoryMenu.Where(m => m.IdCategoryMenu == menuDetail.Category).FirstOrDefault();
                TbCategory.Text = curCategory.Name;
                TbSum.Text = menuDetail.Sum.ToString();
                TbWeight.Text = menuDetail.Volume.ToString();
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtBasket_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as OrderMenu;
            NavigationService.Navigate(new AddProductCartPage(edit));
            //viewModel = new Entities.OrderMenu();
            //viewModel.Menu = new Entities.Menu();
            //DataContext = viewModel;
            //OrderMenu orderMenu = new OrderMenu();
            //orderMenu.MenuBarCard = viewModel.Menu.IdMenu;
            //orderMenu.Quantity = 1;
            //orderMenu.Sum = viewModel.Menu.Sum;
            //App.db.OrderMenu.Add(orderMenu);
            //try
            //{
            //    App.db.SaveChanges();
            //    MessageBox.Show("Блюдо добавлено в корзину.");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Ошибка при сохранении данных: " + ex.Message);
            //}
        }
    }
}
