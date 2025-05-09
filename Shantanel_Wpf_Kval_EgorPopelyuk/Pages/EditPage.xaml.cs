﻿using Microsoft.Win32;
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
        private Product prod = new Product();
        private int number = 0;
        public OpenFileDialog openFileDialog = new OpenFileDialog()
        {
            Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
        };
        public EditPage()
        {
            InitializeComponent();
            TitlePageTB.Text = "Добавление";
            var photo = App.db.Type_product.ToList();
            TypeProdCB.ItemsSource = photo;
            PhotoProdImg.Source = ByteArrayToImage(prod.Images);
            TypeProdCB.DisplayMemberPath = "Name";
            number = 1;
        }

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
            TypeProdCB.SelectedIndex = (int)prod.ID_prod;
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
            prod.Images = File.ReadAllBytes(openFileDialog.FileName);
            if (number == 1)
            {
                App.db.Product.Add(prod);
            }
            App.db.SaveChanges();
            MessageBox.Show("Данные успешно сохранены!!!");
            NavigationService.Navigate(new Pages.MainMarketPage());
        }

        private void Button_Click_EditPhoto(object sender, RoutedEventArgs e)
        {
           
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                prod.Images = File.ReadAllBytes(openFileDialog.FileName);
                PhotoProdImg.Source = new BitmapImage(new Uri(openFileDialog.FileName));

            }
            MessageBox.Show("Вы успешно выбрали новое фото!!!");
        }


        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.MainMarketPage());
        }
    }
}
