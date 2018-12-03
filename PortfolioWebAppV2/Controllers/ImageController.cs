using PortfolioWebAppV2.Models.DatabaseModels;
using PortfolioWebAppV2.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace PortfolioWebAppV2.Controllers
{
    public class ImageController : Controller
    {
        private ImageRepository _repository;

        public ImageController(ImageRepository repo)
        {
            _repository = repo;
        }

        public ActionResult IconsManagement()
        {
            IEnumerable<Image> images = _repository.GetAll();

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
    }
}