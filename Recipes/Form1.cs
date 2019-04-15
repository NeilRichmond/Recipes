using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Recipes.Classes;

namespace Recipes
{
    public partial class Form1 : Form
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        DataTable myTable = new DataTable();
        DBAccess myDBAccess = new DBAccess();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateList();
            DisplayList();
            FillDataDridView();
        }

        private void DisplayList()
        {
            foreach(Ingredient i in ingredients)
            {
                if(!listBox1.Items.Contains(i.name))
                {
                    listBox1.Items.Add(i.name);
                }
                if(!listVariant.Items.Contains(i.variant))
                {
                    listVariant.Items.Add(i.variant);
                }
                
            }
        }

        /// <summary>
        /// List of Ingredient objects for various uses
        /// </summary>
        private void PopulateList()
        {
            ingredients = DBAccess.GetIngredientList();
        }

        /// <summary>
        /// Gets DataTable from the Database helper class (myDBAccess) and stores it to
        /// our local copy of the table. This copy can then be used to fill the DataGridView
        /// or to apply/remove filters
        /// </summary>
        private void FillDataDridView()
        {
            myTable = DBAccess.GetTable();
            dataGridView1.DataSource = myTable;
            dataGridView1.Columns["ingredient_id"].Visible = false; //To hide a column
        }

        /// <summary>
        /// lsitBox1 = Name column
        /// Use this to filter the DataGridView
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = listBox1.GetItemText(listBox1.SelectedItem);
            dataGridView1.DataSource = DBAccess.FilterDV(myTable, "name", s);
            UpdateVariantList(s);
        }

        private void txtNameFilter_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBAccess.FilterDV(myTable, "name", txtNameFilter.Text);
        }

        private void UpdateVariantList(string s)
        {
            listVariant.Items.Clear();
            foreach(Ingredient i in ingredients)
            {
                if (i.name == s && !listVariant.Items.Contains(i.variant))
                {
                    listVariant.Items.Add(i.variant);
                }
            }
        }
    }
}
