$(function () {
    $('div[onload]').trigger('onload');
});

$(document).ready(function () {
    var getUrlParameter = function getUrlParameter(sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    };

    var x = document.getElementById('marks_range');
    if (x) {
        var param = getUrlParameter('marks');
        if (param)
            x.value = param;
    }

    var technologies = [];

    $.ajax({
        type: "GET",
        url: '/BackOffice/Technologies',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false,
    }).done(function (res) {
        JSON.parse(res).forEach(function (tech) {
            technologies.push(tech.description);
        });
    });

    $('#input-technologies').tagator({
        autocomplete: technologies,
        allowAutocompleteOnly: true
    });




});



function populateTechnologyFilter() {
    var technologies = [];

    $.ajax({
        type: "GET",
        url: '/BackOffice/Technologies',
        contentType: "application/json; charset=utf-8",
        ataType: "html",
        async: false,
    }).done(function (res) {
        JSON.parse(res).forEach(function (tech) {
            technologies.push({ description: tech.description, idTechnology: tech.idTechnology });
        });
    });

    var technologyList1 = [];
    var technologyList2 = [];
    var technologyList3 = [];

    if (technologies.length >= 3) {
        for (var i = 0; i < parseInt(technologies.length / 3); i++) {
            technologyList1.push(technologies[i]);
        }

        for (var i = parseInt(technologies.length / 3); i < parseInt(technologies.length / 3 * 2); i++) {
            technologyList2.push(technologies[i]);
        }

        for (var i = parseInt(technologies.length / 3 * 2); i < technologies.length; i++) {
            technologyList3.push(technologies[i]);
        }
    }
    else {
        technologyList1 = technologies;
    }

    var container = document.getElementById("technology-group");

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }

    if (technologyList1.length > 0) {
        var mainDiv1 = document.createElement("div");
        mainDiv1.setAttribute("class", "controls span2");

        technologyList1.forEach(technology => {
            var label = document.createElement("label");
            label.setAttribute("class", "checkbox");
            label.setAttribute("style", "margin-right: 1.5%");

            var input = document.createElement("input");
            input.setAttribute("type", "checkbox");
            input.setAttribute("value", technology.description);
            input.setAttribute("id", "technologyFilter" + technology.idTechnology);

            label.appendChild(input);
            label.insertAdjacentText("beforeend", technology.description);

            mainDiv1.appendChild(label);
        });

        container.appendChild(mainDiv1);
    }

    if (technologyList2.length > 0) {
        var mainDiv2 = document.createElement("div");
        mainDiv2.setAttribute("class", "controls span2");

        technologyList2.forEach(technology => {
            var label = document.createElement("label");
            label.setAttribute("class", "checkbox");
            label.setAttribute("style", "margin-right: 1.5%");

            var input = document.createElement("input");
            input.setAttribute("type", "checkbox");
            input.setAttribute("value", technology.description);
            input.setAttribute("id", "technologyFilter" + technology.idTechnology);

            label.appendChild(input);
            label.insertAdjacentText("beforeend", technology.description);

            mainDiv2.appendChild(label);
        });

        container.appendChild(mainDiv2);
    }

    if (technologyList3.length > 0) {
        var mainDiv3 = document.createElement("div");
        mainDiv3.setAttribute("class", "controls span2");

        technologyList3.forEach(technology => {
            var label = document.createElement("label");
            label.setAttribute("class", "checkbox");
            label.setAttribute("style", "margin-right: 1.5%");

            var input = document.createElement("input");
            input.setAttribute("type", "checkbox");
            input.setAttribute("value", technology.description);
            input.setAttribute("id", "technologyFilter" + technology.idTechnology);

            label.appendChild(input);
            label.insertAdjacentText("beforeend", technology.description);

            mainDiv3.appendChild(label);
        });

        container.appendChild(mainDiv3);
    }
}

