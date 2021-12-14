/**
   @Name：表单项配置
   @Author：guohx
   @version 2020-07-29
 */
layui.extend({
    uxutil: 'ux/util',
    mainTable: 'views/system/setting/Chart/mainTable'
}).use(['uxutil', 'table', 'form', 'mainTable'], function () {
    var $ = layui.$,
        form = layui.form,
        table = layui.table,
        uxutil = layui.uxutil,
        mainTable = layui.mainTable;
    //全局变量
    var config = {
        params: {
            toolbar: '#Toolbar', defaultToolbar: 'filter', page: true, limit: 50,
            externalWhere: '', isHideColumnToolbar: false, isInitLaboratory: true, isInitComponentType: true
        }
    };
    //初始化
    init();
    //初始化
    function init() {
        //初始化列表
        initMainTable();
        setTimeout(function () {
            mainTable.onSearch('mainTable');
        }, 100);
    }
    //页面表单配置项’
    function initMainTable() {
        var Obj = {
            elem: '#mainTable',
            title: '',
            height: 'full-50',
            id: 'mainTable'
        };
        mainTable.render(Obj);
    };
});