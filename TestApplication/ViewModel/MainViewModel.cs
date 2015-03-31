using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Win32;
using TestApplication.Helper;
using TestApplication.Model;
using FileInfo = TestApplication.Model.FileInfo;

namespace TestApplication.ViewModel
{
    class MainViewModel : ViewModelBase
    {

        public ObservableCollection<ObjectInfrastructure> Infrastructures { get; set; }

        public List<object> Objects { get { return new List<object>() { SelectedObject }; } }

        private object _selectedObject;

        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                OnPropertyChanged("SelectedObject");
                OnPropertyChanged("Objects");
                ChangeStatus();
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        private bool _sameNameValidationError;
       

        public bool SameNameValidationError
        {
            get { return _sameNameValidationError; }
            set
            {
                _sameNameValidationError = value;
                OnPropertyChanged("SameNameValidationError");
            }
        }
        

        private void ChangeStatus()
        {
            if (SelectedObject == null) return;
            var type = SelectedObject.GetType().Name;
            switch (type)
            {
                case "ObjectInfrastructure":
                    var objectInfrastructure = SelectedObject as ObjectInfrastructure;
                    if (objectInfrastructure != null) Status = "Всего комплектов: " + objectInfrastructure.Sets.Count;
                    break;
                case "Set":
                    var set = SelectedObject as Set;
                    if (set != null) Status = "Всего документов: " + set.Documents.Count;
                    break;
                case "Document":
                    var document = SelectedObject as Document;
                    if (document != null) Status = "Всего файлов: " + document.FileInfos.Count;
                    break;
                default:
                    Status = string.Empty;
                    break;
            }
        }


        public RelayCommand<object> SelectionchangeRelayCommand { get; private set; }
        public RelayCommand ImportXmlRelayCommand { get; private set; }
        public RelayCommand ExportXmlRelayCommand { get; private set; }


        /// <summary>
        /// костыль, так как  SelectedItem в TreeView только для чтения
        /// </summary>
        /// <param name="param"></param>
        private void Selection(object param)
        {
            SelectedObject = param;
        }

        private void ExportXml()
        {
            var saveFileDialog = new SaveFileDialog() { FileName = "SPF4ImportData.xml" };
            if (saveFileDialog.ShowDialog() != true) return;
            var directory = Directory.GetParent(saveFileDialog.FileName);

            XDocument doc = new XDocument();
            XElement rootElement = new XElement("SPF4ImportData");
            rootElement.Add(new XAttribute("Configuration", "Unknown"));
            rootElement.Add(new XAttribute("FileVersion", "0.4.1-simplified"));
            doc.Add(rootElement);

            foreach (var infrastructure in Infrastructures)
            {
                var countSet = 0;
                foreach (var set in infrastructure.Sets.ToList())
                {
                    var setDiretory = set.Name.Translit();
                    Directory.CreateDirectory(directory + "\\" + setDiretory);
                    XElement xElementinfrastructure = new XElement("ProjDocBinderRevision");
                    xElementinfrastructure.Add(new XAttribute("RNUObj", infrastructure.Name));
                    xElementinfrastructure.Add(new XAttribute("Name", set.Name));
                    xElementinfrastructure.Add(new XAttribute("Description", set.Description));
                    xElementinfrastructure.Add(new XAttribute("RNContract", set.Contract));
                    xElementinfrastructure.Add(new XAttribute("RNProjectDiscipline", set.Discipline));
                    xElementinfrastructure.Add(new XAttribute("RevisionNumber", set.RevisionNumber));
                    xElementinfrastructure.Add(new XAttribute("SPFRevIssueDate", set.ReleaseDateRevisiony.ToShortDateString()));
                    xElementinfrastructure.Add(new XAttribute("Owner", set.Owner));
                    rootElement.Add(xElementinfrastructure);
                    foreach (var docment in set.Documents.ToList())
                    {
                        countSet++;
                        var documentFolderName = docment.DocumentType.Translit() + "-" + countSet.ToString().PadLeft(3, '0');
                        var documentDirectory = setDiretory + "\\" + documentFolderName + "\\" + docment.RevisionNumber;
                        Directory.CreateDirectory(directory + "\\" + documentDirectory);
                        XElement xElementDocument = new XElement("ProjDocRevision");
                        xElementDocument.Add(new XAttribute("Name", docment.Name));
                        xElementDocument.Add(new XAttribute("Description", docment.Description));
                        xElementDocument.Add(new XAttribute("RNDocType", docment.DocumentType));
                        xElementDocument.Add(new XAttribute("RevisionNumber", docment.RevisionNumber));
                        xElementDocument.Add(new XAttribute("SPFRevIssueDate", docment.ReleaseDateRevisiony.ToShortDateString()));
                        xElementDocument.Add(new XAttribute("Owner", docment.Owner));
                        xElementDocument.Add(new XAttribute("RNAllSheetsSize", docment.NumberOfPage));
                        xElementinfrastructure.Add(xElementDocument);
                        foreach (var file in docment.FileInfos.ToList())
                        {
                            var destFileName = directory + "\\" + documentDirectory + "\\" + file.Name;
                            File.Copy(file.FullName, destFileName);
                            XElement xElementFile = new XElement("File");
                            xElementFile.Add(new XAttribute("FileName", documentDirectory + "\\" + file.Name));
                            xElementDocument.Add(xElementFile);
                        }
                    }
                }

            }
            doc.Save(saveFileDialog.FileName);
        }

