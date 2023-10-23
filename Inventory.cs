using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManualThrow_Reyes
{
    public partial class Inventory : Form
    {
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        private int _Quantity;
        private double _SellPrice;
        BindingSource showProductList;

        //Creating custom exceptions based on the different return type
        class NumberFormatException : Exception
        {
            public NumberFormatException(string quantity) : base(quantity) { }
        }

        class StringFormatException : Exception
        {
            public StringFormatException(string name) : base(name) { }
        }

        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string price) : base(price) { }
        }

        public Inventory()
        {
            InitializeComponent();
            showProductList = new BindingSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _ProductName = Product_Name(txtProductName.Text); 
            _Category = cbCategory.Text; _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd"); 
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd"); 
            _Description = richTxtDescription.Text; 
            _Quantity = Quantity(txtQuantity.Text); 
            _SellPrice = SellingPrice(txtSellPrice.Text); 
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description)); 
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; gridViewProductList.DataSource = showProductList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ListofProductCategory = new string[]
            {
                "Beverages",
                "Bread/Bakery",
                "Canned/Jarred Foods",
                "Diary",
                "Frozen Foods",
                "Meat",
                "Personal Care",
                "Others"
            };
            foreach (string ProductType in ListofProductCategory) 
            {
                cbCategory.Items.Add(ProductType);
            }
        }
        public string Product_Name(string name)
        {
            try
            {
                if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                {
                    throw new StringFormatException(name);
                }
            }
            catch (StringFormatException stringformat)
            {
                MessageBox.Show("Please input letters for Product Name.");
            }
            finally
            {
                Console.WriteLine("Only letters are allowed in Product Name.");
            }

                return name;
        }
        public int Quantity(string qty)
        {
            try
            {
                if (!Regex.IsMatch(qty, @"^[0-9]"))
                {
                    throw new NumberFormatException(qty);
                }
            }
            catch (NumberFormatException numberformat)
            {
                MessageBox.Show("Please input numbers in Quantity.");
            }
            finally
            {
                Console.WriteLine("Only numbers are allowed in Quantity");
            }
                return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {
            try
            {
                if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                {
                    throw new CurrencyFormatException(price);
                }
            }
            catch (CurrencyFormatException currencyformat)
            {
                MessageBox.Show("Please input only decimal numbers in Selling Price");
            }
            finally
            {
                Console.WriteLine("Only decimal numbers are allowed in Selling Price");
            }
                return Convert.ToDouble(price); 
        }
    }
}
