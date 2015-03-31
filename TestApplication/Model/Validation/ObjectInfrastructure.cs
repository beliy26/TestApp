namespace TestApplication.Model
{
    public partial class ObjectInfrastructure
    {
        protected override string ValidationRules(string columnName)
        {
            switch (columnName)
            {
                case "Name":
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }
                    break;
            }
            return null;
        }

    }
}
