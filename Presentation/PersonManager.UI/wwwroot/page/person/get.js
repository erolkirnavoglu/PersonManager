
var Person = function () {

    var getTable = () => {

        DataTableHelper.Init({
            url: "/persons/list",
            tableId: "personTable",
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
                { data: "name" },
                { data: "surname" },
                { data: "company" },
                {
                    data: "createdDate",
                    render: function (data) {
                        if (!data) return "";
                        return new Date(data).toLocaleString("tr-TR");
                    }
                },
                {
                    data: null,
                    orderable: false,
                    searchable: false,
                    className: "text-center",
                    render: function (data, type, row) {
                        return `
                    <button class="btn btn-sm btn-primary me-1" onclick="Person.PersonView('${row.id}')">
                        <i class="fa fa-edit"></i>
                    </button>
                    <button class="btn btn-sm btn-danger" onclick="Person.PersonDelete('${row.id}')">
                        <i class="fa fa-trash"></i>
                    </button>
                    <button class="btn btn-sm btn-danger" onclick="Person.PersonInfoAdd('${row.id}')">
                         <i class="fa fa-user me-1"></i>
                    </button>
                `;
                    }
                }
            ],
            detailConfig: {
                tableId: "personTable",
                columnCount: 7,
                detailUrlCallback: (personId) => `/person-infos/personId/${personId}`,
                templateCallback: (data) => {
                    return `
                        <table class="table table-bordered mb-0">
                            <thead>
                                <tr>
                                    <th>Id</th>
                                    <th>Contact Type</th>
                                    <th>Content</th>
                                    <th>Created Date</th>
                                     <th>Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                ${data.personInfos.map(info => `
                                        <tr>
                                            <td>${info.id}</td>
                                            <td>${info.contactType}</td>
                                            <td>${info.content}</td>
                                            <td>${new Date(info.createdDate).toLocaleString("tr-TR")}</td>
                                            <th>
                                                <button class="btn btn-sm btn-danger" onclick="Person.PersonInfoDelete('${info.id}')">
                                                    <i class="fa fa-trash"></i>
                                                 </button>
                                            </th>
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

    var personView = (id) => {
        AjaxHelper.Request({
            url: `/persons/${id}`,
            type: "GET",
        }).done(function (response) {
            $("#commonModal").empty();
            $("#commonModal").html(response);
            $("#personEditModal").modal("show");
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });
    }
    var personDelete = (id) => {
        if (confirm("Are you sure you want to Delete Person?")) {
            AjaxHelper.Request({
                url: `/persons/delete/${id}`,
                type: "DELETE"
            }).done(function (response) {
                if (response) {
                    DataTableHelper.RefresTable('personTable');
                } else {
                    alert("There was a problem while deleting");
                }

            }).fail(function (xhr, status, error) {
                console.error("Silme hatası:", error);
            });
        }
    }
    var personAdd = (id) => {
        AjaxHelper.Request({
            url: "/persons/create",
            type: "GET"
        }).done(function (response) {
            $("#commonModal").empty();
            $("#commonModal").html(response);
            $("#personAddModal").modal("show");
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    var personInfoAdd = (id) => {
        var url = `/person-infos/create?personId=${id}`;
        AjaxHelper.Request({
            url: url,
            type: "GET",
        }).done(function (response) {
            $("#commonModal").empty();
            $("#commonModal").html(response);
            $("#personInfoAddModal").modal("show");
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    return {
        Init: function () {
            getTable();

        },
        PersonAdd: personAdd,
        PersonDelete: personDelete,
        PersonView: personView,
        PersonInfoAdd: personInfoAdd
    }
}();

$(function () {
    Person.Init();
});
