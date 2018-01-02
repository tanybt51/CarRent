$(function () {
    console.log("validations ready!");



}); function InvalidName(textBox) {
    var text = $(textBox).val();
    console.log(text);
    if (text.length < 1) {
        $("#Invalid").text(textBox.title + ' הוא שדה חובה ')
        $(textBox).focus().addClass("invalid").val("");


    }
    else {
        $(textBox).removeClass("invalid");
        $("#Invalid").text('')
    }
}
function validTZ(tzBox) {
    var tz = $(tzBox).val();
    var master;
    console.log(tz);
    if (tz.length < 9) {
        $("#Invalid").text(tzBox.title + ' צריך להיות מינימום 9 ספרות ')
        $(tzBox).focus().addClass("invalid");


    } else {
        if (master != tz) {
            var master = tz;
            $.post("/Login/TZ?tz=" + tz, function (data, status) {

                //alert("Data: " + data + "\nStatus: " + status);

                if (data) {
                    $(tzBox).removeClass("invalid");
                    $("#Invalid").text('');
                } else {
                    $("#Invalid").text(tzBox.title + ' שהוכנסה אינה תקינה ')
                    $(tzBox).focus().addClass("invalid");
                }
            }

            );
        }
        else {
            $(tzBox).focus().addClass("invalid");
        }
    }
}
function validEmail(emailBox) {
    var email = $(emailBox).val();
    var master;
    console.log(email);
    if (email.length < 3 || !email.includes('@')) {
        $("#Invalid").text(emailBox.title + ' כתובת המייל אינה תקינה ')
        $(emailBox).focus().addClass("invalid");


    } else {
        if (master != email) {
            var master = email;
            $.post("/Login/Email?_email=" + email, function (data, status) {

                //alert("Data: " + data + "\nStatus: " + status);

                if (!data) {
                    $(emailBox).removeClass("invalid");
                    $("#Invalid").text('');
                } else {
                    $("#Invalid").text(emailBox.title + ' תפוס אנא בחר מייל אחר או התחבר ')
                    $(emailBox).focus().addClass("invalid");
                }
            }

            );
        }
        else {
            $(emailBox).focus().addClass("invalid");
        }
    }
}

function validProject(id) {

    var proj = $("#" + id).val();
    if (proj.length != "") {
        var project = _customer.Projects.find(p=>p.Name == proj)
        if (!project) {
            alert("ערך לא תקין");
            $("#" + id).css("background", "#ffcccc").val('').focus();
        } else {
            $("#" + id).css("background", "white");
        }
    }
}