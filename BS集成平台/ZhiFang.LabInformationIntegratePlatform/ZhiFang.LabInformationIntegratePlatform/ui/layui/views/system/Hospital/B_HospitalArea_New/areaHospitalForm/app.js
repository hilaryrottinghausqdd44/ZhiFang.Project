/**
   @Name：主页面
   @Author：郭海祥
   @version 2019-12-12
 */
layui.extend({
    uxutil: 'ux/util',
    areaHospitalForm: 'views/system/Hospital/B_HospitalArea_New/areaHospitalForm/areaHospitalForm',
}).use(['uxutil', 'table', 'form', 'element', 'areaHospitalForm'], function () {
    var $ = layui.$,
        element = layui.element,
        form = layui.form,
        uxutil = layui.uxutil,
        areaHospitalForm = layui.areaHospitalForm,
        table = layui.table;
    //全局变量
    var config = {
        //当前选择行数据
        checkData: []
    };
    var paramsObj = {
       AreaId:null,formType:null
    };

    //初始化
    init();
    //初始化
    function init() {
        //监听联动
        initGroupListeners();
        getParams();
        if(paramsObj.formType == "add"){
        	areaHospitalForm.add(paramsObj);
        }
    };
    
    
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
    };

    //联动
    function initGroupListeners() {
        layui.onevent("SaveForm", "save", function (obj) {
            var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
            parent.layer.close(index); //再执行关闭
        });
    };
    
   
});