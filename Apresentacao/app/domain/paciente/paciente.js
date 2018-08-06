var api = 'http://localhost:53731/api/paciente/';

var tabela = document.querySelector('#pacientes');

obterTodos();

function update(pacientes) {
    tabela.innerHTML = template(pacientes);
}

function template(pacientes = []) {
    return `
    <table class="table table-hover table-bordered">
        <thead>
            <tr>
                <th>#</th>
                <th>Nome</th>
                <th>Cpf</th>
                <th>Consulta</th>
                <th>Ações</th>
            </tr>
        </thead>
        <tbody>
        ${
            pacientes.map(function(paciente){
                return `
                    <tr>
                        <td>${paciente.id}</td>
                        <td>${paciente.nome}</td>
                        <td>${paciente.cpf}</td>
                        <td>${paciente.consulta}</td>
                        <td>
                            <a href="#" onclick="alterarpaciente(${paciente.id})">Editar</a> | 
                            <a href="#" onclick="excluirpaciente(${paciente.id})">Excluir</a>
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
                alert("Ocorreu um erro ao obter os pacientes?");
            }
        })
        .catch(function (response) {
            // console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}

function alterarpaciente(idPaciente) {
    window.location.href = 'pacienteCriar.html?id=' + idPaciente;
}

function excluirpaciente(idPaciente) {
    if (confirm('Tem certeza que deseja excluir esse paciente?')) {
        
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

// Máscaras
$(document).ready(function(){
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', {reverse: true});
    $('.crm').mask('000000/AA');
    $('.senha').mask('AAAAAAAA');
    $('.money').mask('000.000.000.000.000,00', {reverse: true});
  });

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