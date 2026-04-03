let cart = [];

function addToCart(id, name, price) {

    let item = cart.find(x => x.id === id);

    if (item) {
        item.qty++;
    } else {
        cart.push({ id: id, name: name, price: price, qty: 1 });
    }

    renderCart();
}

function renderCart() {
    let html = "";
    let total = 0;

    cart.forEach((item, index) => {
        let itemTotal = item.qty * item.price;
        total += itemTotal;

        html += `
            <tr>
                <td>${item.name}</td>

                <td>
                    <button onclick="decreaseQty(${index})">-</button>
                    ${item.qty}
                    <button onclick="increaseQty(${index})">+</button>
                </td>

                <td>$${item.price}</td>
                <td>$${itemTotal.toFixed(2)}</td>

                <td>
                    <button onclick="removeItem(${index})">❌</button>
                </td>
            </tr>
        `;
    });

    document.getElementById("cartItems").innerHTML = html;
    document.getElementById("totalAmount").innerText = "$" + total.toFixed(2);
    document.getElementById("subtotal").innerText = "$" + total.toFixed(2);
    document.getElementById("total").innerText = "$" + total.toFixed(2);
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

function increaseQty(index) {
    cart[index].qty++;
    renderCart();
}

function decreaseQty(index) {
    if (cart[index].qty > 1) {
        cart[index].qty--;
    } else {
        removeItem(index);
    }
    renderCart();
}
function removeItem(index) {
    cart.splice(index, 1);
    renderCart();
}
