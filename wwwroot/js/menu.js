$(document).ready(function () {

    if ($.fn.DataTable.isDataTable('#menuTable')) {
        $('#menuTable').DataTable().destroy();
    }

    var menuTable = $('#menuTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true,
        columnDefs: [
            {
                targets: 1,
                visible: false,
                searchable: false
            }
        ]
    });

    // SELECT ALL
    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });

    // DELETE SINGLE
    $('#menuTable').on('click', '.delete-btn', function () {
        var id = $(this).data('id');

        if (confirm('Are you sure you want to delete this menu?')) {
            $.ajax({
                url: '/Menu/Delete/' + id,
                type: 'POST',
                success: function () {
                    location.reload();
                },
                error: function () {
                    alert('Delete failed.');
                }
            });
        }
    });

    $('#deleteSelected').on('click', function () {
        deleteSelectedItems({
            table: menuTable,
            url: '/Menu/DeleteSelected'
        });
    });

    // SEARCH
    $('#searchBar').on('keyup', function () {
        menuTable.search(this.value).draw();
    });

    // ADD BUTTON
    $('#btnAddMenu').on('click', function () {

        $('#menuModalTitle').text('Add Menu');
        $('#menuForm').attr('action', '/Menu/Create');

        $('#menuId').val('');
        $('#menuName').val('');
        $('#controllerName').val('');
        $('#actionName').val('');
        $('#icon').val('');
        $('#displayOrder').val
        $('#parentId').val(''); 

    });

    // EDIT BUTTON
    $('#menuTable').on('click', '.edit-btn', function () {

        var id = $(this).data('id');
        var name = $(this).data('name');
        var controller = $(this).data('controller');
        var action = $(this).data('action');
        var icon = $(this).data('icon');
        var order = $(this).data('order');

        $('#menuModalTitle').text('Edit Menu');
        $('#menuForm').attr('action', '/Menu/Update');

        $('#parentId').val($(this).data('parent') || '').trigger('change');
        $('#menuId').val(id);
        $('#menuName').val(name);
        $('#controllerName').val(controller);
        $('#actionName').val(action);
        $('#icon').val(icon);
        $('#displayOrder').val(order);

        $('#menuModal').modal('show');
    });

    $('#parentId').select2({
        placeholder: "-- Select Parent Menu --",
        allowClear: true,
        width: '100%',
        dropdownParent: $('#menuModal') // 🔥 IMPORTANT for modal
    });

});