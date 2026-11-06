using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ClinicaVida.Web.Models;

namespace ClinicaVida.Web.Controllers;

public class ConsultasController : Controller
{
    [HttpGet]
    public IActionResult Agendar()
    {
        CarregarListas();
        return View(new Consulta { Data = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Agendar(Consulta consulta)
    {
        if (consulta.Data.Date < DateTime.Today)
            ModelState.AddModelError("Data", "A consulta precisa ser em uma data futura.");

        if (consulta.Horario < new TimeSpan(7, 0, 0) || consulta.Horario >= new TimeSpan(19, 0, 0))
            ModelState.AddModelError("Horario", "A Clínica Vida+ atende das 07h às 19h.");

        if (!ModelState.IsValid)
        {
            CarregarListas();          // as listas suspensas não sobrevivem ao POST
            return View(consulta);     // mesma View, com os valores digitados e os erros
        }

        consulta.Id = ClinicaEmMemoria.Consultas.Count + 1;
        ClinicaEmMemoria.Consultas.Add(consulta);
        return RedirectToAction(nameof(Confirmacao), new { id = consulta.Id });
    }

    [HttpGet]
    public IActionResult Confirmacao(int id)
    {
        var consulta = ClinicaEmMemoria.Consultas.FirstOrDefault(c => c.Id == id);
        if (consulta is null) return NotFound();

        ViewBag.NomePaciente = ClinicaEmMemoria.Pacientes.First(p => p.Id == consulta.PacienteId).Nome;
        ViewBag.NomeMedico   = ClinicaEmMemoria.Medicos.First(m => m.Id == consulta.MedicoId).Nome;
        return View(consulta);
    }

    private void CarregarListas()
    {
        ViewBag.Pacientes = new SelectList(ClinicaEmMemoria.Pacientes, "Id", "Nome");
        ViewBag.Medicos   = new SelectList(ClinicaEmMemoria.Medicos,   "Id", "Nome");
    }
}