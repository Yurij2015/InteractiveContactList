using InteractiveContactList.Views.ContactList;
using InteractiveContactList.Views.Forms;
using InteractiveContactList.Views.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace InteractiveContactList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        async void SimpleLoginPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleLoginPage(), true);
        }

        async void SimpleSignUpPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SimpleSignUpPage(), true);
        }

        async void AddContactPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddContactPage(), true);
        }

        async void ContactsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsPage(), true);
        }

        async void ContactList(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactList(), true);
        }
    }
}
