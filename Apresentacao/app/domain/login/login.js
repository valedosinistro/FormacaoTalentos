var api = 'http://localhost:53731/api/login/';

var elementosForm = {
    login: document.querySelector('#login'),
    senha: document.querySelector('#senha')
};

document.querySelector('#form-login')
    .addEventListener('submit', function (event) {

        event.preventDefault();

        var obj = {
            user: elementosForm.login.value,
            senha: elementosForm.senha.value
        };

        autenticarUsuario(obj);

    });

function autenticarUsuario(obj) {

    var teste = $('input[name=login]:checked').val();
    var apiatt;
    if (teste === 'medico') {
        // window.location.href = "../medico/medicoPerfil.html";
        apiatt = api + 'medico';
    } else {
        // window.location.href = "../paciente/pacientePerfil.html";
        apiatt = api + 'paciente';
    }

    var request = new Request(apiatt, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(obj)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 200) {
                response.json()
                    .then(function (usuario) {
                        if (teste === 'medico') {
                            window.location.href = "../medico/medicoPerfil.html?id=" + usuario.id;
                        } else {
                            window.location.href = "../paciente/pacientePerfil.html?id=" + usuario.id;
                        }
                    });

            } else {
                response.json()
                    .then(function (message) {
                        alert(message.error);
                    });
            }
        })
        .catch(function (response) {
            console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

$(document).ready(function () {
    $('.senha').mask('AAAAAAAA');
});