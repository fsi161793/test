using System;

namespace SportsStore.LCW.WebUI.Models {
    /// <summary>
    /// 分页信息
    /// </summary>
    public class PagingInfo {

        /// <summary>
        /// 数据项目总数
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// 单页项目总数
        /// </summary>
        public int ItemsPerPage { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 总页码(数据项目总数/单页项目总数)
        /// </summary>
        public int TotalPages {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

    }

}