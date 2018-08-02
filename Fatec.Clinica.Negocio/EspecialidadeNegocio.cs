using System.Collections.Generic;
using Fatec.Clinica.Dado;
using Fatec.Clinica.Dominio;
using Fatec.Clinica.Dominio.Excecoes;
using Fatec.Clinica.Negocio.Abstracao;

namespace Fatec.Clinica.Negocio
{
    /// <summary>
    /// 
    /// </summary>
    public class EspecialidadeNegocio 
        : INegocioBase<Especialidade>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly EspecialidadeRepositorio _especialidadeRepositorio;

        /// <summary>
        /// 
        /// </summary>
        public EspecialidadeNegocio()
        {
            _especialidadeRepositorio = new EspecialidadeRepositorio();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Especialidade> Selecionar()
        {
            return _especialidadeRepositorio.Selecionar();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Especialidade SelecionarPorId(int id)
        {
            var obj = _especialidadeRepositorio.SelecionarPorId(id);

            if (obj == null)
                throw new NaoEncontradoException();

            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Inserir(Especialidade entity)
        {
            return _especialidadeRepositorio.Inserir(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Especialidade Alterar(int id, Especialidade entity)
        {
            entity.Id = id;
            _especialidadeRepositorio.Alterar(entity);

            return _especialidadeRepositorio.SelecionarPorId(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Deletar(int id)
        {
            var obj = SelecionarPorId(id);

            _especialidadeRepositorio.Deletar(obj.Id);
        }
    }
}
