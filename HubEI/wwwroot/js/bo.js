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
    console.log(number);

    var txt_email = document.getElementById("student-email");

    txt_email.value = number + "@estudantes.ips.pt";
}

function clearStudentForm() {
    document.getElementById("student-number").value = "";

    document.getElementById("student-email").value = "@estudantes.ips.pt";

    document.getElementById("student-contact").value = "";

    document.getElementById("student-birthdate").value = "";
}
