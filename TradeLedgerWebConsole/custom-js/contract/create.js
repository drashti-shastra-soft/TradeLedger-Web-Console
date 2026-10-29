$(document).ready(function () {

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


    $('.btn-submit').on('click', function () {
        if (!fnValidation()) return;

        const payload = {
            CompanyId: $('#CompanyId').length > 0 ? $('#CompanyId').val() : 0,
            SellerClientId: $('#SellerClientId').val(),
            SellerBrokerId: $('#SellerBrokerId').val(),
            BuyerClientId: $('#BuyerClientId').val(),
            BuyerBrokerId: $('#BuyerBrokerId').val(),
            ContractItems: collectItems()
        };

        var requestData = JSON.stringify(payload);
        debugger;
        $.ajax({
            url: '/Contract/Create',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: requestData,
            beforeSend: function () {
                $('#loading-element').show();
            },
            success: function (response) {
                debugger
                $('#loading-element').hide();
                debugger
                if (response && response.NotificationType && response.NotificationMsg) {
                    $('#modal-contract').modal('hide');
                    showToastr(response.NotificationType, response.NotificationMsg);
                    $('#frmList').submit();
                }
            },
            error: function () {
                debugger;
                $('#loading-element').hide();
            },
        });
    });

    $('#add-item').click(function () {
        let index = $('.item-entry').length;
        $.get('/Contract/GetContractItemRowPartial?index=' + index, function (html) {
            $('#item-container').append(html);
        });
    });
});

function fnValidation() {
    var isValid = true;
    var companyId = $.trim($('#CompanyId').val());
    var sellerClientId = $.trim($('#SellerClientId').val());
    var sellerBrokerId = $.trim($('#SellerBrokerId').val());
    var buyerClientId = $.trim($('#BuyerClientId').val());
    var buyerBrokerId = $.trim($('#BuyerBrokerId').val());


    // Company
    if ($('#CompanyId').length > 0) {
        var companyId = $.trim($('#CompanyId').val());

        if (companyId == null || companyId == "" || companyId == undefined) {
            $("#CompanyId").next("span").text("Company is required!");
            isValid = false;
        } else {
            $('#CompanyId').next('span').text("");
        }
    }

    // Seller Client
    if (sellerClientId == null || sellerClientId == "" || sellerClientId == undefined) {
        $("#SellerClientId").next("span").text("Seller Client is required!");
        isValid = false;
    } else {
        $('#SellerClientId').next('span').text("");
    }

    //Seller Broker 
    if (sellerBrokerId == null || sellerBrokerId == "" || sellerBrokerId == undefined) {
        $("#SellerBrokerId").next("span").text("Seller Broker is required!");
        isValid = false;
    } else {
        $('#SellerBrokerId').next('span').text("");
    }

    // Buyer Client
    if (buyerClientId == null || buyerClientId == "" || buyerClientId == undefined) {
        $('#BuyerClientId').next('span').text('Buyer Client is required!');
        isValid = false;
    } else {
        $('#BuyerClientId').next('span').text('');
    }

    // Buyer Broker
    if (buyerBrokerId == null || buyerBrokerId == "" || buyerBrokerId == undefined) {
        $('#BuyerBrokerId').next('span').text('Buyer Broker is required!');
        isValid = false;
    } else {
        $('#BuyerBrokerId').next('span').text('');
    }


    return isValid;
}

function collectItems() {
    const contractItems = [];
    $('.item-entry').each(function () {
        const itemIdVal = $(this).find('.ItemId').val();
        const rateVal = $(this).find('.Rate').val();
        const qtyVal = $(this).find('.Qty').val();

        const itemId = itemIdVal ? parseInt(itemIdVal) : 0;
        const rate = rateVal ? parseFloat(rateVal) : 0;
        const qty = qtyVal ? parseInt(qtyVal) : 0;

        contractItems.push({
            ItemId: itemId,
            Rate: rate,
            Qty: qty
        });
    });

    return contractItems;
}