var api = 'http://localhost:53731/api/medico/';

var tabela = document.querySelector('#medicos');

obterTodos();

function update(medicos) {
    tabela.innerHTML = template(medicos);
}

function template(medicos = []) {
    return `
    <table class="table table-hover table-bordered">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome</th>
                <th>Cpf</th>
                <th>Crm</th>
                <th>Especialidade</th>
                <th>Ações</th>
            </tr>
        </thead>
        <tbody>
        ${
            medicos.map(function(medico){
                return `
                    <tr>
                        <td>${medico.id}</td>
                        <td>${medico.nome}</td>
                        <td>${medico.cpf}</td>
                        <td>${medico.crm}</td>
                        <td>${medico.especialidade}</td>
                        <td>
                            <a href="#" onclick="alterarMedico(${medico.id})">Editar</a> | 
                            <a href="#" onclick="excluirMedico(${medico.id})">Excluir</a>
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
                    .then(function (medicos) {
                        update(medicos);
                    });
            } else {
                alert("Ocorreu um erro ao obter os médicos");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarMedico(idMedico) {
    window.location.href = 'medicoCriar.html?id=' + idMedico;
}

function excluirMedico(idMedico) {
    if (confirm('Tem certeza que deseja excluir esse médico?')) {
        
        var request = new Request(api + idMedico, {
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
                    alert("Ocorreu um erro ao excluir os médico");
                }
            })
            .catch(function (response) {
                // console.log(response);
                alert("Desculpe, ocorreu um erro no servidor.");
            });
    }


}