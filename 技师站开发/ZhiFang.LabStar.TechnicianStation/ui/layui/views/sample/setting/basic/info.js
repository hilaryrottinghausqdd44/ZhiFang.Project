/**
   @Name：初审人 审定人 设置
   @Author：GHX
   @version 2021-05-10
 */
layui.extend({
	uxutil: 'ux/util',
	uxbase: 'ux/base'
}).use(['uxutil','uxbase', 'element', 'layer','form','laydate'], function () {
    "use strict";
    var $ = layui.$,
        element = layui.element,
        layer = layui.layer,
		laydate = layui.laydate,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		form = layui.form;
	var app = {};
	//服务地址
	app.url = {
		//获取常规检验参数分类列表. --个性设置->默认设置->出厂设置
		getListUrl: uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryParaValueByParaTypeCode?isPlanish=true',
	};
	//get参数
	app.paramsObj = {
		//检验确认人弹出Handler,审核人弹出Checker,保存到内存时用
		OperateType:'Handler',
		//授权操作类型枚举,检验确认人的Name='检验确认'
		OperateTypeText:'检验确认',
		//授权操作类型枚举，检验确认人的Id='2'
		OperateTypeID:'2',
		sectionId: 1, //小组ID
	};
	app.ParaFilds= [
		'ParaNo', 'CName', 'TypeCode', 'ParaType', 'ParaDesc', 'ParaEditInfo', 'SystemCode',
		'ShortCode', 'BVisible', 'BVisible', 'IsUse', 'ParaValue', 'Id', 'DispOrder'],
	app.fireEventParams={
		AuthorizeUserID:'',//授权人ID
		AuthorizeUser:'',//授权人姓名
		BeginTime:'',//开始时间
		EndTime:''//结束时间
	};
	app.OPERATER_TYPE_LIST=[
    	{"value":"0","text":"登录者本人"},
    	{"value":"1","text":"预授权"}
	];
	app.Para = [];//审定人参数
    //初始化
    app.init = function () {
        var me = this;		
        me.getParams();
        me.initListeners();
		me.initOperateUserInfo();
    };
	//获得参数
	app.getParams = function() {
		var me = this;
		var params = uxutil.params.get(true);
		if (params.SECTIONID) {
			me.paramsObj.sectionId = params.SECTIONID;
		}
		if (params.OPERATETYPE) {
			me.paramsObj.OperateType = params.OPERATETYPE;
		}
		if (params.OPERATETYPETEXT) {
			me.paramsObj.OperateTypeText = params.OPERATETYPETEXT;
		}
		if (params.OPERATETYPEID) {
			me.paramsObj.OperateTypeID = params.OPERATETYPEID;
		}
	};
    //监听
    app.initListeners = function () {
        var me = this;
		form.on('radio(OperaterType)', function(data){
			if(data.value == 0){
				me.hideOrShowDateTime(false);
			}else if(data.value == 1){
				me.hideOrShowDateTime(true);				
				me.openauthorize(false);
			}
		}); 
		$("#authorizebutton").click(function(){			
			me.openauthorize(true);
		});
		$("#authorizeconfirmbut").click(function(){
			me.onConfirmClick();
		});
    };  
	app.openauthorize = function(tabradio){//弹出预授权界面
		var me = this;
		var url = uxutil.path.ROOT + '/ui/layui/views/sample/setting/basic/authorize/authorize.html';
		url += '?SectionID='+me.paramsObj.sectionId+'&OperateType='+me.paramsObj.OperateType+'&OperateTypeText='
				+me.paramsObj.OperateTypeText+"&OperateTypeID="+me.paramsObj.OperateTypeID;
		parent.parent.layer.open({
			type: 2,
			area: ['700px', '500px'],
			fixed: false,
			maxmin: false,
			title: '预授权',
			content: url,
			success:function(layero,index){
				var iframe = $(layero).find("iframe")[0].contentWindow;
				iframe.fireEventSaveInfoFun(function(AuthorizeUserID,AuthorizeUser,BeginTime,EndTime){
					me.fireEventParams["AuthorizeUserID"] = AuthorizeUserID;
					me.fireEventParams["AuthorizeUser"] = AuthorizeUser;
					me.fireEventParams["BeginTime"] = BeginTime;
					me.fireEventParams["EndTime"] = EndTime;
					parent.parent.layer.close(index);
					if(tabradio){
						$("#OperaterType1").prop("checked",true);
						me.hideOrShowDateTime(true);
					}
					me.setfromvalue();
					form.render('radio');
				})
			},
			end: function () {				
			},
			yes:function(index, layero){
				var iframe = $(layero).find("iframe")[0].contentWindow;
			}
		});
	};
	//预授权后依靠返回值给form赋值
	app.setfromvalue = function(){
		var me = this,
			AuthorizeUserID = me.fireEventParams["AuthorizeUserID"],
			AuthorizeUser  = me.fireEventParams["AuthorizeUser"],
			BeginTime = me.fireEventParams["BeginTime"],
			EndTime = me.fireEventParams["EndTime"];
		$("#AuthorizeUserName1").html("【"+AuthorizeUser+"】");	
		$("#AuthorizeUserID").html(AuthorizeUserID);
		$("#DateTimeText").val(BeginTime+" - "+EndTime);
	};
	//时间显示隐藏
	app.hideOrShowDateTime = function(isshow){
		if(isshow){
			if ($("#DateTimeTextDiv").hasClass("layui-hide")) {
				$("#DateTimeTextDiv").removeClass("layui-hide");
			}
			if ($("#AuthorizeUserName1").hasClass("layui-hide")) {
				$("#AuthorizeUserName1").removeClass("layui-hide");
			}
		}else{
			if (!$("#DateTimeTextDiv").hasClass("layui-hide")) {
				$("#DateTimeTextDiv").addClass("layui-hide");
			}
			if (!$("#AuthorizeUserName1").hasClass("layui-hide")) {
				$("#AuthorizeUserName1").addClass("layui-hide");
			}
		}
	};
	//初始化检验人/审核人信息
	app.initOperateUserInfo = function(){
		var me = this,
			UserId = '',
			UserName = '无',
			ConfirmerID = "",//检验者ID
			Confirmer = "",//检验者
			CheckerID = "",//审核者ID
			Checker = "",//审核者
			OperaterType = '',//授权方式
			OperaterTypeName = '',
			BeginTime = '',
			EndTime = '',
			isCheckTip = false;
		var local = uxutil.localStorage.get("LabStar_TS", true),
			userid = uxutil.cookie.get(uxutil.cookie.map.USERID);
		//检验者
		if (local && local[userid] && local[userid]['HandlerInfo']) {
			ConfirmerID = local[userid]['HandlerInfo'].Id || "";
			Confirmer = local[userid]['HandlerInfo'].Name || "";
		}
		//审核者
		if (local && local[userid] && local[userid]['CheckerInfo']) {
			CheckerID = local[userid]['CheckerInfo'].Id || "";
			Checker = local[userid]['CheckerInfo'].Name || "";
		}
			
		if(me.paramsObj.OperateType == 'Handler'){//检验人
			var isok = false;//localstor中是否存在信息  false存在  true不存在
			if(local){
				if(userid && local[userid]){
					if(local[userid]['HandlerInfo']){
						var HandlerInfo = local[userid]['HandlerInfo'];
						if (HandlerInfo["OperaterType"] == "0" && HandlerInfo['Id']) {
							UserId = HandlerInfo.Id || '';
							UserName = HandlerInfo.Name || '无';
							OperaterType = HandlerInfo.OperaterType || me.OPERATER_TYPE_LIST[0].value;
							BeginTime = HandlerInfo.BeginTime || '';
							EndTime = HandlerInfo.EndTime || '';
							isCheckTip = HandlerInfo.isCheckTip || false;
						} else if (HandlerInfo["OperaterType"] == "1" && HandlerInfo['Id']) {
							//判断时间范围是否有效
							if (HandlerInfo.BeginTime && HandlerInfo.EndTime) {
								var presentdatetime = new Date().getTime(),
									bdt = new Date(HandlerInfo.BeginTime).getTime(),
									edt = new Date(HandlerInfo.EndTime).getTime();
								if (bdt <= presentdatetime && presentdatetime <= edt) {
									UserId = HandlerInfo.Id || '';
									UserName = HandlerInfo.Name || '无';
									OperaterType = HandlerInfo.OperaterType || me.OPERATER_TYPE_LIST[0].value;
									BeginTime = HandlerInfo.BeginTime || '';
									EndTime = HandlerInfo.EndTime || '';
									isCheckTip = HandlerInfo.isCheckTip || false;
								} else {
									isok = true;
								}
							} else {
								isok = true;
							}
						} else {
							isok = true;
						}
					}else{
						isok = true;
					}
				}else{
					isok = true;
				}
			}else{
				isok = true;
			}
			if(isok){
				OperaterType =  me.OPERATER_TYPE_LIST[0].value;
			}
		} else if (me.paramsObj.OperateType == 'Checker') {//审核人
			me.getPara();//获得审定人参数
			var isTesterEqualChecker = $("#NTestType_TestFormCheck_Operater_0001").prop("checked");//NTestType_TestFormCheck_Operater_0001 -- 检验者与审核者不同
			var isok = false;
			if(local){
				if(userid && local[userid]){
					if(local[userid]['CheckerInfo']){
						var CheckerInfo = local[userid]['CheckerInfo'];
						if (CheckerInfo["OperaterType"] == "0" && CheckerInfo['Id']) {
							UserId = CheckerInfo.Id || '';
							UserName = CheckerInfo.Name || '无';
							OperaterType = CheckerInfo.OperaterType || me.OPERATER_TYPE_LIST[0].value;
							BeginTime = CheckerInfo.BeginTime || '';
							EndTime = CheckerInfo.EndTime || '';
							isCheckTip = CheckerInfo.isCheckTip || false;
							isTesterEqualChecker = CheckerInfo.isTesterEqualChecker || false;
						} else if (CheckerInfo["OperaterType"] == "1" && CheckerInfo['Id']) {
							if (CheckerInfo.BeginTime && CheckerInfo.EndTime) {
								var presentdatetime = new Date().getTime(),
									bdt = new Date(CheckerInfo.BeginTime).getTime(),
									edt = new Date(CheckerInfo.EndTime).getTime();
								if (bdt <= presentdatetime && presentdatetime <= edt) {
									UserId = CheckerInfo.Id || '';
									UserName = CheckerInfo.Name || '无';
									OperaterType = CheckerInfo.OperaterType || me.OPERATER_TYPE_LIST[0].value;
									BeginTime = CheckerInfo.BeginTime || '';
									EndTime = CheckerInfo.EndTime || '';
									isCheckTip = CheckerInfo.isCheckTip || false;
									isTesterEqualChecker = CheckerInfo.isTesterEqualChecker || false;
								} else {
									isok = true;
								}
							} else {
								isok = true;
							}
						} else {
							isok = true;
						}
					}else{
						isok = true;
					}
				}else{
					isok = true;
				}
			}else{
				isok = true;
			}
			if(isok){
				OperaterType =  me.OPERATER_TYPE_LIST[0].value;
			}
		}
		if(OperaterType == me.OPERATER_TYPE_LIST[0].value){//当前登录者
			OperaterTypeName = me.OPERATER_TYPE_LIST[0].text;
			me.hideOrShowDateTime(false);
			$("#OperaterType1").prop("checked",false);
			$("#OperaterType0").prop("checked",true);
			if (!$("#DateTimeTextDiv").hasClass("layui-hide")) {
				$("#DateTimeTextDiv").addClass("layui-hide");
			}
		}else if(OperaterType == me.OPERATER_TYPE_LIST[1].value){//预授权
			OperaterTypeName = me.OPERATER_TYPE_LIST[1].text;	
			me.hideOrShowDateTime(true);
			$("#OperaterType0").prop("checked",false);
			$("#OperaterType1").prop("checked",true);
			$("#DateTimeText").val(BeginTime.slice(0,10)+" "+BeginTime.slice(-8) + " - "+EndTime.slice(0,10)+" "+EndTime.slice(-8));
		}
		$("#AuthorizeUserName0").html("【"+uxutil.cookie.get(uxutil.cookie.map.USERNAME)+"】");//当前登录人
		if(me.paramsObj.OperateType == 'Handler'){//检验人
			$("#isCheckTip").attr("title","检验确认时,是否提示检验确认人");
			$("#OperateUserName").html("【当前检验人】：" + Confirmer + ' (' + OperaterTypeName + ')');//当前检验人
		} else if (me.paramsObj.OperateType == 'Checker') {//审核人
			$("#isCheckTip").attr("title","审核时,是否提示审核人");
			$("#OperateUserName").html("【当前检验人】：" + Confirmer + "，【当前审核人】：" + Checker + (ConfirmerID == CheckerID ? "<span style='color:red;margin-left:10px;'>【检验者和审核者为同一人!】</span>" : ""));//当前检验人
		}
		$("#isCheckTip").prop("checked", isCheckTip);//是否提示
		$("#NTestType_TestFormCheck_Operater_0001").prop("checked", isTesterEqualChecker);//是否提示
		//预授权模式
		if(OperaterType == me.OPERATER_TYPE_LIST[1].value){
			$("#AuthorizeUserID").html(UserId);//授权人员ID
			$("#AuthorizeUserName1").html("【"+UserName+"】");//授权人员名称
		}
		form.render();		
	};
    //确认操作
	app.onConfirmClick = function(){
		var me = this,
			operatertype =  $("#authorizeform input[name=OperaterType]:checked").val(),
			isCheckTip = $("#isCheckTip").prop("checked"),
			isTesterEqualChecker = $("#NTestType_TestFormCheck_Operater_0001").prop("checked");
		 var info = {
			"OperaterType":operatertype,
			 "isCheckTip": isCheckTip,
			 "isTesterEqualChecker": isTesterEqualChecker
		};
		if(operatertype == '0'){//登陆者本人
			info.Id = uxutil.cookie.get(uxutil.cookie.map.USERID);
			info.Name = uxutil.cookie.get(uxutil.cookie.map.USERNAME);
		}else if(operatertype == '1'){//预授权模式
			info.Id = $("#AuthorizeUserID").html() || '';
			info.Name = $("#AuthorizeUserName1").html().replace("【","").replace("】","") || '';
			if($("#DateTimeText").val() && $("#DateTimeText").val().split(" - ").length > 0){
				info.BeginTime = uxutil.date.toString($("#DateTimeText").val().split(" - ")[0],false);
				info.EndTime = uxutil.date.toString($("#DateTimeText").val().split(" - ")[1],false);
			}else {
				uxbase.MSG.onWarn("时间不可为空或时间格式错误!");
			}
			if(info.BeginTime.length > 0 && info.BeginTime.length < 19){
				uxbase.MSG.onWarn("开始时间格式错误!");
				return;
			}
			if(info.BeginTime.length > 0 && info.BeginTime.length < 19){
				uxbase.MSG.onWarn("结束时间格式错误!");
				return;
			}
			
			if(!info.BeginTime || !info.EndTime){
				uxbase.MSG.onWarn("预授权模式下，开始时间和结束时间不能为空!");
				return;
			}
			if(info.BeginTime > info.EndTime){
				uxbase.MSG.onWarn("预授权模式下，开始时间不能大于结束时间!");
				return;
			}
		}
		me.setOperateData(info);
	};
	app.setOperateData = function(info){
		var me = this,
			localTotalName = "LabStar_TS",
			USERID = uxutil.cookie.get(uxutil.cookie.map.USERID),
			operatetype = me.paramsObj.OperateType+"Info";
		var local = uxutil.localStorage.get("LabStar_TS", true);
		if (local) {
			if (local[USERID]) {//存在当前等录人记录
				local[USERID][operatetype] = [];
				local[USERID][operatetype]=info;
			} else {
				local[USERID] = {};
				local[USERID][operatetype] = [];
				local[USERID][operatetype]=info;
			}
		} else {
			local = {};
			local[USERID] = {};
			local[USERID][operatetype] = [];
			local[USERID][operatetype]=info;
		}
		uxutil.localStorage.set(localTotalName, JSON.stringify(local));
		uxbase.MSG.onSuccess("设置完成!");
		var index = parent.parent.layer.getFrameIndex(parent.window.name); //先得到当前iframe层的索引
		parent.parent.layer.close(index);
	};
	//查询参数 个性化-》默认-》出厂//审定人参数
	app.getPara = function () {
		var me = this,
			paraTypeCode = 'NTestType_TestFormCheck_Operater_Para',
			url = me.url.getListUrl + "&paraTypeCode=" + paraTypeCode + '&objectID=' + me.paramsObj.sectionId;
		if (!paraTypeCode) return;
		url += '&fields=BPara_ParaSource,BPara_' + me.ParaFilds.join(',BPara_');
		uxutil.server.ajax({ url: url, async: false }, function (res) {
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {//存在个性化设置
					me.Para = res.value.list;//记录
					var html = [];
					$.each(res.value.list, function (i, item) {
						html.push('<div class="layui-form-item" style="margin-top:5px;"><div class="layui-inline"><input type="checkbox" name="' + item["BPara_ParaNo"] + '" id="' + item["BPara_ParaNo"] + '" lay-skin="primary" title="' + item["BPara_CName"] + '" ' + (item["BPara_ParaValue"] == 1 ? "checked" : "") + '></div></div>');
					});
					$("#checkBoxs").html(html.join(""));
					form.render();
				} else {
					uxbase.MSG.onError("未找到配置参数!");
				}
			} else {
				uxbase.MSG.onError(res.msg || "个性化参数获取失败!");
			}
		})
	};
	//初始化
    app.init();
});