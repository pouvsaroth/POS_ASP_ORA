let selectedGroup = 0;
$(document).on('change', '.menu-check', function () {

    let isChecked = $(this).prop('checked');
    let currentId = $(this).data('id');

    // ✅ 1. CHECK / UNCHECK CHILDREN
    $('.menu-check').each(function () {
        if ($(this).data('parent') == currentId) {
            $(this).prop('checked', isChecked).trigger('change');
        }
    });

    // ✅ 2. CHECK PARENT IF CHILD SELECTED
    let parentId = $(this).data('parent');

    if (isChecked && parentId != null) {
        $('.menu-check').each(function () {
            if ($(this).data('id') == parentId) {
                $(this).prop('checked', true);
            }
        });
    }

    // ✅ 3. UNCHECK PARENT IF ALL CHILDREN UNCHECKED
    if (!isChecked && parentId != null) {

        let anyChecked = false;

        $('.menu-check').each(function () {
            if ($(this).data('parent') == parentId && $(this).prop('checked')) {
                anyChecked = true;
            }
        });

        if (!anyChecked) {
            $('.menu-check').each(function () {
                if ($(this).data('id') == parentId) {
                    $(this).prop('checked', false);
                }
            });
        }
    }

});
// CLICK GROUP
$('.group-item').click(function () {

    $('.group-item').removeClass('active');
    $(this).addClass('active');

    selectedGroup = $(this).data('id');
    $('#groupId').val(selectedGroup);

    loadMenus(selectedGroup);
});

// LOAD MENU
function loadMenus(groupId) {
    $.get('/Right/GetMenus?groupId=' + groupId, function (data) {

        let html = '';

        data.forEach(menu => {
            let margin = menu.parentId == null ? 0 : 20;

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
        });

        $('#menuContainer').html(html);
    });
}

// SAVE
$('#btnSave').click(function () {

    let menuIds = [];

    $('.menu-check:checked').each(function () {
        menuIds.push(parseInt($(this).val()));
    });

    $.ajax({
        url: '/Right/Save',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(menuIds),
        success: function (res) {
            alert(res.message);
        }
    });
});