namespace PersonManager.UI.Helpers
{
    public  class ApiRoot
    {
        public string Root { get; set; }
        public string GetPersonList => $"{Root}persons/list";
        public string PostPerson => $"{Root}persons/create";
        public string DeletePerson => $"{Root}persons/delete";
        public string ByIdPerson => $"{Root}persons";
        public string EditPerson => $"{Root}persons/edit";

        public string PostPersonInfo => $"{Root}person-infos/create";
        public string GetPersonIdInfo => $"{Root}person-infos/personId";
        public string DeletePersonInfo => $"{Root}person-infos/delete";

        public string GetReportList => $"{Root}reports/list";
        public string PostReport => $"{Root}reports/create";

        public string GetDetailList => $"{Root}report-details/detail";


        /*
        public const string Root = "https://localhost:7013/";
        // Person
        public const string GetPersonList = Root + "persons/list";
        public const string PostPerson = Root + "persons/create";
        public const string DeletePerson = Root + "persons/delete";
        public const string ByIdPerson = Root + "persons";
        public const string EditPerson = Root + "persons/edit";

        // Person Info
        public const string PostPersonInfo = Root + "person-infos/create";
        public const string GetPersonIdInfo = Root + "person-infos/personId";
        public const string DeletePersonInfo = Root + "person-infos/delete";

        // Report
        public const string GetReportList = Root + "reports/list";
        public const string PostReport = Root + "reports/create";

        // Report Detail
        public const string GetDetailList = Root + "report-details/detail";
        */
    }
}
