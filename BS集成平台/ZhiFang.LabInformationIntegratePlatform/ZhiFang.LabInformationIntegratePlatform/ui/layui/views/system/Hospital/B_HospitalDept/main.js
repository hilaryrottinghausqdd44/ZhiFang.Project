/**
   @Name：主页面
   @Author：郭海祥
   @version 2019-12-12
 */
layui.extend({
    uxutil: 'ux/util',
    HospitalTable: 'views/system/Hospital/B_HospitalDept/hospitalTable/hospitalTable',
    HospitalDeptTable: 'views/system/Hospital/B_HospitalDept/hospitalDeptTable/hospitalDeptTable',
}).use(['uxutil', 'table', 'form', 'element', 'HospitalTable','HospitalDeptTable'], function () {
    var $ = layui.$,
        element = layui.element,
        form = layui.form,
        uxutil = layui.uxutil,
        HospitalTable = layui.HospitalTable,
        HospitalDeptTable = layui.HospitalDeptTable,
        table = layui.table;
    //全局变量
    var config = {
        //当前选择行数据
        checkData: []
    };
    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
        //初始化医院字典
        initHospitalTable();
    }
    
    //加载
    function loadHospitalTable(obj) {
        if (table.cache.HospitalDeptTable) {
            HospitalDeptTable.onSearch('HospitalDeptTable', obj);
        } else {
            initHospitalDeptTable(obj);
        }
    }
    
    //联动
    function initGroupListeners() {
        layui.onevent("HospitalTableClick", "click", function (obj) {
            loadHospitalTable(obj);
        });
    }
   
   //基本医院部门渲染’
    function initHospitalDeptTable(data) {
        var Obj = {
            elem: '#HospitalDeptTable',
            title: '医院科室字典',
            height: 'full-50',
            id: 'HospitalDeptTable'
        };
        HospitalDeptTable.render(Obj,data);
    }
    
    //基本医院字典渲染
    function initHospitalTable() {
        var Obj = {
            elem: '#HospitalTable',
            title: '医院字典',
            height: 'full-50',
            id: 'HospitalTable'
        };
        HospitalTable.render(Obj);
    }
});