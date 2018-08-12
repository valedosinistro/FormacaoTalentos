 //Caminho da API
var api = 'http://localhost:53731/api/paciente/';

// Pega o formulário
var form = document.getElementById('form-paciente');

//Pegando os Input
var elementosPaciente = {                     
    nome: document.querySelector('#nome'),
    cpf: document.querySelector('#cpf'),
    sexo: document.querySelector('#sexo'),
    email: document.querySelector('#email'),       
    telefone: document.querySelector('#celular'),
    data_Nasc: document.querySelector('#data-nasc'),
    senha: document.querySelector('#senha')
};

// "Escuta" o evento de ENVIAR do Formulário do Paciente
document.querySelector('#form-paciente').addEventListener('submit', function (event) {

    event.preventDefault();

    // Objeto Paciente
    var paciente = {
        nome: elementosPaciente.nome.value,
        cpf: elementosPaciente.cpf.value,
        sexo: elementosPaciente.sexo.value,
        email: elementosPaciente.email.value,
        telefone: elementosPaciente.telefone.value,
        data_Nasc: elementosPaciente.data_Nasc.value,
        senha: elementosPaciente.senha.value
    };

        inserirPaciente(paciente);
        form.reset();
});

// API que insere os pacientes 
function inserirPaciente(paciente) {

    var request = new Request(api, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(paciente)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Paciente cadastrado com sucesso");
            } else {
		        response.json().then(function(message){
			    alert(message.error);
		});
            }
        })
        .catch(function (response) {
            console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// Máscaras 
$(document).ready(function(){
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 00000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', {reverse: true});
    $('.senha').mask('AAAAAAAA');
    $('.money').mask('000.000.000.000.000,00', {reverse: true});
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