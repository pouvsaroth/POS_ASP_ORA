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