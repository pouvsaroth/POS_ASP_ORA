$(document).ready(function () {

    var table = $('#unittypeTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true
    });

    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });

    $('#deleteSelected').click(function () {

        var ids = $('.row-checkbox:checked').map(function () {
            return this.value;
        }).get();

        if (ids.length === 0) {
            alert("Please select at least one item.");
            return;
        }

        $.ajax({
            url: '/UnitType/DeleteSelected',
            type: 'POST',
            data: { ids: ids },
            traditional: true,
            success: function () {
                location.reload();
            }
        });

    });

    $('#searchBar').keyup(function () {
        var value = $(this).val().toLowerCase();
        $("#unittypeTable tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $('#btnAddUnitType').click(function () {

        $('#unittypeModalTitle').text("Add UnitType");

        $('#unittypeForm').attr('action', '/UnitType/Create');

        $('#unittypeId').val('');
        $('#unittypeName').val('');
        $('#statusCheck').prop('checked', true);

    });

    $('.edit-btn').click(function () {

        var id = $(this).data('id');
        var name = $(this).data('name');
        var status = $(this).data('status');

        $('#unittypeModalTitle').text("Edit UnitType");

        $('#unittypeForm').attr('action', '/UnitType/Update');

        $('#unittypeId').val(id);
        $('#unittypeName').val(name);
        $('#statusCheck').prop('checked', status == 1);

        $('#unittypeModal').modal('show');
    });

});