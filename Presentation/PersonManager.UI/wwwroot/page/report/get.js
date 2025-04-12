
var Report = function () {

    var getTable = () => {

        DataTableHelper.Init({
            url: "/reports/list",
            tableId: "reportTable",
            columns: [
                {
                    data: null,
                    orderable: false,
                    searchable: false,
                    className: "text-center",
                    render: function (data, type, row) {
                        return `<button class="btn btn-sm btn-info btn-detail" data-id="${row.id}">
                            <i class="fa fa-search"></i>
                        </button>`;
                    }
                },
                { data: "id" },
                { data: "reportStatus" },
                {
                    data: "createdDate",
                    render: function (data) {
                        if (!data) return "";
                        return new Date(data).toLocaleDateString("tr-TR");
                    }
                },
            ]
        });

    }
    var reportAdd = (id) => {
        AjaxHelper.Request({
            url: "/reports/create",
            type: "POST"
        }).done(function (response) {
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    return {
        Init: function () {
            getTable();

        },
        ReportAdd: reportAdd,
    }
}();

$(function () {
    Report.Init();
});
