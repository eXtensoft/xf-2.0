// <copyright company="eXtensoft, LLC" file="CertificateViewModel.cs">
// Copyright © 2016 All Right Reserved
// </copyright>

namespace Cyclops.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    public sealed class CertificateViewModel : ViewModel<Certificate>
    {
        [ScaffoldColumn(false)]
        public int CertificateId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Domain")]
        public string Domain { get; set; }
        [Required]
        [Display(Name = "Begin")]
        public DateTime Begin { get; set; }
        [Required]
        [Display(Name = "End")]
        public DateTime End { get; set; }

        public CertificateViewModel()
        {

        }
        public CertificateViewModel(Certificate model)
        {
            CertificateId = model.CertificateId;
            Name = model.Name;
            Domain = model.Domain;
            Begin = model.Begin;
            End = model.End;
        }

        public override bool IsValid()
        {
            bool b = true;
            b = (b && !String.IsNullOrWhiteSpace(Name)) ? true : false;
            b = (b && !String.IsNullOrWhiteSpace(Domain)) ? true : false;
            return b;
        }

        public override Certificate ToModel()
        {
            Certificate model = new Certificate();
            model.CertificateId = CertificateId;
            model.Name = Name;
            model.Domain = Domain;
            model.Begin = Begin;
            model.End = End;
            return model;
        }
    }

}
