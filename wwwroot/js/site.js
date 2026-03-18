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
