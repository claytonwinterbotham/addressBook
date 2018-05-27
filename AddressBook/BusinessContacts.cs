using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AddressBook.Repo;

namespace AddressBook
{
    public partial class BusinessContacts : Form
    {
        AddressBookEntities context = new AddressBookEntities();
        bool gridFocus = false;

        public BusinessContacts()
        {
            InitializeComponent();
        }


        //When the main form loads display data
        private void BusinessContacts_Load(object sender, EventArgs e)
        {
            txtID.Hide();
            btnEdit.Enabled = false; //Disable edit button when form first loads
            btnDelete.Enabled = false; //Disable edit button when form first loads
            comboBox1.SelectedIndex = 0; //First item in combobox is selected when the from loads
            GetContacts();
        }

        //Method to GetContacts
        private void GetContacts()
        {
            BusinessContactRepo businessContactRepo = new BusinessContactRepo(context);
            bindingSource1.DataSource = businessContactRepo.getData(); //Set binding source to the result of the select all query from repository.
            dataGridView1.DataSource = bindingSource1; //Set the datagridview source to the binding source.
            dataGridView1.Columns[0].ReadOnly = true; //Set id column to readonly.
            dataGridView1.Refresh(); //Refresh the gridView
        }

        //Add new contact when add button is clicked
        private void btnAdd_Click(object sender, EventArgs e)
        {
            BusinessContactRepo businessContactRepo = new BusinessContactRepo(context);

            //Read values from form and add to addData method as parameters.
            businessContactRepo.addData(
                    dateTimePicker1.Value.Date,
                    txtWebsite.Text,
                    txtCompany.Text,
                    txtTitle.Text,
                    txtFName.Text,
                    txtLName.Text,
                    txtAddress.Text,
                    txtCity.Text,
                    txtProvince.Text,
                    txtPostalCode.Text,
                    txtEmail.Text,
                    txtPhoneNumber.Text,
                    txtNotes.Text
                );
            GetContacts(); //Reload contact list
        }

        //This allow use to edit individual cells in the gridview table
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            //retrieve values from the selected row and update to database
            var id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            var date = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            var company = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            var website = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
            var title = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
            var fn = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
            var ls = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
            var address = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
            var city = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            var province = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
            var postalCode = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
            var phoneNumber = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
            var email = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            var notes = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[13].Value);

            BusinessContactRepo businessContactRepo = new BusinessContactRepo(context);

            businessContactRepo.updateData(id, date, company, website, title, fn, ls, address, city,
                province, postalCode, email, phoneNumber, notes);//update the database

            MessageBox.Show("Update Successful"); //Let user know update was successful

            GetContacts(); //Reload contact list

        }

        //This changes the state of the form depending on what row is in focus
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Implement the following if grid is in focus
            if (gridFocus)
            {
                int rowIndex = dataGridView1.Rows.Count - 1; //Find the last row of the dataGridtable
                if (e.RowIndex == rowIndex) //If the last(empty) row is in focus
                {
                    btnAdd.Enabled = true; //Enable add button

                    //Disable edit and delete buttons
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;

                    //Empty the form
                    emptyFields();
                }
                else //If any data filled row is in focus
                {
                    btnAdd.Enabled = false; // Disable the add button

                    //Enable the edit and delete buttons
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    //Populate the form inputs with the associate selected row data
                    txtID.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    dateTimePicker1.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                    txtCompany.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                    txtWebsite.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    txtTitle.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                    txtFName.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[5].Value);
                    txtLName.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                    txtAddress.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[7].Value);
                    txtCity.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
                    txtProvince.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[9].Value);
                    txtPostalCode.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[10].Value);
                    txtPhoneNumber.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[11].Value);
                    txtEmail.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
                    txtNotes.Text = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[13].Value);

                }
            }

        }

        //Update contact when edit button is clicked
        private void btnEdit_Click(object sender, EventArgs e)
        {
            BusinessContactRepo businessContactRepo = new BusinessContactRepo(context);

            //Get id from hidden text input and convert to int
            int id = Convert.ToInt32(txtID.Text);

            businessContactRepo.updateData(
                    id,
                    dateTimePicker1.Value.Date,
                    txtWebsite.Text,
                    txtCompany.Text,
                    txtTitle.Text,
                    txtFName.Text,
                    txtLName.Text,
                    txtAddress.Text,
                    txtCity.Text,
                    txtProvince.Text,
                    txtPostalCode.Text,
                    txtEmail.Text,
                    txtPhoneNumber.Text,
                    txtNotes.Text);//update the database

            MessageBox.Show("Update Successful"); //Let user know update was successful

            GetContacts(); //Reload contact list

        }

        //Delete contact when delete button is clicked
        private void btnDelete_Click(object sender, EventArgs e)
        {
            BusinessContactRepo businessContactRepo = new BusinessContactRepo(context);

            //Get id from hidden text input and convert to int
            int id = Convert.ToInt32(txtID.Text);

            businessContactRepo.deleteData(
                    id);//remove from database

            MessageBox.Show("Contact Removed"); //Let user know contact was removed

            GetContacts(); //Reload contact list
        }

        //If the datagrid is in focus change gridFocus boolean to true
        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            gridFocus = true;
        }

        //This method clears the input fields in the form
        private void emptyFields()
        {
            txtID.Text = "";
            dateTimePicker1.Text = "";
            txtCompany.Text = "";
            txtWebsite.Text = "";
            txtTitle.Text = "";
            txtFName.Text = "";
            txtLName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtProvince.Text = "";
            txtPostalCode.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtNotes.Text = "";
            txtSearch.Text = "";

        }

        //Clear form when reset button is clicked
        private void btnReset_Click(object sender, EventArgs e)
        {
            emptyFields();

            btnAdd.Enabled = true; //Enable add button

            //Disable edit and delete buttons
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        //Allow users to refine results in datagrid by searching by firstname, lastname or company.
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BusinessContactRepo businessContactRepo = new BusinessContactRepo(context);
            switch (comboBox1.SelectedItem.ToString())
            {
                case "First Name":
                    bindingSource1.DataSource = businessContactRepo.getByFName(txtSearch.Text.ToLower()); //Set binding source to the result of the select by firstname query from repository.
                    dataGridView1.DataSource = bindingSource1; //Set the datagridview source to the binding source.
                    dataGridView1.Columns[0].ReadOnly = true; //Set id column to readonly.
                    dataGridView1.Refresh(); //Refresh the gridView
                    break;
                case "Last Name":
                    bindingSource1.DataSource = businessContactRepo.getByLName(txtSearch.Text.ToLower()); //Set binding source to the result of the select by lastname query from repository.
                    dataGridView1.DataSource = bindingSource1; //Set the datagridview source to the binding source.
                    dataGridView1.Columns[0].ReadOnly = true; //Set id column to readonly.
                    dataGridView1.Refresh(); //Refresh the gridView
                    break;
                case "Company":
                    bindingSource1.DataSource = businessContactRepo.getCompany(txtSearch.Text.ToLower()); //Set binding source to the result of the select by company query from repository.
                    dataGridView1.DataSource = bindingSource1; //Set the datagridview source to the binding source.
                    dataGridView1.Columns[0].ReadOnly = true; //Set id column to readonly.
                    dataGridView1.Refresh(); //Refresh the gridView
                    break;

            }
        }
    }
}
