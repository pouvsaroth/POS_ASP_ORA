let selectedGroup = 0;

// ==========================
// ✅ GROUP CLICK
// ==========================
$(document).on('click', '.group-item', function () {

    $('.group-item').removeClass('active');
    $(this).addClass('active');

    selectedGroup = $(this).data('id');
    $('#groupId').val(selectedGroup);

    loadMenus(selectedGroup);
});


// ==========================
// ✅ LOAD MENUS
// ==========================
function loadMenus(groupId) {

    $.get('/Right/GetMenus?groupId=' + groupId, function (data) {

        data = data.map(x => ({
            menuId: Number(x.menuId),
            parentId: x.parentId == null || x.parentId === "" ? null : Number(x.parentId),
            displayOrder: Number(x.displayOrder) || 0,
            menuName: x.menuName,
            isSelected: x.isSelected
        }));

        // ✅🔥 ADD THIS GLOBAL SORT (VERY IMPORTANT)
        data.sort((a, b) => {
            if (a.parentId === b.parentId) {
                return a.displayOrder - b.displayOrder;
            }
            return (a.parentId || 0) - (b.parentId || 0);
        });

        let html = `
        <div>
            <input type="checkbox" id="selectAllMenu" />
            <b>Select All</b>
        </div>
    `;

        html += buildMenuTree(data, null, 0);

        $('#menuContainer').html(html);

        $('.menu-check:checked').each(function () {
            let parentId = $(this).data('parent');
            if (parentId) {
                checkParent(parentId);
            }
        });
    });
}
function buildMenuTree(data, parentId, level) {

    let html = '';

    let children = data.filter(x => (x.parentId ?? null) == parentId);

    children.forEach(menu => {

        let margin = level * 20;

        html += `
            <div style="margin-left:${margin}px">
                <input type="checkbox"
                       class="menu-check"
                       data-id="${menu.menuId}"
                       data-parent="${menu.parentId ?? ''}"
                       value="${menu.menuId}"
                       ${menu.isSelected == 1 ? 'checked' : ''} />

                ${menu.menuName}
            </div>
        `;

        html += buildMenuTree(data, menu.menuId, level + 1);
    });

    return html;
}


// ==========================
// ✅ CHECK / UNCHECK CHILDREN
// ==========================
function checkChildren(parentId, isChecked) {

    $('.menu-check').each(function () {

        if ($(this).data('parent') == parentId) {

            $(this).prop('checked', isChecked);

            // recursive (multi-level)
            checkChildren($(this).data('id'), isChecked);
        }
    });
}


// ==========================
// ✅ CHECK PARENT
// ==========================
function checkParent(parentId) {

    let parent = $('.menu-check[data-id="' + parentId + '"]');

    if (parent.length) {

        parent.prop('checked', true);

        let nextParent = parent.data('parent');

        if (nextParent) {
            checkParent(nextParent);
        }
    }
}


// ==========================
// ✅ UNCHECK PARENT IF NEEDED
// ==========================
function uncheckParentIfNeeded(parentId) {

    let anyChecked = false;

    $('.menu-check').each(function () {

        if ($(this).data('parent') == parentId && $(this).prop('checked')) {
            anyChecked = true;
        }
    });

    if (!anyChecked) {

        let parent = $('.menu-check[data-id="' + parentId + '"]');
        parent.prop('checked', false);

        let nextParent = parent.data('parent');

        if (nextParent) {
            uncheckParentIfNeeded(nextParent);
        }
    }
}


// ==========================
// ✅ MENU CHECK EVENT
// ==========================
$(document).on('change', '.menu-check', function () {

    let isChecked = $(this).prop('checked');
    let currentId = $(this).data('id');
    let parentId = $(this).data('parent');

    // children
    checkChildren(currentId, isChecked);

    // parent
    if (isChecked && parentId) {
        checkParent(parentId);
    }

    // uncheck parent
    if (!isChecked && parentId) {
        uncheckParentIfNeeded(parentId);
    }
});


// ==========================
// ✅ SELECT ALL
// ==========================
$(document).on('change', '#selectAllMenu', function () {

    let isChecked = $(this).prop('checked');

    $('.menu-check').prop('checked', isChecked);
});


// ==========================
// ✅ SAVE
// ==========================
$('#btnSave').click(function () {

    let menuIds = new Set();

    $('.menu-check:checked').each(function () {

        let id = parseInt($(this).val());
        menuIds.add(id);

        // ✅ include parents automatically
        let parentId = $(this).data('parent');

        while (parentId) {
            menuIds.add(parseInt(parentId));

            let parent = $('.menu-check[data-id="' + parentId + '"]');
            parentId = parent.data('parent');
        }
    });

    $.ajax({
        url: '/Right/Save',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            groupId: selectedGroup,
            menuIds: Array.from(menuIds)
        }),
        success: function (res) {
            alert(res.message);
        }
    });
});



// OPEN ADD
$('#btnOpenAddGroup').click(function () {

    $('#groupModalTitle').text('Add Group');
    $('#groupId').val('');
    $('#groupNameInput').val('');
    $('#remarkInput').val('');
    $('#companyIdInput').val('');

    $('#groupModal').modal('show');
});


// OPEN EDIT
$(document).on('click', '.edit-group', function () {

    $('#groupModalTitle').text('Edit Group');

    $('#groupId').val($(this).data('id'));
    $('#groupNameInput').val($(this).data('name'));
    $('#remarkInput').val($(this).data('remark'));
    $('#companyIdInput').val($(this).data('company'));

    $('#groupModal').modal('show');
});


// SAVE (ADD + UPDATE)
$('#btnSaveGroup').click(function () {

    let model = {
        GroupId: $('#groupId').val(),
        GroupName: $('#groupNameInput').val(),
        Remark: $('#remarkInput').val(),
        CompanyId: $('#companyIdInput').val()
    };

    let url = model.GroupId ? '/Right/UpdateGroup' : '/Right/CreateGroup';

    $.ajax({
        url: url,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(model),
        success: function (res) {
            alert(res.message);
            location.reload();
        }
    });
});


$(document).ready(function () {

    loadCompanies();

});

function loadCompanies() {
    $.ajax({
        url: '/Right/GetCompanies',
        type: 'GET',
        success: function (data) {

            let $select = $('#companyIdInput');

            $select.empty();
            $select.append('<option value="">-- Select Company --</option>');

            $.each(data, function (i, item) {
                $select.append(`<option value="${item.id}">${item.companyName}</option>`);
            });

            // Initialize Select2
            $select.select2({
                dropdownParent: $('#groupModal'),
                width: '100%'
            });
        }
    });
}