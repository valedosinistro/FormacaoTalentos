var api = 'http://localhost:53731/api/Consulta/medico/';

var urlParams = new URLSearchParams(location.search);
var idMedico = urlParams.get('id');

var link = document.getElementById('consultaAgendada');
link.href = '../dashboard/consultasPendentes.html?id=' + idMedico;

var link2 = document.querySelector('.inicio');
link2.href = 'medicoDash.html?id=' + idMedico;

var link3 = document.querySelector('.perfil');
link3.href = '../medico/medicoPerfil.html?id=' + idMedico;

var link4 = document.querySelector('.sair');
link4.href = '../login/login.html';

var tabela = document.querySelector('#ConsultasPendentes');

obterTodos();

$(document).ready(function() {
    $('#example').DataTable();
} );

function update(consultas) {
    tabela.innerHTML = template(consultas);
}

function template(consultas = [], id = null) {
    return `
    <table class="table table-hover table-dark" style="margin: auto">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome do Paciente</th>
                <th>Data Consulta</th>
                <th>Hora Consulta</th>
                <th>Status</th>
            </tr>
        </thead>
        <tbody id="example">
        ${
        consultas.map(function (consulta) {
            console.log(consulta);
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