using System.ComponentModel;
using Microsoft.Win32;
using TestApplication.Helper;
using TestApplication.Model;

namespace TestApplication.ViewModel
{
    class AddViewModel : ViewModelBase
    {


        private ObjectInfrastructure _objectInfrastructure;

        public ObjectInfrastructure ObjectInfrastructure
        {
            get { return _objectInfrastructure; }
            set
            {
                _objectInfrastructure = value;
                OnPropertyChanged("ObjectInfrastructure");
            }
        }

        private Set _set;

        public Set Set
        {
            get { return _set; }
            set
            {
                _set = value;
                OnPropertyChanged("Set");
            }
        }

        private Document _document;

        public Document Document
        {
            get { return _document; }
            set
            {
                _document = value;
                OnPropertyChanged("Document");
            }
        }

        private FileInfo _fileInfo;

        public FileInfo FileInfo
        {
            get { return _fileInfo; }
            set
            {
                _fileInfo = value;
                OnPropertyChanged("FileInfo");
            }
        }

        private bool _validateRevisonNumber;

        public bool ValidateRevisionNumber
        {
            get { return _validateRevisonNumber; }
            set
            {
                _validateRevisonNumber = value;
                OnPropertyChanged("ValidateRevisionNumber");
            }
        }



        public RelayCommand SubmitRelayCommand { get; private set; }
        public RelayCommand LoadRelayCommand { get; private set; }
        public RelayCommand SelectFileRelayCommand { get; private set; }

        private void Load()
        {
            ObjectInfrastructure = new ObjectInfrastructure();
            Set = new Set();
            Document = new Document();
            FileInfo = new FileInfo();
            Document.PropertyChanged += DocumentOnPropertyChanged;
            Set.PropertyChanged += DocumentOnPropertyChanged;
        }

        private void SelectFile()
        {
            var openFileDialog = new OpenFileDialog() { Multiselect = false };
            if (openFileDialog.ShowDialog() != true) return;
            FileInfo.FullName = openFileDialog.FileName;
            FileInfo.Name = openFileDialog.SafeFileName;
        }

        private bool IsValid()
        {
            var validObjectInfrastructure = ObjectInfrastructure != null && ObjectInfrastructure.IsValid();
            var validSet = Set != null && Set.IsValid();
            var validDocument = Document != null && Document.IsValid();
            var validFile = FileInfo != null && FileInfo.IsValid();
            return validObjectInfrastructure && validSet && validDocument && validFile && !ValidateRevisionNumber;
        }

        private bool ValidateRevision()
        {
            int setRevision, documentRevision;
            if (!int.TryParse(Set.RevisionNumber.Substring(1), out setRevision) ||
                !int.TryParse(Document.RevisionNumber.Substring(1), out documentRevision)) return false;
            return !(setRevision >= documentRevision);
        }

        private void DocumentOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != "RevisionNumber") return;
            ValidateRevisionNumber = ValidateRevision();

        }

        public AddViewModel()
        {

            LoadRelayCommand = new RelayCommand(Load);

            SubmitRelayCommand = new RelayCommand(() =>
            {
                ObjectSender.Send(this, new ObjectContainer()
                {
                    Set = Set,
                    Document = Document,
                    FileInfo = FileInfo,
                    ObjectInfrastructure = ObjectInfrastructure
                });
            }, IsValid);

            SelectFileRelayCommand = new RelayCommand(SelectFile);



        }

    }
}
