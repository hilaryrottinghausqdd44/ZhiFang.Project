/**
   @Name：表单项配置--客户
   @Author：guohx
   @version 2020-07-29
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/system/setting/formItem_client/main/mainTable'
}).use(['uxutil', 'table', 'form', 'mainTable'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        uxutil = layui.uxutil,
        mainTable = layui.mainTable;
    //全局变量
    var config = {
        params: {
            isSearch: false,
            systemHost: '' // 各个平台的systemHost类似于'ZhiFang.LabInformationIntegratePlatform'
        },
        //用户信息
        userInfo: {
            EmpId: uxutil.cookie.get(uxutil.cookie.map.USERID),//当前账户Id
            EmpName: uxutil.cookie.get(uxutil.cookie.map.USERNAME)//当前账户名
        },
        getLaboratoryUrl: '/ServerWCF/DictionaryService.svc/ST_UDTO_SearchBEmpLaboratoryLinkByHQL?isPlanish=true',//获得实验室
    };
    //初始化
    init();
    //初始化
    function init() {
        //获得参数
        getParams();
        //初始化列表
        initMainTable();
        //查询
        if (String(config.params.isSearch) == "true") {
            setTimeout(function () {
            	var obj = {
            		systemHost: config.params.systemHost
            	};
                mainTable.onSearch('mainTable',obj);
            }, 100);
        }
        //监听联动
        initGroupListeners();
    }
    //获得参数
    function getParams() {
        var params = uxutil.params.get();
        config.params = $.extend({}, config.params, params);
    }
    //联动
    function initGroupListeners() {
        layui.onevent('leftTableSarch', 'search', function (obj) {
            if (parent.layui.mainTable) parent.layui.mainTable.onSearch('mainTable');
        });
    }
    //页面表单配置项’
    function initMainTable() {
        var Obj = {
            elem: '#mainTable',
            title: '',
            height: 'full-30',
            id: 'mainTable'
        };
        mainTable.render(Obj);
    }
    //获得所有实验室
    function initLaboratory() {
        var index = layer.load();
        var url = uxutil.path.ROOT + config.getLaboratoryUrl + '&where=IsUse=1 and EmpId=' + config.userInfo.EmpId + '&fields=BEmpLaboratoryLink_Id,BEmpLaboratoryLink_LabId,BEmpLaboratoryLink_LabNo,BEmpLaboratoryLink_LabName&sort=[{property:"BEmpLaboratoryLink_LabNo",direction:"ASC"}]';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            layer.close(index);
            if (data && data.ResultDataValue) {
                //var list = JSON.parse(data.ResultDataValue).list || [],
                //    str = '<option value="">请选择</option>';
                //$.each(list, function (i, itemI) {
                //    str += '<option value="' + itemI.BEmpLaboratoryLink_LabId + '">' + itemI.BEmpLaboratoryLink_LabName + '</option>';
                //});
                //updateSelect('Hospital', str);
            } else {
                layer.msg(data.msg);
            }
        });
    };
});