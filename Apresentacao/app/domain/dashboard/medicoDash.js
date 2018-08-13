var api = 'http://localhost:53731/api/Consulta/medico/';

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

    var request = new Request(api, idMedico, {
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

$(document).ready(function () {
    $("#lanca").click(function () {
        $("#lanca").css("animation", "amarelo 1500ms infinite");
        $("#lanca").text("Procurando...");
        setInterval(function () { 
            $("#lanca").css("animation", "verde 1500ms infinite");
            $("#lanca").text("Paciente Encontrado");
        }, 10000);
    });
});