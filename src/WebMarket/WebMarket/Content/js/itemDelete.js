$('#btnDeleteProduct').on('click', function () {
    var url = $('#removeItem').data("targeturl");
    $('#confirmItemDeleteDialog').modal('hide');
    $.ajax({
        type: "POST",
        url: url,
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        complete: function (data, textStatus, r) {
            window.location.href = data.responseJSON;
        }
    });
});

$('#btnCancel').on('click', function () {
    $('#confirmItemDeleteDialog').modal('hide');
});