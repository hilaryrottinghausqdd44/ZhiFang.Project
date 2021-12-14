/**
   @Name：主页面
   @Author：郭海祥
   @version 2019-12-12
 */
layui.extend({
    uxutil: 'ux/util',
    HositalDeptForm: 'views/system/Hospital/B_HospitalDept/hospitalDeptForm/hospitalDeptForm',
}).use(['uxutil', 'table', 'form', 'element', 'HositalDeptForm'], function () {
    var $ = layui.$,
        element = layui.element,
        form = layui.form,
        uxutil = layui.uxutil,
        HositalDeptForm = layui.HositalDeptForm,
        table = layui.table;
    //全局变量
    var config = {
        //当前选择行数据
        checkData: []
    };
    var paramsObj = {
       HospitalId:null,HospitalName:null,formType:null,HospitalDeptId:null
    };

    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
        getParams();
        if(paramsObj.formType == "add"){
        	HositalDeptForm.add(paramsObj);
        }else if(paramsObj.formType == "edit"){
        	HositalDeptForm.isEdit(paramsObj.HospitalDeptId);
        }
    }
    
    
    //获得参数
    function getParams() {
        var params = location.search.split("?")[1].split("&");
        //参数赋值
        for (var j in paramsObj) {
            for (var i = 0; i < params.length; i++) {
                if (j.toUpperCase() == params[i].split("=")[0].toUpperCase()) {
                    paramsObj[j] = decodeURIComponent(params[i].split("=")[1]);
                }
            }
        }
    }

    //联动
    function initGroupListeners() {
        layui.onevent("SaveForm", "save", function (obj) {
        	parent.layer.closeAll();
        });
    }
});