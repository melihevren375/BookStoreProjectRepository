using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects.BookDataTransferObjects
{
    public abstract record BookDtoForManipulation
    {
        public Guid Id { get; init; }

        [Required(ErrorMessage = "Title boş olamaz.")]
        [MaxLength(100, ErrorMessage = "Title max 100 karakter olmalı.")]
        [MinLength(2, ErrorMessage = "Title min 2 karakter olmalı.")]
        public string Title { get; init; }
       
        [Required(ErrorMessage = "Price boş olamaz.")]
        [Range(10, 1000, ErrorMessage = "Fiyat 10 ile 1000 arasında olmalı.")]
        public decimal Price { get; init; }

        [Required(ErrorMessage = "Stock is a required field and cannot be empty.")]
        [Range(0, 500, ErrorMessage = "Stock must be between 0 and 500")]
        public uint Stock { get; init; }

    }
}
