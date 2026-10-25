using Microsoft.AspNetCore.Mvc;
using ClinicaVida.Web.Models;

namespace ClinicaVida.Web.Controllers;

public class MedicosController : Controller
{
    public IActionResult Index(int? especialidadeId)
    {
        var medicos = ClinicaEmMemoria.Medicos.AsEnumerable();

        // 1. Se veio um ID de especialidade válido, filtra a lista
        if (especialidadeId.HasValue && especialidadeId.Value > 0)
        {
            medicos = medicos.Where(m => m.EspecialidadeId == especialidadeId.Value);
        }

        var lista = medicos.OrderBy(m => m.Nome).ToList();

        // 2. Associa o objeto da Especialidade em cada médico para exibir o Nome na tabela
        foreach (var m in lista)
        {
            m.Especialidade = ClinicaEmMemoria.Especialidades
                .FirstOrDefault(e => e.Id == m.EspecialidadeId);
        }

        // 3. Envia os dados para a View
        ViewBag.Especialidades = ClinicaEmMemoria.Especialidades;
        ViewBag.EspecialidadeIdSelecionada = especialidadeId;

        return View(lista);
    }

    public IActionResult Details(int id)
    {
        var medico = ClinicaEmMemoria.Medicos.FirstOrDefault(m => m.Id == id);
        if (medico is null) return NotFound();

        medico.Especialidade = ClinicaEmMemoria.Especialidades
            .FirstOrDefault(e => e.Id == medico.EspecialidadeId);

        return View(medico);
    }
}