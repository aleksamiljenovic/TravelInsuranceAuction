using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelInsuranceAuction.Models
{
    public class AutoBiddingSetting
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Upišite početnu cenu")]
        [DisplayName("Početna cena u €")]
        public double DefaultStartPrice { get; set; }

        [Required(ErrorMessage = "Upišite minimalnu cenu")]
        [DisplayName("Minimalna cena u €")]
        public double DefaultMinPrice { get; set; }

        [Required(ErrorMessage = "Upišite procenat smanjenja")]
        [DisplayName("Procenat smanjenja")]
        public double PriceDecrease  { get; set; }

        [Required(ErrorMessage = "Upišite vreme za koje će se cena smanjiti")]
        [DisplayName("Interval smanjivanja")]
        public int LoweringTime { get; set; }

        [DisplayName("Specifični uslovi")]
        public string? SpecialConditions { get; set; }
        public int? AgencyId { get; set; }
        [ForeignKey("AgencyId")]
        [ValidateNever]
        public IncuranceAgency Agency { get; set; }

    }
}
