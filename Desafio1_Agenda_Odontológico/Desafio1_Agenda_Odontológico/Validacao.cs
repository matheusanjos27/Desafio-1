using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidacaoNS
{
    /// <summary>
    /// Valida todos os dados solicitados ao usuário
    /// </summary>
    public class Validacao
    {
        DateTime xdataNascimento;
        DateTime dataConsulta;

        public Validacao() { }

        /// <summary>
        /// Verifica se o nome é diferente de NULL, vazio e se possui no mínimo 5 caracteres.
        /// </summary>
        /// <param name="validanome"></param>
        /// <returns></returns>
        public bool ValidaNome(string validanome)
        {
            if (validanome != null && validanome != "" && validanome.Length >= 5)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Verifica se o CPF é diferente de NULL, vazio e se possui no mínimo 11 caracteres
        /// </summary>
        /// <param name="validacpf"></param>
        /// <returns></returns>
        public bool ValidaCpf(string validacpf)
        {
            long xcpf;

            if (!long.TryParse(validacpf, out xcpf) || validacpf == "" || validacpf.Length < 11 || validacpf.Length > 11)
            {
                return false;
            }
            else if (!VerificaCpf(validacpf))
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Verifica se o CPF é válido utilizando os cálculos necessários.
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public bool VerificaCpf(string cpf)
        {
            int[] J = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] K = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            if (cpf == "00000000000" ||
                cpf == "11111111111" ||
                cpf == "22222222222" ||
                cpf == "33333333333" ||
                cpf == "44444444444" ||
                cpf == "55555555555" ||
                cpf == "66666666666" ||
                cpf == "77777777777" ||
                cpf == "88888888888" ||
                cpf == "99999999999") { return false; }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * J[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * K[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
        /// <summary>
        /// Verifica se a data inserida pelo usuário é diferente de NULL, vazio e se o paciente possuí no mínimo 13 anos.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool ValidaDataEIdade(string data)
        {
            DateTime hoje = DateTime.Now;
            if (!DateTime.TryParse(data, out xdataNascimento))
            {
                return false;
            }
            int idade = hoje.Year - xdataNascimento.Year;
            if (idade < 13)
            {
                throw new Exception("Erro: paciente só tem " + idade.ToString() + " anos.");
            }
            return true;
        }
        /// <summary>
        /// Verifica se a data da consulta é diferente de NULL, vazio e se a mesma é maior que a data atual, não sendo possível marcar consultas para datas passadas.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ValidaDataConsulta(string data)
        {
            if (!DateTime.TryParse(data, out dataConsulta)) return false;

            DateTime hoje = DateTime.Parse(DateTime.Now.ToShortDateString());
            if (dataConsulta < hoje) return false;

            return true;
        }
        /// <summary>
        /// Após o agendamento de uma consulta verifica se os horários inseridos são definidos de 15 em 15 minutos (Máximo de 4 consultas em 1 hora) e se o mesmo está dentro do horário de funcionamento do consultório.
        /// </summary>
        /// <param name="dataconsulta"></param>
        /// <param name="horaInicial"></param>
        /// <param name="horaFinal"></param>
        /// <returns></returns>
        public bool ValidaHorario(string dataconsulta, string horaInicial, string horaFinal)
        {
            DateTime dtInicial = DateTime.Parse(dataconsulta + " " + horaInicial);
            DateTime dtFinal = DateTime.Parse(dataconsulta + " " + horaFinal);
            if (dtInicial == dtFinal)
            {
                return false;
            }

            if (dtInicial < DateTime.Now)
            {
                return false;
            }
            if (dtFinal < DateTime.Now)
            {
                return false;
            }
            if (dtInicial.Minute % 15 != 0 || dtFinal.Minute % 15 != 0)
            {
                return false;
            }
            if (dtInicial.Hour < 8 || dtFinal.Hour > 19)
            {
                return false;
            }
            return true;
        }
    }
}

