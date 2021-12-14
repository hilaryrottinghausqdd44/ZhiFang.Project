/**
	@name：仪器结果类型表单
	@author：zhangda
	@version 2019-08-21
 */
layui.extend({
    uxutil: 'ux/util',
}).define(['form', 'uxutil', 'element','laydate','layer'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        layer = layui.layer,
        element = layui.element,
        laydate = layui.laydate,
        form = layui.form;


    //外部参数
    var PARAMS = uxutil.params.get(true);
    //仪器
    var EQUIPID = PARAMS.EQUIPID;
    //显示次序
    var DISPORDER = PARAMS.DISPORDER;
    //主键
    var PK = PARAMS.PK;
    //计算是否加载完毕 执行赋值操作
    var Count = 0;

    var resultForm = {
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
        me.config = $.extend({}, me.config, resultForm.config, setings);
        if (EQUIPID) $('#LBEquipResultTH_LBEquip_Id').val(EQUIPID);
        if (DISPORDER) $('#LBEquipResultTH_DispOrder').val(DISPORDER);
        if (PK) $('#LBEquipResultTH_Id').val(PK);
    };
    Class.pt = Class.prototype;
    //默认配置
    Class.pt.config = {
        PK: null,
        //当前已加载的数据
        currData: [],
        addUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBEquipResultTH',
        editUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipResultTHByField',
        selectUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipResultTHById?isPlanish=true&fields=' +
            'LBEquipResultTH_Id,LBEquipResultTH_DispOrder,LBEquipResultTH_LBItem_Id,LBEquipResultTH_SampleTypeID,' +
            'LBEquipResultTH_GenderID,LBEquipResultTH_CalcType,LBEquipResultTH_SourceValue,LBEquipResultTH_ReportValue,' +
            'LBEquipResultTH_AppValue',
        //获得项目
        getItemUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemByHQL?isPlanish=true',
        //获得样本类型
        getSampleTypeUrl:uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
        getEnumTypeUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',//获得枚举 传递枚举类型名 
        //获得性别
        //getGenderUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBGenderByHQL?isPlanish=true',
        //获得年龄单位
        //getAgeUnitUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBAgeUnitByHQL?isPlanish=true'
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
        if (list.LBEquipResultTH_BCollectTime) list.LBEquipResultTH_BCollectTime = list.LBEquipResultTH_BCollectTime.split(" ")[1];
        if (list.LBEquipResultTH_ECollectTime) list.LBEquipResultTH_ECollectTime = list.LBEquipResultTH_ECollectTime.split(" ")[1];
        if (list.LBEquipResultTH_LowAge < 0) list.LBEquipResultTH_LowAge = "";
        if (list.LBEquipResultTH_HighAge < 0) list.LBEquipResultTH_HighAge = "";
        if (list.LBEquipResultTH_LBItem_Id == "") list.LBEquipResultTH_LBItem_Id = -1;
        //if (list.LBSection_IsUse == "false") list.LBSection_IsUse = "";
        //if (list.LBSection_IsImage == "false") list.LBSection_IsImage = "";
        Class.pt.config.currData = list;
        return list;
    };
    //项目-下拉框加载
    Class.pt.loadItem = function () {
        var me = this;
        var url = me.config.getItemUrl + '&where=EquipID=' + EQUIPID +
            '&fields=LBEquipItem_LBItem_CName,LBEquipItem_LBItem_Id';
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
                var tempAjax = "<option value='-1'>所有</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBEquipItem_LBItem_Id + "'>" + value.list[i].LBEquipItem_LBItem_CName + "</option>";
                    $("#LBEquipResultTH_LBItem_Id").empty();
                    $("#LBEquipResultTH_LBItem_Id").append(tempAjax);

                }
                form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
            Count++;
        });
    };
    //样本类型-下拉框加载
    Class.pt.loadSampleType = function () {
        var me = this;
        var url = me.config.getSampleTypeUrl + '&where=IsUse=1'+
            '&fields=LBSampleType_CName,LBSampleType_Id';
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
                var tempAjax = "<option value='0'>所有</option>";
                for (var i = 0; i < value.list.length; i++) {
                    tempAjax += "<option value='" + value.list[i].LBSampleType_Id + "'>" + value.list[i].LBSampleType_CName + "</option>";
                    $("#LBEquipResultTH_SampleTypeID").empty();
                    $("#LBEquipResultTH_SampleTypeID").append(tempAjax);

                }
                form.render('select');//需要渲染一下;
            } else {
                layer.msg(data.msg);
            }
            Count++;
        });
    };
    //性别-下拉框加载
    Class.pt.loadGender = function () {
        var me = this;
        var url = me.config.getEnumTypeUrl + '?classname=GenderType&classnamespace=ZhiFang.Entity.LabStar';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    var html = "<option value='0'>所有</option>";
                    for (var i in data) {
                        var color = data[i].BGColor;
                        if (color != "") {
                            html += '<option value="' + data[i].Id + '" style="color:' + color + '">' + data[i].Name + '</option>';
                        } else {
                            html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
                        }
                    }
                    $("#LBEquipResultTH_GenderID").html(html);
                    form.render('select');

                }
            } else {
                layer.msg("性别枚举查询失败！", { icon: 5, anim: 6 });
            }
            Count++;
        });
    };
    //年龄单位-下拉框加载
    Class.pt.loadAgeUnit = function () {
        var me = this;
        var url = me.config.getEnumTypeUrl + '?classname=AgeUnitType&classnamespace=ZhiFang.Entity.LabStar';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    var defaultValue;
                    var html = "";
                    for (var i in data) {
                        if (data[i].Name == "岁") defaultValue = data[i].Id;
                        var color = data[i].BGColor;
                        if (color != "") {
                            html += '<option value="' + data[i].Id + '" style="color:' + color + '">' + data[i].Name + '</option>';
                        } else {
                            html += '<option value="' + data[i].Id + '">' + data[i].Name + '</option>';
                        }
                    }
                    $("#LBEquipResultTH_AgeUnitID").html(html);
                    form.render('select');
                    //赋默认值--岁
                    if (defaultValue != "undefined") {
                        $("#LBEquipResultTH_AgeUnitID").val(defaultValue);
                    }
                }
            } else {
                layer.msg("年龄单位枚举查询失败！", { icon: 5, anim: 6 });
            }
            Count++;
        });
    };
    //替换类型-下拉框加载
    Class.pt.loadCalcType = function () {
        var me = this;
        var url = me.config.getEnumTypeUrl + '?classname=EquipResultReplaceCompSymbol&classnamespace=ZhiFang.Entity.LabStar';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    var defaultValue;
                    var html = "";
                    for (var i in data) {
                        html += '<option value="' + data[i].Name + '">' + data[i].Name + '</option>';
                    }
                    $("#LBEquipResultTH_CalcType").html(html);
                    form.render('select');
                }
            } else {
                layer.msg("替换类型枚举查询失败！", { icon: 5, anim: 6 });
            }
            Count++;
        });
    };
    //下拉框数据初始化
    Class.pt.iniCombox = function () {
        var me = this;
        me.loadItem();
        me.loadSampleType();
        me.loadGender();
        me.loadAgeUnit();
        me.loadCalcType();
    };
    //时间选择框
    Class.pt.initLayuiDate = function () {
        laydate.render({//没有默认值
            elem: '#LBEquipResultTH_BCollectTime',
            type: 'time'
        });
        laydate.render({//没有默认值
            elem: '#LBEquipResultTH_ECollectTime',
            type: 'time'
        });
    };
    //核心入口
    resultForm.render = function (options) {
        var me = new Class(options);
        me.initLayuiDate();
        me.iniCombox();
        me.initFilterListeners();
        var id = PK;
        if (id) {
            resultForm.isEdit(id);
        } else {
            me.isAdd();
        }
        return options;
    };
    //加载 
    Class.pt.load = function () {
        var me = this;
        if (me.config.PK) {
            var loadIndex = layer.open({ type: 3 });
            //加载数据
            me.loadDatas(me.config.PK, function (data) {
                var task = setInterval(function () {
                    if (Count == 5) {
                        form.val('equipResultForm', me.changeResult(data.ResultDataValue));
                        layer.close(loadIndex);
                        clearInterval(task);
                        task = null;
                    }
                }, 100);
            });
        }
    };
    /**@overwrite 获取新增的数据*/
    Class.pt.getAddParams = function (data) {
        var me = this;
        var DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
        var entity = JSON.stringify(data).replace(/LBEquipResultTH_/g, "");
        if (entity) entity = JSON.parse(entity);
        if (!entity.DispOrder) delete entity.DispOrder;
        if (entity.LBItem_Id && entity.LBItem_Id != -1) {
            entity.LBItem = { Id: entity.LBItem_Id, DataTimeStamp: DataTimeStamp };
        }
        delete entity.LBItem_Id;
        if (entity.LBEquip_Id) {
            entity.LBEquip = { Id: entity.LBEquip_Id, DataTimeStamp: DataTimeStamp };
        }
        delete entity.LBEquip_Id;
        if (!entity.SampleTypeID) delete entity.SampleTypeID;
        if (!entity.GenderID) delete entity.GenderID;
        if (!entity.AgeUnitID) delete entity.GenderID;
        if (!entity.BCollectTime) {
            delete entity.BCollectTime
        } else {
            entity.BCollectTime = uxutil.date.toServerDate(me.getNewDate("-", false) + " " +entity.BCollectTime);
        }
        if (!entity.ECollectTime) {
            delete entity.ECollectTime
        } else {
            entity.ECollectTime = uxutil.date.toServerDate(me.getNewDate("-", false) + " " +entity.ECollectTime);
        }
        if (entity.LowAge == "" || entity.LowAge < 0) delete entity.LowAge;
        if (entity.HighAge == "" || entity.HighAge < 0) delete entity.HighAge;
        if (!entity.Id) delete entity.Id;

        return {
            entity: entity
        };
        return entity;
    };
    /**@overwrite 获取修改的数据*/
    Class.pt.getEditParams = function (data) {
        var me = this;
        var DataTimeStamp = [0, 0, 0, 0, 0, 0, 0, 0];
        var entity = {};
        entity.entity = JSON.stringify(data).replace(/LBEquipResultTH_/g, "");
        if (entity.entity) entity.entity = JSON.parse(entity.entity);
        //处理数据
        if (entity.entity.BCollectTime) {
            entity.entity.BCollectTime = uxutil.date.toServerDate(me.getNewDate("-", false) + " " + entity.entity.BCollectTime);
        } else {
            entity.entity.BCollectTime = null;
        }
        if (entity.entity.ECollectTime) {
            entity.entity.ECollectTime = uxutil.date.toServerDate(me.getNewDate("-", false) + " " + entity.entity.ECollectTime);
        } else {
            entity.entity.ECollectTime = null;
        }
        if (entity.entity.LowAge == "") {
            entity.entity.LowAge = null;
        }
        if (entity.entity.HighAge == "") {
            entity.entity.HighAge = null;
        }
        if (entity.entity.LBItem_Id == -1) {
            entity.entity.LBItem = null;
            //获得fields
            entity.fields = "LBItem,LBEquip_Id,LBEquip_DataTimeStamp";
        } else {
            entity.entity.LBItem = { Id: entity.entity.LBItem_Id, DataTimeStamp: DataTimeStamp };
            //获得fields
            entity.fields = "LBItem_Id,LBItem_DataTimeStamp,LBEquip_Id,LBEquip_DataTimeStamp";
        }
        delete entity.entity.LBItem_Id;
        entity.entity.LBEquip = { Id: entity.entity.LBEquip_Id, DataTimeStamp: DataTimeStamp };
        delete entity.entity.LBEquip_Id;
        for (var i in entity.entity) {
            if (typeof entity.entity[i] == "object") {
                if (i == "ECollectTime" || i == "BCollectTime") {
                    //不做处理
                } else {
                    continue;
                }
            }
            if (entity.fields != "") {
                entity.fields += "," + i;
            } else {
                entity.fields += i;
            }
        }
        if (PK)
            entity.entity.Id = PK;
        return entity;
    };
    //表单保存处理
    Class.pt.onSaveClick = function (data) {
        var me = Class.pt;
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
                    var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                    parent.layer.close(index); //再执行关闭
                    //layui.event("SaveLBEquipForm", "save", { id: id, formtype: me.config.formtype });
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
    //获得当前日期
    Class.pt.getNewDate = function (seperator, hasTime) {
        var date = new Date();
        var seperator = seperator || "-";
        var hasTime = hasTime || false;//是否要包含当前时间
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var strDate = date.getDate();
        if (month >= 1 && month <= 9) {
            month = "0" + month;
        }
        if (strDate >= 0 && strDate <= 9) {
            strDate = "0" + strDate;
        }
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var seconds = date.getSeconds();
        if (hasTime) {
            var currentdate = year + seperator + month + seperator + strDate + " " + hours + ":" + minutes + ":" + seconds;
            return currentdate;
        } else {
            var currentdate = year + seperator + month + seperator + strDate;
            return currentdate;
        }
    }
    //新增
    Class.pt.isAdd = function () {
        var me = this;
        Class.pt.config.PK = null;
        Class.pt.config.formtype = 'add';
        var inst = new Class(me);
        inst.config.PK = null;
        inst.config.formtype = 'add';
        Class.pt.config.currData = {};
    };
    //编辑
    resultForm.isEdit = function (id) {
        var me = this;
        Class.pt.config.PK = id;
        Class.pt.config.formtype = 'edit';
        var inst = new Class(me);
        inst.config.PK = id;
        inst.config.formtype = 'edit';
        inst.load(id);
    };
    //查看
    resultForm.isShow = function (id) {
        var me = this;
        //  	me.PK=id;
        Class.pt.PK = id;
        Class.pt.formtype = 'show';
        //  	Class.pt.load(id);
        var inst = new Class(me);

        inst.config.PK = id;
        inst.config.formtype = 'show';
        inst.load(id);
        form.render('select');
    };
    //resultForm.onReset = function () {
    //    Class.pt.Empty();
    //}
    //清空表单
    //Class.pt.Empty = function () {
    //    var me = this;
    //    $("#equipResultForm").find('input[type=text],select,input[type=hidden]').each(function () {
    //        $(this).val('');
    //    });
    //    form.render('select');
    //    form.render('checkbox');
    //}
    //事件处理
    Class.pt.initFilterListeners = function () {
        var me = this;
        //处理大于小于号转义为&gt;&lt;
        $(document).click(function () {
            var CalcType = $("#LBEquipResultTH_CalcType").val();
            setTimeout(function () {
                $("#LBEquipResultTH_CalcType+div.layui-form-select input").val(CalcType);
            },0);
        });
        //保存
        form.on('submit(save)', function (obj) {
            window.event.preventDefault();
            if (isNaN(obj.field["LBEquipResultTH_LowAge"]) || isNaN(obj.field["LBEquipResultTH_HighAge"])) {
                layer.msg("年龄只能填写数字!", { icon: 0, anim: 0 });
                return;
            }
            var indexs = layer.load();//显示遮罩层
            me.onSaveClick(obj);
        });
        //重置
        $('#cancel').on('click', function () {
            var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
            parent.layer.close(index); //再执行关闭
        });
    };
    resultForm.render();
});