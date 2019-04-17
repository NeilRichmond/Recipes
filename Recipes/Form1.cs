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
        DataView myView = new DataView();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTable = DBAccess.GetTable();
            myView = myTable.AsDataView();

            PopulateList();
            DisplayList();
            FillDataDridView();

        }

        private void DisplayList()
        {
            string s = "";
            foreach(Ingredient i in ingredients)
            {
                if(!listBox1.Items.Contains(i.type))
                {
                    listBox1.Items.Add(i.type);
                }
                s = i.name + ", " + i.variant;
                if(!listVariant.Items.Contains(s))
                {
                    listVariant.Items.Add(s);
                }
                if (!listTypes.Items.Contains(i.type))
                {
                    listTypes.Items.Add(i.type);
                    cmbType.Items.Add(i.type);
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
            dataGridView1.DataSource = myView;
            dataGridView1.Columns["ingredient_id"].Visible = false; //To hide id column
        }

        /// <summary>
        /// lsitBox1 = Name column
        /// Use this to filter the DataGridView
        /// </summary>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = listBox1.GetItemText(listBox1.SelectedItem);
            //dataGridView1.DataSource = DBAccess.FilterDV(myTable, "name", s);
            myView = DBAccess.FilterDV(myView, "name", s);
            FillDataDridView();
            UpdateVariantList(s);
        }

        private void txtNameFilter_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DBAccess.FilterDV(myView, "name", txtNameFilter.Text);
            UpdateVariantList(txtNameFilter.Text); //Will only work when the whole item is typed out, case sensitive
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

        private void UpdateTypesList(string s)
        {
            listTypes.Items.Clear();
            foreach (Ingredient i in ingredients)
            {
                if (i.type == s && !listTypes.Items.Contains(i.type))
                {
                    listTypes.Items.Add(i.type);
                }
            }

            myView = DBAccess.Filter("type", s);
            FillDataDridView();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTypesList(cmbType.GetItemText(cmbType.SelectedItem));
        }

        private void listTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox2.DataSource = DBAccess.QueryList(textBox1.Text, textBox2.Text);
        }
    }
}
