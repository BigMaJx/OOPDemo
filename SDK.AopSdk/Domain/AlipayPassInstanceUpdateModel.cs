using System;
using System.Xml.Serialization;

namespace Aop.Api.Domain
{
    /// <summary>
    /// AlipayPassInstanceUpdateModel Data Structure.
    /// </summary>
    [Serializable]
    public class AlipayPassInstanceUpdateModel : AopObject
    {
        /// <summary>
        /// 代理商代替商户发放卡券后，再代替商户更新卡券时，此值为商户的pid/appid
        /// </summary>
        [XmlElement("channel_id")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 商户指定卡券唯一值
        /// </summary>
        [XmlElement("serial_number")]
        public string SerialNumber { get; set; }

        /// <summary>
        /// 券状态，支持更新为USED、CLOSED两种状态
        /// </summary>
        [XmlElement("status")]
        public string Status { get; set; }

        /// <summary>
        /// 模版动态参数信息【支付宝pass模版参数键值对JSON字符串】
        /// </summary>
        [XmlElement("tpl_params")]
        public string TplParams { get; set; }

        /// <summary>
        /// 核销码串值【当状态变更为USED时，建议传】
        /// </summary>
        [XmlElement("verify_code")]
        public string VerifyCode { get; set; }

        /// <summary>
        /// 核销方式，目前支持：wave（声波方式）、qrcode（二维码方式）、barcode（条码方式）、input（文本方式，即手工输入方式）。pass和verify_type不能同时为空
        /// </summary>
        [XmlElement("verify_type")]
        public string VerifyType { get; set; }
    }
}
