$(document).ready(function () {

    var productTable = $('#productTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true
    });

    // =========================
    // SELECT ALL
    // =========================
    $('#selectAll').on('change', function () {
        $('.row-checkbox').prop('checked', this.checked);
    });

    // =========================
    // SEARCH
    // =========================
    $('#searchBar').on('keyup', function () {
        var value = $(this).val().toLowerCase();
        $('#productTable tbody tr').filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });

    // =========================
    // SELECT2 CATEGORY
    // =========================
    $('#categoryId').select2({
        dropdownParent: $('#productModal'),
        width: '100%'
    });

    loadCategories();

    function loadCategories() {
        $.get('/ProductList/GetCategories', function (data) {
            $('#categoryId').empty();
            data.forEach(c => {
                $('#categoryId').append(`<option value="${c.id}">${c.categoryName}</option>`);
            });
        });
    }

    // =========================
    // SELECT2 CATEGORY
    // =========================
    $('#supplierId').select2({
        dropdownParent: $('#productModal'),
        width: '100%'
    });

    loadSupplier();

    function loadSupplier() {
        $.get('/ProductList/GetSupplier', function (data) {
            $('#supplierId').empty();
            data.forEach(c => {
                $('#supplierId').append(`<option value="${c.id}">${c.supplierName}</option>`);
            });
        });
    }

    // =========================
    // ADD
    // =========================
    $('#btnAddProduct').on('click', function () {
        $('#productModalTitle').text('Add Product');
        $('#productForm')[0].reset();
        $('#productId').val('');
        $('#categoryId').val(null).trigger('change');
        $('#supplierId').val(null).trigger('change');
        $('#statusCheck').prop('checked', true);
    });

    // =========================
    // EDIT
    // =========================
    $('.edit-btn').on('click', function () {

        $('#productModalTitle').text('Edit Product');

        $('#productId').val($(this).data('id'));
        $('#productCode').val($(this).data('code'));
        $('#barcode').val($(this).data('barcode'));
        $('#productName').val($(this).data('name'));
        $('#productNameKh').val($(this).data('namekh'));
        $('#categoryId').val($(this).data('category')).trigger('change');

        let status = $(this).data('status');
        $('#statusCheck').prop('checked', status === 1);

        $('#productModal').modal('show');
    });

    // =========================
    // SAVE (AJAX INSERT/UPDATE)
    // =========================
    $('#productForm').on('submit', function (e) {
        e.preventDefault();

        var formData = {
            Id: $('#productId').val(),
            ProductCode: $('#productCode').val(),
            Barcode: $('#barcode').val(),
            ProductName: $('#productName').val(),
            ProductNameKh: $('#productNameKh').val(),
            CategoryId: $('#categoryId').val(),
            SupplierId: $('#supplierId').val(),
            Status: $('#statusCheck').is(':checked') ? 1 : 0
        };

        var url = formData.Id ? '/ProductList/Update' : '/ProductList/Create';

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            success: function (res) {
                showToast(res);

                if (res.includes("success")) {
                    $('#productModal').modal('hide');
                    setTimeout(() => location.reload(), 800);
                }
            },
            error: function () {
                showToast("Something went wrong!", true);
            }
        });
    });

    // =========================
    // DELETE MULTIPLE (AJAX)
    // =========================
    $('#deleteSelected').on('click', function () {

        var ids = [];
        $('.row-checkbox:checked').each(function () {
            ids.push($(this).val());
        });

        if (ids.length === 0) {
            showToast("Please select at least one product", true);
            return;
        }

        if (!confirm('Are you sure to delete?')) return;

        $.ajax({
            url: '/ProductList/DeleteMultiple',
            type: 'POST',
            data: { ids: ids },
            success: function (res) {
                showToast(res);
                setTimeout(() => location.reload(), 800);
            }
        });
    });

    // =========================
    // TOAST FUNCTION 🔥
    // =========================
    function showToast(message, isError = false) {
        var bg = isError ? 'bg-danger' : 'bg-success';

        var toast = $(`
            <div class="toast align-items-center text-white ${bg} border-0 position-fixed bottom-0 end-0 m-3" role="alert">
                <div class="d-flex">
                    <div class="toast-body">${message}</div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                </div>
            </div>
        `);

        $('body').append(toast);
        new bootstrap.Toast(toast[0]).show();

        setTimeout(() => toast.remove(), 3000);
    }

});

document.addEventListener("DOMContentLoaded", function () {

    const input = document.getElementById("imageInput");

    if (!input) {
        console.error("imageInput not found");
        return;
    }

    input.addEventListener("change", function () {

        const file = this.files[0];
        const preview = document.getElementById("previewImage");
        const placeholder = document.getElementById("placeholderText");

        console.log("FILE:", file); // debug

        if (file) {
            const reader = new FileReader();

            reader.onload = function (e) {
                preview.src = e.target.result;
                preview.style.display = "block";
                placeholder.style.display = "none";
            };

            reader.readAsDataURL(file);
        }
    });

});
