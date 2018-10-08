using System;
using System.ComponentModel.DataAnnotations;

namespace StoreManager.Models
{
    /// <summary>
    /// 订单信息
    /// </summary>
    public class OrdersInfo
    {
        // <summary>
        /// 数据主键
        /// </summary>
        [Key]
        public int TranNum { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        public string GoodsNo { get; set; }
        /// <summary>
        /// 购买数量
        /// </summary>
        public int BuyCount { get; set; }
        /// <summary>
        /// 买主
        /// </summary>
        public string BuyerNo { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime InTime { get; set; }
    }
}
