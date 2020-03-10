﻿using EFCarsambaPazari.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace EFCarsambaPazari
{
    public partial class Form1 : Form
    {
        BazaarContext db = new BazaarContext();

        public Form1()
        {
            InitializeComponent();
            ListProducts();
            ListCategories();
        }

        private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListCategoryProducts();
        }

        private void ListCategoryProducts()
        {
            if (lstCategories.SelectedIndex != -1)
            {
                Category selectedCat = (Category)lstCategories.SelectedItem;
                lstCategoryProducts.DataSource = null;
                lstCategoryProducts.DataSource = selectedCat.Products;
                lstCategoryProducts.DisplayMember = "ProductNameWithPrice";

                lblProductsin.Text = "Products in " + selectedCat.CategoryName;
            }
        }

        private void btnCategoryAdd_Click(object sender, EventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();

            if (categoryName == "")
            {
                MessageBox.Show("Enter category name");
                return;
            }

            if (categoryEdited == null) //veritabanina ekleme yapar
            {
                db.Categories.Add(new Category { CategoryName = categoryName });
                db.SaveChanges();
                txtCategoryName.Clear();
                ListCategories();
            }
            else //duzenleme yapar
            {
                categoryEdited.CategoryName = categoryName;
                db.SaveChanges();
                ListCategories();
                ResetCategoryForm();
            }


        }

        private void ListCategories()
        {
            lstCategories.DataSource = db.Categories.Include("Products").ToList();
            cboCategory.DataSource = db.Categories.ToList();
        }

        private void lstCategories_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstCategories.SelectedIndex > -1)
            {
                Category cat = (Category)lstCategories.SelectedItem;
                db.Categories.Remove(cat);
                db.SaveChanges();
                ListCategories();
                ListProducts();
            }
        }

        private void ListProducts()
        {
            lstProducts.DataSource = db.Products.ToList();
        }

        Category categoryEdited = null;

        private void lstCategories_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lstCategories.IndexFromPoint(e.Location);
            if (index > -1)
            {
                categoryEdited = (Category)lstCategories.SelectedItem;
                txtCategoryName.Text = categoryEdited.CategoryName;
                btnCategoryCancel.Show();
                btnCategoryAdd.Text = "Save";
                txtCategoryName.Focus();
            }
        }

        private void btnCategoryCancel_Click(object sender, EventArgs e)
        {
            ResetCategoryForm();
        }

        private void ResetCategoryForm()
        {
            txtCategoryName.Clear();
            btnCategoryAdd.Text = "Add";
            categoryEdited = null;
            btnCategoryCancel.Hide();
            txtCategoryName.Focus();
        }

        private void btnProductCancel_Click(object sender, EventArgs e)
        {
            ResetProductForm();
        }

        private void ResetProductForm()
        {
            productEdited = null;
            txtProductName.Clear();
            btnProductCancel.Hide();
            nudUnitPrice.Value = 0;
            cboCategory.SelectedIndex = 0;
            txtProductName.Focus();
            btnAProductAdd.Text = "Add";
        }

        private void btnAProductAdd_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();

            if (productName == "")
            {
                MessageBox.Show("Enter a product name");
            }


            if (productEdited == null) //null ise ekleme aciktir
            {
                db.Products.Add(new Product
                {
                    ProductName = productName,
                    UnitPrice = nudUnitPrice.Value,
                    CategoryId = (int)cboCategory.SelectedValue
                });

            }
            else //duzenleme yapar
            {
                productEdited.ProductName = productName;
                productEdited.UnitPrice = nudUnitPrice.Value;
                productEdited.CategoryId = (int)cboCategory.SelectedValue;
            }

            db.SaveChanges();
            ListCategories();
            ListProducts();
            ResetProductForm();
        }

        private void lstCategoryProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lstCategoryProducts.SelectedIndex > -1)
            {
                Product p = (Product)lstCategoryProducts.SelectedItem;
                db.Entry(p).State = EntityState.Deleted;
                db.SaveChanges();

                ListCategories();
                ListProducts();
            }
        }

        Product productEdited = null;

        private void lstCategoryProducts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lstCategoryProducts.IndexFromPoint(e.Location);

            if (index > -1)
            {
                productEdited = (Product)lstCategoryProducts.SelectedItem;
                txtProductName.Text = productEdited.ProductName;
                nudUnitPrice.Value = productEdited.UnitPrice;
                cboCategory.SelectedValue = productEdited.CategoryId;
                btnProductCancel.Show();
                btnAProductAdd.Text = "Save";
                txtProductName.Focus();
            }
        }
    }
}