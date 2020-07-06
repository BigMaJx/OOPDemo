using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Alipay
{
    /// <summary>
    /// 支付宝支付类
    /// </summary>
    public class AliPay
    {
        private static readonly string AppId = MyConfigs.GetConfig("AppId");
        private static readonly string AppPrivateKey = MyConfigs.GetConfig("PrivateKey");
        private static readonly string AliPublicKey = MyConfigs.GetConfig("AliPublicKey");
        private static readonly string NotifyUrl = MyConfigs.GetConfig("NotifyUrl");

        public const string Gateway = "https://openapi.alipay.com/gateway.do";
        public const string Format = "json";
        public const string Version = "1.0";
        public const string SignType = "RSA2";
        public const string Charset = "utf-8";
        public const string ProductCode = "QUICK_MSECURITY_PAY";
        
        static IAopClient aopClient = new DefaultAopClient(Gateway, AppId, AppPrivateKey, Format, Version, SignType, AliPublicKey, Charset, false);


        /// <summary>
        /// 获取扫码支付的二维码图片链接
        /// </summary>
        /// <param name="orderNo">商户订单号,64个字符以内、只能包含字母、数字、下划线；需保证在商户端不重复</param>
        /// <param name="amount">订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000] 如果同时传入了【打折金额】，【不可打折金额】，【订单总金额】三者，则必须满足如下条件：【订单总金额】=【打折金额】+【不可打折金额】</param>
        /// <param name="subject">订单标题</param>
        /// <param name="body">商品描述信息</param>
        /// <param name="storeId">商户门店编号</param>
        /// <returns></returns>
        public string GetQrCodeUrl(string orderNo, decimal amount, string subject, string body, string storeId)
        {
            string link = string.Empty;

            //组装业务参数
            var model = new AlipayTradePrecreateModel()
            {
                Body = body,
                Subject = subject,
                TotalAmount = amount.ToString(),
                OutTradeNo = orderNo,
                StoreId = storeId //商户门店编号
            };

            var request = new AlipayTradePrecreateRequest();
            request.SetBizModel(model);
            request.SetNotifyUrl(NotifyUrl);

            AlipayTradePrecreateResponse response = aopClient.Execute(request);
            if (response != null && response.Code == "10000")
            {
                link = response.QrCode;
            }
            return link;
        }

        /// <summary>
        /// 申请退款
        /// </summary>
        public bool ApplyRefund(string orderNo, decimal refundAmount, string refundNo = null, string reason = null, string tradeNo = null)
        {
            bool success = false;

            //组装业务参数
            var model = new AlipayTradeRefundModel()
            {
                OutTradeNo = orderNo,
                TradeNo = tradeNo,
                RefundAmount = refundAmount.ToString(),
                OutRequestNo = refundNo, //退款单号，同一笔多次退款需要保证唯一，部分退款该参数必填。
                RefundReason = reason
            };

            var request = new AlipayTradeRefundRequest();
            request.SetBizModel(model);
            AlipayTradeRefundResponse response = aopClient.Execute(request);
            if (response != null && response.Code == "10000")
            {
                success = true;
            }
            return success;
        }
    }
}
