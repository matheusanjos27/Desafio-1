using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Desafio1_Agenda_Odontológico
{
    public class Consulta
    {
        /// <summary>
        /// A consulta possui as propriedades Objeto Paciente, Data, Hora Inicial, Hora Final e Tempo da Consulta.
        /// </summary>
        private Paciente paciente;
        public Paciente Paciente { get { return paciente; } }

        private DateTime data;
        public DateTime Data { get { return data; } }

        private DateTime horainicial;
        public DateTime HoraInicial { get { return horainicial; } }

        private DateTime horafinal;
        public DateTime HoraFinal { get { return horafinal; } }

        public TimeSpan Tempo
        {
            get
            {
                return (horafinal - horainicial);
            }
        }

        public Consulta(string _strdata, string _horainicial, string _horafinal, Paciente _paciente)
        {
            DateTime.TryParse(_strdata, out data);
            this.horainicial = DateTime.Parse(_strdata + " " + _horainicial);
            this.horafinal = DateTime.Parse(_strdata + " " + _horafinal);
            this.paciente = _paciente;
        }
        /// <summary>
        /// Verifica se duas consultas são iguais caso o horário inicial seja o mesmo.
        /// </summary>
        /// <param name="agendaconsulta"></param>
        /// <returns></returns>
        public bool ConsultaIgual(Consulta agendaconsulta)
        {
            if (this.horainicial.Equals(agendaconsulta))
            {
                return true;
            }
            return false;
        }
    }
}
