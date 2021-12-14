/**
   @Name：条码打印
   @Author：zhangda
   @version 2020-08-19
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/pre/barcodePrint/mainTable',
    rightTable: 'views/pre/barcodePrint/rightTable'
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
        checkRowData: {},
        //当前激活页签,默认第一个,从0开始
        currTabIndex: 0,
        //已激活页签，用于判断页签是否已加载
        //isLoadTabArr: [{index:0}],
    };
    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
        //初始化日期组件
        initDate();
        //初始化列表
        initMainTable();
        setTimeout(function () {
            mainTable.onSearch('mainTable');
        }, 100);
        initRightTable();
        setTimeout(function () {
            rightTable.onSearch('rightTable');
        }, 100);
    }
    //联动
    function initGroupListeners() {
        //监听页签切换
        element.on('tab(Tab)', function (data) {
            var height = 'full-170';
            config.currTabIndex = data.index;//得到当前Tab的所在下标
            if (data.index == 0)
                height = 'full-170';
            else
                height = 'full-215';
            initMainTable(height, data.index);
            setTimeout(function () {
                mainTable.onSearch('mainTable');
            }, 100);
            initRightTable(height);
            setTimeout(function () {
                rightTable.onSearch('rightTable');
            }, 100);
        });
        //单击行事件
        layui.onevent('rowClick', 'click', function (obj) {
            //存在 则刷新
            //不存在 清空
        });
        //监听未确认条码查询
        form.on('submit(NoConfirmSearch)', function (data) {
            window.event.preventDefault();
            NoConfirmSearch(data.field);
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
        //监听已确认条码查询
        form.on('submit(confirmSearch)', function (data) {
            window.event.preventDefault();
            confirmSearch(data.field);
            return false; //阻止表单跳转。如果需要表单跳转，去掉这段即可。
        });
    }
    //页面表单配置项’
    function initMainTable(height, tabIndex) {
        var tabIndex = (tabIndex == null || tabIndex == 'undefined') ? config.currTabIndex : tabIndex;
        var Obj = {
            elem: '#mainTable',
            title: '',
            height: height ? height :'full-170',
            id: 'mainTable',
            toolbar: tabIndex == 1 ? "#Toolbar2" : "#Toolbar1",
            tabIndex: tabIndex
        };
        mainTable.render(Obj);
    };
    //页面表单配置项’
    function initRightTable(height) {
        var Obj = {
            elem: '#rightTable',
            title: '',
            height: height ? height : 'full-170',
            id: 'rightTable'
        };
        rightTable.render(Obj);
    };
    //初始化日期组件
    function initDate() {
        var today = uxutil.date.toString(new Date(), true);
        //开始日期
        var OrderTime = laydate.render({
            elem: '#OrderTime',
            type: 'date',
            value: today + " - " + today,
            range: true
        });
    };
    //未确认条码查询
    function NoConfirmSearch(obj) {
        console.log(obj);
    }
    //已确认条码查询
    function confirmSearch(obj) {
        console.log(obj);
    }
});