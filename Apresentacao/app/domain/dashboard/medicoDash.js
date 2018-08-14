var api = 'http://localhost:53731/api/Consulta/medico/';
var apiEmerg = 'http://localhost:53731/api/Emergencia';

var urlParams = new URLSearchParams(location.search);
var idMedico = urlParams.get('id');

var link = document.querySelector('.link');
link.href = '../medico/medicoPerfil.html?id=' + idMedico;

var link2 = document.getElementById('consultaAgendada');
link2.href = '../dashboard/consultasPendentes.html?id=' + idMedico; 

var link3 = document.querySelector('.sair');
link3.href = '../login/login.html'; 

console.log(link2);

console.log(idMedico);

var tabela = document.querySelector('#ConsultasPendentes');

obterTodos();

function update(consultas) {
    tabela.innerHTML = template(consultas);
}

function template(consultas = []) {
    return `
    <table class="table table-hover table-dark" style="width:80%; margin: auto">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome do Paciente</th>
                <th>Data Consulta</th>
                <th>Hora Consulta</th>
                <th>Status</th>
            </tr>
        </thead>
        <tbody>
        ${
        consultas.map(function (consulta) {
            return `
                    <tr>
                        <td>${consulta.id}</td>
                        <td>${consulta.nomePaciente}</td>
                        <td>${consulta.dataConsultaFormatada}</td>
                        <td>${consulta.horario}</td>
                        <td>${consulta.status}</td>
                    </tr>
                `;

        }).join('')
        }
        </tbody>
    </table>
    `;
}

function obterTodos() {

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
                    .then(function (consultas) {
                        update(consultas);
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

// 
// 
// 

var sla = {
    id : null
}

$(document).ready(function () {
    setInterval(function () { 
       medicoEmergencia();
    }, 5000);

    $("#lanca").click(function () {
        console.log(sla.id);
        atendePaciente();
    });
});

function atendePaciente() {
    console.log(apiEmerg + "/" + sla.id + "/atendendo/medico/" + idMedico);
    
    var request = new Request("http://localhost:53731/" + sla.id + "/atendendo/medico/" + idMedico, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            console.log(response.status);
            
            if (response.status == 202) {
                response.json()
                    .then(function (emergencia) {
                        // $("#lanca").css("animation", "amarelo 1500ms infinite");
                        // $("#lanca").text("Paciente Encontrado");
                        console.log("Paciente Aceito");
                    });
            } else {
                console.log("Não existe emergências");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function medicoEmergencia() {
    var request = new Request(apiEmerg, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (emergencia) {
                        console.log(emergencia);
                        sla.id = emergencia.id;
                        $("#lanca").css("animation", "amarelo 1500ms infinite");
                        $("#lanca").text("Paciente Encontrado");
                    });
            } else {
                console.log("Não existe emergências");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}