$('#getCallback').on('click', function () {
    var phone = $('#mobilePhone').val();
    $.post("/home/callback", { phone: phone, url: $(location).attr('href') })
    .done(function () {
        $('#callbackDialog').modal('hide');
    });
});