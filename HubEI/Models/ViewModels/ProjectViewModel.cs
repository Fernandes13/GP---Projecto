using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HubEI.Models.ViewModels
{
    public class ProjectViewModel
    {
        [Required(ErrorMessage = "É necessário definir o título do projecto!")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "É necessário definir a descrição do projecto!")]
        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "É necessário introduzir o relatório do projecto!")]
        [Display(Name = "Relatório")]
        public byte[] Report { get; set; }

        [Display(Name = "Data")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required(ErrorMessage = "É necessário definir a data do projecto!")]
        public DateTime ProjectDate { get; set; }

        [Display(Name = "Visível")]
        public byte IsVisible { get; set; }

        [Display(Name = "Tipo de Projecto")]
        [Required(ErrorMessage = "É necessário definir o tipo de projecto!")]
        public long IdProjectType { get; set; }

        [Display(Name = "Estudante")]
        [Required(ErrorMessage = "É necessário definir o tipo de projecto!")]
        public long IdStudent { get; set; }

        [Display(Name = "Empresa")]
        public long IdCompany { get; set; }

        public IEnumerable<SelectListItem> Companies { get; set; }
        public IEnumerable<SelectListItem> Students { get; set; }
        public IEnumerable<SelectListItem> ProjectTypes { get; set; }
    }
}