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


function search_suggestion(search) {
    setTimeout(() => {
        $.ajax({
            type: "GET",
            url: '/Home/SearchSuggestions?search_by=' + search.value,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
        }).done(function (res) {
            renderSuggestions(search.value, JSON.parse(res));
        });
    }, 30);
}


function renderSuggestions(search, projects) {
    clearSuggestions();
    var suggestions = document.getElementById("search-suggestions");

    if (search.length > 0) {
        if (projects.length > 0) {
            projects.forEach(function (project) {
                var link = document.createElement("a");
                link.className += " list-group-item project-title-search";
                link.innerHTML = project.title;
                link.href = "/Project?project_id=" + project.idProject;

                var spanleft = document.createElement("span");
                spanleft.innerHTML = "<b>" + project.idStudentNavigation.name + "</b>";
                spanleft.className += " float-lg-left";


                var spanright = document.createElement("span");
                spanright.innerHTML = project.projectDate.split("T")[0];
                spanright.className += " float-lg-right";

                var spanleft_type = document.createElement("span");
                spanleft_type.innerHTML = "" + project.idProjectTypeNavigation.description + "";
                spanleft_type.className += " float-lg-left";

                link.appendChild(document.createElement("br"));
                link.appendChild(spanleft);
                link.appendChild(spanright);
                link.appendChild(document.createElement("br"));
                link.appendChild(spanleft_type);

                suggestions.appendChild(link);
            });
        }
        else {
            var link = document.createElement("a");
            link.className += " list-group-item project-title-search";
            link.innerHTML = "<b style='color:dimgrey'>Sem correspondências para \"" + search + "\"</b>";
            link.appendChild(document.createElement("br"));
            link.href = "/Project/List?search_by=" + search;

            //var spanleft = document.createElement("span");
            //spanleft.innerHTML = "<b>Ver todos os resultados</b>";
            //spanleft.className += " float-lg-left";
            //link.appendChild(spanleft)

            suggestions.appendChild(link);
        }
    }
}

function clearSuggestions() {
    var suggestions = document.getElementById("search-suggestions");

    while (suggestions.firstChild) {
        suggestions.removeChild(suggestions.firstChild);
    }
}


function searchSuggestionOut(search) {
    setTimeout(function () {
        clearSuggestions();
    }, 100);
    
}



