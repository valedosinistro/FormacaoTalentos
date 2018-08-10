var apiEspecialidade = 'http://localhost:53731/api/especialidade/';
var apiMedicoEspecialidade = 'http://localhost:53731/api/medico/especialidade/';
var apiMedico = 'http://localhost:53731/api/medico/';
var apiCidade = 'http://localhost:53731/api/Medico/Ativos/Cidade/Especialidade/';
var apiMedicoCidade = 'http://localhost:53731/api/Medico/Ativos/Cidade/';
var apiNomeCidade = '/Especialidade/';

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
    var request = new Request(apiCidade + id,{
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
function updateTemplateCidades(cidades, id){
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
$("#especialidade").change(function(){
    var obterId = document.getElementById('especialidade').value;
    obterCidade(obterId);
}); 

// API que seleciona os médicos por Cidade e por Especialidade

function obterMedicoCidade(cidade,id) {
    
    var request = new Request(apiMedicoCidade + cidade + apiNomeCidade + id ,{
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
                       
                        updateTemplateMedicoCidades(MedicoCidades,id);
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
function updateTemplateMedicoCidades(MedicoCidades){
    var inputMedico = document.getElementById('medico');
    inputMedico.innerHTML = templateMedicoCidades(MedicoCidades);
}

function templateMedicoCidades(MedicoCidades = []) {
    return `
        <option>Selecionar Médico</option>
        ${
            MedicoCidades.map(function (MedicoCidade,id) {
              
            return `
                    <option value="${MedicoCidade.id}" ${MedicoCidade.id == id ? 'selected' : ''}>${MedicoCidade.nome}</option>
                `;
        }).join('')
        }
    `;
}

// Pega ID da cidade e seleciona o médico
$("#cidade").change(function(){
    var obterCidade = document.getElementById('cidade').value;
    var obterEspecialidade = document.getElementById('especialidade').value;
    obterMedicoCidade(obterCidade,obterEspecialidade);
}); 


