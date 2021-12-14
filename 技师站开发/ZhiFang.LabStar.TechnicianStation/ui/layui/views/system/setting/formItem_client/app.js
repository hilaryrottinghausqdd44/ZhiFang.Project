/**
   @Name：表单项配置--客户
   @Author：guohx
   @version 2020-07-29
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/system/setting/formItem_client/mainTable'
}).use(['uxutil', 'table', 'form', 'mainTable'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        uxutil = layui.uxutil,
        mainTable = layui.mainTable;
    //全局变量
    var config = { };
    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
        //加载iframe
        setTimeout(function () {
            var IframeHeight = ($(window).height() - 34) + "px";
            var IUrl = uxutil.path.ROOT + '/ui/layui/views/system/setting/formItem_client/main/app.html';
            var html = '<iframe src="' + IUrl + '" id="rightTableIframe" name="rightTableIframe" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" width="100%" height="' + IframeHeight + '">您的浏览器不支持嵌入式框架，或者当前配置为不显示嵌入式框架。</iframe >';
            $("#IframeBox").html(html);
        }, 0);
        //初始化列表
        initMainTable();
        setTimeout(function () {
            mainTable.onSearch('mainTable');
        }, 100);
    }
    //联动
    function initGroupListeners() {
        //单击左列表查询右列表
        layui.onevent('rightTableOnSearch', 'search', function (obj) {
            var IFrame = $("#rightTableIframe")[0].contentWindow;
            if (IFrame.layui.mainTable)
                IFrame.layui.mainTable.onSearch('mainTable', { FormCode: obj.BModuleFormList_FormCode ,systemHost: uxutil.path.ROOT});
            else
                setTimeout(function () {
                    IFrame.layui.mainTable.onSearch('mainTable', { FormCode: obj.BModuleFormList_FormCode ,systemHost: uxutil.path.ROOT});
                }, 500);
        });
        //左列表无数据查询右列表
        layui.onevent('noData', 'noData', function (obj) {
            var IFrame = $("#rightTableIframe")[0].contentWindow;
            if (IFrame.layui.mainTable)
                IFrame.layui.mainTable.onSearch('mainTable', { FormCode: null ,systemHost: uxutil.path.ROOT});
            else
                setTimeout(function () {
                    IFrame.layui.mainTable.onSearch('mainTable', { FormCode: null ,systemHost: uxutil.path.ROOT});
                }, 500);
        });
    }
    //页面表单配置项’
    function initMainTable() {
        var Obj = {
            elem: '#mainTable',
            title: '',
            height: 'full-70',
            id: 'mainTable'
        };
        mainTable.render(Obj);
    };
});