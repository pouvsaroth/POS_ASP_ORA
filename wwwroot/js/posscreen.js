let cart = [];

function addToCart(id, name, price, currency) {

    let item = cart.find(x => x.id === id);

    if (item) {
        item.qty++;
    } else {
        cart.push({ id: id, name: name, price: price, qty: 1 });
    }

    renderCart(currency);
}

function renderCart(currency) {
    let html = "";
    let total = 0;

    cart.forEach((item, index) => {
        let itemTotal = item.qty * item.price;
        total += itemTotal;

        html += `
            <tr>
                <td>${item.name}</td>

                <td>
                    <button onclick="decreaseQty(${index},'${currency}')">-</button>
                    ${item.qty}
                    <button onclick="increaseQty(${index},'${currency}')">+</button>
                </td>

                <td>${item.price} ${currency}</td>
                <td>${itemTotal.toFixed(2)} ${currency}</td>

                <td>
                    <button onclick="removeItem(${index},'${currency}')">❌</button>
                </td>
            </tr>
        `;
    });

    document.getElementById("cartItems").innerHTML = html;
    //document.getElementById("totalAmount").innerText = total.toFixed(2) + currency;
    document.getElementById("subtotal").innerText = total.toFixed(2) + currency;
    document.getElementById("total").innerText = total.toFixed(2) + currency;
}

function calculateChange() {
    let discountValue = parseFloat(document.getElementById("discountInput").value) || 0;
    let total = cart.reduce((sum, i) => sum + i.price * i.qty, 0);
    let change = total - discountValue;
    document.getElementById("total").innerText = "$" + change.toFixed(2);
}

function filterCategory(categoryId) {

    let items = document.querySelectorAll('.product-card');

    items.forEach(item => {
        let catId = item.getAttribute('data-category');

        if (categoryId == 0 || catId == categoryId) {
            item.style.display = "";   // ✅ reset (best for grid)
        } else {
            item.style.display = "none";
        }
    });
}

document.addEventListener("DOMContentLoaded", function () {

    document.querySelectorAll('.tabs button').forEach(btn => {
        btn.addEventListener('click', function () {

            document.querySelectorAll('.tabs button')
                .forEach(b => b.classList.remove('active'));

            this.classList.add('active');
        });
    });

});

function increaseQty(index, currency) {
    cart[index].qty++;
    renderCart(currency);
}

function decreaseQty(index,currency) {
    if (cart[index].qty > 1) {
        cart[index].qty--;
    } else {
        removeItem(index,currency);
    }
    renderCart(currency);
}
function removeItem(index, currency) {
    cart.splice(index, 1);
    renderCart(currency);
}

function cancelSale() {

    cart = [];
    document.getElementById("cartItems").innerHTML = "";
    document.getElementById("cartSummary").innerHTML = "";
    document.getElementById("subtotal").innerText = "$0.00";
    document.getElementById("total").innerText = "$0.00";
    document.getElementById("discountInput").value = 0;
}
function generateInvoiceNo() {
    let now = new Date();
    return "INV" + now.getFullYear()
        + (now.getMonth() + 1).toString().padStart(2, '0')
        + now.getDate().toString().padStart(2, '0')
        + now.getHours().toString().padStart(2, '0')
        + now.getMinutes().toString().padStart(2, '0')
        + now.getSeconds().toString().padStart(2, '0');
}
function printInvoice() {

    if (cart.length === 0) {
        alert("Cart is empty!");
        return;
    }

    let discount = parseFloat(document.getElementById("discountInput").value) || 0;
    let subtotal = cart.reduce((sum, i) => sum + i.price * i.qty, 0);
    let total = subtotal - discount;

    let saleData = {
        customerId: 1, // TODO: dynamic later
        totalAmount: subtotal,
        discount: discount,
        status: 1,

        Details: cart.map(x => ({
            productId: x.id,
            qty: x.qty,
            price: x.price,
            cost: 0,
            subDiscount: 0
        })),

        Payment: {
            paymentMethod: 1, // 1=Cash
            payAmount: total
        }
    };

    // 🔥 SEND TO SERVER
    fetch('/POSScreen/SaveInvoice', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(saleData)
    })
        .then(res => res.json())
        .then(res => {
            console.log(res);

            if (res.status === "SUCCESS") {

                // ✅ USE INVOICE FROM SERVER
                let invoiceNo = res.invoiceNo;

                printReceipt(invoiceNo, subtotal, discount, total);
            }
        })
        .catch(err => {
            alert("Error saving sale!");
            console.error(err);
        });
}
function printReceipt(invoiceNo, subtotal, discount, total) {

    let html = `
    <html>
    <head>
        <title>Receipt</title>
        <style>
            body {
                width: 280px; /* 80mm printer */
                font-family: Arial;
                font-size: 12px;
                margin: 0;
                padding: 5px;
            }

            .center { text-align: center; }
            .right { text-align: right; }

            img {
                max-width: 120px;
                margin: 5px auto;
                display: block;
            }

            h2, h4, p {
                margin: 3px 0;
            }

            table {
                width: 100%;
                border-collapse: collapse;
            }

            td {
                padding: 2px 0;
            }

            hr {
                border: none;
                border-top: 1px dashed #000;
                margin: 5px 0;
            }

            .barcode {
                text-align: center;
                margin-top: 10px;
            }
            /* 🔥 IMPORTANT FOR PRINT */
            @media print {
                body {
                    width: 80mm;
                }

                @page {
                    size: 80mm auto;
                    margin: 0;
                }
            }
        </style>
    </head>
    <body>

        <!-- 🔥 LOGO -->
        <img src="/images/vorakchuncoffeelogo.png" />

        <p class="center">វិក្កយបត្រ / Invoice</p>

        <hr/>

        <p>Invoice: ${invoiceNo}</p>
        <p>Date: ${new Date().toLocaleString()}</p>

        <hr/>

        <table>
    `;

    cart.forEach(item => {
        html += `
        <tr>
            <td>${item.name} x${item.qty}</td>
            <td class="right">${(item.price * item.qty).toFixed(2)}</td>
        </tr>`;
    });

    html += `
        </table>

        <hr/>

        <p>Subtotal: <span class="right">${subtotal.toFixed(2)}</span></p>
        <p>Discount: <span class="right">${discount.toFixed(2)}</span></p>
        <h3>Total: <span class="right">${total.toFixed(2)}</span></h3>

        <hr/>

        <!-- 🔥 BARCODE -->
        <div class="barcode">
            <svg id="barcode"></svg>
        </div>

        <p class="center">សូមអរគុណ 🙏</p>
        <p class="center">Thank you</p>

    </body>
    </html>
    `;

    let win = window.open('', '_blank');

    if (!win) {
        alert("Popup blocked!");
        return;
    }

    win.document.open();
    win.document.write(html);
    win.document.close();

    // 🔥 WAIT + GENERATE BARCODE
    setTimeout(() => {

        // Load JsBarcode dynamically
        let script = win.document.createElement("script");
        script.src = "https://cdn.jsdelivr.net/npm/jsbarcode@3.11.5/dist/JsBarcode.all.min.js";

        script.onload = function () {
            win.JsBarcode("#barcode", invoiceNo, {
                format: "CODE128",
                width: 1.5,
                height: 40,
                displayValue: true
            });

            // Print after barcode ready
            setTimeout(() => {
                win.focus();
                win.print();

                setTimeout(() => {
                    win.close();
                    cancelSale();
                }, 500);

            }, 500);
        };

        win.document.body.appendChild(script);

    }, 300);
}