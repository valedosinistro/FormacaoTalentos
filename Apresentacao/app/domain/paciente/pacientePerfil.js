var api = 'http://localhost:53731/api/paciente/';

//Pegando os Input
var elementosPaciente = {
    email: document.querySelector('#email'),
    telefone: document.querySelector('#celular'),
    senha: document.querySelector('#senha')
};

// "Escuta" o evento de ENVIAR do Formulário do Paciente
document.querySelector('#form-paciente').addEventListener('submit', function (event) {

    event.preventDefault();

    // Objeto Paciente att
    var paciente = {
        email: elementosPaciente.email.value,
        telefone: elementosPaciente.telefone.value,
        senha: elementosPaciente.senha.value
    };

    alterarPaciente(paciente);
    form.reset();
});


// API Pegar Paciente
function obterIdPaciente() {

    var urlParams = new URLSearchParams(location.search);
    var idUser = urlParams.get('id');

    var request = new Request(api + idUser, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (paciente) {
                        document.getElementById('nome').value = paciente.nome;
                    });
            } else {
                alert("Ocorreu um erro ao obter os pacientes?");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

//Chamando função 
obterIdPaciente();

// API Altera Paciente 
function alterarPaciente(paciente) {

    var urlParams = new URLSearchParams(location.search);
    var idUser = urlParams.get('id');

    var request = new Request(api + idUser, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(paciente)
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 202) {
                alert("Paciente alterado com sucesso");
                window.location.href = "pacienteDash.html?id="+idUser;
            } else {
                response.json().then(function (message) {
                    alert(message.error);
                });
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

// Máscaras
$(document).ready(function () {
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.crm').mask('000000/AA');
    $('.senha').mask('AAAAAAAA');
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
});

// confirmar se senhas digitadas estão iguais
var senha1 = document.getElementById("senha");
var senha2 = document.getElementById("confirmar-senha");

function validaSenha() {
    if (senha1.value != senha2.value) {
        senha2.setCustomValidity("Senhas diferentes!");
    } else {
        senha2.setCustomValidity('');
    }
}

senha1.onchange = validatePassword;
senha2.onkeyup = validatePassword;