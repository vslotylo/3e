var app = {};
app.filters = [];
app.initListView = function () {
    $('#filterButton').on('click', function (e) {        
        window.location = app.filters.current();
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
                    app.filters[i].items.push(value);
                }
                continue;
            }
        }
    });

    var val = "";
    var comma = "%3B";
    var isFirst = true;
    for (var i = 0; i < app.filters.length; i++) {
        if (app.filters[i].items.length > 0) {
            if (isFirst) {
                val += "?";
                isFirst = false;
            }
            else {
                val += "&";
            }
            val += app.filters[i].key + "=";
        }
        else {
            continue;
        }
        for (var j = 0; j < app.filters[i].items.length; j++) {
            val += app.filters[i].items[j];
            if (j != app.filters[i].items.length - 1) {
                val += comma;
            }
        }
    }
    return val;
};



