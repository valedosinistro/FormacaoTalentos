var api = '';

var elementosForm = {
    login: document.querySelector('#login'),
    senha: document.querySelector('#senha')
};

document.querySelector('#login-link-cadastrar')
    .addEventListener('click', function (event) {

        event.preventDefault();

        alert("Aguardando implementação.");

    });

document.querySelector('#form-login')
    .addEventListener('submit', function (event) {

        event.preventDefault();

        var obj = {
            login: elementosForm.login.value,
            senha: elementosForm.senha.value
        };

        autenticarUsuario(obj);

    });

function autenticarUsuario(obj){
    console.log(obj);

    window.location.href="../medico/medico.html";
}