var api = 'http://localhost:53731/api/medico/';
var apiEspecialidade = 'http://localhost:53731/api/especialidade/';

var especialidade = document.querySelector('#especialidade');

var form1 = document.getElementById("form-medico");

var elementosMedico = {
    nome: document.querySelector('#nome'),
    cpf: document.querySelector('#cpf'),
    crm: document.querySelector('#crm'),
    Especialidade: document.querySelector('#especialidade'),
    telefone_r: document.querySelector('#telefone_r'),
    telefone_c: document.querySelector('#telefone_c'),
    logradouro: document.querySelector('#logradouro'),
    bairro: document.querySelector('#bairro'),
    numero: document.querySelector('#numero'),
    cidade: document.querySelector('#cidade'),
    uf: document.querySelector('#uf'),
    sexo: document.querySelector('#sexo'),
    email: document.querySelector('#email'),
    senha: document.querySelector('#senha')
};

obterEspecialidades();

document.querySelector('#form-medico').addEventListener('submit', function (event) {

    event.preventDefault();
    console.log(elementosMedico.idEspecialidade);

    var medico = {
        nome: elementosMedico.nome.value,
        cpf: elementosMedico.cpf.value,
        crm: elementosMedico.crm.value,
        idEspecialidade: parseInt(elementosMedico.Especialidade.value),
        telefone_r: elementosMedico.telefone_r.value,
        telefone_c: elementosMedico.telefone_c.value,
        endereco_c: elementosMedico.logradouro.value + ", " + elementosMedico.bairro.value + ", " + elementosMedico.numero.value,
        estado: elementosMedico.uf.value,
        cidade: elementosMedico.cidade.value,
        sexo: elementosMedico.sexo.value,
        email: elementosMedico.email.value,
        senha: elementosMedico.senha.value
    };

    inserirMedico(medico);

});

// API que inseri o médico 
function inserirMedico(medico) {

    var request = new Request(api, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Médico inserido com sucesso");
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

// API que pega a especialidade do médico 
function obterEspecialidades(id) {
    var request = new Request(apiEspecialidade, {
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
                        updateTemplateEspecialidades(especialidades, id);
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

// Buscar CEP 
function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('logradouro').value=("");
    document.getElementById('bairro').value=("");
    document.getElementById('cidade').value=("");
    document.getElementById('uf').value=("");
}

function meu_callback(conteudo) {
if (!("erro" in conteudo)) {
    //Atualiza os campos com os valores.
    document.getElementById('logradouro').value=(conteudo.logradouro);
    document.getElementById('bairro').value=(conteudo.bairro);
    document.getElementById('cidade').value=(conteudo.localidade);
    document.getElementById('uf').value=(conteudo.uf);
} //end if.
else {
    //CEP não Encontrado.
    limpa_formulário_cep();
    alert("CEP não encontrado.");
}
}

function pesquisacep(valor) {

//Nova variável "cep" somente com dígitos.
var cep = valor.replace(/\D/g, '');

//Verifica se campo cep possui valor informado.
if (cep != "") {

    //Expressão regular para validar o CEP.
    var validacep = /^[0-9]{8}$/;

    //Valida o formato do CEP.
    if(validacep.test(cep)) {

        //Preenche os campos com "..." enquanto consulta webservice.
        document.getElementById('logradouro').value="...";
        document.getElementById('bairro').value="...";
        document.getElementById('cidade').value="...";
        document.getElementById('uf').value="...";

        //Cria um elemento javascript.
        var script = document.createElement('script');

        //Sincroniza com o callback.
        script.src = 'https://viacep.com.br/ws/'+ cep + '/json/?callback=meu_callback';

        //Insere script no documento e carrega o conteúdo.
        document.body.appendChild(script);

    } //end if.
    else {
        //cep é inválido.
        limpa_formulário_cep();
        alert("Formato de CEP inválido.");
    }
} //end if.
else {
    //cep sem valor, limpa formulário.
    limpa_formulário_cep();
}
};

// Máscaras
$(document).ready(function(){
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 00000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', {reverse: true});
    $('.crm').mask('000000/AA');
    $('.senha').mask('AAAAAAAA');
    $('.money').mask('000.000.000.000.000,00', {reverse: true});
  });

// Validação de senhas: conferir se são iguais
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