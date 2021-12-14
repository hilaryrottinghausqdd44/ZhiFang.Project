/**
   @Name：样本采集-单个匹配确认
   @Author：zhangda
   @version 2020-08-24
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/pre/sampleCollection/SingleMatchConfirm/mainTable',
    rightTable: 'views/pre/sampleCollection/SingleMatchConfirm/rightTable',
    barcodeTable: 'views/pre/sampleCollection/SingleMatchConfirm/barcodeTable'
}).use(['uxutil', 'table', 'form', 'element', 'laydate', 'mainTable', 'rightTable','barcodeTable'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        laydate = layui.laydate,
        element = layui.element,
        mainTable = layui.mainTable,
        rightTable = layui.rightTable,
        barcodeTable = layui.barcodeTable,
        uxutil = layui.uxutil;
    //全局变量
    var config = {
        checkRowData: {}
    };
    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
        //初始化列表
        initMainTable();
        setTimeout(function () {
            mainTable.onSearch('mainTable');
        }, 100);
        initRightTable();
        initBarcodeTable();
    }
    //联动
    function initGroupListeners() {
        //单击行事件
        layui.onevent('rowClick', 'click', function (obj) {
            //存在 则刷新
            //不存在 清空
        });
        //监听查询
        form.on('submit(search)', function (data) {
            window.event.preventDefault();
            onSearch(data.field);
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
        //监听是否显示查询栏
        form.on('checkbox(IsShowSearch)', function (data) {
            var checked = data.elem.checked; //是否被选中，true或者false
            if (checked) {
                if ($("#searchBox").hasClass("layui-hide")) $("#searchBox").removeClass("layui-hide");
            } else {
                if (!$("#searchBox").hasClass("layui-hide")) $("#searchBox").addClass("layui-hide");
            }
        }); 
    }
    //页面表单配置项’
    function initMainTable(height) {
        var Obj = {
            elem: '#mainTable',
            title: '',
            height: height ? height :'full-130',
            id: 'mainTable'
        };
        mainTable.render(Obj);
    };
    //页面表单配置项’
    function initRightTable(height) {
        var Obj = {
            elem: '#rightTable',
            title: '',
            height: height ? height : 'full-130',
            id: 'rightTable'
        };
        rightTable.render(Obj);
    };
    //页面表单配置项’
    function initBarcodeTable(height) {
        var Obj = {
            elem: '#barcodeTable',
            title: '',
            height: height ? height : 'full-130',
            id: 'barcodeTable'
        };
        barcodeTable.render(Obj);
    };
    //查询
    function onSearch(obj) {
        console.log(obj);
    }
});