
// Author: Ryan Cunneen
// Date Created: 27-Sep-2017
// Date Modified: 27-Sep-2017
document.getElementById("emailBtn").addEventListener("click", emailProtocol);

// A set of email protocols that must be passed before the user may be allowed to receive the program structure email. 
function emailProtocol() {
    var email = document.getElementById("firstEmail");
    var conEmail = document.getElementById("confirmationEmail");
    var emailBtn = document.getElementById("emailBtn");
    if (email.value === "" || conEmail.value === "") {
        alert("You need to enter your student email.");
    }
    else if (email.value !== conEmail.value) {
        alert("Emails do not match.");
    }
    else if (!isUonEmail(email.value)){
        alert("Email is not the correct format.");
    }
    else {
        if (confirm("Do you wish to email your program structure?")) {
            // First we change the text of the courses that have not been assigned.
            changeText();
            // Then we take the snap shot of the program structure. 
            screenshot();
            // revert the text back to normal;
            revertText();
        }
    }
}
// Changes the text of the course boxes.
function changeText() {
    $(".courseBox").each(function () {
        if ($(this).text().includes("Elective")) {
            $(this).text("Empty");
        }
        else if ($(this).html().includes("Core")) {
            $(this).text("Core");
        }     
        else if ($(this).text().includes("3000")) {
            $(this).text("3000 Directed");
        }
        else if ($(this).text().includes("directed")) { 
            $(this).text("Directed");
        }       
    });
}
// Reverts the text (or html) back to its previous form without losing any functionality of the application. 
function revertText() {
    $(".courseBox").each(function () {
        if ($(this).text().includes("Empty")) {
            $(this).text("Elective");
        }
        else if ($(this).text().includes("Core")) {
            // Because the button is an core optional course, it has additional functionality, so instead of changing the text
            // we need to change it back to its originally functionality. 
            $(this).html('<input type="button" id="' + i + 'btnGridDegreeSlot" class="btnGridDegreeSlot" value="Select Core"/>');
        }
        else if ($(this).text().includes("3000")) {
            $(this).text("Click to choose a 3000 level directed course");
        }
        else if ($(this).text().includes("Directed")) {
            $(this).text("Click to choose any directed course");
        }
    });
}

// Determines if the email the user has given is in an actual and correctly formatted uon email
// An example of such email might be e.g. c9999999@uon.edu.au;
// @param email: Email provided by the user. 
function isUonEmail(email) {
    var correct = false;
    try {
        var correctFirstLetter = email.substr(0, 1) === "c";
        var studentNumber = email.substr(1, 7);
        var atSignInCorrectPosition = email.substr(8, 1) === "@"
        var correctDomains = contains(email, 1) && contains(email, 2) && contains(email, 3);
        correct = correctFirstLetter && isNumeric(studentNumber) && atSignInCorrectPosition && correctDomains;
    }
    catch (exception) {
        // If the email not was in the correct format, or something happened
        // during the process of checking the email.
        correct = false;
    }
    return correct;
}

// Determines if the student number is completely comprised of numerical characters,
// and the student number is not infinite. 
// @param studentNumber: The 8 digit number contained in the student email.
function isNumeric(studentNumber) {
    return !isNaN(studentNumber) && isFinite(studentNumber);
}

// Determines if the email has certain identifiers within the email
// to ensure that it is a uon email that the application is sending the program structure to. 
// @param email: The first email the user has entered and wishes the program structure to 
// be emailed to
// @param domain: part of the email will contain particular domains of what sort of email the user has provided. 
// @param identifier: which domain we should be comparing.
    // 1: uon -> newcastle university
    // 2: edu -> an educational institute.
    // 3: au -> the country in which the user is in. 
function contains(email, identifier) {
    var sIndex = 0;       // starting index of the comparable variable
    var length = 0;       // the length of the comparable variable.
    var cVariable = ""; // comparable variable

    switch (identifier) {
        case 1:
            sIndex = 9;
            length = 3;
            cVariable = "uon";
            break;
        case 2:
            sIndex = 13;
            length = 3;
            cVariable = "edu";
            break;
        case 3:
            sIndex = 17;
            // We must extract all characters after the dot
            length = null;
            cVariable = "au";
            break;
    }

    // Why would be set length equal to null? This is because if the 
    // email contains au with characters after it e.g. 'au2', then having length equal to 2
    // will always return the substring 'au', which is not correct when there is another character after 'au'.
    // Therefore, setting length to null, we can omit it and extract all characters after 'au' as well. 
    var partial = "";
    if (length === null) {
        partial = email.substr(sIndex);
    }
    else {
        partial = email.substr(sIndex, length);
    }

    return partial == cVariable;
}

// Copies all the content that is within the div tag 'plan'
function screenshot() {
    var container = document.getElementById("plan");
    var semester = document.getElementById("semester1Box");
    var yearColumn = document.getElementById("yearNameColumn");

    // We must double the semester offset width, as there are 2 semesters. 
    var totalWidth = semester.offsetWidth * 2 + yearColumn.offsetWidth;
    html2canvas(container, {
        onrendered: function (canvas) {
            var screenshot = canvas.toDataURL("screenshot/jpg");
            upload(screenshot);
        },
        width: totalWidth,
        height: semester.offsetHeight
    });
}

// Uploads the 'screenshot' of the elements and uploads them to the server of the application. 
// @param screenshot: 'Screenshot' of the elements inside the div tag 'plan'
function upload(screenshot) {
    var sc = screenshot.replace(/^data:image\/(png|jpg);base64,/, "");
    var to = document.getElementById("firstEmail").value;
    request(sc, to);
}

// Performs an ajax request to upload the 'screenshot'. 
// @param sc: 'Screenshot' of the elements inside the div tag 'plan'
// @param to: Email provided by the user.
function request(sc, to) {
    $.ajax({
        type: "POST",
        url: "/Email/Upload",
        cache: false,
        data: { "screenshot": sc, "to": to },
        dataType: "json",
        success: function (data) {
            var saved = Boolean(data.saved);
            if (saved === true) {
                alert("Email has been sent.");
            }
            else {
                alert("Email could not be sent.");
            }
        },
        error: function () {
            alert("Could not connect to requested page.");
        }
    });
}