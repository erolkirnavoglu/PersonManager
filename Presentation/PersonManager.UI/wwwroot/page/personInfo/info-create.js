var PersonInfoCreate = function () {

    var infoAdd = () => {
        var isValid = true;
        var modalForm = $("#personInfoAddModal form");
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
            if (response) {
                $('#personInfoAddModal').modal('hide');
                DataTableHelper.RefresTable('personTable');
            }
           
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