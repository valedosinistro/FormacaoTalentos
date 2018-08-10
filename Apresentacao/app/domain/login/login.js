var api = 'http://localhost:53731/api/login/';

var elementosForm = {
    login: document.querySelector('#login'),
    senha: document.querySelector('#senha')
};

document.querySelector('#form-login')
    .addEventListener('submit', function (event) {

        event.preventDefault();

        var obj = {
            user: elementosForm.login.value,
            senha: elementosForm.senha.value
        };

        autenticarUsuario(obj);

});

// Autenticar usuário utilizando API 
function autenticarUsuario(obj) {

    var teste = $('input[name=login]:checked').val();
    var apiatt;
    if (teste === 'medico') {
        apiatt = api + 'medico';
    } else {
        apiatt = api + 'paciente';
    }

    var request = new Request(apiatt, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(obj)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 200) {
                response.json()
                    .then(function (usuario) {
                        if (teste === 'medico') {
                            window.location.href = "../dashboard/medicoDash.html?id=" + usuario.id;
                        } else {
                            window.location.href = "../dashboard/pacienteDash.html?id=" + usuario.id;
                        }
                    });

            } else {
                response.json()
                    .then(function (message) {
                        alert(message.error);
                    });
            }
        })
        .catch(function (response) {
            console.log(response);
            alert("Desculpe, ocorreu um erro no servidor.");
        });
}

// CPF ou Email
$("#login").keydown(function(){
    try {
    	$("#login").unmask();
    } catch (e) {}
    
    
    var campo = $("#login").val();
    var tamanho = $("#login").val().length;
 
		if(tamanho > 9 && $.isNumeric(campo)){
        $("#login").mask("999.999.999-99");
    }
    
    // ajustando foco
    var elem = this;
    setTimeout(function(){
    	// mudo a posição do seletor
    	elem.selectionStart = elem.selectionEnd = 10000;
    }, 0);
    // reaplico o valor para mudar o foco
    var currentValue = $(this).val();
    $(this).val('');
    $(this).val(currentValue);
});

// Máscara
$(document).ready(function () {
    $('.senha').mask('AAAAAAAA');
});