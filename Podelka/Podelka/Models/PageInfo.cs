using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Podelka.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; } // номер текущей страницы
        public int PageSize { get; set; } // кол-во объектов на странице
        public int TotalItems { get; set; } // всего объектов
        public int TotalPages  // всего страниц
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
        }
    }

    public class WorkroomsPaginationModel
    {
        public ICollection<WorkroomPreviewModel> Workrooms{ get; set; }
        public PageInfo PageInfo { get; set; }
    }


    public class ProductsPaginationModel
    {
        public ICollection<ProductPreviewModel> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }

    public class ProductsPaginationModelBookmarks
    {
        public ICollection<ProductPreviewModel> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public long UserId { get; set; }
    }

    public class ProductsPaginationModelWorkroomProfile
    {
        public ICollection<ProductPreviewModel> Products { get; set; }
        public PageInfo PageInfo { get; set; }
        public long WorkroomId { get; set; }
    }

    public class WorkroomsPaginationModelUserProfile
    {
        public ICollection<WorkroomPreviewModel> Workrooms { get; set; }
        public PageInfo PageInfo { get; set; }
        public long UserId { get; set; }
    }
    
}