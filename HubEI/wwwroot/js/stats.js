function renderStatistics() {
    $.ajax({
        type: "GET",
        url: '/Statistics/DistrictsStats',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).done(function (res) {
        renderStudentsDistricts(JSON.parse(res));
    });

    $.ajax({
        type: "GET",
        url: '/Statistics/MarksStats',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).done(function (res) {
        renderProjectMarks(JSON.parse(res));
        });

    $.ajax({
        type: "GET",
        url: '/Statistics/Top10Technologies',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false,
    }).done(function (res) {
        renderProjectTechnologies(JSON.parse(res));
    });

    $.ajax({
        type: "GET",
        url: '/Statistics/Top10MentorsAverage',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false,
    }).done(function (res) {
        renderMentorsAverage(JSON.parse(res));
        });

    $.ajax({
        type: "GET",
        url: '/Statistics/Top5BusinessAreas',
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        async: false,
    }).done(function (res) {
        renderTopBusinessAreas(JSON.parse(res));
    });
}

function renderStudentsDistricts(stats) {
    var districts = [];
    var counts = [];

    stats.forEach(function (stat) {
        districts.push(stat.district.description);
        counts.push(stat.count);
    })

    var context = document.getElementById("students_districts_chart").getContext('2d');

    var myLineChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: districts,
            datasets: [
                {
                    data: counts,
                    backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
                    hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
                }
            ]
        },
        options: {
            responsive: true
        }
    });
}

function renderTopBusinessAreas(stats)
{
    var areas = [];
    var counts = [];

    stats.forEach(function (stat) {
        areas.push(stat.name);
        counts.push(stat.count);
    });

    var context = document.getElementById("business_areas_chart").getContext('2d');

    var myLineChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: areas,
            datasets: [
                {
                    data: counts,
                    backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
                    hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
                }
            ]
        },
        options: {
            responsive: true
        }
    });
}

function renderProjectMarks(marks) {
    var context = document.getElementById("project_marks_chart").getContext('2d');

    var myLineChart = new Chart(context, {
        type: 'line',
        data: {
            labels: ["0", "1", "2", "3", "4", "5", "6", "7","8","9","10","11","12","13","14","15","16","17","18","19","20"],
            datasets: [
                {
                    label: "Evalutation",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: marks
                },
            ]
        },
        options: {
            responsive: true
        }
    });
}

function renderProjectTechnologies(stats) {
    var context = document.getElementById("project_technologies_chart").getContext('2d');

    var technologyNames = [];
    var technologyCount = [];

    stats.forEach(technology => {
        technologyNames.push(technology.description);
        technologyCount.push(technology.count);
    });

    var myBarChart = new Chart(context, {
        type: 'bar',
        data: {
            labels: technologyNames,
            datasets: [
                {
                    label: "# of uses",
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(165, 42, 42, 0.2)',
                        'rgba(0, 0, 0, 0.2)',
                        'rgba(0, 100, 0, 0.2)',
                        'rgba(255,20,147,0.2)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(165, 42, 42, 1)',
                        'rgba(0, 0, 0, 1)',
                        'rgba(0, 100, 0, 1)',
                        'rgba(255,20,147,1)'
                    ],
                    borderWidth: 1,
                    data: technologyCount
                },
            ]
        },
        options: {
            responsive: true,
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}

function renderMentorsAverage(stats) {
    var mentors = [];
    var counts = [];

    stats.forEach(function (stat) {
        mentors.push(stat.name);
        counts.push(Math.round(stat.average * 100) / 100);
    });

    var context = document.getElementById("mentors_average_chart").getContext('2d');

    var myLineChart = new Chart(context, {
        type: 'pie',
        data: {
            labels: mentors,
            datasets: [
                {
                    data: counts,
                    backgroundColor: ["#F7464A", "#46BFBD", "#FDB45C", "#949FB1", "#4D5360"],
                    hoverBackgroundColor: ["#FF5A5E", "#5AD3D1", "#FFC870", "#A8B3C5", "#616774"]
                }
            ]
        },
        options: {
            responsive: true
        }
    });
}





$(document).ready(function () {
    renderStatistics();
});