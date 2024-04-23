using Microsoft.Win32;
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
    /// Логика взаимодействия для BarCardEditPage.xaml
    /// </summary>
    public partial class BarCardEditPage : Page
    {
        private Entities.BarCard barCard = null;
        private byte[] img = null;
        Regex regexName = new Regex(@"^[А-ЯЁ][а-яё]+$");
        Regex regexSum = new Regex(@"^\w{1,4}$");
        MatchCollection match;
        public BarCardEditPage()
        {
            InitializeComponent();
        }
        public BarCardEditPage(Entities.BarCard corBarCard)
        {
            InitializeComponent();
            if (corBarCard != null)
            {
                barCard = corBarCard;
                TbName.Text = corBarCard.Name.ToString();
                CbCategory.SelectedIndex = corBarCard.CategoryBarCard.IdBarCard - 1;
                TbSum.Text = corBarCard.Sum.ToString();
                if (corBarCard.PhotoBarCard != null)
                {
                    ImagePhoto.Source = new ImageSourceConverter()
                        .ConvertFrom(corBarCard.PhotoBarCard) as ImageSource;
                }
                TbVolume.Text = corBarCard.Volume.ToString();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var cbCategory = App.db.CategoryBarCard.OrderBy(p => p.IdBarCard).Select(p => p.Name).ToArray();
            for (int i = 0; i < cbCategory.Length; i++)
                CbCategory.Items.Add(cbCategory[i]);
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
                var category = App.db.CategoryBarCard.Where(c => c.Name == CbCategory.SelectedItem.ToString()).FirstOrDefault();
                if (barCard == null)
                {
                    var correctBarCard = new Entities.BarCard { };
                    if (CbCategory.Text == "")
                    {
                        correctBarCard = new Entities.BarCard
                        {
                            Name = TbName.Text,
                            Category =int.Parse("NULL"),
                            Sum = int.Parse(TbSum.Text),
                            PhotoBarCard = img,
                            Volume = int.Parse(TbVolume.Text),
                        };
                    }
                    else
                    {
                        correctBarCard = new Entities.BarCard
                        {
                            Name = TbName.Text,
                            Category = category.IdBarCard,
                            Sum = int.Parse(TbSum.Text),
                            PhotoBarCard = img,
                            Volume = int.Parse(TbVolume.Text),
                        };
                    }
                    App.db.BarCard.Add(correctBarCard);
                    App.db.SaveChanges();
                    MessageBox.Show("Напиток успешно добавлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
                else
                {
                    barCard.Name = TbName.Text;
                    barCard.Category = category.IdBarCard;
                    barCard.Sum = int.Parse(TbSum.Text);
                    if (img != null)
                        barCard.PhotoBarCard = img;
                    barCard.Volume = int.Parse(TbVolume.Text);
                    App.db.SaveChanges();
                    MessageBox.Show("Напиток успешно обновлен", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }
            }
        }
        private string CheckErrors()
        {
            var errorBuilder = new StringBuilder();
            int sum = 0;
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
            if (int.TryParse(TbSum.Text, out sum) == false || sum <= 0)
                errorBuilder.AppendLine("Цена товара должна быть положительным числом.");
            if (string.IsNullOrWhiteSpace(TbVolume.Text))
                errorBuilder.AppendLine("Поле Объем обязательно для заполнения.");
            match = regexSum.Matches(TbVolume.Text);
            if (match.Count == 0)
                errorBuilder.AppendLine("Некорректно введен объем.");
            

            if (errorBuilder.Length > 0)
            {
                errorBuilder.Insert(0, "Устраните следующие ошибки:\n");
            }

            return errorBuilder.ToString();
        }

        private void Btn_img_Click(object sender, RoutedEventArgs e)
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
    }
}
