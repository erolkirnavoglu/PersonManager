var PersonCreate = function () {

    var personAdd = () => {
        var isValid = true;
        var modalForm = $("#personAddModal form");
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
            name: $("input[name='name']").val().trim(),
            surname: $("input[name='surname']").val().trim(),
            company: $("input[name='company']").val().trim()
        };
        
        AjaxHelper.Request({
            url: "/persons/create",
            type: "POST",
            data: formData,
        }).done(function (response) {
            if (response) {
                $('#personAddModal').modal('hide');
                DataTableHelper.RefresTable('personTable');
            }
          
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