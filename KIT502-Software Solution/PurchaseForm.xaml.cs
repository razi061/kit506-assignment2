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
using System.Windows.Shapes;

using KIT502_Software_Solution.Model;
using KIT502_Software_Solution.Utility;

namespace KIT502_Software_Solution
{
    /// <summary>
    /// Interaction logic for PurchaseForm.xaml
    /// </summary>
    public partial class PurchaseForm : Window
    {
        private Product Product;
        private Voucher? Voucher;

        public PurchaseForm(int productId)
        {
            InitializeComponent();

            this.Product = Product.GetById(productId);
            this.LoadPaymentTypes();
            this.LoadProduct();
        }

        private void btnSavePurchase_Click(object sender, RoutedEventArgs e)
        {
            int quantity = ValueConvert.ToInt(this.txtProductQuantity.Text.Trim());

            if (quantity < 0) 
            {
                MessageBox.Show("Please enter quantity.");
                return;
            }

            Buyer buyer = new Buyer
            {
                name = this.txtCustomerName.Text.Trim(),
                email = this.txtCustomerEmail.Text.Trim(),
                address = this.txtCustomerAddress.Text.Trim()
            };

            if (buyer.name == "")
            {
                MessageBox.Show("Please enter customer name.");
                return;
            }

            if(this.chkHomeDelivery.IsChecked == true && buyer.address == "")
            {
                MessageBox.Show("Address is required for home delivery.");
                return;
            }

            Message msg = ValidatePaymentDetails();

            if(msg.Type == Message.MessageTypes.Error)
            {
                MessageBox.Show(msg.Msg);
                return;
            }

            Purchase purchase = new Purchase()
            {
                product_id = this.Product.id,
                quantity = ValueConvert.ToInt(this.txtProductQuantity.Text.Trim()),
                sales_price = this.GetTotalPrice(),
                payment_type_id = ValueConvert.ToInt(this.cmbPaymentType.SelectedValue.ToString()),
                voucher_id = (this.Voucher != null && this.Voucher.id > 0) ? this.Voucher.id : 0,
                total = this.GetTotalPrice(),
                purchase_datetime = DateTime.Now,
                home_delivery = this.chkHomeDelivery.IsChecked == true ? true : false,
                payment_details = this.GetPaymentDetails()
            };

            Buyer dbBuyer = Buyer.GetByEmail(buyer.email);
            
            if(dbBuyer == null || dbBuyer.id <= 0)
            {
                msg = Buyer.Save(buyer);

                if (msg.Type == Message.MessageTypes.Error)
                {
                    MessageBox.Show(msg.Msg);
                    return;
                }

                buyer.id = ValueConvert.ToInt(msg.Msg);
                purchase.buyer_id = buyer.id;
            }
            else
            {
                purchase.buyer_id = dbBuyer.id;
            }

            msg = Purchase.Save(purchase);
            
            if(msg.Type == Message.MessageTypes.Error)
            {
                MessageBox.Show(msg.Msg);
                return;
            }

            msg = Product.DecreaseStock(Product.id, purchase.quantity);
        }

        private void txtProductQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            int quantity = 1;

            if(!int.TryParse(this.txtProductQuantity.Text.Trim(), out quantity))
            {
                MessageBox.Show("Only numbers are allowed in quantity.");
                this.txtProductQuantity.Text = "1";
                quantity = 1;
            }

            if(quantity > this.Product.stock)
            {
                MessageBox.Show("Current stock is this.Product.stock.");
                this.txtProductQuantity.Text = this.Product.stock.ToString();
                quantity = this.Product.stock;
            }

            this.SetTotalPrice();
        }

        private void btnVouchedValidity_Click(object sender, RoutedEventArgs e)
        {
            if(this.txtVoucherId.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a voucher Id.");
                return;
            }

            if (this.txtCustomerEmail.Text.Trim() == "")
            {
                MessageBox.Show("Please enter customer email.");
                return;
            }

            Buyer buyer = Buyer.GetByEmail(this.txtCustomerEmail.Text.Trim());

            if(buyer == null || buyer.id <= 0)
            {
                MessageBox.Show("You are a new customer. Vouchers does noy apply to new buyers.");
                return;
            }

            Voucher voucher = Voucher.GetById(ValueConvert.ToInt(this.txtVoucherId.Text.Trim()));

            if(voucher == null || voucher.id <= 0)
            {
                MessageBox.Show("No voucher found.");
                return;
            }

            if (voucher.expiry_date.Date < DateTime.Now)
            {
                MessageBox.Show("This voucher expired on " + voucher.expiry_date.ToString("yyyy-MM-dd"));
                return;
            }

            if(voucher.buyer_id != buyer.id)
            {
                MessageBox.Show("This voucher was issued to another user.");
                return;
            }

            this.Voucher = voucher;
            this.txtCustomerEmail.IsEnabled = false;
            this.SetTotalPrice();
        }

