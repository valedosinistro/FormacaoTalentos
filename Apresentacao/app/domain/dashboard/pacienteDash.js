var urlParams = new URLSearchParams(location.search);
var idPaciente = urlParams.get('id');

var link = document.querySelector('.link');
link.href = '../paciente/pacientePerfil.html?id=' + idPaciente; 

var link2 = document.querySelector('#agendar');
link2.href = '../dashboard/agendarConsulta.html?id=' + idPaciente; 

var link3 = document.querySelector('.sair');
link3.href = '../login/login.html';