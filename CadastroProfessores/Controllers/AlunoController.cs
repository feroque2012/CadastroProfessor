using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadastroProfessores.Business;
using CadastroProfessores.Model;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Cadastroalunoes.Controllers
{
    public class AlunoController : Controller
    {
        private IConfiguration configuration;
        public AlunoController(IConfiguration iConfig)
        {
            configuration = iConfig;
        }

        public IActionResult Index(int IdProfessor)
        {
            try
            {
                using (AlunoBLL alunoBLL = new AlunoBLL())
                {
                    ViewBag.IdProfessor = IdProfessor;
                    return View(alunoBLL.GetAluno(IdProfessor));
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View(new Aluno());
            }
        }
        [HttpGet]
        public IActionResult Cadastro(int? Id = null, int? IdProfessor = null)
        {
            ViewBag.IdProfessor = IdProfessor;

            try
            {
                using (AlunoBLL alunoBLL = new AlunoBLL())
                {
                    if (Id == null)
                        return View();
                    else
                        return View(alunoBLL.Get((int)Id));

                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View("Cadastro");
            }
        }

        [HttpPost]
        public IActionResult Gravar(Aluno model)
        {
            ViewBag.IdProfessor = model.IdProfessor;

            try
            {
                if (ModelState.IsValid)
                {
                    using (AlunoBLL alunoBLL = new AlunoBLL())
                    {
                        if (model.Id > 0)
                            alunoBLL.Update(model);
                        else
                            alunoBLL.Insert(model);

                        return RedirectToAction("Index", new { IdProfessor = model.IdProfessor });
                    }
                }

                return View("Cadastro");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View("Cadastro");
            }

        }

        [HttpGet]
        public JsonResult Excluir(int Id)
        {
            try
            {
                using (AlunoBLL alunoBLL = new AlunoBLL())
                {
                    alunoBLL.Delete(Id);

                    return Json(new
                    {
                        mensagem = "Exclusão realizada com sucesso!",
                        erro = 0
                    }); ;
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    mensagem = ex.Message,
                    erro = 1
                }); ;
            }
        }

        [HttpPost]
        public IActionResult EnviarArquivo(IFormFile file, int IdProfessor)
        {

            ViewBag.IdProfessor = IdProfessor;

            try
            {
                validaEnvioArquivo();

                using (AlunoBLL alunoBLL = new AlunoBLL())
                {
                    string[] linha;
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        while (reader.Peek() >= 0)
                        {
                            linha = reader.ReadLine().Split("||");
                            alunoBLL.Insert(new Aluno
                            {
                                Nome = linha[0],
                                VlrMensalidade = Convert.ToSingle(linha[1]),
                                DtVencimento = Convert.ToDateTime(linha[2]),
                                IdProfessor = IdProfessor
                            });
                        }
                    }

                    alunoBLL.InsertLog(new LogImportacaoArquivo()
                    {
                        DataImportacao = DateTime.Now,
                        NomeArquivo = file.FileName
                    });

                    return View("Index", alunoBLL.GetAluno(IdProfessor));
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "<b>Falha ao importar arquivo:</b> <br />" + ex.Message;
                return RedirectToAction("Index", "Aluno",new { IdProfessor = IdProfessor });
            }
        }

        private void validaEnvioArquivo()
        {
            int Tempo = Convert.ToInt32(configuration.GetSection("Configuracoes").GetSection("TempoEnvioArquivo").Value);


            using (AlunoBLL alunoBLL = new AlunoBLL())
            {
                var log = alunoBLL.getLog();
                var DataUltimaImportacao = log.DataImportacao.AddMinutes(Tempo);
                var DataAtual = DateTime.Now;

                if (DataUltimaImportacao > DataAtual)
                {
                    throw new Exception("Só está liberado a importação após: " + DataUltimaImportacao.ToString("dd/MM/yyyy HH:mm"));
                }
            }

        }
    }

}
