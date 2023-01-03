using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

namespace OrderRestaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Item> items = new List<Item>();
        public List<Order> orders;
        public Order order;
        ReceiptVM receiptVM = new ReceiptVM();
        public MainWindow()
        {
            InitializeComponent();
            orders = new List<Order>();

            items = new List<Item>
            {
                new Item{Id = 1, Name = "Pizza L", Price = 100 },
                new Item{Id = 2, Name = "Sandwich", Price = 50 },
                new Item{Id = 3, Name = "pizza M ", Price = 80 },
                new Item{Id = 3, Name = "pizza S ", Price = 55 },
                new Item{Id = 3, Name = "7wawshi ", Price = 40 },
                new Item{Id = 3, Name = "Mega Rizo", Price = 88 },
                new Item{Id = 3, Name = "Mighty Love", Price = 184 },
                new Item{Id = 3, Name = "Supreme Love", Price = 146 },
                new Item{Id = 3, Name = "Mighty Plus", Price = 131 },
            };

            ItemsCmb.ItemsSource = items;
            ItemsCmb.SelectedValuePath = "Id";
            ItemsCmb.DisplayMemberPath = "Name";

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            if (ItemsCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Please select Item");
                return;
            }
            //DataGrid.ItemsSource = null;
            Item item = items.Where(x => x.Id == (int)ItemsCmb.SelectedValue).FirstOrDefault();
            order = new Order();
            order.Item = item.Name;
            order.Price = item.Price;
            order.Amount = (AmountTxt.Text == "") ? 1 : int.Parse(AmountTxt.Text);
            order.TotalPrice = (decimal)(item.Price * order.Amount);//validation if text is null add 1 item
            
            DataGrid.Items.Add(order);
            orders.Add(order);
            //start::clear
            ItemsCmb.SelectedIndex = -1;
            AmountTxt.Text = "";
            //end::Clear



        }

        private void printBrn_Click(object sender, RoutedEventArgs e)
        {
            if (CustomerTxt.Text == "" || DataGrid.Items.Count == 0)
            {
                MessageBox.Show("Please fill the required items to print ");
                return;
            }
            //map to view model
            receiptVM.CustomerName = CustomerTxt.Text;
            receiptVM.Date = DateTime.Now;
            receiptVM.Orders = orders;
            receiptVM.FinalPrice = receiptVM.Orders.Sum(x => x.TotalPrice);
            //set view model to the constructor of receipt


            Receipt receipt = new Receipt(receiptVM);
            receipt.Show();
            this.Close();
        }

        private void AmountTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void CustomerTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z_ ]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {

            var Result = MessageBox.Show("Cancel Order", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (Result == MessageBoxResult.Yes)
            {
                DataGrid.Items.Clear();
                orders = new List<Order>();
                DataGrid.Items.Refresh();
            }
            //bool result = MessageBox.Show("Cancel order", "are you sure you want to cancel order?", MessageBoxButton.YesNo, MessageBoxImage.Warning)
        }

        private void DataGrid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            
        }

    }
}
