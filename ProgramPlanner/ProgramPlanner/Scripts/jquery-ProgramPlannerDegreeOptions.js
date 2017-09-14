// Populates the dropdown list 'ddlDegrees' with all Degrees associated with the First university in 'ddlUniverities'
// param: universityID: Will be associated with a particular University selected by the user.
function DegreeOptions() {
    $("#ddlDegrees").html("");  // Remove the contents from the dropdown list associated with Degrees.
    $("#ddlYearDegrees").html("");
    $("#ddlMajors").html("");
    var universityID = $("#ddlUniversities").val();
    $.ajax({
        url: "/MainMenu/DegreeOptions",
        data: { universityID: universityID },
        cache: false,
        type: "POST",
        success: function (data) {
            // Populate the dropdown list with the values extracted from the database.
            var selection = "<option value = " + data[0].Value + " selected=" + "selected" + ">" + data[0].Text + "</option>";
            for (var x = 1; x < data.length; x++) {
                selection += "<option value = " + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#ddlDegrees").html(selection).show();
            // Begin to populate the Years associated with the first degree.
            YearDegreeOptions();
        },
        error: function () {
            alert("Could not connect to requested page.");
        }
    }
    )
}
// Populates the dropdown list 'ddlYears' with all Degrees associated with the First Degree in 'ddlDegrees'
// param: degreeID: Will be associated with a particular Degree selected by the user.
function YearDegreeOptions() {
    $("#ddlYearDegrees").html("");       // Remove the contents from the dropdown list associated with Years.
    $("#ddlMajors").html("");
    var degreeID = $("#ddlDegrees").val();
    $.ajax({
        url: "/MainMenu/YearDegreeOptions",
        data: { degreeID: degreeID },
        cache: false,
        type: "POST",
        success: function (data) {
            var selection = "<option value = " + data[0].Value + " selected=" + "selected" + ">" + data[0].Text + "</option>";
            for (var x = 1; x < data.length; x++) {
                selection += "<option value = " + data[x].Value + ">" + data[x].Text + "</option>";
            }
            $("#ddlYearDegrees").html(selection).show();
            // Begin to populate the Majors associated with the first Year.
            MajorOptions();
        },
        error: function () {
            alert("Could not connect to requested page.");
        }
    }
    )
}
// Populates the dropdown list 'ddlMajors' with all Degrees associated with the First Year in 'ddlYears'
// param: yearDegreeID: Will be associated with a particular Year selected by the user.
function MajorOptions() {
    $("#ddlMajors").html("");    // Remove the contents from the dropdown list associated with Majors.
    var yearDegreeID = $("#ddlYearDegrees").val();
    $.ajax({
        url: "/MainMenu/MajorOptions",
        data: { yearDegreeID: yearDegreeID },
        cache: false,
        type: "POST",
        success: function (data) {
            var selection = "<option value = " + data[0].Value + " selected=" + "selected" + ">" + data[0].Text + "</option>";
            for (var x = 1; x < data.length; x++) {
                selection += "<option value = " + data[x].Value + ">" + data[x].Text + "</option>";
            }
            // Begin to populate the Majors associated with the first Year.
            $("#ddlMajors").html(selection).show();
        },
        error: function () {
            alert("Could not connect to requested page.");
        }
    }
    )
}