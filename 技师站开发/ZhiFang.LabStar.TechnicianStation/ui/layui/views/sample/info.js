/**
	@name：检验单信息
	@author：zhangda
	@version 2021-04-26
 */
layui.extend({
}).define(['form', 'table', 'laydate', 'uxutil', 'tableSelect','uxbase'], function (exports) {
    "use strict";

    var $ = layui.$,
        uxutil = layui.uxutil,
        table = layui.table,
        laydate = layui.laydate,
        tableSelect = layui.tableSelect,
        uxbase = layui.uxbase,
        form = layui.form;
    //每一种权限可能包含多个按钮（一起控制）
    //方案：平台：模块+按钮，角色-模块-按钮；技师站：人员+小组+角色；
    //登录后，根据人员获取小组+角色，实例化按钮；
    var app = {};
    //配置
    app.params = {
        //小组ID
        sectionId: null,
        //员工ID
        userId: uxutil.cookie.get(uxutil.cookie.map.USERID),
        //员工姓名
        userName: uxutil.cookie.get(uxutil.cookie.map.USERNAME),
        //当前检验单
        testFormRecord: [],
        //列表中选中的样本号
        curSampleNo: null,
        //表单显示模式
        formtype: 'show',
        //表单元素ID
        formELemID: 'sampleForm',
        //加载层
        loadIndex: null,
        //是否根据病历号提取患者信息
        isExtractInfoByPatNo: true,
        //就诊信息替换方式
        replacePatientType: 1,//就诊信息替换方式：1：完全替换（默认） 2：已有设置不替换，替换没有设置的 3：不替换
        //记录就诊信息替换方式  -- 页面选择方式
        replacePatientTypeRecordByPage: null,//就诊信息替换方式：1：完全替换（默认） 2：已有设置不替换，替换没有设置的 3：不替换
        //实验室ID
        labid: uxutil.cookie.get(uxutil.cookie.map.LABID)
    };
    //服务地址
    app.url = {
        //根据样本单id 获得样本单具体信息
        getLisTestFormById: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisTestFormById?isPlanish=true',
        //新增检验单信息
        addTestFormUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_AddSingleLisTestForm',
        //修改检验单信息
        //editTestFormUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_EditLisTestFormByField',
        //根据指定的样本号生成新样本号
        getNewSampleNoUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_GetNewSampleNoByOldSampleNo',
        //根据出生日期获得年龄、年龄单位、年龄描述
        getAgeInfoByBirthdayUrl: uxutil.path.ROOT + '/ServerWCF/LabStarCommonService.svc/GetPatientAge',
        //就诊类型
        getSickTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?isPlanish=true',
        //样本类型
        getSampleTypeUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?isPlanish=true',
        //用户 //医生 -- 平台
        getUserUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true',
        //枚举
        getEnumUrl: uxutil.path.ROOT + '/ServerWCF/CommonService.svc/GetClassDic',
        //病区 //科室 -- 平台
        getDeptUrl: uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?isPlanish=true',
        //诊断
        getDiagUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBDiagByHQL?isPlanish=true',
        //获得就诊信息
        getPatientUrl: uxutil.path.ROOT + '/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisPatientByHQL?isPlanish=true'
    };
    //记录查询出的初始数据
    app.initialData = null;
    //病历号查询出的就诊信息
    app.PatientInfoByPatNo = null;
    //固有信息
    app.FixedInfoFields = ["LisPatient_CName", "LisPatient_GenderID", "LisPatient_GenderName", "LisPatient_Birthday", "LisPatient_PatHeight", "LisPatient_PatWeight",
        "LisPatient_FolkID", "LisPatient_FolkName", "LisPatient_PatAddress", "LisPatient_PatPhoto", "LisPatient_PhoneCode", "LisPatient_WeChatNo",
        "LisPatient_EMailAddress", "LisPatient_PatType", "LisPatient_IDCardNo", "LisPatient_HisPatNo", "LisPatient_PatNo", "LisPatient_PatCardNo",
        "LisPatient_InPatNo", "LisPatient_ExamNo", "LisPatient_MedicareNo", "LisPatient_UnionPayNo", "LisPatient_HealthCardNo", "LisPatient_PowerCardNo"];
    //就诊信息
    app.PatientInfoFields = ["LisPatient_Age", "LisPatient_AgeUnitID", "LisPatient_AgeUnitName", "LisPatient_AgeDesc",
        "LisPatient_SickTypeID", "LisPatient_SickType", "LisPatient_DiagID", "LisPatient_DiagName", "LisPatient_DoctorID", "LisPatient_DoctorName",
        "LisPatient_DoctorTell", "LisPatient_ExecDeptID", "LisPatient_DeptID", "LisPatient_DeptName", "LisPatient_VisitTimes", "LisPatient_VisitDate",
        "LisPatient_DistrictID", "LisPatient_DistrictName", "LisPatient_WardID", "LisPatient_WardName", "LisPatient_Bed",
        "LisPatient_HISComment", "LisPatient_PatComment"];
    //初始化方法 -- 外部调用
    app.init = function (params) {
        var me = this;
        me.params = $.extend({}, me.params, params);
        if (me.params.isExtractInfoByPatNo)
            $("#LisTestForm_LisPatient_PatNo").attr("placeholder", "回车查询信息代入");
        else
            $("#LisTestForm_LisPatient_PatNo").attr("placeholder", "");
        me.initDateListeners();
        me.initSelect();
        me.initListeners();
    };
    //监听
    app.initListeners = function () {
        var me = this;
        //监听出生日期 获得年龄、年龄单位、年龄描述
        $('#sampleForm').on('change','#LisTestForm_LisPatient_Birthday', function () {
            var value = uxutil.date.toString($(this).val(), false);
            $(this).val(value);
            me.birthdayListener(value);
        });
        //监听回车按下事件--根据病历号查询代入就诊信息
        $("#sampleForm").on("keydown",':input',(function (event) {
            switch (event.keyCode) {
                case 13:
                    //判断焦点是否在该输入框
                    if (document.activeElement == document.getElementById("LisTestForm_LisPatient_PatNo")) {
                        var PatNo = $("#LisTestForm_LisPatient_PatNo").val();
                        me.getPatientByPatNo(PatNo, function (data) {
                            var list = data[0];
                            me.PatientInfoByPatNo = list;
                            me.params.replacePatientTypeRecordByPage = null;
                            me.setValueByPatientInfo(list);
                        });
                    }
                    //焦点定位下一个
                    me.nextInputFocus($(this));
            }
        }));
        //监听病历号图标点击
        $("#LisTestForm_LisPatient_PatNo+i.layui-icon").on('click', function () {
            var PatNo = $("#LisTestForm_LisPatient_PatNo").val();
            if (!$("#LisTestForm_LisPatient_PatNo").hasClass("layui-disabled")) me.openPatNoCheckLayer(PatNo);
        });
    };
    //新增
    app.isAdd = function () {
        var me = this,
            load = layer.load();
        me.clear();
        me.setDisabled(false);
        me.params.formtype = 'add';
        me.setFormInfoStatus();
        me.setSampleNo();
        me.initialData = null;
        layer.close(load);
    };
    //编辑
    app.isEdit = function (testFormRecord) {
        var me = this,
            testFormRecord = testFormRecord || [],
            id = (testFormRecord && testFormRecord.length > 0) ? testFormRecord[0]["LisTestForm_Id"] : null;
        if (id)
            me.setDisabled(false);
        else
            me.setDisabled(true);
        //存储当前选中样本单信息
        me.params.formtype = 'edit';
        me.setFormInfoStatus();
        me.params.curSampleNo = (testFormRecord && testFormRecord.length > 0) ? testFormRecord[0]["LisTestForm_GSampleNo"] : null;
        me.params.testFormRecord = testFormRecord || [];
        me.load(id);
    };
    //show
    app.isShow = function (testFormRecord) {
        var me = this,
            testFormRecord = testFormRecord || [],
            id = (testFormRecord && testFormRecord.length > 0) ? testFormRecord[0]["LisTestForm_Id"] : null;
        me.setDisabled(true);
        //存储当前选中样本单信息
        me.params.formtype = 'show';
        me.setFormInfoStatus();
        me.params.curSampleNo = (testFormRecord && testFormRecord.length > 0) ? testFormRecord[0]["LisTestForm_GSampleNo"] : null;
        me.params.testFormRecord = testFormRecord || [];
        me.load(id);
    };
    //设置样本单信息编辑状态
    app.setFormInfoStatus = function () {
        var me = this,
            type = me.params.formtype,
            text = type == 'add' ? '新增' : (type == 'edit' ? '编辑' : '查看'),
            color = type == 'add' ? 'red' : '#009688';
        $("#testFormInfoStatus").html("【" + text+"】").css("color",color);
    };
    //样本单信息赋值 -- 不传id则清空
    app.load = function (id) {
        var me = this,
            url = me.url.getLisTestFormById,
            fields = [],
            id = id || null;

        //清空表单
        $("#" + me.params.formELemID + " :input").each(function () {
            $(this).val('');
            fields.push($(this).attr('name'));
        });
        if (!id) return;
        url += '&id=' + id;
        url += '&fields=' + fields.join();
        url += '&t=' + new Date().getTime();
        
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue.replace(/\u000d/g, "\\r").replace(/\u000a/g, "\\n").replace(/\\n/g, "\|").replace(/\n/g, "\|")) : {};
                    data = me.changeResult(data);
                    form.val(me.params.formELemID, data);
                    //me.getPatientInfo();
                    me.initialData = data;
                }
            }
        });
    };
    //清空表单
    app.clear = function () {
        var me = this;
        $("#" + me.params.formELemID + " :input").each(function () {
            if ($(this).attr("name") == 'LisTestForm_TestType')
                $(this).val('1');
            else
                $(this).val('');
        });
    };
    //禁用设置
    app.setDisabled = function (isDisabled) {
        var me = this,
            isDisabled = isDisabled || false;
        $("#" + me.params.formELemID + " :input").each(function () {
            if ($(this).hasClass("default_readonly")) return true;
            $(this).prop("disabled", isDisabled);
            if (isDisabled && !$(this).hasClass("layui-disabled"))
                $(this).addClass("layui-disabled");
            else if (!isDisabled && $(this).hasClass("layui-disabled"))
                $(this).removeClass("layui-disabled");
        });
    };
    //设置新样本号
    app.setSampleNo = function () {
        var me = this,
            NewGTestDate = uxutil.date.toString(new Date(uxutil.server.date.getDate()), true),
            GSampleNo = null;//新样本号
        //计算样本号传递 
        //如果当前检验日期为当日，新增最近样本号样本
        //如果当前检验日期不为当日，新增到当日的最大样本号
        //---
        //存在检验单选中行数据时，如果选中行是当天的样本号，则将该样本号记下，用于样本新增自动计算参数，
        //如果不是当天的样本号，或者如果不存在选中行，则样本号置空
        if (me.params.testFormRecord && me.params.testFormRecord.length > 0) {
            var NowDate = NewGTestDate,
                GTestDate = uxutil.date.toString(me.params.testFormRecord[0]['LisTestForm_GTestDate'], true);
            if (GTestDate == NowDate) {
                me.params.curSampleNo = me.params.testFormRecord[0]['LisTestForm_GSampleNo'];
            } else {
                me.params.curSampleNo = null;
            }
        } else {
            me.params.curSampleNo = null;
        }
        var load = layer.load();
        //获得新样本号
        me.getNewSampleNoByOldSampleNo(me.params.sectionId, NewGTestDate, me.params.curSampleNo, function (data) {
            if (data.success) {
                GSampleNo = data.value;
            } else {
                uxbase.MSG.onError(data.msg);
            }
            var formData = {
                LisTestForm_GTestDate: NewGTestDate,
                LisTestForm_MainTesterId: me.params.userId,
                LisTestForm_MainTester: me.params.userName,
                LisTestForm_GSampleNo: GSampleNo || ""
            };
            form.val(me.params.formELemID, formData);
            //自动新增处理
            var isAutoAdd = $("#isAutoAdd").prop('checked');
            if (!isAutoAdd) $("#isAutoAddInput").val(GSampleNo || "");
            layer.close(load);
        });
    };
    //根据指定的样本号生成新样本号
    app.getNewSampleNoByOldSampleNo = function (sectionID, testDate, curSampleNo, callback) {
        var me = this,
            url = me.url.getNewSampleNoUrl,
            data = { sectionID: sectionID, testDate: testDate };
        if (curSampleNo) data["curSampleNo"] = curSampleNo;

        //保存到后台
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify(data)
        }, function (res) {
            callback(res);
        });
    };
    //新增保存处理
    app.onAddSave = function (callback) {
        var me = this;
        //验证表单
        var msg = me.verify();
        if (msg != "") {
            uxbase.MSG.onWarn(msg);
            return;
        }
        //新增状态则执行新增操作
        if (me.params.formtype == 'add') {
            me.params.loadIndex = layer.load();
            //新增检验单信息
            me.onAddTestFormInfo(false, function (data) {
                layer.close(me.params.loadIndex);
                if (data.success) {
                    layui.event('info', 'add', { id: data.value.id });
                    callback(data.value.id);
                    //layer.msg("保存成功！", { icon: 6, anim: 0 });
                } else {
                    if (data.code == 1) {//样本号已经存在
                        layer.confirm(data.msg + '</BR></BR>选择“确定”：自动生成样本号并保存</BR>选择“取消”：放弃此次保存！', { icon: 3, title: '提示' }, function (index) {
                            me.params.loadIndex = layer.load();
                            me.onAddTestFormInfo(true, function (data) {
                                layer.close(me.params.loadIndex);
                                if (data.success) {
                                    layui.event('info', 'add', { id: data.value.id });
                                    callback(data.value.id);
                                    //layer.msg("保存成功！", { icon: 6, anim: 0 });
                                } else {
                                    layer.msg(data.msg, { icon: 5, anim: 0 });
                                }
                            });
                            layer.close(index);
                        });
                    } else {
                        uxbase.MSG.onError(data.ErrorInfo);
                    }
                }
            });
        }
    };
    //获得编辑保存信息
    app.getEditSaveParams = function () {
        var me = this,
            entity = me.getTestFormInfo(),//获取页面检验单信息
            testFormFields = [],//检验单-需要更新字段
            PatientInfo = me.getSavePatientInfo(),//页面就诊信息
            patientFields = [];//就诊信息-需要更新字段

        //验证表单
        var msg = me.verify();
        if (msg != "") {
            uxbase.MSG.onWarn(msg);
            return false;
        }
        //检验单
        entity.Id = (me.params.testFormRecord && me.params.testFormRecord.length > 0) ? me.params.testFormRecord[0]["LisTestForm_Id"] : null;
        if (entity.Id) {
            for (var e in entity) {
                testFormFields.push(e);
            }
        }
        //就诊信息
        if (PatientInfo) {
            entity.LisPatient = PatientInfo;
            testFormFields.push("LisPatient_Id");
        }
        //患者-需要更新的字段
        if (PatientInfo) {
            for (var o in PatientInfo) {
                patientFields.push(o);
            }
        }
        return { testForm: entity, testFormFields: testFormFields.join(','), patientFields: patientFields.join(',') };
    };
    //验证表单
    app.verify = function () {
        var me = this,
            DATE_FORMAT = /^[0-9]{4}-[0-1]?[0-9]{1}-[0-3]?[0-9]{1}$/, //判断是否是日期格式 yyyy-mm-dd
            DATETIME_FORMAT = /^(?:19|20)[0-9][0-9]-(?:(?:0[1-9])|(?:1[0-2]))-(?:(?:[0-2][1-9])|(?:[1-3][0-1])) (?:(?:[0-2][0-3])|(?:[0-1][0-9])):[0-5][0-9]:[0-5][0-9]$/,//yyyy-mm-dd hh:MM:ss
            msg = [];
        $("#sampleForm :input").each(function () {
            var label = $(this).parent().prev().html(),
                name = $(this).attr("name"),
                value = $(this).val();
            //检测日期 / 样本号不为空
            if ((name == "LisTestForm_GTestDate" || name == "LisTestForm_GSampleNo") && value == "") {
                msg.push(label + "不能为空!");
            }
            //验证是否都是日期
            if ($(this).hasClass("myDate") && value != "") {
                if (name == "LisTestForm_GTestDate") {
                    if (!uxutil.date.isValid(value) || !DATE_FORMAT.test(value)) {
                        msg.push(label + "日期格式不正确!");
                    }
                } else {
                    if (!uxutil.date.isValid(value) || !DATETIME_FORMAT.test(value)) {
                        msg.push(label + "日期格式不正确!");
                    }
                }
                if (name == "LisTestForm_LisPatient_Birthday") {
                    if (new Date(value).getTime() > new Date().getTime()) {
                        msg.push(label + "不能超过当前日期!");
                    }
                }
            }
        });

        return msg.join();
    };
    //新增检验单信息
    app.onAddTestFormInfo = function (isCreateSampleNo, callback) {
        var me = this,
            url = me.url.addTestFormUrl,
            entity = me.getTestFormInfo(),
            PatientInfo = me.getSavePatientInfo();

        //小组样本描述：GSampleInfo????
        //主状态-检验中
        entity.MainStatusID = 0;
        //样本来源-lis录入
        entity.iSource = 1;
        //检验过程状态-检验单生成
        entity.StatusID = 1;
        //小组属性
        entity.LBSection = { Id: me.params.sectionId, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] };
        //就诊信息
        //entity.LisPatient = {Id:PatID,DataTimeStamp:[0,0,0,0,0,0,0,0]};
        if (PatientInfo) {
            entity.LisPatient = PatientInfo;
        }

        //保存到后台
        uxutil.server.ajax({
            url: url,
            type: 'post',
            data: JSON.stringify({ testForm: entity, isCreateSampleNo: isCreateSampleNo })
        }, function (data) {
            callback(data);
        });
    };
    //获取页面患者就诊信息
    app.getPatientInfo = function () {
        var me = this,
            values = me.getSampleFormInfo();
        //患者就诊信息
        var entity = {};
        //患者主键ID
        if (values.LisTestForm_LisPatient_Id) {
            entity.Id = values.LisTestForm_LisPatient_Id;
        }
        //分区日期
        if (values.LisTestForm_GTestDate && !entity.Id) {
            entity.PartitionDate = uxutil.date.toServerDate(values.LisTestForm_GTestDate);
        }
        //姓名
        if (values.LisTestForm_LisPatient_CName) {
            entity.CName = values.LisTestForm_LisPatient_CName;
        } else {
            entity.CName = "";
        }
        //性别
        if (values.LisTestForm_LisPatient_GenderID) {
            entity.GenderID = values.LisTestForm_LisPatient_GenderID;
            entity.GenderName = $("#LisTestForm_LisPatient_GenderID option:selected").text();
        } else {
            entity.GenderID = null;
            entity.GenderName = "";
        }
        //出生日期
        if (values.LisTestForm_LisPatient_Birthday) {
            entity.Birthday = uxutil.date.toServerDate(decodeURIComponent(values.LisTestForm_LisPatient_Birthday.replace("+", " ")));
        } else {
            entity.Birthday = null;
        }
        //年龄
        if (values.LisTestForm_LisPatient_Age) {
            entity.Age = values.LisTestForm_LisPatient_Age;
        } else {
            entity.Age = null;
        }
        //年龄单位
        if (values.LisTestForm_LisPatient_AgeUnitID) {
            entity.AgeUnitID = values.LisTestForm_LisPatient_AgeUnitID;
            entity.AgeUnitName = $("#LisTestForm_LisPatient_AgeUnitName option:selected").text();
        } else {
            entity.AgeUnitID = null;
            entity.AgeUnitName = "";
        }
        //年龄描述
        if (values.LisTestForm_LisPatient_AgeDesc) {
            entity.AgeDesc = values.LisTestForm_LisPatient_AgeDesc;
        } else {
            entity.AgeDesc = "";
        }
        //体重
        if (values.LisTestForm_LisPatient_PatWeight) {
            entity.PatWeight = values.LisTestForm_LisPatient_PatWeight;
        } else {
            entity.PatWeight = null;
        }
        //身高
        if (values.LisTestForm_LisPatient_PatHeight) {
            entity.PatHeight = values.LisTestForm_LisPatient_PatHeight;
        } else {
            entity.PatHeight = null;
        }
        //病历号
        if (values.LisTestForm_LisPatient_PatNo) {
            entity.PatNo = values.LisTestForm_LisPatient_PatNo;
        } else {
            entity.PatNo = "";
        }
        //床号
        if (values.LisTestForm_LisPatient_Bed) {
            entity.Bed = values.LisTestForm_LisPatient_Bed;
        } else {
            entity.Bed = "";
        }
        //病区
        if (values.LisTestForm_LisPatient_DistrictID) {
            entity.DistrictID = values.LisTestForm_LisPatient_DistrictID;
            entity.DistrictName = values.LisTestForm_LisPatient_DistrictName;
        } else {
            entity.DistrictID = null;
            entity.DistrictName = "";
        }
        //科室
        if (values.LisTestForm_LisPatient_DeptID) {
            entity.DeptID = values.LisTestForm_LisPatient_DeptID;
            entity.DeptName = values.LisTestForm_LisPatient_DeptName;
        } else {
            entity.DeptID = null;
            entity.DeptName = "";
        }
        //医生
        if (values.LisTestForm_LisPatient_DoctorID) {
            entity.DoctorID = values.LisTestForm_LisPatient_DoctorID;
            entity.DoctorName = values.LisTestForm_LisPatient_DoctorName;
        } else {
            entity.DoctorID = null;
            entity.DoctorName = "";
        }
        //就诊类型
        if (values.LisTestForm_LisPatient_SickTypeID) {
            entity.SickTypeID = values.LisTestForm_LisPatient_SickTypeID;
            entity.SickType = $("#LisTestForm_LisPatient_SickTypeID option:selected").text();
        } else {
            entity.SickTypeID = null;
            entity.SickType = "";
        }
        //诊断
        if (values.LisTestForm_LisPatient_DiagID) {
            entity.DiagID = values.LisTestForm_LisPatient_DiagID;
            entity.DiagName = values.LisTestForm_LisPatient_DiagName;
        } else {
            entity.DiagID = null;
            entity.DiagName = "";
        }
        //临床HIS备注
        if (values.LisTestForm_LisPatient_HISComment) {
            entity.HISComment = values.LisTestForm_LisPatient_HISComment;
        } else {
            entity.HISComment = "";
        }

        //病人信息是否为空
        var isEmpty = true;
        for (var i in entity) {
            if (entity[i]) {
                isEmpty = false;
            }
        }
        if (isEmpty) {
            entity = null;
        }
        return entity;
    };
    //获取页面检验单信息
    app.getTestFormInfo = function () {
        var me = this,
            values = me.getSampleFormInfo();

        var entity = {};

        //=============关键信息=============
        //检测日期
        if (values.LisTestForm_GTestDate) {
            entity.GTestDate = uxutil.date.toServerDate(values.LisTestForm_GTestDate);
        }
        //小组检测编号（样本号）
        if (values.LisTestForm_GSampleNo) {
            entity.GSampleNo = values.LisTestForm_GSampleNo;
        } else {
            entity.GSampleNo = "";
        }
        //样本号排序
        if (values.LisTestForm_GSampleNoForOrder) {
            entity.GSampleNoForOrder = values.LisTestForm_GSampleNoForOrder;
        }
        //条码号
        if (values.LisTestForm_BarCode) {
            entity.BarCode = values.LisTestForm_BarCode;
        } else {
            entity.BarCode = "";
        }
        //就诊类型
        if (values.LisTestForm_LisPatient_SickTypeID) {
            entity.SickTypeID = values.LisTestForm_LisPatient_SickTypeID;
        } else {
            entity.SickTypeID = null;
        }
        //样本类型
        if (values.LisTestForm_GSampleTypeID) {
            entity.GSampleTypeID = values.LisTestForm_GSampleTypeID;
            entity.GSampleType = $("#LisTestForm_GSampleTypeID option:selected").text();
        } else {
            entity.GSampleTypeID = null;
            entity.GSampleType = "";
        }
        //姓名
        if (values.LisTestForm_LisPatient_CName) {
            entity.CName = values.LisTestForm_LisPatient_CName;
        } else {
            entity.CName = "";
        }
        //病历号
        if (values.LisTestForm_LisPatient_PatNo) {
            entity.PatNo = values.LisTestForm_LisPatient_PatNo;
        } else {
            entity.PatNo = "";
        }
        //加急标识
        if (values.LisTestForm_UrgentState) {
            entity.UrgentState = values.LisTestForm_UrgentState;
        } else {
            entity.UrgentState = "";
        }
        //送检目的
        if (values.LisTestForm_Testaim) {
            entity.Testaim = values.LisTestForm_Testaim;
        } else {
            entity.Testaim = "";
        }
        //检验样本备注
        if (values.LisTestForm_FormMemo) {
            entity.FormMemo = values.LisTestForm_FormMemo.replace(/\|/g, "\n");
        } else {
            entity.FormMemo = "";
        }
        //特殊样本标注
        if (values.LisTestForm_SampleSpecialDesc) {
            entity.SampleSpecialDesc = values.LisTestForm_SampleSpecialDesc.replace(/\|/g, "\n");
        } else {
            entity.SampleSpecialDesc = "";
        }

        //=============检验信息=============
        //采样时间
        if (values.LisTestForm_CollectTime) {
            entity.CollectTime = uxutil.date.toServerDate(decodeURIComponent(values.LisTestForm_CollectTime).replace("+", " "));
        } else {
            entity.CollectTime = null;
        }
        //签收时间
        if (values.LisTestForm_InceptTime) {
            entity.InceptTime = uxutil.date.toServerDate(decodeURIComponent(values.LisTestForm_InceptTime).replace("+", " "));
        } else {
            entity.InceptTime = null;
        }
        //核收时间
        if (values.LisTestForm_ReceiveTime) {
            entity.ReceiveTime = uxutil.date.toServerDate(decodeURIComponent(values.LisTestForm_ReceiveTime).replace("+", " "));
        } else {
            entity.ReceiveTime = null;
        }
        //审核者
        if (values.LisTestForm_CheckerID) {
            entity.CheckerID = values.LisTestForm_CheckerID;
            entity.Checker = values.LisTestForm_Checker;
        } else {
            entity.CheckerID = null;
            entity.Checker = "";
        }
        //审核时间
        if (values.LisTestForm_CheckTime) {
            entity.CheckTime = uxutil.date.toServerDate(values.LisTestForm_CheckTime);
        } else {
            entity.CheckTime = null;
        }
        //检验者
        if (values.LisTestForm_MainTesterId) {
            entity.MainTesterId = values.LisTestForm_MainTesterId;
            entity.MainTester = values.LisTestForm_MainTester;
        } else {
            entity.MainTesterId = null;
            entity.MainTester = "";
        }
        //检验备注
        if (values.LisTestForm_TestComment) {
            entity.TestComment = values.LisTestForm_TestComment.replace(/\|/g, "\n");
        } else {
            entity.TestComment = "";
        }
        //检验评语
        if (values.LisTestForm_TestInfo) {
            entity.TestInfo = values.LisTestForm_TestInfo.replace(/\|/g, "\n");
        } else {
            entity.TestInfo = "";
        }
        //检验类型
        if (values.LisTestForm_TestType) {
            entity.TestType = values.LisTestForm_TestType;
        } else {
            entity.TestType = null;
        }
        //=============就诊信息=============
        //性别
        if (values.LisTestForm_LisPatient_GenderID) {
            entity.GenderID = values.LisTestForm_LisPatient_GenderID;
        } else {
            entity.GenderID = null;
        }
        //年龄
        if (values.LisTestForm_LisPatient_Age) {
            entity.Age = values.LisTestForm_LisPatient_Age;
        } else {
            entity.Age = null;
        }
        //体重
        if (values.LisTestForm_LisPatient_PatWeight) {
            entity.PatWeight = values.LisTestForm_LisPatient_PatWeight;
        } else {
            entity.PatWeight = null;
        }
        //身高
        if (values.LisTestForm_LisPatient_PatHeight) {
            entity.PatHeight = values.LisTestForm_LisPatient_PatHeight;
        } else {
            entity.PatWeight = null;
        }
        //年龄单位
        if (values.LisTestForm_LisPatient_AgeUnitID) {
            entity.AgeUnitID = values.LisTestForm_LisPatient_AgeUnitID;
        } else {
            entity.AgeUnitID = null;
        }
        //年龄描述
        if (values.LisTestForm_LisPatient_AgeDesc) {
            entity.AgeDesc = values.LisTestForm_LisPatient_AgeDesc;
        } else {
            entity.AgeDesc = "";
        }
        //病区
        if (values.LisTestForm_LisPatient_DistrictID) {
            entity.DistrictID = values.LisTestForm_LisPatient_DistrictID;
        } else {
            entity.DistrictID = null;
        }
        //科室
        if (values.LisTestForm_LisPatient_DeptID) {
            entity.DeptID = values.LisTestForm_LisPatient_DeptID;
        } else {
            entity.DeptID = null;
        }

        return entity;
    };
    //@overwrite 返回数据处理方法
    app.changeResult = function (data) {
        var me = this;
        //检测日期
        data.LisTestForm_GTestDate = uxutil.date.toString(data.LisTestForm_GTestDate, true);
        //采样时间
        data.LisTestForm_CollectTime = uxutil.date.toString(data.LisTestForm_CollectTime);
        //签收时间
        data.LisTestForm_InceptTime = uxutil.date.toString(data.LisTestForm_InceptTime);
        //核收时间
        data.LisTestForm_ReceiveTime = uxutil.date.toString(data.LisTestForm_ReceiveTime);
        //审核时间
        data.LisTestForm_CheckTime = uxutil.date.toString(data.LisTestForm_CheckTime);
        //出生日期
        data.LisTestForm_LisPatient_Birthday = uxutil.date.toString(data.LisTestForm_LisPatient_Birthday);
        return data;
    };
    //出生日期监听
    app.birthdayListener = function (birthday) {
        var me = this,
            birthday = birthday || null,
            collectTime = $('LisTestForm_CollectTime').val() || null,
            testTime = $('LisTestForm_ReceiveTime').val() || new Date(),
            DataAddTime = uxutil.date.toString(new Date());
        if (!birthday) return;
        collectTime = collectTime == null ? null : uxutil.date.toString(collectTime);
        testTime = uxutil.date.toString(testTime);
        me.getAgeInfoByBirthday(collectTime, testTime, DataAddTime, birthday, function (value) {
            $('#LisTestForm_LisPatient_Age').val(value["Age"]);
            $('#LisTestForm_LisPatient_AgeUnitID').val(value["AgeUnitID"]);
            $('#LisTestForm_LisPatient_AgeUnitName').val(value["AgeUnitName"]);
            $('#LisTestForm_LisPatient_AgeDesc').val(value["AgeDesc"]);
        });
    };
    //根据出生日期获得年龄、年龄单位、年龄描述
    app.getAgeInfoByBirthday = function (collectTime, testTime, DataAddTime, Birthday, CallBack) {
        var me = this,
            url = me.url.getAgeInfoByBirthdayUrl,
            collectTime = collectTime || null,
            testTime = testTime || null,
            DataAddTime = DataAddTime || null,
            Birthday = Birthday || null;
        if (!Birthday) return;
        url += "?birthday=" + Birthday;
        if (collectTime) url += "&collectTime=" + collectTime;
        if (testTime) url += "&testTime=" + testTime;
        if (DataAddTime) url += "&DataAddTime=" + DataAddTime;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                CallBack(res.value);
            }
        });
    };
    //获得检验单信息--name:value
    app.getSampleFormInfo = function () {
        var me = this,
            entity = {},
            values = $('#' + me.params.formELemID).serialize();
        values = values.split("&");
        $.each(values, function (i, item) {
            entity[item.split("=")[0]] = decodeURI(item.split("=")[1]);
        });
        return entity;
    };
    //根据病历号获得就诊信息
    app.getPatientByPatNo = function (PatNo, CallBack) {
        var me = this,
            PatNo = PatNo || null,
            url = me.url.getPatientUrl + "&where=PatNo='" + PatNo + "'";
        url += "&fields=" + me.FixedInfoFields.join(",") + me.PatientInfoFields.join(",") + ",LisPatient_PartitionDate";
        url += '&sort=[{"property": "LisPatient_PartitionDate", "direction": "DESC"}]';
        if (!PatNo || !me.params.isExtractInfoByPatNo) return;
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    CallBack(data);
                }
            }
        });
    };
    //设置页面就诊信息根据病历号查询出的信息
    app.setValueByPatientInfo = function (list) {
        var me = this,
            replacePatientType = me.params.replacePatientTypeRecordByPage || me.params.replacePatientType,
            list = list || me.PatientInfoByPatNo;
        if (!list) return;
        //固有信息
        $("#LisTestForm_LisPatient_PatNo").val(list["LisPatient_PatNo"]);
        $("#LisTestForm_LisPatient_CName").val(list["LisPatient_CName"]);
        $("#LisTestForm_LisPatient_GenderID").val(list["LisPatient_GenderID"]);
        $("#LisTestForm_LisPatient_Birthday").val(list["LisPatient_Birthday"].replace(RegExp("/", "g"), "-"));
        $("#LisTestForm_LisPatient_PatHeight").val(list["LisPatient_PatHeight"]);
        $("#LisTestForm_LisPatient_PatWeight").val(list["LisPatient_PatWeight"]);
        //重新计算年龄信息
        me.birthdayListener(list["LisPatient_Birthday"]);
        //就诊信息
        if (replacePatientType != 3) {
            if (list["LisPatient_SickTypeID"] || replacePatientType == 1) $("#LisTestForm_LisPatient_SickTypeID").val(list["LisPatient_SickTypeID"]);
            if (list["LisPatient_DiagID"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DiagID").val(list["LisPatient_DiagID"]);
            if (list["LisPatient_DiagName"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DiagName").val(list["LisPatient_DiagName"]);
            if (list["LisPatient_DoctorID"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DoctorID").val(list["LisPatient_DoctorID"]);
            if (list["LisPatient_DoctorName"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DoctorName").val(list["LisPatient_DoctorName"]);
            if (list["LisPatient_DeptID"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DeptID").val(list["LisPatient_DeptID"]);
            if (list["LisPatient_DeptName"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DeptName").val(list["LisPatient_DeptName"]);
            if (list["LisPatient_DistrictID"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DistrictID").val(list["LisPatient_DistrictID"]);
            if (list["LisPatient_DistrictName"] || replacePatientType == 1) $("#LisTestForm_LisPatient_DistrictName").val(list["LisPatient_DistrictName"]);
            if (list["LisPatient_Bed"] || replacePatientType == 1) $("#LisTestForm_LisPatient_Bed").val(list["LisPatient_Bed"]);
            if (list["LisPatient_HISComment"] || replacePatientType == 1) $("#LisTestForm_LisPatient_HISComment").val(list["LisPatient_HISComment"]);
        }
        form.render();
    };
    //获得需要保存的就诊信息
    app.getSavePatientInfo = function () {
        var me = this,
            replacePatientType = me.params.replacePatientTypeRecordByPage || me.params.replacePatientType,
            pageInfo = me.getPatientInfo(),
            patNoSearchInfo = me.PatientInfoByPatNo;
        if (me.PatientInfoByPatNo) {
            //固有信息
            $.each(me.FixedInfoFields, function (i, item) {
                if (typeof pageInfo[item.replace("LisPatient_", "")] == 'undefined') {
                    var value = patNoSearchInfo[item];
                    if (item == "LisPatient_FolkID" || item == "LisPatient_PatPhoto") value = value ? value : null;
                    pageInfo[item.replace("LisPatient_", "")] = value;
                }
            });
            //就诊信息
            if (replacePatientType != 3) {
                $.each(me.PatientInfoFields, function (j, itemJ) {
                    //页面中不存在该字段
                    if (typeof pageInfo[itemJ.replace("LisPatient_", "")] == 'undefined' && (patNoSearchInfo[itemJ] || replacePatientType == 1)) {
                        var value = patNoSearchInfo[itemJ];
                        if (itemJ == "LisPatient_WardID" || itemJ == "LisPatient_VisitDate" || itemJ == "LisPatient_FolkID" || itemJ == "LisPatient_ExecDeptID") value = value ? value : null;
                        if (itemJ == "LisPatient_VisitTimes") value = value ? value : 0;
                        pageInfo[itemJ.replace("LisPatient_", "")] = value;
                    }
                });
            }
        }
        //清楚之前记录的就诊信息
        me.PatientInfoByPatNo = null;
        me.params.replacePatientTypeRecordByPage = null;
        return pageInfo;
    };
    //打开根据病历号查询出的就诊信息选择
    app.openPatNoCheckLayer = function (PatNo,CName) {
        var me = this,
            PatNo = PatNo || "",
            CName = CName || "";
        layer.open({
            title: '选择代入信息',
            type: 2,
            content: 'basic/patient/index.html?PatNo=' + PatNo + '&CName=""&t=' + new Date().getTime(),
            maxmin: true,
            resize: true,
            area: ['1100px', '500px'],
            btn: ['确定'],
            yes: function (index, layero) {
                //var body = layer.getChildFrame('body', index);//这里是获取打开的窗口元素
                var iframeWin = window[layero.find('iframe')[0]['name']], //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
                    patient = iframeWin.layui.patient,
                    checkRowData = patient["checkRowData"],
                    replacePatientType = patient["replacePatientType"];
                if (!checkRowData || checkRowData.length <= 0) {
                    layer.msg("请选择代入信息!", { icon: 0, anim: 0 });
                } else {
                    me.PatientInfoByPatNo = checkRowData[0];
                    me.params.replacePatientTypeRecordByPage = replacePatientType;
                    me.setValueByPatientInfo(checkRowData[0]);
                    layer.close(index);
                }
            },
            success: function (layero, index) { },
            end: function () { }
        });
    };
    //根据记录的初始数据与当前控件值对比 判断是否修改过数据 -- 外部调用
    app.getIsEditDataByCompare = function () {
        var me = this,
            isEdit = false,
            initialData = me.initialData || null;

        if (!initialData) return isEdit;

        $("#" + me.params.formELemID + " :input").each(function () {
            var initValue = initialData[$(this).attr("name")] == null ? "" : initialData[$(this).attr("name")];
            if ($(this).attr("name") && initValue != $(this).val()) {
                isEdit = true;
            }
        });
        return isEdit;
    };
    //焦点定位一下:input
    app.nextInputFocus = function (elem) {
        var me = this,
            elem = elem || null;

        if (!elem || elem.length == 0) return;

        var layuiFormIitem = elem.parents(".layui-form-item").next();

        if (layuiFormIitem.length == 0) return;

        if (layuiFormIitem.hasClass("layui-hide") || layuiFormIitem.find(".layui-input-block>:input").hasClass("layui-disabled")) {
            me.nextInputFocus(layuiFormIitem.find(".layui-input-block>:input"));
        } else {
            layuiFormIitem.find("input").focus();
        }
    };

    //初始化yyyy-mm-dd
    app.initDate = function (id) {
        var me = this;
        //检测日期 yyyy-MM-dd
        laydate.render({//没有默认值
            elem: '#' + id,//'#LisTestForm_GTestDate',
            eventElem: '#' + id + '+i.layui-icon',
            type: 'date',
            show: true
        });
    };
    //初始化yyyy-mm-dd HH:MM:ii
    app.initDateTime = function (id) {
        var me = this;
        //采样时间//签收时间//核收时间//审核时间//出生日期
        laydate.render({//没有默认值
            elem: '#' + id,
            eventElem: '#' + id + '+i.layui-icon',
            type: 'datetime',
            show: true,
            max: 0,
            done: function (value, date, endDate) {
                if (id == "LisTestForm_LisPatient_Birthday") {
                    me.birthdayListener(value);
                }
            }
        });
    };
    //监听新日期控件
    app.initDateListeners = function () {
        var me = this;
        //监听日期图标
        $("#sampleForm").on("click",'input.myDate+i.layui-icon', function () {
            var elemID = $(this).prev().attr("id");
            if ($("#" + elemID).hasClass("layui-disabled")) return false;
            var key = $("#" + elemID).attr("lay-key");
            if ($('#layui-laydate' + key).length > 0) {
                $("#" + elemID).attr("data-type", "date");
            } else {
                $("#" + elemID).attr("data-type", "text");
            }
            var datatype = $("#" + elemID).attr("data-type");
            if (datatype == "text") {
                if (elemID == "LisTestForm_GTestDate") {
                    me.initDate(elemID);
                } else {
                    me.initDateTime(elemID);
                }
                $("#" + elemID).attr("data-type", "date");
            } else {
                $("#" + elemID).attr("data-type", "text");
                var key = $("#" + elemID).attr("lay-key");
                $('#layui-laydate' + key).remove();
            }
        });
        //监听日期input -- 不弹出日期框
        $("#sampleForm").on('focus', '.myDate', function () {
            var device = layui.device();
            if (device.ie) {
                window.event.returnValue = false;
            } else {
                window.event.preventDefault();
            }
            layui.stope(window.event);
            return false;
        });
    };
    //初始化下拉框
    app.initSelect = function () {
        var me = this;
        //就诊类型
        me.initSickType();
        //样本类型
        me.initSampleType();
        //审核者
        me.initChecker();
        //检验者
        me.initMainTester();
        //检验类型
        me.initTestType();
        //性别
        me.initGender();
        //年龄单位
        me.initAgeUnit();
        //送检单位
        me.initHospital();
        //病区
        me.initDistrict();
        //科室
        me.initDept();
        //医生
        me.initDoctor();
        //诊断
        me.initDiag();
    };
    //初始化就诊类型下拉框
    app.initSickType = function () {
        var me = this,
            html = ['<option value="">请选择</option>'],
            url = me.url.getSickTypeUrl + '&sort=[{"property":"LBSickType_DispOrder","direction":"ASC"}]' +
                '&fields=LBSickType_Id,LBSickType_CName,LBSickType_Shortcode,LBSickType_DispOrder&where=lbsicktype.IsUse=1';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    $.each(data, function (i, item) {
                        html.push('<option value="' + item["LBSickType_Id"] + '">' + item["LBSickType_CName"] + '</option>');
                    });
                    $("#LisTestForm_LisPatient_SickTypeID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化样本类型下拉框
    app.initSampleType = function () {
        var me = this,
            html = ['<option value="">请选择</option>'],
            url = me.url.getSampleTypeUrl + '&sort=[{"property":"LBSampleType_DispOrder","direction":"ASC"}]' +
                '&fields=LBSampleType_Id,LBSampleType_CName,LBSampleType_UseCode,LBSampleType_DispOrder&where=lbsampletype.IsUse=1';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue).list;
                    $.each(data, function (i, item) {
                        html.push('<option value="' + item["LBSampleType_Id"] + '">' + item["LBSampleType_CName"] + '</option>');
                    });
                    $("#LisTestForm_GSampleTypeID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化审核者下拉框
    app.initChecker = function () {
        var me = this,
            url = me.url.getUserUrl + '&sort=[{"property":"HREmpIdentity_DispOrder","direction":"ASC"}]' +
                "&fields=HREmpIdentity_HREmployee_Id,HREmpIdentity_HREmployee_CName,HREmpIdentity_HREmployee_StandCode" +
                "&where=hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001001' and LabID='" + me.params.labid + "'";
        tableSelect.render({
            elem: '#LisTestForm_Checker',	//定义输入框input对象 必填
            checkedKey: 'HREmpIdentity_HREmployee_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'hrempidentity.HREmployee.CName,hrempIdentity.HREmployee.StandCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/编码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'HREmpIdentity_HREmployee_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'HREmpIdentity_HREmployee_CName', width: 200, title: '名称', sort: false },
                    { field: 'HREmpIdentity_HREmployee_StandCode', width: 120, title: '编码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["HREmpIdentity_HREmployee_CName"]);
                    $("#LisTestForm_CheckerID").val(record["HREmpIdentity_HREmployee_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_CheckerID").val('');
                }
            }
        });
    };
    //初始化检验者下拉框
    app.initMainTester = function () {
        var me = this,
            url = me.url.getUserUrl + '&sort=[{"property":"HREmpIdentity_DispOrder","direction":"ASC"}]' +
                "&fields=HREmpIdentity_HREmployee_Id,HREmpIdentity_HREmployee_CName,HREmpIdentity_HREmployee_StandCode" +
                "&where=hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001001' and LabID='" + me.params.labid + "'";
        tableSelect.render({
            elem: '#LisTestForm_MainTester',	//定义输入框input对象 必填
            checkedKey: 'HREmpIdentity_HREmployee_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'hrempidentity.HREmployee.CName,hrempIdentity.HREmployee.StandCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/编码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'HREmpIdentity_HREmployee_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'HREmpIdentity_HREmployee_CName', width: 200, title: '名称', sort: false },
                    { field: 'HREmpIdentity_HREmployee_StandCode', width: 120, title: '编码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["HREmpIdentity_HREmployee_CName"]);
                    $("#LisTestForm_MainTesterId").val(record["HREmpIdentity_HREmployee_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_MainTesterId").val('');
                }
            }
        });
    };
    //初始化检验类型下拉框
    app.initTestType = function () {
        var me = this,
            html = [],
            url = me.url.getEnumUrl + '?classnamespace=ZhiFang.Entity.LabStar&classname=TestType';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    $.each(data, function (i, item) {
                        html.push('<option value="' + item["Id"] + '">' + item["Name"] + '</option>');
                    });
                    $("#LisTestForm_TestType").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化性别下拉框
    app.initGender = function () {
        var me = this,
            html = ['<option value="">请选择</option>'],
            url = me.url.getEnumUrl + '?classnamespace=ZhiFang.Entity.LabStar&classname=GenderType';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    $.each(data, function (i, item) {
                        html.push('<option value="' + item["Id"] + '">' + item["Name"] + '</option>');
                    });
                    $("#LisTestForm_LisPatient_GenderID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化年龄单位下拉框
    app.initAgeUnit = function () {
        var me = this,
            html = ['<option value="">请选择</option>'],
            url = me.url.getEnumUrl + '?classnamespace=ZhiFang.Entity.LabStar&classname=AgeUnitType';
        uxutil.server.ajax({
            url: url
        }, function (res) {
            if (res.success) {
                if (res.ResultDataValue) {
                    var data = JSON.parse(res.ResultDataValue);
                    $.each(data, function (i, item) {
                        html.push('<option value="' + item["Id"] + '">' + item["Name"] + '</option>');
                    });
                    $("#LisTestForm_LisPatient_AgeUnitID").html(html.join(''));
                    form.render('select');
                }
            }
        });
    };
    //初始化送检单位下拉框
    app.initHospital = function () {
        var me = this;//暂时不用该下拉框
    };
    //初始化病区下拉框
    app.initDistrict = function () {
        var me = this,
            url = me.url.getDeptUrl + '&sort=[{"property":"HRDeptIdentity_DispOrder","direction":"ASC"}]' +
                '&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode' +
                "&where=hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001102'";
        tableSelect.render({
            elem: '#LisTestForm_LisPatient_DistrictName',	//定义输入框input对象 必填
            checkedKey: 'HRDeptIdentity_HRDept_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'hrdeptidentity.HRDept.CName,hrdeptidentity.HRDept.UseCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'HRDeptIdentity_HRDept_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'HRDeptIdentity_HRDept_CName', width: 200, title: '病区名称', sort: false },
                    { field: 'HRDeptIdentity_HRDept_UseCode', width: 120, title: '快捷码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["HRDeptIdentity_HRDept_CName"]);
                    $("#LisTestForm_LisPatient_DistrictID").val(record["HRDeptIdentity_HRDept_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_LisPatient_DistrictID").val('');
                }
            }
        });
    };
    //初始化科室下拉框
    app.initDept = function () {
        var me = this,
            url = me.url.getDeptUrl + '&sort=[{"property":"HRDeptIdentity_DispOrder","direction":"ASC"}]' +
                '&fields=HRDeptIdentity_HRDept_Id,HRDeptIdentity_HRDept_CName,HRDeptIdentity_HRDept_UseCode' +
                "&where=hrdeptidentity.IsUse=1 and hrdeptidentity.SystemCode='ZF_LAB_START' and hrdeptidentity.TSysCode='1001101'";
        tableSelect.render({
            elem: '#LisTestForm_LisPatient_DeptName',	//定义输入框input对象 必填
            checkedKey: 'HRDeptIdentity_HRDept_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'hrdeptidentity.HRDept.CName,hrdeptidentity.HRDept.UseCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'HRDeptIdentity_HRDept_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'HRDeptIdentity_HRDept_CName', width: 200, title: '病区名称', sort: false },
                    { field: 'HRDeptIdentity_HRDept_UseCode', width: 120, title: '快捷码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["HRDeptIdentity_HRDept_CName"]);
                    $("#LisTestForm_LisPatient_DeptID").val(record["HRDeptIdentity_HRDept_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_LisPatient_DeptID").val('');
                }
            }
        });
    };
    //初始化医生下拉框
    app.initDoctor = function () {
        var me = this,
            url = me.url.getUserUrl + '&sort=[{"property":"HREmpIdentity_DispOrder","direction":"ASC"}]' +
                "&fields=HREmpIdentity_HREmployee_Id,HREmpIdentity_HREmployee_CName,HREmpIdentity_HREmployee_StandCode" +
                "&where=hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001003' and LabID='" + me.params.labid + "'";
        tableSelect.render({
            elem: '#LisTestForm_LisPatient_DoctorName',	//定义输入框input对象 必填
            checkedKey: 'HREmpIdentity_HREmployee_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'hrempidentity.HREmployee.CName,hrempIdentity.HREmployee.StandCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/编码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'HREmpIdentity_HREmployee_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'HREmpIdentity_HREmployee_CName', width: 200, title: '名称', sort: false },
                    { field: 'HREmpIdentity_HREmployee_StandCode', width: 120, title: '编码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["HREmpIdentity_HREmployee_CName"]);
                    $("#LisTestForm_LisPatient_DoctorID").val(record["HREmpIdentity_HREmployee_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_LisPatient_DoctorID").val('');
                }
            }
        });
    };
    //初始化诊断下拉框
    app.initDiag = function () {
        var me = this,
            url = me.url.getDiagUrl + '&sort=[{"property":"LBDiag_DispOrder","direction":"ASC"}]' +
                '&fields=LBDiag_Id,LBDiag_CName,LBDiag_UseCode' +
                '&where=lbdiag.IsUse=1';
        tableSelect.render({
            elem: '#LisTestForm_LisPatient_DiagName',	//定义输入框input对象 必填
            checkedKey: 'LBDiag_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'lbdiag.CName,lbdiag.UseCode',	//搜索输入框的name值 默认keyword
            searchPlaceholder: '名称/快捷码',	//搜索输入框的提示文字 默认关键词搜索
            table: {	//定义表格参数，与LAYUI的TABLE模块一致，只是无需再定义表格elem
                url: url,
                height: '200',
                autoSort: false, //禁用前端自动排序
                page: true,
                limit: 50,
                limits: [50, 100, 200, 500, 1000],
                size: 'sm', //小尺寸的表格
                cols: [[
                    { type: 'radio' },
                    { type: 'numbers', title: '行号' },
                    { field: 'LBDiag_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBDiag_CName', width: 200, title: '样本类型名称', sort: false },
                    { field: 'LBDiag_UseCode', width: 120, title: '快捷码', sort: false }
                ]],
                text: { none: '暂无相关数据' },
                response: function () {
                    return {
                        statusCode: true, //成功状态码
                        statusName: 'code', //code key
                        msgName: 'msg ', //msg key
                        dataName: 'data' //data key
                    }
                },
                parseData: function (res) {//res即为原始返回的数据
                    if (!res) return;
                    var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
                    return {
                        "code": res.success ? 0 : 1, //解析接口状态
                        "msg": res.ErrorInfo, //解析提示文本
                        "count": data.count || 0, //解析数据长度
                        "data": data.list || []
                    };
                }
            },
            done: function (elem, data) {
                //选择完后的回调，包含2个返回值 elem:返回之前input对象；data:表格返回的选中的数据 []
                if (data.data.length > 0) {
                    var record = data.data[0];
                    $(elem).val(record["LBDiag_CName"]);
                    $("#LisTestForm_LisPatient_DiagID").val(record["LBDiag_Id"]);
                } else {
                    $(elem).val('');
                    $("#LisTestForm_LisPatient_DiagID").val('');
                }
            }
        });
    };
    //暴露接口
    exports('info', app);
});
