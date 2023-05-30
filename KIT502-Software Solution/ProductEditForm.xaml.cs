using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xaml;
using KIT502_Software_Solution.Model;
using KIT502_Software_Solution.Utility;
using Microsoft.Win32;

namespace KIT502_Software_Solution
{
    /// <summary>
    /// Interaction logic for ProductEditForm.xaml
    /// </summary>
    public partial class ProductEditForm : Window
    {
        private int tmp_int = 0;
        private double tmp_double = 0;
        private Product? Product;
        private Tv? Tv;
        private Fridge? Fridge;
        private Washing_Machine? Wm;
        private Vacuum_Cleaner? Vc;
        private Air_Fryer? Af;

        private string Selected_Photo = "";

        public ProductEditForm()
        {
            InitializeComponent();
            this.InitializeControls();
        }

        public ProductEditForm(int productId)
        {
            InitializeComponent();
            this.InitializeControls();

            this.Product = Product.GetById(productId);
            this.cmbCategory.IsEnabled = false;
            this.LoadProduct();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.HideAllDetailsSections();

            if (this.cmbCategory.SelectedValue.ToString() == "1")
            {
                this.cnvTv.Visibility = Visibility.Visible;
            }

            if (this.cmbCategory.SelectedValue.ToString() == "2")
            {
                this.cnvFridge.Visibility = Visibility.Visible;
            }

            if (this.cmbCategory.SelectedValue.ToString() == "3")
            {
                this.cnvWashingMachine.Visibility = Visibility.Visible;
            }

            if (this.cmbCategory.SelectedValue.ToString() == "4")
            {
                this.cnvVacuumCleaner.Visibility = Visibility.Visible;
            }

            if (this.cmbCategory.SelectedValue.ToString() == "5")
            {
                this.cnvAirFryer.Visibility = Visibility.Visible;
            }
        }

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.cmbCategory.SelectedValue == null || (int)this.cmbCategory.SelectedValue <= 0)
                {
                    MessageBox.Show("Please select a category first.", "No category", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        this.SaveProduct();
                        this.Close();
                    }
                    catch (Exception f)
                    {
                        MessageBox.Show("The value of " + f.Message + " is invalid",
                            "Invalid input",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                try
                {
                    this.SaveProduct();
                    this.Close();
                }
                catch (Exception f)
                {
                    MessageBox.Show("The value of " + f.Message + " is invalid",
                        "Invalid input",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBrosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";

            if (op.ShowDialog() == true)
            {
                lblSelectedPhotoName.Content = this.Selected_Photo = op.FileName;
            }
        }

        private void InitializeControls()
        {
            this.HideAllDetailsSections();

            var categoryList = Category.LoadAll(true);
            this.cmbCategory.ItemsSource = categoryList;
            this.cmbCategory.SelectedValuePath = "id";
            this.cmbCategory.DisplayMemberPath = "name";
        }

        private void HideAllDetailsSections()
        {
            this.cnvTv.Visibility = Visibility.Hidden;
            this.cnvFridge.Visibility = Visibility.Hidden;
            this.cnvWashingMachine.Visibility = Visibility.Hidden;
            this.cnvVacuumCleaner.Visibility = Visibility.Hidden;
            this.cnvAirFryer.Visibility = Visibility.Hidden;
        }

        private void LoadProduct()
        {
            if (this.Product == null || this.Product.id <= 0)
            {
                return;
            }

            this.HideAllDetailsSections();

            this.cmbCategory.SelectedValue = ValueConvert.ToString(this.Product.category_id.ToString());
            this.txtProductType.Text = ValueConvert.ToString(this.Product.product_type.ToString());
            this.txtBarcode.Text = ValueConvert.ToString(this.Product.barcode.ToString());
            this.txtBrand.Text = ValueConvert.ToString(this.Product.brand.ToString());
            this.txtModel.Text = ValueConvert.ToString(this.Product.model.ToString());
            this.txtWidth.Text = ValueConvert.ToString(this.Product.width.ToString());
            this.txtHeight.Text = ValueConvert.ToString(this.Product.height.ToString());
            this.txtWeight.Text = ValueConvert.ToString(this.Product.weight.ToString());
            this.txtWarrenty.Text = ValueConvert.ToString(this.Product.warranty.ToString());
            this.txtStock.Text = ValueConvert.ToString(this.Product.stock.ToString());
            this.txtListedPrice.Text = ValueConvert.ToString(this.Product.listed_price.ToString());
            this.txtMinimumPrice.Text = ValueConvert.ToString(this.Product.minimum_price.ToString());
            this.txtBasePrice.Text = ValueConvert.ToString(this.Product.base_price.ToString());
            this.chkHomeDelivery.IsChecked = ValueConvert.ToBoolean(this.Product.home_delivery.ToString()) ? true : false;
            this.txtUserRating.Text = ValueConvert.ToString(this.Product.user_rating.ToString());

            if (this.Product.category_id == 1)
            {
                this.Tv = Tv.GetByProductId(this.Product.id);
                this.LoadTv();
            }

            if (this.Product.category_id == 2)
            {
                this.Fridge = Fridge.GetByProductId(this.Product.id);
                this.LoadFridge();
            }

            if (this.Product.category_id == 3)
            {
                this.Wm = Washing_Machine.GetByProductId(this.Product.id);
                this.LoadWashingMachine();
            }

            if (this.Product.category_id == 4)
            {
                this.Vc = Vacuum_Cleaner.GetByProductId(this.Product.id);
                this.LoadVacuumCleaner();
            }

            if (this.Product.category_id == 5)
            {
                this.Af = Air_Fryer.GetByProductId(this.Product.id);
                this.LoadAirFryer();
            }
        }

        private void SaveProduct()
        {
            bool isNewProduct = false;

            if (this.Product == null || this.Product.id <= 0)
            {
                this.Product = new Product();
                isNewProduct = true;
            }
            this.Product.category_id = ValueConvert.ToInt(this.cmbCategory.SelectedValue.ToString());
            this.Product.product_type = this.txtProductType.Text.Trim();
            this.Product.barcode = this.txtBarcode.Text.Trim();
            this.Product.brand = this.txtBrand.Text.Trim();
            this.Product.model = this.txtModel.Text.Trim();
            this.Product.home_delivery = this.chkHomeDelivery.IsChecked == true ? true : false;
            if (double.TryParse(this.txtWidth.Text.Trim(), out tmp_double))
            {
                this.Product.width = ValueConvert.ToDouble(this.txtWidth.Text.Trim());
            }
            else
            {
                throw new ArgumentException("width");
            }
            if (double.TryParse(this.txtHeight.Text.Trim(), out tmp_double))
            {
                this.Product.height = ValueConvert.ToDouble(this.txtHeight.Text.Trim());
            }
            else
            {
                throw new ArgumentException("height");
            }
            if (double.TryParse(this.txtWeight.Text.Trim(), out tmp_double))
            {
                this.Product.weight = ValueConvert.ToDouble(this.txtWeight.Text.Trim());
            }
            else
            {
                throw new ArgumentException("weight");
            }
            if (int.TryParse(this.txtWarrenty.Text.Trim(), out tmp_int))
            {
                this.Product.warranty = ValueConvert.ToInt(this.txtWarrenty.Text.Trim());
            }
            else
            {
                throw new ArgumentException("warranty");
            }
            if (int.TryParse(this.txtStock.Text.Trim(), out tmp_int))
            {
                this.Product.stock = ValueConvert.ToInt(this.txtStock.Text.Trim());
            }
            else
            {
                throw new ArgumentException("stock");
            }
            if (double.TryParse(this.txtListedPrice.Text.Trim(), out tmp_double))
            {
                this.Product.listed_price = ValueConvert.ToDouble(this.txtListedPrice.Text.Trim());
            }
            else
            {
                throw new ArgumentException("listed price");
            }
            if (double.TryParse(this.txtMinimumPrice.Text.Trim(), out tmp_double))
            {
                this.Product.minimum_price = ValueConvert.ToDouble(this.txtMinimumPrice.Text.Trim());
            }
            else
            {
                throw new ArgumentException("minimum price");
            }
            if (double.TryParse(this.txtBasePrice.Text.Trim(), out tmp_double))
            {
                this.Product.base_price = ValueConvert.ToDouble(this.txtBasePrice.Text.Trim());
            }
            else
            {
                throw new ArgumentException("base price");
            }
            this.Product.user_rating = isNewProduct ? 0 : ValueConvert.ToDouble(this.txtUserRating.Text.Trim());
            this.Product.energy_rating = ValueConvert.ToDouble(this.txtEnergyEfficiency.Text.Trim());
            this.Product.depth = ValueConvert.ToDouble(this.txtDepth.Text.Trim());

            if (this.Selected_Photo.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(this.Selected_Photo);
                string destinationFileName = Environment.CurrentDirectory + "/images/" + fileName;
                File.Copy(this.Selected_Photo, destinationFileName);

                this.Product.photo = fileName;
            }
            else if (isNewProduct)
            {
                this.Product.photo = "";
            }

            Message msg = Product.Save(this.Product);

            if(msg.Type == Message.MessageTypes.Information)
            {
                if (isNewProduct)
                {
                    this.Product.id = ValueConvert.ToInt(msg.Msg);
                }

                if (this.Product.category_id == 1)
                {
                    this.GetTv();
                    this.Tv.product_id = this.Product.id;

                    msg = Tv.Save(this.Tv);

                    if (isNewProduct)
                    {
                        this.Tv.id = ValueConvert.ToInt(msg.Msg);
                    }
                }

                if (this.Product.category_id == 2)
                {
                    this.GetFridge();
                    this.Fridge.product_id = this.Product.id;

                    msg = Fridge.Save(this.Fridge);

                    if (isNewProduct)
                    {
                        this.Fridge.id = ValueConvert.ToInt(msg.Msg);
                    }
                }

                if (this.Product.category_id == 3)
                {
                    this.GetWashingMachine();
                    this.Wm.product_id = this.Product.id;

                    msg = Washing_Machine.Save(this.Wm);

                    if (isNewProduct)
                    {
                        this.Wm.id = ValueConvert.ToInt(msg.Msg);
                    }
                }

                if (this.Product.category_id == 4)
                {
                    this.GetVacuumCleaner();
                    this.Vc.product_id = this.Product.id;

                    msg = Vacuum_Cleaner.Save(this.Vc);

                    if (isNewProduct)
                    {
                        this.Vc.id = ValueConvert.ToInt(msg.Msg);
                    }
                }

                if (this.Product.category_id == 5)
                {
                    this.GetAirFryer();
                    this.Af.product_id = this.Product.id;

                    msg = Air_Fryer.Save(this.Af);

                    if (isNewProduct)
                    {
                        this.Af.id = ValueConvert.ToInt(msg.Msg);
                    }
                }
            }

            if(msg.Type == Message.MessageTypes.Information)
            {
                MessageBox.Show("Save successful.");
                this.ClearAll();
                
                if(isNewProduct)
                {
                    this.Close();
                    var plf = new ProductListForm();
                    plf.Show();
                }
            }
            else
            {
                MessageBox.Show(msg.Msg);
            }
        }

        private void LoadTv()
        {
            if (this.Tv == null || this.Tv.id <= 0)
            {
                return;
            }

            this.cnvTv.Visibility = Visibility.Visible;

            this.txtTvRange.Text = ValueConvert.ToString(this.Tv.tv_range.ToString());
            this.txtTvScreenType.Text = ValueConvert.ToString(this.Tv.screen_type.ToString());
            this.txtTvScreenDefinition.Text = ValueConvert.ToString(this.Tv.screen_definition.ToString());
            this.txtTvScreenResolution.Text = ValueConvert.ToString(this.Tv.screen_resolution.ToString());
            this.txtTvScreenSize.Text = ValueConvert.ToString(this.Tv.screen_size.ToString());
            this.txtTvConnectivity.Text = ValueConvert.ToString(this.Tv.connectivity.ToString());
            this.txtTvHdmiPorts.Text = ValueConvert.ToString(this.Tv.no_hdmi_ports.ToString());
            this.txtTvUsbPorts.Text = ValueConvert.ToString(this.Tv.no_usb_ports.ToString());
        }

        private void LoadFridge()
        {
            if (this.Fridge == null || this.Fridge.id <= 0)
            {
                return;
            }

            this.cnvFridge.Visibility = Visibility.Visible;

            this.txtFridgeColor.Text = ValueConvert.ToString(this.Fridge.colour.ToString());
            this.txtFridgeFridgeFeatures.Text = ValueConvert.ToString(this.Fridge.fridge_features.ToString());
            this.txtFridgeFridgeCapacity.Text = ValueConvert.ToString(this.Fridge.fridge_capacity.ToString());
            this.txtFridgeFreezerFeatures.Text = ValueConvert.ToString(this.Fridge.freezer_features.ToString());
            this.txtFridgeFreezerCapacity.Text = ValueConvert.ToString(this.Fridge.freezer_capacity.ToString());
        }

        private void LoadWashingMachine()
        {
            if (this.Wm == null || this.Wm.id <= 0)
            {
                return;
            }

            this.cnvWashingMachine.Visibility = Visibility.Visible;

            this.txtWmColor.Text = ValueConvert.ToString(this.Wm.colour.ToString());
            this.txtWmPowerConsumption.Text = ValueConvert.ToString(this.Wm.power_consumption.ToString());
            this.txtWmWaterEfficiency.Text = ValueConvert.ToString(this.Wm.wels_water_efficiency.ToString());
            this.txtWmWaterConsumption.Text = ValueConvert.ToString(this.Wm.wels_water_consumption.ToString());
            this.txtWmRegistrationNumber.Text = ValueConvert.ToString(this.Wm.wels_registration_number.ToString());
            this.txtWmDelayStart.Text = ValueConvert.ToString(this.Wm.delay_start.ToString());
            this.txtWmWashingCapacity.Text = ValueConvert.ToString(this.Wm.washing_capacity.ToString());
            this.txtWmInternalTubeMaterial.Text = ValueConvert.ToString(this.Wm.internal_tube_material.ToString());
        }

        private void LoadVacuumCleaner()
        {
            if (this.Vc == null || this.Vc.id <= 0)
            {
                return;
            }

            this.cnvVacuumCleaner.Visibility = Visibility.Visible;

            this.txtVcColor.Text = ValueConvert.ToString(this.Vc.colour.ToString());
            this.txtVcMaxCapacity.Text = ValueConvert.ToString(this.Vc.max_capacity.ToString());
            this.txtVcVacuumBag.Text = ValueConvert.ToString(this.Vc.vacuum_bag.ToString());
            this.txtVcStandardRuntime.Text = ValueConvert.ToString(this.Vc.standard_run_time.ToString());
        }

        private void LoadAirFryer()
        {
            if (this.Af == null || this.Af.id <= 0)
            {
                return;
            }

            this.cnvAirFryer.Visibility = Visibility.Visible;

            this.txtAfColor.Text = ValueConvert.ToString(this.Af.colour.ToString());
        }

        private void GetTv()
        {
            if (this.Tv == null || this.Tv.id <= 0)
            {
                this.Tv = new Tv();
            }

            this.Tv.tv_range = this.txtTvRange.Text.Trim();
            this.Tv.screen_type = this.txtTvScreenType.Text.Trim();
            this.Tv.screen_definition = this.txtTvScreenDefinition.Text.Trim();
            this.Tv.screen_resolution = this.txtTvScreenResolution.Text.Trim();
            if (double.TryParse(this.txtTvScreenSize.Text.Trim(), out tmp_double))
            {
                this.Tv.screen_size = ValueConvert.ToDouble(this.txtTvScreenSize.Text.Trim());
            }
            else
            {
                throw new ArgumentException("screen size");
            }
            if (int.TryParse(this.txtTvHdmiPorts.Text.Trim(), out tmp_int))
            {
                this.Tv.no_hdmi_ports = ValueConvert.ToInt(this.txtTvHdmiPorts.Text.Trim());
            }
            else
            {
                throw new ArgumentException("HDMI ports");
            }
            if (int.TryParse(this.txtTvUsbPorts.Text.Trim(), out tmp_int))
            {
                this.Tv.no_usb_ports = ValueConvert.ToInt(this.txtTvUsbPorts.Text.Trim());
            }
            else
            {
                throw new ArgumentException("USB ports");
            }
            this.Tv.connectivity = this.txtTvConnectivity.Text.Trim();
        }

        private void GetFridge()
        {
            if (this.Fridge == null || this.Fridge.id <= 0)
            {
                this.Fridge = new Fridge();
            }

            this.Fridge.colour = this.txtFridgeColor.Text.Trim();
            this.Fridge.fridge_features = this.txtFridgeFridgeFeatures.Text.Trim();
            if (int.TryParse(this.txtFridgeFridgeCapacity.Text.Trim(), out tmp_int))
            {
                this.Fridge.fridge_capacity = ValueConvert.ToInt(this.txtFridgeFridgeCapacity.Text.Trim());
            }
            else
            {
                throw new ArgumentException("fridge capacity");
            }
            this.Fridge.freezer_features = this.txtFridgeFreezerFeatures.Text.Trim();
            if (int.TryParse(this.txtFridgeFreezerCapacity.Text.Trim(), out tmp_int))
            {
                this.Fridge.freezer_capacity = ValueConvert.ToInt(this.txtFridgeFreezerCapacity.Text.Trim());
            }
            else
            {
                throw new ArgumentException("freezer capacity");
            }
        }

        private void GetWashingMachine()
        {
            if (this.Wm == null || this.Wm.id <= 0)
            {
                this.Wm = new Washing_Machine();
            }

            this.Wm.colour = this.txtWmColor.Text.Trim();
            if (int.TryParse(this.txtWmPowerConsumption.Text.Trim(), out tmp_int))
            {
                this.Wm.power_consumption = ValueConvert.ToInt(this.txtWmPowerConsumption.Text.Trim());
            }
            else
            {
                throw new ArgumentException("power consumption");
            }
            if (double.TryParse(this.txtWmWaterEfficiency.Text.Trim(), out tmp_double))
            {
                this.Wm.wels_water_efficiency = ValueConvert.ToDouble(this.txtWmWaterEfficiency.Text.Trim());
            }
            else
            {
                throw new ArgumentException("water efficiency");
            }
            if (int.TryParse(this.txtWmWaterConsumption.Text.Trim(), out tmp_int))
            {
                this.Wm.wels_water_consumption = ValueConvert.ToInt(this.txtWmWaterConsumption.Text.Trim());
            }
            else
            {
                throw new ArgumentException("water consumption");
            }
            this.Wm.wels_registration_number = this.txtWmRegistrationNumber.Text.Trim();
            this.Wm.delay_start = this.txtWmDelayStart.Text.Trim();
            if (int.TryParse(this.txtWmWashingCapacity.Text.Trim(), out tmp_int))
            {
                this.Wm.washing_capacity = ValueConvert.ToInt(this.txtWmWashingCapacity.Text.Trim());
            }
            else
            {
                throw new ArgumentException("water capacity");
            }
            this.Wm.internal_tube_material = this.txtWmInternalTubeMaterial.Text.Trim();
        }

        private void GetVacuumCleaner()
        {
            if (this.Vc == null || this.Vc.id <= 0)
            {
                this.Vc = new Vacuum_Cleaner();
            }

            this.Vc.colour = this.txtVcColor.Text.Trim();
            if (int.TryParse(this.txtVcMaxCapacity.Text.Trim(), out tmp_int))
            {
                this.Vc.max_capacity = ValueConvert.ToInt(this.txtVcMaxCapacity.Text.Trim());
            }
            else
            {
                throw new ArgumentException("max capacity");
            }
            this.Vc.vacuum_bag = this.txtVcVacuumBag.Text.Trim();
            if (int.TryParse(this.txtVcStandardRuntime.Text.Trim(), out tmp_int))
            {
                this.Vc.standard_run_time = ValueConvert.ToInt(this.txtVcStandardRuntime.Text.Trim());
            }
            else
            {
                throw new ArgumentException("standard runtime");
            }
        }

        private void GetAirFryer()
        {
            if (this.Af == null || this.Af.id <= 0)
            {
                this.Af = new Air_Fryer();
            }

            this.Af.colour = this.txtAfColor.Text.Trim();
        }

        private void ClearAll()
        {
            this.txtProductType.Clear();
            this.txtBarcode.Clear();
            this.txtBrand.Clear();
            this.txtModel.Clear();
            this.txtWidth.Clear();
            this.txtHeight.Clear();
            this.txtWeight.Clear();
            this.txtWarrenty.Clear();
            this.txtStock.Clear();
            this.txtListedPrice.Clear();
            this.txtMinimumPrice.Clear();
            this.txtBasePrice.Clear();
            this.chkHomeDelivery.IsChecked = false;
            this.txtUserRating.Clear();
            this.lblSelectedPhotoName.Content = "";
            this.Selected_Photo = "";

            this.txtTvRange.Clear();
            this.txtTvScreenType.Clear();
            this.txtTvScreenDefinition.Clear();
            this.txtTvScreenResolution.Clear();
            this.txtTvScreenSize.Clear();
            this.txtTvConnectivity.Clear();
            this.txtTvHdmiPorts.Clear();
            this.txtTvUsbPorts.Clear();

            this.txtFridgeColor.Clear();
            this.txtFridgeFridgeFeatures.Clear();
            this.txtFridgeFridgeCapacity.Clear();
            this.txtFridgeFreezerFeatures.Clear();
            this.txtFridgeFreezerCapacity.Clear();

            this.txtWmColor.Clear();
            this.txtWmPowerConsumption.Clear();
            this.txtWmWaterEfficiency.Clear();
            this.txtWmWaterConsumption.Clear();
            this.txtWmRegistrationNumber.Clear();
            this.txtWmDelayStart.Clear();
            this.txtWmWashingCapacity.Clear();
            this.txtWmInternalTubeMaterial.Clear();

            this.txtVcColor.Clear();
            this.txtVcMaxCapacity.Clear();
            this.txtVcVacuumBag.Clear();
            this.txtVcStandardRuntime.Clear();

            this.txtAfColor.Clear();
        }
    }
}
