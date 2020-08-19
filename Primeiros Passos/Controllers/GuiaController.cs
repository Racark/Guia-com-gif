using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Primeiros_Passos.DAL;
using Primeiros_Passos.Models;

namespace Primeiros_Passos.Controllers
{
    public class GuiaController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Guia
        public ActionResult Index(string lan = "pt")
        {
            return View(db.GuiaDb.Where(f => f.Language == lan).ToList());
        }

        public ActionResult Save(int id = 0)
        {
            Guia guia = new Guia();

            if(id > 0)
            {
                guia = db.GuiaDb.Find(id);
            }

            return View(guia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save([Bind(Include = "GuiaId,Titulo,descrição,ImagemUpload,Pos,Premium,Language,")] Guia guia)
        {
            if (ModelState.IsValid)
            {
                guia.UpdateTime = DateTime.Now;

                Guia busca = db.GuiaDb.Find(guia.GuiaId);

                if(busca != null)
                {
                    if (guia.ImagemUpload != null)
                    {
                        //Pega o nome da imagem/ e sua extensão
                        var imagemNome = String.Format("{0}", guia.GuiaId);
                        var extensao = System.IO.Path.GetExtension(guia.ImagemUpload.FileName).ToLower();

                        //Recebe lê a imagem
                        using (var img = Image.FromStream(guia.ImagemUpload.InputStream))
                        {
                            //Da a referencia da imagem
                            guia.Imagem = String.Format("/Imagens/{0}{1}", imagemNome, extensao);
                            // Salva imagem na pasta 
                            img.Save(Server.MapPath(guia.Imagem));
                        }

                    }
                    db.GuiaDb.AddOrUpdate(guia);
                }
                else
                {
                    if (guia.Titulo == null)
                    {
                        return RedirectToAction("Index");
                    }

                    else
                    {

                        busca = db.GuiaDb.Add(guia);

                        if (guia.ImagemUpload != null)
                        {
                            //Pega o nome da imagem/ e sua extensão
                            var imagemNome = String.Format("{0}", busca.GuiaId);
                            var extensao = System.IO.Path.GetExtension(guia.ImagemUpload.FileName).ToLower();

                            //Recebe lê a imagem
                            using (var img = Image.FromStream(guia.ImagemUpload.InputStream))
                            {
                                //Da a referencia da imagem
                                guia.Imagem = String.Format("/Imagens/{0}{1}", imagemNome, extensao);
                                // Salva imagem na pasta 
                                img.Save(Server.MapPath(guia.Imagem));
                            }
                        }

                        busca.Imagem = guia.Imagem;

                        db.GuiaDb.AddOrUpdate(busca);
                        
                    }
                }
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Up(int id)
        {
            Guia guia = db.GuiaDb.Find(id);
            guia.Pos--;
            db.Entry(guia).State = EntityState.Modified;
            return RedirectToAction("Index");
        }

        public ActionResult Down(int id)
        {
            Guia guia = db.GuiaDb.Find(id);
            guia.Pos++;
            db.Entry(guia).State = EntityState.Modified;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Guia guia = db.GuiaDb.Find(id);
            db.GuiaDb.Remove(guia);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
