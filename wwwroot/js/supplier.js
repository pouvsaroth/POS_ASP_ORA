$(document).ready(function () {

    var supplierTable = $('#supplierTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true
    });

    // ✅ SELECT ALL
    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });


    // ✅ DELETE SINGLE
    $(document).on('click', '.delete-btn', function () {

        var id = $(this).data('id');

        if (confirm('Are you sure you want to delete this supplier?')) {

            $.ajax({
                url: '/Supplier/Delete/' + id,
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


    // ✅ DELETE SELECTED
    $('#deleteSelected').on('click', function () {

        var selectedIds = [];

        $('.row-checkbox:checked').each(function () {
            selectedIds.push(parseInt($(this).val()));
        });

        if (selectedIds.length === 0) {
            alert('Please select at least one supplier.');
            return;
        }

        if (!confirm('Are you sure you want to delete selected suppliers?'))
            return;

        $.ajax({
            url: '/Supplier/DeleteSelected',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(selectedIds),

            success: function (response) {
                alert(response.message);
                location.reload();
            },

            error: function () {
                alert('Delete failed.');
            }
        });
    });


    // ✅ SEARCH
    $('#searchBar').on('keyup', function () {

        var value = $(this).val().toLowerCase();

        $('#supplierTable tbody tr').filter(function () {

            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);

        });

    });


    // ✅ ADD BUTTON
    $('#addSupplierBtn').on('click', function () {

        $('#supplierModalTitle').text('Add Supplier');

        $('#supplierForm').attr('action', '/Supplier/Create');

        $('#supplierForm')[0].reset();

        $('#supplierId').val('');

    });


    // ✅ EDIT BUTTON
    $(document).on('click', '.edit-btn', function () {

        var id = $(this).data('id');
        var name = $(this).data('name');
        var sex = $(this).data('sex');
        var phone = $(this).data('phone');
        var email = $(this).data('email');
        var address = $(this).data('address');

        $('#supplierModalTitle').text('Edit Supplier');

        $('#supplierForm').attr('action', '/Supplier/Update');

        $('#supplierId').val(id);
        $('#supplierName').val(name);
        $('#sex').val(sex);
        $('#phone').val(phone);
        $('#email').val(email);
        $('#address').val(address);

        $('#supplierModal').modal('show');

    });

});