var PersonUpdate = function () {

    var personEdit = () => {
        var isValid = true;
        var modalForm = $("#personEditModal form");
        modalForm.find(".text-danger").remove();
        modalForm.find("input").removeClass("is-invalid");
        modalForm.find("input.form-control").each(function () {
            var input = $(this);
            var value = input.val().trim();

            if (!value) {
                isValid = false;
                input.addClass("is-invalid");
                input.after(`<span class="text-danger">Bu alan boş olamaz.</span>`);
            }
        });

        if (!isValid) return;
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
            if (response) {
                $('#personEditModal').modal('hide');
                DataTableHelper.RefresTable('personTable');
            }
           
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