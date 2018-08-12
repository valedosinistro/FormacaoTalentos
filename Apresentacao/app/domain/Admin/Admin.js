var api = 'http://localhost:53731/api/medico';

$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

function obterMedico() {

    var request = new Request(api, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {

            if (response.status == 200) {
                response.json()
                    .then(function (medico) {
                        // var quebrar = medico.endereco_c.split(", ");
                        document.getElementById('nome').value = medico.nome;
                        document.getElementById('crm').value = medico.crm;
                        document.getElementById('email').value = medico.email;
                        document.getElementById('telefone_r').value = medico.telefone_r;
                        // document.getElementById('senha').value = medico.senha;
                        // document.getElementById('logradouro').value = quebrar[0];
                        // document.getElementById('bairro').value = quebrar[1];
                        // document.getElementById('numero').value = quebrar[2];
                        document.getElementById('cidade').value = medico.cidade;
                        // document.getElementById('estado').value = medico.estado;
                        // document.getElementById('telefone_c').value = medico.telefone_c;
                    });
            } else {
                alert("Ocorreu um erro ao obter o médico");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

var tabela = document.querySelector('.medicos');


function templateMédicos(medicoS) {


    return `
    <table class="table table-hover table-bordered">
    <h4 class="text-center">Médicos</h4>
    <thead>
        <tr>
            <th>NOME</th>
            <th>CRM</th>
            <th>CIDADE</th>
            <th>E-MAIL</th>
        </tr>
    </thead>
    <tbody>
    ${
        medicoS.map(function (medico) {
            return `
            <tr>
                <td>${medico.nome}</td>
                <td>${medico.crm}</td>
                <td>${medico.cidade}</td>
                <td>${medico.email}</td>
                <td>${medico.telefone_r}</td>
            </tr>
            `;
        }).join('')
        }      
    </tbody>
    `;
}

function update(medicos) {
    tabela.innerHTML = template(medicos);
}

// LOGIN NO PAINEL ADMIN
document.querySelector('#form-login')
    .addEventListener('submit', function (event) {

        event.preventDefault();
        loginAdmin();
    
    });

function loginAdmin() {

    var form = document.getElementById('form-login');
    var login = document.getElementById('login');
    var senha = document.getElementById('senha');

    if (login.value == "admin@admin" && senha.value == "admin") {
        window.location.href = "dashboard.html";
    }
    else {
        form.reset();
    }   
}

