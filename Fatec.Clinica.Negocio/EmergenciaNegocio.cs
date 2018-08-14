using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Dto;
using Fatec.Clinica.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Fatec.Clinica.Negocio
{
    public class EmergenciaNegocio
    {
        private readonly EmergenciaRepositorio _emergenciaRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public EmergenciaNegocio()
        {
            _emergenciaRepositorio = new EmergenciaRepositorio();
        }

        public int CriarEmergencia(Emergencia entity)
        {
            return _emergenciaRepositorio.CriarEmergencia(entity);
        }

        public Emergencia ExibirEmergenciaParaOMedico()
        {
            var obj = _emergenciaRepositorio.ExibirEmergenciaParaOMedico();

            if (obj == null)
                throw new NaoEncontradoException("Não existe pacientes aguardando médicos !");

            return obj;
        }

        public void AlterarStatusAtendendo(int IdMedico,int id)
        {
            _emergenciaRepositorio.AlterarStatusAtendendo(IdMedico,id);
        }

        public void AlterarStatusRealizada(int id)
        {
            _emergenciaRepositorio.AlterarStatusRealizada(id);
        }
    }
}
