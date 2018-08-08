﻿var api = 'http://localhost:53731/api/medico/';

//Pegando os Input
var elementosMedico = {
    email: document.querySelector('#email'),
    senha: document.querySelector('#senha'),
    telefone_r: document.querySelector('#telefone_r'),
    telefone_c: document.querySelector('#telefone_c'),
    logradouro: document.querySelector('#logradouro'),
    bairro: document.querySelector('#bairro'),
    numero: document.querySelector('#numero'),
    cidade: document.querySelector('#cidade'),
    uf: document.querySelector('#uf'),
};

// "Escuta" o evento de ENVIAR do Formulário do Paciente
document.querySelector('#form-medico').addEventListener('submit', function (event) {

    event.preventDefault();

    // Objeto Medico att
    var paciente = {
        telefone_r: elementosMedico.telefone_r.value,
        telefone_c: elementosMedico.telefone_c.value,
        endereco_c: elementosMedico.logradouro.value + ", " + elementosMedico.bairro.value + ", " + elementosMedico.numero.value,
        estado: elementosMedico.uf.value,
        cidade: elementosMedico.cidade.value,
        email: elementosMedico.email.value,
        senha: elementosMedico.senha.value
    };

    alterarMedico(medico);
});

// API obter Médico 
function obterMedico() {

    var urlParams = new URLSearchParams(location.search);
    var idMedico = urlParams.get('id');

    var request = new Request(api + idMedico, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            
            if (response.status == 200) {
                response.json()
                    .then(function (medico) {
                        document.getElementById('nome').value = medico.nome;
                        document.getElementById('crm').value = medico.crm;
                    });
            } else {
                alert("Ocorreu um erro ao obter o médico");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

obterMedico();

// API alterar médico 
function alterarMedico(medico) {

    var urlParams = new URLSearchParams(location.search);
    var idMedico = urlParams.get('id');

    var request = new Request(api + idMedico, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 202) {
                alert("Médico alterado com sucesso");
                window.location.href = 'medicoDash.html?id=' + idMedico;
            } else {
                response.json().then(function (message) {
                    alert(message.error);
                });
            }
        })
        .catch(function (response) {
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