using System.Text.Json;


internal class CheckRiskTransaction
{
    static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();
        // string transactionHash = "853793d552635f533aa982b92b35b00e63a1c1add062c099da2450a15119bcb2";
        Console.WriteLine("Введите хеш транзакции:");
        string transactionHash = Console.ReadLine() ?? "";
        string url = $"https://apilist.tronscan.org/api/transaction-info?hash={transactionHash}";

        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseText = await response.Content.ReadAsStringAsync();
            JsonElement transactionInfo = JsonSerializer.Deserialize<JsonElement>(responseText);

            bool isRiskTransaction = transactionInfo.GetProperty("riskTransaction").GetBoolean();
            string riskLevel = isRiskTransaction ? "Высокий риск" : "Низкий риск";
            Console.WriteLine($"\nУровень риска транзакции: {riskLevel}");
            Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Произошла ошибка: {e.Message}");
        }
    }
}