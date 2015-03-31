using System;
using System.Collections.ObjectModel;

namespace TestApplication.Model
{
    public partial class Document : ValidatableModel
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

        private string _documentType;

        public string DocumentType
        {
            get { return _documentType; }
            set
            {
                _documentType = value;
                OnPropertyChanged("DocumentType");
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

        private string _numberOfPage;

        public string NumberOfPage
        {
            get { return _numberOfPage; }
            set
            {
                _numberOfPage = value;
                OnPropertyChanged("NumberOfPage");
            }
        }

        public ObservableCollection<FileInfo> FileInfos { get; set; }

        #endregion

        public Document()
        {
            FileInfos=new ObservableCollection<FileInfo>();
            ReleaseDateRevisiony = DateTime.Now;
        }
    }
}
