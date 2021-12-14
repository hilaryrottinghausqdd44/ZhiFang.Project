/**
	@name：表单
	@author：zhangda
	@version 2019-09-09
 */
layui.extend({
}).define(['form'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        formSelects = layui.formSelects,
        form = layui.form;
    var areaHospitalForm = {
        //全局项
        config: {
            formtype: '',
            PK: null,
            //当前已加载的数据
            currData: []
        },
        //设置全局项
        set: function (options) {
            var me = this;
            me.config = $.extend({}, me.config, options);
            return me;
        }
    };
    //构造器
    var Class = function (setings) {
        var me = this;
        me.config = $.extend({}, me.config, areaHospitalForm.config, setings);
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {
        PK: null,
        //当前已加载的数据
        currData: [],
        editUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalByField',
        
    };
    /**创建数据字段*/
    Class.pt.getStoreFields = function () {
        var fields = [];
        $(":input").each(function () {
            if (this.name) fields.push(this.name)
        });
        return fields;
    };
    //加载表单数据	
    Class.pt.loadDatas = function (id, callback) {
        var me = this;
        var url = me.config.selectUrl + '&id=' + id +
            '&fields=' + me.getStoreFields().join(',');
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                callback(data);
            } else {
                layer.msg(data.msg);
            }
        });
    };
    /**@overwrite 返回数据处理方法*/
    Class.pt.changeResult = function (data) {
        var me = this;
        var list = JSON.parse(data);
        //if (list.LBSection_IsUse == "false") list.LBSection_IsUse = "";
        Class.pt.config.currData = list;
        return list;
    };
    

    //核心入口
    areaHospitalForm.render = function (options) {
        var me = new Class(options);
        me.initFilterListeners();
        return options;
    };
    //加载 
    Class.pt.load = function () {
        var me = this;
        if (me.config.PK) {
            //加载数据
            me.loadDatas(me.config.PK, function (data) {
                form.val('AreaForm', me.changeResult(data.ResultDataValue));
            });
        }
    };
    /**@overwrite 获取修改的数据*/
    Class.pt.getEditParams = function (data) {
        var me = this;
        var entity = {};
        console.log(data);
        if(data.BHospital_Id == ""){
        	layer.msg("请选择医院！",{icon: 5,anim:6});
        	return;
        }
        entity.entity = JSON.stringify(data).replace(/BHospital_/g, "");
        if (entity.entity) entity.entity = JSON.parse(entity.entity);
       
        entity.fields = "";
        //获得fields
        for (var i in entity.entity) {
            if (typeof entity.entity[i] == "object") continue;
            if (entity.fields != "") {
                entity.fields += "," + i;
            } else {
                entity.fields += i;
            }
        }
        return entity;
    };
    //表单保存处理
    Class.pt.onSaveClick = function (data,index) {
        var me = Class.pt;
        var url = me.config.editUrl;
        var params =  me.getEditParams(data.field);
        if (!params) return;
        var id = params.entity.Id;
        params = JSON.stringify(params);
        //显示遮罩层
        var config = {
            type: "POST",
            url: url,
            data: params
        };
        uxutil.server.ajax(config, function (data) {
            //隐藏遮罩层
            if (data.success) {
                if (index) {
                    layui.event("SaveForm", "save", {});
                }
            } else {
                
                layer.msg("新增失败！", { icon: 5, anim: 6 });
            }
        });
    };
    //新增
    Class.pt.isAdd = function (obj) {
        var me = this;
        Class.pt.config.PK = null;
        Class.pt.config.formtype = 'add';
        var inst = new Class(me);
        inst.config.PK = null;
        inst.config.formtype = 'add';
        Class.pt.config.currData = {};
    };
    areaHospitalForm.add = function (obj) {
        var me = this;
        var inst = new Class(me);
        inst.isAdd(obj);
    };
    areaHospitalForm.onReset = function () {
        Class.pt.Empty();
    }
    //事件处理
    Class.pt.initFilterListeners = function () {
        var me = this;
        //保存
        form.on('submit(save)', function (obj) {
            window.event.preventDefault();
            var getName = layui.formSelects.value('HospitalId');
            var idnex = 0;
            var flag = false;
            for (var i = 0; i < getName.length; i++) {
                idnex++;
                if (idnex == getName.length) {
                    flag = true;
                }
                me.onSaveClick({
                    field: {
                        "BHospital_Id": getName[i].value,
                        "BHospital_AreaID": obj.field.BHospital_AreaID,
                    }
                }, flag );
            }

        });
    };
     //中心医院字段
	CenterHospitallist();
    function CenterHospitallist() {
        var me = this;
        var url =  uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?isPlanish=true' + '&where=IsUse=1 and AreaID is null' +
            '&fields=BHospital_Name,BHospital_Id,';
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {            	
                var value = data[uxutil.server.resultParams.value];
                if (value && typeof (value) === "string") {
                    if (isNaN(value)) {
                        value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
                        value = value.replace(/\\"/g, '&quot;');
                        value = value.replace(/\\/g, '\\\\');
                        value = value.replace(/&quot;/g, '\\"');
                        value = eval("(" + value + ")");
                    } else {
                        value = value + "";
                    }
                }
                if (!value) return;
                var tempAjax = "<option value=''>请选择</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].BHospital_Id + "'>" + value.list[i].BHospital_Name + "</option>";
                    $("#HospitalId").empty();
                    $("#HospitalId").append(tempAjax);
                }
                formSelects.render('HospitalId');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
    areaHospitalForm.render();
    //暴露接口
    exports('areaHospitalForm', areaHospitalForm);
});