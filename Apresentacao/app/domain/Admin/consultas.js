var api = 'http://localhost:53731/api/consulta/';
var tabela = document.querySelector('#consultas');

obterTodos();

function update(consultas) {
    tabela.innerHTML = template(consultas);
}

function template(consultas = []) {
    return `
    <table class="table table-hover table-dark" style="width:70%; margin: auto">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome do Paciente</th>
                <th>Nome do Médico</th>
                <th>Especialidade</th>
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
                        <td>${consulta.nomeMedico}</td>
                        <td>${consulta.especialidade}</td>
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
