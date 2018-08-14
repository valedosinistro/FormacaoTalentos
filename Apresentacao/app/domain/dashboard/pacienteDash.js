var apiEmerg = 'http://localhost:53731/api/Emergencia';


var urlParams = new URLSearchParams(location.search);
var idPaciente = urlParams.get('id');

var link = document.querySelector('.link');
link.href = '../paciente/pacientePerfil.html?id=' + idPaciente;

var link2 = document.querySelector('#agendar');
link2.href = '../dashboard/agendarConsulta.html?id=' + idPaciente;

var link3 = document.querySelector('.sair');
link3.href = '../login/login.html';

// 

var emergencia = {
    idPaciente : idPaciente
}

$(document).ready(function () {
    $("#lanca").click(function () {
        $("#lanca").css("animation", "amarelo 1500ms infinite");
        $("#lanca").text("Procurando...");
        emergenciaPaciente(emergencia);
        setInterval(function () { 
            $("#lanca").css("animation", "verde 1500ms infinite");
            $("#lanca").text("Médico Encontrado");
        }, 5000);
    });
});


// 
// 
// 


function emergenciaPaciente(emergencia) {

    var request = new Request(apiEmerg, {
        method: "POST",
        headers: new Headers({
            'Content-Type': 'application/json'
        }),
        body: JSON.stringify(emergencia)
    });

    fetch(request)
        .then(function (response) {
            console.log(response);
            if (response.status == 201) {
                alert("Emergência Acionada");
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


          