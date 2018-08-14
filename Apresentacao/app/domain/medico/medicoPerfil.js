var api = 'http://localhost:53731/api/medico/';
var apiDesativa = 'http://localhost:53731/api/Medico/MudarAtivo/';

var urlParams = new URLSearchParams(location.search);
var idMedico = urlParams.get('id');

var link = document.querySelector('.inicio');
link.href = '../dashboard/medicoDash.html?id=' + idMedico;

var link2 = document.querySelector('.agendar');
link2.href = '../dashboard/consultasPendentes.html?id=' + idMedico;

var link3 = document.querySelector('.sair');
link3.href = '../login/login.html';

//Pegando os Input
var elementosMedico = {
    email: document.querySelector('#email'),
    senha: document.querySelector('#senha'),
    telefone_r: document.querySelector('#telefone_r'),
    telefone_c: document.querySelector('#telefone_c'),
    logradouro: document.querySelector('#logradouro'),
    bairro: document.querySelector('#bairro'),
    numero: document.querySelector('#numero'),
    estado: document.querySelector('#uf'),
    cidade: document.querySelector('#cidade')
    
};

// "Escuta" o evento de ENVIAR do Formulário do Paciente
document.querySelector('#form-medico').addEventListener('submit', function (event) {

    event.preventDefault();

    // Objeto Medico att
    var medico = {
        telefone_r: elementosMedico.telefone_r.value,
        telefone_c: elementosMedico.telefone_c.value,
        endereco_c: elementosMedico.logradouro.value + ", " + elementosMedico.bairro.value + ", " + elementosMedico.numero.value,
        estado: elementosMedico.estado.value,
        cidade: elementosMedico.cidade.value,
        email: elementosMedico.email.value,
        senha: elementosMedico.senha.value
    };
    alterarMedico(medico);
});

// API obter Médico 
function obterMedico() {

    var urlParams = new URLSearchParams(location.search);
    var idMedico = urlParams.get('id');

    var request = new Request(api + idMedico, {
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
                        var quebrar = medico.endereco_c.split(", ");
                        verificaStatus(medico.ativo);
                        document.getElementById('nome').value = medico.nome;
                        document.getElementById('crm').value = medico.crm;
                        document.getElementById('email').value = medico.email;
                        document.getElementById('telefone_r').value = medico.telefone_r;
                        document.getElementById('senha').value = medico.senha;
                        document.getElementById('confirmar-senha').value = medico.senha;
                        document.getElementById('telefone_c').value = medico.telefone_c;
                        document.getElementById('logradouro').value = quebrar[0];
                        document.getElementById('bairro').value = quebrar[1];
                        document.getElementById('numero').value = quebrar[2];
                        document.getElementById('cidade').value = medico.cidade;
                        document.getElementById('uf').value = medico.estado;
                    });
            } else {
                alert("Ocorreu um erro ao obter o médico");
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

obterMedico();

// API alterar médico 
function alterarMedico(medico) {

    var urlParams = new URLSearchParams(location.search);
    var idMedico = urlParams.get('id');

    var request = new Request(api + idMedico, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });
    fetch(request)
        .then(function (response) {
            
            if (response.status == 202) {
                alert("Médico alterado com sucesso");
                window.location.href = '../dashboard/medicoDash.html?id=' + idMedico;
            } else {
                response.json().then(function (message) {
                    alert(message.error);
                });
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// Buscar CEP 
function limpa_formulário_cep() {
    //Limpa valores do formulário de cep.
    document.getElementById('logradouro').value = ("");
    document.getElementById('bairro').value = ("");
    document.getElementById('cidade').value = ("");
    document.getElementById('uf').value = ("");
}

function meu_callback(conteudo) {
    if (!("erro" in conteudo)) {
        //Atualiza os campos com os valores.
        document.getElementById('logradouro').value = (conteudo.logradouro);
        document.getElementById('bairro').value = (conteudo.bairro);
        document.getElementById('cidade').value = (conteudo.localidade);
        document.getElementById('uf').value = (conteudo.uf);
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
        if (validacep.test(cep)) {

            //Preenche os campos com "..." enquanto consulta webservice.
            document.getElementById('logradouro').value = "...";
            document.getElementById('bairro').value = "...";
            document.getElementById('cidade').value = "...";
            document.getElementById('uf').value = "...";

            //Cria um elemento javascript.
            var script = document.createElement('script');

            //Sincroniza com o callback.
            script.src = 'https://viacep.com.br/ws/' + cep + '/json/?callback=meu_callback';

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


function verificaStatus(ativo) {
    var botao = document.querySelector('#botaoDesativa');
    console.log(ativo);
    if (ativo === true) {
        botao.value = "Desativar Conta";
    } else {
        botao.value = "Ativar Conta";
    }

}
//Método que o médico desativa sua conta
function desativarMedico(medico) {

    var urlParams = new URLSearchParams(location.search);
    var idUser = urlParams.get('id');

    var request = new Request(apiDesativa + idUser, {
        method: "PUT",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(medico)
    });

    fetch(request)
        .then(function (response) {
            if (response.status == 202) {
                alert("Operação efetuada com sucesso");
                window.location.href = "../login/login.html";
            } else {
                response.json().then(function (message) {
                    alert(message.error);
                });
            }
        })
        .catch(function (response) {
            alert("Desculpe, ocorreu um erro no servidor.");
        });

}


// Máscaras
$(document).ready(function () {
    $('.date').mask('00/00/0000');
    $('.time').mask('00:00:00');
    $('.date_time').mask('00/00/0000 00:00:00');
    $('.cep').mask('00000-000');
    $('.phone').mask('0000-0000');
    $('.phone_with_ddd').mask('(00) 0000-0000');
    $('.phone_us').mask('(000) 000-0000');
    $('.mixed').mask('AAA 000-S0S');
    $('.cpf').mask('000.000.000-00', { reverse: true });
    $('.crm').mask('000000/AA');
    $('.senha').mask('AAAAAAAA');
    $('.money').mask('000.000.000.000.000,00', { reverse: true });
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