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
    /// Логика взаимодействия для MarketPages.xaml
    /// </summary>
    public partial class MarketPages : Page
    {
        public Product prod = new Product();
        public MarketPages()
        {
            InitializeComponent();
            var photo = App.db.Product.ToList();
            photo.Insert(0,new Product(){ ID = 0, Name = "Все"});
            PhotoCB.ItemsSource = photo;
            PhotoCB.DisplayMemberPath = "Name";
        }

        private void Photo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                prod.Images = File.ReadAllBytes(openFileDialog.FileName);
                PHTIMG.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
            App.db.SaveChanges();
        }

        private void PhotoCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (Product)PhotoCB.SelectedItem;
            prod = item;
            NameTB.Text = prod.Name;
            PHTIMG.Source = ByteArrayToImage(prod.Images);

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Photo();
        }
    }
}
