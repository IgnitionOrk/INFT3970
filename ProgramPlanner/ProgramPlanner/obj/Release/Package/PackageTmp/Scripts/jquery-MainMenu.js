// Event listener:
document.getElementById("btnPlanner").addEventListener("click", moveToPlanner);


// Determines if the selected values are not null, as well as moves to the Program Planner if they are correct values.
function moveToPlanner() {
    var yearDegreeID = $("#ddlYearDegrees").val();
    var majorID = $("#ddlMajors").val();
    if (yearDegreeID === null || majorID === null) {
        alert("Select the appropriate values for degree or major.");
    }
    else {
        window.location.href = "/Plan/Create?yearDegreeID=" + yearDegreeID + "&majorID=" + majorID;
    }
}