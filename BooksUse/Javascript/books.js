$(document).ready(function () {
    $(document).on('click', '.submitBooksButton', function (event) {
        event.preventDefault()
        let error = false

        $("#title").removeClass("is-invalid")
        $("#isbn").removeClass("is-invalid")
        $("#author").removeClass("is-invalid")
        $("#stock").removeClass("is-invalid")
        $("#price").removeClass("is-invalid")


        if ($("#title").val() == "") {
            $("#title").addClass("is-invalid")
            error = true
        }
        if ($("#isbn").val() == "") {
            $("#isbn").addClass("is-invalid")
            error = true
        }
        if ($("#author").val() == "") {
            $("#author").addClass("is-invalid")
            error = true
        }
        if ($("#stock").val() == "") {
            $("#stock").addClass("is-invalid")
            error = true
        }
        if ($("#price").val() == "") {
            $("#price").addClass("is-invalid")
            error = true
        }
        if (!error) {
            document.getElementById("form").submit();
        }
    })
})