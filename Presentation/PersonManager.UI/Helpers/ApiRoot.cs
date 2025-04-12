namespace PersonManager.UI.Helpers
{
    public static class ApiRoot
    {
        public const string Root = "https://localhost:7013/";
        public const string GetPersonList = Root + "persons/list";
        public const string PostPerson = Root + "persons/create";
        public const string DeletePerson = Root + "persons/delete";
        public const string ByIdPerson = Root + "persons";
        public const string EditPerson = Root + "persons/edit";

        public const string PostPersonInfo = Root + "person-infos/create";
        public const string GetPersonIdInfo = Root + "person-infos/personId";

        public const string GetReportList = Root + "reports/list";
    }
}
