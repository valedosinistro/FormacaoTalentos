var urlParams = new URLSearchParams(location.search);
var idMedico = urlParams.get('id');

var link = document.querySelector('.link');
link.href = '../medico/medicoPerfil.html?id=' + idMedico; 