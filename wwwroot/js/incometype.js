$(document).ready(function () {
    var incometypeTable = $('#incometypeTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true
    });

    // Select all rows
    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });

    // Delete selected rows
    $('#deleteSelected').on('click', function () {
        var ids = $('.row-checkbox:checked').map(function () { return this.value; }).get();
        if (ids.length === 0) {
            alert("Please select at least one IncomeType.");
            return;
        }

        if (confirm("Are you sure you want to delete selected items?")) {
            $.ajax({
                url: '/IncomeType/DeleteSelected',
                type: 'POST',
                data: { ids: ids },
                traditional: true,
                success: function () { location.reload(); },
                error: function () { alert('Delete failed'); }
            });
        }
    });

    // Search
    $('#searchBar').on('keyup', function () {
        var value = $(this).val().toLowerCase();
        $('#incometypeTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    // Add IncomeType
    $('#btnAddIncomeType').on('click', function () {
        $('#incometypeModalTitle').text('Add IncomeType');
        $('#incometypeForm').attr('action', '/IncomeType/Create');
        $('#incometypeId').val('');
        $('#incometypeName').val('');
        $('#statusCheck').prop('checked', true);
    });

    // Edit IncomeType
    $('.edit-btn').on('click', function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        var status = $(this).data('status');

        $('#incometypeModalTitle').text('Edit IncomeType');
        $('#incometypeForm').attr('action', '/IncomeType/Update');
        $('#incometypeId').val(id);
        $('#incometypeName').val(name);
        $('#statusCheck').prop('checked', status === 1);

        $('#incometypeModal').modal('show');
    });
});