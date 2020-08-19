using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Primeiros_Passos.Models
{
    public class Guia
    {
        public int GuiaId { get; set; }

        public string Titulo { get; set; }

        public string descrição { get; set; }

        public string Imagem { get; set; }

        [NotMapped]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImagemUpload { get; set; }

        public int Pos { get; set; }

        public bool Premium { get; set; }

        public string Language { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}