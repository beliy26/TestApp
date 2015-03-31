using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace TestApplication.Model
{
    public abstract class ValidatableModel : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get { return ValidationRules(columnName); }
        }

        public string Error
        {
            get { return null; }
        }

        protected virtual string ValidationRules(string columnName)
        {
            return null;
        }

        public bool IsValid()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).All(propertyInfo => ValidationRules(propertyInfo.Name) == null);
        }
    }
}
