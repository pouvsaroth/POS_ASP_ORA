$(document).ready(function () {

    var menuTable = $('#menuTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true
    });

    // SELECT ALL
    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });

    // DELETE SINGLE
    $('.delete-btn').on('click', function () {
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

    // DELETE SELECTED
    $('#deleteSelected').on('click', function () {

        var selectedIds = [];

        $('.row-checkbox:checked').each(function () {
            selectedIds.push(parseInt($(this).val()));
        });

        if (selectedIds.length === 0) {
            showWarning('Please select at least one menu to delete.');
            return;
        }

        confirmDelete(function () {

            $.ajax({
                url: '/Menu/DeleteSelected',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(selectedIds),

                success: function (response) {

                    Swal.fire({
                        icon: 'success',
                        title: 'Deleted',
                        text: response.message
                    }).then(() => {
                        location.reload();
                    });

                },

                error: function () {
                    showError('Failed to delete menus.');
                }

            });

        });

    });

    // SEARCH
    $('#searchBar').on('keyup', function () {
        var value = $(this).val().toLowerCase();

        $('#menuTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
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
        $('#displayOrder').val('');

    });

    // EDIT BUTTON
    $('.edit-btn').on('click', function () {

        var id = $(this).data('id');
        var name = $(this).data('name');
        var controller = $(this).data('controller');
        var action = $(this).data('action');
        var icon = $(this).data('icon');
        var order = $(this).data('order');

        $('#menuModalTitle').text('Edit Menu');
        $('#menuForm').attr('action', '/Menu/Update');

        $('#menuId').val(id);
        $('#menuName').val(name);
        $('#controllerName').val(controller);
        $('#actionName').val(action);
        $('#icon').val(icon);
        $('#displayOrder').val(order);

        $('#menuModal').modal('show');
    });

});