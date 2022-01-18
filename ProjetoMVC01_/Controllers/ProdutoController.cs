using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoMVC01_.Entities;
using ProjetoMVC01_.Models;
using ProjetoMVC01_.Reports;
using ProjetoMVC01_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_.Controllers
{
    [Authorize]
    public class ProdutoController : Controller
    {
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //faz com que o método receba o evento SUBMIT da página
        public IActionResult Cadastro(ProdutoCadastroModel model,
            [FromServices] ProdutoRepository produtoRepository)
        {
            //verificando se todos os campos da model
            //passaram nas regras de validação..
            if (ModelState.IsValid)
            {
                try
                {
                    //cadastrar no banco de dados..
                    Produto produto = new Produto();
                    produto.Nome = model.Nome;
                    produto.Preco = Convert.ToDecimal(model.Preco);
                    produto.Quantidade = Convert.ToInt32(model.Quantidade);

                    //inserir o produto no banco de dados..
                    produtoRepository.Inserir(produto);

                    TempData["Mensagem"] = $"Produto {produto.Nome}, cadastrado com sucesso.";
                    ModelState.Clear(); //limpar os campos do formulário
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao cadastrar o produto: " + e.Message;
                }
            }

            return View();
        }

        //método que abre a página de consulta
        public IActionResult Consulta([FromServices] ProdutoRepository produtoRepository)
        {
            //classe de modelo de dados criada para a página de consulta
            var model = new ProdutoConsultaModel();

            try
            {
                //executar a consulta no banco de dados e armazenar o resultado no atributo 'Produtos' da classe ProdutoConsultaModel
                model.Produtos = produtoRepository.Consultar();
            }
            catch (Exception e)
            {
                //exibir mensagem de erro na página..
                TempData["Mensagem"] = "Erro ao consultar o produto: " + e.Message;
            }

            //enviando o objeto 'model' para a página..
            return View(model);
        }

        public IActionResult Exclusao(Guid id, [FromServices] ProdutoRepository produtoRepository)
        {
            try
            {
                // buscar no banco de dados o produto através do id
                var produto = produtoRepository.ObterPorId(id);

                //excluindo o produto
                produtoRepository.Excluir(produto);

                TempData["Mensagem"] = "Produto excluído com sucesso.";
            }
            catch (Exception e)
            {
                // exibir mensagem de erro na página
                TempData["Mensagem"] = "Erro ao excluir o produto: " + e.Message;
            }

            //redirecionamento do usuário de volta para a página de consulta
            return RedirectToAction("Consulta");
        }

        public IActionResult Edicao(Guid id, [FromServices] ProdutoRepository produtoRepository)
        {
            //classe de modelo de dados
            var model = new ProdutoEdicaoModel();

            try
            {
                // buscar o produto no banco de dados através do Id
                var produto = produtoRepository.ObterPorId(id);

                //trasnsferir os dados do produto para a classe model
                model.IdProduto = produto.IdProduto;
                model.Nome = produto.Nome;
                model.Preco = produto.Preco;
                model.Quantidade = produto.Quantidade;
            }
            catch (Exception e)
            {
                // exibir mensagem de erro na página
                TempData["Mensagem"] = "Erro ao exibir o produto: " + e.Message;
            }

            //enviando o objeto model para a página
            return View(model);
        }

        [HttpPost] // revebe o evento SUBMIT do formulário
        public IActionResult Edicao(ProdutoEdicaoModel model, [FromServices] ProdutoRepository produtoRepository)
        {
            // verifica se todos os campos da model passaram nas regras de validação do formulário 
            if (ModelState.IsValid)
            {
                try
                {
                    // buscar o produto no banco de dados através do Id
                    var produto = produtoRepository.ObterPorId(model.IdProduto);

                    // alterando os dados do produto
                    produto.Nome = model.Nome;
                    produto.Preco = Convert.ToDecimal(model.Preco);
                    produto.Quantidade = Convert.ToInt32(model.Quantidade);

                    // atualizando no banco de dados
                    produtoRepository.Alterar(produto);
                    TempData["Mensagem"] = "Produto atualizado com sucesso.";

                    // redirecionando de volta para a página de consulta
                    return RedirectToAction("Consulta");
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao atualizar o produto: " + e.Message;

                }
            }

            return View();
        }

        public IActionResult Relatorio()
        {
            return View();
        }

        [HttpPost] //recebe os dados enviados pelo formulário
        public IActionResult Relatorio(ProdutoRelatorioModel model, [FromServices] ProdutoRepository produtoRepository)
        {
            // verifica se todos os campos da model foram validados com sucesso
            if (ModelState.IsValid)
            {
                try
                {
                    // capturando as datas informadas na página (formulário)
                    var filtroDataMin = Convert.ToDateTime(model.DataMin);
                    var filtroDataMax = Convert.ToDateTime(model.DataMax);

                    // executando a consulta de produtos no banco de dados
                    var produtos = produtoRepository.ConsultarPorDatas(filtroDataMin, filtroDataMax);


                    // verificando se o tipo de relatório selecionado é PDF
                    if (model.TipoRelatorio.Equals("pdf"))
                    {
                        var produtoReport = new ProdutoReportPdf();
                        var pdf = produtoReport.GerarPdf(filtroDataMin, filtroDataMax, produtos);

                        // fazer o download do arquivo
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.Headers.Add("content-disposition", "attachment; filename=produtos.pdf");
                        Response.Body.WriteAsync(pdf, 0, pdf.Length);
                        Response.Body.Flush();
                        Response.StatusCode = StatusCodes.Status200OK;
                    }
                    // verificando se o tipo de relatório é EXCEL
                    else if (model.TipoRelatorio.Equals("excel"))
                    {
                        var produtoReport = new ProdutoReportExcel();
                        var excel = produtoReport.GerarExcel(filtroDataMin, filtroDataMax, produtos);

                        //fazer o download do arquivo
                        Response.Clear();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.Headers.Add("content-disposition","attachment; filename=produtos.xlsx");
                        Response.Body.WriteAsync(excel, 0, excel.Length);
                        Response.Body.Flush();
                        Response.StatusCode = StatusCodes.Status200OK;
                    }
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = "Erro ao gerar relatório:" + e.Message;
                }
            }

            return View();
        }

        // método que será chamado (executado) por um código JavaScript localizado em alguma página no sistema
        public JsonResult ObterDadosGrafico([FromServices] ProdutoRepository produtoRepository)
        {
            try
            {
                // retornar para JavaScript, o conteúdo da consulta feira no banco de dados
                return Json(produtoRepository.ConsultarTotal());
            }
            catch(Exception e)
            {
                //retornando mensagem de erro..
                return Json(e.Message);
            }
        }

    }
}
