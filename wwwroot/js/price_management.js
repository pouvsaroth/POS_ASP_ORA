$(document).ready(function () {

    var priceTable = $('#priceTable').DataTable({
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

        if (confirm('Are you sure you want to delete this price?')) {

            $.ajax({
                url: '/PriceManagement/Delete/' + id,
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
            alert('Please select at least one record.');
            return;
        }

        if (!confirm('Are you sure you want to delete selected records?'))
            return;

        $.ajax({
            url: '/PriceManagement/DeleteSelected',
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
        $('#priceTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });


    // ✅ ADD BUTTON
    $('#addBtn').on('click', function () {

        $('#priceModalTitle').text('Add Product Price');

        $('#priceForm').attr('action', '/PriceManagement/Create');

        $('#priceForm')[0].reset();

        $('#priceId').val('');

        $('#ProductId').val(null).trigger('change');
        $('#CurrencyId').val(null).trigger('change');
    });


    // ✅ EDIT BUTTON
    $(document).on('click', '.edit-btn', function () {

        var id = $(this).data('id');
        var product = $(this).data('product');
        var price = $(this).data('price');
        var currency = $(this).data('currency');
        var changedby = $(this).data('changedby');
        var remark = $(this).data('remark');

        $('#priceModalTitle').text('Edit Product Price');

        $('#priceForm').attr('action', '/PriceManagement/Update');

        $('#priceId').val(id);
        $('#SalePrice').val(price);
        $('#ChangedBy').val(changedby);
        $('#Remark').val(remark);

        $('#ProductId').val(product).trigger('change');
        $('#CurrencyId').val(currency).trigger('change');

        $('#priceModal').modal('show');
    });


    // =========================
    // SELECT2 PRODUCT
    // =========================
    $('#ProductId').select2({
        dropdownParent: $('#priceModal'),
        width: '100%',
        placeholder: "Select product",
        allowClear: true
    });

    site_loadDataDropDown('#ProductId', '/Base/GetProductlistDropDown',1);

    


    // =========================
    // SELECT2 CURRENCY
    // =========================
    $('#CurrencyId').select2({
        dropdownParent: $('#priceModal'),
        width: '100%',
        placeholder: "Select currency",
        allowClear: true
    });

    site_loadDataDropDown('#CurrencyId', '/Base/GetCurrencyDropDown',2);


});