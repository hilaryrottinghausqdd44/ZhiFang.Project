/**
 * @name：modules/pre/sample/refuse/index 样本拒收工具栏
 * @author：liangyl
 * @version 2020-08-31
 */
layui.extend({
	uxutil:'ux/util',
	uxtable:'ux/table',
    uxbase: 'ux/base',
	CommonSelectDept:'modules/common/select/dept',
	CommonSelectUser:'modules/common/select/user',
	CommonSelectSickType :'modules/common/select/sicktype',
	PreSampleRefuseParams:'modules/pre/sample/refuse/params'
}).define(['uxutil','form','uxtable','uxbase','laydate','CommonSelectDept','CommonSelectUser','PreSampleRefuseParams','CommonSelectSickType'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		laydate = layui.laydate,
		uxtable = layui.uxtable,
		uxbase = layui.uxbase,
		CommonSelectDept = layui.CommonSelectDept,
		CommonSelectUser = layui.CommonSelectUser,
		CommonSelectSickType = layui.CommonSelectSickType,
		PreSampleRefuseParams = layui.PreSampleRefuseParams,
		MOD_NAME ='PreSampleRefuseIndex';
	
	//获取拒收短语
	var GET_PHRASES_WATCH_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchByHQL?isPlanish=true";
    //查询按钮和手动拒收扫条码号获取数据调服务
	var GET_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreRefuseAcceptGetSampleListByWhere";
	//自动拒收和批量拒收操作调服务
	var GET_BARCODE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreRefuseAcceptEditSample";

	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
			  '<div class="layui-inline">',
                   '<label class="layui-form-label">条码号:</label>',
					'<div class="layui-input-inline">',
					   '<input type="text" name="{domId}-barCode" id="{domId}-barCode" autocomplete="off" class="layui-input" />',
					'</div>',
				'</div>',
				'<div class="layui-inline layui-hide">', 
		        '<label class="layui-form-label">就诊类型:</label>',
		        '<div class="layui-input-inline">',
		         '<select name="{domId}-sickTypeID" id="{domId}-sickTypeID" lay-search=""  lay-filter="{domId}-sickTypeID"> <option value="">请选择</option> </select>', 
		        '</div>', 
		       '</div>',
			'</div>',
			'<div class="layui-form-item" style="margin-bottom:0;">',
			'<div class="layui-inline">', 
                   '<label class="layui-form-label">拒收原因:</label>', 
				   '<div class="layui-input-inline">',
						'<select name="{domId}-refuseReason" id="{domId}-refuseReason" lay-filter="{domId}-refuseReason" lay-search="" >',
						'</select>',
				   '</div>',
				'</div>',
				'<div class="layui-inline">', 
                   '<label class="layui-form-label">处理意见:</label>', 
				   '<div class="layui-input-inline">',
						'<select name="{domId}-handleAdvice" id="{domId}-handleAdvice" lay-search=""  lay-filter="{domId}-handleAdvice">',
						'</select>',
				   '</div>',
				'</div>',
			  '<div class="layui-inline">', 
                   '<label class="layui-form-label">接听人:</label>', 
					'<div class="layui-input-inline">',
                       '<select name="{domId}-answerPeople" id="{domId}-answerPeople" lay-search=""  lay-filter="{domId}-answerPeople">',
						'</select>',
					'</div>',
				'</div>',
				'<div class="layui-inline">',
                   '<label class="layui-form-label">电话号码:</label>',
				   '<div class="layui-input-inline">',
				    	'<input type="text" name="{domId}-phoneNum" id="{domId}-deptTelNo" autocomplete="off" class="layui-input" />',
				   '</div>',
				'</div>',
				'<div class="layui-inline">', 
                   '<label class="layui-form-label">拒收备注:</label>', 
				   '<div class="layui-input-inline">',
				    	'<input type="text" name="{domId}-refuseRemark" id="{domId}-refuseRemark" autocomplete="off" class="layui-input" />',
				   '</div>',
				'</div>',
				'<div class="layui-inline" style="float:right;">',
                 '<input type="checkbox" id="{domId}-show_search_toolbar" lay-filter="{domId}-show_search_toolbar" title="查询" lay-skin="primary" />',  
				'</div>',
					'<div class="layui-inline" style="float:right;padding-left: 10px;">',
                    '<label id="{domId}-num" class="layui-form-label" style="color: blue;font-size: 18px;font-weight: bold ;width:120px;">数量:0</label>', 
				'</div>',
			'</div>',
			'<div class="layui-form-item layui-hide" id="{domId}-show-toolbar" style="margin-bottom:0;">',
			  '<div class="layui-inline">', 
		        '<label class="layui-form-label">姓名:</label>',
		        '<div class="layui-input-inline">', 
		         '<input type="text" name="{domId}-cName" autocomplete="off" class="layui-input" />',
		        '</div>',
		       '</div>',
		       '<div class="layui-inline">',
		        '<label class="layui-form-label">病历号:</label>', 
		        '<div class="layui-input-inline">', 
		         '<input type="text" name="{domId}-patNo" autocomplete="off" class="layui-input" />', 
		        '</div>', 
		       '</div>', 
		       '<div class="layui-inline">', 
		        '<label class="layui-form-label">开单科室:</label>',
		        '<div class="layui-input-inline">',
		         '<select name="{domId}-deptID" id="{domId}-deptID" lay-search=""  lay-filter="{domId}-deptID"> <option value="">请选择</option> </select>', 
		        '</div>', 
		       '</div>',
		       '<div class="layui-inline">', 
		        '<label class="layui-form-label">样本状态:</label>', 
		        '<div class="layui-input-inline">', 
	            '<select name="{domId}-barCodeStatus" id="{domId}-barCodeStatus" lay-search=""  lay-filter="{domId}-barCodeStatus"> <option value="">请选择</option> <option value="BarCodeStatusID<7">未签收</option> <option value="BarCodeStatusID=7 and InceptTime!=null">已签收</option> <option value="BarCodeStatusID=9">已拒收</option> <option value="BarCodeStatusID=10">已让步</option> </select>',
		        '</div>', 
		       '</div>', 
		      '<div class="layui-inline">', 
		        '<label class="layui-form-label">时间类型:</label>', 
		        '<div class="layui-input-inline" style="width: 120px;">', 
		         '<select name="{domId}-dateType" id="{domId}-dateType" lay-search="" lay-filter="{domId}-dateType"></select>', 
		        '</div>',
		        '<div class="layui-input-inline" style="width:190px;">', 
		         '<input type="text" id="{domId}-gDate" name="{domId}-gDate" class="layui-input myDate" placeholder="开始时间-结束时间" />', 
		         '<i class="layui-icon layui-icon-date"></i>',
		        '</div>', 
		       '</div>',
		       '<div class="layui-inline">', 
		        '<button type="button" id="{domId}-search" class="layui-btn layui-btn-xs"><i class="layui-icon layui-icon-search"></i>查询</button>', 
		       '</div>', 
			'</div>',
		'</div>',
	   '<div class="layui-row" style="margin:0px;padding:0px;">',
			'<div class="layui-col-xs12 layui-col-sm12 layui-col-md12 layui-col-lg12" id="{domId}-table-row">',
				'<table class="layui-hide" id="{domId}-table" lay-filter="{domId}-table"></table>',
			'</div>',
		'</div>',
		'<style>',
			'.{domId}-grid-div{padding:2px;margin-bottom:0px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
		'</style>'
	].join('');

	//列表字段：格式=BarCode&条码号&100&show,OrderExecTime&医嘱指定执行时间&100&show,
	var LIST_COLS_INFO = null;
	//后台获取字段数组
	var LIST_FIELDS = null;
    //样本拒收样本单实例
	var BarCodeFormListInstance = null;
    //功能参数
	var PreSampleRefuseParamsInstance = null;
	//门诊样本条码
	var PreSampleRefuseIndex = {
		//对外参数
		config:{
			domId:null,
			height:null
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			toolbar:null,
			skin:'line',//行边框风格
			//even:true,//开启隔行背景
			size:'sm',//小尺寸的表格
			defaultToolbar:null,
			where:{},
			height:'full-25',
			initSort: {
				field:'DataAddTime',//排序字段
				type:'desc'
			},
			cols:[],
			text: {none: '暂无相关数据' }
			
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,PreSampleRefuseIndex.config,setings);
	};
	
	
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);
		//时间类型   lisbarcodeform.LisOrderForm.OrderTime-开单时间,lisbarcodeform.InceptTime-签收时间,lisbarcodeform.CollectTime-采样时间,lisbarcodeform.ArriveTime-送达时间
        if(PreSampleRefuseParamsInstance.get('Pre_OrderRejection_DefaultPara_0009')){
        	var date1 = PreSampleRefuseParamsInstance.get('Pre_OrderRejection_DefaultPara_0009').split(',');
        	var htmls = [];
        	for(var i in date1){
        		var arr = date1[i].split('-');
        		htmls.push("<option value='" + arr[0] +"'>" + arr[1] + "</option>");
        	}
			$('#'+me.config.domId + '-dateType').html(htmls.join(""));
			form.render('select');
        }
		//初始化table
		me.initTableHtml();
		//处理意见和拒收原因下拉框初始化
		me.initPhrasesWatchHtml();
		//就诊类型
		CommonSelectSickType.render({
			domId:me.config.domId+'-sickTypeID',
			done:function(){
				
			}
		});
		//开单科室
		CommonSelectDept.render({
			domId:me.config.domId+'-deptID',
			code:'1001101',
			done:function(){
				
			}
		});
		//接听人
		CommonSelectUser.render({
			domId:me.config.domId+'-answerPeople',
			code:'1001002',
			done:function(){
			}
		});
		//时间范围初始化
		me.initDateHtml();
		form.render();
	};
	//初始化HTML
	Class.prototype.initTableHtml = function(){
		var me =  this;
		var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height()-55;
        
		me.tableConfig = $.extend({},me.tableConfig,me.tableConfig);
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		me.tableConfig.height = height;
		me.tableConfig.cols  = [me.getCols()];
		
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			res.data.forEach(function (item, index) {
				var ColorValue = item.LisBarCodeFormVo_LisBarCodeForm_ColorValue;
				if (ColorValue){//采样管颜色
					//背景色-红色
					$('div[lay-id="'+me.tableId+'"]').
					find('tr[data-index="' + index + '"]').
					find('td[data-field="LisBarCodeFormVo_LisBarCodeForm_Color"]').
					css('background-color', ColorValue);
				}
			});
		};
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({data:[]});
	};
	//从服务器获取拒收短语信息列表
	Class.prototype.getPhrasesWatch = function(callback){
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_PHRASES_WATCH_LIST_URL,
			type:'get',
			async:false, 
			data:{
				page:1,
				limit:100,
				fields:'LBPhrasesWatch_PhrasesType,LBPhrasesWatch_CName,LBPhrasesWatch_Id',
				where:'lbphraseswatch.PhrasesType in(1,2)'
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value ||{}).list || []);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
    //处理意见和拒收原因下拉框初始化
    Class.prototype.initPhrasesWatchHtml = function(){
    	var me = this;
    	me.getPhrasesWatch(function(data){
    		var list1=[],//拒收原因
		        list2=[];//处理意见
		    for(var i in data){
			    if(data[i].LBPhrasesWatch_PhrasesType =='1'){ //拒收原因
			 	   list1.push(data[i]);
			    }else{ //处理意见
			       list2.push(data[i]);
			    }
			    var htmls = ['<option value="">请选择拒收原因</option>'];
				for(var i=0;i<list1.length;i++){
					var selectHtml ='';
					htmls.push("<option value='" + list1[i].LBPhrasesWatch_Id + "'"+selectHtml+">" + list1[i].LBPhrasesWatch_CName + "</option>");
				}
				$('#' + me.config.domId +'-refuseReason').html(htmls.join(''));
				
				var	htmls2 = ['<option value="">请选择处理意见</option>'];
				for(var i=0;i<list2.length;i++){
					htmls2.push("<option value='" + list2[i].LBPhrasesWatch_Id + "'>" + list2[i].LBPhrasesWatch_CName + "</option>");
				}
				$('#' + me.config.domId +'-handleAdvice').html(htmls2.join(''));
			}
		    form.render('select');
		});
    };
     //时间范围初始化
    Class.prototype.initDateHtml = function(){
    	var me = this;
    	var today = new Date();
		var defaultvalue = uxutil.date.toString(today, true) + " - " + uxutil.date.toString(today, true);
    	$('#' + me.config.domId+"-gDate").val(defaultvalue);

      //监听日期图标
		 $('#' + me.config.domId+"-gDate+i.layui-icon").on("click", function () {
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
				//时间范围
				laydate.render({
					 elem:'#' + me.config.domId+"-gDate",
					 eventElem:me.config.domId+'-gDate+i.layui-icon',
					 type:'date',
					 range: true,
					 show:true
				 });
				 $("#" + elemID).attr("data-type", "date");
			 } else {
				 $("#" + elemID).attr("data-type", "text");
				 var key = $("#" + elemID).attr("lay-key");
				 $('#layui-laydate' + key).remove();
			 }
		 });
		 //监听日期input -- 不弹出日期框
		 $('#' + me.config.domId+"-form").on('focus','#' + me.config.domId+'gDate', function () {
			 me.preventDefault();
			 layui.stope(window.event);
			 return false;
		 });
	};
	//列表列
	Class.prototype.getCols = function(){
		var cols = [
			{type: 'checkbox', fixed: 'left'},
			{field:'LisBarCodeFormVo_IsConfirm', width:80, title: '是否已确认拒收', hide: true},
			{field:'LisBarCodeFormVo_failureInfo', width:80, title: 'LisBarCodeFormVo_failureInfo', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID', width:80, title: '状态', hide: true},
			{field:'LisBarCodeFormVo_LisBarCodeForm_ColorValue', width:80, title: 'LisBarCodeFormVo_LisBarCodeForm_ColorValue', hide: true}];
		for(var i in LIST_COLS_INFO){
			//BarCode&条码号&100&show
			var arr = LIST_COLS_INFO[i].split('&');
			cols.push({
				field:arr[0],title:arr[1],width:arr[2],
				hide:(arr[3] =='show' ? false : true)
			});
		}
		cols.push({field:'LisBarCodeFormVo_RefuseAcceptReason', width:100, title: '拒收原因'});
		cols.push({field:'LisBarCodeFormVo_RefuseAcceptPerson', width:100, title: '拒收人'});
		cols.push({field:'LisBarCodeFormVo_RefuseAcceptAdvice', width:100, title: '拒收处理意见'});
		return cols;
	};
	//显示隐藏查询工具栏
	Class.prototype.showToolbar = function(bo){
		var me = this;
        if(bo){
        	$("#"+me.config.domId+"-show-toolbar").removeClass('layui-hide');
        }else{
        	$("#"+me.config.domId+"-show-toolbar").addClass('layui-hide');
        }
        me.Size();
	};
	//获取按钮查询条件
	Class.prototype.getWhere = function(){
		var me = this,startDate='',endDate='',
			values = form.val(me.config.domId +'-form'),
			barCode = values[me.config.domId +'-barCode'], //条码号
	        CName = values[me.config.domId +'-cName'], //姓名
	        PatNo = values[me.config.domId +'-patNo'], //病历号
	        BarCodeStatusID = values[me.config.domId +'-barCodeStatus'], //样本状态BarCodeStatusID
	        ExecDeptID = values[me.config.domId +'-deptID'], //开单科室
	        dateType = values[me.config.domId +'-dateType'], //时间类型
            gDate = values[me.config.domId + '-gDate']; //时间范围
        if(gDate){
        	startDate = gDate.substring(0,10); //开始日期
            endDate = gDate.substring(13,gDate.length); //结束时间
        }      
		var where = "",arr=[];
		//姓名
		if(CName)arr.push("LisPatient.CName='"+CName+"'");
		//病历号
		if(PatNo)arr.push("LisPatient.PatNo='"+PatNo+"'");
		//开单科室LisBarCodeFormVo_LisBarCodeForm_ExecDeptID
		if(ExecDeptID)arr.push("ExecDeptID="+ExecDeptID);
		//样本状态BarCodeStatusID
		if(BarCodeStatusID)arr.push(BarCodeStatusID);
		//时间类型
		if(dateType && startDate && endDate){
			arr.push(dateType + " between '" + startDate +' 00:00:00' + "' and '" + endDate + " 23:59:59'");
		}
		//条码号   ---查询按钮和手动拒收扫条码号获取数据调服务1，通过条码号查询时，需要将条件拼在where参数里
		if(barCode){
			arr = [];
			arr.push(" BarCode='"+barCode+"'");
		}
		if(arr.length>0)where= arr.join(' and ');
		
		return where;
	};
	//批量拒收操作/确认拒收实体
	Class.prototype.getParams = function(isReject,barcodes){
		var me = this,
			values = form.val(me.config.domId +'-form'),
			refuseID = values[me.config.domId +'-refuseReason'], //拒收原因
			handleAdvice = values[me.config.domId +'-handleAdvice'],//处理意见
			answerPeople = values[me.config.domId +'-answerPeople'], //接听人
			refuseRemark = values[me.config.domId +'-refuseRemark'], //拒收备注
			phoneNum = values[me.config.domId +'-phoneNum'],
			sickTypeId =  $("#"+me.config.domId + "-sickTypeID").val(), //就诊类型
			sickTypeName = $("#"+me.config.domId + "-sickTypeID").find("option:selected").text(); 
		if(!sickTypeId)sickTypeName='';

		var refuseReason = $("#"+me.config.domId + "-refuseReason").find("option:selected").text(); //拒收原因
		if(handleAdvice)$("#"+me.config.domId + "-handleAdvice").find("option:selected").text(); //处理意见
		return {
			nodetypeID:me.config.nodetype,//-站点类型
			barcodes:barcodes,//条码集合，多个用逗号分开
			refuseID:refuseID,
			refuseReason:refuseReason, //拒收原因
			handleAdvice:handleAdvice, //处理意见
			answerPeople:answerPeople,
			phoneNum:phoneNum,
			refuseRemark:refuseRemark, //拒收备注
			fields:me.getFields(),
			isPlanish:true,
			sickTypeId:sickTypeId,
			sickTyepName:sickTypeName,
			isForceReject:isReject //false ，有交互传true
		};
	};
	//2.自动拒收和批量拒收操作调服务2，多个条码号用英文逗号分隔barcodes，isForceReject传fasle
	Class.prototype.onRefuse = function(isReject,barcodes){
		var me = this;
		if(!barcodes){
			layer.msg("没有数据不能做拒收操作！",{icon:5});
			//清空扫码输入框数据
	    	$("#"+me.config.domId+"-barCode").val('');
	        $("#"+me.config.domId+"-barCode").focus();
		}else{
			//拒收原因不能为空
			var isExec = me.getRefuseReason();
			if(!isExec){
				layer.msg("拒收原因不能为空！",{icon:5});
				return false;
			}
		    me.bataRefuse(isReject,barcodes,function(list){
				//提示信息处理
				me.onFailureInfoHandle(list, function (resultList) {
					var Msg = [],addlist=[];//弹出信息
					$.each(resultList, function (i, item) {
						var data = item["data"],
							tipList = item["tipList"];
						$.each(list, function (a, itemA) {
							if (item["isSuccess"]){
								if (data.LisBarCodeFormVo_LisBarCodeForm_BarCode == itemA["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
		                        	data.LAY_CHECKED = true;
		                        	var BarCodeStatusID =   data.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID ?   data.LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID  : 0;
									data.LisBarCodeFormVo_IsConfirm = Number(BarCodeStatusID) > 8 ? '1' : '';
		                            addlist.push(data);
		                        }
							} 
						});
						//弹出信息处理
						if (tipList.length > 0) {
							$.each(tipList, function (k, itemK) {
								Msg.push(itemK);
							});
						}
					});
					//弹出信息处理
				    if(Msg.length > 0)layer.alert(Msg.join('<br>'), { icon: 0, anim: 0 });
				    if(addlist.length>0)me.addRow(addlist);
				});
			});
		}
	};
	
	//拒收原因不能为空校验
	Class.prototype.getRefuseReason = function(){
	    var me = this,
	        isExec = true,
			values = form.val(me.config.domId +'-form'),
			refuseReason = values[me.config.domId +'-refuseReason']; //拒收原因
	    if(!refuseReason)isExec=false;
	    return isExec;
	};		
	//样本单列表add行
	Class.prototype.addRow = function(list){
		var me = this;
		//新增行
		me.loadData(list);
    	//清空扫码输入框数据
    	$("#"+me.config.domId+"-barCode").val('');
        $("#"+me.config.domId+"-barCode").focus();
        
        var num = me.uxtable.table.cache[me.tableId].length;
        //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+num);
	};
	
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		//查询按钮查询
		$('#'+me.config.domId+'-search').on('click',function(){
			me.onSearch(function(list){
				me.clearData();//清除
				me.addRow(list); // 新增
			});
		});
		//扫码,回车事件
	    $("#"+me.config.domId+"-barCode").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	var barCode = $("#"+me.config.domId+"-barCode").val();
	        	if(barCode){
	        		//去掉前后空格
	        		barCode = barCode.replace(/(^\s*)|(\s*$)/g, "");
	        	    //判断条码号是否已扫码
		        	var isExist = me.isExistBarcode(barCode);
		        	if(isExist){
		        		//清空扫码输入框数据
				    	$("#"+me.config.domId+"-barCode").val('');
				        $("#"+me.config.domId+"-barCode").focus();
		        		return false;
		        	}
	        		//拒收是否提示
	        		var isTips = PreSampleRefuseParamsInstance.get('Pre_OrderRejection_DefaultPara_0002');
	                $("#"+me.config.domId+"-barCode").blur();
	                if(isTips== '0'){//不提示
	                	me.onBarCode(barCode);
	                }else{//提示
	                	var msginfo = "条码"+barCode+"是否确定拒收?";
	                	uxbase.MSG.onConfirm(msginfo,{ icon: 3, title: '提示',enter:true},
		                	function(index){
		                		me.onBarCode(barCode);
								//这里可以写需要处理的流程
								layer.close(index);//执行完后关闭
		                	},
		                	function(index){
		                		layer.close(index);//执行完后关闭
		                	}
		                );
	                }
	        	}else{
	        		layer.msg('条码号不能为空,请扫码!',{icon:5});
	        	}
	            return false;
	        }
	    });
	    form.on('checkbox('+me.config.domId+'-show_search_toolbar)', function(data){
			me.showToolbar(data.elem.checked);
		}); 
	};
	//扫码处理
	Class.prototype.Size = function(){
		var me = this;
		var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height()-55;
        var bo = $('#'+me.config.domId+'-show_search_toolbar').prop('checked');
        if(bo)height = height - 2;
        me.tableConfig.height = height;
        var list = me.uxtable.table.cache[me.tableId];
        me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({data:list});
	};
	//扫码处理
	Class.prototype.onBarCode = function(barCode){
		var me = this;
		//是否是手动拒收
	    var isAuto = PreSampleRefuseParamsInstance.get('Pre_OrderRejection_DefaultPara_0004');
		if(isAuto == '0'){//手动拒收
			me.onSearch(function(list){
				if(list.length==0){
					layer.msg("未找到该条码信息!", { icon: 5, anim: 0 });
					return false;
				}
				for(var i in list){
					list[i].LAY_CHECKED='true';
				}
				me.addRow(list);
			});
		}else{//自动拒收
			me.onRefuse(false,barCode);
		}
	};
	
	//拒收确认
	Class.prototype.onAccept = function(){
		var me = this,
			barcodes = me.getCheckedBarcodes();
			
		if(barcodes.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			//允许核收后拒收（参数)
			var ischeck= PreSampleRefuseParamsInstance.get('Pre_OrderRejection_DefaultPara_0003');
			var barcode = barcodes.join(',');
			if(ischeck=="0"){//不需要校验  直接拒收
				me.onRefuse(true,barcode);
			}else{//需要校验
				 //列表刷新--待确认
				me.onRefuse(false,barcode);
		    }
		}
	};
	//扫码判断条码号是否已存在
    Class.prototype.isExistBarcode = function(value){
    	var me = this,
			tableCache =  me.uxtable.table.cache[me.tableId],
			isExist = false,isExec=false;

		$.each(tableCache, function (i, item) {
			if (value == item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) {
				isExist = true;
				return false;
			}
		});
		if(isExist){
				//2.重复扫入条码没有任何提示，只是勾选条码号
			me.checkRow(value);
//			layer.msg("该条码已存在!", { icon: 5});
			isExec = true;
		}
		return isExec;
    };
	 //如果条码已存在，只勾选行
    Class.prototype.checkRow = function(BarCode){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    for(var i=0;i<list.length;i++){  //新增数据
	    	if(list[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == BarCode){
    		    list[i].LAY_CHECKED='true';
    		}
	    }
		me.uxtable.instance.reload({
			url: '',
			data:list || []
		});
	};
	//列表数据清空
    Class.prototype.clearData= function(){
    	var me = this;
    	me.uxtable.instance.reload({
			data:[]
		});
    	//数量显示
    	$("#"+me.config.domId+"-num").text("数量:0");
    };
    //数据加载(新增数据)
    Class.prototype.loadData = function(data){
		var me = this,
	    	list = me.uxtable.table.cache[me.tableId];
	    for(var i=0;i<data.length;i++){  //新增数据
	    	if(data[i].LisBarCodeFormVo_LisBarCodeForm_BarCodeStatusID>8){
				data[i].LisBarCodeFormVo_IsConfirm = '1';
			}
	    	var isExec=true;
	    	for(var j=0;j<list.length;j++){
	    		if(data[i].LisBarCodeFormVo_LisBarCodeForm_BarCode == list[j].LisBarCodeFormVo_LisBarCodeForm_BarCode){
	    			isExec = false;	
	    			data[i].LAY_CHECKED='true';
	    			data[i].LisBarCodeFormVo_IsConfirm  =  data[i].LisBarCodeFormVo_IsConfirm;
	    			list[j] = data[i];
	    		}
	    	}
	    	if(isExec)list.push(data[i]);
	    }
		me.uxtable.instance.reload({
			data:list || []
		});
//		me.Size();
	};
    //打印清单
	Class.prototype.onListPrint = function(){
		var me = this,
			CheckedList = me.uxtable.table.checkStatus(me.tableId).data;
			
		if(CheckedList.length == 0){
			layer.msg("请勾选样本条码！",{icon:5});
		}else{
			var data = [];
			for(var i in CheckedList){
				//已确认的数据行
				if(CheckedList[i].LisBarCodeFormVo_IsConfirm =='1')data.push(CheckedList[i]);
			}
			if(data.length==0){
				layer.msg("去选行没有已确认拒收的数据，请先确认拒收",{icon:5});
				return false;
			}
			var PrinterName = null;
			var url = uxutil.path.LAYUI + '/views/system/comm/JsonPrintTemplateManage/print/index.html?BusinessType=3&ModelType=8&ModelTypeName=样本拒收_样本清单&isDownLoadPDF=true'+ (PrinterName ? ("&PrinterName=" + PrinterName) : "");
			
			if(data.length==0)return false;
			 //去除前缀
			data = JSON.stringify([data]).replace(RegExp("LisBarCodeFormVo_", "g"), "").replace(RegExp("LisBarCodeForm_", "g"), "");

			layer.open({
				title:'打印清单',
				type:2,
				content:url,
				maxmin:true,
				toolbar:true,
				resize:true,
				area:['95%','95%'],
				success:function(layero,index){
					var iframe = $(layero).find('iframe')[0].contentWindow;
					iframe.PrintDataStr = data;
				}
			});
		}
	};
	//1.查询按钮和手动拒收扫条码号获取数据调服务1，通过条码号查询时，需要将条件拼在where参数里
	Class.prototype.onSearch = function(callback){
		var me = this,
		    values = form.val(me.formId),
			sickTypeId = $("#"+me.config.domId + "-sickTypeID").val(), //就诊类型
			sickTypeName = $("#"+me.config.domId + "-sickTypeID").find("option:selected").text(); 
		if(!sickTypeId)sickTypeName='';
		if(!me.getWhere()){
			layer.msg("条件不能为空!", { icon: 5});
			return false;
		}
		var params = {
			nodetype:me.config.nodetype,
			where:me.getWhere(),
			fields:me.getFields(),
			isPlanish:true,
			sickTypeId:sickTypeId,
			sickTypeName:sickTypeName
		};
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_LIST_URL,
			type:'post',
			data:JSON.stringify(params)
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
			    var list = (data.value ||{}).list || [];
                callback(list);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		},true);
	};
	//拒收操作2.自动拒收和批量拒收操作调服务2，多个条码号用英文逗号分隔barcodes，isForceReject传fasle
	Class.prototype.bataRefuse = function(isReject,barcodes,callback){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		var params = me.getParams(isReject,barcodes)
		uxutil.server.ajax({
			url:GET_BARCODE_LIST_URL,
			type:'post',
			async:false,
			data:JSON.stringify(params)
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.msg,{icon:5});
				//清空扫码输入框数据
		    	$("#"+me.config.domId+"-barCode").val('');
		        $("#"+me.config.domId+"-barCode").focus();
			}
		},true);
	};
	//获取勾选的条码数组
	Class.prototype.getCheckedBarcodes = function(){
		var me = this,
			barcodes = [];
		var checkedList = me.uxtable.table.checkStatus(me.tableId).data;
		for(var i in checkedList){
			barcodes.push(checkedList[i].LisBarCodeFormVo_LisBarCodeForm_BarCode);
		}
		return barcodes;
	};
	 //参数配置的提示方式处理
	Class.prototype.onFailureInfoHandle = function (list,callback) {
		var me = this,
			list = JSON.parse(JSON.stringify(list)),
			isOut = false,//是否是等待确认框执行
			pendingData = [],//暂时存储数据集合 等待再次发送服务数据返回后一起处理
			needUpdateBarcode = [],//需要再次发送服务条码号集合
			resultList = [];//{ data: null, isSuccess: true, tipList: [] };//data:当前数据，isSuccess：是否成功,tipList:提示信息集合
		$("#"+me.config.domId+"-barCode").blur();
		$.each(list, function (i, item) {
			//自主选择-- 不允许
			if (item["IsNotAllow"] && item["IsNotAllow"] == 1) {
				resultList.push({ data: item, isSuccess: false, tipList: [item["NotAllowInfo"]] ,needUpdateBarcode:""});
				//不需要再次发送服务的数据存储
				if (needUpdateBarcode.join().indexOf(item["LisBarCodeFormVo_LisBarCodeForm_BarCode"]) == -1) pendingData.push(item);
				return true;
			}
			resultList.push({ data: item, isSuccess: true, tipList: [] ,needUpdateBarcode:''});
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
								me.onFailureInfoHandle(list, callback);
								//这里可以写需要处理的流程
								layer.close(index);//执行完后关闭
		                	},
		                	function(index){
		                		isOut = false;
								list[i]["IsNotAllow"] = 1;
								list[i]["NotAllowInfo"] = failureInfo+",用户操作不允许拒收!";
								me.onFailureInfoHandle(list, callback);
		                		layer.close(index);//执行完后关闭
//		                		$("#"+me.config.domId+"-barCode").focus();
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
			me.bataRefuse(true,needUpdateBarcode.join(), function (data) {
				me.onFailureInfoHandle(data.concat(pendingData),callback);
			});
		}
		//执行完成
		if (!isOut && needUpdateBarcode.length == 0) callback && callback(resultList);
	};
	
	 //获取查询字段
	Class.prototype.getFields = function(){
		var me = this,
		    cols = me.tableConfig.cols[0],
			fields = [];
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
	    return fields.join(',');
	};
	
	//核心入口
	PreSampleRefuseIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//参数功能
	    PreSampleRefuseParamsInstance = PreSampleRefuseParams.render({nodetype:me.config.nodetype});
		//初始化功能参数
		PreSampleRefuseParamsInstance.init(function(){
			//列表字段
			LIST_COLS_INFO = PreSampleRefuseParamsInstance.get('Pre_OrderRejection_DefaultPara_0006').split(',') || [];
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
	//暴露接口
	exports(MOD_NAME,PreSampleRefuseIndex);
});