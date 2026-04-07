using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using siteFilm.Models;

namespace siteFilm.Controllers;

public class FilmController : Controller
{
    public static List<Film> Films = new()
    {
        new Film { Id = 1, Titre = "Inception", Genre = "Science-Fiction", Annee = 2010 },
        new Film { Id = 2, Titre = "The Dark Knight", Genre = "Action", Annee = 2008 },
        new Film { Id = 3, Titre = "Interstellar", Genre = "Science-Fiction", Annee = 2014 },
        new Film { Id = 4, Titre = "Forrest Gump", Genre = "Drame", Annee = 1994 },
        new Film { Id = 5, Titre = "Gladiator", Genre = "Action", Annee = 2000 },
        new Film { Id = 6, Titre = "La La Land", Genre = "Comédie Musicale", Annee = 2016 }
    };

    public IActionResult Index(string? genre = null)
    {
        var films = string.IsNullOrEmpty(genre)
            ? Films
            : Films.Where(f => f.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase)).ToList();

        return View(films);
    }

    public IActionResult IndexTriCroissant()
    {
        var films = Films.OrderBy(f => f.Titre).ToList();
        return View("Index", films);
    }

    public IActionResult Details(int id)
    {
        var film = Films.FirstOrDefault(f => f.Id == id);

        if (film is null)
        {
            TempData["Erreur"] = "Le film demandé n'existe pas.";
            return RedirectToAction("Index");
        }

        TempData["Info"] = $"Vous consultez les détails du film : {film.Titre}";
        return View(film);
    }

    public IActionResult APropos()
    {
        ViewData["TotalFilms"] = Films.Count;
        ViewBag.NbGenres = Films.Select(f => f.Genre).Distinct().Count();
        ViewData["PlusAncien"] = Films.Min(f => f.Annee);
        ViewData["PlusRecent"] = Films.Max(f => f.Annee);

        return View();
    }

    [HttpGet]
    public IActionResult Create() => View();

    [HttpPost]
    public IActionResult Create(Film film)
    {
        if (!ModelState.IsValid)
        {
            TempData["Erreur"] = "Le formulaire contient des erreurs.";
            return View(film);
        }

        film.Id = Films.Max(f => f.Id) + 1;
        Films.Add(film);

        TempData["success"] = $"Le film « {film.Titre} » a été ajouté avec succès.";
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var film = Films.FirstOrDefault(f => f.Id == id);

        if (film is null)
        {
            TempData["Erreur"] = "Impossible de supprimer : film introuvable.";
            return RedirectToAction("Index");
        }

        Films.Remove(film);

        TempData["success"] = $"Le film « {film.Titre} » a été supprimé avec succès.";
        return RedirectToAction("Index");
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewBag.Genres = Films.Select(f => f.Genre).Distinct().OrderBy(g => g);
        base.OnActionExecuting(context);
    }
}

