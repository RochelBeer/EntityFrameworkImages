$(function () {
    const id = $("#image-id").val();

    setInterval(() => {
        $.get("/home/getlikes", { id }, function (likes) {
            $("#likes-count").text(likes);
        })
        
    }, 1000);


    $(".btn-primary").on("click", function () {
        console.log("like!")


        $.get("/home/addLike", { id }, function () {

        })
        $(".btn-primary").prop("disabled", true)
    })


})