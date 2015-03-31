using System;
using System.Collections.ObjectModel;

namespace TestApplication.Model
{
    public partial class Set : ValidatableModel
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

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        private string _contract;

        public string Contract
        {
            get { return _contract; }
            set
            {
                _contract = value;
                OnPropertyChanged("Contract");
            }
        }

        private string _discipline;

        public string Discipline
        {
            get { return _discipline; }
            set
            {
                _discipline = value;
                OnPropertyChanged("Discipline");
            }
        }

        private string _revisionNumber;

        public string RevisionNumber
        {
            get { return _revisionNumber; }
            set
            {
                _revisionNumber = value;
                OnPropertyChanged("RevisionNumber");
            }
        }

        private DateTime _releaseDateRevision;

        public DateTime ReleaseDateRevisiony
        {
            get { return _releaseDateRevision; }
            set
            {
                _releaseDateRevision = value;
                OnPropertyChanged("ReleaseDateRevision");
            }
        }

        private string _owner;

        public string Owner
        {
            get { return _owner; }
            set
            {
                _owner = value;
                OnPropertyChanged("Owner");
            }
        }

        public ObservableCollection<Document> Documents { get; set; }

        public Set()
        {
            Documents = new ObservableCollection<Document>();
            ReleaseDateRevisiony = DateTime.Now;
        }
        #endregion
      
    }
}
