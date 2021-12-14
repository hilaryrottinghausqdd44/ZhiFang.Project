/**
   @Name：样本采集
   @Author：zhangda
   @version 2020-08-24
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/pre/sampleCollection/singleConfirm/mainTable',
    rightTable: 'views/pre/sampleCollection/singleConfirm/rightTable'
}).use(['uxutil', 'table', 'form', 'element','laydate', 'mainTable','rightTable'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        laydate = layui.laydate,
        element = layui.element,
        mainTable = layui.mainTable,
        rightTable = layui.rightTable,
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
    }
    //联动
    function initGroupListeners() {
        //单击行事件
        layui.onevent('rowClick', 'click', function (obj) {
            //存在 则刷新
            //不存在 清空
        });
        //监听已确认条码查询
        form.on('submit(search)', function (data) {
            window.event.preventDefault();
            onSearch(data.field);
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
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
    //查询
    function onSearch(obj) {
        console.log(obj);
    }
});