function renderTechnologies(id, technologies) {
    if (id)
        var body = document.getElementById("technologiesBody_" + id);
    else
        var body = document.getElementById("technologiesBody");

    while (body.firstChild) {
        body.removeChild(body.firstChild);
    }

    var row = document.createElement("div");
    row.setAttribute("class", "row");

    var firstRow = document.createElement("div");
    firstRow.setAttribute("class", "col-lg-6");
    var firstRowUl = document.createElement("ul");
    firstRowUl.setAttribute("class", "list-unstyled mb-0");

    var secondRow = document.createElement("div");
    secondRow.setAttribute("class", "col-lg-6");
    var secondRowUl = document.createElement("ul");
    secondRowUl.setAttribute("class", "list-unstyled mb-0");

    if (technologies.length > 0) {

        for (var i = 0; i < parseInt(technologies.length / 2); i++) {
            var li = document.createElement("li");
            var a = document.createElement("a");
            a.textContent = technologies[i].IdTechnologyNavigation.Description;
            a.setAttribute("href", "#");
            li.appendChild(a);
            firstRowUl.appendChild(li);
        }

        if (technologies.length > 1) {

            for (var i = parseInt(technologies.length / 2); i < technologies.length; i++) {
                var li = document.createElement("li");
                var a = document.createElement("a");
                a.textContent = technologies[i].IdTechnologyNavigation.Description;
                a.setAttribute("href", "#");
                li.appendChild(a);
                secondRowUl.appendChild(li);
            }
        }
    }
    else {
        var p = document.createElement("p");
        p.textContent = "Sem tecnologias associadas.";
        firstRowUl.appendChild(p);
    }

    firstRow.appendChild(firstRowUl);
    secondRow.appendChild(secondRowUl);

    row.appendChild(firstRow);
    row.appendChild(secondRow);

    body.appendChild(row);
}

function renderMentors(mentors) {
    var body = document.getElementById("mentorsBody");

    while (body.firstChild) {
        body.removeChild(body.firstChild);
    }

    var row = document.createElement("div");
    row.setAttribute("class", "row");

    var firstRow = document.createElement("div");
    firstRow.setAttribute("class", "col-lg-6");
    var firstRowUl = document.createElement("ul");
    firstRowUl.setAttribute("class", "list-unstyled mb-0");

    var secondRow = document.createElement("div");
    secondRow.setAttribute("class", "col-lg-6");
    var secondRowUl = document.createElement("ul");
    secondRowUl.setAttribute("class", "list-unstyled mb-0");

    if (mentors.length > 0) {
        for (var i = 0; i < mentors.length / 2; i++) {
            var li = document.createElement("li");
            var a = document.createElement("a");
            a.textContent = mentors[i].IdSchoolMentorNavigation.Name;
            a.setAttribute("href", "#");
            li.appendChild(a);
            firstRowUl.appendChild(li);
        }

        if (mentors.length > 1) {

            for (var i = parseInt(mentors.length / 2); i < mentors.length; i++) {
                var li = document.createElement("li");
                var a = document.createElement("a");
                a.textContent = mentors[i].IdSchoolMentorNavigation.Name;
                a.setAttribute("href", "#");
                li.appendChild(a);
                secondRowUl.appendChild(li);
            }
        }
    }
    else {
        var p = document.createElement("p");
        p.textContent = "Sem orientadores associados.";
        firstRowUl.appendChild(p);
    }


    firstRow.appendChild(firstRowUl);
    secondRow.appendChild(secondRowUl);

    row.appendChild(firstRow);
    row.appendChild(secondRow);

    body.appendChild(row);
}

function filterSearch() {
    var searchQuery = document.getElementById("search-query").value;

    if (searchQuery.trim().length == 0)
        searchQuery = "";

    var technologyFilters = document.getElementById("input-technologies").value;
    var marksRange = document.getElementById("marks_range").value;

    window.location.replace('/Projects?q=' + searchQuery + "&technologies=" + technologyFilters + "&marks=" + marksRange);


    //$.ajax({
    //    type: "GET",
    //    url: '/Projects?search_by=' + searchQuery + "&technologies=" + technologyFilters,
    //    contentType: "application/json; charset=utf-8",
    //    dataType: "html",
    //})
}