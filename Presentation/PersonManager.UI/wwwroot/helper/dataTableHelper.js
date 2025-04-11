var DataTableHelper = (function () {

    function Init({ url, tableId, columns, method = "GET", data = null }) {
        if (!url || !tableId || !columns) {
            return;
        }

        $(`#${tableId}`).DataTable({
            destroy: true,
            processing: true,
            serverSide: false,
            autoWidth: true,
            ajax: {
                url: url,
                type: method,
                contentType: "application/json",
                data: function (d) {
                    return data ? JSON.stringify(data) : null;
                },
                dataSrc: "data"
            },
            columns: columns
        });
    }

    var refresTable = (id) => {
        $(`#${id}`).DataTable().ajax.reload();
    }

    return {
        Init: Init,
        RefresTable: refresTable
    };

})();