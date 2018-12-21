using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ImageController : Controller
    {
        private ImageRepository _repository;

        public ImageController(ImageRepository repo)
        {
            _repository = repo;
        }

        public ActionResult IconsManagement()
        {
            IEnumerable<Image> images = _repository.GetAll().Where(i => i.ImageType == ImageType.Icon);

            return View(images);
        }

        public ActionResult UploadIcon(HttpPostedFileBase file)
        {
            if (!file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("CustomError", @"Wgrywany plik nie jest plikiem graficznym."); //error += "\nWgrywany plik nie jest plikiem graficznym.";
            }
            else
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream, true, true);
                if (img.Width > 150 || img.Height > 150) ModelState.AddModelError("CustomError", @"Maksymalny rozmiar ikony to 150x150px.");//error += "Maksymalny rozmiar ikony to 150x150px.";
                if (img.Width < 50 || img.Height < 50) ModelState.AddModelError("CustomError", @"Minimalny rozmiar ikony to 50x50px."); //error += "\nMinimalny rozmiar ikony to 50x50px.";
            }

            file.InputStream.Seek(0, SeekOrigin.Begin);

            if (file.ContentLength > 0 && ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();

                Image image = new Image()
                {
                    FileName = file.FileName,
                    ImageType = ImageType.Icon,
                    Favorite = false,
                    Guid = guid.ToString()
                };
                BlobConnector.UploadIcon(file, image);
                _repository.Add(image);

                return RedirectToAction("IconsManagement");
            }
            else ModelState.AddModelError("CustomError", @"Błąd wgrywania pliku."); //error += "\nBłąd wgrywania pliku";


            return View("IconsManagement", _repository.GetAll());
        }

        public ActionResult RemoveIcon(int id)
        {
            Image image = _repository.Get(id);
            if (image == null)
            {
                ModelState.AddModelError("CustomError", @"Nie znaleziono ikony o id " + id.ToString());

                return View("IconsManagement", _repository.GetAll());
            }
            if (!BlobConnector.RemoveIcon(image))
            {
                ModelState.AddModelError("CustomError", @"Nie znaleziono na serwerze ikony o nazwie: " + image.Guid + image.FileName);

                return View("IconsManagement", _repository.GetAll());
            }
            if (!_repository.Remove(image))
            {
                ModelState.AddModelError("CustomError", @"Błąd usuwania ikony z bazy danych");

                return View("IconsManagement", _repository.GetAll());
            }


            return RedirectToAction("IconsManagement");
        }

        public ActionResult ProjectsList()
        {
            IEnumerable<Project> list = _repository.GetAllProjects();

            return View(list);
        }

        public ActionResult ScreenshotsManagement(int projectId)
        {
            var project = _repository.GetProject(projectId);

            return View(project);
        }

        public ActionResult UploadScreenshot(HttpPostedFileBase file, int projectId)
        {
            if (!file.ContentType.Contains("image"))
            {
                ModelState.AddModelError("CustomError", @"Wgrywany plik nie jest plikiem graficznym."); //error += "\nWgrywany plik nie jest plikiem graficznym.";
            }
            else
            {
                System.Drawing.Image img = System.Drawing.Image.FromStream(file.InputStream, true, true);
                if (img.Width > 5000 || img.Height > 5000) ModelState.AddModelError("CustomError", @"Maksymalny rozmiar zrzutu ekranu to 5000x5000px.");//error += "Maksymalny rozmiar ikony to 150x150px.";
                if (img.Width < 50 || img.Height < 50) ModelState.AddModelError("CustomError", @"Minimalny rozmiar zrzutu ekranu to 50x50px."); //error += "\nMinimalny rozmiar ikony to 50x50px.";
            }

            file.InputStream.Seek(0, SeekOrigin.Begin);

            if (file.ContentLength > 0 && ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();

                Image image = new Image()
                {
                    FileName = file.FileName,
                    ImageType = ImageType.ScreenShot,
                    Favorite = false,
                    Guid = guid.ToString()
                };
                BlobConnector.UploadScreenshot(file, image);
                _repository.AddScreenshot(image, projectId);
            }
            else ModelState.AddModelError("CustomError", @"Błąd wgrywania pliku."); //error += "\nBłąd wgrywania pliku";


            return View("ScreenshotsManagement", _repository.GetAllProjects().First(p => p.ProjectId == projectId));
        }

        public ActionResult RemoveScreenshot(int id, int projectId)
        {
            Image image = _repository.Get(id);
            if (image == null)
            {
                ModelState.AddModelError("CustomError", @"Nie znaleziono zrzutu ekranu o id " + id.ToString());

                return View("ScreenshotsManagement", _repository.GetAllProjects().First(p => p.ProjectId == projectId));
            }
            if (!BlobConnector.RemoveScreenshot(image))
            {
                ModelState.AddModelError("CustomError", @"Nie znaleziono na serwerze zrzutu ekranu o nazwie: " + image.Guid + image.FileName);

                return View("ScreenshotsManagement", _repository.GetAllProjects().First(p => p.ProjectId == projectId));
            }
            if (!_repository.RemoveScreenshot(id, projectId))
            {
                ModelState.AddModelError("CustomError", @"Błąd usuwania zrzutu ekranu z bazy danych");

                return View("ScreenshotsManagement", _repository.GetAllProjects().First(p => p.ProjectId == projectId));
            }


            return View("ScreenshotsManagement", _repository.GetAllProjects().First(p => p.ProjectId == projectId));

        }
    }
}