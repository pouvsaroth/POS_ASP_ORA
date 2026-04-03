
var imageBasePath = '/@Configuration["FileUpload:ProductImagePath"]/';

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
        width: '100%',
        placeholder: "Select category",
        allowClear: true
    });

    loadCategories();

    function loadCategories() {  
        let $select = $('#categoryId');

        $.get('/ProductList/GetCategories', function (data) {
            $select.empty();

            // 🔥 Keep empty option
            $select.append(`<option value=""></option>`);

            data.forEach(c => {
                $select.append(
                    `<option value="${c.id}">${c.categoryName}</option>`
                );
            });

            // 🔥 Reset value
            $select.val(null).trigger('change');
        });
    }

    // =========================
    // SELECT2 CATEGORY
    // =========================
    $('#supplierId').select2({
        dropdownParent: $('#productModal'),
        width: '100%',
        placeholder: "Select supplier",
        allowClear: true
    });

    loadSupplier();

    function loadSupplier() {
        let $select = $('#supplierId');

        $.get('/ProductList/GetSupplier', function (data) {
            $select.empty();

            // 🔥 Keep empty option
            $select.append(`<option value=""></option>`);

            data.forEach(c => {
                $select.append(
                    `<option value="${c.id}">${c.supplierName}</option>`
                );
            });

            // 🔥 Reset value
            $select.val(null).trigger('change');
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
        resetForm();
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
        $('#qtyAlert').val($(this).data('qtyalert'));
        $('#categoryId').val($(this).data('category')).trigger('change');
        $('#supplierId').val($(this).data('supplier')).trigger('change');
        $('#description').val($(this).data('description')); 
        $('#oldImageName').val($(this).data('imagename'));
        let status = $(this).data('status');
        $('#statusCheck').prop('checked', status === 1);

        // 🔥 IMAGE PART
        let fileName = $(this).data('imagename');
        var imageBasePath = $('#imageBasePath').val();
        const preview = document.getElementById("previewImage");
        const placeholder = document.getElementById("placeholderText");

        if (fileName) {
            preview.src = imageBasePath + fileName; // ✅ combine
            preview.style.display = "block";
            placeholder.style.display = "none";
        } else {
            preview.style.display = "none";
            placeholder.style.display = "block";
        }

        $('#productModal').modal('show');
    });

    // =========================
    // SAVE (AJAX INSERT/UPDATE)
    // =========================
    $('#productForm').on('submit', function (e) {
        e.preventDefault();

        var form = document.getElementById('productForm');
        var formData = new FormData(form); // 🔥 IMPORTANT

        var id = $('#productId').val();
        var url = id ? '/ProductList/Update' : '/ProductList/Create';

        $.ajax({
            url: url,
            type: 'POST',
            data: formData,
            processData: false,   // 🔥 REQUIRED
            contentType: false,   // 🔥 REQUIRED
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
        var ImageNames = [];
        $('.row-checkbox:checked').each(function () {
            ids.push($(this).val());
            ImageNames.push($(this).data('imagename'));
        });

        if (ids.length === 0) {
            showToast("Please select at least one product", true);
            return;
        }

        if (!confirm('Are you sure to delete?')) return;

        $.ajax({
            url: '/ProductList/DeleteMultiple',
            type: 'POST',
            data: { ids: ids, ImageNames: ImageNames },
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
function resetForm() {
    $('#productForm')[0].reset();
    $('#previewImage').hide();
    $('#placeholderText').show();
}
//Handle Missing Image
