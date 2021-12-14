/**
 * @name：modules/pre/sample/inspect/index 样本送检
 * @author：liangyl
 * @version 2020-09-23
 */
layui.extend({
	uxutil:'ux/util',
	uxbase: 'ux/base',
    CommonSelectUser: 'modules/common/select/preuser',
	PreSampleContentIndex:'modules/pre/sample/inspect/content',
	SearchBar:'modules/pre/sample/inspect/search'
}).define(['uxutil','form','uxbase','PreSampleContentIndex','PreSampleInspectBasicParams','SearchBar','CommonSelectUser'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		table = layui.table,
		PreSampleContentIndex = layui.PreSampleContentIndex,
		PreSampleInspectBasicParams = layui.PreSampleInspectBasicParams,
		SearchBar = layui.SearchBar,
		CommonSelectUser = layui.CommonSelectUser,
		MOD_NAME = 'PreSampleInspectIndex';
	
	//列表字段：格式=BarCode&条码号&100&show,OrderExecTime&医嘱指定执行时间&100&show,
	var LIST_COLS_INFO = null;
	//后台获取字段数组
	var LIST_FIELDS = null;
	//扫码样本数据_更新样本状态 (样本送检_根据条码号获取样本数据_更新样本状态)
	var BARCODE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleExchangeInspectAndUpdateStateByBarCode";
	//根据护工号获取身份信息（护工或护士）
	var GET_EMP_INFO_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampledeliveryGetEmpInfo";
	//登录服务
	var LOGIN_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//强制更新（确认送检按钮）
	var UPDATE_STATE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode";
		//获取账户服务路径
	var GET_USER_ACCOUNT_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true";

	//送检信息特定字段匹配数据临时存储（第一次扫码的字段内容）
	var FILED_MATE = '';
	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
			   '<div class="layui-inline"> ',
                    '<label class="layui-form-label">条码号:</label> ',
					'<div class="layui-input-inline" style="width:185px;">',
					    '<input type="text" name="{domId}-barCode" id="{domId}-barCode"  placeholder="请扫描条码" autocomplete="off" class="layui-input" />',
					'</div>',
				'</div>',
				'<div class="layui-inline">', 
                    '<label class="layui-form-label">送检人:</label>', 
				    '<div class="layui-input-inline">',
						'<input type="text" name="{domId}-userName" id="{domId}-userName" readonly="readonly" autocomplete="off" class="layui-input" />',
				    '</div>',
				'</div>',
				'<div class="layui-inline layui-hide" id="{domId}-transport-userName">', 
                    '<label class="layui-form-label">运送人:</label>', 
				    '<div class="layui-input-inline" id="{domId}-transport-user-com">',
				    '</div>',
				'</div>',
				'<div class="layui-inline layui-hide" id="{domId}-transport-record" style="padding-left: 10px;">', 
                   '<input type="checkbox" name="like1[write]" lay-skin="primary" title="记录运送人员">',
				'</div>',
				 '<div class="layui-inline" style="float:right;">',
                  '<input type="checkbox" id="{domId}-show-search-toolbar" lay-filter="{domId}-show-search-toolbar" title="查询" lay-skin="primary" />',  
				'</div>',
				'<div class="layui-inline" style="float: right;">', 
                    '<label id="{domId}-num" class="layui-form-label" style="color: blue;font-size: 18px;font-weight: bold ;width:120px;">数量:0</label>', 
				'</div>',
			'</div>',
		'</div>',
		'<div class="{domId}-grid-div layui-hide" id="{domId}-SearchTool"></div>',
		'<div class="layui-row" style="margin:0px;padding:0px;">',
		    '<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12" id="{domId}-Content-row">',
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
	var PreSampleInspectBasicParamsInstance = null;
	//查询工具栏
	var SearchBarInstance = null;
	
	var PreSampleInspectIndex = {
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:null, //站点类型
			MODELTYPE:'1' //模式
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleInspectIndex.config,setings);
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
		//查询工具
		SearchBarInstance = SearchBar.render({
			domId: me.config.domId+'-SearchTool',
			height:'30px',
			//是否查询默认科室
			IsDefaultDept:PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0011'),
			//送检查询字段
			DateType:PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0024'),
			MODELTYPE:me.config.MODELTYPE,
			nodetype:me.config.nodetype, //站点类型
			//查询未送检按钮点击触发事件
			notInspectionClickFun:function(obj){
			    me.clearData();
				if(!obj.notwhere){
					layer.msg('未送检查询条件不能为空',{icon:5});
					return false;
				}
				PreSampleContentIndexInstance.onSearch(obj.notwhere,'',function(){
					 //数量显示
    	            $("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
    	            me.Size();
				});
			},
 			//查询已送检按钮点击触发事件
			inspectionClickFun:function(obj){
				me.clearData();
				if(!obj.where && !obj.userID){
					layer.msg('已送检查询条件不能为空',{icon:5});
					return false;
				}
				var relationForm = '';
				if(obj.userID){
					relationForm ='LisOperate lisoperate';
					obj.where += ' and ' + obj.relationForm;
				}
				PreSampleContentIndexInstance.onSearch(obj.where,relationForm,function(){
					 //数量显示
    	            $("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
    	            me.Size();
				});
			}
		});
		var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height()-$("#"+me.config.domId+'-SearchTool').height()-55;

       //根据送检信息特定字段匹配数据是否加入列表
        var fields = me.getMateField();
        var cols = [],cols_info = LIST_FIELDS.join(',');
        for(var i in fields){
        	if(cols_info.indexOf(fields[i]) == -1){  //如果不存在需要匹配的特定字段，则加入
//      		LisBarCodeFormVo_LisBarCodeForm_BarCode&条码号&100&show,
        		LIST_COLS_INFO.push(fields[i]+'&'+fields[i]+'&100&hide');
        	}
        }
		//样本信息列表实例 
		PreSampleContentIndexInstance = PreSampleContentIndex.render({
			domId: me.config.domId+'-Content',
			height:height,
			cols:LIST_COLS_INFO,
			MODELTYPE:me.config.MODELTYPE,
			PrinterName:PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0023'),
			IsOrderListPrinter:PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0022'),//是否打印附加清单
			nodetype:me.config.nodetype, //站点类型
			//刷新
			refresh:function(){
				me.clearData();
				var obj = SearchBarInstance.getWhere();
				if(!obj.notwhere){
					layer.msg('未送检查询条件不能为空',{icon:5});
					return false;
				}
				PreSampleContentIndexInstance.onSearch(obj.notwhere,'',function(){
					 //数量显示
    	            $("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
				});
			}
		});
	};
	//初始化参数设置HTML
	Class.prototype.initParamsHtml = function(){
		var me = this;
		//是否记录护送人员   
		var showtransport = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0003');
		//显示护送人
		if(showtransport=='1')$('#' + me.config.domId+'-transport-userName').removeClass('layui-hide');
		
        //手动控制护送人员(是否显示记录运送人员复选框)
		var showtransportcheckbox = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0021');
        //显示记录运送人员复选框
        if(showtransportcheckbox=='1' && showtransport=='1')$('#' + me.config.domId+'-transport-record').removeClass('layui-hide');
		//运送人显示方式(组件显示方式文本或者下拉)
		var comtype = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0013'); 
		//是否允许手工录入护工
		var edittransport = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0015');
        //只读html
        var readonlyHtml = 'readonly="readonly"';
        if(edittransport=='1')readonlyHtml='';
		//运送人显示方式默认显示文本
		var comHtml = '<input type="text" name="'+me.config.domId+'-userName2" id="'+me.config.domId+'-userName2" '+readonlyHtml+' data-value="" autocomplete="off" class="layui-input" />';
		if(comtype=='1'){//组件类型为下拉
			//只读html
	        var readonlyHtml = 'disabled="disabled"';
	        if(edittransport=='1')readonlyHtml='';
			comHtml =  '<select name="'+me.config.domId+'-userName2" id="'+me.config.domId+'-userName2" lay-filter="'+me.config.domId+'-userName2" '+readonlyHtml+' >',
					'<option value="">请选择</option>',
				'</select>';
		   //运送人
			CommonSelectUser.render({
				domId:me.config.domId+'-userName2',
				code:[1001002, 1001004],
				done:function(){
					
				}
			});
		}
		$('#' + me.config.domId+'-transport-user-com').append(comHtml); 
		//送检人显示
		var userName = uxutil.cookie.get(uxutil.cookie.map.USERNAME);
		$('#' + me.config.domId+'-userName').val(userName);
		form.render();
		
	};
	//是否是条码号
	Class.prototype.isBarcode = function (value) {
		var me = this,
		    isHG = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0025'),//使用非HG护工号
			BarCodeMinLength = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0026');//条码号最小位数	      
		value = String(value).trim() || null;
        //存在HG 则为护工号
		if (value.toUpperCase().indexOf('HG') != -1) return false;
        //使用非HG护工号  ==1说明护工号可能不带hg，需要根据lis条码最小位数判断，判断条件，小于条码最小位数的为护工号，如果lis条码号最小位数==0或者等于空 默认10 
        if(isHG=='1'){
        	//如果lis条码号最小位数==0或者等于空 默认10
        	if(!BarCodeMinLength || BarCodeMinLength=='0' ) BarCodeMinLength=10;
			if (value.length < Number(BarCodeMinLength))return false;
        }
		return true;
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		$("#"+me.config.domId+"-barCode").focus();
		//扫码,回车事件
	    $("#"+me.config.domId+"-barCode").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	var barcode = $("#"+me.config.domId+"-barCode").val();
	        	//判断条码号是否已扫码
	        	var isExist = PreSampleContentIndexInstance.isExistBarcode(barcode);
	        	if(isExist){
	        		//清空扫码输入框数据
			    	$("#"+me.config.domId+"-barCode").val('');
			        $("#"+me.config.domId+"-barCode").focus();
	        		return false;
	        	}
	        	if(barcode){
		        	//是否记录护送人员
			        var showtransport = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0003');
		            //1.如果开启记录护送人员(1)  只要带hg 的都当护工号处理
		            if(showtransport=='1'){
		            	//是否是条码
		            	var isbarcode = me.isBarcode(barcode);
		            	if(isbarcode){//条码
		            		me.onBarCode(barcode);
		            	}else{ //护工，需根据护工号获取身份信息
		            		//转成大写
					        barcode = barcode.toUpperCase();
							barcode = String(barcode).trim() || null;
		            		me.getHGByCode(barcode,function(list){
		            			if (list && list.length > 0) {
									me.setDeliveryer(list);
								} else {
									layer.msg("未查询到护工信息!", { icon: 0, anim: 0 });
								}
								//清空扫码输入框数据
						    	$("#"+me.config.domId+"-barCode").val('');
						        $("#"+me.config.domId+"-barCode").focus();
		            		});
		            	}
		            }else{
		            	me.onBarCode(barcode);
		            }
		        }else{
		        	layer.msg('条码号不能为空,请扫码!',{icon:5});
		        }
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
    	//身份验证确认按钮
		form.on('submit(idenyity)', function (data) {
			var account = data.field["account"],
				password = data.field["password"];
			var loadIndex = layer.load();
			//请求登入接口
			uxutil.server.ajax({
				url:LOGIN_URL,
				cache:false,
				data:{
					strUserAccount:account,
					strPassWord:password
				}
			}, function (data) {
				layer.closeAll('loading');
				if(data === true){
				   me.bataAccept();
				   layer.closeAll();
				}else{
					layer.msg('密码错误！');
					$('#account').focus();
				}
			});
		});
	
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
	Class.prototype.Size = function(){
		var me = this;
		 var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height()-$("#"+me.config.domId+'-SearchTool').height()-55;
        var bo = $('#'+me.config.domId+'-show-search-toolbar').prop('checked');
        if(bo)height = height-5;
        $("#"+me.config.domId+"-Content-row").css('height',height+'px');
        PreSampleContentIndexInstance.changeSize(height,bo);
	};

	//获取运送人
	Class.prototype.getTransportUser = function(){
		var me = this,
		    userid ='',
		    username = '';
		//是否记录护送人员   
		var showtransport = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0003');
		//手动控制护送人员(是否显示记录运送人员复选框)
		var showtransportcheckbox = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0021');
        //运送人显示方式(组件显示方式文本或者下拉)
		var comtype = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0013'); 
        //是否允许手工录入护工
		var edittransport = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0015');
  
         //显示记录运送人员复选框
        if(showtransportcheckbox=='1' && showtransport=='1'){
        	if(comtype=='1'){ //下拉
        		userid = $("#" + me.config.domId + "-userName2").val();
			    username = $("#" + me.config.domId + "-userName2 option:selected").text();
			    if(!userid)username="";
        	}else{//文本
        		if(edittransport=='1'){//文本可编辑时，id=0
        			userid =  0;
        		    username = $("#"+me.config.domId+'-userName2').val();
        		}else{
        			userid =  $('#'+me.config.domId+'-userID2').val();
        		    username = $("#"+me.config.domId+'-userName2').val();
        		}
        	}
        }
        return {userid:userid,username:username};
	};
	//根据护工号获得护工具体信息
	Class.prototype.getHGByCode = function (value,callback) {
		var me = this,
			value = value,
			load = layer.load(),
			config = {
				type: "POST",
				url: GET_EMP_INFO_LIST_URL,
				data: JSON.stringify({ cardno: value })
			};
		uxutil.server.ajax(config, function (data) {
			//隐藏遮罩层
			layer.close(load);
			if (data.success) {
				callback(data.value);
			} else {
				layer.msg(data.ErrorInfo || "获取护工信息失败!", { icon: 5, anim:0 });
			}
		})
	}
	//设置运送人
	Class.prototype.setDeliveryer = function (list) {
		var me = this;
		//是否记录护送人员   
		var showtransport = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0003');
        //运送人显示方式(组件显示方式文本或者下拉)
		var comtype = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0013'); 
        $("#" + me.config.domId + "-barcode").val("");//清空条码号框
        if(comtype=='1'){//下拉
        	$("#" + me.config.domId + "-userName2").val(list[0]["Id"]);
        	form.render('select');
        }else{//文本
        	$("#" + me.config.domId + "-userName2").attr("data-value", list[0]["Id"]);
		    $("#" + me.config.domId + "-userName2").val(list[0]["CName"]);
        }
	};
	//清空运送人
	Class.prototype.clearDeliveryer = function () {
		var me = this;
		$("#" + me.config.domId + "-userName2").attr("data-value", "");
		$("#" + me.config.domId + "-userName2").val("");
	};
	/**根据条码号获取样本数据_更新样本状态
     * barcodes 条码号字符串，多个使用英文逗号分割
     * isupdate 是否更新
     * */
	Class.prototype.getListByBarCode = function(barcodes,isupdate,callback){
		var me = this;
		var params ={
			nodetype:me.config.nodetype,//站点类型
			barcodes:barcodes,//条码集合，多个用逗号分开
			isupdate:isupdate,
			fields:PreSampleContentIndexInstance.getFields(),
		    isPlanish:true 
		};
		var transport = me.getTransportUser();
	    if(transport.userid || transport.username){ //记录运送人
	    	params.userid = transport.userid;
	    	params.username = transport.username;
	    }
		var config = {
			type:'post',
			url:BARCODE_LIST_URL,
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
	//扫码处理(按模式处理)
	Class.prototype.onBarCode = function(barcodes){
		var me = this;
		if(!barcodes){
			layer.msg("请扫码！",{icon:5});
			return false;
		}
		//按模式处理   模式1 isupdate = true  模式2,3,4 isupdate  = false；
		var isupdate = false;
		if(me.config.MODELTYPE =='1')isupdate = true;
		//判断条码是否已存在
		var isExec = PreSampleContentIndexInstance.isExistBarcode();
		if(isExec){
			//清空扫码输入框数据
	    	$("#"+me.config.domId+"-barCode").val('');
	        $("#"+me.config.domId+"-barCode").focus();
			return false;
		}
		//模式3需要匹配
        if(me.config.MODELTYPE == '3'){
        	//必须先有病人信息才能匹配
    		if(PreSampleContentIndexInstance.getMateList().length==0){
    			layer.msg('请先查询未送检的样本!',{icon:5});
    			return false;
    		}
    		//先判断右侧的列表存不存在
        	var isMate = me.isMateData(barcodes);
        	if(!isMate) {
        		layer.msg('没有找到匹配的样本!',{icon:5});
        		return false;
        	}
        }
		//根据送检信息特定字段匹配数据是否加入列表
        var Field = me.getMateField();
        //按第一个刷入条码的字段内容为准，后面的条码如果信息不一样，直接提示，并且不能进入后续操作（后续数据与第一条数据的某个字段进行匹配如果一样则可以继续流程，不一样就提示并禁止后续操作）
		if(Field.length>0 && PreSampleContentIndexInstance.getList().length>0 && FILED_MATE){
			if(me.config.MODELTYPE =='1')isupdate = false;
			me.ScanBarCode(barcodes,isupdate,function(list){
				//是否匹配，匹配则能加入
				var isExec2 = PreSampleContentIndexInstance.isSpecificField(Field,FILED_MATE,list);
			    if(!isExec2){ //匹配不上 提示并且不能进入后续操作
			    	layer.msg("与设置的送检信息特定字段数据不匹配,不能送检！",{icon:5});
				    return false;
			    }else{ //匹配后加入列表 
			    	isupdate = true;
			    	me.ScanBarCode(barcodes,isupdate,function(list){
			    		me.BarCodeList(list);
			    	});
			    }
			});
		}else{ //不需要根据送检信息特定字段匹配数据是否加入列表
			me.ScanBarCode(barcodes,isupdate,function(list){
				me.BarCodeList(list);
			});
		}
	};
	//扫码返回数据新增处理
	Class.prototype.BarCodeList = function(list){
		var me = this;
		var arr = list[0].LisBarCodeFormVo_failureInfo;
		if(arr)arr = JSON.parse(arr);
		if(arr.length==0){//没有需要再次确认的数据
			//数据已被确认送检,打上已确认送检标记 
			if(me.config.MODELTYPE =='1'){
				list[0].LisBarCodeFormVo_IsConfirm = '1';
				list[0].LAY_CHECKED='true';
		        layer.msg('送检确认成功',{icon:6});
			}
			list[0].LAY_CHECKED='true';
			//新增行数据
		    me.addRow(list);
		}else{ //需再次确认
			//是否强制更新
			var isConstraintUpdate = false;
			//是否提示
			var isTip = false;
			if(me.config.MODELTYPE=='1'){
				isConstraintUpdate = true;
				isTip = false;
			}
			me.onVerifyHandle(isConstraintUpdate,list,function(barcodelist,TipMsg,alterMode){
			    if (TipMsg.length > 0)
			       //清空扫码输入框数据
                $("#"+me.config.domId+"-barCode").blur()
                if(TipMsg.length>0){
               	    layer.alert(TipMsg.join('<br>'), { icon: 0, anim: 0 },function(index){
			       		//清空扫码输入框数据
				    	$("#"+me.config.domId+"-barCode").val('');
				        $("#"+me.config.domId+"-barCode").focus();
				        layer.close(index);
			        });
                }
			    if(barcodelist.length>0){
                    var bo = TipMsg.length > 0 ? true : false;
			    	me.addRow(barcodelist,bo);
			    }
    		});
		}
	};
	//扫码实现
	Class.prototype.ScanBarCode = function(barcodes,isupdate,callback){
		var me = this;
			//扫码操作
		me.getListByBarCode(barcodes,isupdate,function(list){
			if(list.length==0){
    			layer.msg('未找到该条码信息!',{icon:5});
    			return false;
    		}
			callback(list);
		});
	};

	 /**样本送检_根据条码号更新送检状态*/
	Class.prototype.updateSampleState = function(barcodes,isConstraintUpdate,callback){
		var me = this;
	    var params ={
			nodetype:me.config.nodetype,//站点类型
			barcodes:barcodes,//条码集合，多个用逗号分开
			isConstraintUpdate:isConstraintUpdate
		};
		var transport = me.getTransportUser();
	    if(transport.userid){ //记录运送人
	    	params.userid = transport.userid;
	    	params.username = transport.username;
	    }
		var config = {
			type:'post',
			async:false,
			url:UPDATE_STATE_URL,
			data:JSON.stringify(params)
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value ||{}).list || []);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	//参数配置的提示方式处理
	Class.prototype.onFailureInfoHandle = function (list,isConstraintUpdate, callback) {
		var me = this,
			list = JSON.parse(JSON.stringify(list)),
			isOut = false,//是否是等待确认框执行
			pendingData = [],//暂时存储数据集合 等待再次发送服务数据返回后一起处理
			needUpdateBarcode = [],//需要再次发送服务条码号集合
			resultList = [];//{ data: null, isSuccess: true, tipList: [] };//data:当前数据，isSuccess：是否成功,tipList:提示信息集合
		$.each(list, function (i, item) {
			//自主选择-- 不允许
			if (item["IsNotAllow"] && item["IsNotAllow"] == 1) {
				resultList.push({ data: item, isSuccess: false, tipList: [item["NotAllowInfo"]] });
				//不需要再次发送服务的数据存储
				if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1) pendingData.push(item);
				return true;
			}
			resultList.push({ data: item, isSuccess: true, tipList: [] });
			var failureInfoArr = item["LisBarCodeFormVo_failureInfo"] ? JSON.parse(item["LisBarCodeFormVo_failureInfo"]) : [];
			$.each(failureInfoArr, function (j, itemJ) {
				if (isOut) return false;
				var alterMode = itemJ["alterMode"],
					failureInfo = itemJ["failureInfo"];
				switch (String(alterMode)) {
					case "4"://用户自行选择
						isOut = true;
						var msginfo = "条码号为：" + item["LisBarCodeFormVo_LisBarCodeForm_BarCode"] + "，" + failureInfo + ",是否允许操作？";
	                	uxbase.MSG.onConfirm(msginfo,{ icon: 3, title: '提示',enter:true},
		                	function(index){
		                		isOut = false;
								failureInfoArr[j]["alterMode"] = -1;
								list[i]["LisBarCodeFormVo_failureInfo"] = JSON.stringify(failureInfoArr);
								me.onFailureInfoHandle(list,isConstraintUpdate, callback);
								//这里可以写需要处理的流程
								layer.close(index);    //执行完后关闭
		                	},
		                	function(index){
		                		isOut = false;
								list[i]["IsNotAllow"] = 1;
								list[i]["NotAllowInfo"] =  failureInfo+"用户操作不允许送检!";
								me.onFailureInfoHandle(list,isConstraintUpdate, callback);
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
						if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1)
							needUpdateBarcode.push(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]);
						break;
					default:
						break;
				}

			});
			//不需要再次发送服务的数据存储
			if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1) pendingData.push(item);

			if (isOut) return false;
		});
		//存在需要再次更新数据
		if (!isOut && needUpdateBarcode.length > 0) {
			me.updateSampleState(needUpdateBarcode.join(), isConstraintUpdate, function (data) {
				me.onFailureInfoHandle(data.concat(pendingData),isConstraintUpdate, callback);
			});
		}
		//执行完成
		if (!isOut && needUpdateBarcode.length == 0) callback && callback(resultList);

	};

	//校验
	Class.prototype.onVerifyHandle = function(isConstraintUpdate,list,callback){
		var me = this;
        var TipMsg = [],alterMode="";
		if(list.length==0){
    		layer.msg('未找到该条码信息!',{icon:5});
			return false;
	    }
		//提示信息处理
		me.onFailureInfoHandle(list,isConstraintUpdate, function (resultList) {
			var Msg = [],addlist=[];//弹出信息
			$.each(resultList, function (i, item) {
				var data = item["data"],
					tipList = item["tipList"];
				if (item["isSuccess"]) {
					$.each(list, function (a, itemA) {
						if (data.LisBarCodeForm && data.LisBarCodeForm.BarCode == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
							list[a]["LAY_CHECKED"] = true;
							itemA.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID = data.LisBarCodeForm.BarCodeStatusID;
							var BarCodeStatusID =  data.LisBarCodeForm.BarCodeStatusID ?  data.LisBarCodeForm.BarCodeStatusID  : 0;
							itemA.LisBarCodeFormVo_IsConfirm = Number(BarCodeStatusID) > 3 ? '1' : '';
							itemA.LisBarCodeFormVo_LisBarCodeForm_CollectTime= data.LisBarCodeForm.CollectTime;
                            addlist.push(itemA);
                        }else if(data.LisBarCodeFormVo_LisBarCodeForm_BarCode && data.LisBarCodeFormVo_LisBarCodeForm_BarCode == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]){
                        	data.LAY_CHECKED = true;
                        	var BarCodeStatusID =   data.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID ?   data.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID  : 0;
							data.LisBarCodeFormVo_IsConfirm = Number(BarCodeStatusID) > 3 ? '1' : '';
							data.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID =  data.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID;
               			    data.LisBarCodeFormVo_LisBarCodeForm_CollectTime =  data.LisBarCodeFormVo_LisBarCodeForm_CollectTime;
                            addlist.push(data);
                        }
					});
				} 
				//弹出信息处理
				if (tipList.length > 0) {
					alterMode = item["alterModeList"];
					$.each(tipList, function (k, itemK) {
						TipMsg.push("条码号为:" + itemK);
					});
				}
			});
		    callback(addlist,TipMsg,alterMode);
		});
	};
	Class.prototype.showTip= function(resultList,isConstraintUpdate,callback){
		var me = this;
		var barcodelist = [],Msg=[],isSuccess=false,alterMode="";//找到已确认拒收的条码
        $.each(resultList, function (i, item) {
			var data = item["data"],
				tipList = item["tipList"];
			if(item["isSuccess"]) {
				data.LAY_CHECKED = 'true';
				data.LisBarCodeFormVo_IsConfirm = '1'; //已确认送检标识
				isSuccess = true;//提示图标
				barcodelist.push(data);
				if(tipList.length==0)tipList = [data.LisBarCodeFormVo_LisBarCodeForm_BarCode+"确认送检成功"];
			}
			//弹出信息处理
			if (tipList.length > 0 && isConstraintUpdate) {
				alterMode = item["alterModeList"];
				$.each(tipList, function (k, itemK) {
					Msg.push("条码号为:" + itemK);
				});
			}
		});
		callback(Msg,barcodelist,alterMode);
	};
	//根据用户Id,找到用户账号
	Class.prototype.getAccountByUserId = function(UserID,callback){
		var me = this;
	    var loadIndex =layer.load();
		var url = GET_USER_ACCOUNT_URL + '&fields=RBACUser_Account&where=rbacuser.HREmployee.Id=' + UserID;
		uxutil.server.ajax({
			url:url
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
	//获取匹配字段,返回字段数组
	Class.prototype.getMateField = function(){
		var me = this,fields=[],
		    Field = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0017');
		var arr = Field.split(',');
		for(var i in arr){
			var arr2 = arr[i].split('&');
            if(arr2.length>0)fields.push(arr2[0]);
		}
        return fields;
	};
	//列表列是否已配置特定字段匹配数据列（根据送检信息特定字段匹配数据)
	Class.prototype.isMateField = function(){
		var me = this;
		var isExec=true;
		//根据送检信息特定字段匹配数据是否加入列表
		var fields = me.getMateField();
		var str1 = LIST_FIELDS.join(',');
        if(fields.length==0)return isExec;
        for(var i=0;i<fields.length;i++){
        	if(str1.indexOf(fields[i]) != -1){
        		isExec= false;
        		return isExec;
        	}
        }
        return isExec;
	};
	//打印清单
    Class.prototype.onListPrint = function(){
    	var me = this;
    	PreSampleContentIndexInstance.onListPrint();
    };
    //新增行  list新增的数据行（如果已确认需打上确认标记再传进来）
     Class.prototype.addRow = function(list,bo){
     	var me = this;
    	//新增数据行
		PreSampleContentIndexInstance.loadData(list);
		//模式3 需要刷新匹配列表
		if(me.config.MODELTYPE =='3')PreSampleContentIndexInstance.findBarCode(list[0].LisBarCodeFormVo_LisBarCodeForm_BarCode);
		if(!bo){
			//清空扫码输入框数据
	    	$("#"+me.config.domId+"-barCode").val('');
	        $("#"+me.config.domId+"-barCode").focus();
		}
		
        //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
    	me.Size();
    	//FILED_MATE 临时存储,记录第一行特定字段值
    	//根据送检信息特定字段匹配数据是否加入列表
    	var Field = me.getMateField();
        if(Field.length>0 && PreSampleContentIndexInstance.getList().length==1){
        	for(var i=0;i<Field.length;i++){
        		FILED_MATE += list[0][Field[i]];//如果存在多个特定字段匹配则是多字字段拼接值
        	}
        }
	};
    //验证身份
	Class.prototype.onVerifiySign = function () {
		var me = this;
		layer.open({
			type: 1,
			skin: 'layui-layer-rim', //加上边框
			area: '300px', //宽高
			content: IDENYITY_DOM,
			success: function (layero, index) {
				var userId =uxutil.cookie.get(uxutil.cookie.map.USERID);
				//默认值（系统当前登录者）
				if(userId){
                    me.getAccountByUserId(userId,function(list){
			           var account = "";
			           if(list.length>0)account = list[0].RBACUser_Account;
			      	   $('#account').val(account);
			        });
				}
			}
		});
	};
    //确认送检
    Class.prototype.onAccept = function(){
     	var me = this;
    	var barcodes = PreSampleContentIndexInstance.getCheckedBarcodes();
    	if(barcodes.length==0){
    		layer.msg('请勾选要送检确认的数行!',{icon:5});
    		return false;
    	}
    	//是否批量确认模式验证采集人
    	var isVerifiy = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0006');
        if(isVerifiy=='1'){//批量确认模式验证采集人
        	me.onVerifiySign();
        }else{
            me.bataAccept();
        }
     };
    //批量确认 
    Class.prototype.bataAccept = function(){
    	var me = this;
    	var CheckedList = PreSampleContentIndexInstance.getCheckedList();
    	var barcodes = PreSampleContentIndexInstance.getCheckedBarcodes();
    	if(barcodes.length==0){
    		layer.msg('请勾选要送检确认的数行!',{icon:5});
    		return false;
    	}
    	var barcode = barcodes.join(',');
    	//是否开启送检校验
		var ischeck = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0016');
        if(ischeck=='0'){ //未开启送检校验
        	//列表刷新--待确认
	    	me.updateSampleState(barcode,true,function(data){
	    		//已采集标记
	    		for(var i=0;i<CheckedList.length;i++){
	    			CheckedList[i].LisBarCodeFormVo_IsConfirm = '1';
	    		}
	        	PreSampleContentIndexInstance.loadData(CheckedList);
	    	    layer.msg("确认送检成功",{icon:6,time:2000});
	        });
        }else{
        	me.updateSampleState(barcode,false,function(list){
        		var arr = [];
        		for(var j=0;j<CheckedList.length;j++){
	    			for(var i=0;i<list.length;i++){
	        			var BarCode = list[i].LisBarCodeForm.BarCode;
	                    var BarCodeStatusID = list[i].LisBarCodeForm.BarCodeStatusID;
			    		if(CheckedList[j].LisBarCodeFormVo_LisBarCodeForm_BarCode==BarCode ){
	    				    CheckedList[j].LisBarCodeFormVo_failureInfo = list[i].failureInfo;
	    				    if(list[i].failureInfo.length==0){
                               arr.push(CheckedList[j]);
		    				}
	    		        }
		    		}
	    		}
        		//不需要验证数据 ，全都已经送检成功
        		if(CheckedList.length == arr.length){
        			me.addRow(CheckedList);
        			layer.msg("确认送检成功",{icon:6,time:2000});
        			return false;
        		}
        		var isConstraintUpdate = true;//强制更新
	            me.onVerifyHandle(isConstraintUpdate,CheckedList,function(barcodelist,TipMsg,alterMode){
				    //清空扫码输入框数据
	                $("#"+me.config.domId+"-barCode").blur();
                    if(TipMsg.length>0){
	               	    layer.alert(TipMsg.join('<br>'), { icon: 0, anim: 0 },function(index){
				       		//清空扫码输入框数据
					    	$("#"+me.config.domId+"-barCode").val('');
					        $("#"+me.config.domId+"-barCode").focus();
					        layer.close(index);
				        });
	                }
				    if(barcodelist.length>0){
	                    var bo = TipMsg.length > 0 ? true : false;
				    	me.addRow(barcodelist,bo);
				    	layer.msg("确认送检成功",{icon:6,time:2000});
				    }
				   
	    		});
        	});
    		
        }
    };
  
    //数据清空
    Class.prototype.clearData = function(){
    	var me = this;
    	FILED_MATE = "";
    	PreSampleContentIndexInstance.clearData();
    	 //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
        me.Size();
    };
    //条码号与查询到的样本单匹配
    Class.prototype.isMateData= function(BarCode){
    	var me = this;
    	var isExec = false;
		var list = PreSampleContentIndexInstance.getMateList();
	    for(var i=0;i<list.length;i++){
	    	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == BarCode ){
	    		isExec =true;
	    		break;
	    	}
	    }
	    return isExec;
	};
	//核心入口
	PreSampleInspectIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//参数功能
	    PreSampleInspectBasicParamsInstance = PreSampleInspectBasicParams.render({nodetype:me.config.nodetype});
		//初始化功能参数
		PreSampleInspectBasicParamsInstance.init(function(){
			//列表字段
			LIST_COLS_INFO = PreSampleInspectBasicParamsInstance.get('Pre_OrderExchangeInspect_DefaultPara_0027').split(',') || [];
			LIST_FIELDS = [];
			for(var i in LIST_COLS_INFO){
				var arr = LIST_COLS_INFO[i].split('&');
				LIST_FIELDS.push(arr[0]);
			}
			//初始化HTML
			me.initHtml();
			//监听事件
			me.initListeners();
		});
		return me;
	};
		//返回撤销成功的条码号
	function afterUpateInspectRevocation(data, barCode){
		//被撤销确认的条码号
		if (data && data[0]) {
			var obj = JSON.parse(data[0]);
			if (obj[barCode] == 'true') {
				layer.msg("撤销送检成功", { icon: 6, time: 2000 });
				PreSampleContentIndexInstance.onRevocation(barCode);
			} else if (obj[barCode] == 'false'){
				layer.alert("以下条码撤销失败，请检查样本状态：" + BarCodeConfirmError);
			}
		}
	}
	window.afterUpateInspectRevocation = afterUpateInspectRevocation;
	//暴露接口
	exports(MOD_NAME,PreSampleInspectIndex);
});