using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Desktop_Frontend
{
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
            categoryGrid.ItemsSource = categories; // Set the ItemsSource directly
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            // Input validation
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Category name cannot be empty.");
                return;
            }

            var category = new Category
            {
                CategoryName = txtCategoryName.Text
            };

            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                LoadCategories(); // Refresh the grid
                ClearSelection(); // Clear input fields
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while adding the category: {ex.Message}");
            }
        }

        private void UpdateCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ensure a category is selected
                if (_selectedCategory != null)
                {
                    // Fetch the category from the database
                    var existingCategory = _context.Categories.Find(_selectedCategory.Id);
                    if (existingCategory != null)
                    {
                        existingCategory.CategoryName = txtCategoryName.Text; // Update the name
                        _context.SaveChanges(); // Save changes
                        LoadCategories(); // Refresh the grid
                        ClearSelection(); // Clear selection
                    }
                    else
                    {
                        MessageBox.Show("Selected category does not exist in the database.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a category to update.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the category: {ex.Message}");
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Ensure a category is selected
                if (_selectedCategory != null)
                {
                    var result = MessageBox.Show("Are you sure you want to delete this category?", "Confirmation", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        _context.Categories.Remove(_selectedCategory);
                        _context.SaveChanges();
                        LoadCategories(); // Refresh the grid
                        ClearSelection(); // Clear selection
                    }
                }
                else
                {
                    MessageBox.Show("Please select a category to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the category: {ex.Message}");
            }
        }

        private void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the selected category and the text box
            if (categoryGrid.SelectedItem is Category selectedCategory)
            {
                _selectedCategory = selectedCategory;
                txtCategoryName.Text = selectedCategory.CategoryName; // Load the selected category's name into the textbox
            }
        }

        private void ClearSelection()
        {
            _selectedCategory = null;
            txtCategoryName.Text = string.Empty;
            categoryGrid.SelectedIndex = -1;  // Reset the selection in the grid
        }
    }
}
