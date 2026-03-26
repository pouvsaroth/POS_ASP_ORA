$(document).ready(function () {

    var companyTable = $('#companyTable').DataTable({
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

        if (confirm('Are you sure you want to delete this company?')) {

            $.ajax({
                url: '/Company/Delete/' + id,
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


    // ✅ DELETE SELECTED
    $('#deleteSelected').on('click', function () {

        var selectedIds = [];

        $('.row-checkbox:checked').each(function () {
            selectedIds.push(parseInt($(this).val()));
        });

        if (selectedIds.length === 0) {
            alert('Please select at least one company.');
            return;
        }

        if (!confirm('Are you sure you want to delete selected companies?'))
            return;

        $.ajax({
            url: '/Company/DeleteSelected',
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
        $('#companyTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });


    // ✅ ADD BUTTON
    $('#addCompanyBtn').on('click', function () {

        $('#companyModalTitle').text('Add Company');

        $('#companyForm').attr('action', '/Company/Create');

        $('#companyForm')[0].reset();

        $('#companyId').val('');
    });


    // ✅ EDIT BUTTON
    $(document).on('click', '.edit-btn', function () {

        var id = $(this).data('id');
        var name = $(this).data('name');
        var location = $(this).data('location');
        var phone = $(this).data('phone');
        var remark = $(this).data('remark');

        $('#companyModalTitle').text('Edit Company');

        $('#companyForm').attr('action', '/Company/Update');

        $('#companyId').val(id);
        $('#companyName').val(name);
        $('#location').val(location);
        $('#phone').val(phone);
        $('#remark').val(remark);

        $('#companyModal').modal('show');
    });

});