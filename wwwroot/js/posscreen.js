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
                    <button onclick="removeItem(${index},${currency})">❌</button>
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
    let payment = parseFloat(document.getElementById("paymentInput").value) || 0;
    let total = cart.reduce((sum, i) => sum + i.price * i.qty, 0);

    let change = payment - total;
    document.getElementById("change").innerText = "$" + change.toFixed(2);
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
        removeItem(index);
    }
    renderCart(currency);
}
function removeItem(index, currency) {
    cart.splice(index, 1);
    renderCart(currency);
}
