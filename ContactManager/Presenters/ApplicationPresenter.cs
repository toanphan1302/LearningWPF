using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ContactManager.Model;

namespace ContactManager.Presenters
{
    public class ApplicationPresenter : PresenterBase<Shell> 
    {
        private readonly ContactRepository _contactRepository;
        private ObservableCollection<Contact> _currentContacts;
        private string _statusText;

        public ApplicationPresenter(
            Shell view,
            ContactRepository contactRepository)
            : base(view)
        {
            _contactRepository = contactRepository;

            _currentContacts = new ObservableCollection<Contact>(
                _contactRepository.FindAll()
                );
        }

        public string StatusText
        {
            get { return _statusText; }
            set 
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }


        public ObservableCollection<Contact> CurrentContacts
        {
            get { return _currentContacts; }
            set 
            {
                _currentContacts = value;
                OnPropertyChanged("CurrentContacts");
            }
        }

        public void Search(string criteria)
        {
            if (!string.IsNullOrEmpty(criteria) && criteria.Length > 2)
            {
                CurrentContacts = new ObservableCollection<Contact>(
                    _contactRepository.FindByLookup(criteria)
                    );

                StatusText = string.Format(
                    "{0} contatts found",
                    CurrentContacts.Count
                    );
            }
            else
            {
                CurrentContacts = new ObservableCollection<Contact>(
                    _contactRepository.FindAll()
                    );

                StatusText = "Displaying all contacts.";
            }
        }

        public void NewContact()
        {
            OpenContact(new Contact());
        }

        public void SaveContact(Contact contact)
        {
            if (!CurrentContacts.Contains(contact))
                CurrentContacts.Add(contact);

            _contactRepository.Save(contact);

            StatusText = string.Format(
                "Contact '{0}' was saved.",
                contact.LookupName
                );
        }

        public void DeleteContact(Contact contact)
        {
            if (CurrentContacts.Contains(contact))
                CurrentContacts.Remove(contact);

            _contactRepository.Delete(contact);

            StatusText = string.Format(
                "Contact '{0}' was deleted.",
                contact.LookupName
                );
        }

        public void CloseTab<T>(PresenterBase<T> presenter)
        {
            View.RemoveTab(presenter);
        }
        private void OpenContact(Contact contact)
        {
            throw new NotImplementedException();
        }

        private void DisplayAllContacts()
        {
            throw new NotImplementedException();
        }
    }
}
