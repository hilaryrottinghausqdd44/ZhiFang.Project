layui.extend({
	uxutil: 'ux/util',
}).use(['uxutil', 'form'], function () {
	var $ = layui.$,
		form = layui.form,
        uxutil = layui.uxutil;

	//获取新闻消息列表服务地址
    var GET_NEWS_INFO_URL = uxutil.path.ROOT + "/ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileById?isPlanish=true";
	//外部参数
    var PARAMS = uxutil.params.get(true);
    var NewId = PARAMS.ID;
    //获取新闻信息
    function getNewInfo(callback) {
        var url = GET_NEWS_INFO_URL + '&isAddFFileReadingLog=1&isAddFFileOperation=0&id=' + NewId +
            "&fields=FFile_Title,FFile_Memo,FFile_Content";

		uxutil.server.ajax({
			url:url
        }, function (data) {
            if (data.success) {
                var info = data.value || [];
                $("#outlineInfo").html(info.FFile_Memo);
                $("#detailInfo").html(info.FFile_Content);
                if (typeof callback == 'function') callback();
			}else{
				layer.msg(data.msg);
			}
		});
    };
    getNewInfo();
});