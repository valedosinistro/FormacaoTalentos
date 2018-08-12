var apiEspecialidade = 'http://localhost:53731/api/especialidade/';
var apiMedicoEspecialidade = 'http://localhost:53731/api/medico/especialidade/';
var apiMedico = 'http://localhost:53731/api/medico/';
var apiCidade = 'http://localhost:53731/api/Medico/Ativos/Cidade/Especialidade/';
var apiMedicoCidade = 'http://localhost:53731/api/Medico/Ativos/Cidade/';
var apiNomeCidade = '/Especialidade/';
var apiHora = 'http://localhost:53731/api/Consulta/data/';
var apiHorario = '/Medico/'
var api = 'http://localhost:53731/api/Consulta/';
var pacienteConsulta = 'http://localhost:53731/api/Consulta/paciente';

var urlParams = new URLSearchParams(location.search);
var idPaciente = urlParams.get('id');


var linkInicio = document.querySelector('.inicio');
linkInicio.href = "pacienteDash.html?id=" + idPaciente;

var linkPerfil = document.querySelector('.perfil');
linkPerfil.href = '../paciente/pacientePerfil.html?id=' + idPaciente; 

var linkSair = document.querySelector('.sair');
linkSair.href = "../login/login.html";

var form1 = document.getElementById('form-agenda');

var elementosConsulta = {
    idMedico: document.querySelector('#medico'),
    dataConsulta: document.querySelector('#data'),
    horario: document.querySelector('#hora'),
};

// "Escuta" o evento de ENVIAR do Formulário do Paciente
document.querySelector('#form-agenda').addEventListener('submit', function (event) {

    event.preventDefault();
    var data = elementosConsulta.dataConsulta.value.split('/').reverse().join('-');
    // Objeto Consulta
    var consulta = {
        idMedico: elementosConsulta.idMedico.value,
        horario: elementosConsulta.horario.value,
        dataConsulta: data,
        idPaciente: idPaciente,
        status: "A"
    };
    inserirConsulta(consulta);
    form1.reset();
});

function inserirConsulta(consulta) {

    var request = new Request(api, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(consulta)
    });
    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Consulta agendada com sucesso");
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


// API que pega as especialidades 
function obterEspecialidades(id) {
    var request = new Request(apiEspecialidade, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (especialidades) {
                        updateTemplateEspecialidades(especialidades, id);
                    });
            } else {
                alert("Ocorreu um erro ao obter as especialidades");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// Recebe o ID da especialidade e mostra no Select 
function updateTemplateEspecialidades(especialidades, id) {
    especialidade.innerHTML = templateEspecialidades(especialidades, id);
}

function templateEspecialidades(especialidades = [], id = null) {
    return `
        <option>Selecionar Especialidade</option>
        ${
        especialidades.map(function (especialidade) {
            return `
                    <option value="${especialidade.id}" ${especialidade.id == id ? 'selected' : ''}>${especialidade.nome}</option>
                `;
        }).join('')
        }
    `;
}

obterEspecialidades();

// API que pega os Médicos de uma determinada especialidade
function obterCidade(id) {
    var request = new Request(apiCidade + id, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (cidades) {
                        updateTemplateCidades(cidades, id);
                    });
            } else {
                alert("Ocorreu um erro ao obter as Cidades");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// Recebe Cidades
function updateTemplateCidades(cidades, id) {
    cidade.innerHTML = templateCidades(cidades, id);
}

function templateCidades(cidades = [], id = null) {
    return `
        <option>Selecionar Cidade</option>
        ${
        cidades.map(function (cidade) {
            return `
                    <option value="${cidade.cidade}" ${cidade.cidade == cidade ? 'selected' : ''}>${cidade.cidade}</option>
                `;
        }).join('')
        }
    `;
}

// Pega ID da especialidade e seleciona o médico
$("#especialidade").change(function () {
    var obterId = document.getElementById('especialidade').value;
    obterCidade(obterId);
});

// API que seleciona os médicos por Cidade e por Especialidade

function obterMedicoCidade(cidade, id) {

    var request = new Request(apiMedicoCidade + cidade + apiNomeCidade + id, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (MedicoCidades) {

                        updateTemplateMedicoCidades(MedicoCidades, id);
                    });
            } else {
                alert("Ocorreu um erro ao obter os Médicos");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// Recebe Medicos
function updateTemplateMedicoCidades(MedicoCidades, id) {
    var inputMedico = document.getElementById('medico');
    inputMedico.innerHTML = templateMedicoCidades(MedicoCidades, id);
}

function templateMedicoCidades(MedicoCidades = [], id = null) {
    return `
        <option>Selecionar Médico</option>
        ${
        MedicoCidades.map(function (MedicoCidade, id) {

            return `
                    <option value="${MedicoCidade.id}" ${MedicoCidade.id == id ? 'selected' : ''}>${MedicoCidade.nome}</option>
                `;
        }).join('')
        }
    `;
}

// Pega ID da cidade e seleciona o médico
$("#cidade").change(function () {
    var obterCidade = document.getElementById('cidade').value;
    var obterEspecialidade = document.getElementById('especialidade').value;
    obterMedicoCidade(obterCidade, obterEspecialidade);
});


$("#data").change(function () {
    var obterId = document.getElementById('medico').value;
    var dataEscolhida = document.getElementById('data').value;
    obterHora(obterId, dataEscolhida);
});

function obterHora(obterId, dataEscolhida) {
    var request = new Request(apiHora + dataEscolhida + apiHorario + obterId, {
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (horarios) {
                        updateTemplateHorarios(horarios);
                    });
            } else {
                alert("Ocorreu um erro ao obter os horários");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// Recebe Horários
function updateTemplateHorarios(horarios) {
    var inputHora = document.getElementById('hora');
    inputHora.innerHTML = templateHorarios(horarios);
}

function templateHorarios(horarios = [], id = null) {
    return `
        <option>Selecionar Horário</option>
        ${
        horarios.map(function (horario) {

            return `
                    <option value="${horario}" ${horario ? 'selected' : ''}>${horario}</option>
                `;
        }).join('')
        }
    `;
}

// 
// 
// 

var tabela = document.querySelector('#minhasConsultas');

obterTodos();

function update(consultas) {
    tabela.innerHTML = template(consultas);
}

function template(consultas = []) {
    return `
    <table class="table table-hover table-dark" style="width:100%; margin: auto">
        <thead>
            <tr>
                <th>Especialidade</th>
                <th>Data Consulta</th>
                <th>Hora Consulta</th>
            </tr>
        </thead>
        <tbody>
        ${
        consultas.map(function (consulta) {
            return `
                    <tr>
                        <td>${consulta.especialidade}</td>
                        <td>${consulta.dataConsultaFormatada}</td>
                        <td>${consulta.horario}</td>
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
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}