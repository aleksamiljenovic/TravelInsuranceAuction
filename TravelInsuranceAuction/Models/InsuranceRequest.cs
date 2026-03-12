using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelInsuranceAuction.Models
{
    public class InsuranceRequest
    {
        [Key]
        public int Id {  get; set; }
      
        [Required(ErrorMessage ="Izaberite lokaciju")]
        [DisplayName("Destinacija")]
        public string Destination {  get; set; }

        [Required(ErrorMessage = "Izaberite datum polaska")]
        [DisplayName("Datum polaska")]
        public DateOnly? StartDate {  get; set; }

        [Required(ErrorMessage = "Izaberite datum povratka")]
        [DisplayName("Datum povratka")]
        public DateOnly? EndDate {  get; set; }

        [Required(ErrorMessage ="Izaberite broj putnika")]
        [Range(1,50,ErrorMessage ="Broj putnika mora biti izmedju 1 i 50")]
        [DisplayName("Broj putnika")]
        public int? NumberOfTravelers {  get; set; }

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }


    }
}
