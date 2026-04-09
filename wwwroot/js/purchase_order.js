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
            <select class="form-control productunits-select">
                <option value="">Select</option>
            </select>
        </td>
        <td>
            <select class="form-control unit-select">
                <option value="">Select</option>
            </select>
        </td>
         <td>
            <input type="number" class="form-control cost" value="0" />
        </td>
        <td>
            <select class="form-control currency-select">
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
    let $lastCurrencySelect = $('#purchaseItems tr:last .currency-select');

    // 🔥 load data into that select only
    site_loadDataDropDownTable($lastProductSelect, '/Base/GetProductlistDropDown');
    site_loadDataDropDownTable($lastProductUnitsSelect, '/Base/GetProductUnitDropDown');
    site_loadDataDropDownTable($lastUnitSelect, '/Base/GetUnitDropDown');
    site_loadDataDropDownTable($lastCurrencySelect, '/Base/GetCurrencyDropDown',2);
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
function savePurchase() {

    let purchaseItems = []; // reset

    $('#purchaseItems tr').each(function () {

        let row = $(this);

        let item = {
            ProductId: parseInt(row.find('.product-select').val()) || 0,
            Qty: parseFloat(row.find('.qty').val()) || 0,
            ProductUnitId: parseInt(row.find('.productunits-select').val()) || 0,
            UnitId: parseInt(row.find('.unit-select').val()) || 0,
            Cost: parseFloat(row.find('.cost').val()) || 0,
            CurrencyId: parseInt(row.find('.currency-select').val()) || 0,
            Total: parseFloat(row.find('.total').text()) || 0,
            Vat: parseFloat(row.find('.vat').text()) || 0,
            Discount: parseFloat(row.find('.discount').text()) || 0
        };

        // prevent empty row
        if (item.ProductId) {
            purchaseItems.push(item);
        }
    });

    // 🚨 Check again after collecting
    if (purchaseItems.length === 0) {
        alert("Please add product!");
        return;
    }

    let data = {
        SupplierId: parseInt($("#supplierId").val()) || 0,
        BillNo: $("#billNo").val(),
        PurchaseDate: formatDateToISO($("#purchaseDate").val()),
        Total: parseFloat($("#total").val()) || 0,
        Status: 1,
        Items: purchaseItems
    };

    $.ajax({
        url: "/PurchaseOrder/Save",
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
$("#savePurchase").click(() => savePurchase());