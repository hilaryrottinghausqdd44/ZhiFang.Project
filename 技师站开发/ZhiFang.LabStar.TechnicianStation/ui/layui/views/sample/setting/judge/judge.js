/**
   @Name：智能审核
   @Author：GHX
   @version 2021-05-19
   @Name：修改
   @Author：zhangda
   @version 2021-07-06
 */
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
		loadindex:null,
		tabindex: 0,
		isLoadTabArr:[],
		TypeCode:"",
		TabData:{},
		ParaFilds:[
				'ParaNo','CName','TypeCode','ParaType','ParaDesc','ParaEditInfo','SystemCode',
				'ShortCode','BVisible','BVisible','IsUse','ParaValue','Id','DispOrder'],
		defaultOrderBy:[{property:"BPara_DispOrder",direction:"ASC"}],
		icol: ["&#xe6ac;", "&#xe6a1;", "&#xe85e;", "&#xe61d;", "&#xe622;", "&#xe65b;"],
		//智能审核显示页签
		judgeTabCode:'NTestType_SysJudge_OrderInfo_Para,NTestType_SysJudge_TestDate_Para,NTestType_SysJudge_TestResult_Para,NTestType_SysJudge_OtherInfo_Para'
	}
	//服务地址
	app.url={
		//保存当前页面设置(默认设置)
		saveDefaultUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemDefaultPara',
		//保存当前页面设置(个性化设置 -- 小组)
		savePersonalParaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SaveSystemParaItem',
		//清除(个性化设置 -- 小组)
		delPersonalParaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DeleteSystemParaItem',
		//获取常规检验参数分类列表
		GET_ENUMURL: uxutil.path.ROOT +'/ServerWCF/CommonService.svc/GetClassDic?classnamespace=ZhiFang.Entity.LabStar&classname=Para_NTestType',
		//获取常规检验参数分类列表. --个性设置->默认设置->出厂设置
		getListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaValueByParaTypeCode?isPlanish=true',
		//获取默认设置.
		getFactoryParaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?isPlanish=true',
		//查询个性参数
		getPersonalParaUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?isPlanish=true'
												
	};
	//get参数
	app.paramsObj = {
		sectionId: null, //小组ID
		sectionCName: null //小组名称
	};
    //初始化
    app.init = function () {
        var me = this;		
        me.getParams();
        me.initListeners();
		me.loadEnumData();
    };
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.SECTIONID) {
			me.paramsObj.sectionId = params.SECTIONID;
			me.paramsObj.sectionCName = params.SECTIONCNAME;
		}
	};
    //监听
    app.initListeners = function () {
        var me = this;
		$("#initializebut").click(function () {
			window.event.preventDefault();
			me.onFactoryClick();
		});
		//保存为默认设置
		$("#judgesavebut").click(function () {
			window.event.preventDefault();
			me.onSaveClick(me.url.saveDefaultUrl);
		});
		//保存为小组设置
		$("#judgesavesectionbut").click(function () {
			window.event.preventDefault();
			var objectInfo = [{ ObjectID: me.paramsObj.sectionId, ObjectName: me.paramsObj.sectionCName }];
			me.onSaveClick(me.url.savePersonalParaUrl, objectInfo);
		});
		//清除小组设置
		$("#judgedelsectionbut").click(function () {
			window.event.preventDefault();
			me.onDelPersonalPara();
		});
		//监听页签切换
		element.on('tab(judge_Tab)', function (data) {
			var index = data.index,
				layid = $("#judge_Tab .layui-tab-title>li:eq("+index+")").attr("lay-id");
			me.config.tabindex = index;
			me.config.TypeCode = layid;
			var isLoad = false, source = 0;
			$.each(me.config.isLoadTabArr, function (i, item) {
				if (item["index"] == index) {
					isLoad = true;
					source = item["source"];
				}
			});
			if (!isLoad) me.onTabJudeg(index, layid);
			//判断删除小组设置按钮是否需要
			if (isLoad || index == 0) {
				if (source == '2' && index != 0) {
					if ($("#judgedelsectionbut").hasClass("layui-hide")) $("#judgedelsectionbut").removeClass("layui-hide");
				} else {
					if (!$("#judgedelsectionbut").hasClass("layui-hide")) $("#judgedelsectionbut").addClass("layui-hide");
				}
			}
		});
    };
	//加载智能审核设置枚举
	app.loadEnumData=function(){
		var me = this,
			url =  me.url.GET_ENUMURL;
		uxutil.server.ajax({url: url},function(res){
			if (res.success) {
				var list = res.value || [],
					arr = [];
				$.each(list, function (i, item) {
					if (me.config.judgeTabCode.indexOf(item["Code"]) != -1) arr.push(item);
				});
				me.createItems(arr);
			}else{
				$("#judge_Tab").html('<div style="padding:50px 10px;text-align:center;">' + res.msg + '</div>');
			}
		})
	};
	//创建内部页签
	app.createItems= function(list) {
		var me = this,
			len = list.length,
			titlearr = $("#judge_tab_title").html(),
			conentarr = $("#judge_tab_content").html(),
			firstlayid = null;
		
		for (var i = 0; i < len; i++){
			if (i == 0) firstlayid = list[i].Code;
			titlearr += '<li lay-id="'+list[i].Code+'"><i class="iconfont">'+me.config.icol[i]+'</i>&nbsp;'+list[i].Name+'</li>'; 
			conentarr += '<div class="layui-tab-item"></div>';
		}
		$("#judge_tab_title").html(titlearr);
		$("#judge_tab_content").html(conentarr);
		element.init();
		if (firstlayid) element.tabChange('judge_Tab', firstlayid);
	};
	//页签切换事件
	app.onTabJudeg = function (index, paraTypeCode) {
		var me = this,
			index = index || 0,
			paraTypeCode = paraTypeCode || null;
		if (!paraTypeCode) return;

		var loadindex = layer.load();
		me.getPara(paraTypeCode, function (res) {
			layer.close(loadindex);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {//存在个性化设置
					me.config.TabData[paraTypeCode] = res.value.list;//记录
					var html = [], source = 0;
					$.each(res.value.list, function (i, item) {
						if (i == 0) {
							source = item["BPara_ParaSource"];
							//判断是否是个性化设置 ParaSource属性：0出厂参数，1默认参数，2个性参数
							if (source == '2') {
								if ($("#judgedelsectionbut").hasClass("layui-hide")) $("#judgedelsectionbut").removeClass("layui-hide");
							} else {
								if (!$("#judgedelsectionbut").hasClass("layui-hide")) $("#judgedelsectionbut").addClass("layui-hide");
							}
						}
						html.push('<div class="layui-block"><input type="checkbox" name="' + item["BPara_ParaNo"] + '" id="' + item["BPara_Id"] + '" lay-skin="primary" title="' + item["BPara_CName"] + '" ' + (item["BPara_ParaValue"] == 1 ? "checked" : "") + '></div>');
					});
					$("#judge_tab_content .layui-tab-item:eq(" + index + ")").html('<div class="layui-form"><div class="layui-form-item">'
						+ html.join("") + '</div></div>');
					form.render();
					me.config.isLoadTabArr.push({ index: index, source: source });
				} else {
					uxbase.MSG.onWarn("未找到配置参数!");
				}
			} else {
				uxbase.MSG.onError(res.msg);
			}
		});

	};
	//当前页签采用出厂设置
	app.onFactoryClick=function(){
		var me = this;
		me.getFactoryPara(me.config.TypeCode, function (res) {
			if (res.success) {
				var list = (res.value || {}).list || [];
				for (var i = 0; i < list.length; i++) {
					if (list[i].BPara_ParaValue == "0") {
						$("input[name='" + list[i].BPara_ParaNo + "']").prop("checked", false);
					} else {
						$("input[name='" + list[i].BPara_ParaNo + "']").prop("checked", true);
					}
				}
				form.render("checkbox");
			} else {
				uxbase.MSG.onError(res.msg);
			}
		});
	};
	//保存设置
	app.onSaveClick = function (url, objectInfo){
		var me = this,
			url = url || '',
			isPersonal = objectInfo ? true : false,
			params = {};
		params.entityList = me.getEntityList(isPersonal);
		if (isPersonal) params.objectInfo = JSON.stringify(objectInfo);

		if (!url) return;
		var loadindex = layer.load();
		//保存到后台
		uxutil.server.ajax({
		    url: url,
		    type: 'post',
			data: JSON.stringify(params)
		}, function (data) {
			layer.close(loadindex);
			if(data.success){
				uxbase.MSG.onSuccess("保存成功!");
				if (objectInfo)
					me.onTabJudeg(me.config.tabindex, me.config.TypeCode);
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	};
	//保存设置封装
	app.getEntityList = function (isPersonal){
		var me = this,
			isPersonal = isPersonal || false,//是否是个性化设置保存
			entityList=[];
		var comtab = me.config.TabData[me.config.TypeCode];
		for(var i=0;i<comtab.length;i++){
			var check = $("input[name='" + comtab[i].BPara_ParaNo + "']").prop("checked");
			if (isPersonal) {
				entityList.push({
					BPara: { Id: comtab[i].BPara_Id, DataTimeStamp: [0, 0, 0, 0, 0, 0, 0, 0] },
					IsUse: true,
					ParaNo: comtab[i].BPara_ParaNo,
					ParaValue: check? "1": "0",
					OperatorID: uxutil.cookie.get(uxutil.cookie.map.USERID),
					Operator: uxutil.cookie.get(uxutil.cookie.map.USERNAME)
				});
			} else {
				entityList.push({
					Id: comtab[i].BPara_Id,
					ParaValue: check ? "1" : "0",
					OperatorID: uxutil.cookie.get(uxutil.cookie.map.USERID),
					Operator: uxutil.cookie.get(uxutil.cookie.map.USERNAME),
					BVisible: 1,
					CName: comtab[i].BPara_CName,
					IsUse: 1,
					ParaEditInfo: comtab[i].BPara_ParaEditInfo,
					ParaNo: comtab[i].BPara_ParaNo,
					ParaType: comtab[i].BPara_ParaType,
					ShortCode: comtab[i].BPara_ShortCode,
					SystemCode: comtab[i].BPara_SystemCode,
					TypeCode: comtab[i].BPara_TypeCode
				});
			}
		}
		return entityList;
	};
	//清除小组设置
	app.onDelPersonalPara = function () {
		var me = this,
			url = me.url.delPersonalParaUrl || '',
			objectInfo = [{ ObjectID: me.paramsObj.sectionId, ObjectName: me.paramsObj.sectionCName }];;
		if (!url) return;
		var loadindex = layer.load();
		//保存到后台
		uxutil.server.ajax({
			url: url,
			type: 'post',
			data: JSON.stringify({ objectInfo: JSON.stringify(objectInfo) })
		}, function (data) {
			layer.close(loadindex);
			if (data.success) {
				uxbase.MSG.onSuccess("保存成功!");
				me.onTabJudeg(me.config.tabindex, me.config.TypeCode);
			} else {
				uxbase.MSG.onError(data.msg);
			}
		});
	};
	//查询参数 个性化-》默认-》出厂
	app.getPara = function (paraTypeCode,callBack) {
		var me = this,
			paraTypeCode = paraTypeCode || null,
			url = me.url.getListUrl + "&paraTypeCode=" + paraTypeCode + '&objectID=' + me.paramsObj.sectionId;
		if (!paraTypeCode) return;
		url += '&fields=BPara_ParaSource,BPara_' + me.config.ParaFilds.join(',BPara_');
		uxutil.server.ajax({ url: url }, function (res) {
			if (typeof callBack == "function") callBack(res);
		})
	};
	//查询出厂参数
	app.getFactoryPara = function (paraTypeCode, callBack) {
		var me = this,
			paraTypeCode = paraTypeCode || null,
			url = me.url.getFactoryParaUrl + '&paraTypeCode=' + paraTypeCode;
		if (!paraTypeCode) return;
		url += '&fields=BPara_' + me.config.ParaFilds.join(',BPara_');
		url += '&sort=' + JSON.stringify(me.config.defaultOrderBy);
		uxutil.server.ajax({ url: url }, function (res) {
			if (typeof callBack == "function") callBack(res);
		});
	};
    //初始化
    app.init();
});