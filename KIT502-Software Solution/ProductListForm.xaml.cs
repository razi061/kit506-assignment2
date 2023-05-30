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
    /// Interaction logic for ProductListForm.xaml
    /// </summary>
    public partial class ProductListForm : Window
    {
        private Product Selected_Product = new Product();

        public ProductListForm()
        {
            InitializeComponent();

            this.grpProductDetails.Visibility = Visibility.Collapsed;
            this.HideAllDetailsSections();
            this.PopulateData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            this.lbProductList.ItemsSource = null;
            this.grpProductDetails.Visibility = Visibility.Collapsed;
            this.Selected_Product = new Product();
            this.SearchProducts();
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            if (this.Selected_Product.id > 0)
            {
                var pdf = new ProductEditForm(this.Selected_Product.id);
                pdf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product and try again.");
            }
        }

        private void btnReorderProduct_Click(object sender, RoutedEventArgs e)
        {
            if(this.Selected_Product.id > 0)
            {
                var rof = new ReorderForm(this.Selected_Product.id);
                rof.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product and try again.");
            }
        }

        private void lbProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Selected_Product = (Product)lbProductList.SelectedItem;
            this.ShowProductDetails(this.Selected_Product);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var pof = new ProductEditForm();
            pof.Show();
            this.Close();
        }

        private void btnBuyProduct_Click(object sender, RoutedEventArgs e)
        {
            if (this.Selected_Product.id > 0)
            {
                var pf = new PurchaseForm(this.Selected_Product.id);
                pf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product and try again.");
            }
        }

        private void btnSalesHistory_Click(object sender, RoutedEventArgs e)
        {
            if (this.Selected_Product.id > 0)
            {
                SalesHistoryForm shf = new SalesHistoryForm(this.Selected_Product.id);
                shf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a product and try again.");
            }
        }

        private void PopulateData()
        {
            var categoryList = Category.LoadAll(true);
            this.cmbCategory.ItemsSource = categoryList;
            this.cmbCategory.SelectedValuePath = "id";
            this.cmbCategory.DisplayMemberPath = "name";
            this.cmbCategory.SelectedValue = 0;
            this.SearchProducts();
        }

        private void SearchProducts()
        {
            var categoryId = ValueConvert.ToInt(this.cmbCategory.SelectedValue.ToString());
            var query = this.txtSearch.Text.Trim();

            var productList = Product.Find(categoryId, query);
            this.lbProductList.ItemsSource = productList;
            this.lbProductList.SelectedValuePath = "id";
            this.lbProductList.DisplayMemberPath = "name";
        }

        private void HideAllDetailsSections()
        {
            this.cnvTV.Visibility = Visibility.Hidden;
            this.cnvFridge.Visibility = Visibility.Hidden;
            this.cnvWashingMachine.Visibility = Visibility.Hidden;
            this.cnvVacuumCleaner.Visibility = Visibility.Hidden;
            this.cnvAirFryer.Visibility = Visibility.Hidden;
        }

        private void ShowProductDetails(Product product)
        {
            if(this.Selected_Product == null || this.Selected_Product.id <= 0)
            {
                this.grpProductDetails.Visibility = Visibility.Collapsed;
                return;
            }

            this.grpProductDetails.Visibility = Visibility.Visible;
            this.HideAllDetailsSections();

            this.lblProductCode.Content = ValueConvert.ToString(product.barcode.ToString());
            this.lblProductType.Content = ValueConvert.ToString(product.product_type.ToString());
            this.lblBrand.Content = ValueConvert.ToString(product.brand.ToString());
            this.lblModel.Content = ValueConvert.ToString(product.model.ToString());
            this.lblWidth.Content = ValueConvert.ToString(product.width.ToString());
            this.lblHeight.Content = ValueConvert.ToString(product.height.ToString());
            this.lblWeight.Content = ValueConvert.ToString(product.weight.ToString());
            this.lblWarrenty.Content = ValueConvert.ToString(product.warranty.ToString());
            this.lblStock.Content = ValueConvert.ToString(product.stock.ToString());
            this.lblListedPrice.Content = ValueConvert.ToString(product.listed_price.ToString());
            this.lblMinimumPrice.Content = ValueConvert.ToString(product.minimum_price.ToString());
            this.lblBasePrice.Content = ValueConvert.ToString(product.base_price.ToString());
            this.lblHomeDelivery.Content = ValueConvert.ToBoolean(product.home_delivery.ToString()) ? "Yes" : "No";
            this.lblUserRating.Content = ValueConvert.ToString(product.user_rating.ToString());

            if(product.photo != null && product.photo.Length > 0)
            {
                string fileName = Environment.CurrentDirectory + "/images/" + product.photo;

                if(File.Exists(fileName))
                {
                    this.imgImage.Source = new BitmapImage(new Uri(fileName));
                }
            }

            if(product.category_id == 1)
            {
                var tv = Tv.GetByProductId(product.id);
                this.ShowTvDetails(tv);
            }

            if(product.category_id == 2)
            {
                var fridge = Fridge.GetByProductId(product.id);
                this.ShowFridgeDetails(fridge);
            }

            if (product.category_id == 3)
            {
                var wm = Washing_Machine.GetByProductId(product.id);
                this.ShowWashingMachineDetails(wm);
            }

            if (product.category_id == 4)
            {
                var vc = Vacuum_Cleaner.GetByProductId(product.id);
                this.ShowVacuumCleanerDetails(vc);
            }

            if (product.category_id == 5)
            {
                var af = Air_Fryer.GetByProductId(product.id);
                this.ShowAirFryerDetails(af);
            }
        }

        private void ShowTvDetails(Tv tv)
        {
            if (tv == null || tv.id <= 0)
            {
                return;
            }

            this.cnvTV.Visibility= Visibility.Visible;

            this.lblTvRange.Content = ValueConvert.ToString(tv.tv_range.ToString());
            this.lblTvScreenType.Content = ValueConvert.ToString(tv.screen_type.ToString());
            this.lblTvScreenDefinition.Content = ValueConvert.ToString(tv.screen_definition.ToString());
            this.lblTvScreenResolution.Content = ValueConvert.ToString(tv.screen_resolution.ToString());
            this.lblTvScreenSize.Content = ValueConvert.ToString(tv.screen_size.ToString());
            this.lblTvConnectivity.Content = ValueConvert.ToString(tv.connectivity.ToString());
            this.lblTvHdmiPorts.Content = ValueConvert.ToString(tv.no_hdmi_ports.ToString());
            this.lblTvUsbPorts.Content = ValueConvert.ToString(tv.no_usb_ports.ToString());
        }

        private void ShowFridgeDetails(Fridge fridge)
        {
            if (fridge == null || fridge.id <= 0)
            {
                return;
            }

            this.cnvFridge.Visibility = Visibility.Visible;

            this.lblFridgeColor.Content = ValueConvert.ToString(fridge.colour.ToString());
            this.LblFridgeFridgeFeatures.Content = ValueConvert.ToString(fridge.fridge_features.ToString());
            this.lblFridgeFridgeCapacity.Content = ValueConvert.ToString(fridge.fridge_capacity.ToString());
            this.LblFridgeFreezerFeatures.Content = ValueConvert.ToString(fridge.freezer_features.ToString());
            this.lblFridgeFreezerCapacity.Content = ValueConvert.ToString(fridge.freezer_capacity.ToString());
        }

        private void ShowWashingMachineDetails(Washing_Machine wm)
        {
            if (wm == null || wm.id <= 0)
            {
                return;
            }

            this.cnvWashingMachine.Visibility = Visibility.Visible;

            this.lblWmColor.Content = ValueConvert.ToString(wm.colour.ToString());
            this.lblWmPowerConsumption.Content = ValueConvert.ToString(wm.power_consumption.ToString());
            this.lblWmWaterEffieiency.Content = ValueConvert.ToString(wm.wels_water_efficiency.ToString());
            this.lblWmWaterConsumption.Content = ValueConvert.ToString(wm.wels_water_consumption.ToString());
            this.lblWmRegistrationNumber.Content = ValueConvert.ToString(wm.wels_registration_number.ToString());
            this.lblWmDelayStart.Content = ValueConvert.ToString(wm.delay_start.ToString());
            this.lblWmWashingCapacity.Content = ValueConvert.ToString(wm.washing_capacity.ToString());
            this.lblWmInternalTubeMaterial.Content = ValueConvert.ToString(wm.internal_tube_material.ToString());
        }

        private void ShowVacuumCleanerDetails(Vacuum_Cleaner vc)
        {
            if (vc == null || vc.id <= 0)
            {
                return;
            }

            this.cnvVacuumCleaner.Visibility = Visibility.Visible;

            this.lblVcColor.Content = ValueConvert.ToString(vc.colour.ToString());
            this.lblVcMaxCapacity.Content = ValueConvert.ToString(vc.max_capacity.ToString());
            this.lblVcVacuumBag.Content = ValueConvert.ToString(vc.vacuum_bag.ToString());
            this.lblVcStandardRuntime.Content = ValueConvert.ToString(vc.standard_run_time.ToString());
        }

        private void ShowAirFryerDetails(Air_Fryer af)
        {
            if (af == null || af.id <= 0)
            {
                return;
            }

            this.cnvAirFryer.Visibility = Visibility.Visible;

            this.lblAfColor.Content = ValueConvert.ToString(af.colour.ToString());
        }
    }
}
