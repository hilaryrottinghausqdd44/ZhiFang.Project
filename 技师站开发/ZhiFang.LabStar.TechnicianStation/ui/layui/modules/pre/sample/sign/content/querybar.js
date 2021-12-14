/**
 * @name：modules/pre/sample/sign/content/querybar 签收列表
 * @author：zhangda
 * @version 2021-07-28
 */
layui.extend({
    uxutil: 'ux/util',
    tableSelect: '../src/tableSelect/tableSelect',
    CommonSelectUser: 'modules/common/select/preuser',
    CommonSelectDept: 'modules/common/select/dept',
    CommonSelectEnum: 'modules/common/select/enum',
    CommonSelectSickType: 'modules/common/select/sicktype',
    CommonSelectSampleType: 'modules/common/select/sampletype',
    CommonSelectSection: 'modules/common/select/section',
}).define(['uxutil','tableSelect', 'laydate', 'form', 'PreSampleSignParams', 'CommonSelectUser', 'CommonSelectDept', 'CommonSelectEnum', 'CommonSelectSickType', 'CommonSelectSampleType', 'CommonSelectSection'],function(exports){
	"use strict";
	
	var $ = layui.$,
		laydate = layui.laydate,
		form = layui.form,
        uxutil = layui.uxutil,
        tableSelect = layui.tableSelect,
        PreSampleSignParams = layui.PreSampleSignParams,
        CommonSelectUser = layui.CommonSelectUser,
        CommonSelectDept = layui.CommonSelectDept,
        CommonSelectEnum = layui.CommonSelectEnum,
        CommonSelectSickType = layui.CommonSelectSickType,
        CommonSelectSampleType = layui.CommonSelectSampleType,
        CommonSelectSection = layui.CommonSelectSection,
		MOD_NAME = 'querybar';
    //获取检验项目
    var GET_TESTITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?isPlanish=true&sort=[{property:"LBItem_DispOrder",direction:"ASC"}]';
    //根据小组获取小组项目
    var GET_SECTIONITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetAllItemIdBySectionID';
    //根据专业大组获得对应项目  专业大组-小组-小组项目
    var GET_SUPGROUPITEM_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetAllItemIdBySuperSectionID';
	//内部列表+表头dom
    var QUERYBAR_DOM = [
        {
            code: 'SampleStatus',
            isUse: true,
            width: '100px',
            text:'标本状态',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-SampleStatus" id="{domId}-SampleStatus" lay-filter="{domId}-SampleStatus" lay-search=""><option value="">{text}</option></select> ' +
                '</div >'
        }, {
            code: 'DateType',
            isUse: true,
            width: '120px',
            text: '时间类型',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-DateType" id="{domId}-DateType" lay-filter="{domId}-DateType" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'Date',
            isUse: true,
            width: '300px',
            text: '-',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<input type="text" id="{domId}-Date" name="{domId}-Date" placeholder="{text}" autocomplete="off" class="layui-input" />' +
                '</div >'
        }, {
            code: 'BarCode',
            isUse: true,
            width: '100px',
            text: '条码号',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<input type="text" id="{domId}-BarCode" name="{domId}-BarCode" placeholder="{text}" autocomplete="off" class="layui-input" />' +
                '</div >'
        }, {
            code: 'PatNo',
            isUse: true,
            width: '100px',
            text: '病历号',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<input type="text" id="{domId}-PatNo" name="{domId}-PatNo" placeholder="{text}" autocomplete="off" class="layui-input" />' +
                '</div >'
        }, {
            code: 'CName',
            isUse: true,
            width: '100px',
            text: '姓名',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<input type="text" id="{domId}-CName" name="{domId}-CName" placeholder="{text}" autocomplete="off" class="layui-input" />' +
                '</div >'
        }, {
            code: 'SignPerson',
            isUse: true,
            width: '100px',
            text: '签收人',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-SignPerson" id="{domId}-SignPerson" lay-filter="{domId}-SignPerson" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'SampleType',
            isUse: true,
            width: '100px',
            text: '样本类型',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-SampleType" id="{domId}-SampleType" lay-filter="{domId}-SampleType" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'SickType',
            isUse: true,
            width: '100px',
            text: '就诊类型',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-SickType" id="{domId}-SickType" lay-filter="{domId}-SickType" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'Dept',
            isUse: true,
            width: '100px',
            text: '开单科室',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-Dept" id="{domId}-Dept" lay-filter="{domId}-Dept" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'Section',
            isUse: true,
            width: '100px',
            text: '检验小组',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-Section" id="{domId}-Section" lay-filter="{domId}-Section" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'SupGroup',
            isUse: true,
            width: '100px',
            text: '专业大组',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<select name="{domId}-SupGroup" id="{domId}-SupGroup" lay-filter="{domId}-SupGroup" lay-search=""><option value="">{text}</option></select>' +
                '</div >'
        }, {
            code: 'TestItemID',
            isUse: true,
            width: '160px',
            text: '检验项目',
            elem: '<div class="layui-inline my-inline" style="width:{width};">' +
                '<input type="hidden" id="{domId}-TestItemID" name="{domId}-TestItemID" placeholder="{text}" readOnly autocomplete="off" class="layui-input" />' +
                '<input type="text" id="{domId}-TestItem" name="{domId}-TestItem" placeholder="{text}" readOnly autocomplete="off" class="layui-input" />' +
                '</div >'
        },
    ];
    //小组对应项目
    var SectionItemsMap = {};
    //专业大组对应项目
    var SuperSectionItemsMap = {};

	//医嘱单列表
	var querybar = {
		//对外参数
		config:{
			domId:null,
            height: null,
            PreSampleSignParamsInstance: null//功能参数实例
			//hisDeptNo:null,//HIS科室编码
			//patno:null,//病历号
			//sickTypeNo:null//就诊类型
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({}, me.config, querybar.config,setings);
		
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
        var me = this,
            PreSampleSignParamsInstance = me.config.PreSampleSignParamsInstance;
        var html = me.initQueryBar();
        $('#' + me.config.domId).html(html);
        //查询天数
        var searchdays = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0052');
        var today = uxutil.date.toString(new Date(), true);
        //时间类型内容
        var DateTypeContent = PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0062');
        console.log(DateTypeContent);
        if (DateTypeContent) {
            var arr = DateTypeContent.split(','),
                html = ["<option value=''>时间类型</option>"];
            $.each(arr, function (i, item) {
                html.push("<option value='" + item.split('-')[0] + "'" + (item.split('-')[1].indexOf("条码生成") != -1 ? "selected" : "") + ">" + item.split('-')[1] + "</option>");
            });
            $("#" + me.config.domId + '-DateType').html(html.join(""));
        }
		//日期时间选择器
		laydate.render({
            elem: '#' + me.config.domId+'-Date',
            type: 'datetime',
            range:true,
            value: (searchdays && searchdays > 0) ? uxutil.date.toString(uxutil.date.getNextDate(today, -searchdays), true) + " 00:00:00" + " - " + today + " 23:59:59" : today + " 00:00:00" + " - " + today + " 23:59:59"
        });
        //标本状态
        uxutil.server.ajax({
            url: uxutil.path.ROOT + "/ServerWCF/CommonService.svc/GetClassDic",
            data: { classname: "Pre_OrderSignFor_SapmpleStatus" }//, classnamespace:"PreParaEnumTypeEntity"
        }, function (data) {
            var htmls = ["<option value=''>标本状态</option>"];
            if (data.success) {
                var list = data.value || [];
                for (var i = 0; i < list.length; i++) {
                    htmls.push('<option value="' + list[i].Memo + '">' + list[i].Name + '</option>');
                }
            } else {
                layer.msg(data.msg);
            }
            $("#" + me.config.domId + '-SampleStatus').html(htmls.join(""));
        });

        //CommonSelectEnum.render({ domId: me.config.domId + '-SampleStatus', className: 'Pre_OrderSignFor_SapmpleStatus', defaultName:'标本状态',done: function () {}});
        //签收人
        CommonSelectUser.render({ domId: me.config.domId + "-SignPerson", defaultName: '签收人', code: [1001001], done: function () { } });
        //就诊类型
        CommonSelectSickType.render({ domId: me.config.domId + '-SickType', defaultName: '就诊类型', done: function () { } });
        //样本类型
        CommonSelectSampleType.render({
            domId: me.config.domId + '-SampleType', defaultName: '样本类型', done: function () {
                if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0056'))
                    $("#" + me.config.domId + "-Dept").val(PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0056'));
            }
        });
        //开单科室
        CommonSelectDept.render({
            domId: me.config.domId + "-Dept", code: '1001101', defaultName: '开单科室', done: function () {
                if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0053'))
                    $("#" + me.config.domId + "-Dept").val(PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0053'));
            }
        });
        //检验小组
        CommonSelectSection.render({
            domId: me.config.domId + "-Section", defaultName: '检验小组', done: function () {
                if (PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0055'))
                    $("#" + me.config.domId + "-Dept").val(PreSampleSignParamsInstance.get('Pre_OrderSignFor_DefaultPara_0055'));
            }
        });
        //专业大组
        CommonSelectDept.render({ domId: me.config.domId + "-SupGroup", code: '1001107', defaultName: '专业大组', done: function () { } });
        //检验项目
        me.initTestItem();

        form.render();
	};
	//监听事件
	Class.prototype.initListeners = function(){
        var me = this;
        form.on('select(' + me.config.domId + '-Section)', function (data) {
            var value = data.value; //得到被选中的值
            if (SectionItemsMap[value]) return;
            me.initSectionItem(value, function (itemids) {
                SectionItemsMap[value] = itemids;
            });
        });
        form.on('select(' + me.config.domId + '-SupGroup)', function (data) {
            var value = data.value; //得到被选中的值
            if (SuperSectionItemsMap[value]) return;
            me.initSuperGroupItem(value, function (itemids) {
                SuperSectionItemsMap[value] = itemids;
            });
        });
    };
    //获得条件
    Class.prototype.getWhere = function () {
        var me = this,
            where = [],
            relationForm=[],
            SampleStatus = $("#" + me.config.domId + "-SampleStatus").val(),//标本状态
            TimeType = $("#" + me.config.domId + "-DateType").val(),//时间类型
            TimeTypeValue = $("#" + me.config.domId + "-Date").val(),//时间
            BarCode = $("#" + me.config.domId + "-BarCode").val(),//条码号
            PatNo = $("#" + me.config.domId + "-PatNo").val(),//病历号
            CName = $("#" + me.config.domId + "-CName").val(),//姓名
            SignForManID = $("#" + me.config.domId + "-SignPerson").val(),//签收人
            SampleTypeID = $("#" + me.config.domId + "-SampleType").val(),//样本类型
            SickTypeID = $("#" + me.config.domId + "-SickType").val(),//就诊类型
            DeptID = $("#" + me.config.domId + "-Dept").val(),//开单科室
            SectionID = $("#" + me.config.domId + "-Section").val(),//检验小组
            ProfessionalLargeGroupID = $("#" + me.config.domId + "-SupGroup").val(),//专业大组
            TestItemID = $("#" + me.config.domId + "-TestItemID").val();//检验项目

        if (SampleStatus) where.push(SampleStatus);
        if (TimeType && TimeTypeValue) where.push(TimeType + ">='" + TimeTypeValue.split(' - ')[0] + "' and " + TimeType + "<='" + TimeTypeValue.split(' - ')[1] + "'");
        if (SampleTypeID) where.push("lisbarcodeform.SampleTypeID='" + SampleTypeID+"'");
        if (DeptID) where.push("lisbarcodeform.LisPatient.DeptID='" + DeptID + "'");
        if (BarCode) where.push("lisbarcodeform.BarCode='" + BarCode + "'");
        if (PatNo) where.push("lisbarcodeform.LisPatient.PatNo='" + PatNo+"'");
        if (SickTypeID) where.push("lisbarcodeform.LisPatient.SickTypeID='" + SickTypeID + "'");
        if (SignForManID) {
            where.push("lisbarcodeform.Id=lisoperate.OperateObjectID  and lisoperate.OperateUserID='" + SignForManID + "' and lisoperate.OperateTypeID=100015");
            relationForm.push("LisOperate lisoperate");
        }
        if (SectionID && SectionItemsMap[SectionID]) {
            where.push("lisbarcodeform.Id=lisbarcodeitem.LisBarCodeForm.Id and lisbarcodeitem.BarCodesItemID in (" + SectionItemsMap[SectionID] + ")");
            if (relationForm.indexOf("LisBarCodeItem lisbarcodeitem") == -1) relationForm.push("LisBarCodeItem lisbarcodeitem");
        }
        if (ProfessionalLargeGroupID && SuperSectionItemsMap[ProfessionalLargeGroupID]) {
            where.push("lisbarcodeform.Id=lisbarcodeitem.LisBarCodeForm.Id and lisbarcodeitem.BarCodesItemID in (" + SuperSectionItemsMap[ProfessionalLargeGroupID] + ")");
            if (relationForm.indexOf("LisBarCodeItem lisbarcodeitem") == -1) relationForm.push("LisBarCodeItem lisbarcodeitem");
        }
        if (TestItemID) {
            where.push("lisbarcodeform.Id=lisbarcodeitem.LisBarCodeForm.Id and lisbarcodeitem.BarCodesItemID=" + TestItemID);
            if (relationForm.indexOf("LisBarCodeItem lisbarcodeitem") == -1) relationForm.push("LisBarCodeItem lisbarcodeitem");
        }

        return { where: where.join(' and '), relationForm: relationForm.join() };
    };
    //初始化查询栏
    Class.prototype.initQueryBar = function () {
        var me = this,
            html = [];

        $.each(QUERYBAR_DOM, function (i, item) {
            if (item["isUse"]) html.push(item["elem"].replace(/{domId}/g, me.config.domId).replace(/{width}/g, item["width"]).replace(/{text}/g, item["text"]));
        });

        html.push('<style>'+
            '.my-inline{margin-right:5px;}'+
            '</style>');

        return html.join('');
    };
    //初始化检验项目
    Class.prototype.initTestItem = function () {
        var me = this,
            url = GET_TESTITEM_LIST_URL +
                '&fields=LBItem_Id,LBItem_CName,LBItem_Shortcode' +
                "&where=IsUse=1";
        tableSelect.render({
            elem: '#' + me.config.domId +"-TestItem",	//定义输入框input对象 必填
            checkedKey: 'LBItem_Id', //表格的唯一建值，非常重要，影响到选中状态 必填
            searchKey: 'LBItem.CName,LBItem.Shortcode',	//搜索输入框的name值 默认keyword
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
                    { field: 'LBItem_Id', width: 150, title: '主键ID', sort: false, hide: true },
                    { field: 'LBItem_CName', width: 200, title: '项目名称', sort: false },
                    { field: 'LBItem_Shortcode', width: 120, title: '快捷码', sort: false }
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
                    $(elem).val(record["LBItem_CName"]);
                    $("#" + me.config.domId + "-TestItemID").val(record["LBItem_Id"]);
                } else {
                    $(elem).val('');
                    $("#" + me.config.domId + "-TestItemID").val('');
                }
            }
        });
    };
    //根据小组查询该小组下项目
    Class.prototype.initSectionItem = function (sectionid,callback) {
        var me = this,
            url = GET_SECTIONITEM_LIST_URL;
        if (!sectionid) callback && callback('');
        uxutil.server.ajax({
            url: url,
            type:'POST',
            data: JSON.stringify({ sectionID: sectionid })
        }, function (data) {
            var htmls = '';
            if (data.success) {
                if (data.ResultDataValue != "") htmls = data.ResultDataValue;
            } else {
                layer.msg(data.msg);
            }
            callback && callback(htmls);
        });
    };
    //根据专业大组查询该小组下项目
    Class.prototype.initSuperGroupItem = function (supergroupid, callback) {
        var me = this,
            url = GET_SUPGROUPITEM_LIST_URL;
        if (!supergroupid) callback && callback('');
        uxutil.server.ajax({
            url: url,
            type: 'POST',
            data: JSON.stringify({ superSectionID: supergroupid })
        }, function (data) {
            var htmls = '';
            if (data.success) {
                if (data.ResultDataValue != "") htmls = data.ResultDataValue;
            } else {
                layer.msg(data.msg);
            }
            callback && callback(htmls);
        });
    };
	//核心入口
	querybar.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		
		//监听事件
		me.initListeners();
		
		return me;
	};
	
	//暴露接口
	exports(MOD_NAME, querybar);
});