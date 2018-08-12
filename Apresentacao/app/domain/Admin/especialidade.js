var api = 'http://localhost:53731/api/especialidade/';
var tabela = document.querySelector('#especialidades');

obterTodos();

function update(especialidades) {
    tabela.innerHTML = template(especialidades);
}

function template(especialidades = []) {
    return `
    <table class="table table-hover table-dark" style="width:70%; margin: auto">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome da Especialidade</th>
                <th>Ações</th>
            </tr>
        </thead>
        <tbody>
        ${
            especialidades.map(function (especialidade) {
            return `
                    <tr>
                        <td>${especialidade.id}</td>
                        <td>${especialidade.nome}</td>
                        <td>
                            <a href="#" onclick="excluirEspecialidade(${especialidade.id})">Excluir</a>
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
                    .then(function (especialidades) {
                        update(especialidades);
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

function criarEspecialidade(idEspecialidade) {
    window.location.href = 'medicoCriar.html?id=' + idEspecialidade;
}

function excluirEspecialidade(idEspecialidade) {
    if (confirm('Tem certeza que deseja excluir esse médico?')) {

        var request = new Request(api + idEspecialidade, {
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
                    alert("Ocorreu um erro ao excluir as especialidades");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }
}