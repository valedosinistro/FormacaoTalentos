var apiEspecialidade = 'http://localhost:53731/api/especialidade/';
var apiMedicoEspecialidade = 'http://localhost:53731/api/medico/especialidade/';
var apiMedico = 'http://localhost:53731/api/medico/';

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

obterEspecialidades();

// API que pega os Médicos de uma determinada especialidade
function obterMedicoPorEspecialidade(id) {
    var request = new Request(apiMedicoEspecialidade + id,{
        method: "GET",
        headers: new Headers({
            'Content-Type': 'application/json'
        })
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 200) {
                response.json()
                    .then(function (medicos) {
                        updateTemplateMedicos(medicos, id);
                    });
            } else {
                alert("Ocorreu um erro ao obter os médicos");
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
        <option>Especialidade</option>
        ${
        especialidades.map(function (especialidade) {
            return `
                    <option value="${especialidade.id}" ${especialidade.id == id ? 'selected' : ''}>${especialidade.nome}</option>
                `;
        }).join('')
        }
    `;
}

// Recebe o ID do Médico e mostra no Select
function updateTemplateMedicos(medicos, id){
    medico.innerHTML = templateMedicos(medicos, id);
}

function templateMedicos(medicos = [], id = null) {
    return `
        <option>Selecionar Médico</option>
        ${
        medicos.map(function (medico) {
            return `
                    <option value="${medico.id}" ${medico.id == id ? 'selected' : ''}>${medico.nome}</option>
                `;
        }).join('')
        }
    `;
}
// Pega ID da especialidade e seleciona o médico
$("#especialidade").change(function(){
    var obterId = document.getElementById('especialidade').value;
    obterMedicoPorEspecialidade(obterId);
}); 
