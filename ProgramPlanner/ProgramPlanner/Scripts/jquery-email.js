// Author: Ryan Cunneen
// Date Created: 27-Sep-2017
// Date Modified: 27-Sep-2017
document.getElementById("emailBtn").addEventListener("click", emailProtocol);
// Get the modal
var modal = document.getElementById('myModal');

// Get the button that opens the modal
document.getElementById("myBtn").addEventListener("click", function () {
    modal.style.display = "block";
    // Hide the loading
    loadingScreen("hide");
});

// When the user clicks on the span id = closeModal, it will close the form. 
document.getElementById("closeModal").addEventListener("click", function () {
    modal.style.display = "none";
});

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
};
// A set of email protocols that must be passed before the user may be allowed to receive the program structure email. 
function emailProtocol() {
    var email = document.getElementById("firstEmail");
    var conEmail = document.getElementById("confirmationEmail");
    var emailBtn = document.getElementById("emailBtn");
    /*
    if (email.value === "" || conEmail.value === "") {
        alert("You need to enter your student email.");
    }
    else if (email.value !== conEmail.value) {
        alert("Emails do not match.");  
    }
    else {
        if (confirm("Do you wish to email your program structure?")) {
            // First we change the text of the courses that have not been assigned.
            changeText();
            // Then we take the snap shot of the program structure. 
            screenshot();
            // revert the text back to default;
            defaultText();
        }
    }*/
}
// Changes the text of the course boxes.
function changeText() {
    $(".courseBox").each(function () {
        // Why not use the hasClass method, this is because if we are checking
        // if the div hasClass("class"), then all of the divs will be changed including the ones
        // that have a issued course. Therefore, it is better to check the text. 
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
function defaultText() {
    $(".courseBox").each(function () {
        if ($(this).text().includes("Empty")) {
            $(this).text("Elective");
        }
        else if ($(this).text().includes("Core")) {
            // Because the button is an core optional course, it has additional functionality, so instead of changing the text
            // we need to change it back to its originally functionality. 
            $(this).html('<input type="button" id="' + $(this).attr("id") + '" class="btnGridDegreeSlot" value="Select Core"/>');
        }
        else if ($(this).text().includes("3000")) {
            $(this).text("Click to choose a 3000 level directed course");
        }
        else if ($(this).text().includes("Directed")) {
            $(this).text("Click to choose any directed course");
        }
    });
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
    var name = document.getElementById("fname").value;
    var to = document.getElementById("firstEmail").value;
    request(sc, name ,to);
}

// Performs an ajax request to upload the 'screenshot'. 
// @param sc: 'Screenshot' of the elements inside the div tag 'plan'
// @param to: Email provided by the user.
function request(sc, name, to) {
    $.ajax({
        type: "POST",
        url: "/Email/Upload",
        cache: false,
        data: { "screenshot": sc,"name":name, "to": to },
        dataType: "json",
        async: true,
        timeout: 400000,
        beforeSend: function () {
            // display the loading display
            loadingScreen("display");
        },
        success: function (data) {
            try {
                // Why using the saved attribute? If something went wrong at the backend,
                // the frontend won't know about it, so we tell the backend to send a boolean value
                // that determines if all went well. 
                var saved = Boolean(data.saved);
                if (saved === true) {
                    // Close the loading screen.
                    loadingScreen("close");
                }
                else {
                    // Display an error as the email could not be sent. 
                    loadingScreen("error");
                }
            }
            catch(e){
                loadingScreen("error");
            }
        },
        error: function () {
            alert("Could not connect to requested page.");
        }
    });
}
// Determines what particular scenario the loading screen should respond to.
function loadingScreen(action) {
    switch (action) {
        case "display":
            $("#loader").show();
            $("#loader").html("");
            $("#loader").addClass("loadingAnimation");
            $("#fname").prop('disabled', true);
            $("#firstEmail").prop('disabled', true);
            $("#confirmationEmail").prop('disabled', true);
            $("#submit").prop('disabled', true);
            break;
        case "close":
            $("#loader").removeClass("loadingAnimation");
            $("#loader").html("&#9989 Your email has been sent.");
            $("#fname").prop('disabled', false);
            $("#firstEmail").prop('disabled', false);
            $("#confirmationEmail").prop('disabled', false);
            $("#submit").prop('disabled', false);
            break;
        case "error":
            $("#loader").removeClass("loadingAnimation");
            $("#loader").html("&#10060 Oops! Your email could not be sent.");
            $("#fname").prop('disabled', false);
            $("#firstEmail").prop('disabled', false);
            $("#confirmationEmail").prop('disabled', false);
            $("#submit").prop('disabled', false);
            break;
        case "hide":
            $("#loader").hide();
            break;
    }
}