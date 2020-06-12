window.onload = (event) => {
    GetCartAmount();
}

GetCartAmount()

function GetCartAmount() {
    fetch("https://localhost:44312/api/cart/getcartamount")
        .then(response => {
            console.log("response: ", response);
            if (response.ok) {
                return response.text();
            }
        }).then(data => {
            var element = document.getElementById("cart-amount");
            element.innerHTML = data;
        });
}

function AddToCart(seedId) {
    fetch("https://localhost:44312/api/cart/addtocart?id=" + seedId)
        .then(response => {
            console.log("response: ", response);
            if (response.ok) {
                return response.text();
            }
        }).then(data => {
            var element = document.getElementById("cart-amount");
            element.innerHTML = data;
        });
}