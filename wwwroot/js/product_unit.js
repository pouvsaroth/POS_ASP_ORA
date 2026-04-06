$(document).ready(function () {

    var unitTable = $('#unitTable').DataTable({
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

        if (confirm('Are you sure you want to delete this unit?')) {

            $.ajax({
                url: '/ProductUnit/Delete/' + id,
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
            alert('Please select at least one unit.');
            return;
        }

        if (!confirm('Are you sure you want to delete selected units?'))
            return;

        $.ajax({
            url: '/ProductUnit/DeleteSelected',
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
        $('#unitTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });


    // ✅ ADD BUTTON
    $('#addBtn').on('click', function () {

        $('#unitModalTitle').text('Add Unit');

        $('#unitForm').attr('action', '/ProductUnit/Create');

        $('#unitForm')[0].reset();

        $('#unitId').val('');
    });


    // ✅ EDIT BUTTON
    $(document).on('click', '.edit-btn', function () {

        var id = $(this).data('id');
        var name = $(this).data('name');
        
        
        var qty = $(this).data('qty');
        var remark = $(this).data('remark');

        $('#unitModalTitle').text('Edit Unit');

        $('#unitForm').attr('action', '/ProductUnit/Update');

        $('#unitId').val(id);
        $('#unitName').val(name);
        $('#remark').val(remark);
        $('#UnitTypeId').val($(this).data('type')).trigger('change');
        $('#qtyPerUnit').val(qty);
        

        $('#unitModal').modal('show');
    });

    // =========================
    // SELECT2 CATEGORY
    // =========================
    $('#UnitTypeId').select2({
        dropdownParent: $('#unitModal'),
        width: '100%',
        placeholder: "Select unit type",
        allowClear: true
    });

    loadUnitTypes();

    function loadUnitTypes() {
        let $select = $('#UnitTypeId');

        $.get('/ProductUnit/GetUnitTypes', function (data) {
            $select.empty();

            // 🔥 Keep empty option
            $select.append('<option value=""></option>');

            data.forEach(c => {
                $select.append(
                    `<option value="${c.id}">${c.unitTypeName}</option>`
                );
            });

            // 🔥 Reset value
            $select.val(null).trigger('change');
        });
    }
   

});