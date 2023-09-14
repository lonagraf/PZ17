using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace OnlineGameStore;

public partial class MainWindow : Window
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    private List<PersonalAccount> _personalAccount;
    private MySqlConnection _connection;
    public MainWindow()
    {
        InitializeComponent();
        string fullTable = "select PersonalAccountID, UserName, GameName, Online from onlinegamestore.personal_account " + 
                     "join onlinegamestore.user u on u.UserID = personal_account.User " +
                     "join onlinegamestore.game g on g.GameID = personal_account.Game;";
        ShowTable(fullTable);
    }

    public void ShowTable(string sql)
    {
        //string sql = "select PersonalAccountID, UserName, GameName, Online from onlinegamestore.personal_account " +
                    // "join onlinegamestore.user u on u.UserID = personal_account.User " +
                     //"join onlinegamestore.game g on g.GameID = personal_account.Game;";
        _personalAccount = new List<PersonalAccount>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentAccount = new PersonalAccount()
            {
                PersonalAccountID = reader.GetInt32("PersonalAccountID"),
                GameName = reader.GetString("GameName"),
                Online = reader.GetDateTime("Online"),
                UserName = reader.GetString("UserName")
            };
            _personalAccount.Add(currentAccount);
        }
        _connection.Close();
        OnlineGameStoreGrid.ItemsSource = _personalAccount;
    }

    private void MenuItem_OnClickUser(object? sender, RoutedEventArgs e)
    {
        UserWindow userWindow = new UserWindow();
        userWindow.Show();
    }

    private void MenuItem_OnClickGame(object? sender, RoutedEventArgs e)
    {
        GameWindow gameWindow = new GameWindow();
        gameWindow.Show();
    }

    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string searchSql = "select PersonalAccountID, UserName, GameName, Online from onlinegamestore.personal_account " + 
                           "join onlinegamestore.user u on u.UserID = personal_account.User " +
                           "join onlinegamestore.game g on g.GameID = personal_account.Game where GameName like '%" +
                           txtSearch.Text + "%';";
        ShowTable(searchSql);
    }
}