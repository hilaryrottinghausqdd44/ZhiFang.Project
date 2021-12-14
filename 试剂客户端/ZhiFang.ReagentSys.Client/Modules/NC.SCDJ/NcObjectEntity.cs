using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NC.SCDJ
{
    public class NcObjectEntity
    {
    }

    /// <summary>
    /// NC货品实体
    /// </summary>
    public class NcGoods
    {
        /// <summary>
        /// 货品编码
        /// </summary>
        public string INVCODE { get; set; }

        /// <summary>
        /// 厂商产品编码（助记码）
        /// </summary>
        public string INVMNECODE { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string INVNAME { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string FORINVNAME { get; set; }

        /// <summary>
        /// 简码
        /// </summary>
        public string INVSHORTNAME { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string INVPINPAI { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string INVSPEC { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public string INVTYPE { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string MEASNAME { get; set; }

        /// <summary>
        /// 是否医疗器械
        /// </summary>
        public string ISMED { get; set; }

        /// <summary>
        /// 装量(测试数)
        /// </summary>
        public int? ZL { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime? TS { get; set; }

        /// <summary>
        /// 注册证号
        /// </summary>
        public string VAPPROVALNUM { get; set; }

        /// <summary>
        /// 注册证到期日期
        /// </summary>
        public DateTime? VDOTECHNICS { get; set; }

        /// <summary>
        /// 产地
        /// </summary>
        public string VPRODUCINGAREA { get; set; }

        /// <summary>
        /// 储存条件
        /// </summary>
        public string VMEDNATURALCODE { get; set; }

        /// <summary>
        /// 挂网流水号
        /// </summary>
        public string GWLS { get; set; }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public int PK_CORP { get; set; }

        /// <summary>
        /// 停止/启用
        /// </summary>
        public string VISIBLE { get; set; }

        /// <summary>
        /// 参考价格
        /// </summary>
        public double? PRICE { get; set; }

    }

    /// <summary>
    /// NC出库单
    /// </summary>
    public class NcOutOrder
    {
        /// <summary>
        /// 规格
        /// </summary>
        public string INVSPEC { get; set; }

        /// <summary>
        /// 供货商(如仪器公司1002)
        /// </summary>
        public string GHS_ID { get; set; }

        /// <summary>
        /// 货品编码
        /// </summary>
        public string INVCODE { get; set; }

        /// <summary>
        /// 货品名称
        /// </summary>
        public string INVNAME { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string MEASNAME { get; set; }

        /// <summary>
        /// NC单据号
        /// </summary>
        public string NC_BILLNO { get; set; }

        /// <summary>
        /// ZF订货单号
        /// </summary>
        public string ZF_BILLNO { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string INVPINPAI { get; set; }

        /// <summary>
        /// 出库数量
        /// </summary>
        public string NOUTNUM { get; set; }

        /// <summary>
        /// 助记码
        /// </summary>
        public string INVMNECODE { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? DVALIDATE { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string VBATCHCODE { get; set; }

        /// <summary>
        /// 订货方名称
        /// </summary>
        public string DHF_NAME { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime? DPRODUCEDATE { get; set; }

        /// <summary>
        /// 某商品含税单价
        /// </summary>
        public double? NPRICE { get; set; }

        /// <summary>
        /// 某商品含税总价=某商品含税单价*出库数量
        /// </summary>
        public double? NMNY { get; set; }
        
    }

}
