namespace VladyslavOrlovPromo.DataAccess
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        public byte[] Content { get; set; }

        public bool? IsActive { get; set; }

        public byte[] Image { get; set; }
    }
}