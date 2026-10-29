$(document).ready(function () {

    $('#MobileNumber').mask('+91 00000 00000', {
        translation: {
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            if (!val.startsWith('+91')) {
                field.val('+91 ' + val.replace(/[^\d]/g, '').substring(0, 10).replace(/(\d{5})(\d{0,5})/, '$1 $2'));
            }
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

    $('#PanNumber').mask('AAAAA0000A', {
        translation: {
            'A': { pattern: /[A-Za-z]/ },
            '0': { pattern: /[0-9]/ }
        },
        onKeyPress: function (val, e, field, options) {
            val = val.toUpperCase(); // Convert to uppercase
            field.val(val);
        }
    });


    $('#GSTNumber').mask('00AAAAA0000A0A', {
        translation: {
            '0': { pattern: /[0-9]/ },
            'A': { pattern: /[A-Za-z]/ }
        },
        onKeyPress: function (val, e, field, options) {
            val = val.toUpperCase(); // Convert to uppercase
            field.val(val);
        }
    });

    $("[data-form]").click(function () {
        if ($(this).data("form") === "frmCreate" && fnValidationCreateClient()) {
            $('#' + $(this).data("form")).submit();
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

function fnValidationCreateClient() {
    var isValid = true;
    
    var emailRegex = /^[^\s@@]+@@[^\s@@]+\.[^\s@@]+$/;
    var clientTypeId = $.trim($("#ClientTypeId").val());
    var name = $.trim($("#Name").val());
    var emailAddress = $.trim($("#EmailAddress").val());
    var mobileNumber = $.trim($("#MobileNumber").val());
    var gstNumber = $.trim($("#GSTNumber").val());
    var panNumber = $.trim($("#PanNumber").val());
    var contactPerson = $.trim($("#ContactPerson").val());



    // contactPerson
    if (contactPerson == null || contactPerson == "" || contactPerson == undefined) {
        $("#ContactPerson").next("span").text("Contact Person is required!");
        isValid = false;
    } else {
        $("#ContactPerson").next("span").text("");
    }

    // clientType
    if (clientTypeId == null || clientTypeId == "" || clientTypeId == undefined) {
        $("#ClientTypeId").next("span").text("Client Type is required!");
        isValid = false;
    } else {
        $("#ClientTypeId").next("span").text("");
    }


    // name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $("#Name").next("span").text("");
    }

    if (gstNumber == null || gstNumber == "" || gstNumber == undefined) {
        $("#GSTNumber").next("span").text("GST Number is required!");
        isValid = false;
    } else {
        $("#GSTNumber").next("span").text("");
    }

    if (panNumber == null || panNumber == "" || panNumber == undefined) {
        $("#PanNumber").next("span").text("PAN Number is required!");
        isValid = false;
    } else {
        $("#PanNumber").next("span").text("");
    }

    // Email Address
    if (emailAddress == null || emailAddress == "" || emailAddress == undefined) {
        $("#EmailAddress").next("span").text("Email Address   is required!");
        isValid = false;
    } else {
        $("#EmailAddress").next("span").text("");
    }

    // Mobile Number
    if (mobileNumber == null || mobileNumber == "" || mobileNumber == undefined) {
        $("#MobileNumber").next("span").text("Mobile Number is required!");
        isValid = false;
    } else {
        $("#MobileNumber").next("span").text("");
    }

    return isValid;
}
