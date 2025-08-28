using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static async Task Main()
    {
        try
        {
            var htmlFilePath = "esajSimulado.html";
            var apiUrl = "http://localhost:5094/jurisprudencia";

            if (!File.Exists(htmlFilePath))
            {
                Console.WriteLine($"Arquivo não encontrado: {htmlFilePath}");
                return;
            }

            string htmlContent = System.IO.File.ReadAllText(htmlFilePath);
            var parser = new JurisprudenciaParser();
            var resultados = parser.ParseHtml(htmlContent);

            Console.WriteLine($"Foram encontrados {resultados.Count} resultados:");
            Console.WriteLine("==============================================");

            var apiService = new ApiService(apiUrl);

            Console.WriteLine("\nEnviando dados para a API...");

            // Opção 1: Sem autenticação
            var response = await apiService.EnviarDadosJurisprudenciaAsync(resultados);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Dados enviados com sucesso! Status: {response.StatusCode}");
                Console.WriteLine($"Resposta: {responseContent}");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Erro ao enviar dados. Status: {response.StatusCode}");
                Console.WriteLine($"Erro: {errorContent}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Detalhes: {ex.InnerException.Message}");
            }
        }

        Console.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey();
    }
}