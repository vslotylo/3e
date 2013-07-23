$('#getCallback').on('click', function (e) {
    var phone = $('#mobilePhone').val();
    $.post("/home/callback", { phone: phone })
    .done(function (e) {
        $('#callbackDialog').modal('hide');
    });
});