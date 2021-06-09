// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// for logout confirmation
$("#logout-btn").on("click", function (e) {
  $("#logoutModal").modal("show");
  e.preventDefault();
});

function logout() {
  $("#logout-form").submit();
}

// for cart removal
let toRemoveitemId;

const confirmRemoveItem = (itemId, itemCount) => {
    console.log(itemId, itemCount);
    toRemoveitemId = itemId;
    if(itemCount == 1) {
        $("#removeItemModal").modal('show');
    } else {
        removeItem();
    }
};

const removeItem = () => {
    window.location.href = `Cart/RemoveFromCart?musicId=${toRemoveitemId}`;
}

// for clear cart
const confirmClearCart = () => {
    $("#clearCartModal").modal('show');
}

const clearCart = () => {
    window.location.href = "Cart/ClearCart";
}