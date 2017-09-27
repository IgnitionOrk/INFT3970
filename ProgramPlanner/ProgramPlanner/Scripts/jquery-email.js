/*
    Author: Ryan Cunneen
    Date Created: 27-Sep-2017
    Date Modified: 27-Sep-2017
*/
document.getElementById("emailBtn").addEventListener("click", checkEmails);


function checkEmails() {
    var email = document.getElementById("firstEmail");
    var conEmail = document.getElementById("confirmationEmail");
    var emailBtn = document.getElementById("emailBtn");
    if (email.value == "" || conEmail.value == "") {
        alert("You need to enter your student email.");
    }
    else if (email.value !== conEmail.value) {
        alert("Emails do not match.");
    }
    else {
            if (confirm("Do you wish to email your program structure?")) {
                screenshot();
            }
        }
    }
  

function screenshot() {
    var container = document.getElementById("plan");
    var semester = document.getElementById("semester1Box");
    var yearColumn = document.getElementById("yearNameColumn");

    // We must double the semester offset width, as there are 2 semesters. 
    var totalWidth = (semester.offsetWidth * 2) + yearColumn.offsetWidth;
    html2canvas(container, {
        onrendered: function (canvas) {
            var screenshot = canvas.toDataURL("screenshot/jpg");
            upload(screenshot);
        },
        width: totalWidth,
        height: semester.offsetHeight
    });
}

function upload(screenshot) {
    var sc = screenshot.replace(/^data:image\/(png|jpg);base64,/, "");
    var to = document.getElementById("firstEmail").value;
    request(sc, to);
}

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