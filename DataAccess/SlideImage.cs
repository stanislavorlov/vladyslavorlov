using System.ComponentModel.DataAnnotations.Schema;

namespace VladyslavOrlovPromo.DataAccess
{
    public class SlideImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageName { get; set; }

        public int OrderNumber { get; set; }

        public  byte[] Image { get; set; }
    }
}