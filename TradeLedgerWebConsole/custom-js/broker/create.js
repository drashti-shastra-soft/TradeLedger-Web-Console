$(document).ready(function () {

    $('#PhoneNumber').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '').substring(0, 10).replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }
        }
    });

    $('#AadharNumber').mask('0000 0000 0000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        }
    });

    $('#PANNumber').mask('AAAAA0000A', {
        translation: {
            'A': { pattern: /[A-Za-z]/ },
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            val = val.toUpperCase(); // Convert to uppercase
            field.val(val);
        }
    });

    $('#EmailAddress').on('input', function () {
        let val = $(this).val();

        // Allow only valid email characters
        val = val.replace(/[^a-zA-Z0-9@._\-]/g, '');

        // Prevent double @ symbols
        const parts = val.split('@');
        if (parts.length > 2) {
            val = parts[0] + '@' + parts[1];
        }

        $(this).val(val);
    });

    $(".btn-submit").click(function () {
        if (fnValidation()) {
            $('#frmCreate').submit();
        }
    });


    var notificationType = $.trim($('#NotificationType').val());
    var notificationMsg = $.trim($('#NotificationMsg').val());

    if (notificationType != null && notificationType != undefined && notificationType != '') {
        if (notificationMsg != null && notificationMsg != undefined && notificationMsg != '') {

            showToastr(notificationType, notificationMsg)
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }

});

function fnValidation() {
    var isValid = true;
    
    var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
    var name = $.trim($("#Name").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var phoneNumber = $.trim($("#PhoneNumber").val());
    var panNumber = $.trim($("#PANNumber").val());
    var aadharNumber = $.trim($("#AadharNumber").val());
    var contactPerson = $.trim($("#ContactPerson").val());
    var brokerage = $.trim($("#Brokerage").val()).replace(/[^\d]/g, '');


    //Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    //Brokerage
    if (isNaN(brokerage) || brokerage < 0 || brokerage > 100) {
        $("#Brokerage").next("span").text("Please enter a valid brokerage between 0% and 100%!");
        isValid = false;
    } else {
        $("#Brokerage").next("span").text("");
    }

    //Contact Person
    if (contactPerson == null || contactPerson == "" || name == undefined) {
        $("#ContactPerson").next("span").text("Contact Person is required!");
        isValid = false;
    } else {
        $("#ContactPerson").next("span").text("");
    }

    //PAN Number
    if (panNumber == null || panNumber == "" || panNumber == undefined) {
        $("#PANNumber").next("span").text("PAN Number is required!");
        isValid = false;
    } else {
        $("#PANNumber").next("span").text("");
    }

    //Aadhar Number
    if (aadharNumber == null || aadharNumber == "" || aadharNumber == undefined) {
        $("#AadharNumber").next("span").text("Aadhar Number is required!");
        isValid = false;
    } else {
        $("#AadharNumber").next("span").text("");
    }

    //Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address  is required!");
        isValid = false;
    } else {
        $("#EmailAddress").next("span").text("");
    }

    //Phone Number
    if (phoneNumber == null || phoneNumber == "" || phoneNumber == undefined) {
        $("#PhoneNumber").next("span").text("Mobile Number is required!");
        isValid = false;
    } else {
        $("#PhoneNumber").next("span").text("");
    }

    return isValid;
}
