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
            Console.WriteLine("5. Buscar FIlme");
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
                case 5:
                    BuscarFilme();
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

        var comando = conexao.CreateCommand();
        comando.CommandText = @"
        CREATE TABLE IF NOT EXISTS Filmes (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            DataLancamento DATE NOT NULL
            );";
        comando.ExecuteNonQuery();

    }

    static void CadastrarFilme()
    {
        Console.WriteLine("Digite o nome do Filme");
        string nome = Console.ReadLine();
        Console.WriteLine("Digite a Data de Lançamento");
        string data = Console.ReadLine();
        DateTime dataLancamento;
        try
        {
            dataLancamento = DateTime.Parse(data);
        }
        catch (FormatException)
        {
            Console.WriteLine("Data Inválida, seu filme não foi cadastrado!");
            return;
        }
        var conexao = new SqliteConnection(diretorio);
        conexao.Open();

        var comando = conexao.CreateCommand();
        comando.CommandText = "INSERT INTO Filmes (Nome, DataLancamento) VALUES ($nome, $data)";
        comando.Parameters.AddWithValue("$nome", nome);
        comando.Parameters.AddWithValue("$data", data);
        comando.ExecuteNonQuery();
        Console.WriteLine("Filme Cadastrado!");
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

    static void BuscarFilme()
    {

    }
}