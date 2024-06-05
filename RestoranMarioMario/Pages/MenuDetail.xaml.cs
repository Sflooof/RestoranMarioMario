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
                else
                {
                    ImgPhoto.Source = new BitmapImage(new Uri("file:///D:/приложения/RestoranMarioMario/RestoranMarioMario/Resources/no.png"));

                }
                TbName.Text = menuDetail.Name;
                var curCategory = App.db.CategoryMenu.Where(m => m.IdCategoryMenu == menuDetail.Category).FirstOrDefault();
                var ingredients = App.db.MenuIngredient.Where(mi => mi.IdMenu == menuDetail.IdMenu).Select(mi => mi.Ingredient1.Name.ToLower()).ToList();
                if (ingredients.Count > 0)
                {
                    string ingredientsString = string.Join(", ", ingredients);
                    TbIngredient.Text = ingredientsString;
                }
                else
                {
                    TbIngredient.Visibility = Visibility.Collapsed;
                    TbIngredients.Visibility = Visibility.Collapsed;
                }
                TbCategory.Text = curCategory.Name.ToLower();
                TbSum.Text = menuDetail.Sum.ToString();
                TbWeight.Text = menuDetail.Volume.ToString();
                curMenuDetail = menuDetail;
                if (curCategory.IdCategoryMenu == 1 ||
                curCategory.IdCategoryMenu == 2 || curCategory.IdCategoryMenu == 3 || curCategory.IdCategoryMenu == 4 ||
                curCategory.IdCategoryMenu == 5 || curCategory.IdCategoryMenu == 6 || curCategory.IdCategoryMenu == 7 ||
                curCategory.IdCategoryMenu == 8 || curCategory.IdCategoryMenu == 9 || curCategory.IdCategoryMenu == 10 ||
                curCategory.IdCategoryMenu == 11 || curCategory.IdCategoryMenu == 12 || curCategory.IdCategoryMenu == 13)
                {
                    BtBasket.Visibility = Visibility.Collapsed;
                }
                else
                { 
                    BtBasketPassword.Visibility = Visibility.Collapsed;
                    PbPasport.Visibility = Visibility.Collapsed;
                }
                App.CurrentUserPassword = null;
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtBasket_Click(object sender, RoutedEventArgs e)
        {
            var login = App.db.Users.Where(x => x.Password == PbPasport.Password && x.Role == 2).FirstOrDefault();
            if (PbPasport.Password == "")
            {
                if (SpPassword.Visibility == Visibility.Collapsed)
                {
                    NavigationService.Navigate(new AddProductCartPage(curMenuDetail));
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (login == null)
            {
                MessageBox.Show("Неверный пароль.", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                App.CurrentUserPassword = login;
                NavigationService.Navigate(new AddProductCartPage(curMenuDetail));
            }
        }

        private void BtBasketPassword_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Скоро подойдет менеджер для подтверждения возраста.", "Внимание!", MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                SpPassword.Visibility = Visibility.Visible;
                BtBasketPassword.Visibility = Visibility.Collapsed;
                BtBasket.Visibility = Visibility.Visible;
                BtBasket.IsEnabled = false;
                BtBasket.Background = Brushes.Gray;
            }
        }

        private void PbPasport_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = (PasswordBox)sender;
            Button button = (Button)FindName("BtBasket");
            if (passwordBox.Password.Length > 0)
            {
                button.IsEnabled = true;
                BtBasket.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#96c12a")); ;
            }
        }

        private void BtBasket_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#96c12a"));
        }
    }
}
