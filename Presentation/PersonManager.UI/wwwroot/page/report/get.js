
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
                        return new Date(data).toLocaleString("tr-TR");
                    }
                },
            ],
            detailConfig: {
                tableId: "reportTable",
                columnCount: 7,
                detailUrlCallback: (reportId) => `/report-details/detail/${reportId}`,
                templateCallback: (data) => {
                    return `
                        <table class="table table-bordered mb-0">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Location</th>
                                    <th>Person Count</th>
                                    <th>Phone Number Count</th>
                                    <th>Created Date</th>
                                     
                                </tr>
                            </thead>
                            <tbody>
                                ${data.details.map(info => `
                                        <tr>
                                            <td>${info.id}</td>
                                            <td>${info.location}</td>
                                            <td>${info.personCount}</td>
                                             <td>${info.phoneNumberCount}</td>
                                            <td>${new Date(info.createdDate).toLocaleString("tr-TR")}</td>
                                        </tr>
                                    `).join("")
                        }
                            </tbody>
                        </table>
                        `;
                }
            }
        });

    }
    var reportAdd = (id) => {
        AjaxHelper.Request({
            url: "/reports/create",
            type: "POST",
            data: {}
        }).done(function (response) {
            if (response) {
                DataTableHelper.RefresTable('reportTable');
            }
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
