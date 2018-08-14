var api = 'http://localhost:53731/api/especialidade/';
var apiAdicionar = 'http://localhost:53731/api/Especialidade';
var tabela = document.querySelector('#especialidades');

var Especialidades = {
    nome: document.querySelector('#nova-especialidade')
};

var form1 = document.getElementById("form-especialidade");

obterTodos();

function update(especialidades) {
    tabela.innerHTML = template(especialidades);
}

function template(especialidades = []) {
    return `
    <table class="table table-hover table-dark" style="width:50vh; margin: auto; margin-bottom: -40px">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome da Especialidade</th>
            </tr>
        </thead>
        <tbody>
        ${
        especialidades.map(function (especialidade) {
            return `
                    <tr>
                        <td>${especialidade.id}</td>
                        <td>${especialidade.nome}</td>
                        
                    </tr>
                `;
        }).join('')
        }
        </tbody>
    </table>
    `;
}
//                 <th>Ações</th>
// // <td>
//     <a href="#" onclick="editarEspecialidade(${especialidade.id})">Editar</a>
// </td>
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

document.querySelector('#form-especialidade').addEventListener('submit', function (event) {

    event.preventDefault();

  
    var especialidade = {
        nome: Especialidades.nome.value,
    };
    inserirEspecialidade(especialidade);
    form1.reset();

});


function inserirEspecialidade(especialidade) {

    var request = new Request(apiAdicionar, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(especialidade)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Especialidade inserida com sucesso");
                obterTodos();
                form1.reset();
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

