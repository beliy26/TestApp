using System.Collections.ObjectModel;

namespace TestApplication.Model
{
    public partial class ObjectInfrastructure:ValidatableModel
    {
        #region Properties
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<Set> Sets { get; set; }

        public ObjectInfrastructure()
        {
            Sets=new ObservableCollection<Set>();
        }
        #endregion

    }
}
