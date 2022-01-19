$(document).ready(function () {
    $(document).on("click", ".open-books-modal", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");

        fetch(url)
            .then(response => response.text())
            .then(data => {
                console.log(data)
           
              $("#quickModal .modal-dialog").html(data)


              $("#quickModal").modal(true);
            });

    })
    $(document).on("click", ".add-to-basket", function (e) {
        e.preventDefault();

        let url = $(this).attr("href");

        fetch(url)
            .then(function (response) {
                if (response.ok) {
                    alert("Added to the Basket")
                }
                else {
                    alert("ERROR!")
                }
                return response.text();
            }).then(data => {
                $(".cart-block").html(data)
            });


    })
    //$(document).on("click", "#categories", function (e) {
    //    e.preventDefault();

    //    let url = "https://localhost:44355/Book/" + `GetBookbyGenre?id=${$(this).attr("data-genre-id")}`

    //    fetch(url)
    //        .then(response => response.text())
    //        .then(data => {
    //            $("#shop-category").html(data)

    //        })

    //})
})