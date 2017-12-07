using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeCenter.Models;

namespace TradeCenter.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private TradeCenterDB _context = new TradeCenterDB();

        //
        // GET: Product/Add
        public ActionResult Add()
        {
            return View();
        }

        //
        // POST: /Product/Add
        [HttpPost]
        public ActionResult Add(Product product)
        {
            product.Owner = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            //_context.Users.Attach(product.Owner);    
            product.DatePublished = DateTime.Now.Date;

            SetPictures(product, Request.Files);

            _context.Products.Add(product);

            _context.SaveChanges();

            Messages.Set(AppMessages.ItemAdded);

            return RedirectToAction("Index", "Home");
        }

        //
        // GET: Product/Details/?productID={Product ID}
        [AllowAnonymous]
        public ActionResult Details(int productID)
        {
            var product = _context.Products.Include("Owner").SingleOrDefault(p => p.ID == productID);
            if (product != null)
                return View(product);
            else
                return HttpNotFound();
        }

        //
        // GET: Product/Edit/?productID={Product ID}
        public ActionResult Edit(int productID)
        {
            var product = _context.Products.SingleOrDefault(p => p.ID == productID);
            if (product != null)
                return View(product);
            else
                return HttpNotFound();
        }

        //
        // POST: /Product/Edit
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Product current = _context.Products.SingleOrDefault(p => p.ID == product.ID);

            current.Title = product.Title;
            current.ShortDescription = product.ShortDescription;
            current.LongDescription = product.LongDescription;
            current.Price = product.Price;
            current.DateEdited = DateTime.Now.Date;

            SetPictures(current, Request.Files);

            _context.SaveChanges();

            Messages.Set(AppMessages.ItemSaved);

            return RedirectToAction("Details", "Product", new { productID = product.ID });
        }

        //
        // GET: Product/Delete/?productID={Product ID}
        public ActionResult Delete(int productID)
        {
            var product = _context.Products.SingleOrDefault(p => p.ID == productID);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            else
                return HttpNotFound();

            Messages.Set(AppMessages.ItemDeleted);

            return RedirectToAction("Index", "Home");
        }

        //
        // POST: Product/DeletePicture/?productID={Product ID}&pictureIndex={index}
        [HttpPost]
        public void DeletePicture(int productID, int pictureIndex)
        {
            var product = _context.Products.SingleOrDefault(p => p.ID == productID);
            if (product != null)
            {
                switch (pictureIndex)
                {
                    case 0:
                        product.Picture1 = null;
                        product.Picture1_MimeType = null;
                        break;
                    case 1:
                        product.Picture2 = null;
                        product.Picture2_MimeType = null;
                        break;
                    case 2:
                        product.Picture3 = null;
                        product.Picture3_MimeType = null;
                        break;
                    default:
                        break;
                }

                _context.SaveChanges();
            }
        }

        //
        // GET: Product/AddToCart/?productID={Product ID}
        public ActionResult AddToCart(int productID)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);

            var product = _context.Products.SingleOrDefault(p => p.ID == productID);
            if (product != null)
            {
                product.State = ProductState.InCart;
                product.UserID = user.ID;

                _context.SaveChanges();

                Messages.Set(AppMessages.ItemAddedToCart);

                return RedirectToAction("Index", "Home");
            }
            else
                return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult GetPictureByProductID(int productID, int pictureIndex = 0)
        {
            byte[] picture = null;
            string pictureMimeType = string.Empty;

            var product = _context.Products.SingleOrDefault(p => p.ID == productID);
            switch (pictureIndex)
            {
                case 0:
                    picture = product.Picture1;
                    pictureMimeType = product.Picture1_MimeType;
                    break;
                case 1:
                    picture = product.Picture2;
                    pictureMimeType = product.Picture2_MimeType;
                    break;
                case 2:
                    picture = product.Picture3;
                    pictureMimeType = product.Picture3_MimeType;
                    break;
                default:
                    break;
            }

            if (picture == null)
                return File("~/Content/Images/missingImage.png", "image/png");

            return File(picture, pictureMimeType);
        }

        private void SetPictures(Product product, HttpFileCollectionBase files)
        {
            byte[] buffer = null;

            if (files.Count > 0)
            {
                if (Image2ByteArray(files[0], out buffer))
                {
                    product.Picture1 = buffer;
                    product.Picture1_MimeType = buffer != null ? files[0].ContentType : null;
                }
            }

            if (files.Count > 1)
            {
                if (Image2ByteArray(files[1], out buffer))
                {
                    product.Picture2 = buffer;
                    product.Picture2_MimeType = buffer != null ? files[1].ContentType : null;
                }
            }

            if (files.Count > 2)
            {
                if (Image2ByteArray(files[2], out buffer))
                {
                    product.Picture3 = buffer;
                    product.Picture3_MimeType = buffer != null ? files[2].ContentType : null;
                }
            }
        }

        // Returns true if input file contains an image
        private bool Image2ByteArray(HttpPostedFileBase file, out byte[] buffer)
        {
            buffer = null;

            if (file.ContentType.Contains("image"))
            {
                buffer = new byte[file.ContentLength];

                int totalBytesCount = 0;
                while (totalBytesCount < buffer.Length)
                {
                    int bytesCount = file.InputStream.Read(buffer, totalBytesCount, buffer.Length - totalBytesCount);
                    if (bytesCount == 0)
                        break;
                    totalBytesCount += bytesCount;
                }

                // In case of an error/incomplete read - reset buffer to null
                if (totalBytesCount < buffer.Length)
                    buffer = null;

                return true;
            }

            return false;
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
            base.Dispose(disposing);
        }

        ~ProductController()  // Overrides Object.Finalize()
        {
            Dispose();
        }
    }
}