var api = 'http://localhost:53731/api/medico/';

obterTodos();

function obterTodos() {

    var request = new Request(api, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 200) {
                response.json()
                    .then(function (medicos) {
                        update(medicos);
                    });
            } else {
                alert("Ocorreu um erro ao obter os médicos");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarMedico(idMedico) {
    window.location.href = 'medicoCriar.html?id=' + idMedico;
}

function alterarMedico(idMedico, medico) {

    var request = new Request(api + idMedico, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 202) {
                alert("Médico alterado com sucesso");
                window.location.href = "medico.html";
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

function obterMedico(idMedico) {
    var request = new Request(api + idMedico, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 200) {
                response.json()
                    .then(function (medico) {
                        atribuirValorAoFormulario(medico);
                        obterEspecialidades(medico.idEspecialidade);
                    });
            } else {
                alert("Ocorreu um erro ao obter o médico");
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

// Validação de senhas: conferir se são iguais
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