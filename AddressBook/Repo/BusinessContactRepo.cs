using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Repo
{
    class BusinessContactRepo
    {
        AddressBookEntities _context = new AddressBookEntities();

        public BusinessContactRepo(AddressBookEntities context)
        {
            _context = context;
        }

        //Retrieve all business contacts
        public List<BusinessContact> getData()
        {
            var contactList = context.BusinessContacts.ToList();
            return contactList;
        }

        //Add a business contact
        public void addData(DateTime date, string company, string website, string title, string fName, string lName, string address, 
            string city, string province, string postalCode,string email, string phone, string notes )
        {
            BusinessContact businessContact = new BusinessContact();

            businessContact.Date_Added = date;
            businessContact.Company = company;
            businessContact.Website = website;
            businessContact.Title = title;
            businessContact.First_Name = fName;
            businessContact.Last_Name = lName;
            businessContact.Address = address;
            businessContact.City = city;
            businessContact.Province = province;
            businessContact.Postal_Code = postalCode;
            businessContact.Email = email;
            businessContact.Phone = phone;
            businessContact.Notes = notes;

            _context.BusinessContacts.Add(businessContact);  //Add businessContact object
            _context.SaveChanges(); //Save the changes
        }

        //Update a business contact
        public void updateData(int id, DateTime date, string company, string website, string title, string fName, string lName, string address,
            string city, string province, string postalCode, string email, string phone, string notes)
        {
            BusinessContact businessContact = (from b in _context.BusinessContacts
                                               where b.ID == id
                                               select b).FirstOrDefault();
            if(businessContact != null)
            {
                businessContact.Date_Added = date;
                businessContact.Company = company;
                businessContact.Website = website;
                businessContact.Title = title;
                businessContact.First_Name = fName;
                businessContact.Last_Name = lName;
                businessContact.City = city;
                businessContact.Province = province;
                businessContact.Postal_Code = postalCode;
                businessContact.Email = email;
                businessContact.Phone = phone;
                businessContact.Notes = notes;

                _context.SaveChanges();
            }
        }

        //Delete a business contact
        public void deleteData(int id)
        {
            BusinessContact businessContact = (from b in _context.BusinessContacts
                                               where b.ID == id
                                               select b).FirstOrDefault();
            if (businessContact != null)
            {
                _context.BusinessContacts.Remove(businessContact);
                _context.SaveChanges();
            }
        }

        //Search by first name
        public List<BusinessContact> getByFName(string fName)
        {
            var contactList = _context.BusinessContacts.Where(row => row.First_Name.ToLower().Contains(fName)).ToList();
            return contactList;
        }

        //Search by last name
        public List<BusinessContact> getByLName(string lName)
        {
            var contactList = _context.BusinessContacts.Where(row => row.Last_Name.ToLower().Contains(lName)).ToList();
            return contactList;
        }

        //Search by company
        public List<BusinessContact> getCompany(string company)
        {
            var contactList = _context.BusinessContacts.Where(row => row.Company.ToLower().Contains(company)).ToList();
            return contactList;
        }

    }
}
