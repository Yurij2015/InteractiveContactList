using InteractiveContactList.ViewModels.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace InteractiveContactList.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Vk { get; set; }
        public string LinkedIn { get; set; }
        public string Twitter { get; set; }
        /// <summary>
        /// Get data from socengines
        /// </summary>
        public string FacebookData { get; set; }
        public string InstagramData { get; set; }
        public string VkData { get; set; }
        public string LinkedInData { get; set; }
        public string TwitterData { get; set; }


        public override bool Equals(object obj)
        {
            Contact contact = obj as Contact;
            return this.Id == contact.Id;
        }

    }
}
