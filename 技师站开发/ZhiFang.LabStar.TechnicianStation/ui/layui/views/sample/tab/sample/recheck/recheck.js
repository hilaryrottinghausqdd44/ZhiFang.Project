/**
   @Name：复检
   @Author：GHX
   @version 2021-05-24
 */
var fireEventSaveInfo;
var PTestFromId = null;
var PTestFromSampleTypeID = null;
var PTestItemList = null;
window.fireEventSaveInfoFun = function (TestFromId,SampleTypeID,TestItemList,callback){
	PTestFromId = TestFromId;
	PTestFromSampleTypeID = SampleTypeID;
	PTestItemList = TestItemList;
	fireEventSaveInfo = callback;
};
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base'
}).use(['uxutil','uxbase', 'element', 'layer','form','laydate','table'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
		laydate = layui.laydate,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		table = layui.table,
		form = layui.form;
    var app = {};
	app.config = {
		loadindex:null
	}
	//服务地址
	app.url={
		//检验单复检+选中项目复检服务（传项目则按项目复检，不存在项目则整单复检）
		LisTestFormReCheck:uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormReCheck',
		//检验单整单取消复检服务
		LisTestFormReCheckCancel:uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestFormReCheckCancel',
		//检验单项目取消复检服务
		LisTestItemReCheckCancel: uxutil.path.ROOT +'/ServerWCF/LabStarService.svc/LS_UDTO_LisTestItemReCheckCancel'								
	};
	//get参数
	app.paramsObj = {
		//复检类型
		Type:null,
	};
    //初始化
    app.init = function () {
        var me = this;		
        me.getParams();
		me.inithtml();
        me.initListeners();
    };
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.TYPE) {
			me.paramsObj.Type = params.TYPE;
		}
	};
	app.inithtml=function(){
		var me = this;
		if(me.paramsObj.Type == 1 || me.paramsObj.Type == 3 || me.paramsObj.Type == 5){
			if ($("#recheckdiv").hasClass("layui-hide")) {
				$("#recheckdiv").removeClass("layui-hide");
			}
			if (!$("#cancelrecheckdiv").hasClass("layui-hide")) {
				$("#cancelrecheckdiv").addClass("layui-hide");
			}
			if(me.paramsObj.Type == 5){
				if ($("#TestItemValue").hasClass("layui-hide")) {
					$("#TestItemValue").removeClass("layui-hide");
				}
			}else{
				if (!$("#TestItemValue").hasClass("layui-hide")) {
					$("#TestItemValue").addClass("layui-hide");
				}
			}
		}else if(me.paramsObj.Type == 2 || me.paramsObj.Type == 4){
			if (!$("#recheckdiv").hasClass("layui-hide")) {
				$("#recheckdiv").addClass("layui-hide");
			}
			if ($("#cancelrecheckdiv").hasClass("layui-hide")) {
				$("#cancelrecheckdiv").removeClass("layui-hide");
			}
		}
	};
    //监听
    app.initListeners = function () {
        var me = this;
		$("#recheckbut").click(function () {
			var type = me.paramsObj.Type;
			switch (String(type)) {
				case "1"://选中项目复检
					me.onLisTestItemReCheck();
					break;
				case "2"://选中项目取消复检
					me.onLisTestItemUnReCheck();
					break;
				case "3"://整单复检
					me.onLisTestFormReCheck();
					break;
				case "4"://整单取消复检
					me.onLisTestFormUnReCheck();
					break;
				case "5"://当前项目复检
					me.onLisTestItemReCheck();
					break;
			}
		});
		//监听复检原因双击
		$("#recheck_textarea").off("dblclick").on("dblclick", function () {
			var testitemid = PTestItemList.length > 0 ? PTestItemList[0]["LisTestItem_Id"] : null,
				itemid = PTestItemList.length > 0 ? PTestItemList[0]["LisTestItem_LBItem_Id"] : null,
				sampletypeid = PTestFromSampleTypeID,
				elem = $(this);
			me.openPhrase($(this).val(), "XMFJYY", "项目复检原因", itemid, sampletypeid, function (val) {
				elem.val(val);
			});
		});
	};

	//选中项目复检+当前项目复检
	app.onLisTestItemReCheck = function () {
		var me = this,
			url = me.url.LisTestFormReCheck,
			type = me.paramsObj.Type,
			comment = $("#recheck_textarea").val(),
			reportvalue = $("#recheck_reportvalue").val();

		if (!PTestFromId) {
			uxbase.MSG.onWarn("检验单ID为空!");
			return;
		}
		if (PTestItemList.length == 0) {
			uxbase.MSG.onWarn("不存在待复检的检验项目!");
			return;
		}
		var itemlist = [];
		for (var i = 0; i < PTestItemList.length; i++) {
			var testitem = {};
			testitem.Id = PTestItemList[i]["LisTestItem_Id"];
			testitem.ReportValue = PTestItemList[i]["LisTestItem_ReportValue"];
			testitem.RedoDesc = PTestItemList[i]["LisTestItem_RedoDesc"];
			//存在复检备注 则赋值 不存在则保留之前的
			if (comment) testitem.RedoDesc = comment;
			//当前项目复检可以填写报告值
			if (type == 5 && reportvalue) testitem.ReportValue = reportvalue;
			//拼接
			itemlist.push(testitem);
		}

		var configs = {
			url: url,
			type: 'post',
			data: JSON.stringify({ testFormID: PTestFromId, listReCheckTestItem: itemlist, memoInfo: comment })
		}
		me.onServerHandle(configs);
	};
	//选中项目取消复检
	app.onLisTestItemUnReCheck = function () {
		var me = this,
			url = me.url.LisTestItemReCheckCancel,
			isClearRedoValues = $("#clearReportValue").prop("checked"),
			isClearRedoDesc = $("#clearRecheckInfo").prop("checked");
		if (!PTestFromId) {
			uxbase.MSG.onWarn("检验单ID为空!");
			return;
		}
		if (PTestItemList.length == 0) {
			uxbase.MSG.onWarn("不存在待复检的检验项目!");
			return;
		}
		var idlist = [];
		for (var i = 0; i < PTestItemList.length; i++) {
			if (PTestItemList[i]["LisTestItem_RedoStatus"] == 1) idlist.push(PTestItemList[i].LisTestItem_Id);
		}
		var configs = {
			url: url,
			type: 'post',
			data: JSON.stringify({ testFormID: PTestFromId, testItemIDList: idlist.join(","), isClearRedoDesc: isClearRedoDesc, isClearRedoValues: isClearRedoValues })
		}
		me.onServerHandle(configs);
	};
	//整单复检
	app.onLisTestFormReCheck = function () {
		var me = this,
			url = me.url.LisTestFormReCheck,
			comment = $("#recheck_textarea").val();
		if (!PTestFromId) {
			uxbase.MSG.onWarn("检验单ID为空!");
			return;
		}
		var configs = {
			url: url,
			type: 'post',
			data: JSON.stringify({ testFormID: PTestFromId, memoInfo: comment })
		}
		me.onServerHandle(configs);
	};
	//整单取消复检
	app.onLisTestFormUnReCheck = function () {
		var me = this,
			url = me.url.LisTestFormReCheckCancel,
			isClearRedoValues = $("#clearReportValue").prop("checked"),
			isClearRedoDesc = $("#clearRecheckInfo").prop("checked");
		if (!PTestFromId) {
			uxbase.MSG.onWarn("检验单ID为空!");
			return;
		}
		var configs = {
			url: url,
			type: 'post',
			data: JSON.stringify({ testFormIDList: PTestFromId, isClearRedoDesc: isClearRedoDesc, isClearRedoValues: isClearRedoValues })
		}
		me.onServerHandle(configs);
	};
	//服务处理
	app.onServerHandle = function (configs) {
		var me = this,
			load = layer.load();
		uxutil.server.ajax(configs, function (res) {
			layer.close(load);
			if (res.success) {
				fireEventSaveInfo();
			} else {
				uxbase.MSG.onError(res.msg);
			}
		});
	};
	//弹出短语选择
	app.openPhrase = function (value, TypeCode, TypeName, ItemID, SampleTypeID, callback) {
		var me = this,
			//sectionID = me.config.defaultParams.sectionID || null,
			ItemID = ItemID || null,
			value = value || "",
			//短语表配置
			TypeCode = TypeCode || null,
			TypeName = TypeName || null,
			ObjectType = 2,//针对类型1：小组样本 2：检验项目
			ObjectID = ItemID,
			PhraseType = "ItemPhrase",//枚举
			SampleTypeID = SampleTypeID || null;//样本类型
		if (!ItemID) {
			uxbase.MSG.onWarn("项目不能为空!");
			return;
		}
		if (!TypeCode) {
			uxbase.MSG.onWarn("缺少TypeCode参数!");
			return;
		}
		if (!TypeName) {
			uxbase.MSG.onWarn("缺少TypeName参数!");
			return;
		}
		parent.layer.open({
			type: 2,
			area: ['600px', '420px'],
			fixed: false,
			maxmin: true,
			title: TypeName,
			content: uxutil.path.ROOT + '/ui/layui/views/sample/basic/phrase/new/index.html?CName=' + TypeName + '&ObjectType=' + ObjectType + '&ObjectID=' + ObjectID + '&PhraseType=' + PhraseType + '&TypeName=' + TypeName + '&TypeCode=' + TypeCode + '&SampleTypeID=' + SampleTypeID + '&isAppendValue=1&ISNEXTLINEADD=1',
			success: function (layero, index) {
				var body = parent.layer.getChildFrame('body', index);//这里是获取打开的窗口元素
				body.find('#Comment').val(value);
				var iframeWin = parent.window[layero.find('iframe')[0]['name']]; //得到iframe页的窗口对象，执行iframe页的方法：iframeWin.method();
				iframeWin.externalCallFun(function (v) { callback(v); });
			}
		});
	};
    //初始化
    app.init();
});