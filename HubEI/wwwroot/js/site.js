//// Write your JavaScript code.
//$(".nav-item").on("click", function () {
//    $("nav").find(".active").removeClass("active");
//    $(this).addClass("active");
//});

function showProjectCreate() {
    $("#addProjectModal").html("");

    //document.getElementById("fade-background").style.display = "block";
    document.getElementById("addProjectModal").style.display = "block";

    var url = "/BackOffice/Projects/CreateProject";

    $.ajax(
        {
            type: 'GET',
            dataType: 'html',
            url: url,
            beforeSend: function () {
                //$('#loading-div').show();
            },
            success: function (result) {
                setTimeout(function () {
                    $("#addProjectModal").html(result);
                    //$('.loading-div').hide();
                }, 1000)
            },
            error: function (error) {
            }
        });
}
