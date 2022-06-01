using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1_Agenda_Odontológico
{
    /// <summary>
    /// O paciente possui as propriedades Nome, CPF, Data de nascimento e Idade.
    /// </summary>
    public class Paciente
    {
        private string nome;
        public string Nome { get { return nome; } }

        private string cpf;
        public string Cpf { get { return cpf; } }

        private string datanasc;
        public string DataNasc { get { return datanasc; } }

        private int idade;
        public int Idade { get { return idade; } }

        public Paciente(string _nome, string _cpf, string _datanasc)
        {
            this.nome = _nome;
            this.cpf = _cpf;
            this.datanasc = _datanasc;
            this.idade = this.CalcularIdade(_datanasc);
        }
        /// <summary>
        /// Calcula a idade o paciente a partir da data de nascimento e a data atual.
        /// </summary>
        /// <param name="_datanasc"></param>
        /// <returns></returns>
        public int CalcularIdade(string _datanasc)
        {
            DateTime hoje = DateTime.Now;
            DateTime idadedata;

            DateTime.TryParse(_datanasc, out idadedata);
            return hoje.Year - idadedata.Year;
        }
        /// <summary>
        /// Verifica se dois pacientes são iguais pelo CPF.
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PacienteIgual(Paciente paciente)
        {
            if (paciente == null)
            {
                throw new Exception("Paciente não encontrado!");
            }
            if (this.cpf.Equals(paciente.cpf))
            {
                return true;
            }
            return false;
        }
    }
}
