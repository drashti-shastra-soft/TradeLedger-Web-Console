$(document).ready(function () {

    $('#HNSCode').on('input', function () {
        let code = this.value;

        // Mask everything except the first 3 and last 3 characters
        if (code.length > 6) {
            let masked = code.substring(0, 3) + "***" + code.substring(code.length - 3);
            this.value = masked;
        }
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
            showToastr(notificationType, notificationMsg);
            $('.modal').modal('hide');
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('#frmList').submit();
        }
    }
});

function fnValidation() {
    var isValid = true;

    var name = $.trim($('#Name').val());
    var code = $.trim($('#Code').val());
    var hSNCode = $.trim($('#HSNCode').val());
    var unitId = $.trim($('#UnitId').val());


    // Name
    if (name == null || name == "" || name == undefined) {
        $("#Name").next("span").text("Name is required!");
        isValid = false;
    } else {
        $('#Name').next('span').text("");
    }

    //code
    if (code == null || code == "" || code == undefined) {
        $("#Code").next("span").text("Code is required!");
        isValid = false;
    } else {
        $('#Code').next('span').text("");
    }

    // hSNCode
    if (hSNCode == null || hSNCode == "" || hSNCode == undefined) {
        $('#HSNCode').next('span').text('HSN Code is required!');
        isValid = false;
    } else {
        $('#HSNCode').next('span').text('');
    }

    // unitId
    if (unitId == null || unitId == "" || unitId == undefined) {
        $('#UnitId').next('span').text('Unit is required!');
        isValid = false;
    } else {
        $('#UnitId').next('span').text('');
    }


    return isValid;
}