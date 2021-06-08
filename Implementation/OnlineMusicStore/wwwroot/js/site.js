// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// for logout confirmation
$('#logout-btn').on('click', function (e) {
    $('#logoutModal').modal('show');
    e.preventDefault();
});

function logout() {
    $('#logout-form').submit();
}