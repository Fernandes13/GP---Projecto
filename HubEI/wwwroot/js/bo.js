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

    var edit_number = document.getElementById("edit-student-number").value;

    var edit_txt_email = document.getElementById("edit-student-email");

    edit_txt_email.value = edit_number + "@estudantes.ips.pt";
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

function editStudent(id) {
    id = id.replace('edit_', '');
    $.ajax({
        type: "GET",
        url: '/BackOffice/GetStudent?student_id=' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).done(function (res) {
        fillStudentForm(JSON.parse(res));

    });
}

function fillStudentForm(student) {
    var student_id = document.getElementById("edit-student-id");
    student_id.value = student.idStudent;

    var student_name = document.getElementById("edit-student-name");
    student_name.value = student.name;

    var student_number = document.getElementById("edit-student-number");
    student_number.value = student.studentNumber;

    var student_email = document.getElementById("edit-student-email");
    student_email.value = student.email;

    var student_telephone = document.getElementById("edit-student-telephone");
    student_telephone.value = student.telephone;

    /* ADDRESS */
    var address = student.idAddressNavigation;

    var student_address_id = document.getElementById("edit-student-address-id");
    student_address_id.value = student.idAddress;

    var student_address = document.getElementById("edit-student-address");
    student_address.value = address.address1;

    var student_postalcode = document.getElementById("edit-student-postalcode");
    student_postalcode.value = address.postalCode;

    var student_door = document.getElementById("edit-student-door");
    student_door.value = address.door;

    var student_locality = document.getElementById("edit-student-locality");
    student_locality.value = address.locality;

    var student_birthdate = document.getElementById("edit-student-birthdate");
    student_birthdate.value = student.birthDate.split("T")[0];

    var student_district = document.getElementById("edit-address-district");
    student_district.value = address.idDistrict;

    var student_branch = document.getElementById("edit-student-branch");
    student_branch.value = student.idStudentBranch;
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