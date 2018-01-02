function InvalidMail(textbox) {

    if (textbox.value == '') {
        textbox.setCustomValidity('אנא הכנס כתובת מייל');
    }
    else if (textbox.validity.typeMismatch) {
        textbox.setCustomValidity('אנא הכנס כתובת תקינה');
    }
    else {
        textbox.setCustomValidity('');
    }
    return true;
}

function InvalidName(textbox) {

    if (textbox.value == '') {
        textbox.setCustomValidity('שדה זה הוא שדה חובה');
    }
    else {
        textbox.setCustomValidity('');
    }
    return true;
}

function InvalidPass(textbox) {

    if (textbox.value == '') {
        textbox.setCustomValidity('אנא הכנס סיסמה');
    }

    else {
        textbox.setCustomValidity('');
    }
    return true;
}