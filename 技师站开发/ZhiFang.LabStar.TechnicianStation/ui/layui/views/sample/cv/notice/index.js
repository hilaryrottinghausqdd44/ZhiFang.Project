/**
   @Name：危急值发送
   @Author：zhangda
   @version 2021-09-06
 */
layui.extend({
    uxutil: 'ux/util',
    notice: 'views/sample/cv/basic/notice',
}).use(['uxutil', 'layer','notice'], function () {
    "use strict";
    var $ = layui.$,
        layer = layui.layer,
        notice = layui.notice,
        uxutil = layui.uxutil;


    //外部参数
    var PARAMS = uxutil.params.get(true);
    //检验单ID
    var TESTFORMID = PARAMS.TESTFORMID;
    //发送实例
    var NoticeInstance = null;

    //初始化页面
    function initHtml() {
        //初始化危急值发送列表
        NoticeInstance = notice.render({
            domId: "CVNotice",
            height: 'full-200',
            //模式 1：查看界面 2：检验界面调用
            model: 2,
            where: "MsgType=1 and ReportStatus=1 and LisTestForm.Id='" + TESTFORMID +"'"
        });
        //初始化监听
        initListeners();
    };


    //监听
    function initListeners() {
        //监听发送成功事件
        layui.onevent('notice', 'save', function (data) {
            var noticeTableCache = data["tableCache"] || [],
                flag = true;
            $.each(noticeTableCache, function (i, item) {
                if (item["LisTestFormMsg_PhoneStatus"] == 0) {
                    flag = false;
                    return false;
                }
            });
            if (flag) {
                var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                parent.layer.close(index); //再执行关闭
            }
        });
    };
    //初始化
    function init() {
        if (!TESTFORMID) {
            $("#CVNotice").html("缺少检验单参数!");
            return;
        }
        initHtml();
    };
    //初始化
    init();
});