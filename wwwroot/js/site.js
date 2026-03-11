// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#signInForm').on('submit', function (e) {
        e.preventDefault();

        const formData = $(this).serialize();

        $.ajax({
            url: '/Authentication/Login',
            type: 'POST',
            data: formData,
            success: function (response) {
                if (response.success) {
                    alert(response.message);
                    location.reload(); // Reload the page after successful login
                } else {
                    alert(response.message);
                }
            },
            error: function () {
                alert('An error occurred while processing your request.');
            }
        });
    });
});
