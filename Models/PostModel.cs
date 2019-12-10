using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VladyslavOrlovPromo.Models
{
    public class PostModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [AllowHtml]
        [Required]
        public string Body { get; set; }
    }
}