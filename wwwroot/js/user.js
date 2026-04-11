$(document).ready(function () {

    var userTable = $('#userTable').DataTable({

        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true

    });


    // SEARCH

    $('#searchBar').keyup(function () {

        userTable.search(this.value).draw();

    });


    // SELECT ALL

    $('#selectAll').change(function () {

        var rows = userTable.rows({ 'search': 'applied' }).nodes();

        $('input.row-checkbox', rows).prop('checked', this.checked);

    });


    // ADD USER

    $('#addUserBtn').click(function () {

        $('#userModalTitle').text("Add User");

        $('#userForm').attr('action', '/User/Create');

        $('#userForm')[0].reset();

        $('#userId').val('');

        $('#isActive').prop('checked', true);

    });


    // EDIT USER

    $(document).on('click', '.edit-btn', function () {

        var id = $(this).data('id');

        var username = $(this).data('username');

        var email = $(this).data('email');

        var active = $(this).data('active');

        $('#userModalTitle').text("Edit User");

        $('#userForm').attr('action', '/User/Update');

        $('#userId').val(id);

        $('#username').val(username);

        $('#email').val(email);

        $('#password').val('');

        $('#confirmPassword').val('');

        $('#isActive').prop('checked', active);

    });


    // PASSWORD VALIDATION

    $('#userForm').submit(function (e) {

        var pwd = $('#password').val();

        var confirm = $('#confirmPassword').val();

        if (pwd !== confirm) {

            alert("Passwords do not match");

            e.preventDefault();

        }

    });


    // DELETE SELECTED

    $('#deleteSelected').click(function () {

        var selectedIds = [];

        $('.row-checkbox:checked').each(function () {

            selectedIds.push($(this).val());

        });

        if (selectedIds.length === 0) {

            alert("Please select users");

            return;

        }

        if (!confirm("Delete selected users?")) return;

        $.ajax({

            url: '/User/DeleteSelected',

            type: 'POST',

            contentType: 'application/json',

            data: JSON.stringify(selectedIds),

            success: function (res) {

                alert(res.message);

                location.reload();

            },

            error: function () {

                alert("Delete failed");

            }

        });

    });

});