const uri = 'api/vault';
let logs = null;
function getCount(data) {
    const el = $('#counter');
    let name = 'log';
    if (data) {
        if (data > 1) {
            name = 'logs';
        }
        el.text(data + ' ' + name);
    } else {
        el.html('No ' + name);
    }
}

$(document).ready(function () {
    getData();
});

function getData() {
    $.ajax({
        type: 'GET',
        url: uri,
        success: function (data) {
            $('#logs').empty();
            getCount(data.length);
            $.each(data, function (key, item) {

                $('<tr><td>' + item.description + '</td>' +
                    '<td>' + item.date + '</td>' +
                    '<td><button onclick="editItem(' + item.id + ')">Edit</button></td>' +
                    '<td><button onclick="deleteItem(' + item.id + ')">Delete</button></td>' +
                    '</tr>').appendTo($('#logs'));
            });

            logs = data;
        }
    });
}

function addItem() {
    const item = {
        'description': $('#add-description').val(),
        'isComplete': false
    };

    $.ajax({
        type: 'POST',
        accepts: 'application/json',
        url: uri,
        contentType: 'application/json',
        data: JSON.stringify(item),
        error: function (jqXHR, textStatus, errorThrown) {
            alert('here');
        },
        success: function (result) {
            getData();
            $('#add-description').val('');
        }
    });
}

function deleteItem(id) {
    $.ajax({
        url: uri + '/' + id,
        type: 'DELETE',
        success: function (result) {
            getData();
        }
    });
}

function editItem(id) {
    $.each(logs, function (key, item) {
        if (item.id === id) {
            $('#edit-description').val(item.description);
            $('#edit-id').val(item.id);
            $('#edit-isComplete').val(item.isComplete);
        }
    });
    $('#spoiler').css({ 'display': 'block' });
}

$('.my-form').on('submit', function () {
    const item = {
        'description': $('#edit-description').val(),
        'id': $('#edit-id').val()
    };

    $.ajax({
        url: uri + '/' + $('#edit-id').val(),
        type: 'PUT',
        accepts: 'application/json',
        contentType: 'application/json',
        data: JSON.stringify(item),
        success: function (result) {
            getData();
        }
    });

    closeInput();
    return false;
});

function closeInput() {
    $('#spoiler').css({ 'display': 'none' });
}