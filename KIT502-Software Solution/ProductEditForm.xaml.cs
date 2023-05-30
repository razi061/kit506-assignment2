﻿using System;
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
            this.SaveProduct();
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
            this.Product.width = ValueConvert.ToInt(this.txtWidth.Text.Trim());
            this.Product.height = ValueConvert.ToInt(this.txtHeight.Text.Trim());
            this.Product.weight = ValueConvert.ToInt(this.txtWeight.Text.Trim());
            this.Product.warranty = ValueConvert.ToInt(this.txtHeight.Text.Trim());
            this.Product.stock = ValueConvert.ToInt(this.txtStock.Text.Trim());
            this.Product.listed_price = ValueConvert.ToDouble(this.txtListedPrice.Text.Trim());
            this.Product.minimum_price = ValueConvert.ToDouble(this.txtMinimumPrice.Text.Trim());
            this.Product.base_price = ValueConvert.ToDouble(this.txtBasePrice.Text.Trim());
            this.Product.home_delivery = this.chkHomeDelivery.IsChecked == true ? true : false;
            this.Product.user_rating = ValueConvert.ToDouble(this.txtUserRating.Text.Trim());

            if(this.Selected_Photo.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(this.Selected_Photo);
                string destinationFileName = Environment.CurrentDirectory + "/images/" + fileName;
                File.Copy(this.Selected_Photo, destinationFileName);

                this.Product.photo = destinationFileName;
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
                }

                if (this.Product.category_id == 3)
                {
                    this.GetWashingMachine();
                    this.Wm.product_id = this.Product.id;
                }

                if (this.Product.category_id == 4)
                {
                    this.GetVacuumCleaner();
                    this.Vc.product_id = this.Product.id;
                }

                if (this.Product.category_id == 5)
                {
                    this.GetAirFryer();
                    this.Af.product_id = this.Product.id;
                }
            }

            if(msg.Type == Message.MessageTypes.Information)
            {
                MessageBox.Show("Save successfull.");
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
            this.Tv.screen_size = ValueConvert.ToDouble(this.txtTvScreenSize.Text.Trim());
            this.Tv.no_hdmi_ports = ValueConvert.ToInt(this.txtTvHdmiPorts.Text.Trim());
            this.Tv.no_usb_ports = ValueConvert.ToInt(this.txtTvUsbPorts.Text.Trim());
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
            this.Fridge.fridge_capacity = ValueConvert.ToInt(this.txtFridgeFridgeCapacity.Text.Trim());
            this.Fridge.freezer_features = this.txtFridgeFreezerFeatures.Text.Trim();
            this.Fridge.freezer_capacity = ValueConvert.ToInt(this.txtFridgeFreezerCapacity.Text.Trim());
        }

        private void GetWashingMachine()
        {
            if (this.Wm == null || this.Wm.id <= 0)
            {
                this.Wm = new Washing_Machine();
            }

            this.Wm.colour = this.txtWmColor.Text.Trim();
            this.Wm.power_consumption = ValueConvert.ToInt(this.txtWmPowerConsumption.Text.Trim());
            this.Wm.wels_water_efficiency = ValueConvert.ToDouble(this.txtWmWaterEfficiency.Text.Trim());
            this.Wm.wels_water_consumption = ValueConvert.ToInt(this.txtWmWaterConsumption.Text.Trim());
            this.Wm.wels_registration_number = this.txtWmRegistrationNumber.Text.Trim();
            this.Wm.delay_start = this.txtWmDelayStart.Text.Trim();
            this.Wm.washing_capacity = ValueConvert.ToInt(this.txtWmWashingCapacity.Text.Trim());
            this.Wm.internal_tube_material = this.txtWmInternalTubeMaterial.Text.Trim();
        }

        private void GetVacuumCleaner()
        {
            if (this.Vc == null || this.Vc.id <= 0)
            {
                this.Vc = new Vacuum_Cleaner();
            }

            this.Vc.colour = this.txtVcColor.Text.Trim();
            this.Vc.max_capacity = ValueConvert.ToInt(this.txtVcMaxCapacity.Text.Trim());
            this.Vc.vacuum_bag = this.txtVcVacuumBag.Text.Trim();
            this.Vc.standard_run_time = ValueConvert.ToInt(this.txtVcStandardRuntime.Text.Trim());
        }

        private void GetAirFryer()
        {
            if (this.Af == null || this.Af.id <= 0)
            {
                this.Af = new Air_Fryer();
            }

            this.Af.colour = this.txtAfColor.Text.Trim();
        }
    }
}
