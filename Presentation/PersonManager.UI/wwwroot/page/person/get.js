﻿
var Person = function () {

    var getTable = () => {

        DataTableHelper.Init({
            url: "/persons/list",
            tableId: "personTable",
            columns: [
                { data: "id" },
                { data: "name" },
                { data: "surname" },
                { data: "company" },
                {
                    data: null,
                    orderable: false,
                    searchable: false,
                    className: "text-center",
                    render: function (data, type, row) {
                        return `
                    <button class="btn btn-sm btn-primary me-1" onclick="personEdit('${row.id}')">
                        <i class="fa fa-edit"></i>
                    </button>
                    <button class="btn btn-sm btn-danger" onclick="Person.PersonDelete('${row.id}')">
                        <i class="fa fa-trash"></i>
                    </button>
                `;
                    }
                }
            ]
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
    var personAdd = () => {
        AjaxHelper.Request({
            url: "/persons/create",
            type: "GET",
        }).done(function (response) {
            $("#commonModal").empty();
            $("#commonModal").html(response);
            $("#personAddModal").modal("show");
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    return {
        Init: function () {
             getTable();

        },
        PersonAdd: personAdd,
        PersonDelete: personDelete
    }
}();

$(function () {
    Person.Init();
});
