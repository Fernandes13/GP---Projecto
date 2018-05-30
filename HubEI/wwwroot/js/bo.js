$(document).ready(function () {

    $('[data-toggle="tooltip"]').tooltip({ 'trigger': 'hover' });

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
        autocomplete: technologies
    });

    $('#input-technologies-edit').tagator({
        autocomplete: technologies
    });
});

function countUpInsert() {
    var length = $('#project-insert-description').val().length;
    var maxLength = document.getElementById("project-insert-description").maxLength;
    var myCounter = document.getElementById("insert-desc-count").textContent = length + "/" + maxLength;
}

function countUpEdit() {
    var length = $('#edit-project-description').val().length;
    var maxLength = document.getElementById("edit-project-description").maxLength;
    var myCounter = document.getElementById("edit-desc-count").textContent = length + "/" + maxLength;
}

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
}

function deleteStudents() {

    var students = document.getElementsByName('students');

    students.forEach(function (student) {
        if (student.checked)
            students_choices.push(student.id.replace('cb_', ''));
    })

    console.log(students_choices);
}

var mentors_choices = [];

function deleteMentor(id) {
    mentors_choices = [id];
}

function editMentor(id) {
    id = id.replace('edit_', '');
    $.ajax({
        type: "GET",
        url: '/BackOffice/GetMentor?mentor_id=' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).done(function (res) {
        fillMentorForm(JSON.parse(res));
    });
}


function fillMentorForm(mentor) {
    var mentor_id = document.getElementById("edit-mentor-id");
    mentor_id.value = mentor.idSchoolMentor;

    var mentor_name = document.getElementById("edit-mentor-name");
    mentor_name.value = mentor.name;

    var mentor_email = document.getElementById("edit-mentor-email");
    mentor_email.value = mentor.email;
}

function eliminatePendingMentors() {
    mentors_choices.forEach(function (mentor) {
        $.ajax({
            type: "DELETE",
            url: '/BackOffice/Mentor?MentorId=' + mentor,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
        }).done(function (res) {
            $.ajax({
                type: "GET",
                url: '/BackOffice/Mentors',
                dataType: "html",
            }).done(function (res) {

            });
        });
    });

    setTimeout()
}

function deleteMentors() {

    var mentors = document.getElementsByName('mentors');

    mentors.forEach(function (mentor) {
        if (mentor.checked)
            mentors_choices.push(mentor.id.replace('cb_', ''));
    })

    console.log(mentors_choices);
}

var projects_choices = [];

function deleteProject(id) {
    projects_choices = [id];
}
var pendingEditProject;

function editProject(id) {
    id = id.replace('edit_', '');

    pendingEditProject = id;

    $.ajax({
        type: "GET",
        url: '/BackOffice/GetProject?project_id=' + id,
        contentType: "application/json; charset=utf-8",
        dataType: "html",
    }).done(function (res) {
        fillProjectForm(JSON.parse(res));
    });
}

function fillProjectForm(project) {
    var project_id = document.getElementById("edit-project-id");
    project_id.value = project.idProject;

    var project_title = document.getElementById("edit-project-title");
    project_title.value = project.title;

    var project_description = document.getElementById("edit-project-description");
    project_description.value = project.description;

    var project_date = document.getElementById("edit-project-date");
    project_date.value = project.projectDate.split("T")[0];

    var project_isVisible = document.getElementById("edit-project-isVisible");
    project_isVisible.checked = project.isVisible;

    var project_type = document.getElementById("edit-project-type");
    project_type.value = project.idProjectType;

    var project_student = document.getElementById("edit-project-student");
    project_student.value = project.idStudent;

    var project_company = document.getElementById("edit-project-company");
    project_company.value = project.idCompany;

    var project_grade = document.getElementById("edit-project-grade");
    project_grade.value = project.grade;

    var project_report = document.getElementById("edit-project-report");
    project_report.value = "";

    var project_report_backup = document.getElementById("edit-project-report-backup");
    project_report_backup.value = project.report;

    countUpEdit();
    fillMentorsList(project.projectAdvisor);

    var technologies = [];
    project.projectTechnology.forEach(function (tech) {
        technologies.push(tech.idTechnologyNavigation.description);
   })

   document.getElementById("input-technologies-edit").value = technologies.join(',');
    $('#input-technologies-edit').tagator('refresh');

}

function fillMentorsList(mentors) {
    var checkboxes = document.querySelectorAll('*[id^="edit-project-mentor"]');

    checkboxes.forEach(checkbox => {
        checkbox.checked = false;
    });

    mentors.forEach(mentor => {
        var checkbox = document.getElementById("edit-project-mentor-" + mentor.idSchoolMentor);
        if (checkbox != null) {
            checkbox.checked = true;
        }
    });
}

function eliminatePendingProjects() {
    projects_choices.forEach(function (project) {
        $.ajax({
            type: "DELETE",
            url: '/BackOffice/Project?ProjectId=' + project,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
        }).done(function (res) {
            $.ajax({
                type: "GET",
                url: '/BackOffice/Projects',
                dataType: "html",
            }).done(function (res) {

            });
        });
    });
}

function editProjectTechnologies() {
    setTimeout(function () {
        var technologies_list;

        technologies_list = document.getElementById("input-technologies-edit").value.trim().split(',');

        console.log(technologies_list);

        $.ajax({
            type: "DELETE",
            url: '/BackOffice/ClearProjectTechnologies?project_id=' + pendingEditProject,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
        })
    }, 500);


    setTimeout(function () {
        var technologies_list;

        technologies_list = document.getElementById("input-technologies-edit").value.trim().split(',');

        console.log(technologies_list);

        technologies_list.forEach(function (tech) {
            $.ajax({
                type: "POST",
                url: '/BackOffice/EditProjectTechnology?project_id=' + pendingEditProject + '&tech=' + tech,
                contentType: "application/json; charset=utf-8",
                dataType: "html",
            })
        })
    }, 1000);
}

function addTechnologies() {
    setTimeout(function () {
        var technologies_list;

        technologies_list = document.getElementById("input-technologies").value.trim().split(',');

        console.log(technologies_list);

        technologies_list.forEach(function (tech) {
            $.ajax({
                type: "POST",
                url: '/BackOffice/AddProjectTechnology?tech=' + tech,
                contentType: "application/json; charset=utf-8",
                dataType: "html",
            })
        })
    }, 1000);
}

function deleteProjects() {

    var projects = document.getElementsByName('projects');

    projects.forEach(function (project) {
        if (project.checked)
            projects_choices.push(project.id.replace('cb_', ''));
    })
}

$("#add-project-form").keypress(function (e) {
    if (e.which == 13) {
        return false;
    }
});


$("#edit-project-form").keypress(function (e) {
    if (e.which == 13) {
        return false;
    }
});


function setFormValidations() {
    console.log("correu");
        'use strict';
        
            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.getElementsByClassName('needs-validation');
            // Loop over them and prevent submission
            var validation = Array.prototype.filter.call(forms, function (form) {
                form.addEventListener('submit', function (event) {
                    if (form.checkValidity() === false) {
                        event.preventDefault();
                        event.stopPropagation();
                    }
                    form.classList.add('was-validated');
                }, false);
            });
        
    
}

