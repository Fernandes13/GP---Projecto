
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

var egg = new Egg();
egg
    .addCode("f,u,n,i,l", function () {

        showFunil();

    })
    .listen();


function showFunil() {
    var funil = document.getElementById("funil");
    var audio = new Audio('./mp3/sw.mp3');
    audio.play();

    funil.style.display = "block";

    setTimeout(function () {
        funil.style.display = "none";
        audio.pause();
        audio.currentTime = 0;
    }, 12500);
}

function toggleLogin() {
    let displayState = document.getElementById("loginForm");


    if (displayState.classList.contains("show")) {
        displayState.classList.remove("show");
        displayState.classList.toggle("hide");
    } else {
        displayState.classList.remove("hide");
        displayState.classList.toggle("show");
    }
}