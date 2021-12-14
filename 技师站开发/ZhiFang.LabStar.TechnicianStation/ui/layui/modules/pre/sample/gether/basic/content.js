/**
 * @name：modules/pre/sample/gether/index 样本采集
 * @author：liangyl
 * @version 2020-09-23
 */
layui.extend({
	uxutil:'ux/util',
	uxbase: 'ux/base',
	BarCodeFormList:'modules/pre/sample/gether/basic/list',
	MateList:'modules/pre/sample/gether/basic/matelist',
}).define(['uxutil','form','uxbase','BarCodeFormList','MateList'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		uxbase = layui.uxbase,
		table = layui.table,
		BarCodeFormList = layui.BarCodeFormList,
		MateList = layui.MateList,
		MOD_NAME = 'PreSampleContentIndex';
		
    	//登录服务
	var LOGIN_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_BA_Login';
	//获取账户服务路径
	var GET_USER_ACCOUNT_URL = uxutil.path.LIIP_ROOT + "/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?isPlanish=true";
	//样本采集_根据条码号获取样本数据_更新样本状态
	var BARCODE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleGatherAndUpdateStateByBarCode";
	//样本采集_根据条码号更新采集状态
	var UPDATE_GATHERSTATE_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreUpdateSampleGatherStateByBarCode";
      //打包清单服务
    var COLLECT_PACK_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleGatherCreateCollectPackNoByBarCode";
    //样本采集_根据条码号获取样本数据_更新样本状态  (参数特定核收字段名核收)
	var GET_PARAMS_HESHOU_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere";
  
	//模块DOM
	var MOD_DOM = [
	    '<div class="layui-row style="margin:0px;padding:0px;">',
			'<div class="layui-col-xs8" id="{domId}-col-BarCodeForm">',
                '<div id="{domId}-BarCodeForm"></div>',
			'</div>',
			'<div class="layui-col-xs4" id="{domId}-col-BarCodeForm-Mate">',
                '<div id="{domId}-BarCodeForm-Mate"></div>',
			'</div>',
		'</div>'
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
	var BarCodeFormListInstance = null;
	//样本单匹配列表实例
	var MateListInstance = null;
	var PreSampleContentIndex = {
		//对外参数
		config:{
			domId:null,
			height:null,
			nodetype:null,
			cols:[],
			fields:"LisBarCodeFormVo_LisBarCodeForm_BarCode,LisBarCodeFormVo_LisBarCodeForm_LisPatient_CName,LisBarCodeFormVo_LisBarCodeForm_IsUrgent,LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID",
			MODELTYPE:1,//模式1
			//是否自动打印采样清单
			IsAutoPrint:0,
			//是否批量确认采集人
			IsConfirmUser:0,
			PrinterName:null,//采集清单打印机
			IsPreview:1 //是否预览，1是默认预览  0就是不预览	
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleContentIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		
		//样本信息列表实例 
		BarCodeFormListInstance = BarCodeFormList.render({
			domId: me.config.domId+'-BarCodeForm',
			height:me.config.height+'px',
			cols:me.config.cols,
			nodetype:me.config.nodetype //站点类型
		});
		//模式2,模式5 -显示匹配列表
		if(me.config.MODELTYPE !='2' && me.config.MODELTYPE !='5'){
			//隐藏右列表
			$('#' + me.config.domId+'-col-BarCodeForm-Mate').removeClass('layui-col-xs4');
			$('#' + me.config.domId+'-col-BarCodeForm-Mate').addClass('layui-col-xs12');
			$('#' + me.config.domId+'-col-BarCodeForm-Mate').addClass('layui-hide');
		    //左列表宽度修改
		    $('#' + me.config.domId+'-col-BarCodeForm').removeClass('layui-col-xs8');
			$('#' + me.config.domId+'-col-BarCodeForm').addClass('layui-col-xs12');
		}else{
			MateListInstance = MateList.render({
				domId: me.config.domId+'-BarCodeForm-Mate',
				height:me.config.height+'px',
				nodetype:me.config.nodetype //站点类型
			});
		}
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
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
				   me.acceptExec();
				   layer.closeAll();
				}else{
					layer.msg('密码错误！');
					$('#account').focus();
				}
			});
		});
	};
	//列表列
	Class.prototype.getCols = function(){
		var me = this;
		var cols = [
			{type: 'checkbox', fixed: 'left'},
			{field:'LisBarCodeFormVo_IsConfirm', width:80, title: '是否已确认', hide: false},
			{field:'LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID', width:80, title: '状态', hide: false},
			{field:'LisBarCodeFormVo_failureInfo', width:80, title: 'LisBarCodeFormVo_failureInfo', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_ColorValue', width:80, title: 'LisBarCodeFormVo_LisBarCodeForm_ColorValue', hide: true}];
		for(var i in me.config.cols){
			//BarCode&条码号&100&show
			var arr = me.config.cols[i].split('&');
			cols.push({
				field:arr[0],title:arr[1],width:arr[2],
				hide:(arr[3] =='show' ? false : true)
			});
		}
		return cols;
	};
	//查询样本单列表字段
	Class.prototype.getFields = function(){
		return BarCodeFormListInstance.getFields();
	};
    Class.prototype.changeSize= function(height){
    	var me = this;
    	//模式2,模式5 -需改变两个列表高度（匹配列表高度也需调整）
		if(me.config.MODELTYPE =='2' || me.config.MODELTYPE =='5' ){
			$('#'+me.config.domId+'-col-BarCodeForm-Mate').css('height',height+'px');
			$('#'+me.config.domId+'-col-BarCodeForm-Mate').css('background-color','royalblue');
			$('#'+me.config.domId+'-BarCodeForm-Mate').css('height',height-2+'px');
			$('#'+me.config.domId+'-BarCodeForm-Mate').css('background-color','yellowgreen');

		}
		$('#'+me.config.domId+'-col-BarCodeForm').css('height',height+'px');
		$('#'+me.config.domId+'-BarCodeForm').css('height',height-2+'px');
    	BarCodeFormListInstance.changeSize(height);
    };
    //列表数据清空
    Class.prototype.clearData= function(){
    	var me = this;
    	BarCodeFormListInstance.clearData();
    	if(me.config.MODELTYPE =='2' || me.config.MODELTYPE =='5'){
			MateListInstance.clearData();
		}
    	
    };
    //扫码判断条码号是否已存在
    Class.prototype.isExistBarcode = function(value){
    	var me = this,
			tableCache = BarCodeFormListInstance.getListData(),
			isExist = false,isExec=false;

		$.each(tableCache, function (i, item) {
			if (value == item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
				isExist = true;
				return false;
			}
		});
		if(isExist){
			//2.重复扫入条码没有任何提示，只是勾选条码号
			BarCodeFormListInstance.checkRow(value);
//			layer.msg("该条码已存在!", { icon: 5});
			isExec = true;
		}
		return isExec;
    };
     //条码号与查询到的样本单匹配
    Class.prototype.isMateData= function(BarCode){
    	var me = this;
    	var isExec = false;
    	//获取匹配列表数据
		var list = me.getMateList();
	    for(var i=0;i<list.length;i++){
	    	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == BarCode ){
	    		isExec =true;
	    		break;
	    	}
	    }
	    return isExec;
	};
	    //获取匹配列表数据
    Class.prototype.getMateList = function(){
    	var me = this;
    	return MateListInstance.getListData();
    };

    //样本单数据重载
    Class.prototype.loadData = function(data){
    	var me = this;
    	BarCodeFormListInstance.loadData(data);
    	if(me.config.MODELTYPE =='2' || me.config.MODELTYPE =='5'){ //匹配查询列表查询
	    	//数据匹配到病人信息列表（刷新列表）
			MateListInstance.findBarCode(data[0].LisBarCodeFormVo_LisBarCodeForm_BarCode);
		}
    };
     //数量计算
    Class.prototype.getNum = function(){
    	var me = this;
    	return BarCodeFormListInstance.getNum();
    };
    //查询按钮查询
    Class.prototype.onSearch = function(where,callback){
    	var me = this;
    	me.clearData();
    	if(me.config.MODELTYPE =='2' || me.config.MODELTYPE =='5'){ //匹配查询列表查询
    		MateListInstance.onSearch(where,function(list){
    			MateListInstance.loadData(list);
    		});
    	}else{ //样本单列表查询
    		BarCodeFormListInstance.onSearch(where,function(list){
    			BarCodeFormListInstance.loadData(list);
    			callback();
    		});
    	}
    };
    Class.prototype.bLoadData= function(list){
    	var me = this;
    	BarCodeFormListInstance.loadData(list);
    };
    Class.prototype.mLoadData= function(list){
    	var me = this;
    	MateListInstance.loadData(list);
    };
    Class.prototype.changeSize= function(height){
    	var me = this;
    	height = height;
    	$('#'+me.config.domId+'-col-BarCodeForm').css('height',height+'px');
    	$('#'+me.config.domId+'-BarCodeForm').css('height',height-10+'px');
		BarCodeFormListInstance.changeSize(height);
		
		if(me.config.MODELTYPE =='2' || me.config.MODELTYPE =='5'){ //匹配查询列表查询
			$('#'+me.config.domId+'-col-BarCodeForm-Mate').css('height',height+'px');
			$('#'+me.config.domId+'-BarCodeForm-Mate').css('height',height-10+'px');
	    	MateListInstance.changeSize(height);
	    }
    };
    //打包清单服务
	Class.prototype.packNoByBarCode = function(barcodes,callback){
		var me = this;
		var config = {
			type:'post',
			url:COLLECT_PACK_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				barcodes:barcodes
			})
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = data.value || '';
				callback(list);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
    //获取匹配列表数据
    Class.prototype.getMateList = function(){
    	var me = this;
    	return MateListInstance.getListData();
    };
    //匹配列表更新匹配上的标记 改变颜色-
    Class.prototype.findBarCode = function(barcode){
    	var me = this;
    	 MateListInstance.findBarCode(barcode);
    };
    //获取样本单列表数据
    Class.prototype.getList = function(){
    	var me = this;
    	return BarCodeFormListInstance.getListData();
    };
    //获取样本单勾选的数组
    Class.prototype.getCheckedList = function(){
    	var me = this;
    	return BarCodeFormListInstance.getCheckedList();
    };
    //获取样本单勾选的条码号
    Class.prototype.getCheckedBarcodes = function(){
    	var me = this;
    	return BarCodeFormListInstance.getCheckedBarcodes();
    };
   //参数配置的提示方式处理
	Class.prototype.onFailureInfoHandle = function (list,isConstraintUpdate,callback) {
		var me = this,
			list = JSON.parse(JSON.stringify(list)),
			isOut = false,//是否是等待确认框执行
			pendingData = [],//暂时存储数据集合 等待再次发送服务数据返回后一起处理
			needUpdateBarcode = [],//需要再次发送服务条码号集合
			resultList = [];//{ data: null, isSuccess: true, tipList: [] };//data:当前数据，isSuccess：是否成功,tipList:提示信息集合
		$.each(list, function (i, item) {
			//自主选择-- 不允许
			if (item["IsNotAllow"] && item["IsNotAllow"] == 1) {
				resultList.push({ data: item, isSuccess: false, tipList: [item["NotAllowInfo"]] ,needUpdateBarcode:""});
				//不需要再次发送服务的数据存储
				if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1) pendingData.push(item);
				return true;
			}
			resultList.push({ data: item, isSuccess: true, tipList: [] ,needUpdateBarcode:'',alterModeList:""});
			var failureInfoArr = item["LisBarCodeFormVo_failureInfo"] ? JSON.parse(item["LisBarCodeFormVo_failureInfo"]) : [];
			$.each(failureInfoArr, function (j, itemJ) {
				if (isOut) return false;
				var alterMode = itemJ["alterMode"],
					failureInfo = itemJ["failureInfo"];
				switch (String(alterMode)) {
					case "4"://用户自行选择
						isOut = true;
						var msginfo = failureInfo+"是否允许操作？";
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
								list[i]["NotAllowInfo"] =  failureInfo+"用户操作不允许采集!";
								me.onFailureInfoHandle(list,isConstraintUpdate, callback);
		                		layer.close(index);//执行完后关闭
		                	}
		                );
						break;
					case "3"://允许且提示
						resultList[resultList.length - 1].tipList.push(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode+','+failureInfo);
						resultList[resultList.length - 1].alterModeList=alterMode;
						break;
					case "2"://不允许不提示
						resultList[resultList.length - 1].tipList.push(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode+','+failureInfo);
						resultList[resultList.length - 1].isSuccess = false;
						resultList[resultList.length - 1].alterModeList=alterMode;
						break;
					case "1"://不允许且提示
						resultList[resultList.length - 1].tipList.push(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode+','+failureInfo);
						resultList[resultList.length - 1].isSuccess = false;
						resultList[resultList.length - 1].alterModeList=alterMode;
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
			me.updateSampleGatherState(needUpdateBarcode.join(), isConstraintUpdate, function (data) {
				me.onFailureInfoHandle(data.concat(pendingData),isConstraintUpdate,callback);
			});
		}
		//执行完成
		if (!isOut && needUpdateBarcode.length == 0) callback && callback(resultList);
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
	//扫码处理  非匹配模式
	Class.prototype.onBarcode = function(barcode,isupdate,isConstraintUpdate,callback){
        var me = this;
        var TipMsg = [],alterMode="";
	    me.getBarcodeList(barcode,isupdate,function(list){
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
					$.each(list, function (a, itemA) {
						if (item["isSuccess"]){
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
						}
					});
					//弹出信息处理
					if (tipList.length > 0) {
						alterMode = item["alterModeList"];
						$.each(tipList, function (k, itemK) {
							TipMsg.push("条码号为:" + itemK);
							Msg.push("条码号为:" + itemK);
						});
					}
				});
			    callback(addlist,TipMsg,alterMode);
			});
    	});
    };
	/**样本采集_根据条码号获取样本数据_更新样本状态
     * barcodes 条码号字符串，多个使用英文逗号分割
     * isupdate 是否更新
     * */
	Class.prototype.getBarcodeList = function(barcodes,isupdate,callback){
		var me = this;
		if(!barcodes){
			layer.msg("请扫码！",{icon:5});
			return false;
		}
		var config = {
			type:'post',
			url:BARCODE_LIST_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				barcodes:barcodes,//条码集合，多个用逗号分开
				isupdate:isupdate,
				fields:BarCodeFormListInstance.getFields(),
			    isPlanish:true 
			})
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
	 /**样本采集_根据条码号更新采集状态*/
	Class.prototype.updateSampleGatherState = function(barcodes,isConstraintUpdate,callback){
		var me = this;
		var config = {
			type:'post',
			url:UPDATE_GATHERSTATE_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				barcodes:barcodes,//条码集合，多个用逗号分开
				isConstraintUpdate:isConstraintUpdate
			})
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
	 //参数--特定核收字段及名称
	Class.prototype.onReceive = function(value,receiveType,callback){
		var me = this;
		if(!value){
			layer.msg("请输入核收数据！",{icon:5});
			return false;
		}
		var config = {
			type:'post',
			url:GET_PARAMS_HESHOU_LIST_URL,
			data:JSON.stringify({
				nodetype:me.config.nodetype,//站点类型
				receiveType:receiveType,//核收字段
				value:value,
				fields:me.config.fields,
			    isPlanish:true 
			})
		};
        var loadIndex = layer.load();
		uxutil.server.ajax(config,function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value ||{}).list || [];
				callback(list);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
	//撤销采集,取消采集的数据应清掉
	Class.prototype.onRevocation = function(barcode){
		var me = this,
			arr=[];
		var list = me.getList();
		for(var i in list){
			var IsExec = true;
		 	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode ==barcode ){
                IsExec = false;
		 	}
		 	if(IsExec)arr.push(list[i]);
		}
		BarCodeFormListInstance.clearData();
		BarCodeFormListInstance.loadData(arr);
    	if(me.config.MODELTYPE =='2' || me.config.MODELTYPE =='5'){ //匹配查询列表查询
	    	//数据匹配到病人信息列表（刷新列表）
			MateListInstance.findBarCodeRevocation(barcode);
		}
	};
    //打包清单
    Class.prototype.onpackNoByBarCode = function(){
    	var me = this;
    	var checklist  = me.getCheckedList();
		if(checklist.length==0){
			layer.msg("请勾选行数据!", { icon: 0, anim: 0 });
			return false;
		}
		var barcodes= [];    	
        for(var i in checklist){
			if(checklist[i].LisBarCodeFormVo_IsConfirm=='1'){
				barcodes.push(checklist[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
			}
		}
        if(barcodes.length==0){
			layer.msg("请先采集确认再打包清单!", { icon: 0, anim: 0 });
			return false;
		}
        var list = me.getList();
    	me.packNoByBarCode(barcodes.join(','),function(CollectPackNo){
    		//打包号
			if(CollectPackNo){
				var printList=[];
				//打包号更新到列表中
				for(var i in list){
					for(var j in checklist){
						if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == checklist[j].LisBarCodeFormVo_LisBarCodeForm_BarCode ){
							list[i].LisBarCodeFormVo_CollectPackNo = CollectPackNo;
							printList.push(list[i]);
							checklist.splice(j,1);
							break;
						}
					}
				}
				BarCodeFormListInstance.loadData(list);
			    me.onListPrint(printList);
			}else{
				layer.msg('没有返回打包号', { icon: 5});
				return;
			}
    	});
	};
    //打印清单
    Class.prototype.onListPrint = function(PrintList){
    	var me = this;
        if(PrintList.length==0){
        	layer.msg("请先采集确认再打印清单!", { icon: 0, anim: 0 });
			return false;
        }
        var modeltype = "10";
        var modeltypename  = "样本采集_样本清单";
    	me.onPrint(PrintList,modeltype,modeltypename);
    };
    //打印
	Class.prototype.onPrint = function(data,modeltype,modeltypename){
		var me = this;
		data = data || [];
		if(data.length==0)return false;
		 //去除前缀
		data = JSON.stringify([data]).replace(RegExp("LisBarCodeFormVo_", "g"), "").replace(RegExp("LisBarCodeForm_", "g"), "");
		var PrinterName = me.config.PrinterName;
		var IsPreview = me.config.IsPreview;
		var url = uxutil.path.LAYUI + '/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=3&ModelType='+modeltype+'&ModelTypeName='+modeltypename+'&isDownLoadPDF=true&IsPreview='+me.config.IsPreview+ (me.config.PrinterName ? ("&PrinterName=" + me.config.PrinterName) : "");
		layer.open({
			title:'打印清单',
			type:2,
			content:url,
			maxmin:true,
			toolbar:true,
			resize:true,
			area:['500px','380px'],
			success:function(layero,index){
				var iframe = $(layero).find('iframe')[0].contentWindow;
				iframe.PrintDataStr = data;
			}
		});
	};
	    //确认采集
    Class.prototype.onAccept= function(){
    	var me = this;
    	window.event.preventDefault();
    	if(me.config.IsConfirmUser=='1'){//批量确认模式验证采集人
    		me.onPreventDefault();
        	me.onVerifiySign();
        }else{
           me.acceptExec();
        }
    };
    //執行採集確認
    Class.prototype.acceptExec = function(){
    	var me = this;
    	var barcodes = me.getCheckedBarcodes();
    	if(barcodes.length==0){
    		layer.msg('请勾选要采样确认的数据行!',{icon:5});
    		return false;
    	}
    	var barcode = barcodes.join(',');
    	var CheckedList = me.getCheckedList();
    	//列表刷新--待确认
    	me.updateSampleGatherState(barcode,true,function(data){
    		//已采集标记
    		for(var i=0;i<CheckedList.length;i++){
    			CheckedList[i].LisBarCodeFormVo_IsConfirm = '1';
    		}
        	BarCodeFormListInstance.loadData(CheckedList);
    	    layer.msg("确认采集成功",{icon:6,time:2000});
    	    //采集确认打印清单
		    if(me.config.IsAutoPrint== '1')me.onListPrint(CheckedList);
        });
    };
    //阻止默认事件
    Class.prototype.onPreventDefault = function() {
        var device = layui.device();
        if (device.ie) {
            window.event.returnValue = false;
        } else {
            window.event.preventDefault();
        }
    };
	//核心入口
	PreSampleContentIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		me.initHtml();
		me.initListeners();
		return me;
	};
	//暴露接口
	exports(MOD_NAME,PreSampleContentIndex);
});