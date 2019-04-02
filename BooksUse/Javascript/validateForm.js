document.addEventListener('DOMContentLoaded', function () {
    $("#form").on("change paste keyup click", function () {
        if ($(this).valid()) {
            $('#submitButton').prop('disabled', false)
        }
        else {
            $('#submitButton').prop('disabled', true)
        }
    });
})