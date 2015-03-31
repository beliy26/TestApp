namespace TestApplication.Model
{
    public partial class FileInfo
    {
        protected override string ValidationRules(string columnName)
        {
            switch (columnName)
            {
                case "Name":
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        return "Необходимо выбрать файл";
                    }
                    break;
            }
            return null;
        }
    }
}
