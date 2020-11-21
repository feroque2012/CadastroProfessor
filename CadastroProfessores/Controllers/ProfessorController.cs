using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CadastroProfessores.Business;
using CadastroProfessores.Model;

namespace CadastroProfessores.Controllers
{
    public class ProfessorController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                using (ProfessorBLL professorBLL = new ProfessorBLL())
                {
                    return View(professorBLL.Get());
                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View(new Professor());
            }
        }
        [HttpGet]
        public IActionResult Cadastro(int? Id = null)
        {
            try
            {
                using (ProfessorBLL professorBLL = new ProfessorBLL())
                {
                    if (Id == null)
                        return View();
                    else
                        return View(professorBLL.Get((int)Id));

                }
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Gravar(Professor model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (ProfessorBLL professorBLL = new ProfessorBLL())
                    {
                        if (model.Id > 0)
                            professorBLL.Update(model);
                        else
                            professorBLL.Insert(model);

                        return RedirectToAction("Index");
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
                using (ProfessorBLL professorBLL = new ProfessorBLL())
                {
                    professorBLL.Delete(Id);

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
    }
}
