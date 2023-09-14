using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace OnlineGameStore;

public partial class GameWindow : Window
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    private List<Game> _game;
    private MySqlConnection _connection;
    public GameWindow()
    {
        InitializeComponent();
        ShowTable();
    }
    public void ShowTable()
    {
        string sql = "select GameID, GameName, Price, Developer from game "+
                     "join onlinegamestore.developer d on d.DeveloperID = game.Developer;";
        _game = new List<Game>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGame = new Game()
            {
                GameID = reader.GetInt32("GameID"),
                GameName = reader.GetString("GameName"),
                Price = reader.GetDouble("Price"),
                Developer = reader.GetString("Developer"),
            };
            _game.Add(currentGame);
        }
        _connection.Close();
        GameGrid.ItemsSource = _game;
    }

    private void MenuItem_OnClickPersonalAccount(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }

    private void MenuItem_OnClickUser(object? sender, RoutedEventArgs e)
    {
        UserWindow userWindow = new UserWindow();
        userWindow.Show();
    }
}