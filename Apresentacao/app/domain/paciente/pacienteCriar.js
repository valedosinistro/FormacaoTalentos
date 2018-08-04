var api = 'http://localhost:53731/api/paciente/';
var apiEspecialidade = 'http://localhost:53731/api/especialidade/';

var titulo = document.querySelector('#titulo-paciente');
var especialidade = document.querySelector('#consulta');

var elementosPaciente = {
    nome: document.querySelector('#nome'),
    cpf: document.querySelector('#cpf'),
    /* consulta: document.querySelector('#consulta')*/
};

var query = location.search.slice(1); // Pega as informações enviadas após o ponto de interrogação na URL

var partes = query.split('&'); // Caso tenha mais de um parâmetro eles serão separados pelo caracter & - A função split quebra a string em arrays, conforme um caracter informado

var data = {}; // Cria um objeto que conterá as informações passadas pela url

partes.forEach(function (parte) { // percorre as informações passadas

    var chaveValor = parte.split('='); // quebra em array o nome da informação e seu valor
    var chave = chaveValor[0]; // o nome da informação fica na posição 0
    var valor = chaveValor[1]; // o valor da informação fica na posição 1
    data[chave] = valor; // Essa é uma notação de array, porém, como chave não é um número, isso acaba também funcioando para criar um objeto
});

console.log(data); 

if(data.id) { // Se voi passado um id, quer dizer que eu estou alterando
    obterPaciente(data.id);
    titulo.innerHTML = 'ALTERAR PACIENTE';
}

else{
    obterConsultas();
    
    titulo.innerHTML ='ADICIONAR PACIENTE';
    
}


document.querySelector('#form-paciente').addEventListener('submit', function (event) {

    event.preventDefault();

    var paciente = {
        nome: elementosPaciente.nome.value,
        cpf: elementosPaciente.cpf.value,
        crm: elementosPaciente.crm.value,
        idEspecialidade: parseInt(elementosPaciente.especialidade.value)
    };

    if(data.id){
        alterarpaciente(data.id, paciente);
    }
    else{
        inserirpaciente(paciente);
    }

});

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
                alert("Paciente inserido com sucesso");
                atribuirValorAoFormulario();
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

function alterarPaciente(idPaciente, paciente) {

    var request = new Request(api + idpaciente, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(paciente)
    });

    fetch(request)
        .then(function (response) {
            // console.log(response);
            if (response.status == 202) {
                alert("Paciente alterado com sucesso");
                window.location.href="paciente.html";
            } else {
		response.json().then(function(message){
			alert(message.error);
		});
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function obterPaciente(idPaciente) {
    var request = new Request(api + idPaciente, {
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
                .then(function(paciente){
                    atribuirValorAoFormulario(paciente);
                    obterConsultas(paciente.idConsulta);
                });
            } else {
                alert("Ocorreu um erro ao obter o paciente");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}



function obterConsultas(id){
    var request = new Request(apiConsulta, {
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
                .then(function(consultas){
                    updateTemplateConsultas(consultas, id);
                });
            } else {
                alert("Ocorreu um erro ao obter as consultas");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function updateTemplateConsultas(consultas, id){
    consulta.innerHTML = templateConsultas(consultas, id);
}

function templateConsultas(consulta = [], id = null){
    return `
        <option>Selecione</option>
        ${
            consulta.map(function(consulta){
                return `
                    <option value="${consulta.id}" ${consulta.id == id ? 'selected' : ''}>${consulta.nome}</option>
                `;
            }).join('')
        }
    `;
}


function atribuirValorAoFormulario(paciente = {}) {
    elementospaciente.nome.value = paciente.nome || '';
    elementospaciente.cpf.value = paciente.cpf || '';
    elementospaciente.crm.value = paciente.crm || '';
    elementospaciente.consulta.value = paciente.idConsulta || '';
}