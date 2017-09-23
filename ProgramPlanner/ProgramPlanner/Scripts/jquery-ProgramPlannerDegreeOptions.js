
// Author: Ryan Cunneen
// Student Number: 3179234
// Date Created: 14-Sep-2017
// Date Last Modified: 15-Sep-2017

// JQuery Event Listeners:
$(document).on("change", "#ddlUniversities", function () {
    DegreeOptions();
});

$(document).on("change", "#ddlDegrees", function () {
    YearDegreeOptions();
});

$(document).on("change", "#ddlYearDegrees", function () {
    MajorOptions();
});

// Populates the dropdown list 'ddlDegrees' with all Degrees associated with the First university in 'ddlUniverities'
// param: universityID: Will be associated with a particular University selected by the user.
function DegreeOptions() {
    $("#ddlDegrees").html("");  // Remove the contents from the dropdown list associated with Degrees.
    $("#ddlYearDegrees").html("");
    $("#ddlMajors").html("");
    var universityID = $("#ddlUniversities").val();

    // Request the data for the drop down list "#ddlDegrees"
    request("/MainMenu/DegreeOptions", "universityID", universityID, "#ddlDegrees", YearDegreeOptions)
}
// Populates the dropdown list 'ddlYears' with all Degrees associated with the First Degree in 'ddlDegrees'
// param: degreeID: Will be associated with a particular Degree selected by the user.
function YearDegreeOptions() {
    $("#ddlYearDegrees").html("");       // Remove the contents from the dropdown list associated with Years.
    $("#ddlMajors").html("");
    var degreeID = $("#ddlDegrees").val();

    // Request the data for the drop down list "#ddlYearDegrees"
    request("/MainMenu/YearDegreeOptions", "degreeID", degreeID, "#ddlYearDegrees", MajorOptions)
}

// Populates the dropdown list 'ddlMajors' with all Degrees associated with the First Year in 'ddlYears'
// param: yearDegreeID: Will be associated with a particular Year selected by the user.
function MajorOptions() {
    $("#ddlMajors").html("");    // Remove the contents from the dropdown list associated with Majors.
    var yearDegreeID = $("#ddlYearDegrees").val();

    // Request the data for the drop down list "#ddlMajors"
    request("/MainMenu/MajorOptions", "yearDegreeID", yearDegreeID, "#ddlMajors", null)
}

// Issues an ajax call to request the data from a particular function (url)
function request(url, field, id, selector, nextOptionsFunction) {

    // Create a simply object that stores the field, and id. 
    var dataObj = {};
    dataObj[field] = id;
    $.ajax({
        url: url,
        cache: false,
        type: "POST",
        data: dataObj,
        success: function (data) {
            // Save the options in the drop down list (selector). 
            implement(data, selector)

            // Possibly cascade to the last drop down list for implementing the data.
            nextOptionsFunction();
        },
        error: function () {
            alert("Could not connect to requested page.");
        }
    })
}

// Implements the data for a particular drop down list (selector). 
function implement(data, selector) {
    var selection = "<option value = " + data[0].Value + " selected=" + "selected" + ">" + data[0].Text + "</option>";
    for (var x = 1; x < data.length; x++) {
        selection += "<option value = " + data[x].Value + ">" + data[x].Text + "</option>";
    }
    // Begin to populate the Majors associated with the first Year.
    $(selector).html(selection).show();
}