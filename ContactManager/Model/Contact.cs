using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Model
{
    [Serializable]
    public class Contact : Notifier
    {
        private Address _address = new Address();
        private string _cellPhone;
        private string _firstName;
        private string _homePhone;
        private Guid _id = Guid.Empty;
        private string _imagePath;
        private string _jobTitle;
        private string _lastName;
        private string _officePhone;
        private string _organization;
        private string _primaryEmail;
        private string _secondaryEmail;



        public string SecondaryEmail
        {
            get { return _secondaryEmail; }
            set 
            {
                _secondaryEmail = value;
                OnPropertyChanged("SecondaryEmail");
            }
        }

        public string PrimaryEmail
        {
            get { return _primaryEmail; }
            set 
            {
                _primaryEmail = value;
                OnPropertyChanged("PrimaryEmail");
            }
        }


        public string Organization
        {
            get { return _organization; }
            set
            {
                _organization = value;
                OnPropertyChanged("Organization");
            }
        }

        public string OfficePhone
        {
            get { return _officePhone; }
            set 
            {
                _officePhone = value;
                OnPropertyChanged("OfficePhone");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }


        public string JobTitle
        {
            get { return _jobTitle; }
            set 
            {
                _jobTitle = value;
                OnPropertyChanged("JobTitle");
            }
        }


        public string ImagePath
        {
            get { return _imagePath; }
            set 
            {
                _imagePath = value;
                OnPropertyChanged("ImagePath");
            }
        }


        public Guid Id
        {
            get { return _id; }
            set 
            { 
                _id = value;
                OnPropertyChanged("Id");
            }
        }


        public string HomePhone
        {
            get { return _homePhone; }
            set 
            {
                _homePhone = value;
                OnPropertyChanged("HomePhone");
            }
        }


        public string FirstName
        {
            get { return _firstName; }
            set 
            { 
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }


        public string CellPhone
        {
            get { return _cellPhone; }
            set 
            { 
                _cellPhone = value;
                OnPropertyChanged("CellPhone");
            }
        }
        

        public Address Address 
        {
            get { return _address; }
            set { 
                _address = value;
                OnPropertyChanged("Address");
                }
        }
        


        ///Methos
        public string LookupName
        {
            get { return string.Format("{0}, {1}", _lastName, _firstName); }
        }

        public override string ToString()
        {
            return LookupName;
        }

        public override bool Equals(object obj)
        {
            Contact other = obj as Contact;
            return other != null && other.Id == Id;
        }

    }
}
