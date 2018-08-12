var api = 'http://localhost:53731/api/paciente/';
var tabela = document.querySelector('#pacientes');

obterTodos();

function update(pacientes) {
    tabela.innerHTML = template(pacientes);
}

function template(pacientes = []) {
    return `
    <table class="table table-hover table-dark">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome</th>
                <th>Cpf</th>
                <th>Email</th>
                <th>Celular</th>
                <th>Data N.</th>
                <th>Sexo</th>
                <th>Ações</th>
            </tr>
        </thead>
        <tbody>
        ${
        pacientes.map(function (paciente) {
            return `
                    <tr>
                        <td>${paciente.id}</td>
                        <td>${paciente.nome}</td>
                        <td>${paciente.cpf}</td>
                        <td>${paciente.email}</td>
                        <td>${paciente.telefone}</td>
                        <td>${paciente.data_Nasc}</td>
                        <td>${paciente.sexo}</td>
                        <td>
                            <a href="#" onclick="alterarMedico(${paciente.id})">Editar</a> | 
                            <a href="#" onclick="excluirMedico(${paciente.id})">Excluir</a>
                        </td>
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
                    .then(function (pacientes) {
                        update(pacientes);
                    });
            } else {
                alert("Ocorreu um erro ao obter os pacientes");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

function alterarPaciente(idPaciente) {
    window.location.href = 'medicoCriar.html?id=' + idPaciente;
}

function excluirPaciente(idPaciente) {
    if (confirm('Tem certeza que deseja excluir esse médico?')) {

        var request = new Request(api + idPaciente, {
            method: "DELETE",
            headers: new Headers({
                'Content-Type': 'application/json'
            })
        });

        fetch(request)
            .then(function (response) {
                // console.log(response);
                if (response.status == 200) {
                    obterTodos();
                } else {
                    alert("Ocorreu um erro ao excluir os paciente");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }
}