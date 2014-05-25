var app = {};
app.filters = [];
app.initListView = function () {
    $('#filterButton').on('click', function () {
        var subPath = app.filters.current();
        var url = window.location.origin;
        var path = window.location.pathname;
        if (path.indexOf('/') == 0) {
            path = path.substring(1, path.length);
        }

        var controller = path.substring(0, path.indexOf('/'));
        if (controller != '') {
            url += "/" + controller;
        } else {
            url = path;
        }

        if (subPath != '') {
            if (subPath.indexOf('?') != 0) {
                url += "/";
            }
            url += subPath;
        }

        window.location = url;
    });
};
app.filters.current = function () {
    var list = $('.entityFilter');
    for (var i = 0; i < list.length; i++) {
        var key = list[i].id;
        app.filters[i] = {};
        app.filters[i].key = key;
        app.filters[i].items = [];
    }
    $('.entityFilterContainer').find('[type=checkbox]').each(function () {
        var key = $(this).parents('.entityFilter').attr("id");
        for (var i = 0; i < list.length; i++) {
            if (key === app.filters[i].key) {
                var value = $(this).siblings(':hidden.filterValue').val();
                if ($(this).is(':checked') == true) {
                    app.filters[i].items.push(value.toLowerCase());
                }
                continue;
            }
        }
    });

    var val = "";
    var separator = "-";
    var isFirst = true;
    for (var i = 0; i < app.filters.length; i++) {
        if (app.filters[i].items.length > 0) {
            if (app.filters[i].key != 'producers') {
                if (isFirst) {
                    val += "?";
                    isFirst = false;
                }
                else {
                    val += "&";
                }
                val += app.filters[i].key + "=";
            }
        }
        else {
            continue;
        }
        for (var j = 0; j < app.filters[i].items.length; j++) {
            if (val != '' && val.lastIndexOf('=') != val.length-1) {
                val += separator;
            }
            val += app.filters[i].items[j];
        }
    }
    return val;
};

$(document).ready(function () {
    app.initListView();
});