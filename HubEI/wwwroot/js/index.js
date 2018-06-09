$(document).ready(function () {
    $('#rpgdModal').modal('show');


    $("#rpgdModal").on("hidden.bs.modal", function () {
        window.location.href = 'Home/SaveRPGCookie';
    });
});

