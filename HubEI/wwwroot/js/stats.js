function renderStatistics() {
    renderStudentsDistricts();
    renderProjectMarks();

}

function renderStudentsDistricts() {
    var context = document.getElementById("students_districts_chart").getContext('2d');

    var myLineChart = new Chart(context, {
        type: 'doughnut',
        data: {
            labels: ["Red", "Green", "Yellow", "Grey", "Dark Grey",],
            datasets: [
                {
                    data: [300, 50, 100, 40, 120],
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
function renderProjectMarks() {
    var context = document.getElementById("project_marks_chart").getContext('2d');

    var myLineChart = new Chart(context, {
        type: 'line',
        data: {
            labels: ["0", "1", "2", "3", "4", "5", "6", "7","8","9","10","11","12","13","14","15","16","17","18","19","20"],
            datasets: [
                {
                    label: "Nota",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [0, 1, 2, 3,4, 5, 6, 7,8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 190]
                },
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