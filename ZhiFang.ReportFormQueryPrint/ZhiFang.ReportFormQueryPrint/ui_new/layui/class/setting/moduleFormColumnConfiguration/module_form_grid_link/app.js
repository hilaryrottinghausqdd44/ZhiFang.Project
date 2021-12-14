/**
   @Name：模块表单表格配置应用关系
   @Author：guohx
   @version 2020-07-29
 */
layui.extend({
    uxutil: 'ux/util',
    moduleTree: 'class/setting/moduleFormColumnConfiguration/module_form_grid_link/moduleTree',
    mainTable: 'class/setting/moduleFormColumnConfiguration/module_form_grid_link/mainTable'
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
        role: '',//根据该属性判断是否可以操作  system
        list: {//集合中类型和样式
            FormCode: {
                type: [{ value: 1, text: "查询" }, { value: 2, text: "编辑" }, { value: 3, text: "交互" }],
                class: [{ value: 1, text: "普通" }, { value: 2, text: "收缩" }]
            },
            GridCode: {
                type: [{ value: 1, text: "普通分页" }, { value: 2, text: "合计" }, { value: 3, text: "固定列" }, { value: 4, text: "交互" }],
                class: []
            },
            ChartCode: {
                type: [{ value: 1, text: "柱状图" }, { value: 2, text: "饼图" }, { value: 3, text: "折线图" }],
                class: []
            }
        },
        //getLaboratoryUrl: '/ServiceWCF/ReportFormSASService.svc/GetBHospital',//获得实验室
        getFormListUrl: '/ServiceWCF/DictionaryService.svc/GetBModuleFormList?isPlanish=true',//获得表单配置集合
        getGridListUrl: '/ServiceWCF/DictionaryService.svc/GetBModuleGridList?isPlanish=true',//获得表格配置集合
        //getChartListUrl: '/ServiceWCF/DictionaryService.svc/ST_UDTO_SearchBModuleChartListByHQL?isPlanish=true'//获得图表配置集合
    };
    //初始化
    init();
    //初始化
    function init() {
        $(".cardHeight").css("height", ($(window).height() - 58) + "px");//设置容器高度
        //获得参数
        getParams();
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
        initFormList();
        //初始化表格配置集合
        initGridList();
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
            mainTable.onSearch('mainTable',obj.id);
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
            height: 'full-50',
            toolbar: config.role == 'system' ? '#Toolbar' : false,
            defaultToolbar: config.role == 'system' ? ['filter'] : [],
            role: config.role,
            id: 'mainTable'
        };
        mainTable.render(Obj);
    };
     //初始化实验室
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
    };
    //初始化表单配置集合
    function initFormList() {
        var url = uxutil.path.ROOT + config.getFormListUrl + '&where=IsUse=1&fields=FormID,FormCode,CName,ShortCode,ShortName&page=1&limit=9999';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue).list || [],
                    str = '<option value="">直接选择或搜索选择</option>';
                $.each(list, function (i, itemI) {
                    str += '<option value="' + itemI.FormID + '" data-code="' + itemI.FormCode + '" data-shortcode="' + itemI.ShortCode + '" data-shortname="' + itemI.ShortName+'">'+ itemI.CName + '</option>';
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
        var url = uxutil.path.ROOT + config.getGridListUrl + '&where=IsUse=1&fields=GridID,GridCode,CName,ShortCode,ShortName&page=1&limit=9999';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data && data.ResultDataValue) {
                var list = JSON.parse(data.ResultDataValue).list || [],
                    str = '<option value="">直接选择或搜索选择</option>';
                $.each(list, function (i, itemI) {
                    str += '<option value="' + itemI.GridID + '" data-code="' + itemI.GridCode + '" data-shortcode="' + itemI.ShortCode + '" data-shortname="' + itemI.ShortName +'">' + itemI.CName + '</option>';
                });
                $("#GridID").html(str);
                form.render("select");
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //初始化图表配置集合
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
    };
});