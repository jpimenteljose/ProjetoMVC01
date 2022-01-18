using OfficeOpenXml;
using ProjetoMVC01_.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_.Reports
{
    public class ProdutoReportExcel
    {
        public byte[] GerarExcel(DateTime dataMin, DateTime dataMax, List<Produto> produtos)
        {
            // definindo a licença não comercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // abrindo o arquivo Excel
            using (var excelPackage = new ExcelPackage())
            {
                // definindo o nome da planilha
                var sheet = excelPackage.Workbook.Worksheets.Add("Relatório de Produtos");

                // escrevendo o conteúdo do arquivo
                sheet.Cells["A1"].Value = "Relatorio de Produtos";

                sheet.Cells["A3"].Value = "Data de Início";
                sheet.Cells["B3"].Value = dataMin.ToString("dd/MM/YYYY");

                sheet.Cells["A4"].Value = "Data de Término";
                sheet.Cells["B4"].Value = dataMax.ToString("dd/MM/YYYY");

                sheet.Cells["A6"].Value = "Nome do Produto";
                sheet.Cells["B6"].Value = "Preço";
                sheet.Cells["C6"].Value = "Quantidade";
                sheet.Cells["D6"].Value = "Total";
                sheet.Cells["E6"].Value = "Data de Cadastro";

                var linha = 7;

                foreach(var item in produtos)
                {
                    sheet.Cells[$"A{linha}"].Value = item.Nome;
                    sheet.Cells[$"B{linha}"].Value = item.Preco;
                    sheet.Cells[$"C{linha}"].Value = item.Quantidade;
                    sheet.Cells[$"D{linha}"].Formula = $"=B{linha}*C{linha}";
                    sheet.Cells[$"E{linha}"].Value = item.DataCadastro.ToString("dd/MM/yyyy");
                    
                    linha++;
                }

                // formatação para ajustar a largura das colunas na planilha
                sheet.Cells["A:E"].AutoFitColumns();

                // finalizar e retornar o arquivo Excel
                return excelPackage.GetAsByteArray();
            }
        }
    }
}
