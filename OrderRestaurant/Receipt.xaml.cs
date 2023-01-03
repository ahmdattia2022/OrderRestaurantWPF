using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OrderRestaurant
{
    /// <summary>
    /// Interaction logic for Receipt.xaml
    /// </summary>
    public partial class Receipt : Window
    {
        ReceiptVM _receiptVM = new ReceiptVM();
        public Receipt(ReceiptVM receiptVM)
        {
            InitializeComponent();
            _receiptVM = receiptVM;
            CustomerNameTxt.Text = receiptVM.CustomerName;
            DateTxt.Text = DateTime.Now.ToString(); 
            ReceiptNoTxt.Text = "0001";
            receiptVM.Orders.Add(new Order { Item = "Fianl Price",Amount = null,Price = null , TotalPrice = receiptVM.FinalPrice});
            DataGrid.ItemsSource = receiptVM.Orders;
            
            DataGrid.Height = double.NaN;

            //FinalPriceTxt.Text = receiptVM.FinalPrice.ToString();
            //ListView.ItemsSource = receiptVM.Orders;
            //Observable<Order> sources = receiptVM.Orders;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;
                PrintDialog printDialog = new PrintDialog();
                if(printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "Receipt");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
