function isInputValid(value) {
    if (!isNaN(value) && value > 0 && value <= 1000 && value % 1 === 0) {
        return true;
    }

    return false;
}

function addToCart(quantity, pid, handler) {
    var valid = isInputValid(quantity);
    if (!valid) {
        return;
    }

    $.post("/cart/add", { pid: pid, quantity: quantity })
    .done(function (e) {
        if (typeof (handler) !== 'undefined' && handler != null) {
            handler(e);
        }
    });
}

function deleteFromCart(pid, handler) {
    if (pid == null) {
        return;
    }

    $.post("/cart/delete", { pid: pid })
    .done(function (e) {
        if (typeof (handler) !== 'undefined' && handler != null) {
            handler(e);
        }
    });
}

function calculateItemPrice(quantity, price) {
    return quantity * price;
}

$(document).ready(function () {
    $('#addToCartBtn').on('click', function (e) {
        var quantity = $('#count').val();
        var pid = $('#pid').val();
        $('#addToCartModal').modal('hide');
        addToCart(quantity, pid, function (e) {
            $('#cartTotalCount').html(e.TotalItems);
            $('#cartTotalSum').html(e.TotalPrice);
        });
    });
    $('#addAndPurchase').on('click', function (e) {
        var quantity = $('#count').val();
        var pid = $('#pid').val();
        addToCart(quantity, pid, function () {
            window.location = "/order";
        });
    });

    function onCountChanged() {
        var input = $('#count');
        var quantity = input.val();
        var valid = isInputValid(quantity);
        if (valid) {
            var val = $('#originalPrice').val();
            var value = calculateItemPrice(val, quantity);
            input.parents('div').removeClass('error');
            $('#addToCartBtn').removeClass('disabled');
            $('#addAndPurchase').removeClass('disabled');
            var formatvalue = value + " грн.";
            $('#price').text(formatvalue);
        }
        else {
            $('#addToCartBtn').addClass('disabled');
            $('#addAndPurchase').addClass('disabled');
            input.parents('div').addClass('error');
        }
    }

    $('#count').on('change', function () {
        onCountChanged();
    });

    $('#count').keyup(function () {
        onCountChanged();
    });
    $('#addToCartModal').on('show', function () {
        $('#count').val("1");
        $('#count').parents('div').removeClass('error');
        $('#addToCartBtn').removeClass('disabled');
        $('#addAndPurchase').removeClass('disabled');        
        var val = $('#originalPrice').val();
        var formatvalue = val + " грн.";
        $('#price').text(formatvalue);
    });
});