/**
   @Name：模块表单表格配置应用关系
   @Author：guohx
   @version 2020-07-29
   @description：该模块通过role来区分用户的身份
   role为system(开发人员)：
   	可以完成对模块集合的新增（将模块里面的配置进行添加）和修改（修改集合的名称和显示次序，是否使用）；
   	可以完成集合里面配置项的设置（增删改---操作的是controlList）
     用户：
   	可以完成对集合里面配置项的设置（增删改（开发配置好的配置项）----操作的是controlSet）
 */
layui.extend({
    uxutil: 'ux/util',
    moduleTree: 'views/system/setting/module_form_grid_link/moduleTree',
    mainTable: 'views/system/setting/module_form_grid_link/mainTable'
}).use(['uxutil', 'table', 'form','tree', 'mainTable','moduleTree'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        tree = layui.tree,
        uxutil = layui.uxutil,
        moduleTree = layui.moduleTree,
        mainTable = layui.mainTable;
    //全局变量
    var config = {
    	systemList: [],
        role: '',//根据该属性判断是否可以操作  (system表示是开发配置，没有就是用户配置)
        list: {//集合中类型和样式
            FormCode: {
                type: [{ value: 1, text: "查询" }, { value: 2, text: "编辑" }, { value: 3, text: "交互" }],
                class: [{ value: 1, text: "普通" }, { value: 2, text: "收缩" }]
            },
            GridCode: {
                type: [{ value: 1, text: "普通分页" }, { value: 2, text: "合计" }, { value: 3, text: "固定列" }, { value: 4, text: "交互" }],
                class: []
            }
            /*,
            ChartCode: {
                type: [{ value: 1, text: "柱状图" }, { value: 2, text: "饼图" }, { value: 3, text: "折线图" }],
                class: []
            }*/
        },
//      getLaboratoryUrl: '/ServerWCF/ReportFormSASService.svc/GetBHospital',//获得实验室
        getFormListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleFormListByHQL?isPlanish=true',//获得表单配置集合
        getGridListUrl: '/ServerWCF/ModuleConfigService.svc/ST_UDTO_SearchBModuleGridListByHQL?isPlanish=true',//获得表格配置集合
//      getChartListUrl: '/ServerWCF/SingleTableService.svc/ST_UDTO_SearchBModuleChartListByHQL?isPlanish=true'//获得图表配置集合

 		// 匹配不同平台
        selectAllSystemUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/LIIPService.svc/ST_UDTO_SearchIntergrateSystemSetByHQL?fields=IntergrateSystemSet_SystemCode,IntergrateSystemSet_SystemName,IntergrateSystemSet_SystemHost'
       
    };
    //初始化
    init();
    //初始化
    function init() {
        $(".cardHeight").css("height", ($(window).height() - 50) + "px");//设置容器高度
        //获得参数
        getParams();
        // 获取平台上集成的所有系统的相关信息
        getAllSystem();
        //树列表功能参数配置
        var options = {
            elem: '#moduleTree',
            id: 'moduleTree',
            role: config.role
        };
        moduleTree.render(options);
        //初始化列表
        initMainTable();
        //监听联动
        initGroupListeners();
        //初始化实验室
        //initLaboratory();
        //初始化表单配置集合
//      initFormList();
        //初始化表格配置集合
//      initGridList();
        //初始化图表配置集合
        //initChartList();
    }
    //获得参数
    function getParams() {
        var params = uxutil.params.get();
        config = $.extend({}, config, params);
    }
    //联动
    function initGroupListeners() {
        // 窗体大小改变时，调整高度显示
        $(window).resize(function () {
            var width = $(this).width();
            var height = $(this).height();
            $(".cardHeight").css("height", (height - 50) + "px");//设置容器高度
        });
        //树组件点击
        layui.onevent("treeClick", "click", function (obj) {
        	var curSys = config.systemList.find(function(item){
        		return item.SystemCode === obj.systemCode;
        	});
            mainTable.onSearch('mainTable',obj.codeList,curSys.SystemHost);
        });
        //集合新增成功刷新该集合下拉框
        layui.onevent("loadList", "load", function (obj) {
            if (obj.type == "FormCode")
                initFormList();
            else if (obj.type == "GridCode")
                initGridList();
            else
                initChartList();
        });
        //监听新增集合中集合类型下拉框
        form.on('select(addListModalFormListType)', function (data) {
            var value = data.value;
            //类型
            var addListModalFormTypeIDHtml = '';
            $.each(config.list[value]["type"], function (i, itemI) {
                addListModalFormTypeIDHtml += '<option value="' + itemI["value"] + '">' + itemI["text"] + '</option>';
            });
            $("#addListModalFormTypeID").html(addListModalFormTypeIDHtml);
            //样式
            var addListModalFormClassIDHtml = '<option value="">请选择</option>';
            $.each(config.list[value]["class"], function (i, itemI) {
                addListModalFormClassIDHtml += '<option value="' + itemI["value"] + '">' + itemI["text"] + '</option>';
            });
            $("#addListModalFormClassID").html(addListModalFormClassIDHtml);
            form.render("select");
        }); 
    }
    //页面表单配置项’
    function initMainTable() {
        var Obj = {
            elem: '#mainTable',
            title: '',
            height: 'full-70',
            toolbar: config.role == 'system' ? '#Toolbar' : false,
            defaultToolbar: config.role == 'system' ? ['filter'] : [],
            role: config.role,
            id: 'mainTable'
        };
        mainTable.render(Obj);
    };
   // 获取平台上所有集成的其他系统
   function getAllSystem(){
   		var url = config.selectAllSystemUrl;
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if(data && data.ResultDataValue) {
               config.systemList = data.value.list;
            } else {
                layer.msg(data.msg);
            }
        });
   }
   /* //初始化实验室
    function initLaboratory() {
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
    }; */
    //初始化表单配置集合
    function initFormList() {
        var url = uxutil.path.ROOT + config.getFormListUrl + '&where=IsUse=1&fields=BModuleFormList_Id,BModuleFormList_FormCode,BModuleFormList_CName,BModuleFormList_ShortCode,BModuleFormList_ShortName&page=1&limit=9999';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue).list || [],
                    str = '<option value="">直接选择或搜索选择</option>';
                $.each(list, function (i, itemI) {
                    str += '<option value="' + itemI.BModuleFormList_Id + '" data-code="' + itemI.BModuleFormList_FormCode + '" data-shortcode="' + itemI.BModuleFormList_ShortCode + '" data-shortname="' + itemI.BModuleFormList_ShortName+'">'+ itemI.BModuleFormList_CName + '</option>';
                });
                $("#FormID").html(str);
                form.render("select");
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //初始化表格配置集合
    function initGridList() {
        var url = uxutil.path.ROOT + config.getGridListUrl + '&where=IsUse=1&fields=BModuleGridList_Id,BModuleGridList_GridCode,BModuleGridList_CName,BModuleGridList_ShortCode,BModuleGridList_ShortName&page=1&limit=9999';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue).list || [],
                    str = '<option value="">直接选择或搜索选择</option>';
                $.each(list, function (i, itemI) {
                    str += '<option value="' + itemI.BModuleGridList_Id + '" data-code="' + itemI.BModuleGridList_GridCode + '" data-shortcode="' + itemI.BModuleGridList_ShortCode + '" data-shortname="' + itemI.BModuleGridList_ShortName +'">' + itemI.BModuleGridList_CName + '</option>';
                });
                $("#GridID").html(str);
                form.render("select");
            } else {
                layer.msg(data.msg);
            }
        });
    };
   /* //初始化图表配置集合
    function initChartList() {
        var url = uxutil.path.ROOT + config.getChartListUrl + '&where=IsUse=1&fields=ChartID,ChartCode,CName,ShortCode,ShortName&page=1&limit=9999';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue).list || [],
                    str = '<option value="">直接选择或搜索选择</option>';
                $.each(list, function (i, itemI) {
                    str += '<option value="' + itemI.ChartID + '" data-code="' + itemI.ChartCode + '" data-shortcode="' + itemI.ShortCode + '" data-shortname="' + itemI.ShortName +'">' + itemI.CName + '</option>';
                });
                $("#ChartID").html(str);
                form.render("select");
            } else {
                layer.msg(data.msg);
            }
        });
    }; */
});