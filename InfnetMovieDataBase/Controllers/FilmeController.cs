using InfnetMovieDataBase.Domain;
using InfnetMovieDataBase.Repository;
using Microsoft.AspNetCore.Mvc;

namespace InfnetMovieDataBase.Controllers
{
    public class FilmeController : Controller
    {
        FilmeRepository repository = new FilmeRepository();
        // GET: Filme
        public ActionResult Index()
        {
            var filmes = repository.ListarFilmes();
            return View(filmes);
        }

        // GET: Filme/Details/5
        public ActionResult Details(int id)
        {
            var filmes = repository.DetalharFilme(id);

            return View(filmes);
        }

        // GET: Filme/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Filme/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Filme filme)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    repository.CriarFilme(filme);
                    return RedirectToAction(nameof(Index));
                }

                return View(filme);
            }
            catch
            {
                return View();
            }
        }

        // GET: Filme/Edit/5
        public ActionResult Edit(int id)
        {
            var filme = repository.DetalharFilme(id);

            return View(filme);
        }

        // POST: Filme/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Filme filme)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.AtualizarFilme(filme);

                    return RedirectToAction(nameof(Index));
                }

                return View(filme);
            }
            catch
            {
                return View();
            }
        }

        // GET: Filme/Delete/5
        public ActionResult Delete(int id)
        {
            var filme = repository.DetalharFilme(id);

            return View(filme);
        }

        // POST: Filme/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Filme filme)
        {
            repository.ExcluirFilme(filme.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}