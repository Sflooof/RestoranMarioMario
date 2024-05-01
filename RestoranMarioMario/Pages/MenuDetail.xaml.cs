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
        public string path = Path.Combine(Directory.GetParent(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)).FullName, @"Resources\");
        public Entities.Menu curMenuDetail = null;
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
                curMenuDetail = menuDetail;
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtBasket_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductCartPage(curMenuDetail));
        }
    }
}
