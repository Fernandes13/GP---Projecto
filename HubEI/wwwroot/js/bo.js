$(document).ready(function () {
    // Activate tooltip
    //$('[data-toggle="tooltip"]').tooltip();



    var cb_selected_count = 0;


    // Select/Deselect checkboxes
    var checkbox = $('table tbody input[type="checkbox"]');
    $("#selectAll").click(function () {
        if (this.checked) {
            checkbox.each(function () {
                this.checked = true;
                $("#btn-remove-selected").show();
            });
        } else {
            checkbox.each(function () {
                this.checked = false;
                cb_selected_count = 0;
                $("#btn-remove-selected").hide();
            });
        }



    });

    checkbox.click(function () {
        if (!this.checked) {
            cb_selected_count--;
            $("#selectAll").prop("checked", false);
        }
        else
            cb_selected_count++;

        if (cb_selected_count > 0) {
            $("#btn-remove-selected").show();
        }
        else
            $("#btn-remove-selected").hide();
    });
});


function fillStudentEmail() {
    var number = document.getElementById("student-number").value;

    var txt_email = document.getElementById("student-email");

    txt_email.value = number + "@estudantes.ips.pt";
}

function clearStudentForm() {
    /* document.getElementById("student-number").value = "";
 
     document.getElementById("student-email").value = "@estudantes.ips.pt";
 
     document.getElementById("student-contact").value = "";
 
     document.getElementById("student-birthdate").value = "";
     */
}

var students_choices = [];

function deleteStudent(id) {
    students_choices = [id];
}


function eliminatePendingStudents() {
    students_choices.forEach(function (student) {
        $.ajax({
            type: "DELETE",
            url: '/BackOffice/Student?StudentId=' + student,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
        }).done(function (res) {
        });
    });

    setTimeout(function () {
        $.ajax({
            type: "GET",
            url: '/BackOffice/Students',
            contentType: "application/json; charset=utf-8",
            dataType: "html",
        }).done(function (res) {

        });
    }, 800);

}



function deleteStudents() {

    var students = document.getElementsByName('students');

    students.forEach(function(student){
        if (student.checked)
            students_choices.push(student.id.replace('cb_', ''));
    })

    console.log(students_choices);
}




