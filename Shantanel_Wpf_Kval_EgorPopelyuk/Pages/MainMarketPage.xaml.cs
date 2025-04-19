using Shantanel_Wpf_Kval_EgorPopelyuk.Models;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для MainMarketPage.xaml
    /// </summary>
    public partial class MainMarketPage : Page
    {
        public MainMarketPage()
        {
            InitializeComponent();
            ProductListLW.ItemsSource = App.db.Product.ToList();
            CountProdTB.Text = App.db.Product.Count().ToString();
        }
  

        private void Button_Click_Collapse(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this);
            if (mainWindow != null)
            {
                mainWindow.WindowState = WindowState.Minimized;
            }
        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ProductListLW_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductListLW.SelectedItem != null)
            {
                var selectedItem = (Product)ProductListLW.SelectedItem;
                NavigationService.Navigate(new Pages.EditPage(selectedItem));
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var listView = App.db.Product.ToList();
            if (!string.IsNullOrWhiteSpace(SearchTB.Text))
            {
                listView = listView.Where(x => x.Name.Contains(SearchTB.Text)).ToList();
            }
            ProductListLW.ItemsSource = listView;
        }


        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {

        }

    }
}
