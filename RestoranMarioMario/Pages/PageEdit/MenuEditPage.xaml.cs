using Microsoft.Win32;
using RestoranMarioMario.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RestoranMarioMario.Pages.PageEdit
{
    /// <summary>
    /// Логика взаимодействия для MenuEditPage.xaml
    /// </summary>
    public partial class MenuEditPage : Page
    {
        private Entities.Menu menu = null;
        private byte[] img = null;
        Regex regexName = new Regex(@"^[А-ЯЁа-яё,\s0-9]+$");
        Regex regexSum = new Regex(@"^[0-9]{1,6}(\,[0-9]{2})?$");
        MatchCollection match;
        public MenuEditPage()
        {
            InitializeComponent();
        }
        public MenuEditPage(Entities.Menu corMenu)
        {
            InitializeComponent();
            if (corMenu != null)
            {
                menu = corMenu;
                TbName.Text = corMenu.Name.ToString();
                CbCategory.SelectedIndex = (int)corMenu.Category - 1;
                TbSum.Text = corMenu.Sum.ToString();
                if (corMenu.PhotoMenu != null)
                {
                    ImagePhoto.Source = new ImageSourceConverter()
                        .ConvertFrom(corMenu.PhotoMenu) as ImageSource;
                }
                TbWeight.Text = corMenu.Volume.ToString();
            }
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            var error = CheckErrors();
            if (error.Length > 0)
            {
                MessageBox.Show(error, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var category = App.db.CategoryMenu.Where(c => c.Name == CbCategory.SelectedItem.ToString()).FirstOrDefault();
                if (menu == null)
                {
                    var correstMenu = new Entities.Menu { };
                    if (CbCategory.Text == "")
                    {
                        correstMenu = new Entities.Menu
                        {
                            Name = TbName.Text,
                            Category = int.Parse("NULL"),
                            Sum = decimal.Parse(TbSum.Text),
                            PhotoMenu = img,
                            Volume = int.Parse(TbWeight.Text),
                        };
                    }
                    else
                    {
                        correstMenu = new Entities.Menu
                        {
                            Name = TbName.Text,
                            Category = category.IdCategoryMenu,
                            Sum = decimal.Parse(TbSum.Text),
                            PhotoMenu = img,
                            Volume = int.Parse(TbWeight.Text),
                        };
                    }
                    App.db.Menu.Add(correstMenu);
                    App.db.SaveChanges();
                    MessageBox.Show("Блюдо успешно добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    menu.Name = TbName.Text;
                    menu.Category = category.IdCategoryMenu;
                    menu.Sum = decimal.Parse(TbSum.Text);
                    if (img != null)
                        menu.PhotoMenu = img;
                    menu.Volume = int.Parse(TbWeight.Text);
                    App.db.SaveChanges();
                    MessageBox.Show("Блюдо успешно добавлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }

        private void BtnImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileImg = new OpenFileDialog();
            fileImg.Multiselect = false;
            fileImg.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (fileImg.ShowDialog() == true)
            {
                img = File.ReadAllBytes(fileImg.FileName);

                ImagePhoto.Source = new ImageSourceConverter()
                    .ConvertFrom(img) as ImageSource;

            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            decimal sum;
            if (string.IsNullOrWhiteSpace(TbName.Text))
                errorBuilder.AppendLine("Поле Название обязательно для заполнения.");
            match = regexName.Matches(TbName.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введено название");
            if (CbCategory.SelectedItem == null)
                errorBuilder.AppendLine("Поле Категория обязательно для заполнения.");
            if (string.IsNullOrWhiteSpace(TbSum.Text))
                errorBuilder.AppendLine("Поле Цена обязательно для заполнения.");
            match = regexSum.Matches(TbSum.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введена цена.");
            if (decimal.TryParse(TbSum.Text, out sum) == false || sum <= 0)
                errorBuilder.AppendLine("Цена товара должна быть положительным числом.");
            if (string.IsNullOrWhiteSpace(TbWeight.Text))
                errorBuilder.AppendLine("Поле Вес обязательно для заполнения.");
            match = regexSum.Matches(TbWeight.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введен вес.");


            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbCategory = App.db.CategoryMenu.OrderBy(p => p.IdCategoryMenu).Select(p => p.Name).ToArray();
            for (int i = 0; i < cbCategory.Length; i++)
                CbCategory.Items.Add(cbCategory[i]);
        }
    }
}
