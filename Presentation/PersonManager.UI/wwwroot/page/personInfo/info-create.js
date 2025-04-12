var PersonInfoCreate = function () {

    var infoAdd = () => {
        var fromData = {
            personId: $("input[name='personId']").val(),
            content: $("input[name='content']").val(),
            contactType: $("select[name='contactType']").val(),
        }
        AjaxHelper.Request({
            url: "/person-infos/create",
            type: "POST",
            data: fromData
        }).done(function (response) {
            $('#personInfoAddModal').modal('hide');
            DataTableHelper.RefresTable('personTable');
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    return {
        Init: function () {
        },
        InfoAdd: infoAdd
    }
}();

$(function () {
    PersonInfoCreate.Init();
});