        private void ImportXml()
        {
            var openFileDialog = new OpenFileDialog() { Multiselect = false };
            if (openFileDialog.ShowDialog() == false) return;
            Infrastructures.Clear();
            var doc = XDocument.Load(openFileDialog.FileName);
            foreach (var el in doc.Root.Elements())
            {
                var objectInfrastructure = new ObjectInfrastructure() { Name = el.Attribute("RNUObj").Value };
                var obj = Infrastructures.FirstOrDefault(inf => inf.Name.Equals(objectInfrastructure.Name));
                if (obj == null)
                {
                    obj = objectInfrastructure;
                    Infrastructures.Add(obj);
                }
                var set = new Set()
                {
                    Contract = el.Attribute("RNContract").Value,
                    Description = el.Attribute("Description").Value,
                    Discipline = el.Attribute("RNProjectDiscipline").Value,
                    Name = el.Attribute("Name").Value,
                    Owner = el.Attribute("Owner").Value,
                    ReleaseDateRevisiony = Convert.ToDateTime(el.Attribute("SPFRevIssueDate").Value),
                    RevisionNumber = el.Attribute("RevisionNumber").Value
                };
                obj.Sets.Add(set);
                foreach (var el2 in el.Elements())
                {
                    var documnet = new Document()
                    {
                        Description = el2.Attribute("Description").Value,
                        DocumentType = el2.Attribute("RNDocType").Value,
                        Name = el2.Attribute("Name").Value,
                        NumberOfPage = el2.Attribute("RNAllSheetsSize").Value,
                        Owner = el2.Attribute("Owner").Value,
                        ReleaseDateRevisiony = Convert.ToDateTime(el2.Attribute("SPFRevIssueDate").Value),
                        RevisionNumber = el2.Attribute("RevisionNumber").Value
                    };
                    set.Documents.Add(documnet);
                    foreach (var el3 in el2.Elements())
                    {
                        var name = el3.Attribute("FileName").Value.Split('\\').Last();
                        var fullname = Directory.GetParent(openFileDialog.FileName) + "\\" + el3.Attribute("FileName").Value;
                        var fileInfo = new FileInfo()
                        {
                            Name = name,
                            FullName = fullname
                        };
                        documnet.FileInfos.Add(fileInfo);
                    }
                }

            }


        }

        /// <summary>
        /// обработчик события добавления элемента из формы добавления
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void ObjectSenderOnOnReceive(object sender, EventArgs eventArgs)
        {
            var container = eventArgs as ObjectContainer;
            if (container == null) return;
            var objectInfrastructure = container.ObjectInfrastructure;
            var curentObjInfrastucture = Infrastructures.FirstOrDefault(i => i.Name.Equals(objectInfrastructure.Name));
            if (curentObjInfrastucture == null)
            {
                curentObjInfrastucture = objectInfrastructure;
                Infrastructures.Add(curentObjInfrastucture);
            }

            var set = container.Set;
            var curentSet = curentObjInfrastucture.Sets.FirstOrDefault(s => s.Name.Equals(set.Name));
            if (curentSet == null)
            {
                curentSet = set;
                curentObjInfrastucture.Sets.Add(curentSet);
            }
            var document = container.Document;
            var file = container.FileInfo;
            var curentDocument = curentSet.Documents.FirstOrDefault(d => d.Name.Equals(document.Name));
            if (curentDocument == null)
            {
                curentDocument = document;
                curentSet.Documents.Add(document);
            }

            curentDocument.FileInfos.Add(file);
        }

        public MainViewModel()
        {
            Infrastructures = new ObservableCollection<ObjectInfrastructure>();
            Infrastructures.CollectionChanged += Infrastructures_CollectionChanged;
            ObjectSender.OnReceive += ObjectSenderOnOnReceive;
            SelectionchangeRelayCommand = new RelayCommand<object>(Selection);
            ImportXmlRelayCommand = new RelayCommand(ImportXml);
            ExportXmlRelayCommand = new RelayCommand(ExportXml, ()=>Infrastructures.Any() && !SameNameValidationError);

        }

        /// <summary>
        /// при изменении коллекции подписываемся на событие изменения свойств ObjectInfrastructure
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Infrastructures_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var objectInfrastructure in Infrastructures)
            {
                objectInfrastructure.PropertyChanged += objectInfrastructure_PropertyChanged;
            }
        }

        /// <summary>
        /// при изменеии имени любого из объетов проверям не содержится в коллекции объект с таким именем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void objectInfrastructure_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            
            if (e.PropertyName != "Name") return;
            var infr = sender as ObjectInfrastructure;
            if (infr==null) return;
            var list = Infrastructures.ToList();
            list.Remove(infr);
            SameNameValidationError = list.Any(inf => inf.Name == infr.Name);
        }
    }
}
