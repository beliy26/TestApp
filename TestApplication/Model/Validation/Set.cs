using System.Text.RegularExpressions;

namespace TestApplication.Model
{
    public partial class Set
    {
        protected override string ValidationRules(string columnName)
        {
            switch (columnName)
            {
                case "Contract":
                    if (string.IsNullOrWhiteSpace(Contract))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }
                    break;

                case "Description":
                    if (string.IsNullOrWhiteSpace(Description))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }
                    break;

                case "Discipline":
                    if (string.IsNullOrWhiteSpace(Discipline))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }
                    break;
                case "Name":
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }
                    break;
                case "Owner":
                    if (string.IsNullOrWhiteSpace(Owner))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }
                    break;
                case "RevisionNumber":
                    if (string.IsNullOrWhiteSpace(RevisionNumber))
                    {
                        return ResourceErrorMessage.EmptyStringError;
                    }

                    var regex = new Regex(@"[A-C][0-9]{2}$");
                    var match = regex.Match(RevisionNumber);
                    if (!match.Success)
                    {
                        return ResourceErrorMessage.RevisionFormatError;
                    }
                    break;



            }
            return null;
        }
    }
}
