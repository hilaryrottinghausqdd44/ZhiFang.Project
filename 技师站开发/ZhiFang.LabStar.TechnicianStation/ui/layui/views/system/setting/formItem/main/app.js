/**
   @Name：表单项配置
   @Author：zguohx
   @version 2020-07-29
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/system/setting/formItem/main/mainTable'
}).use(['uxutil', 'table', 'form', 'mainTable'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        uxutil = layui.uxutil,
        mainTable = layui.mainTable; 
    //全局变量
    var config = {
        params: {
            toolbar: '#Toolbar', defaultToolbar: 'filter', page: true, limit: 50, externalWhere: '', isHideColumnToolbar: false, isInitLaboratory: false, isInitComponentType: true,isSearch:false,isSaveSuccessLoadTable:true,
            systemHost: '' // 各个平台的systemHost类似于'ZhiFang.LabInformationIntegratePlatform'
        },
        GetClassDicListUrl: '/ServerWCF/CommonService.svc/GetClassDicList',//获得枚举
//      GetClassDicListUrl: '/ServerWCF/CommonService.svc/GetClassDicList',//获得枚举
//      getLaboratoryUrl: '/ServerWCF/ReportFormSASService.svc/GetBHospital'//获得实验室
    };
    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
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
        //初始化实验室下拉框
//      if (String(config.params.isInitLaboratory) == 'true') initLaboratory();
        //初始化组件类型
        if (String(config.params.isInitComponentType) == 'true') initComponentType();
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
            toolbar: String(config.params.toolbar) == 'true' ? true : (String(config.params.toolbar) == 'false' ? false : config.params.toolbar),
            page: String(config.params.page) == 'true' ? true : (String(config.params.page) == 'false' ? false : config.params.page),
            limit: config.params.limit,
            externalWhere: config.params.externalWhere,
            defaultToolbar: [config.params.defaultToolbar],
            id: 'mainTable'
        };
        mainTable.render(Obj, String(config.params.isHideColumnToolbar) == 'true', String(config.params.isSaveSuccessLoadTable) == "true");
    }
     //初始化实验室
    /*function initLaboratory() {
        var url = uxutil.path.ROOT + config.getLaboratoryUrl + '?where=IsUse=1&fields=Name,Id,LabID';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue) || [],
                    str = '<option value="">直接选择或搜索选择</option>';
                $.each(list, function (i, itemI) {
                    str += '<option value="' + itemI.Id + '" data-labno="' + itemI.LabID +'">(' + itemI.LabID + ")" + itemI.Name + '</option>';
                });
                $("#LabID").html(str);
                form.render("select");
            } else {
                layer.msg(data.msg);
            }
        });
    };*/
    //初始化组件类型
    function initComponentType() {
         var url = uxutil.path.ROOT + config.GetClassDicListUrl;
        uxutil.server.ajax({
            type: "POST",
            url: url,
            data: JSON.stringify({
                'jsonpara': [{
                	"classnamespace": "ZhiFang.Entity.Common",
                    "classname":"FormControl_Config"
                }]
            })
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue),
                    str = '';
                if (list.length > 0) {
                    $.each(list[0]["FormControl_Config"], function (i, itemI) {
                        str += '<option value="' + itemI.Id + '">' + itemI.Name + '</option>';
                    });
                }
                $("#TypeID").html(str);
                form.render("select");
            } else {
                layer.msg(data.msg);
            }
        });
    }
});