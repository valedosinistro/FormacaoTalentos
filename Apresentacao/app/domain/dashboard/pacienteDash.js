var urlParams = new URLSearchParams(location.search);
var idPaciente = urlParams.get('id');

var link = document.querySelector('.link');
link.href = '../dashboard/pacienteDash.html?id=' + idPaciente;

