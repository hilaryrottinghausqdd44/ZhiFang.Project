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
        form = layui.form;
    var HositalDeptForm = {
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
        me.config = $.extend({}, me.config, HositalDeptForm.config, setings);
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {
        PK: null,
        //当前已加载的数据
        currData: [],
        addUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_AddBHospitalDept',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LIIPCommonService.svc/ST_UDTO_SearchBHospitalDeptById?isPlanish=true',
        editUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_UpdateBHospitalDeptByField',
        delUrl: uxutil.path.ROOT +'/ServerWCF/LIIPCommonService.svc/ST_UDTO_DelBHospitalDept',
        
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
    HositalDeptForm.render = function (options) {
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
                form.val('deptForm', me.changeResult(data.ResultDataValue));
            });
        }
    };
    
    
    /**@overwrite 获取新增的数据*/
    Class.pt.getAddParams = function (data) {
        var me = this;
        var entity = JSON.stringify(data).replace(/BHospitalDept_/g, "");
        if (entity) entity = JSON.parse(entity);
        //delete entity.HospitalArea_Id;
        if (!entity.Id) delete entity.Id;
        
        return {
            entity: entity
        };
    };
    /**@overwrite 获取修改的数据*/
    Class.pt.getEditParams = function (data) {
        var me = this;
        var entity = {};
        entity.entity = JSON.stringify(data).replace(/BHospitalDept_/g, "");
        if (entity.entity) entity.entity = JSON.parse(entity.entity);
        //console.log($("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch'));
        if(!$("#isUse").next('.layui-form-switch').hasClass('layui-form-onswitch')){
			entity.entity.IsUse = "false";
		}else{
			entity.entity.IsUse = "true";
		}
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
        if (data["BHospitalDept_Id"])
            entity.entity.Id = data["BHospitalDept_Id"];
        return entity;
    };
    //表单保存处理
    Class.pt.onSaveClick = function (data) {
        var me = Class.pt;
        var url = me.config.formtype == 'add' ? me.config.addUrl : me.config.editUrl;
        var params = me.config.formtype == 'add' ? me.getAddParams(data.field) : me.getEditParams(data.field);
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
                id = me.config.formtype == 'add' ? data.value.id : id;
                id += '';
                layui.event("SaveForm", "save", { id: id, formtype: me.config.formtype });
            } else {
                var msg = me.config.formtype == 'add' ? '新增失败！' : '修改失败！';
                if (!data.msg) data.msg = msg;
                layer.msg(data.msg, { icon: 5, anim: 6 });
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
        inst.onResetClick();
    };
    //删除方法 
    Class.pt.onDelClick = function () {
        var me = this;
        var id = document.getElementById("HospitalArea_Id").value;
        if (!id) return;
        var url = me.config.delUrl + '?id=' + id;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            uxutil.server.ajax({
                url: url
            }, function (data) {
                layer.closeAll('loading');
                if (data.success === true) {
                    layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0, time: 2000 });
                    Class.pt.config.currData = {};
                    layui.event("DelForm", "del", { id: id });
                } else {
                    layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
                }
            });
        });
    };
    //编辑
    HositalDeptForm.isEdit = function (id) {
        var me = this;
        Class.pt.config.PK = id;
        Class.pt.config.formtype = 'edit';
        var inst = new Class(me);
        inst.config.PK = id;
        inst.config.formtype = 'edit';
        inst.load(id);

        //  	Class.pt.config.PK=id;
        //  	Class.pt.config.formtype='edit';
        //  	Class.pt.load(id);
    };
    HositalDeptForm.add = function (obj) {
        var me = this;
        var inst = new Class(me);
        inst.isAdd(obj);
    };
    HositalDeptForm.onReset = function () {
        Class.pt.Empty();
    }
    //清空表单
    Class.pt.Empty = function () {
        var me = this;
        $("#deptForm").find('input[type=text],select,input[type=hidden]').each(function () {
            if ($(this).attr("id") == "HospitalName" || $(this).attr("id") == "HospitalID") {
	            } else {
	                $(this).val('');
	            }
        });
        form.render('select');
        form.render('checkbox');
    }
    //重置表单
    /**@overwrite 重置按钮点击处理方法*/
    Class.pt.onResetClick = function () {
        var me = this;
        if (Class.pt.config.formtype == 'add') {
            $("#deptForm").find('input[type=text],select,input[type=hidden]').each(function () {
            	if ($(this).attr("id") == "HospitalName" || $(this).attr("id") == "HospitalID") {
	            } else {
	                $(this).val('');
	            }
            });
            document.getElementById("isUse").value = 'true';
        } else {
            form.val('deptForm', Class.pt.config.currData);
        }
        form.render('select');
        form.render('checkbox');
    };
    //事件处理
    Class.pt.initFilterListeners = function () {
        var me = this;
        //保存
        form.on('submit(save)', function (obj) {
            window.event.preventDefault();
            me.onSaveClick(obj);
        });
        //重置
        $('#reset').on('click', function () {
            me.onResetClick();
        });
    };
    HositalDeptForm.render();
    //暴露接口
    exports('HositalDeptForm', HositalDeptForm);
});