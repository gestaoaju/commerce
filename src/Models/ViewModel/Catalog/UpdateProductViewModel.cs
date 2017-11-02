/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.ComponentModel.DataAnnotations;

namespace Gestaoaju.Models.ViewModel.Catalog
{
    public class UpdateProductViewModel : CreateProductViewModel
    {
        [Required]
        public int? Id { get; set; }
    }
}
