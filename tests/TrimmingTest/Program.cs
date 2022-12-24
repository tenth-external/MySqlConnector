using MySqlConnector;

using var dataSource = new MySqlDataSource("server=localhost;user=root;password=pass;database=mysqltest");
using var connection = await dataSource.OpenConnectionAsync();
using var transaction = await connection.BeginTransactionAsync();
using var command = connection.CreateCommand();
command.CommandText = "SELECT 1;";
command.Transaction = transaction;
using (var reader = await command.ExecuteReaderAsync())
{
	while (await reader.ReadAsync())
	{
		Console.WriteLine(reader.GetValue(0));
	}
}
await transaction.CommitAsync();
