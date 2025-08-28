using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;


public class JurisprudenciaParser
{
    public List<JurisprudenciaItem> ParseHtml(string htmlContent)
    {
        var resultados = new List<JurisprudenciaItem>();
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var divResultados = doc.DocumentNode.SelectSingleNode("//div[@id='divDadosResultado-A']");

        if (divResultados == null)
            return resultados;

        var tabelasResultados = divResultados.SelectNodes(".//tr[@class='fundocinza1']");

        if (tabelasResultados == null)
            return resultados;

        foreach (var tabela in tabelasResultados)
        {
            var item = new JurisprudenciaItem();

            var linkProcesso = tabela.SelectSingleNode(".//a[@class='esajLinkLogin downloadEmenta']");
            if (linkProcesso != null)
            {
                item.NumeroProcesso = linkProcesso.InnerText.Trim();
            }

            var spanAssuntoClasse = tabela.SelectSingleNode(".//span[@class='assuntoClasse']");
            if (spanAssuntoClasse != null)
            {
                var texto = spanAssuntoClasse.InnerText.Trim();
                if (texto.Contains("Classe/Assunto:"))
                {
                    var partes = texto.Split(new[] { "Classe/Assunto:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length > 1)
                    {
                        var classeAssunto = partes[1].Trim();
                        var divisao = classeAssunto.Split('/');
                        item.Classe = divisao[0].Trim();
                        item.Assunto = divisao.Length > 1 ? divisao[1].Trim() : "";
                    }
                }
            }

            var trRelator = tabela.SelectSingleNode(".//tr[@class='ementaClass2' and contains(., 'Relator')]");
            if (trRelator != null)
            {
                var texto = trRelator.InnerText.Trim();
                if (texto.Contains("Relator(a):"))
                {
                    var partes = texto.Split(new[] { "Relator(a):" }, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length > 1)
                    {
                        item.Relator = partes[1].Trim();
                    }
                    else if (partes.Length == 1)
                    {
                        item.Relator = partes[0].Trim();
                    }
                }
            }

            var trOrgao = tabela.SelectSingleNode(".//tr[@class='ementaClass2' and contains(., 'Órgão julgador')]");
            if (trOrgao != null)
            {
                var texto = trOrgao.InnerText.Trim();
                if (texto.Contains("Órgão julgador:"))
                {
                    var partes = texto.Split(new[] { "Órgão julgador:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length > 1)
                    {
                        item.OrgaoJulgador = partes[1].Trim();
                    }
                    else if (partes.Length == 1)
                    {
                        item.OrgaoJulgador = partes[0].Trim();
                    }
                }
            }

            var trData = tabela.SelectSingleNode(".//tr[@class='ementaClass2' and contains(., 'Data do julgamento')]");
            if (trData != null)
            {
                var texto = trData.InnerText.Trim();
                if (texto.Contains("Data do julgamento:"))
                {
                    var partes = texto.Split(new[] { "Data do julgamento:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length > 1)
                    {
                        var dataJulgamentoString = partes[1].Trim();
                        DateTime dataJulgamento = DateTime.ParseExact(dataJulgamentoString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                    else if (partes.Length == 1)
                    {
                        var dataJulgamentoString = partes[0].Trim();
                        item.DataJulgamento = DateTime.ParseExact(dataJulgamentoString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }
                }
            }

            var divEmenta = tabela.SelectSingleNode(".//div[@align='justify' and contains(., 'Ementa:')]");
            if (divEmenta != null)
            {
                var textoEmenta = divEmenta.InnerText.Trim();
                if (textoEmenta.Contains("Ementa:"))
                {
                    var partes = textoEmenta.Split(new[] { "Ementa:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length > 1)
                    {
                        var ementa = partes[1].Trim();
                        var indiceImg = ementa.IndexOf("<img");
                        if (indiceImg > 0)
                        {
                            ementa = ementa.Substring(0, indiceImg).Trim();
                        }
                        item.Ementa = ementa;
                    }
                    else if (partes.Length == 1)
                    {
                        var ementa = partes[0].Trim();
                        var indiceImg = ementa.IndexOf("<img");
                        if (indiceImg > 0)
                        {
                            ementa = ementa.Substring(0, indiceImg).Trim();
                        }
                        item.Ementa = ementa;
                    }
                }
            }

            resultados.Add(item);
        }

        return resultados;
    }
}
