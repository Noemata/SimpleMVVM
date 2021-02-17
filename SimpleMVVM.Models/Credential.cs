using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace SimpleMVVM.Models
{
    public class Credential : ObservableObject
    {

        private string _name;
        private string _email;
        private string _role;

        public Credential() {}

        public Credential(string name, string email, string role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }
    }
}
