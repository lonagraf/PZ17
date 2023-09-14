using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace OnlineGameStore;

public partial class UserWindow : Window
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    private List<User> _user;
    private MySqlConnection _connection;
    public UserWindow()
    {
        InitializeComponent();
        ShowTable();
    }
    public void ShowTable()
    {
        string sql = "select * from User";
        _user = new List<User>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentUser = new User()
            {
                UserID = reader.GetInt32("UserID"),
                UserName = reader.GetString("UserName"),
                DateOfBirth = reader.GetDateTime("DateOfBirth"),
                RegistrationDate = reader.GetDateTime("RegistrationDate"),
            };
            _user.Add(currentUser);
        }
        _connection.Close();
        UserGrid.ItemsSource = _user;
    }

    private void MenuItem_OnClickPersonalAccount(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
    private void MenuItem_OnClickGame(object? sender, RoutedEventArgs e)
    {
        GameWindow gameWindow = new GameWindow();
        gameWindow.Show();
    }
}