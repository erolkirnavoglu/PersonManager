var PersonCreate = function () {

    var personAdd = () => {

        var formData = {
            name: $("input[name='name']").val(),
            surname: $("input[name='surname']").val(),
            company: $("input[name='company']").val()
        };
        
        AjaxHelper.Request({
            url: "/persons/create",
            type: "POST",
            data: formData,
        }).done(function (response) {
            $('#personAddModal').modal('hide');
            DataTableHelper.RefresTable('personTable');
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    return {
        Init: function () {
        },
        PersonAdd: personAdd
    }
}();

$(function () {
    PersonCreate.Init();
});