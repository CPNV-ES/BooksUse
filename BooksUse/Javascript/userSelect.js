document.addEventListener('DOMContentLoaded', function () {

    function setBooksSelect() {
        $("#booksSelect").find('option').remove().end()
        $.get('/api/BooksAPI/GetAvaibleBooks/' + $("#userSelect").val(), function (data) {
            var avaibleBooks = ""
            for (var i = 0; i < data.length; i++) {
                avaibleBooks += "<option value='" + data[i].id + "'>" + data[i].title + "</option>";
            }
            $('#booksSelect').append(avaibleBooks);
            $('#submitButton').prop('disabled', false)
            if (data.length == 0) {
                $('#submitButton').prop('disabled', true)
            }
        })
    }

    setBooksSelect()

    $("#userSelect").change(function (event) {
        setBooksSelect()
    })
})