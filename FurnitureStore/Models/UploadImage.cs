using System.ComponentModel.DataAnnotations;

namespace FurnitureStore.Models
{
    public class UploadImage
    {
        #region Properties
        public int Id { get; set; }

        [StringLength(550)]
        public string OldName { get; set; }

        [StringLength(550)]
        public string NewName { get; set; }

        [StringLength(500)]
        public string ImagePath { get; set; }
        public byte[] ImageBytes { get; set; }
        public int ProductId { get; set; }
        #endregion

        #region Navigation Properties
        public Product Product { get; set; }
        #endregion
    }
}