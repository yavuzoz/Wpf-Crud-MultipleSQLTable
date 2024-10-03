using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Backend.Data;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop_Frontend
{
    /// <summary>
    /// Interaction logic for CategoryControl.xaml
    /// </summary>

    public partial class CategoryControl : UserControl
    {
        private readonly ApplicationDbContext _context;
        private Category _selectedCategory;
        private readonly Action _notifyUpdate;

        public CategoryControl(Action notifyUpdate)
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            _notifyUpdate = notifyUpdate;
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = _context.Categories.ToList();
            categoryGrid.ItemsSource = categories;
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            var category = new Category
            {
                CategoryName = txtCategoryName.Text
            };

            _context.Categories.Add(category);
            _context.SaveChanges();
            LoadCategories();
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCategory != null)
            {
                _selectedCategory.CategoryName = txtCategoryName.Text;
                _context.SaveChanges();
                LoadCategories();
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCategory != null)
            {
                _context.Categories.Remove(_selectedCategory);
                _context.SaveChanges();
                LoadCategories();
            }
        }

        private void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (categoryGrid.SelectedItem is Category selectedCategory)
            {
                _selectedCategory = selectedCategory;
                txtCategoryName.Text = selectedCategory.CategoryName;
            }
        }
    }

}
