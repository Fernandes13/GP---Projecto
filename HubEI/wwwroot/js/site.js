
//// Write your JavaScript code.
//$(".nav-item").on("click", function () {
//    $("nav").find(".active").removeClass("active");
//    $(this).addClass("active");
//});


var egg = new Egg();
egg
    .addCode("f,u,n,i,l", function () {

        showFunil();

    })
    .listen();


function showFunil() {
    var funil = document.getElementById("funil");
    var audio = new Audio('../mp3/sw.mp3');
    audio.play();
    audio.volume = 0.4;

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