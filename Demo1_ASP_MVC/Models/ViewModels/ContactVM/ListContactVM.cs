

using Demo1_ASP_MVC.Models.ContactModels;

namespace Demo1_ASP_MVC.Models.ViewModels.ContactVM

{
    public class ListContactVM
    {

        public ListContactVM(List<Contact> contacts)
        {
            _contacts = contacts;
            
        }

        private List<Contact> _contacts;

        public List<Contact> Contacts { get => _contacts; }

        public int NbContact { get => _contacts.Count(); }

        public void AddContat(Contact newContact)
        {
            if (newContact == null) throw new ArgumentNullException(nameof(newContact));
            if (_contacts == null) _contacts = new List<Contact>();
            if (!_contacts.Contains(newContact)) _contacts.Add(newContact);

        }

       
    }
}
