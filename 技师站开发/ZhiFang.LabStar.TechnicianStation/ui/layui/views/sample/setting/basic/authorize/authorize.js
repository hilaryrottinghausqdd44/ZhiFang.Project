/**
   @Name：预授权
   @Author：GHX
   @version 2021-05-12
 */
var fireEventSaveInfo ;
window.fireEventSaveInfoFun = function(callback){
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
		ClassDict:{},
		loadindex:null
	}
	//服务地址
	app.url={
		//登录校验
		checkLoginUrl:uxutil.path.LIIP_ROOT
						+'/ServerWCF/RBACService.svc/RBAC_BA_Login?isValidate=true',
		//根据员工ID获取用户账号
		selectUserUrl:uxutil.path.LIIP_ROOT
						+'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true',
		//新增操作权限
		addAuthorizeUrl:uxutil.path.ROOT
						+'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateAuthorize', 
		//新增操作授权对应小组
		addAuthorizeSectionUrl:uxutil.path.ROOT
						+'/ServerWCF/LabStarService.svc/LS_UDTO_AddLisOperateASection',
		//查询人员
		selectuserlisturl:uxutil.path.LIIP_ROOT
						+'/ServerWCF/RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?isPlanish=true',
		//查询更多小组
		selectSectionUrl:uxutil.path.ROOT
						+'/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true',
		//查询操作授权对应小组
		selectLisOperateASectionUrl:uxutil.path.ROOT
						+'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateASectionByHQL?isPlanish=true',
		//根据ID查询操作授权
		selectLisOperateAuthorizeByIdUrl:uxutil.path.ROOT
						+'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeById?isPlanish=true',
		//查询操作授权
		selectLisOperateAuthorizeUrl:uxutil.path.ROOT
						+'/ServerWCF/LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeByHQL?isPlanish=true',
		//查询枚举
		GET_ENUMURL: uxutil.path.ROOT +'/ServerWCF/CommonService.svc/GetClassDic'								
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
    //初始化
    app.init = function () {
        var me = this;		
        me.getParams();
        me.initListeners();
		me.initemp();
		me.initdatatime();
		me.initinfo();
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
		element.on('tab(authorize_Tab)', function (data) {
		   if(data.index == 0){//快捷授权
			  me.initemp();
			  me.initdatatime();
			  me.initinfo();
		   }else if (data.index == 1){//选择已有授权
			 app.initTable();
			 app.loadTableData();
		   }
		});
		form.on('radio(authorize_datetimeradio)', function(data){
			me.loadAuthorizeDateTime(data.value);
		});  
		form.on('select(authorize_username)',function(data){
			$("#AuthorizeUserID").val(data.value);
		});
		form.on('select(authorize_section_select)',function(data){
			var name = $("#authorize_section_select option:selected").text();
			var itemlist = '<input type="checkbox" name="isCheckTip_'+data.value+'" id="isCheckTip_'+data.value+'" lay-skin="primary" value="'+data.value+'" title="'+name+'">';
			$("#authorize_section_checkbox").append(itemlist);
			var select = document.getElementById("authorize_section_select");
			for(var i=0;i<select.length;i++){
				if(select.options[i].value == data.value){//删除公文库管理的显示
					select.options.remove(i);
				    break;
			    }
			}  
			form.render();
		});
		$("#confirmAuthorizeBut").click(function(){					
			var authorize_username = $("#authorize_username option:selected").text(),//授权人名称
				AuthorizeUserID = $("#AuthorizeUserID").val(),//授权人ID
				authorize_userpassword = $("#authorize_userpassword").val(),//密码
				authorize_beuser = $("#authorize_beuser").val(),//被授权人名称
				authorize_beuserid = $("#authorize_beuserid").val(),//被授权人ID
				authorize_datascope1 = $("#authorize_datascope1").val(),//开始日期
				authorize_timescope1 = $("#authorize_timescope1").val(),//开始时间
				authorize_datascope2 = $("#authorize_datascope2").val(),//结束日期
				authorize_timescope2 = $("#authorize_timescope2").val(),//结束时间
				authorize_section = $("#authorize_section_checkbox input[type='checkbox']:checked");//授权小组
			if(!authorize_username || !authorize_userpassword){
				uxbase.MSG.onWarn("授权人或密码为必填项!");
				return;
			}	
			if(!authorize_datascope1 || !authorize_timescope1 || !authorize_datascope2 || !authorize_timescope2){
				uxbase.MSG.onWarn("开始时间与结束时间为必填项!");
				return;
			}	
			if(!app.onTimeRangeValid()){
				return;
			}
			if(authorize_section.length <= 0){
				uxbase.MSG.onWarn("请选择被授权小组!");
				return;
			}
			//根据授权人ID返回登录账号
			me.getAccountByEmpId(AuthorizeUserID,function(account){
				//先校验授权人输入密码是否正确
				me.checkLogin(account,authorize_userpassword,function(){
					//新增保存授权操作
					me.addAuthorize(function(AuthorizeId){
						//保存所有操作授权对应小组关系
						me.addAuthorizeSectionList(AuthorizeId,function(SectionIds){
							//保存后处理
							me.afterSave(SectionIds);
						});
					}); 
				});
			}); 
		});
		//监听行单击事件
		table.on('row(authorizeTable)', function (obj) {
		    //标注选中样式
		    //obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
			$(".layui-table-body tr ").attr({ "style": "background:#FFFFFF" });
			$(obj.tr.selector).attr({ "style": "background:#77DDFF" });
			me.loadRecodeInfo(obj);
		});
		$("#usebut").click(function(){
			me.onAcceptClick();
		});
    };
	//选择已有授权-初始化左侧列表
	app.initTable = function() {
		var me = this,
			url = "";
		table.render({
			elem: '#authorizeTable',
			height: 'full-40',
			defaultToolbar: [],
			size: 'sm',
			page: true,
			//data: data,
			url: url,
			cols:[[ 
				{
					field: 'LisOperateAuthorize_Id',minWidth: 60,title: '主键ID',sort: false,hide: true
				}, {
					field: 'LisOperateAuthorize_AuthorizeUser',title: '授权人',	minWidth: 100,hide: false,sort: false,
				}, {
					field: 'LisOperateAuthorize_AuthorizeType',	title: '授权类型',	minWidth: 60,hide: false,sort: false,
					templet: function (data) {
						if(me.config.ClassDict && me.config.ClassDict[data["LisOperateAuthorize_AuthorizeType"]]){
								return me.config.ClassDict[data["LisOperateAuthorize_AuthorizeType"]];
						}
					}
				}, {
					field: 'LisOperateAuthorize_BeginTime',	title: '授权开始时间',minWidth: 125,hide: false,	sort: false,
					templet: function (data) {
						if (data["LisOperateAuthorize_AuthorizeType"]+"" == "1") {
							return uxutil.date.toString(data["LisOperateAuthorize_BeginTime"],false) || '';
						} else if(data["LisOperateAuthorize_AuthorizeType"]+"" == "2"){
							var str = data["LisOperateAuthorize_BeginTime"].split(' ');
							if(str.length == 2){
								return str[1];
							}else{
								return "";
							}
						}
					}
				}, {
					field: 'LisOperateAuthorize_EndTime',	title: '授权结束时间',minWidth: 125,hide: false,	sort: false,
					templet: function (data) {
						if (data["LisOperateAuthorize_AuthorizeType"]+"" == "1") {
							return uxutil.date.toString(data["LisOperateAuthorize_EndTime"],false) || '';
						} else if(data["LisOperateAuthorize_AuthorizeType"]+"" == "2"){
							var str = data["LisOperateAuthorize_EndTime"].split(' ');
							if(str.length == 2){
								return str[1];
							}else{
								return "";
							}
						}
					}
				}, {
					field: 'LisOperateAuthorize_OperateType',	title: '授权操作类型',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_OperateUserID',	title: '被授权人',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_AuthorizeInfo',	title: '授权说明',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day1',	title: '周一',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day2',	title: '周二',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day3',	title: '周三',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day4',	title: '周四',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day5',	title: '周五',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day6',	title: '周六',minWidth: 125,hide: true,	sort: false
				} , {
					field: 'LisOperateAuthorize_Day0',	title: '周日',minWidth: 125,hide: true,	sort: false
				}  
			]],
			limit: 50,
			limits:[50,100,150,200,250,300],
			autoSort: true, //禁用前端自动排序
			text: {
				none: '暂无相关数据'
			},
			response: function() {
				return {
					statusCode: true, //成功状态码
					statusName: 'code', //code key
					msgName: 'msg ', //msg key
					dataName: 'data' //data key
				}
			},
			parseData: function(res) { //res即为原始返回的数据
				if (!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				if(data.count > 0){
					var sysdate = uxutil.server.date.getDate(),
					DataAddTime =  uxutil.date.toString(uxutil.date.getDate(sysdate.toString()),false),
					v = uxutil.date.getDate(DataAddTime);
					//按周期类型数据过滤处理,只显示周期打勾的数据，如果开始时间大于结束时间，时间往前挪一天(周期)
					for(var i=0;i<data.list.length;i++){
						//周期类型有效数据
						if(data.list[i].LisOperateAuthorize_AuthorizeType+"" == "2"){
							var BeginTime = data.list[i].LisOperateAuthorize_BeginTime;
							var EndTime = data.list[i].LisOperateAuthorize_EndTime;
							//当天是周几
							var dayNum = v.getDay();
							var str = 'LisOperateAuthorize_Day'+dayNum;
							//当天数据是否已选
							var isCheck = data.list[i][str];
							//开始时间大于结束时间时，需要把上一天的数据也给显示出来
							if(uxutil.date.getDate(BeginTime)>uxutil.date.getDate(EndTime)){
								//上一天日期
								var preday = uxutil.date.getNextDate(uxutil.date.toString(DataAddTime,true),-1);
								dayNum = preday.getDay();
								str = 'LisOperateAuthorize_Day'+dayNum;
								isCheck = data.list[i][str];
								if(isCheck=='false')data.list.splice(i, 1);
							}else{
								if(isCheck=='false')data.list.splice(i, 1);
							}
						}
					}
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			done: function(res, curr, count) {	
				if(me.config.loadindex){
					layer.close(me.config.loadindex);
				}
				if(count>0){
					if ($("#authorizeTable+div .layui-table-body table.layui-table tbody tr:first-child")[0])
					    $("#authorizeTable+div .layui-table-body table.layui-table tbody tr:first-child")[0].click();
				}
			}
		});
	};
	//选择已有授权-加载表格数据
	app.loadTableData = function(){
		var me = this,
			tableurl  = me.url.selectLisOperateAuthorizeUrl;
		var url = app.url.GET_ENUMURL +"?classnamespace=ZhiFang.Entity.LabStar&classname=AuthorizeType";
		uxutil.server.ajax({ url: url,async: false }, function (res) {
			if (res.success && res.ResultDataValue) {
				var data =  $.parseJSON(res.ResultDataValue);
				var value = data;
				if(!value){
					uxbase.MSG.onWarn("未获取到授权类型,请重新刷新!");
					return;
				}
				$.each(value, function (i, item) {
					me.config.ClassDict[item["Id"]] = item["Name"];
				});
				//查询条件拼接
				tableurl +="&fields=LisOperateAuthorize_Id,LisOperateAuthorize_AuthorizeUser,LisOperateAuthorize_AuthorizeType,LisOperateAuthorize_BeginTime,LisOperateAuthorize_EndTime,LisOperateAuthorize_OperateType,LisOperateAuthorize_OperateUserID,LisOperateAuthorize_AuthorizeInfo,LisOperateAuthorize_Day1,LisOperateAuthorize_Day2,LisOperateAuthorize_Day3,LisOperateAuthorize_Day4,LisOperateAuthorize_Day5,LisOperateAuthorize_Day6,LisOperateAuthorize_Day0";
				var params = [],
					sysdate = uxutil.server.date.getDate(),
					DataAddTime =  sysdate.toString(),
					v = uxutil.date.toString(uxutil.date.getDate(DataAddTime),false);
					//周几
				if(DataAddTime){
					var str = "(lisoperateauthorize.AuthorizeType=1"+
					" and lisoperateauthorize.BeginTime<='"+v +
					"' and lisoperateauthorize.EndTime>='"+v +
					"' and lisoperateauthorize.IsUse=1"+
					" and lisoperateauthorize.OperateTypeID="+me.paramsObj.OperateTypeID+")"+
					" or (lisoperateauthorize.AuthorizeType=2"+
					" and lisoperateauthorize.IsUse=1"+
					" and lisoperateauthorize.OperateTypeID="+me.paramsObj.OperateTypeID+")";
					params.push(str);
				}
				
				if(params.length > 0) {
					tableurl += "&where="+params.join(' and ');
				} else {
					tableurl += "&where=";
				}
				tableurl += "&_dc="+new Date().getTime();
				me.config.loadindex = layer.load();
				table.reload('authorizeTable',{
					url:tableurl
				});
			}
		});
		
	};
	//选择已有授权-加载单条信息
	app.loadRecodeInfo = function(obj){
		var me = this;
		me.clearRightInfo();//清空信息
		var url = me.url.selectLisOperateAuthorizeByIdUrl;
		url += '&id='+obj.data.LisOperateAuthorize_Id;
		url += '&fields=Comment,LisOperateAuthorize_Id,LisOperateAuthorize_OperateType,LisOperateAuthorize_OperateTypeID,LisOperateAuthorize_AuthorizeUserID,LisOperateAuthorize_OperateUserID,LisOperateAuthorize_AuthorizeUser,LisOperateAuthorize_OperateUser,LisOperateAuthorize_BeginTime,LisOperateAuthorize_EndTime,LisOperateAuthorize_AuthorizeInfo,LisOperateAuthorize_AuthorizeType,LisOperateAuthorize_Day1,LisOperateAuthorize_Day2,LisOperateAuthorize_Day3,LisOperateAuthorize_Day4,LisOperateAuthorize_Day5,LisOperateAuthorize_Day6,LisOperateAuthorize_Day0';
		var indexload = layer.load();//显示遮罩层
		uxutil.server.ajax({
		    url: url,
			async: false
		}, function (res) {
			if (res.success) {
				if(res.value){
					app.writeRightInfo(res.value,indexload);
				}else{
					layer.close(indexload);
					uxbase.MSG.onWarn("未查询到详细信息!");
				}
			} else {
				layer.close(indexload);
				uxbase.MSG.onError(res.msg);
			}
		});
	};
	//选择已有授权-清空右侧信息
	app.clearRightInfo = function(){
		var me = this;
		$("#authorize_textarea").val("");//授权详细信息
		$("#authorize_section_checkbox2").html();//授权小组
		$("#LisOperateAuthorize_Id").val("");
		$("#LisOperateAuthorize_OperateType").val("");
		$("#LisOperateAuthorize_OperateTypeID").val("");
		$("#LisOperateAuthorize_AuthorizeUserID").val("");
		$("#LisOperateAuthorize_OperateUserID").val("");
		$("#LisOperateAuthorize_AuthorizeUser").val("");
		$("#LisOperateAuthorize_OperateUser").val("");
		$("#LisOperateAuthorize_BeginTime").val("");
		$("#LisOperateAuthorize_EndTime").val("");
		$("#LisOperateAuthorize_AuthorizeInfo").val("");
		$("#LisOperateAuthorize_AuthorizeType").val("");
		$("#LisOperateAuthorize_Day1").val("");
		$("#LisOperateAuthorize_Day2").val("");
		$("#LisOperateAuthorize_Day3").val("");
		$("#LisOperateAuthorize_Day4").val("");
		$("#LisOperateAuthorize_Day5").val("");
		$("#LisOperateAuthorize_Day6").val("");
		$("#LisOperateAuthorize_Day0").val("");
	};
	//选择已有授权-填充右侧信息
	app.writeRightInfo = function(data,indexload){
		var me = this;
		var info =me.config.ClassDict[data.LisOperateAuthorize_AuthorizeType];
		var strComment = '授权操作类型:'+data.LisOperateAuthorize_OperateType+'\n';
		var AuthorizeUser ="";
		strComment+= '授权人:'+data.LisOperateAuthorize_AuthorizeUser+'\n';
		var OperateUser ="";
		strComment+= '被授权人:'+data.LisOperateAuthorize_OperateUser+'\n';
		if("2" == data.LisOperateAuthorize_AuthorizeType){//周期 授权时段不显示日期,周期需要显示周几
			var dayArr=[];
			if(data.LisOperateAuthorize_Day1 == "true")dayArr.push('周一');
			if(data.LisOperateAuthorize_Day2 == "true")dayArr.push('周二');
			if(data.LisOperateAuthorize_Day3 == "true")dayArr.push('周三');
			if(data.LisOperateAuthorize_Day4 == "true")dayArr.push('周四');
			if(data.LisOperateAuthorize_Day5 == "true")dayArr.push('周五');
			if(data.LisOperateAuthorize_Day6 == "true")dayArr.push('周六');
			if(data.LisOperateAuthorize_Day0 == "true")dayArr.push('周日');
			var strDay = dayArr.join(',') || "";
			strComment+= '周期:'+strDay+'\n';
			var arr1 = data.LisOperateAuthorize_BeginTime.split(' ');	
			var arr2 = data.LisOperateAuthorize_EndTime.split(' ');	
			var strDateTime = arr1[1]+'-'+arr2[1];
			strComment+= '授权时间段:'+strDateTime+'\n';
		}else{
			var strDateTime = uxutil.date.toString(data.LisOperateAuthorize_BeginTime)+'-'+uxutil.date.toString(data.LisOperateAuthorize_EndTime);
			strComment+= '授权时间段:'+strDateTime+'\n';
		}
		strComment+= '授权说明:'+data.LisOperateAuthorize_AuthorizeInfo+' \n';
		strComment+= '授权类型:'+info;
		
		$("#authorize_textarea").val(strComment);//授权详细信息
		$("#LisOperateAuthorize_Id").val(data.LisOperateAuthorize_Id);
		$("#LisOperateAuthorize_OperateType").val(data.LisOperateAuthorize_OperateType);
		$("#LisOperateAuthorize_OperateTypeID").val(data.LisOperateAuthorize_OperateTypeID);
		$("#LisOperateAuthorize_AuthorizeUserID").val(data.LisOperateAuthorize_AuthorizeUserID);
		$("#LisOperateAuthorize_OperateUserID").val(data.LisOperateAuthorize_OperateUserID);
		$("#LisOperateAuthorize_AuthorizeUser").val(data.LisOperateAuthorize_AuthorizeUser);
		$("#LisOperateAuthorize_OperateUser").val(data.LisOperateAuthorize_OperateUser);
		$("#LisOperateAuthorize_BeginTime").val(data.LisOperateAuthorize_BeginTime);
		$("#LisOperateAuthorize_EndTime").val(data.LisOperateAuthorize_EndTime);
		$("#LisOperateAuthorize_AuthorizeInfo").val(data.LisOperateAuthorize_AuthorizeInfo);
		$("#LisOperateAuthorize_AuthorizeType").val(data.LisOperateAuthorize_AuthorizeType);
		$("#LisOperateAuthorize_Day1").val(data.LisOperateAuthorize_Day1);
		$("#LisOperateAuthorize_Day2").val(data.LisOperateAuthorize_Day2);
		$("#LisOperateAuthorize_Day3").val(data.LisOperateAuthorize_Day3);
		$("#LisOperateAuthorize_Day4").val(data.LisOperateAuthorize_Day4);
		$("#LisOperateAuthorize_Day5").val(data.LisOperateAuthorize_Day5);
		$("#LisOperateAuthorize_Day6").val(data.LisOperateAuthorize_Day6);
		$("#LisOperateAuthorize_Day0").val(data.LisOperateAuthorize_Day0);
		
		me.loadSectionDataByID(data.LisOperateAuthorize_Id,indexload,function(list){
			var checkIds = [],itemlist = "";
			for(var i=0;i<list.length;i++){
				checkIds.push(list[i].LisOperateASection_LBSection_Id);
				itemlist += '<input type="checkbox" name="checkedsection_'+list[i].LisOperateASection_LBSection_Id+'" id="checkedsection_'+list[i].LisOperateASection_LBSection_Id+'" lay-skin="primary" value="'+list[i].LisOperateASection_LBSection_Id+'" title="'+list[i].LisOperateASection_LBSection_CName+'" checked="">';
			}
			$("#authorize_section_checkbox2").html(itemlist);//授权小组
			form.render('checkbox');
		});
		
	};
	//选择已有授权-采用按钮监听
	app.onAcceptClick = function(){
		var me = this,
			LisOperateAuthorize_Id = $("#LisOperateAuthorize_Id").val(),
			LisOperateAuthorize_OperateType = $("#LisOperateAuthorize_OperateType").val(),
			LisOperateAuthorize_OperateTypeID = $("#LisOperateAuthorize_OperateTypeID").val(),
			LisOperateAuthorize_AuthorizeUserID = $("#LisOperateAuthorize_AuthorizeUserID").val(),
			LisOperateAuthorize_OperateUserID = $("#LisOperateAuthorize_OperateUserID").val(),
			LisOperateAuthorize_AuthorizeUser = $("#LisOperateAuthorize_AuthorizeUser").val(),
			LisOperateAuthorize_OperateUser = $("#LisOperateAuthorize_OperateUser").val(),
			LisOperateAuthorize_BeginTime = $("#LisOperateAuthorize_BeginTime").val(),
			LisOperateAuthorize_EndTime = $("#LisOperateAuthorize_EndTime").val(),
			LisOperateAuthorize_AuthorizeInfo = $("#LisOperateAuthorize_AuthorizeInfo").val(),
			LisOperateAuthorize_AuthorizeType = $("#LisOperateAuthorize_AuthorizeType").val(),
			LisOperateAuthorize_Day1 = $("#LisOperateAuthorize_Day1").val(),
			LisOperateAuthorize_Day2 = $("#LisOperateAuthorize_Day2").val(),
			LisOperateAuthorize_Day3 = $("#LisOperateAuthorize_Day3").val(),
			LisOperateAuthorize_Day4 = $("#LisOperateAuthorize_Day4").val(),
			LisOperateAuthorize_Day5 = $("#LisOperateAuthorize_Day5").val(),
			LisOperateAuthorize_Day6 = $("#LisOperateAuthorize_Day6").val(),
			LisOperateAuthorize_Day0 = $("#LisOperateAuthorize_Day0").val();
		
		//如果是周期，1970-01-01 替换为当前日期,如果开始时间小于结束时间，开始时间往前推一天
		var BeginTime = LisOperateAuthorize_BeginTime;
		var EndTime = LisOperateAuthorize_EndTime;
		if("2" == LisOperateAuthorize_AuthorizeType ){//周期			
			var sysdate = uxutil.date.getDate(uxutil.server.date.getDate().toString());
			var end = EndTime.split(" ");
			var start = BeginTime.split(" ");
			//开始时间大于结束时间时，需要把上一天的数据也给显示出来
			if(uxutil.date.getDate(BeginTime)>uxutil.date.getDate(EndTime)){
				//上一天日期
				var preday = uxutil.date.getNextDate(uxutil.date.toString(sysdate,true),-1);
				BeginTime = uxutil.date.toString(preday,true)+ ' '+start[1];
				EndTime = uxutil.date.toString(sysdate,true)+ ' '+end[1];
			}else{
				BeginTime = uxutil.date.toString(sysdate,true)+ ' '+start[1];
				EndTime = uxutil.date.toString(sysdate,true)+ ' '+end[1];
			}
		}
		//是否包含当前小组
		var isCurrSection = true;
		//授权小组是否包含当前小组
		var ids = $("#authorize_section_checkbox2 input[type=checkbox]:checked");       
		if(!ids)isCurrSection=false;
		var type = typeof(ids);
		if (type == 'array'){
			if(ids.indexOf(me.paramsObj.sectionId)==-1)isCurrSection=false;
		}
		if(!isCurrSection){
			uxbase.MSG.onWarn("该授权不包含当前小组，请重新选择!");
			return;
		}
		
		var data = {
			AuthorizeUserID:LisOperateAuthorize_AuthorizeUserID,//检验确认人ID
			AuthorizeUserName:LisOperateAuthorize_AuthorizeUser,//检验确认人
			BeginTime:BeginTime,//预授权开始时间
			EndTime:EndTime,//预授权结束时间
			AuthorizeType:LisOperateAuthorize_AuthorizeType, //授权类型
			Day1:LisOperateAuthorize_Day1,//周一
			Day2:LisOperateAuthorize_Day2,
			Day3:LisOperateAuthorize_Day3,
			Day4:LisOperateAuthorize_Day4,//周一
			Day5:LisOperateAuthorize_Day5,
			Day6:LisOperateAuthorize_Day6,
			Day0:LisOperateAuthorize_Day0,
			CurrSection:isCurrSection
		};
		fireEventSaveInfo(LisOperateAuthorize_AuthorizeUserID,LisOperateAuthorize_AuthorizeUser,BeginTime,EndTime);	
	};
	//根据授权操作id查询存在关系小组并赋值
	app.loadSectionDataByID = function(Id,indexload,callback){
		var me = this;
		var url =  me.url.selectLisOperateASectionUrl;
		url += '&fields=LisOperateASection_LBSection_CName,LisOperateASection_LBSection_Id';
		url+="&where=lisoperateasection.LisOperateAuthorize.Id="+Id;
		uxutil.server.ajax({
		    url: url
		}, function (res) {
			layer.close(indexload);
			if (res.success) {
				var list = res.value ? res.value.list : [];
				callback(list);
			} else {
				uxbase.MSG.onError(res.msg);
			}
		});	
	};
	//保存后处理
	app.afterSave=function(SectionIds){
		var me = this,
			entity = me.getAddParams(),
			isThisSection = false;
			
		for(var i in SectionIds){
			if(SectionIds[i] == me.paramsObj.sectionId){
				isThisSection = true;
				break;
			}
		}
		if(!isThisSection){
			uxbase.MSG.onWarn("该授权不包含当前小组，请重新选择!");
			return;
		}
		fireEventSaveInfo(entity.AuthorizeUserID,entity.AuthorizeUser,entity.BeginTime,entity.EndTime);	
	};
	//保存所有操作授权对应小组关系
	app.addAuthorizeSectionList = function(AuthorizeId,callback){
		var me = this,
			SectionIds = $("#authorize_section_checkbox input[type='checkbox']:checked"),//授权小组
			sectionarr = [];
		for(var i = 0; i<SectionIds.length;i++){
			sectionarr.push(SectionIds[i].value);
		}
		if(typeof(sectionarr)  == 'string'){
			sectionarr = [sectionarr];
		}
		me.addAuthorizeSectionOne(AuthorizeId,callback,sectionarr,0,0);
	};
	//单个保存操作授权对应小组关系
	app.addAuthorizeSectionOne = function(AuthorizeId,callback,SectionIds,index,errorCount){
		//操作授权对应小组Lis_OperateASection
		var me = this,
			url =  me.url.addAuthorizeSectionUrl;
		if(index >= SectionIds.length){//结束保存
			if(errorCount == 0){
				callback(SectionIds);
			}else{
				uxbase.MSG.onError('操作授权对应小组关系保存中' + errorCount + '条失败！');
			}
			return;
		}
		var entity ={
			LisOperateAuthorize:{Id:AuthorizeId,DataTimeStamp:[0,0,0,0,0,0,0,0]},
			LBSection:{Id:SectionIds[index],DataTimeStamp:[0,0,0,0,0,0,0,0]}
		};		
		var load = layer.load();
		uxutil.server.ajax({
		    url: url,
		    type: 'post',
		    data: JSON.stringify({ entity: entity})
		}, function (res) {
		        layer.close(load);
		        if(!res.success){
		        	errorCount++;
		        }
		        me.addAuthorizeSectionOne(AuthorizeId,callback,SectionIds,++index,errorCount);
		});
	},
	//新增操作权限
	app.addAuthorize = function(callback){
		var me = this,
			url =  me.url.addAuthorizeUrl;
		var entity = me.getAddParams();
		if(!entity){
			return;
		}
		entity.BeginTime = uxutil.date.toServerDate(entity.BeginTime);
		entity.EndTime = uxutil.date.toServerDate(entity.EndTime);
		var load = layer.load();
		uxutil.server.ajax({
		    url: url,
		    type: 'post',
		    data: JSON.stringify({ entity: entity})
		}, function (res) {
		        layer.close(load);
		        if(res.success){
		        	var id = (res.value || {}).id || '';
		        	callback(id);
		        }else{
					uxbase.MSG.onError(res.msg);
		        }
		});
	};
	//获取新增的数据
	app.getAddParams=function(){
		var me = this,
			authorize_username = $("#authorize_username option:selected").text(),//授权人名称
			AuthorizeUserID = $("#AuthorizeUserID").val(),//授权人ID
			authorize_beuser = $("#authorize_beuser").val(),//被授权人名称
			authorize_beuserid = $("#authorize_beuserid").val();//被授权人ID
		var entity = {
			AuthorizeType:'1',//临时
			OperateTypeID:me.paramsObj.OperateTypeID,
			OperateType:me.paramsObj.OperateTypeText,
			AuthorizeUserID:AuthorizeUserID,
			AuthorizeUser:authorize_username,
			OperateUserID:authorize_beuserid,
			OperateUser:authorize_beuser,
			IsUse:1
		};
		
		var TimeRange = me.onTimeRangeValid();
		if(!TimeRange){
			return;
		}
		entity.BeginTime = TimeRange.BeginTime;
		entity.EndTime = TimeRange.EndTime;
		
		return entity;
	};
	app.onTimeRangeValid = function(){
		var me = this,
			BeginTime_Date = $("#authorize_datascope1").val(),//开始日期
			BeginTime_Time = $("#authorize_timescope1").val(),//开始时间
			EndTime_Date = $("#authorize_datascope2").val(),//结束日期
			EndTime_Time = $("#authorize_timescope2").val();//结束时间
		
		BeginTime_Date = uxutil.date.toString(BeginTime_Date,true) || '';
		BeginTime_Time = uxutil.date.toString(BeginTime_Date + " "+BeginTime_Time,false) || '';
		EndTime_Date = uxutil.date.toString(EndTime_Date,true) || '';
		EndTime_Time = uxutil.date.toString(EndTime_Date + " "+EndTime_Time,false) || '';
		
		var info = {
			BeginTime:BeginTime_Date + ' ' + BeginTime_Time.slice(-8),
			EndTime:EndTime_Date + ' ' + EndTime_Time.slice(-8)
		};
		
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
		
		return info;
	};
	//校验授权人用户名和密码是否正确
	app.checkLogin = function(account,passWord,callback) {
		var me = this,
			url = me.url.checkLoginUrl;
		url += '&strUserAccount=' + account + '&strPassWord=' + passWord;
		var indexload = layer.load();//显示遮罩层
		uxutil.server.ajax({
		    url: url
		}, function (res) {
		        layer.close(indexload);
		        if(res+'' == 'true'){
		        	callback();
		        }else{
					uxbase.MSG.onError('授权人密码错误,请重新输入!');
		        }
		});
	};
	//根据员工ID获取登录账号
	app.getAccountByEmpId = function(EmpId,callback){
		var me = this,
			url =  me.url.selectUserUrl;
		url += '&fields=RBACUser_Account&where=rbacuser.HREmployee.Id=' + EmpId;
		var indexload = layer.load();//显示遮罩层
		uxutil.server.ajax({
		    url: url
		}, function (res) {
		        layer.close(indexload);
		        if (res.success) {
		          var list = (res.value || {}).list || [];
		          if(list.length == 0){
					  uxbase.MSG.onWarn("被授权人没有找到账号，请先维护!");
		          	return;
		          }else if(list.length > 1){
					  uxbase.MSG.onWarn("被授权人存在多个账号信息，请联系管理员维护!");
		          	return;
		          }else{
		          	callback(list[0].RBACUser_Account);
		          }
		        } else {
					uxbase.MSG.onError('检验项目删除失败!');
		        }
		});
	};
	app.loadAuthorizeDateTime = function(value){
		var me = this;
		var	sysdate = uxutil.server.date.getDate(),
			startDate = uxutil.date.toString( uxutil.date.getDate(sysdate.toString())),
			endDate = "";
		switch(value){
			case '1'://5分钟
				endDate = uxutil.date.toString(me.getTimeByMinute(5));
				break;
			case '2'://半小时
				endDate = uxutil.date.toString(me.getTimeByMinute(30));
				break;
			case '3'://2小时
				endDate = uxutil.date.toString(me.getTimeByMinute(120));
				break;
			case '4'://4小时
				endDate = uxutil.date.toString(me.getTimeByMinute(240));
				break;
			case '5'://当日
				startDate = uxutil.date.toString(sysdate,true) + ' 00:00:00';
				endDate = uxutil.date.toString(sysdate,true) + ' 23:59:59';
				break;
			default://自定义
				startDate = "";
				endDate = "";
				break;
		}
		$("#authorize_datascope1").val(startDate.slice(0,10));
		$("#authorize_timescope1").val(startDate.slice(-8));
		$("#authorize_datascope2").val(endDate.slice(0,10));
		$("#authorize_timescope2").val(endDate.slice(-8));
		//form.render('select');
	};
	//根据分钟返回截止时间
	app.getTimeByMinute=function(minute){
		var me = this,
			sysdate = uxutil.server.date.getDate();
		if(!sysdate){
			return "";
		}
		return new Date(uxutil.server.date.getTimes() + 1000*60*minute);
	};
	app.initemp = function(){
		var me = this,
			labid = uxutil.cookie.get(uxutil.cookie.map.LABID);
		var url = me.url.selectuserlisturl+"&fields=HREmpIdentity_HREmployee_CName,HREmpIdentity_HREmployee_StandCode,HREmpIdentity_HREmployee_Id";
			url+= '&sort=[{"property":"HREmpIdentity_DispOrder","direction":"ASC"}]';
		url += "&where=(hrempidentity.IsUse=1 and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.TSysCode='1001001') and (LabID='" + labid+"')";
		uxutil.server.ajax({
			url: url,
			async: false
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = '<option value="">请选择</option>';
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].HREmpIdentity_HREmployee_Id + "'>" + value.list[i].HREmpIdentity_HREmployee_CName + "</option>";
					$("#authorize_username").empty();
					$("#authorize_username").append(tempAjax);
				}
				form.render('select'); //需要渲染一下;
			} else {
				uxbase.MSG.onError(data.msg);
			}
		}); 
	 };
	app.initdatatime=function(){
		var me = this;
		laydate.render({
			 elem: '#authorize_datascope1',
			 type: 'date',
			 trigger: 'click',
			 format: 'yyyy-MM-dd',
			 done: function (value, date, endDate) {
			 }
		});
		laydate.render({
			 elem: '#authorize_datascope2',
			 type: 'date',
			 trigger: 'click',
			 format: 'yyyy-MM-dd',
			 done: function (value, date, endDate) {
			 }
		});
		/* var tempAjax = '<option value=""></option>';
		for(var i=0;i<24;i++){
			for(var a=0;a<60;a+=15){
				var numtime =  "";
				if(i<10){
					if(a==0){
						numtime = "0"+i+":00:00";
						tempAjax += "<option value='" + numtime + "'>" + numtime + "</option>";
						$("#authorize_timescope1").empty();
						$("#authorize_timescope1").append(tempAjax);
						$("#authorize_timescope2").empty();
						$("#authorize_timescope2").append(tempAjax);
					}else{
						numtime = "0"+i+":"+a+":00";
						tempAjax += "<option value='" + numtime + "'>" + numtime + "</option>";
						$("#authorize_timescope1").empty();
						$("#authorize_timescope1").append(tempAjax);
						$("#authorize_timescope2").empty();
						$("#authorize_timescope2").append(tempAjax);
					}
				}else{
					if(a==0){
						numtime = i+":00:00";
						tempAjax += "<option value='" + numtime + "'>" + numtime + "</option>";
						$("#authorize_timescope1").empty();
						$("#authorize_timescope1").append(tempAjax);
						$("#authorize_timescope2").empty();
						$("#authorize_timescope2").append(tempAjax);
					}else{
						numtime = i+":"+a+":00";
						tempAjax += "<option value='" + numtime + "'>" + numtime + "</option>";
						$("#authorize_timescope1").empty();
						$("#authorize_timescope1").append(tempAjax);
						$("#authorize_timescope2").empty();
						$("#authorize_timescope2").append(tempAjax);
					}
				}
			}
		}
		form.render('select'); */ //需要渲染一下;
	 }
	app.initinfo = function(){
		var me = this;
		$("#authorize_operate_name").html("授权操作:"+me.paramsObj.OperateTypeText);
		$("#authorize_beuser").val(uxutil.cookie.get(uxutil.cookie.map.USERNAME));
		$("#authorize_beuserid").val(uxutil.cookie.get(uxutil.cookie.map.USERID));
		$("#authorize_datetimeradio2").prop("checked",true);
		me.loadAuthorizeDateTime('2');
		var local = uxutil.localStorage.get("LabStar_TS", true),
			userid = uxutil.cookie.get(uxutil.cookie.map.USERID),
			sectionList = [];
		if(local){
			if(userid && local[userid]){
				if(local[userid]['OpenedSectionList']){
					sectionList = local[userid]['OpenedSectionList'];
					var itemlist = "";
					for(var i=0;i<sectionList.length;i++){
						if(sectionList[i].Id == me.paramsObj.sectionId){
							itemlist += '<input type="checkbox" name="isCheckTip_'+sectionList[i].Id+'" id="isCheckTip_'+sectionList[i].Id+'" lay-skin="primary" value="'+sectionList[i].Id+'" title="'+sectionList[i].Name+'" checked="">';
						}else{
							itemlist += '<input type="checkbox" name="isCheckTip_'+sectionList[i].Id+'" id="isCheckTip_'+sectionList[i].Id+'" lay-skin="primary" value="'+sectionList[i].Id+'" title="'+sectionList[i].Name+'">';
						}
					}
					$("#authorize_section_checkbox").html(itemlist);
					form.render();
				}
			}
		}	
		me.initsection(sectionList);
	};
	//初始化更多小组选择
	app.initsection = function(sectionList){
		var me = this;		
		var sectionid = [];
		for(var i=0;i<sectionList.length;i++){
			sectionid.push(sectionList[i].Id);
		}
		var url = me.url.selectSectionUrl+"&fields=LBRight_LBSection_CName,LBRight_LBSection_UseCode,LBRight_LBSection_DispOrder,LBRight_LBSection_Id";
			url+= '&sort=[{"property":"LBRight_LBSection_DispOrder","direction":"ASC"}]';
			url += "&where=(lbright.LBSection.Id not in("+sectionid.join(",")+") and lbright.RoleID is null and lbright.EmpID="+uxutil.cookie.get(uxutil.cookie.map.USERID)+")";
		uxutil.server.ajax({
			url: url,
			async: false
		}, function(data) {
			if(data) {
				var value = data[uxutil.server.resultParams.value];
				if(value && typeof(value) === "string") {
					if(isNaN(value)) {
						value = value.replace(/\\u000d\\u000a/g, '').replace(/\\u000a/g, '</br>').replace(/[\r\n]/g, '');
						value = value.replace(/\\"/g, '&quot;');
						value = value.replace(/\\/g, '\\\\');
						value = value.replace(/&quot;/g, '\\"');
						value = eval("(" + value + ")");
					} else {
						value = value + "";
					}
				}
				if(!value) return;
				var tempAjax = '<option value="">更多小组选择</option>';
				for(var i = 0; i < value.list.length; i++) {
					tempAjax += "<option value='" + value.list[i].LBRight_LBSection_Id + "'>" + value.list[i].LBRight_LBSection_CName + "</option>";
					$("#authorize_section_select").empty();
					$("#authorize_section_select").append(tempAjax);
				}
				form.render('select'); //需要渲染一下;
			} else {
				uxbase.MSG.onError(data.msg);
			}
		}); 
	 };
    //初始化
    app.init();
});