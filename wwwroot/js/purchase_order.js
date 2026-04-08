let purchaseItems = [];
$(document).ready(function () {
    var purchaseOrderTable = $('#purchaseOrderTable').DataTable({
        dom: 'rt<"bottom d-flex justify-content-between align-items-center"lip>',
        searching: false,
        scrollY: "60vh",
        scrollCollapse: true,
        paging: true
    });
    // init datepicker
    $('#purchaseDate').datepicker({
        format: 'dd-mm-yyyy',
        autoclose: true,
        todayHighlight: true
    });
    

    // =========================
    // SELECT2 CATEGORY
    // =========================
    $('#supplierId').select2({
        dropdownParent: $('#purchaseModal'),
        width: '100%',
        placeholder: "Select supplier",
        allowClear: true
    });

    site_loadDataDropDown('#supplierId', '/Base/GetSupplierDropDown');

    

});

// =========================
// ADD
// =========================

$('#btnAddPurchaseOrder').on('click', function () {
    //$('#divBillNo').hide();
    $('#purchaseDate').val(setTodayDateDDMMYYYY());
});

// =========================
// EDIT
// =========================
$('.edit-btn').on('click', function () {
   //$('#divBillNo').disabled();
});

$('#btnAddRow').click(function () {

    let row = `
    <tr>

        <td>
            <select class="form-control product-select">
                <option value="">Select</option>
            </select>
        </td>

        <td>
            <input type="number" class="form-control qty" value="1" />
        </td>

        <td>
            <input type="number" class="form-control cost" value="0" />
        </td>
         <td>
            <select class="form-control productunits-select">
                <option value="">Select</option>
            </select>
        </td>
        <td>
            <select class="form-control unit-select">
                <option value="">Select</option>
            </select>
        </td>
        <td class="discount">0.00</td>
        <td class="vat">0.00</td>

        <td class="total">0.00</td>

        <td>
            <button class="btn btn-danger btn-sm btn-remove"><i class="bi bi-trash"></i></button>
        </td>

    </tr>`;

    $('#purchaseItems').append(row);

    let $lastProductSelect = $('#purchaseItems tr:last .product-select');
    let $lastProductUnitsSelect = $('#purchaseItems tr:last .productunits-select');
    let $lastUnitSelect = $('#purchaseItems tr:last .unit-select');

    // 🔥 load data into that select only
    site_loadDataDropDownTable($lastProductSelect, '/Base/GetProductlistDropDown');
    site_loadDataDropDownTable($lastProductUnitsSelect, '/Base/GetProductUnitDropDown');
    site_loadDataDropDownTable($lastUnitSelect, '/Base/GetUnitDropDown');
    setTimeout(() => {
        $('#purchaseItems tr:last .qty').focus();
    }, 100);
});
$(document).on('input', '.qty, .cost', function () {

    let row = $(this).closest('tr');

    let qty = parseFloat(row.find('.qty').val()) || 0;
    let cost = parseFloat(row.find('.cost').val()) || 0;
    if (qty < 1) {
        qty = 1;
        row.find('.qty').val(1);
    }
    let total = qty * cost;

    row.find('.total').text(total.toFixed(2));

    calculateSummary();
});
$(document).on('click', '.btn-remove', function () {

    $(this).closest('tr').remove();

    calculateSummary();
});
function calculateSummary() {

    let subtotal = 0;

    $('#purchaseItems tr').each(function () {

        let total = parseFloat($(this).find('.total').text()) || 0;
        subtotal += total;

    });

    $('#subtotal').val(subtotal.toFixed(2));

    let discount = parseFloat($('#discount').val()) || 0;
    let total = subtotal - discount;
    let paid = parseFloat($('#paid').val()) || 0;

    $('#total').val(total.toFixed(2));
    $('#balance').val((total - paid).toFixed(2));
}

function addItem(productId, name, cost = 0) {
    let exist = purchaseItems.find(x => x.productId === productId);

    if (exist) {
        exist.qty++;
    } else {
        purchaseItems.push({ productId, name, qty: 1, cost });
    }

    renderTable();
}

function renderTable() {
    let html = "";
    let subtotal = 0;

    purchaseItems.forEach((item, i) => {
        let total = item.qty * item.cost;
        subtotal += total;

        html += `
        <tr>
            <td>${item.name}</td>
            <td><input type="number" value="${item.qty}" onchange="updateQty(${i}, this.value)" /></td>
            <td><input type="number" value="${item.cost}" onchange="updateCost(${i}, this.value)" /></td>
            <td>${total.toFixed(2)}</td>
            <td><button class="btn btn-danger btn-sm" onclick="removeItem(${i})">X</button></td>
        </tr>`;
    });

    $("#purchaseItems").html(html);
    $("#subtotal").val(subtotal.toFixed(2));

    calculateTotal();
}

function updateQty(i, val) {
    purchaseItems[i].qty = parseFloat(val) || 0;
    renderTable();
}

function updateCost(i, val) {
    purchaseItems[i].cost = parseFloat(val) || 0;
    renderTable();
}

function removeItem(i) {
    purchaseItems.splice(i, 1);
    renderTable();
}

function calculateTotal() {
    let subtotal = parseFloat($("#subtotal").val()) || 0;
    let discount = parseFloat($("#discount").val()) || 0;
    let total = subtotal - discount;
    let paid = parseFloat($("#paid").val()) || 0;

    $("#total").val(total.toFixed(2));
    $("#balance").val((total - paid).toFixed(2));
}

$("#discount, #paid").on("keyup", calculateTotal);

// SAVE
function savePurchase(isPay) {

    if (purchaseItems.length === 0) {
        alert("Please add product!");
        return;
    }

    let data = {
        supplierId: $("#supplierId").val(),
        billNo: $("#billNo").val(),
        purchaseDate: $("#purchaseDate").val(),
        discount: $("#discount").val(),
        paid: isPay ? $("#paid").val() : 0,
        items: purchaseItems
    };

    $.ajax({
        url: "/Purchase/Save",
        type: "POST",
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function (res) {
            alert(res);
            location.reload();
        },
        error: function () {
            alert("Error");
        }
    });
}

$("#savePurchase").click(() => savePurchase(false));
$("#saveAndPay").click(() => savePurchase(true));