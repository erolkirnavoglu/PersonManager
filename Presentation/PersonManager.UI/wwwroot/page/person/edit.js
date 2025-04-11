var PersonUpdate = function () {

    var personEdit = () => {

        var formData = {
            id: $("input[name='id']").val(),
            name: $("input[name='name']").val(),
            surname: $("input[name='surname']").val(),
            company: $("input[name='company']").val()
        };
        
        AjaxHelper.Request({
            url: "/persons/edit",
            type: "PUT",
            data: formData,
        }).done(function (response) {
            $('#personEditModal').modal('hide');
            DataTableHelper.RefresTable('personTable');
        }).fail(function (xhr, status, error) {
            console.error("Hata:", error);
        });

    }

    return {
        Init: function () {
        },
        PersonEdit: personEdit
    }
}();

$(function () {
    PersonUpdate.Init();
});