        private void txtVoucherId_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.Voucher = null;
            this.txtCustomerEmail.IsEnabled = true;
            this.SetTotalPrice();
        }

        private void cmbPaymentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.lblCardNo.Visibility = Visibility.Collapsed;
            this.txtCardNo.Visibility = Visibility.Collapsed;
            this.lblCardExpiry.Visibility = Visibility.Collapsed;
            this.txtCardExpiry.Visibility = Visibility.Collapsed;
            this.lblCardCvv.Visibility = Visibility.Collapsed;
            this.txtCardCvv.Visibility = Visibility.Collapsed;

            if (this.cmbPaymentType.SelectedValue.ToString() == "1")
            {

            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "2")
            {
                this.lblCardNo.Visibility = Visibility.Visible;
                this.lblCardNo.Content = "Card No: ";
                this.txtCardNo.Visibility = Visibility.Visible;
                this.lblCardExpiry.Visibility = Visibility.Visible;
                this.txtCardExpiry.Visibility = Visibility.Visible;
                this.lblCardCvv.Visibility = Visibility.Visible;
                this.txtCardCvv.Visibility = Visibility.Visible;
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "3")
            {
                this.lblCardNo.Visibility = Visibility.Visible;
                this.lblCardNo.Content = "Gift Card No: ";
                this.txtCardNo.Visibility = Visibility.Visible;
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "4")
            {
                this.lblCardNo.Visibility = Visibility.Visible;
                this.lblCardNo.Content = "ZIP No: ";
                this.txtCardNo.Visibility = Visibility.Visible;
            }
        }

        private void LoadProduct() 
        {
            this.lblProductName.Content = this.Product.name;
            this.lblProductPrice.Content = "Price: " + this.Product.listed_price;
            this.lblProductStock.Content = "Current Stock: " + Product.stock;
            this.txtProductQuantity.Text = "1";
            
            if(this.Product.home_delivery)
            {
                this.chkHomeDelivery.IsEnabled = true;
            }
            else
            {
                this.chkHomeDelivery.IsChecked = false;
                this.chkHomeDelivery.IsEnabled = false;
            }

            if (this.Product.photo != null && this.Product.photo.Length > 0)
            {
                string fileName = Environment.CurrentDirectory + "/images/" + this.Product.photo;

                if (File.Exists(fileName))
                {
                    this.imgImage.Source = new BitmapImage(new Uri(fileName));
                }
            }
        }

        private void LoadPaymentTypes()
        {
            var paymentTypeList = Payment_Type.LoadAll(false);
            this.cmbPaymentType.ItemsSource = paymentTypeList;
            this.cmbPaymentType.SelectedValuePath = "id";
            this.cmbPaymentType.DisplayMemberPath = "name";
            this.cmbPaymentType.SelectedValue = "1";
        }

        private double SetTotalPrice()
        {
            this.lblTotalPrice.Content = "Total price: " + this.GetTotalPrice();
            int quantity = ValueConvert.ToInt(this.txtProductQuantity.Text.Trim());
            return quantity * this.Product.listed_price;
        }

        private double GetTotalPrice()
        {
            int quantity = ValueConvert.ToInt(this.txtProductQuantity.Text.Trim());
            double price = quantity * this.Product.listed_price;

            if(this.Voucher != null && this.Voucher.id > 0)
            {
                price -= (price * this.Voucher.discount / 100);
            }

            return price;
        }

        private string GetPaymentDetails()
        {
            string details = "";

            if (this.cmbPaymentType.SelectedValue.ToString() == "1")
            {
                details = "Cash;";
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "2")
            {
                details = "Card; Card No: " + this.txtCardNo.Text.Trim() +
                    ", card expiry: " + this.txtCardExpiry.Text.Trim();
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "3")
            {
                details = "Gift Card; Gift card No: " + this.txtCardNo.Text.Trim();
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "4")
            {
                details = "ZIP; Zip No: " + this.txtCardNo.Text.Trim();
            }

            return details;
        }

        private Message ValidatePaymentDetails()
        {
            Message msg = new Message(Message.MessageTypes.Information, "");

            if (this.cmbPaymentType.SelectedValue.ToString() == "1")
            {
                
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "2")
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    msg = new Message(Message.MessageTypes.Error, "Please enter card number.");
                    return msg;
                }

                if (this.txtCardExpiry.Text.Trim() == "")
                {
                    msg = new Message(Message.MessageTypes.Error, "Please enter card expiry date.");
                    return msg;
                }

                if (this.txtCardCvv.Text.Trim() == "")
                {
                    msg = new Message(Message.MessageTypes.Error, "Please enter card CVV number.");
                    return msg;
                }

            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "3")
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    msg = new Message(Message.MessageTypes.Error, "Please enter gift card number.");
                    return msg;
                }
            }

            if (this.cmbPaymentType.SelectedValue.ToString() == "4")
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    msg = new Message(Message.MessageTypes.Error, "Please enter ZIP number.");
                    return msg;
                }
            }

            return msg;
        }
    }
}
