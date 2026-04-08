// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function switchToLoginModal() {
    // Close the register modal
    const registerModal = bootstrap.Modal.getInstance(document.getElementById('registerModal'));
    registerModal.hide();

    // Wait for the register modal to close, then show the login modal
    registerModal._element.addEventListener('hidden.bs.modal', function () {
        const loginModal = new bootstrap.Modal(document.getElementById('loginModal'));
        loginModal.show();
    }, { once: true });
}

//delete selected item
function deleteSelectedItems(options) {

    var table = options.table;
    var url = options.url;
    var confirmMessage = options.confirmMessage || 'Are you sure you want to delete selected items?';

    var selectedIds = [];

    // ✅ Get all selected rows (DataTable safe)
    table.$('.row-checkbox:checked').each(function () {
        selectedIds.push(parseInt($(this).val()));
    });

    if (selectedIds.length === 0) {
        showWarning('Please select at least one item.');
        return;
    }

    confirmDelete(function () {

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(selectedIds),

            success: function (response) {
                Swal.fire({
                    icon: 'success',
                    title: 'Deleted',
                    text: response.message || 'Deleted successfully'
                }).then(() => {
                    location.reload();
                });
            },

            error: function () {
                showError('Failed to delete items.');
            }

        });

    });
}
function site_loadDataDropDown(idDropdown,url) {
    let $select = $(idDropdown);

    $.get(url, function (data) {
        $select.empty();

        // 🔥 Keep empty option
        $select.append(`<option value=""></option>`);

        data.forEach(c => {
            $select.append(
                `<option value="${c.id}">${c.name}</option>`
            );
        });

        // 🔥 Reset value
        $select.val(null).trigger('change');
    });
}

function site_loadDataDropDownTable($select, url) {

    $.get(url, function (data) {

        $select.empty();

        // empty option
        $select.append(`<option value=""></option>`);

        data.forEach(c => {
            $select.append(
                `<option value="${c.id}">${c.name}</option>`
            );
        });

    });
}
function setTodayDateYYYYMMDD() {
    let today = new Date();

    let yyyy = today.getFullYear();
    let mm = String(today.getMonth() + 1).padStart(2, '0');
    let dd = String(today.getDate()).padStart(2, '0');

    let formatted = `${yyyy}-${mm}-${dd}`; // required for input type=date

    return formatted;
}
function setTodayDateDDMMYYYY() {
    let today = new Date();

    let dd = String(today.getDate()).padStart(2, '0');
    let mm = String(today.getMonth() + 1).padStart(2, '0');
    let yyyy = today.getFullYear();

    let formatted = `${dd}-${mm}-${yyyy}`;

    return formatted;
}