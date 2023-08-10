"use strict";
var initTable = function () {
    var table = $('#table');

    table.DataTable({
        responsive: true,
        pagingType: 'full_numbers',
    });

    table.on('change', '.kt-group-checkable', function () {
        var set = $(this).closest('table').find('td:first-child .kt-checkable');
        var checked = $(this).is(':checked');

        $(set).each(function () {
            if (checked) {
                $(this).prop('checked', true);
                $(this).closest('tr').addClass('active');
            }
            else {
                $(this).prop('checked', false);
                $(this).closest('tr').removeClass('active');
            }
        });
    });

    table.on('change', 'tbody tr .kt-checkbox', function () {
        $(this).parents('tr').toggleClass('active');
    });

    table.on('change', '.kt-group-checkable, .kt-checkable', function () {
        var ids = [];

        $("input:checkbox[class='kt-checkable']:checked").each(function () {
            ids.push($(this).val());
        });

        var count = ids.length;
        $('#selected_number').html(count);
        if (count > 0) { $('#group_action_form').collapse('show'); }
        else { $('#group_action_form').collapse('hide'); }
        $('#selected_ids').val(ids);
    });
};

initTable();