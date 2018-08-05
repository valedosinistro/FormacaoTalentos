 //Caminho da API
var api = 'http://localhost:53731/api/paciente/';

//Pegando os Input
var elementosPaciente = {                     
    nome: document.querySelector('#nome'),
    cpf: document.querySelector('#cpf'),
    sexo: document.querySelector('#sexo'),
    email: document.querySelector('#email'),       
    telefone: document.querySelector('#celular'),
    data_Nasc: document.querySelector('#data-nasc'),
    senha: document.querySelector('#senha')
};

// "Escuta" o evento de ENVIAR do Formulário do Paciente
document.querySelector('#form-paciente').addEventListener('submit', function (event) {

    event.preventDefault();

    // Objeto Paciente
    var paciente = {
        nome: elementosPaciente.nome.value,
        cpf: elementosPaciente.cpf.value,
        sexo: elementosPaciente.sexo.value,
        email: elementosPaciente.email.value,
        telefone: elementosPaciente.telefone.value,
        data_Nasc: elementosPaciente.data_Nasc.value,
        senha: elementosPaciente.senha.value
    };

        inserirPaciente(paciente);
});

// Função que insere os pacientes 
function inserirPaciente(paciente) {

    var request = new Request(api, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(paciente)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Paciente cadastrado com sucesso");
            } else {
		        response.json().then(function(message){
			    alert(message.error);
		});
            }
        })
        .catch(function (response) {
            console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}
