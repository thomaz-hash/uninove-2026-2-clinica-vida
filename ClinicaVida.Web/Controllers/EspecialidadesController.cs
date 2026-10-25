using Microsoft.AspNetCore.Mvc;

namespace ClinicaVida.Web.Controllers;

public class EspecialidadesController : Controller
{
    public IActionResult Index()
    {
        ViewData["Titulo"] = "Especialidades da Clínica Vida+";

        return View();
    }

    public IActionResult Details(int id, string? nome)
    {
        if (id < 1 || id > 3)
        {
            return NotFound(); // 404: essa especialidade não existe na clínica
        }

        ViewData["Id"] = id;
        ViewData["Nome"] = nome;

        return View();
    }
}