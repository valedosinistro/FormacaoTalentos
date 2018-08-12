// MENU
$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

// LOGIN NO PAINEL ADMIN
document.querySelector('#form-login')
    .addEventListener('submit', function (event) {

        event.preventDefault();
        loginAdmin();

    });

function loginAdmin() {

    var form = document.getElementById('form-login');
    var login = document.getElementById('login');
    var senha = document.getElementById('senha');

    if (login.value == "admin@admin" && senha.value == "admin") {
        window.location.href = "dashboard.html";
    }
    else {
        form.reset();
    }
}



