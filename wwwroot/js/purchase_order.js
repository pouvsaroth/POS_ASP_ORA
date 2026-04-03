let purchaseItems = [];

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