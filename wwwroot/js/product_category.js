

$(document).ready(function () {
    var categoryTable = $('#categoryTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true
    });

    // Handle select all
    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });

    // Handle delete button click
    $('.delete-btn').on('click', function () {
        var id = $(this).data('id');
        if (confirm('Are you sure you want to delete this category?')) {
            $.ajax({
                url: '/ProductCategory/Delete/' + id,
                type: 'POST',
                success: function (result) {
                    location.reload();
                },
                error: function () {
                    alert('Delete failed.');
                }
            });
        }
    });

    // Handle delete selected 
    $('#deleteSelected').on('click', function () {
        deleteSelectedItems({
            table: categoryTable,
            url: '/ProductCategory/DeleteSelected'
        });  
    });


    // Handle search
    $('#searchBar').on('keyup', function () {
        var value = $(this).val().toLowerCase();
        $('#categoryTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    // Handle Add button click
    $('#btnAddCategory').on('click', function () {
        // Reset the modal for Add operation
        $('#categoryModalTitle').text('Add Category');
        $('#categoryForm').attr('action', '/ProductCategory/Create');
        $('#categoryId').val('');
        $('#categoryName').val('');
        $('#statusCheck').prop('checked', true);
    });

    // Handle Edit button click
    $('.edit-btn').on('click', function () {
        // Get data attributes from the clicked button
        var id = $(this).data('id');
        var name = $(this).data('name');
        var status = $(this).data('status');

        // Populate the modal fields with the category data
        $('#categoryModalTitle').text('Edit Category');
        $('#categoryForm').attr('action', '/ProductCategory/Update');
        $('#categoryId').val(id);
        $('#categoryName').val(name);
        $('#statusCheck').prop('checked', status === 1);

        // Show the modal
        $('#categoryModal').modal('show');
    });
});