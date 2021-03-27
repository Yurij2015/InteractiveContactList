using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using InteractiveContactList.Services;
using InteractiveContactList.Models;
using InteractiveContactList.Views.Forms;

namespace InteractiveContactList.ViewModels.Forms
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        bool initialized = false;   // была ли начальная инициализация
        Contact selectedContact;  // выбранный друг
        private bool isBusy;    // идет ли загрузка с сервера

        public ObservableCollection<Contact> Contacts { get; set; }
        ContactsService ContactsService = new ContactsService();
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateContactCommand { protected set; get; }
        public ICommand DeleteContactCommand { protected set; get; }
        public ICommand SaveContactCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public ApplicationViewModel()
        {
            Contacts = new ObservableCollection<Contact>();
            IsBusy = false;
            CreateContactCommand = new Command(CreateContact);
            DeleteContactCommand = new Command(DeleteContact);
            SaveContactCommand = new Command(SaveContact);
            BackCommand = new Command(Back);
        }

        public Contact SelectedContact
        {
            get { return selectedContact; }
            set
            {
                if (selectedContact != value)
                {
                    Contact tempContact = new Contact()
                    {
                        Id = value.Id,
                        FirstName = value.FirstName,
                        LastName = value.LastName,
                        Date = value.Date,
                        Gender = value.Gender,
                        PhoneNumber = value.PhoneNumber,
                        Address = value.Address,
                        City = value.City,
                        Facebook = value.Facebook,
                        Instagram = value.Instagram,
                        Vk = value.Vk,
                        LinkedIn = value.LinkedIn,
                        Twitter = value.Twitter

                    };
                    selectedContact = null;
                    OnPropertyChanged("SelectedContact");
                    Navigation.PushAsync(new ContactPage(tempContact, this));
                }
            }
        }
        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void CreateContact()
        {
            await Navigation.PushAsync(new ContactPage(new Contact(), this));
        }
        private void Back()
        {
            Navigation.PopAsync();
        }

        public async Task GetContacts()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<Contact> contacts = await ContactsService.Get();

            // очищаем список
            //Contacts.Clear();
            while (Contacts.Any())
                Contacts.RemoveAt(Contacts.Count - 1);

            // добавляем загруженные данные
            foreach (Contact f in contacts)
                Contacts.Add(f);
            IsBusy = false;
            initialized = true;
        }
        private async void SaveContact(object ContactObject)
        {
            Contact Contact = ContactObject as Contact;
            if (Contact != null)
            {
                IsBusy = true;
                // редактирование
                if (Contact.Id > 0)
                {
                    Contact updatedContact = await ContactsService.Update(Contact);
                    // заменяем объект в списке на новый
                    if (updatedContact != null)
                    {
                        int pos = Contacts.IndexOf(updatedContact);
                        Contacts.RemoveAt(pos);
                        Contacts.Insert(pos, updatedContact);
                    }
                }
                // добавление
                else
                {
                    Contact addedContact = await ContactsService.Add(Contact);
                    if (addedContact != null)
                        Contacts.Add(addedContact);
                }
                IsBusy = false;
            }
            Back();
        }
        private async void DeleteContact(object ContactObject)
        {
            Contact Contact = ContactObject as Contact;
            if (Contact != null)
            {
                IsBusy = true;
                Contact deletedContact = await ContactsService.Delete(Contact.Id);
                if (deletedContact != null)
                {
                    Contacts.Remove(deletedContact);
                }
                IsBusy = false;
            }
            Back();
        }
    }
}