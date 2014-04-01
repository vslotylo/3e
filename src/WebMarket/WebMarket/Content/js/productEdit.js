$(document).ready(function () {
    function generateNewId(id) {
        var splits = id.split('_');
        var newId = parseInt(splits[1]) + 1;
        return id.replace(splits[1], newId);
    }

    function generateNewName(name) {
        var start = name.indexOf('[');
        var end = name.indexOf(']');
        var oldId = name.substring(start + 1, end);
        var newId = parseInt(oldId) + 1;
        return name.replace(oldId, newId);
    }

    function createTd(input) {
        var newId = generateNewId(input.id);
        var newName = generateNewName(input.name);
        return '<td><input type="text" id="' + newId + '" name="' + newName + '" class="form-control" /></td>';
    }

    $('#btnAddNewDynamicProperty').on('click', function () {
        $('#addDynamicPropertyDialog').modal('hide');
        var inputs = $('#tableDynamicProperties > tbody > tr:last > td > input');
        var tdKey = createTd(inputs[0]);
        var tdValue = createTd(inputs[1]);
        $('#tableDynamicProperties').append('<tr>' + tdKey + tdValue + '</tr>');
    });
});