document.querySelectorAll(".issue").forEach(function (ele) {
    ele.addEventListener("click", function (event) {
        event.preventDefault();
        let bookId = this.id;
        window.location.href = "/Member/IssueBook?Id=" + bookId;
    });
});

document.querySelectorAll(".reserve").forEach(function (ele) {
    ele.addEventListener("click", function (event) {
        event.preventDefault();
        var bookId = parseInt(this.dataset.id);
        console.log("this is book id", bookId);

        fetch(`/Member/ReserveBook/${bookId}`)
            .then(response => response.json())
            .then(result => {
                if (result.success) {
                    alert("Your book is reserved");
                }
            });



    });
});

