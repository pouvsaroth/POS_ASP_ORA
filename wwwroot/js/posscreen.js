let cart = [];

function addToCart(name, price) {
    let item = cart.find(x => x.name === name);

    if (item) {
        item.qty++;
    } else {
        cart.push({ name: name, price: price, qty: 1 });
    }

    renderCart();
}

function renderCart() {
    let html = "";
    let total = 0;

    cart.forEach(item => {
        let itemTotal = item.qty * item.price;
        total += itemTotal;

        html += `
            <tr>
                <td>${item.name}</td>
                <td>${item.qty}</td>
                <td>$${item.price}</td>
                <td>$${itemTotal}</td>
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