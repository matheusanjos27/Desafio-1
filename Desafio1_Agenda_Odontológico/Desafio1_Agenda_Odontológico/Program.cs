using System;
using ValidacaoNS;
using Desafio1_Agenda_Odontológico;


DadosApp.Dados();
public class DadosApp
{
    public static void Dados()
    {
        string? strnome = "";
        string? strcpf = "";
        string? strdatanasc = "";

        string horaInicial;
        string horaFinal;
        string dataConsulta;

        string? opcao;
        DateTime data;
        PacienteAgenda listaPacienteAgenda = new PacienteAgenda();
        ValidacaoNS.Validacao validacao = new ValidacaoNS.Validacao();
        bool sairprincipal = false;
        while (!sairprincipal)
        {
            bool sairpacientes = false;
            bool sairagenda = false;

            Console.Clear();
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1 - Cadastro de pacientes");
            Console.WriteLine("2 - Agenda");
            Console.WriteLine("3 - Fim");

            opcao = Console.ReadLine();
            if (string.IsNullOrEmpty(opcao)) continue;
            int opcaoprincipal = Convert.ToInt16(opcao);
            switch (opcaoprincipal)
            {
                case 1:
                    while (!sairpacientes)
                    {
                        Console.Clear();
                        Console.WriteLine("Menu do Cadastro de Pacientes");
                        Console.WriteLine("1 - Cadastrar novo paciente");
                        Console.WriteLine("2 - Excluir paciente");
                        Console.WriteLine("3 - Listar pacientes (ordenado por CPF)");
                        Console.WriteLine("4 - Listar pacientes (ordenado por nome)");
                        Console.WriteLine("5 - Voltar p/ menu principal");

                        opcao = Console.ReadLine();
                        if (string.IsNullOrEmpty(opcao)) continue;
                        int opcaopacientes = Convert.ToInt16(opcao);
                        switch (opcaopacientes)
                        {
                            case 1:
                                try
                                {
                                    Console.Clear();
                                    strcpf = LerCpf();
                                    strnome = LerNome();
                                    strdatanasc = LerDataNascimento();

                                    if (!validacao.ValidaCpf(strcpf))
                                    {
                                        do
                                        {
                                            Console.WriteLine("Erro: O Cpf precisa ter 11 digitos e ser válido: " + strcpf);
                                            strcpf = LerCpf();
                                        } while (!validacao.ValidaCpf(strcpf));
                                    }
                                    if (!validacao.ValidaNome(strnome))
                                    {
                                        do
                                        {
                                            Console.WriteLine("Erro: Nome possui menos de 5 caracteres: " + strnome);
                                            strnome = LerNome();
                                        } while (!validacao.ValidaNome(strnome));
                                    }

                                    if (!validacao.ValidaDataEIdade(strdatanasc))
                                    {
                                        do
                                        {
                                            Console.WriteLine("Erro: Data inválida " + strdatanasc);
                                            strdatanasc = LerDataNascimento();
                                        } while (!validacao.ValidaDataEIdade(strdatanasc));
                                    }

                                    Paciente paciente = new Paciente(strnome, strcpf, strdatanasc);

                                    if (!listaPacienteAgenda.AddPaciente(paciente))
                                    {
                                        Console.WriteLine("Erro: CPF já cadastrado");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cadastro realizado com sucesso!");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.WriteLine("Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            case 2:
                                foreach (Paciente pacientes in listaPacienteAgenda.ListaDePacientes)
                                {
                                    Console.WriteLine("Nome              :    " + pacientes.Nome);
                                    Console.WriteLine("CPF               :    " + pacientes.Cpf);
                                    Console.WriteLine("Data de Nascimento:    " + pacientes.DataNasc);
                                    Console.WriteLine();
                                }
                                Console.Clear();
                                Console.WriteLine("Remover Paciente");
                                Console.Write("CPF: ");
                                strcpf = Console.ReadLine();
                                Paciente removepaciente = listaPacienteAgenda.GetPaciente(strcpf);

                                try
                                {
                                    if (!listaPacienteAgenda.PacienteTemAgendamento(removepaciente))
                                    {
                                        if (listaPacienteAgenda.RemovePaciente(removepaciente))
                                        {
                                            Console.WriteLine("Paciente exclúido com sucesso!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Erro: paciente não cadastrado");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                                Console.WriteLine("Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            case 3:
                            case 4:
                                Console.WriteLine("---------------------------------------------------------------------------------------");
                                Console.WriteLine(" CPF          Nome                                    Dt.Nasc.          Idade       ");
                                Console.WriteLine("---------------------------------------------------------------------------------------");
                                foreach (Paciente _paciente in listaPacienteAgenda.ListaDePacientes.OrderBy(o => (opcaopacientes == 3 ? o.Cpf : o.Nome)).ToList())
                                {
                                    Console.Write(" ");
                                    Console.Write(_paciente.Cpf.PadLeft(11, ' ') + "  ");
                                    Console.Write(_paciente.Nome.PadRight(37, ' ') + "   ");
                                    Console.Write(_paciente.DataNasc + "         ");
                                    Console.WriteLine(_paciente.Idade.ToString().PadLeft(3, ' '));
                                    foreach (Consulta consulta in listaPacienteAgenda.Agenda.Where(w => w.Paciente.Cpf == _paciente.Cpf).ToList())
                                    {
                                        Console.Write(" ".PadRight(14, ' '));
                                        Console.WriteLine("Agendado para: " + consulta.Data.ToShortDateString());
                                        Console.Write(" ".PadRight(14, ' '));
                                        Console.Write(consulta.HoraInicial.ToShortTimeString().PadLeft(4, ' ') + " às ");
                                        Console.WriteLine(consulta.HoraFinal.ToShortTimeString().PadLeft(4, ' ') + "  ");
                                    }
                                }
                                Console.WriteLine("Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            case 5:
                                sairpacientes = true;
                                break;
                        }
                    }
                    break;
                case 2:
                    while (!sairagenda)
                    {
                        Console.Clear();
                        Console.WriteLine("Menu da Agenda");
                        Console.WriteLine("1 - Agendar consulta");
                        Console.WriteLine("2 - Cancelar agendamento");
                        Console.WriteLine("3 - Listar agenda");
                        Console.WriteLine("4 - Voltar p/ menu principal");

                        opcao = Console.ReadLine();
                        if (string.IsNullOrEmpty(opcao)) continue;
                        int opcaoagenda = Convert.ToInt16(opcao);
                        switch (opcaoagenda)
                        {
                            case 1:

                                Console.Clear();
                                Console.Write("CPF: ");
                                strcpf = Console.ReadLine();
                                if (!validacao.ValidaCpf(strcpf))
                                {
                                    do
                                    {
                                        Console.WriteLine("Erro: O Cpf precisa ter 11 digitos e ser válido: " + strcpf);
                                        strcpf = LerCpf();
                                    } while (!validacao.ValidaCpf(strcpf));
                                }

                                Console.Write("Data da consulta (DD/MM/AAAA): ");
                                dataConsulta = Console.ReadLine();
                                if (!validacao.ValidaDataConsulta(dataConsulta))
                                {
                                    do
                                    {
                                        Console.WriteLine("Erro: Data inválida " + dataConsulta);
                                        dataConsulta = LerDataConsulta();
                                    } while (!validacao.ValidaDataConsulta(dataConsulta));
                                }

                                Console.WriteLine("Horario inicial (HH:MM): ");
                                horaInicial = Console.ReadLine();
                                Console.WriteLine("Horario final (HH:MM): ");
                                horaFinal = Console.ReadLine();

                                if (!validacao.ValidaHorario(dataConsulta, horaInicial, horaFinal))
                                {
                                    do
                                    {
                                        Console.WriteLine("Erro: Horário inválido " + horaInicial + " - " + horaFinal);

                                        Console.WriteLine("Horario inicial (HH:MM): ");
                                        horaInicial = Console.ReadLine();
                                        Console.WriteLine("Horario final (HH:MM): ");
                                        horaFinal = Console.ReadLine();

                                    } while (!validacao.ValidaHorario(dataConsulta, horaInicial, horaFinal));
                                }

                                Paciente agendapaciente = listaPacienteAgenda.GetPaciente(strcpf);
                                Consulta agendahorario = new Consulta(dataConsulta, horaInicial, horaFinal, agendapaciente);
                                data = DateTime.Parse(dataConsulta);

                                if (listaPacienteAgenda.PodeAgendar(agendahorario))
                                {
                                    if (listaPacienteAgenda.CheckHorario(agendahorario))
                                    {
                                        if (listaPacienteAgenda.AddAgendamento(agendapaciente, agendahorario))
                                        {
                                            Console.WriteLine("Agendamento realizado com sucesso!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Erro: paciente não cadastrado");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Erro: horario ja tem consulta");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Paciente ja tem um agendamento futuro!");
                                }

                                Console.WriteLine("Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            case 2:
                                Console.Clear();
                                Console.Write("CPF: ");
                                strcpf = Console.ReadLine();
                                if (!validacao.ValidaCpf(strcpf))
                                {
                                    do
                                    {
                                        Console.WriteLine("Erro: O Cpf precisa ter 11 digitos e ser válido: " + strcpf);
                                        strcpf = LerCpf();
                                    } while (!validacao.ValidaCpf(strcpf));
                                }

                                Console.Write("Data da consulta (DD/MM/AAAA): ");
                                dataConsulta = Console.ReadLine();
                                if (!validacao.ValidaDataConsulta(dataConsulta))
                                {
                                    do
                                    {
                                        Console.WriteLine("Erro: Data inválida" + dataConsulta);
                                        dataConsulta = LerDataConsulta();
                                    } while (!validacao.ValidaDataConsulta(dataConsulta));
                                }
                                Console.Write("Hora incial: ");
                                horaInicial = Console.ReadLine();

                                Paciente removepaciente = listaPacienteAgenda.GetPaciente(strcpf);
                                Consulta removeconsulta = listaPacienteAgenda.GetConsulta(dataConsulta);
                                if (listaPacienteAgenda.RemoveAgendamento(removepaciente, removeconsulta, DateTime.Parse(dataConsulta + " " + horaInicial)))
                                {
                                    Console.WriteLine("Agendamento cancelado com sucesso!");
                                }
                                else
                                {
                                    Console.WriteLine("Erro: agendamento não encontrado");
                                }
                                Console.WriteLine("Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            case 3:

                                Console.WriteLine("Apresentar a agenda T-Toda ou P-Periodo: ");
                                string apreAgenda = Console.ReadLine().ToUpper();
                                if (string.IsNullOrEmpty(apreAgenda)) continue;
                                if (!apreAgenda.Equals("T") && !apreAgenda.Equals("P"))
                                {
                                    Console.WriteLine("Opção inválida!");
                                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                                    Console.ReadKey();
                                    continue;
                                }

                                List<Consulta> consultas = new List<Consulta>();
                                if (apreAgenda.Equals("T"))
                                {
                                    consultas.AddRange(listaPacienteAgenda.Agenda);
                                }
                                else if (apreAgenda.Equals("P"))
                                {
                                    string sdtInicial = LerData("Data Inicial");
                                    string sdtFinal = LerData("Data Final");
                                    if (string.IsNullOrEmpty(sdtInicial)) continue;
                                    if (string.IsNullOrEmpty(sdtFinal)) continue;

                                    DateTime dtInicial = DateTime.Parse(sdtInicial);
                                    DateTime dtFinal = DateTime.Parse(sdtFinal);
                                    consultas.AddRange(listaPacienteAgenda.Agenda.Where(w => w.Data >= dtInicial && w.Data <= dtFinal).ToList());
                                }


                                Console.WriteLine("---------------------------------------------------------------------------------------");
                                Console.WriteLine("    Data    H.Ini H.Fim Tempo Nome                                 Dt.Nasc.      ");
                                Console.WriteLine("---------------------------------------------------------------------------------------");
                                DateTime dataTemp = DateTime.MinValue;
                                foreach (Consulta consulta in consultas)
                                {

                                    Console.Write(" ");
                                    Paciente _paciente = consulta.Paciente;
                                    if (dataTemp != consulta.Data)
                                    {
                                        Console.Write(consulta.Data.ToString("dd/MM/yyyy").PadLeft(10, ' ') + " ");
                                        dataTemp = consulta.Data;
                                    }
                                    else
                                    {
                                        Console.Write("           ");
                                    }
                                    Console.Write(consulta.HoraInicial.ToShortTimeString().PadLeft(4, ' ') + " ");
                                    Console.Write(consulta.HoraFinal.ToShortTimeString().PadLeft(4, ' ') + " ");
                                    Console.Write(consulta.Tempo.Hours.ToString().PadLeft(2, '0') + ":" + consulta.Tempo.Minutes.ToString().PadLeft(2, '0') + " ");
                                    Console.Write(_paciente.Nome.PadRight(32, ' ') + "  ");
                                    Console.WriteLine(_paciente.DataNasc);
                                }
                                Console.WriteLine("Pressione qualquer tecla para continuar");
                                Console.ReadKey();
                                break;
                            case 4:

                                sairagenda = true;
                                break;
                        }
                    }
                    break;
                case 3:
                    sairprincipal = true;
                    break;
            }
        }
    }

    private static string? LerNome()
    {
        string? strnome;
        Console.Write("Nome: ");
        strnome = Console.ReadLine();
        return strnome;
    }

    private static string? LerCpf()
    {
        string? strcpf;
        Console.Write("CPF: ");
        strcpf = Console.ReadLine();
        return strcpf;
    }
    private static string? LerDataNascimento()
    {
        string? strdatanasc;
        Console.Write("Digite a data de nascimento (DD/MM/AAAA) : ");
        strdatanasc = Console.ReadLine();
        return strdatanasc;
    }
    private static string? LerDataConsulta()
    {
        string? strdataconsulta;
        Console.Write("Digite a data da consulta (DD/MM/AAAA) : ");
        strdataconsulta = Console.ReadLine();
        return strdataconsulta;
    }

    private static string? LerData(string mensagem)
    {
        string? strdatanasc;
        Console.Write(mensagem + " (DD/MM/AAAA) : ");
        strdatanasc = Console.ReadLine();
        return strdatanasc;
    }

}