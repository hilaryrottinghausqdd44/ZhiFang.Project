/**
 * @name：modules/pre/sample/tranrule/index 样本分发\n
 * @author：liangyl
 * @version 2020-09-23
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table',
	uxbase: 'ux/base',
	PreSampleContentIndex:'modules/pre/sample/tranrule/content',
	SearchBar:'modules/pre/sample/tranrule/search',
	PreSampleTranruleBasicParams:'modules/pre/sample/tranrule/params',
	CommonSelectSickType: 'modules/common/select/sicktype'
}).define(['uxutil','form','uxbase','PreSampleContentIndex','SearchBar','PreSampleTranruleBasicParams','CommonSelectSickType'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		table = layui.table,
		uxbase = layui.uxbase,
		PreSampleContentIndex = layui.PreSampleContentIndex,
		PreSampleTranruleBasicParams = layui.PreSampleTranruleBasicParams,
		SearchBar = layui.SearchBar,
		CommonSelectSickType = layui.CommonSelectSickType,
		MOD_NAME = 'PreSampleTranRuleIndex';
	
	//列表字段：格式=BarCode&条码号&100&show,OrderExecTime&医嘱指定执行时间&100&show,
	var LIST_COLS_INFO = null;
	//后台获取字段数组
	var LIST_FIELDS = null;
	//列表字段：医嘱信息
	var ORDER_LIST_COLS_INFO = null;
	//后台获取字段数组 --医嘱信息
	var ORDER_LIST_FIELDS = null;
	
	//根据条码号获取样本列表
	var GET_BARCODE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleDispenseGetBarCodeFormByBarCode";
	//登录服务
	var LOGIN_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//强制更新（确认送检按钮）
	var UPDATE_STATE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode";
	 //根据条码号签收并查询数据服务路径
	var AUTO_SIGN_BY_BARCODE_GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSignForSampleByBarCode';
	//获得取单凭证服务
	var GET_VOUCHERRETRIEVAL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara';
	//获得frx模板数据  --取单凭证
	var GET_MODEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelAndAutoUploadModel?isPlanish=true';
	//通过条码号签收
	var DISPENSE_SINGN_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleDispenseSingnForBarCodeFormByBarCode';
	//通过条码号分发，多条要循环调用
	var DISPENSE_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleDispenseByBarCode';
    //通过条码号进行分发取消操作
	var CLEAR_DISPENSE_BARCODE_URL = uxutil.path.ROOT + '/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleDispenseCancelByBarCodeFormId';
	
	//身份验证失效时间 -毫秒数  -- 存储到local中的名称
	var Sign_Local_VerifiyInvalidDate_Name = 'PreSampleSignIndex_Local_VerifiyInvalidDate';

	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
			   '<div class="layui-inline">',
                  '<label class="layui-form-label">条码号:</label> ',
				  '<div class="layui-input-inline" style="width:185px;">',
					 '<input type="text" name="{domId}-barCode" id="{domId}-barCode"  placeholder="请扫描条码" autocomplete="off" class="layui-input" />',
				  '</div>',
				'</div>',
				'<div class="layui-inline layui-hide" id="{domId}-div-SickTypeID">', 
                    '<label class="layui-form-label">就诊类型:</label>', 
				    '<div class="layui-input-inline">',
	                 '<select name="{domId}-SickTypeID" id="{domId}-SickTypeID" lay-filter="{domId}-SickTypeID"> <option value="">请选择</option></select>',
				    '</div>',
				'</div>',
				 '<div class="layui-inline" style="float:right;" id="{domId}-checkbox-search-toolbar">',
					'<input type="checkbox" id="{domId}-show-search-toolbar" lay-filter="{domId}-show-search-toolbar" title="查询" lay-skin="primary" />',  
				'</div>',
				'<div class="layui-inline" style="float: right;">', 
	                '<label id="{domId}-num" class="layui-form-label" style="color: blue;font-size: 18px;font-weight: bold ;">数量:0</label>', 
				'</div>',
				'<div class="layui-inline" style="float: right;">', 
					'<input type="checkbox" id="{domId}-isAutoPrint" lay-filter="{domId}-isAutoPrint" title="分发自动打印" lay-skin="primary" />',  
				'</div>',
			'</div>',
		'</div>',
		'<div class="{domId}-grid-div layui-hide" id="{domId}-SearchTool"></div>',
		'<div class="layui-row style="margin:0px;padding:0px;">',
		    '<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12" id = "{domId}-Content-row">',
                '<div id="{domId}-Content"></div>',
			'</div>',
		'</div>',
		'<style>',
			'.{domId}-grid-div{padding:2px;margin-bottom:0px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
			'.layui-form-onswitch-red{border-color:#FF5722;background-color:#FF5722;}',
		'</style>'
	].join('');
		//验证身份
	var IDENYITY_DOM = [
		'<div class="layui-form" style="padding:15px;">',
		'<div class="layui-form-item" style="margin-bottom:5px;">',
		'<label class="layui-form-label">用户名:</label> ',
		'<div class="layui-input-inline" style="width:185px;">',
		   '<input type="text" name="account" id="account" lay-filter="account" lay-verify="required" placeholder="用户名" autocomplete="off" class="layui-input"/>',
		'</div>',
		'</div>',
		'<div class="layui-form-item" style="margin-bottom:5px;">',
		'<label class="layui-form-label">密码:</label> ',
		'<div class="layui-input-inline" style="width:185px;">',
		  '<input type="password" name="password" id="password" lay-filter="password" lay-verify="required" placeholder="密码" autocomplete="off" class="layui-input"/>',
		'</div>',
		'</div>',
		'<div class="layui-form-item" style="text-align:right;margin-top:10px;">',
		'<button type="button" id="confirm" class="layui-btn layui-btn-xs layui-btn-normal" lay-filter="idenyity" lay-submit><i class="layui-icon layui-icon-ok"></i>确定</button>',
		'</div>',
		'</div>'
	].join('');
	
	//样本单实例
	var PreSampleContentIndexInstance = null;
	//参数实例
	var PreSampleTranruleBasicParamsInstance = null;
	//查询工具栏
	var SearchBarInstance = null;
	
	//所有列表数据
	var LIST_DATA = [];
	//打印模板_业务类型  ---签收
	var BusinessType = 3;
	//打印模板_模板类型 ---签收
	var ModelType = 8;
	//打印模板_模板类型   ---签收
	var ModelTypeName = "样本签收_样本清单";

	var PreSampleTranRuleIndex = {
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:null //站点类型
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleTranRuleIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		$('#' + me.config.domId).html('');
		$('#' + me.config.domId+'-grid-div').html('');
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		//初始化参数设置HTML
		me.initParamsHtml();
		var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height()-$("#"+me.config.domId+'-SearchTool').height()-55;
		//查询工具栏
		SearchBarInstance = SearchBar.render({
			domId: me.config.domId+'-SearchTool',
			height:'30px',
			//过滤的开单科室条件
			defalutDeptID:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0053'),
			//过滤的检验小组条件
			defalutSectionID:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0055'),
	        //过滤的样本类型条件
			defalutSampleTypeID:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0056'),
			//默认查询已签收样本日期范围,选已签收时 显示默认的时间范围
			defalutDate:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0052'),
			//查询条件时间类型  ---时间类型下拉内容
			defalutDateType:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0062'),
			nodetype:me.config.nodetype, //站点类型
			searchClickFun:function(where){ //查询按钮查询事件
				if(!where.where){
					me.clearData();
					layer.msg("条件为空不能查询！",{icon:5});
				    return false;
				}
				PreSampleContentIndexInstance.onSearch(where,function(list){
					LIST_DATA = list;
					 //数量显示
    	            $("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
    	            me.Size();
				});
			}
		});
	     //样本单列字段与医嘱信息字段比较，如果样本单列表字段中不存在医嘱信息字段，则从样本单列表加入医嘱信息字段
        var cols = [],cols_info = LIST_FIELDS.join(',');
        for(var i in ORDER_LIST_FIELDS){
        	if(cols_info.indexOf(ORDER_LIST_FIELDS[i]) == -1){  //如果不存在，则加入
//      		LisBarCodeFormVo_LisBarCodeForm_BarCode&条码号&100&show,
        		LIST_COLS_INFO.push(ORDER_LIST_FIELDS[i]+'&'+ORDER_LIST_FIELDS[i]+'&100&hide');
        	}
        }
		//样本信息列表实例
		PreSampleContentIndexInstance = PreSampleContentIndex.render({
			domId: me.config.domId+'-Content',
			height:height,
			cols:LIST_COLS_INFO,
			ordercols:ORDER_LIST_COLS_INFO,//医嘱信息配置字段
			//界面排序
			sortFields:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0035'),
			//标签打印机名  ---补打条码的打印机名
			TagPrintName:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0037'),
			//样本清单打印机名
			PrintName:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0038'),
			//是否显示分发日期下拉框
			IsShowDateCom:PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0065'),
			nodetype:me.config.nodetype //站点类型
		});
		form.render();
	};
	//初始化参数设置HTML
	Class.prototype.initParamsHtml = function(){
		var me = this;
		//是否显示查询功能(查询勾选框)
		var showSearchTool = PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0039');
        if(showSearchTool == '0')$('#'+me.config.domId+'-checkbox-search-toolbar').addClass('layui-hide');
		//是否显示就诊类型条件
		var showSickType = PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0040');
		if(showSickType=='1'){ //显示
			$('#'+me.config.domId+'-div-SickTypeID').removeClass('layui-hide');
			//就诊类型
			CommonSelectSickType.render({
				domId:me.config.domId+'-SickTypeID',
				done: function () {
	                if(PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0041')=='1'){
	                	if(me.getHistoryInfo() && me.getHistoryInfo().SickTypeID){
							$('#'+me.config.domId+'-SickTypeID').val(me.getHistoryInfo().SickTypeID);
						}
	                }
                }
			});
		}
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		$("#"+me.config.domId+"-barCode").focus();
		//扫码,回车事件
	    $("#"+me.config.domId+"-barCode").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	var barcode = $("#"+me.config.domId+"-barCode").val();
	        	if(!barcode){
					layer.msg("条码号不能为空,请扫码！",{icon:5});
					return false;
				}
	        	//判断条码号是否已扫码
	        	var isExist = PreSampleContentIndexInstance.isExistBarcode(barcode);
	        	if(isExist){
	        		//清空扫码输入框数据
			    	$("#"+me.config.domId+"-barCode").val('');
			        $("#"+me.config.domId+"-barCode").focus();
	        		return false;
	        	}
	        	me.onBarCode(barcode);
	            return false;
	        }
	    });
	    form.on('checkbox('+me.config.domId+'-show-search-toolbar)', function(data){
			me.showToolbar(data.elem.checked);
		}); 
		 // 窗体大小改变时，调整高度显示
    	$(window).resize(function() {
			me.Size();
    	});
    	form.on('select('+me.config.domId+'-SickTypeID)', function(data){
            if(PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0041') == '1'){
		        me.insertHistoryInfo({SickTypeID:data.value});
		    }
		}); 
	    //身份验证确认按钮
		form.on('submit(idenyity)', function (data) {
			var account = data.field["account"],
				password = data.field["password"];
			layer.load();
			//请求登入接口
			uxutil.server.ajax({
				url: LOGIN_URL,
				cache: false,
				data: {
					strUserAccount: account,
					strPassWord: password
				}
			}, function (data) {
				layer.closeAll('loading');
				if (data === true) {
					if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0011')) {
						//分钟转为毫秒+当前毫秒数 -- 存储到local
						var VerifiyInvalidDate = uxutil.server.date.getTimes() + PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0011') * 60 * 1000;
						uxutil.localStorage.set(Sign_Local_VerifiyInvalidDate_Name, JSON.stringify(VerifiyInvalidDate));
					}
					me.onSignClick(true);
					layer.closeAll();
				} else {
					layer.msg('账号或密码错误！', { icon: 5, anim: 0 });
				}
			});
		});
    };
    //获取默认就诊类型信息
	Class.prototype.getHistoryInfo = function(){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
		return uxutil.localStorage.get('PreSampleBarcodeTranruleSickTypeID_' + me.config.nodetype + empId,true);
	};
	//记录默认就诊类型{SickTypeID:''}
	Class.prototype.insertHistoryInfo = function(info){
		var me = this,
			empId = uxutil.cookie.get(uxutil.cookie.map.USERID);
		uxutil.localStorage.set('PreSampleBarcodeTranruleSickTypeID_' + me.config.nodetype + empId,JSON.stringify(info));
	};
    //显示隐藏查询工具栏
	Class.prototype.showToolbar = function(bo){
		var me = this;
        if(bo){
        	$("#"+me.config.domId+"-SearchTool").removeClass('layui-hide');
        }else{
        	$("#"+me.config.domId+"-SearchTool").addClass('layui-hide');
        }
        me.Size();
	};
	/**根据条码号获取样本列表 */
	Class.prototype.Size = function(){
		var me = this;
		var win = $(window),
		    maxheight = win.height();
        var height = maxheight - ($("#"+me.config.domId+'-form').height() + $("#"+me.config.domId+'-SearchTool').height())-60;
        var bo = $('#'+me.config.domId+'-show-search-toolbar').prop('checked');
        if(!bo)height = height+5;
        $("#"+me.config.domId+"-Content-row").css('height',height+'px');
        PreSampleContentIndexInstance.changeSize(height,bo);
	};
	/**根据条码号获取样本列表 */
	Class.prototype.getListByBarCode = function(barcodes,callback){
		var me = this;
		var params ={
			nodetype:me.config.nodetype,//站点类型
			barCode:barcodes,//条码集合，多个用逗号分开
			fields:PreSampleContentIndexInstance.getFields(),
		    isPlanish:true 
		};
		//就诊类型
		if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-SickTypeID").val()) {
			params.sickType = $("#" + me.config.domId + "-SickTypeID").val();
		}
		var config = {
			type:'post',
			url:GET_BARCODE_LIST_URL,
			data:JSON.stringify(params)
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = ((data.value || {}).list || []);
				callback(list);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	
	//扫码处理(按模式处理分发模式)
	Class.prototype.onBarCode = function(barcodes){
		var me = this;
		//分发方式
		var distribute_model = PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0063').split(',');
		if(distribute_model =='1'){//自动分发
			//需要先看下参数设置签收方式是否是自动的，如果是自动就先做签收再做分发，如果签收是手动，调用通过条码号分发服务
            me.dispenseSign(barcodes);
		}else{//手动分发
			//直接调用通过条码获取数据的服务
			me.getListByBarCode(barcodes,function(list){
				//数据加入列表
			    me.addRow(list);
			});
		}
	};
    //通过条码号分发，多条要循环调用     参数：isForceDispense=true -强制分发
	Class.prototype.onDispense = function (barcode,isForceDispense,callback) {
		var me = this,
			load = layer.load();
		var	entity = {
				nodetypeID: me.config.nodetype, //站点类型
				barCodes: barcode, //
				fields:PreSampleContentIndexInstance.getFields(),//字段
				sickType:null, 	//就诊类型
				isPlanish:true,
				isForceDispense:isForceDispense, //强制分发
				TestDate:null,//检测日期
				ruleType:null //规则类型
			};
		//就诊类型
		if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-SickTypeID").val()) {
			entity.sickType = $("#" + me.config.domId + "-SickTypeID").val();
		}
		var TestDate = PreSampleContentIndexInstance.getRule().TestDate;
		var ruleType = PreSampleContentIndexInstance.getRule().ruleType;
		//检测日期
		if(TestDate)entity.TestDate = TestDate;
		//ruleType
		if(ruleType)entity.ruleType = ruleType;
		var config = { type: "POST", url: DISPENSE_BARCODE_URL,data: JSON.stringify(entity) };
		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "条码分发失败!", { icon: 5, anim: 0 });
			}
		})
	};
	
	//通过条码号分发取消
	Class.prototype.onClearDispense = function (barCodeFormIds,callback) {
		var me = this,
			load = layer.load(),
			entity = {
				nodetypeID: me.config.nodetype, //站点类型
				barCodeFormIds: barCodeFormIds, //
				fields:PreSampleContentIndexInstance.getFields(),//字段
				isPlanish:true
			};
		var config = { type: "POST", url:CLEAR_DISPENSE_BARCODE_URL, data: JSON.stringify(entity) };
		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到barCodeFormIds!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "取消分发失败!", { icon: 5, anim: 0 });
			}
		})
	};
	//------------------------------------签收部分代码---------------------------------------------
	//分发是签收操作
	Class.prototype.dispenseSign = function(barcode){
		var me = this;
		//签收方式
		var model = PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0009').split(',');
	    if(model == '1'){//自动签收  ----自动就先做签收再做分发，
	    	//根据条码号自动签收  isAutoSignFor:自动签收；isForceSignFor：强制签收
	    	var isAutoSignFor = true;//自动签收
	    	var isForceSignFor = true;//强制签收
            me.SignByBarCode(barcode,isAutoSignFor,isForceSignFor,function(list){
            	//通过条码号分发
            	me.dispense(barcode,false);
            });
	    }else{
	    	//通过条码号分发
            me.dispense(barcode,false);
	    }
	};
	//分发
	Class.prototype.dispense = function(barcode,isForceSignFor){
		var me = this;
		var model = '0';//分发
		me.onDispense(barcode,isForceSignFor,function(list){
			//提示信息处理
			me.onFailureInfoHandle(list,model, function (resultList) {
				var Msg = [];//弹出信息
				$.each(resultList, function (i, item) {
					var data = item["data"],
						tipList = item["tipList"];
					$.each(list, function (a, itemA) {
						if (data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
							if (item["isSuccess"]) {
								list[a]["LAY_CHECKED"] = true;
								data.LisBarCodeFormVo_IsConfirm = '1';
                                list[a] = data;
							} 
							return false;
						}
					});
					//弹出信息处理
					if (tipList.length > 0) {
						$.each(tipList, function (k, itemK) {
							Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
						});
					}
				});
				if (Msg.length > 0)
					layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
				 
				 //分发自动打印  -isAutoPrint
			    var isAutoPrint = $('#'+me.config.domId+'-isAutoPrint').prop('checked');
			    //自动打印分发标签
			    if(isAutoPrint)PreSampleContentIndexInstance.onBarcodePrint(list,true);
				//数据加入列表
				me.addRow(list);
			});
		});
	};
	//分发(确定)
	Class.prototype.dispense2 = function(isForceSignFor,checklist,TipMsg){
		var me = this;
		var model = '0';//分发
		var barcode = null;
	    var TipMsg = TipMsg || [];
		if(checklist.length>0){
			 barcode = checklist[0].LisBarCodeFormVo_LisBarCodeForm_BarCode;
		}
		me.onDispense(barcode,isForceSignFor,function(list){
			//提示信息处理
			me.onFailureInfoHandle(list,model, function (resultList) {
				var Msg = [];//弹出信息
				$.each(resultList, function (i, item) {
					var data = item["data"],
						tipList = item["tipList"];
					$.each(list, function (a, itemA) {
						if (data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
							if (item["isSuccess"])    {
								list[a]["LAY_CHECKED"] = true;
								data.LisBarCodeFormVo_IsConfirm = '1';
                                list[a] = data;
							} 
							return false;
						}
					});
					//弹出信息处理
					if (tipList.length > 0) {
						$.each(tipList, function (k, itemK) {
							TipMsg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
							Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
						});
					}
				});
				
				 //分发自动打印  -isAutoPrint
			    var isAutoPrint = $('#'+me.config.domId+'-isAutoPrint').prop('checked');
			    //自动打印分发标签
			    if(isAutoPrint)PreSampleContentIndexInstance.onBarcodePrint(list,true);
				//数据加入列表
				me.addRow(list);
				checklist.splice(checklist-(checklist.length-1),1);
				if(checklist.length>0){
					me.dispense2(isForceSignFor,checklist,TipMsg);
				}
				if (TipMsg.length > 0 && checklist.length==0 )
					layer.alert(TipMsg.join('<br>'), { icon: 0, anim: 0 });
				 
			});
		});
	};
	//样本签收按钮
	Class.prototype.onSignClick = function (isVerified) {
		var me = this,
			isVerified = isVerified || false,//是否已验证过身份
			VerifiyInvalidDate = uxutil.localStorage.get(Sign_Local_VerifiyInvalidDate_Name, true) || null,
			errorlistTableCache = [];
		//获得选中的条码
		var barcodes = PreSampleContentIndexInstance.getCheckedBarcodes();
		if (barcodes.length > 0) {
			//是否需要签收人身份验证
			if (!isVerified && PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0010') == 1 && (!VerifiyInvalidDate || uxutil.server.date.getTimes() > VerifiyInvalidDate)) {
				me.onVerifiySign();
				return;
			}
			var isAutoSignFor = true;//自动签收；
			var isForceSignFor= false;//强制签收
			var model = '1';//签收
			//根据条码号签收
			me.onAutoSignByBarCode(barcodes.join(), isAutoSignFor, isForceSignFor, function (list) {
				//提示信息处理
				me.onFailureInfoHandle(list,model, function (resultList) {
					var Msg = [];//弹出信息
					var PrintList = [],//打印清单数据
						VoucherRetrievalList = [];//取单凭证
					//根据参数判断是否清空列表
					if(PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0012') != 1)LIST_DATA = [];
					$.each(resultList, function (i, item) {
						var data = item["data"],
							tipList = item["tipList"];
						$.each(LIST_DATA, function (a, itemA) {
							if (data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
								if (item["isSuccess"]) {
									LIST_DATA[a] = data;
									LIST_DATA[a]["LAY_CHECKED"] = true;
									//自动签收是否自动打印签收单
									if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0013') == 1) PrintList.push(data);
									//签收后是否自动打印取单凭证
									if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0031') == 1) VoucherRetrievalList.push(data);
								} else {
									//失败信息加入失败列表
									$.each(tipList, function (j, itemJ) {
										errorlistTableCache.push('条码号:'+data["LisBarCodeFormVo_LisBarCodeForm_BarCode"]+ itemJ );
									});
									//签收失败是否显示样本信息
									if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0015') == 1) {
										LIST_DATA[a] = data;
										LIST_DATA[a]["LAY_CHECKED"] = true;
										layer.msg(errorlistTableCache,{icon:1}); //失败信息弹出提示
									} else {
										LIST_DATA.splice(a, 1);
									}
								}
								return false;
							}
						});
						//弹出信息处理
						if (tipList.length > 0) {
							$.each(tipList, function (k, itemK) {
								Msg.push("条码号为:" + data["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "," + itemK);
							});
						}
					});
					if (Msg.length > 0)
						layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
					//无数据清空样本项目和医嘱信息
					if (LIST_DATA.length == 0)me.clearData();
					//数据加入列表
					me.addRow(LIST_DATA);
					//打印签收单
					if (PrintList.length > 0)PreSampleContentIndexInstance.onPrintList(PrintList,ModelType,ModelTypeName,'打印清单');
					//打印取单凭证
					if (VoucherRetrievalList.length > 0) me.onPrintVoucherRetrieval(VoucherRetrievalList);
				});
			});
		} else {
			layer.msg("请先勾选签收数据!", { icon:0,anim:0});
		}
	};
	//根据条码号自动签收  签收参数(服务封装使用)  isAutoSignFor:自动签收；isForceSignFor：强制签收
	Class.prototype.getSignParams = function (barCodes, isAutoSignFor, isForceSignFor) {
	    var me = this,
			isAutoSignFor = isAutoSignFor || false,
			isForceSignFor = isForceSignFor || false,
			fields = PreSampleContentIndexInstance.getFields(),
			entity = { 
				nodetypeID: me.config.nodetype,
				barCodes: barCodes, 
				sickType: null, 
				deliverierID: null, 
				deliverier: null, 
				fields: fields, 
				isPlanish: true, 
				isAutoSignFor: isAutoSignFor, 
				isForceSignFor: isForceSignFor 
		    };
		//就诊类型
		if (PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0040') == 1 && $("#" + me.config.domId + "-SickTypeID").val()) {
			entity.sickType = $("#" + me.config.domId + "-SickTypeID").val();
		}
		return entity;
	};

	//根据条码号自动签收  isAutoSignFor:自动签收；isForceSignFor：强制签收
	Class.prototype.onAutoSignByBarCode = function (barCodes, isAutoSignFor, isForceSignFor, callback) {
		var me = this,
			load = layer.load(),
			entity = me.getSignParams(barCodes, isAutoSignFor, isForceSignFor);
		var config = { type: "POST", url: AUTO_SIGN_BY_BARCODE_GET_LIST_URL, data: JSON.stringify(entity) };
		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})
	};
	//根据条码号自动签收----扫码  isAutoSignFor:自动签收；isForceSignFor：强制签收
	Class.prototype.SignByBarCode = function (barCodes, isAutoSignFor, isForceSignFor, callback) {
		var me = this,
			load = layer.load(),
			entity = me.getSignParams(barCodes, isAutoSignFor, isForceSignFor);
		var config = { type: "POST", url: DISPENSE_SINGN_BARCODE_URL, data: JSON.stringify(entity) };
		uxutil.server.ajax(config, function (res) {
			//隐藏遮罩层
			layer.close(load);
			if (res.success) {
				if (res.value && res.value.list && res.value.list.length > 0) {
					callback && callback(res.value.list);
				} else {
					layer.msg(res.ErrorInfo || "未查到该条码信息!", { icon: 0, anim: 0 });
				}
			} else {
				layer.msg(res.ErrorInfo || "获取条码信息失败!", { icon: 5, anim: 0 });
			}
		})
	};
   //打印取单凭证
	Class.prototype.onPrintVoucherRetrieval = function (list) {
		var me = this,
			BusinessTypeCode = BusinessType,//前处理
			ModelTypeCode = 9,//样本签收_样本清单
			ModelTypeName = "样本签收_取单凭证",
			barcodes = [],
			list = list || [],
			config = {};
		if (list && list.length > 0) {
			//拼接条码号
			$.each(list, function (i, item) {
				barcodes.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
			});
			//调用通用打印界面
			config = {
				nodetypeID: me.config.nodetype,
				barCodes: barcodes.join()
			};
			layer.open({
				type: 2,
				area: ['45%', '58%'],
				fixed: false,
				maxmin: false,
				title: '打印',
				content: uxutil.path.ROOT + '/ui/layui/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=' + BusinessTypeCode + '&ModelType=' + ModelTypeCode + '&ModelTypeName=' + ModelTypeName + '&isDownLoadPDF=false',
				success: function (layero, index) {
					var iframe = $(layero).find("iframe")[0].contentWindow;
					iframe.PrintDataStr = JSON.stringify(config);
					iframe.GetPDFUrl = GET_VOUCHERRETRIEVAL_URL;
				},
				end: function () { }
			});
		}
	};
	 //验证身份(签收按钮操作才需要)
	Class.prototype.onVerifiySign = function () {
		var me = this;
		layer.open({
			type: 1,
			skin: 'layui-layer-rim', //加上边框
			area: '300px', //宽高
			content: IDENYITY_DOM,
			success: function (layero, index) {
				
			}
		});
	};
	//参数配置的提示方式处理,model=1 -签收,否则分发
	Class.prototype.onFailureInfoHandle = function (list,model, callback) {
		var me = this,
			list = JSON.parse(JSON.stringify(list)),
			isOut = false,//是否是等待确认框执行
			pendingData = [],//暂时存储数据集合 等待再次发送服务数据返回后一起处理
			needUpdateBarcode = [],//需要再次发送服务条码号集合
			resultList = [];//{ data: null, isSuccess: true, tipList: [] };//data:当前数据，isSuccess：是否成功,tipList:提示信息集合
		
		var failureInfo_field = "LisBarCodeFormVo_failureInfo";
		var BarCode_field = "LisBarCodeFormVo_LisBarCodeForm_BarCode";
		$.each(list, function (i, item) {
			//自主选择-- 不允许
			if (item["IsNotAllow"] && item["IsNotAllow"] == 1) {
				resultList.push({ data: item, isSuccess: false, tipList: [item["NotAllowInfo"]] });
				//不需要再次发送服务的数据存储
				if (needUpdateBarcode.join().indexOf(item[BarCode_field]) == -1) pendingData.push(item);
				return true;
			}
			resultList.push({ data: item, isSuccess: true, tipList: [] });
			var failureInfoArr = item[failureInfo_field] ? JSON.parse(item[failureInfo_field]) : [];
			$.each(failureInfoArr, function (j, itemJ) {
				if (isOut) return false;
				var alterMode = itemJ["alterMode"],
					failureInfo = itemJ["failureInfo"];
				switch (String(alterMode)) {
					case "4"://用户自行选择
						isOut = true;
						var msginfo = "条码号为：" + item[BarCode_field] + "，" + failureInfo + ",是否允许操作？";
	                	uxbase.MSG.onConfirm(msginfo,{ icon: 3, title: '提示',enter:true},
		                	function(index){
		                		isOut = false;
								failureInfoArr[j]["alterMode"] = -1;
								list[i][failureInfo_field] = JSON.stringify(failureInfoArr);
								me.onFailureInfoHandle(list,model, callback);
								//这里可以写需要处理的流程
								layer.close(index);//执行完后关闭
		                	},
		                	function(index){
		                		//不允许
								isOut = false;
								list[i]["IsNotAllow"] = 1;
								list[i]["NotAllowInfo"] = failureInfo+",用户操作不允许签收!";
								me.onFailureInfoHandle(list,model, callback);
		                		layer.close(index);//执行完后关闭
		                	}
		                );
						break;
					case "3"://允许且提示
						resultList[resultList.length - 1].tipList.push(failureInfo);
						break;
					case "2"://不允许不提示
						resultList[resultList.length - 1].isSuccess = false;
						break;
					case "1"://不允许且提示
						resultList[resultList.length - 1].tipList.push(failureInfo);
						resultList[resultList.length - 1].isSuccess = false;
						break;
					case "-1"://需要再次发送服务
						if (needUpdateBarcode.join().indexOf(item[BarCode_field]) == -1)
							needUpdateBarcode.push(item[BarCode_field]);
						break;
					default:
						break;
				}
			});
			//不需要再次发送服务的数据存储
			if (needUpdateBarcode.join().indexOf(item[BarCode_field]) == -1) pendingData.push(item);

			if (isOut) return false;
		});
		//存在需要再次更新数据
		if (!isOut && needUpdateBarcode.length > 0) {
			if(model =='1'){//签收
				me.onAutoSignByBarCode(needUpdateBarcode.join(), true, true, function (data) {
					me.onFailureInfoHandle(data.concat(pendingData),model,callback);
				});
			}else{
				me.onDispense(needUpdateBarcode.join(),true,function(data){
					me.onFailureInfoHandle(data.concat(pendingData),model,callback);
				});
			}
		}
		//执行完成
		if (!isOut && needUpdateBarcode.length == 0) callback && callback(resultList);
	};
	//打印清单
    Class.prototype.onListPrint = function(){
    	var me = this;
    		var checklist  = PreSampleContentIndexInstance.getCheckedList();
		if(checklist.length==0){
			layer.msg("请勾选行数据!", { icon: 0, anim: 0 });
			return false;
		}
		var PrintList= [];
        for(var i in checklist){
			if(checklist[i].LisBarCodeFormVo_IsConfirm=='1'){
				PrintList.push(checklist[i]);
			}
		}
        if(PrintList.length==0){
        	layer.msg("请先分发再打印清单!", { icon: 0, anim: 0 });
			return false;
        }
        var modeltype = "13";
        var modeltypename  = "样本分发_样本清单";
    	PreSampleContentIndexInstance.onPrintList(PrintList,modeltype,modeltypename,'打印清单');
    };
     //打印流转单
    Class.prototype.onPrintRoam = function(){
    	var me = this;
    	var checklist  = PreSampleContentIndexInstance.getCheckedList();
		if(checklist.length==0){
			layer.msg("请勾选行数据!", { icon: 0, anim: 0 });
			return false;
		}
		var PrintList= [];
        for(var i in checklist){
			if(checklist[i].LisBarCodeFormVo_IsConfirm=='1'){
				var TagAndFlowSheetInfo = checklist[i].LisBarCodeFormVo_PrintDispenseTagAndFlowSheetInfo;
				if(TagAndFlowSheetInfo)TagAndFlowSheetInfo = JSON.parse(TagAndFlowSheetInfo);
                 TagAndFlowSheetInfo  = TagAndFlowSheetInfo || [];
                if(TagAndFlowSheetInfo.length>0){
                	for(var j in TagAndFlowSheetInfo){
                		var List = [];
                		checklist[i].GSampleNo = TagAndFlowSheetInfo[j].GSampleNo;
                		checklist[i].ItemNames = TagAndFlowSheetInfo[j].ItemNames;
                		PrintList.push(checklist[i]);
                		List.push(checklist[i]);
                		var modeltype = "15";
                        var modeltypename = "样本分发_流转单";
    	                PreSampleContentIndexInstance.PrintModel(BusinessType,modeltype,modeltypename,List);
                	}
                }
			}
		}
        if(PrintList.length==0){
        	layer.msg("请先分发再打印流转单!", { icon: 0, anim: 0 });
			return false;
        }
   };
    
    
    //样本分发（确定按钮
	Class.prototype.onAccept= function () {
		var me = this;
		var checklist  = PreSampleContentIndexInstance.getCheckedList();
		if(checklist.length==0){
			layer.msg("请勾选需要样本分发的行数据!", { icon: 0, anim: 0 });
			return false;
		}
        //通过条码号分发
    	me.dispense2(false,checklist); 
	};
	//分发取消
	Class.prototype.revoke = function () {
		var me = this,
		    barCodeFormIds = [],msg=[];
		var checklist  = PreSampleContentIndexInstance.getCheckedList();
		if(checklist.length==0){
			layer.msg("请勾选需要分发取消的行数据!", { icon: 0, anim: 0 });
			return false;
		}
		var clearData = [];
		for(var i in checklist){
			if(checklist[i].LisBarCodeFormVo_IsConfirm=='1'){
				barCodeFormIds.push(checklist[i].LisBarCodeFormVo_LisBarCodeForm_Id);
				clearData.push(checklist[i]);
			}
		}
		if(clearData.length==0){
			layer.msg("请先分发再取消分发", { icon: 0, anim: 0 });
			return false;
		}
	    //执行分发取消
	    me.onClearDispense(barCodeFormIds.join(','),function(list){//去掉已分发标识
	    	for(var i in list){
	    		list[i].LisBarCodeFormVo_IsConfirm = '';
	    	}
	    	me.addRow(list);
	    });
	};
    //新增行
    Class.prototype.addRow = function(list){
     	var me = this;
    	//新增数据行
		PreSampleContentIndexInstance.loadData(list);
	    //清空扫码输入框数据
    	$("#"+me.config.domId+"-barCode").val('');
        $("#"+me.config.domId+"-barCode").focus();
        //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
	    me.Size();
    };
    //数据清空
    Class.prototype.clearData = function(){
    	var me = this;
    	LIST_DATA = [];
    	PreSampleContentIndexInstance.clearData();
    	 //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
        me.Size();
    };
	//核心入口
	PreSampleTranRuleIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//参数功能
	    PreSampleTranruleBasicParamsInstance = PreSampleTranruleBasicParams.render({nodetype:me.config.nodetype});
		//初始化功能参数
		PreSampleTranruleBasicParamsInstance.init(function(){
			//列表字段
			LIST_COLS_INFO = PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0058').split(',') || [];
			LIST_FIELDS = [];
			for(var i in LIST_COLS_INFO){
				var arr = LIST_COLS_INFO[i].split('&');
				LIST_FIELDS.push(arr[0]);
			}
			//医嘱列表字段
			ORDER_LIST_COLS_INFO = PreSampleTranruleBasicParamsInstance.get('Pre_OrderDispense_DefaultPara_0070').split(',') || [];
			ORDER_LIST_FIELDS = [];
			for(var i in ORDER_LIST_COLS_INFO){
				var arr = ORDER_LIST_COLS_INFO[i].split('&');
				ORDER_LIST_FIELDS.push(arr[0]);
			}
			//初始化HTML
			me.initHtml();
			//监听事件
			me.initListeners();
		});
		return me;
	};
	//暴露接口
	exports(MOD_NAME,PreSampleTranRuleIndex);
});