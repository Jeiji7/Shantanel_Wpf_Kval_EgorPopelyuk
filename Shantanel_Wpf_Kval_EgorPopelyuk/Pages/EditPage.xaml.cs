using Microsoft.Win32;
using Shantanel_Wpf_Kval_EgorPopelyuk.Models;
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

namespace Shantanel_Wpf_Kval_EgorPopelyuk.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        private Product prod;
        public EditPage(Product product)
        {
            InitializeComponent();
            prod = product;
            NameProdTB.Text = prod.Name;
            CostTB.Text = (prod.Cost).ToString();
            DiscriptionTB.Text = prod.Discription;
            var photo = App.db.Type_product.ToList();
            TypeProdCB.ItemsSource = photo;
            PhotoProdImg.Source = ByteArrayToImage(prod.Images);
            TypeProdCB.DisplayMemberPath = "Name";
        }

        private ImageSource ByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            using (var stream = new MemoryStream(imageData))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
        private void Button_Click_SaveProduct(object sender, RoutedEventArgs e)
        {
            prod.Name = NameProdTB.Text;
            prod.Cost = int.Parse(CostTB.Text);
            prod.Discription = DiscriptionTB.Text;
            prod.ID_prod = TypeProdCB.SelectedIndex + 1;
            
            App.db.SaveChanges();
            MessageBox.Show("Данные успешно сохранены");
            NavigationService.Navigate(new Pages.MainMarketPage());
        }

        private void Button_Click_EditPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                prod.Images = File.ReadAllBytes(openFileDialog.FileName);
                PhotoProdImg.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
            App.db.SaveChanges();
        }
    }
}
