using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio1_Agenda_Odontológico
{
    /// <summary>
    /// Possui a listagem dos pacientes cadastrados e a listagem da consulta agendadas para cada paciente.
    /// </summary>
    public class PacienteAgenda
    {
        private List<Paciente> listadepacientes;
        public List<Paciente> ListaDePacientes { get { return listadepacientes; } }

        private List<Consulta> agenda;
        public List<Consulta> Agenda { get { return agenda; } }

        public PacienteAgenda()
        {
            listadepacientes = new List<Paciente>();
            agenda = new List<Consulta>();
        }
        /// <summary>
        /// Adiciona um paciente e seus referidos dados na listagem de pacientes
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool AddPaciente(Paciente paciente)
        {
            for (int i = 0; i < listadepacientes.Count; i++)
            {
                if (listadepacientes[i].PacienteIgual(paciente))
                {
                    return false;
                }
            }
            this.listadepacientes.Add(paciente);
            return true;
        }
        /// <summary>
        /// Remove um paciente da listagem de pacientes
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        public bool RemovePaciente(Paciente paciente)
        {
            for (int i = 0; i < listadepacientes.Count; i++)
            {
                if (listadepacientes[i].PacienteIgual(paciente))
                {
                    for (int j = 0; j < agenda.Count; j++)
                    {
                        if (agenda[j].Paciente == paciente) agenda.RemoveAt(j);
                    }
                    listadepacientes.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Método para retornar o paciente da lista que possui o CPF igual a desejado.
        /// </summary>
        /// <param name="_cpf"></param>
        /// <returns></returns>
        public Paciente GetPaciente(string _cpf)
        {
            for (int i = 0; i < listadepacientes.Count; i++)
            {
                if (listadepacientes[i].Cpf.Equals(_cpf))
                {
                    return listadepacientes[i];
                }
            }
            return null;
        }
        /// <summary>
        /// Verifica se o horário que o paciente deseja marcar a consulta está disponível.
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public bool CheckHorario(Consulta consulta)
        {
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Data != consulta.Data)
                {
                    return true;
                }
                else if (agenda[i].HoraInicial == consulta.HoraInicial)
                {
                    return false;
                }
                else if ((agenda[i].HoraInicial > consulta.HoraInicial) && (agenda[i].HoraFinal == consulta.HoraFinal))
                {
                    return false;
                }
                else if ((agenda[i].HoraInicial < consulta.HoraInicial) && (agenda[i].HoraFinal > consulta.HoraFinal))
                {
                    return false;
                }
                else if ((agenda[i].HoraInicial > consulta.HoraInicial) && (agenda[i].HoraFinal < consulta.HoraFinal))
                {
                    return false;
                }
                else if ((agenda[i].HoraInicial == consulta.HoraFinal) && (agenda[i].HoraFinal > consulta.HoraFinal))
                {
                    return true;
                }
            }
            return true;
        }
        /// <summary>
        /// Ao excluir um paciente do cadastro verifica se o mesmo possui alguma consulta agendada, caso possua alguma consulta futura não permite que o paciente seja excluído
        /// </summary>
        /// <param name="paciente"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PacienteTemAgendamento(Paciente paciente)
        {
            DateTime checkData = DateTime.Now;
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Paciente.PacienteIgual(paciente) && agenda[i].Data > checkData)
                {
                    throw new Exception("Erro: paciente está agendado para " +
                        agenda[i].Data.ToShortDateString() + " as " +
                        agenda[i].HoraInicial.ToShortTimeString().PadLeft(4, ' ') + "h.");
                }
            }
            return false;
        }
        /// <summary>
        /// Verifica se o paciente já possui consulta marcada, caso a consulta seja futura não permite o agendamento de outra consulta.
        /// </summary>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public bool PodeAgendar(Consulta consulta)
        {
            DateTime checkData = DateTime.Now;
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Paciente.PacienteIgual(consulta.Paciente) && agenda[i].Data > checkData && consulta.Data >= agenda[i].Data)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Adiciona uma nova consulta para um paciente caso ele esteja cadastrado.
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="consulta"></param>
        /// <returns></returns>
        public bool AddAgendamento(Paciente paciente, Consulta consulta)
        {
            if (!listadepacientes.Contains(paciente))
            {
                return false;
            }
            this.agenda.Add(consulta);
            return true;
        }
        /// <summary>
        /// Remove um agendamento feito por um paciente.
        /// </summary>
        /// <param name="paciente"></param>
        /// <param name="consulta"></param>
        /// <param name="_horainicial"></param>
        /// <returns></returns>
        public bool RemoveAgendamento(Paciente paciente, Consulta consulta, DateTime _horainicial)
        {
            DateTime checkData = DateTime.Now;
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Paciente.PacienteIgual(paciente) && agenda[i].Data.Equals(consulta.Data) && agenda[i].HoraInicial.Equals(_horainicial))
                {
                    agenda.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        ///Método para retornar uma consulta da lista que possui Data igual
        /// </summary>
        /// <param name="_data"></param>
        /// <returns></returns>
        public Consulta GetConsulta(string _data)
        {
            DateTime compareData;
            DateTime.TryParse(_data, out compareData);
            for (int i = 0; i < agenda.Count; i++)
            {
                if (agenda[i].Data.Equals(compareData))
                {
                    return agenda[i];
                }
            }
            return null;
        }
    }
}
