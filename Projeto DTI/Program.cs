using System;
using Microsoft.Data.Sqlite;

class program
{
    static string diretorio = "Data Source = BancoDeDados.db";

    static void Main(string[] args)
    {
        int i = 0;
        CriarTabela();

        var conexao = new SqliteConnection(diretorio);
        conexao.Open();

        while (i == 0)
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Cadastrar Filme");
            Console.WriteLine("2. Excluir Filme");
            Console.WriteLine("3. Alterar Filme");
            Console.WriteLine("4. Listar Filmes");
            Console.WriteLine("0. Sair");
            Console.Write("Escolha uma opção: ");
            int op = int.Parse(Console.ReadLine());
            switch (op)
            {
                case 1:
                    CadastrarFilme();
                    break;
                case 2:
                    ExcluirFilme();
                    break;
                case 3:
                    AlterarFilme();
                    break;
                case 4:
                    ListarFilmes();
                    break;
                case 0:
                    break;
            }

        }

    }

    static void CriarTabela()
    {
        var conexao = new SqliteConnection(diretorio);
        conexao.Open();
        var command = conexao.CreateCommand();

    }

    static void CadastrarFilme()
    {

    }

    static void ExcluirFilme()
    {

    }

    static void ListarFilmes()
    {

    }

    static void AlterarFilme()
    {

    }
}