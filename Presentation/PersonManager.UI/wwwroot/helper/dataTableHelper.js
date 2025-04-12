var DataTableHelper = (function () {

    function Init({ url, tableId, columns, method = "GET", data = null, detailConfig = null }) {
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
            columns: columns,
            createdRow: function (row, rowData, dataIndex) {
            }
        });

        if (detailConfig) {
            attachDetailToggle(detailConfig);
        }
    }

    function attachDetailToggle({ tableId, detailUrlCallback, columnCount, templateCallback }) {
        
        $(`#${tableId}`).on('click', '.btn-detail', function () {
            let row = $(this).closest('tr');
            let id = $(this).data("id");

            if (row.next('tr').hasClass('details-row')) {
                row.next('tr').remove();
                return;
            }

            $.ajax({
                url: detailUrlCallback(id),
                method: "GET",
                success: function (data) {
                    let detailsHtml = templateCallback(data);
                    let detailsRow = `<tr class="details-row">
                        <td colspan="${columnCount}">
                            ${detailsHtml}
                        </td>
                    </tr>`;
                    row.after(detailsRow);
                }
            });
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
