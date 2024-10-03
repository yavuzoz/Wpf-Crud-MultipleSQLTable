using Backend.Data.Models;
using Backend.Data;
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
using Microsoft.EntityFrameworkCore;

namespace Desktop_Frontend
{
    /// <summary>
    /// Interaction logic for ProductControl.xaml
    /// </summary>
    public partial class ProductControl : UserControl
    {
      
            private readonly ApplicationDbContext _context;

            public ProductControl()
            {
                InitializeComponent();
                _context = new ApplicationDbContext();
                LoadCategories();
                LoadProducts();
            }

            public void LoadCategories()
            {
                var categories = _context.Categories.ToList();
                cmbCategory.ItemsSource = categories;
            }

            private void LoadProducts()
            {
                var products = _context.Products.Include(p => p.Category).ToList();
                productGrid.ItemsSource = products;
            }

            private void AddProduct_Click(object sender, RoutedEventArgs e)
            {
                if (cmbCategory.SelectedItem is Category selectedCategory)
                {
                    var product = new Product
                    {
                        Name = txtName.Text,
                        Price = decimal.Parse(txtPrice.Text),
                        ManufactureDate = dpManufactureDate.SelectedDate.GetValueOrDefault(),
                        CategoryId = selectedCategory.Id
                    };

                    _context.Products.Add(product);
                    _context.SaveChanges();
                    LoadProducts();
                }
            }

            private void UpdateProduct_Click(object sender, RoutedEventArgs e)
            {
                if (productGrid.SelectedItem is Product selectedProduct)
                {
                    selectedProduct.Name = txtName.Text;
                    selectedProduct.Price = decimal.Parse(txtPrice.Text);
                    selectedProduct.ManufactureDate = dpManufactureDate.SelectedDate.GetValueOrDefault();
                    selectedProduct.CategoryId = ((Category)cmbCategory.SelectedItem).Id;

                    _context.SaveChanges();
                    LoadProducts();
                }
            }

            private void DeleteProduct_Click(object sender, RoutedEventArgs e)
            {
                if (productGrid.SelectedItem is Product selectedProduct)
                {
                    _context.Products.Remove(selectedProduct);
                    _context.SaveChanges();
                    LoadProducts();
                }
            }
        }
    
}
