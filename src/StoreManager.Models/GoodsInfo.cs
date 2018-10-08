using System;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.Models
{
    public class GoodsInfo
    {
        /// <summary>
        /// 数据主键
        /// </summary>
        [Key]
        public int TranNum { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsNo { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }
    }
}
