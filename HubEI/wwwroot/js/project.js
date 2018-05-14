$(function () {
    $('div[onload]').trigger('onload');
});

function renderTechnologies(id, technologies)
{   
    console.log(technologies);
    if(id)
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
        for (var i = 0; i < technologies.length / 2; i++) {
            var li = document.createElement("li");
            var a = document.createElement("a");
            console.log(i);
            a.textContent = technologies[i].IdTechnologyNavigation.Description;
            a.setAttribute("href", "#");
            li.appendChild(a);
            firstRowUl.appendChild(li);
        }

        if (technologies.length > 1) {
            var half = parseInt(technologies.length / 2) % 2 === 0 ? parseInt(technologies.length / 2) : parseInt(technologies.length / 2) + 1;

            for (var i = half; i < technologies.length; i++) {
                var li = document.createElement("li");
                var a = document.createElement("a");
                console.log(i);
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

function renderMentors(mentors)
{
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
            var half = parseInt(mentors.length / 2) % 2 === 0 ? parseInt(mentors.length / 2) : parseInt(mentors.length / 2) + 1;

            for (var i = half; i < mentors.length; i++) {
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