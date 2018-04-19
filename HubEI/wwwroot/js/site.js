// Write your JavaScript code.
$(".nav-item").on("click", function () {
    $("nav").find(".active").removeClass("active");
    $(this).addClass("active");
});