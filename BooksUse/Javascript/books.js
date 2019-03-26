document.addEventListener('DOMContentLoaded', function () {
    $('.check').on('input', function () {
        value = $(this).val()
        id = $(this).attr('id')
        

        //api/BooksAPI/Exist/field/value
        if (value.length > 0) {
            $.get(`/api/BooksAPI/Exist/${id}/${value}`, function (data) {
                if (data == true) {
                    document.getElementById(id).classList.add("is-invalid")
                    $('.submitBooksButton').prop('disabled', true)
                }
                else if (data == false) {
                    document.getElementById(id).classList.remove("is-invalid")
                    $('.submitBooksButton').prop('disabled', false)
                }
                else {
                    console.log(data)
                }
            })
        }        
    })
})