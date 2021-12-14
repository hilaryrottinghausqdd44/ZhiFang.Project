/**
	@name：仪器表单
	@author：zhangda
	@version 2019-08-15
 */
layui.extend({
}).define(['form'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        form = layui.form;

    var equipForm = {
        //全局项
        config: {
            formtype: 'show',
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
        me.config = $.extend({}, me.config, equipForm.config, setings);
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {
        PK: null,
        //当前已加载的数据
        currData: [],
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBEquip',
        editUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipByField',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipById?isPlanish=true',
        //删除
        delUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBEquip',
        //获取指定实体字段的最大号
        select_maxno_Url: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetMaxNoByEntityField?entityName=LBEquip&entityField=DispOrder',
        //提取中文字符串拼音字头
        select_pinyin_Url: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPinYinZiTou',
        //获得检验小组
        select_section_Url: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?isPlanish=true',
        //获得专业
        select_specialty_Url: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?isPlanish=true'
    };
    /**创建数据字段*/
    Class.pt.getStoreFields = function () {
        var fields = [];
        $(":input").each(function () {
            if (this.name) fields.push(this.name)
        });
        fields.push('LBEquip_IsUse');
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
        //if (list.LBSection_IsVirtualGroup == "false") list.LBSection_IsVirtualGroup = "";
        //if (list.LBSection_IsUse == "false") list.LBSection_IsUse = "";
        //if (list.LBSection_IsImage == "false") list.LBSection_IsImage = "";
        if (list.LBEquip_SpecialtyID == 0) list.LBEquip_SpecialtyID = "";
        Class.pt.config.currData = list;
        return list;
    };
    //检验小组-下拉框加载
    Class.pt.loadSection = function () {
        var me = this;
        var url = me.config.select_section_Url + '&where=IsUse=1' +
            '&fields=LBSection_CName,LBSection_Id';
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
                    tempAjax += "<option value='" + value.list[i].LBSection_Id + "'>" + value.list[i].LBSection_CName + "</option>";
                    $("#LBEquip_LBSection_Id").empty();
                    $("#LBEquip_LBSection_Id").append(tempAjax);

                }
                form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //专业-下拉框加载
    Class.pt.loadSpecialty = function () {
        var me = this;
        var url = me.config.select_specialty_Url + '&where=IsUse=1' +
            '&fields=LBSpecialty_CName,LBSpecialty_Id' +
            '&sort=[{property:"LBSpecialty_DispOrder",direction:"ASC"}]';
            
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
                    tempAjax += "<option value='" + value.list[i].LBSpecialty_Id + "'>" + value.list[i].LBSpecialty_CName + "</option>";
                    $("#LBEquip_SpecialtyID").empty();
                    $("#LBEquip_SpecialtyID").append(tempAjax);

                }
                form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //下拉框数据初始化
    Class.pt.iniCombox = function () {
        var me = this;
        me.loadSection();
        me.loadSpecialty();
    };
    //核心入口
    equipForm.render = function (options) {
        var me = new Class(options);
        me.iniCombox();
        me.initFilterListeners();
        return options;
    };
    //加载 
    Class.pt.load = function () {
        var me = this;
        if (me.config.PK) {
            //加载数据
            me.loadDatas(me.config.PK, function (data) {
                form.val('LBEquipForm', me.changeResult(data.ResultDataValue));
                var list = JSON.parse(data.ResultDataValue);
                if (list.LBEquip_IsUse == "true") {
                    if (!$("#LBEquip_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                        $("#LBEquip_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                        $("#LBEquip_IsUse").next('.layui-form-switch').children("em").html("是");
                        $("#LBEquip_IsUse")[0].checked = true;
                    }
                } else {
                    if ($("#LBEquip_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                        $("#LBEquip_IsUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
                        $("#LBEquip_IsUse").next('.layui-form-switch').children("em").html("否");
                        $("#LBEquip_IsUse")[0].checked = false;
                    }
                }
            });
        }
    };
    Class.pt.changePinYinZiTou = function () {
        var me = this;
        var LBEquip_CName = document.getElementById("LBEquip_CName"),
            LBEquip_PinYinZiTou = document.getElementById('LBEquip_PinYinZiTou'),
            LBEquip_Shortcode = document.getElementById('LBEquip_Shortcode');

        var val = LBEquip_CName.value;
        var Shortcode = LBEquip_Shortcode.value;
        if (val != "") {
            me.getPinYinZiTou(val, function (data) {
                LBEquip_PinYinZiTou.value = data;//data
                //				if(!Shortcode)LBEquip_Shortcode.value=data;
            });
        } else {
            LBEquip_PinYinZiTou.value('');
            LBEquip_Shortcode.value('');
        }
    };
    //拼音字头
    Class.pt.getPinYinZiTou = function (val, callback) {
        var me = this;
        var url = me.config.select_pinyin_Url + '?chinese=' + encodeURI(val);
        if (val == "") {
            if (typeof (callback) == "function") {
                callback(chinese);
            }
            return;
        }
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (typeof (callback) == "function") {
                    callback(res.ResultDataValue);
                }
            } else {
                layer.msg("拼音字头获得失败！", { icon: 5, anim: 6 });
            }
        });
    };
    //获取指定实体字段的最大号
    Class.pt.getMaxNo = function (callback) {
        var me = this;
        var url = me.config.select_maxno_Url;
        var result = "";
        uxutil.server.ajax({
            url: url
        }, function (data) {
            if (data) {
                callback(data.ResultDataValue);
            } else {
                layer.msg(data.msg);
            }
        });
    };
    //联动监听
    Class.pt.initPinYinZiTouListeners = function () {
        var me = this;
        var LBEquip_CName = document.getElementById("LBEquip_CName");
        var LBEquip_SName = document.getElementById("LBEquip_SName");
        var LBEquip_PinYinZiTou = document.getElementById("LBEquip_PinYinZiTou");
        var LBEquip_Shortcode = document.getElementById("LBEquip_Shortcode");

        $('#LBEquip_CName').on("input propertychange", function () {
            me.changePinYinZiTou();
            //这里写你的处理代码
        });
        //简称 必有（输入名称后，简称为空时，简称-=名称）
        $("#LBEquip_CName").on('blur', function () {
            if (!LBEquip_SName.value) {
                LBEquip_SName.value = LBEquip_CName.value;
            }
            //快捷码 为空时，也默认为汉字拼音字头
            if (!LBEquip_Shortcode.value && LBEquip_PinYinZiTou.value) {
                LBEquip_Shortcode.value = LBEquip_PinYinZiTou.value;
            }
        });

    };
    /**@overwrite 获取新增的数据*/
    Class.pt.getAddParams = function (data) {
        var me = this;
        var entity = JSON.stringify(data).replace(/LBEquip_/g, "");
        if (entity) entity = JSON.parse(entity);
        entity.IsUse = $("#LBEquip_IsUse+div.layui-form-switch").hasClass("layui-form-onswitch");
        if (entity.LBSection_Id) {
            var DataTimeStamp = [0, 0, 0, 0, 0, 0, 41, 186];
            entity.LBSection = { Id: entity.LBSection_Id, DataTimeStamp: DataTimeStamp };
        }
        delete entity.LBSection_Id;
        if (!entity.EquipResultType) delete entity.EquipResultType;
        if (!entity.DoubleFlag) delete entity.DoubleFlag;
        if (!entity.SpecialtyID) delete entity.SpecialtyID;
        if (!entity.Id) delete entity.Id;
        for (var i in entity) {
            if (typeof entity[i] == "string") entity[i] = entity[i].trim();
        }
        return {
            entity: entity
        };
        return entity;
    };
    /**@overwrite 获取修改的数据*/
    Class.pt.getEditParams = function (data) {
        var me = this;
        var entity = {};
        entity.entity = JSON.stringify(data).replace(/LBEquip_/g, "");
        if (entity.entity) entity.entity = JSON.parse(entity.entity);
        //处理数据
        entity.entity.IsUse = $("#LBEquip_IsUse+div.layui-form-switch").hasClass("layui-form-onswitch");
        entity.entity.LBSection_Id = entity.entity.LBSection_Id != "" ? entity.entity.LBSection_Id : 0;
        var DataTimeStamp = [0, 0, 0, 0, 0, 0, 41, 186];
        entity.entity.LBSection = { Id: entity.entity.LBSection_Id, DataTimeStamp: DataTimeStamp };
        delete entity.entity.LBSection_Id;
        if (!entity.entity.SpecialtyID) entity.entity.SpecialtyID = 0;
        //获得fields
        entity.fields = "LBSection_Id,LBSection_DataTimeStamp";
        for (var i in entity.entity) {
            if (typeof entity.entity[i] == "object") continue;
            if (typeof entity.entity[i] == "string") entity.entity[i] = entity.entity[i].trim();
            if (entity.fields != "") {
                entity.fields += "," + i;
            } else {
                entity.fields += i;
            }
        }
        if (data["LBEquip_Id"])
            entity.entity.Id = data["LBEquip_Id"];
        return entity;
    };
    //表单保存处理
    Class.pt.onSaveClick = function (data) {
        var me = Class.pt;
        if (me.config.formtype == 'show') return;
        try {
            var url = me.config.formtype == 'add' ? me.config.addUrl : me.config.editUrl;
            var params = me.config.formtype == 'add' ? me.getAddParams(data.field) : me.getEditParams(data.field);
            if (!params) {
                layer.closeAll('loading');//隐藏遮罩层
                return;
            }
            var id = params.entity.Id;
            params = JSON.stringify(params);
            var config = {
                type: "POST",
                url: url,
                data: params
            };
            uxutil.server.ajax(config, function (data) {
                layer.closeAll('loading');//隐藏遮罩层
                if (data.success) {
                    id = me.config.formtype == 'add' ? data.value.id : id;
                    id += '';
                    layui.event("SaveLBEquipForm", "save", { id: id, formtype: me.config.formtype });
                } else {
                    var msg = me.config.formtype == 'add' ? '新增失败！' : '修改失败！';
                    if (!data.msg) data.msg = msg;
                    layer.msg(data.msg, { icon: 5, anim: 6 });
                }
            });
        } catch (err) {
            layer.closeAll('loading');//隐藏遮罩层
            layer.msg(err);
        }
    };
    //新增
    Class.pt.isAdd = function () {
        var me = this;
        $('#formType').removeAttr("layui-hide");
        $('#formType').html("新增");
        Class.pt.config.PK = null;
        Class.pt.config.formtype = 'add';
        var inst = new Class(me);
        inst.config.PK = null;
        inst.config.formtype = 'add';
        inst.setDisabled(false);
        Class.pt.config.currData = {};
        inst.onResetClick();
        inst.getMaxNo(function (val) {
            document.getElementById('LBEquip_DispOrder').value = val;
        });
        inst.isDelEnable(false);
        inst.isSaveEnable(true);
        //  	Class.pt.config.PK=null;
        //  	Class.pt.config.formtype='add';
        //  	me.onResetClick();
        //  	me.getMaxNo(function(val){
        //  		document.getElementById('LBEquip_DispOrder').value=val;
        //  	});
    };
    //删除方法 
    Class.pt.onDelClick = function () {
        var me = this;
        var id = document.getElementById("LBEquip_Id").value;
        if (!id) return;
        var url = me.config.delUrl + '?id=' + id;
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            var loadIndex = layer.load();//显示遮罩层
            uxutil.server.ajax({
                url: url
            }, function (data) {
                layer.close(loadIndex);//隐藏遮罩层
                if (data.success === true) {
                    layer.close(index);
                    layer.msg("删除成功！", { icon: 6, anim: 0, time: 2000 });
                    Class.pt.config.currData = {};
                    layui.event("DelLBEquipForm", "del", { id: id });
                } else {
                    layer.msg("删除失败！", { icon: 5, anim: 6 });
                }
            });
        });
    };
    //编辑
    equipForm.isEdit = function (id) {
        var me = this;
        $('#formType').removeAttr("layui-hide");
        $('#formType').html("编辑");
        Class.pt.config.PK = id;
        Class.pt.config.formtype = 'edit';
        var inst = new Class(me);
        inst.config.PK = id;
        inst.config.formtype = 'edit';
        inst.load(id);
        inst.setDisabled(false);
        inst.isDelEnable(true);
        inst.isSaveEnable(true);

        //  	Class.pt.config.PK=id;
        //  	Class.pt.config.formtype='edit';
        //  	Class.pt.load(id);
    };
    //查看
    equipForm.isShow = function (id) {
        var me = this;
        $('#formType').removeAttr("layui-hide");
        $('#formType').html("查看");
        //  	me.PK=id;
        Class.pt.PK = id;
        Class.pt.formtype = 'show';
        //  	Class.pt.load(id);
        var inst = new Class(me);

        inst.config.PK = id;
        inst.config.formtype = 'show';
        inst.load(id);
        inst.setDisabled(true);
        inst.isDelEnable(true);
        inst.isSaveEnable(false);
        form.render('select');
    };
    //重置
    equipForm.onReset = function () {
        Class.pt.Empty();
    }
    //禁用处理
    Class.pt.setDisabled = function (isDisabled) {
        $("#LBEquipForm :input").each(function (i, item) {
            if ($(item)[0].nodeName == 'BUTTON') return true;
            $(item).attr("disabled", isDisabled);
            if (isDisabled) {
                if (!$(item).hasClass("layui-disabled")) $(item).addClass("layui-disabled");
            } else {
                if ($(item).hasClass("layui-disabled")) $(item).removeClass("layui-disabled");
            }
        });
        form.render();
    };
    //删除按钮是否禁用 del
    Class.pt.isDelEnable = function (bo) {
        if (bo)
            $("#del").removeClass("layui-btn-disabled").removeAttr('disabled', true);
        else
            $("#del").addClass("layui-btn-disabled").attr('disabled', true);
    };
    //保存按钮是否禁用 save
    Class.pt.isSaveEnable = function (bo) {
        if (bo)
            $("#save").removeClass("layui-btn-disabled").removeAttr('disabled', true);
        else
            $("#save").addClass("layui-btn-disabled").attr('disabled', true);
    };
    //清空表单
    Class.pt.Empty = function () {
        var me = this;
        $("#LBEquipForm").find('input[type=text],select,input[type=hidden]').each(function () {
            if ($(this).attr("id") == "LBEquip_EquipResultType") {
                $(this).find("option[value='常规']").prop("selected", "selected");
            }else if ($(this).attr("id") == "LBEquip_DoubleFlag") {
                $(this).val('1');
            } else {
                $(this).val('');
            }
        });
        me.isDelEnable(false);
        form.render('select');
        form.render('checkbox');
    }
    //重置表单
    /**@overwrite 重置按钮点击处理方法*/
    Class.pt.onResetClick = function () {
        var me = this;
        if (Class.pt.config.formtype == 'add') {
            $("#LBEquipForm").find('input[type=text],select,input[type=hidden]').each(function () {
                if ($(this).attr("id") == "LBEquip_EquipResultType") {
                    $(this).find("option[value='常规']").prop("selected", "selected");
                } else if ($(this).attr("id") == "LBEquip_DoubleFlag") {
                    $(this).val('1');
                } else {
                    $(this).val('');
                }
            });
            if (!$("#LBEquip_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                $("#LBEquip_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                $("#LBEquip_IsUse").next('.layui-form-switch').children("em").html("是");
                $("#LBEquip_IsUse")[0].checked = true;
            }
        } else {
            form.val('LBEquipForm', Class.pt.config.currData);
            if (Class.pt.config.currData.LBEquip_IsUse == "true") {
                if (!$("#LBEquip_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    $("#LBEquip_IsUse").next('.layui-form-switch').addClass('layui-form-onswitch');
                    $("#LBEquip_IsUse").next('.layui-form-switch').children("em").html("是");
                    $("#LBEquip_IsUse")[0].checked = true;
                }
            } else {
                if ($("#LBEquip_IsUse").next('.layui-form-switch').hasClass('layui-form-onswitch')) {
                    $("#LBEquip_IsUse").next('.layui-form-switch').removeClass('layui-form-onswitch');
                    $("#LBEquip_IsUse").next('.layui-form-switch').children("em").html("否");
                    $("#LBEquip_IsUse")[0].checked = false;
                }
            }
        }
        form.render('select');
        form.render('checkbox');
    };
    //事件处理
    Class.pt.initFilterListeners = function () {
        var me = this;
        me.initPinYinZiTouListeners();
        //新增
        $('#add').on('click', function () {
            me.isAdd();
        });
        //保存
        form.on('submit(save)', function (obj) {
            window.event.preventDefault();
            var loadIndex = layer.load();//显示遮罩层
            me.onSaveClick(obj);
        });
        //重置
        $('#reset').on('click', function () {
            me.onResetClick();
        });
        //删除
        $('#del').on('click', function () {
            me.onDelClick();
        });
        
    };
    equipForm.render();
    //暴露接口
    exports('equipForm', equipForm);
});