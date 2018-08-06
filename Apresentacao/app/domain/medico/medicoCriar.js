﻿var api = 'http://localhost:53731/api/medico/';
var apiEspecialidade = 'http://localhost:53731/api/especialidade/';

var especialidade = document.querySelector('#especialidade');

var form1 = document.getElementById("form-medico");

var elementosMedico = {
    nome: document.querySelector('#nome'),
    cpf: document.querySelector('#cpf'),
    crm: document.querySelector('#crm'),
    Especialidade: document.querySelector('#especialidade'),
    telefone_r: document.querySelector('#telefone_r'),
    telefone_c: document.querySelector('#telefone_c'),
    logradouro: document.querySelector('#logradouro'),
    bairro: document.querySelector('#bairro'),
    numero: document.querySelector('#numero'),
    cidade: document.querySelector('#cidade'),
    uf: document.querySelector('#uf'),
    sexo: document.querySelector('#sexo'),
    email: document.querySelector('#email'),
    senha: document.querySelector('#senha')
};

// var query = location.search.slice(1); // Pega as informações enviadas após o ponto de interrogação na URL

// var partes = query.split('&'); // Caso tenha mais de um parâmetro eles serão separados pelo caracter & - A função split quebra a string em arrays, conforme um caracter informado

// var data = {}; // Cria um objeto que conterá as informações passadas pela url

// partes.forEach(function (parte) { // percorre as informações passadas

//     var chaveValor = parte.split('='); // quebra em array o nome da informação e seu valor
//     var chave = chaveValor[0]; // o nome da informação fica na posição 0
//     var valor = chaveValor[1]; // o valor da informação fica na posição 1
//     data[chave] = valor; // Essa é uma notação de array, porém, como chave não é um número, isso acaba também funcioando para criar um objeto
// });

// console.log(data); 

// if(data.id) { // Se foi passado um id, quer dizer que eu estou alterando
//     obterMedico(data.id);
//     titulo.innerHTML = 'ALTERAR MÉDICO';
// }
// else{
obterEspecialidades();
// titulo.innerHTML ='ADICIONAR MÉDICO';
// }

document.querySelector('#form-medico').addEventListener('submit', function (event) {

    event.preventDefault();
    console.log(elementosMedico.idEspecialidade);

    var medico = {
        nome: elementosMedico.nome.value,
        cpf: elementosMedico.cpf.value,
        crm: elementosMedico.crm.value,
        idEspecialidade: parseInt(elementosMedico.Especialidade.value),
        telefone_r: elementosMedico.telefone_r.value,
        telefone_c: elementosMedico.telefone_c.value,
        endereco_c: elementosMedico.logradouro.value + ", " + elementosMedico.bairro.value + ", " + elementosMedico.numero.value,
        estado: elementosMedico.uf.value,
        cidade: elementosMedico.cidade.value,
        sexo: elementosMedico.sexo.value,
        email: elementosMedico.email.value,
        senha: elementosMedico.senha.value
    };

    inserirMedico(medico);
    form1.reset();



});

function inserirMedico(medico) {

    var request = new Request(api, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Médico inserido com sucesso");
            } else {

                response.json().then(function (message) {
                    alert(message.error);
                });

            }
        })
        .catch(function (response) {
            console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

// function alterarMedico(idMedico, medico) {

//     var request = new Request(api + idMedico, {
//         method: "PUT",
//         headers: new Headers({
//             'Content-Type': 'application/json'
//         }),
//         body: JSON.stringify(medico)
//     });

//     fetch(request)
//         .then(function (response) {
//             // console.log(response);
//             if (response.status == 202) {
//                 alert("Médico alterado com sucesso");
//                 window.location.href = "medico.html";
//             } else {
//                 response.json().then(function (message) {
//                     alert(message.error);
//                 });
//             }
//         })
//         .catch(function (response) {
//             // console.log(response);
//             alert("Desculpe, ocorreu um erro no servidor.");
//         });

// }

// function obterMedico(idMedico) {
//     var request = new Request(api + idMedico, {
//         method: "GET",
//         headers: new Headers({
//             'Content-Type': 'application/json'
//         })
//     });

//     fetch(request)
//         .then(function (response) {
//             // console.log(response);
//             if (response.status == 200) {
//                 response.json()
//                     .then(function (medico) {
//                         atribuirValorAoFormulario(medico);
//                         obterEspecialidades(medico.idEspecialidade);
//                     });
//             } else {
//                 alert("Ocorreu um erro ao obter o médico");
//             }
//         })
//         .catch(function (response) {
//             // console.log(response);
//             alert("Desculpe, ocorreu um erro no servidor.");
//         });
// }

function obterEspecialidades(id) {
    var request = new Request(apiEspecialidade, {
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
                    .then(function (especialidades) {
                        updateTemplateEspecialidades(especialidades, id);
                    });
            } else {
                alert("Ocorreu um erro ao obter as especialidades");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function updateTemplateEspecialidades(especialidades, id) {
    especialidade.innerHTML = templateEspecialidades(especialidades, id);
}

function templateEspecialidades(especialidades = [], id = null) {
    return `
        <option>Especialidade</option>
        ${
        especialidades.map(function (especialidade) {
            return `
                    <option value="${especialidade.id}" ${especialidade.id == id ? 'selected' : ''}>${especialidade.nome}</option>
                `;
        }).join('')
        }
    `;
}

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

