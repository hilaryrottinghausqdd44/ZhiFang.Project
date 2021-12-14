/**
 * @name：modules/pre/sample/inspect/index 样本送检
 * @author：liangyl
 * @version 2020-09-23
 */
layui.extend({
	uxutil:'ux/util',
	PreSampleGetherBasicParams:'modules/pre/sample/gether/basic/params',
	PreSampleContentIndex:'modules/pre/sample/gether/basic/content',
	SearchBar:'modules/pre/sample/gether/basic/search'
}).define(['uxutil','form','PreSampleContentIndex','PreSampleGetherBasicParams','SearchBar'],function(exports){
	"use strict";
	
	var $ = layui.$,
		form = layui.form,
		uxutil = layui.uxutil,
		table = layui.table,
		PreSampleContentIndex = layui.PreSampleContentIndex,
		PreSampleGetherBasicParams = layui.PreSampleGetherBasicParams,
		SearchBar = layui.SearchBar,
		MOD_NAME = 'PreSampleGetherIndex';
	
	//列表字段：格式=BarCode&条码号&100&show,OrderExecTime&医嘱指定执行时间&100&show,
	var LIST_COLS_INFO = null;
	//后台获取字段数组
	var LIST_FIELDS = null;
	//样本采集_根据条码号获取样本数据_更新样本状态
	var BARCODE_LIST_URL = uxutil.path.ROOT + "/ServerWCF/LabStarPreService.svc/LS_UDTO_PreSampleGatherAndUpdateStateByBarCode";
	//送检信息特定字段匹配数据临时存储（第一次扫码的字段内容）
	var FILED_MATE = '';
	//模块DOM
	var MOD_DOM = [
		'<div class="layui-form {domId}-grid-div" id="{domId}-form" lay-filter="{domId}-form" style="margin-bottom:0; padding-bottom:0;">',
			'<div class="layui-form-item" style="margin-bottom:0;">',
			   '<div class="layui-inline"> ',
                    '<label class="layui-form-label">条码号:</label> ',
					'<div class="layui-input-inline">',
					    '<input type="text" name="{domId}-barCode" id="{domId}-barCode"  placeholder="请扫描条码" autocomplete="off" class="layui-input" />',
					'</div>',
				'</div>',
				'<div class="layui-inline layui-hide" id="{domId}-accept-config"> ',
                    '<label class="layui-form-label"  id="{domId}-Pre-OrderGether-DefaultPara-0003">:</label> ',
					'<div class="layui-input-inline">',
					    '<input type="text" name="{domId}-accept" id="{domId}-accept" autocomplete="off"  placeholder="回车核收" class="layui-input" />',
					'</div>',
				'</div>',
				'<div class="layui-inline">', 
                    '<label class="layui-form-label">采样人:</label>', 
				    '<div class="layui-input-inline">',
						'<input type="text" name="{domId}-userName" id="{domId}-userName" readonly="readonly" autocomplete="off" class="layui-input" />',
				    '</div>',
				'</div>',
				 '<div class="layui-inline" style="float:right;">',
                  '<input type="checkbox" id="{domId}-show-search-toolbar" lay-filter="{domId}-show-search-toolbar" title="查询" lay-skin="primary" />',  
				'</div>',
				'<div class="layui-inline" style="float:right;padding-left: 10px;">',
				   '<label id="{domId}-num" style="color: blue;font-size: 18px;font-weight: bold ;padding-right: 10px;">数量:0</label>', 
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
		    '.layui-form-item .layui-input-inline{float: left;width:110px;  margin-right: 0px;}',
			'.{domId}-grid-div{padding:1px;margin-bottom:0px;border-bottom:1px solid #e6e6e6;background-color:#f2f2f2;}',
			'.layui-form-onswitch-red{border-color:#FF5722;background-color:#FF5722;}',
		'</style>'
	
	].join('');
	
	//样本单实例
	var PreSampleContentIndexInstance = null;
	//参数实例
	var PreSampleGetherBasicParamsInstance = null;
	//查询工具栏
	var SearchBarInstance = null;
	
	var PreSampleGetherIndex = {
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
		me.config = $.extend({},me.config,PreSampleGetherIndex.config,setings);
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		$('#' + me.config.domId).html('');
		$('#' + me.config.domId+'-grid-div').html('');
		var html = MOD_DOM.replace(/{domId}/g,me.config.domId);
		$('#' + me.config.domId).append(html);

	    //核收字段及名称   
		var receive = PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0003');
		if(receive){
			$('#' + me.config.domId+'-accept-config').removeClass('layui-hide');
			//"ParaValue": "lispatient.PatNo&病历号&Text
			var arr = receive.split('&');
            $('#' + me.config.domId+'-Pre-OrderGether-DefaultPara-0003').text(arr[1]+':');
            //具体核收字段
            $('#' + me.config.domId+'-accept').attr("data-type",arr[0]);
		}
		
		var win = $(window),
		    maxheight = win.height();
        var height = maxheight - $("#"+me.config.domId+'-form').height()-$("#"+me.config.domId+'-SearchTool').height()-60;

        //查询工具栏
		SearchBarInstance = SearchBar.render({
			domId: me.config.domId+'-SearchTool',
			height:'30px',
			//查询时间字段
			DateType:PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0008'),
			nodetype:me.config.nodetype, //站点类型
			searchClickFun:function(where){ //查询按钮查询事件
				if(!where){
					layer.msg("条件为空不能查询！",{icon:5});
				    return false;
				}
				PreSampleContentIndexInstance.onSearch(where,function(list){
    	            me.Size();
				});
			}
		});
		//样本信息列表实例 
		PreSampleContentIndexInstance = PreSampleContentIndex.render({
			domId: me.config.domId+'-Content',
			height:height,
			cols:LIST_COLS_INFO,
			MODELTYPE:2,
			//是否自动打印采样清单
			IsAutoPrint:PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0005'),
			//是否批量确认模式验证采集人
    	    IsConfirmUser:PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0006'),
			PrinterName:PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0010'),
			IsPreview:PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0011'),
			nodetype:me.config.nodetype //站点类型
		});
			//采样人显示
		var userName = uxutil.cookie.get(uxutil.cookie.map.USERNAME);
		$('#' + me.config.domId+'-userName').val(userName);
		form.render();
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this;
		$("#"+me.config.domId+"-barCode").focus();
		//扫码,回车事件
	    $("#"+me.config.domId+"-barCode").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	var barcode = $("#"+me.config.domId+"-barCode").val();
	        	if(barcode){
	        		//去掉前后空格
	        		barcode = barcode.replace(/(^\s*)|(\s*$)/g, "");
	        	    //判断条码号是否已扫码
		        	var isExist = PreSampleContentIndexInstance.isExistBarcode(barcode);
		        	if(isExist){
		        		//清空扫码输入框数据
				    	$("#"+me.config.domId+"-barCode").val('');
				        $("#"+me.config.domId+"-barCode").focus();
		        		return false;
		        	}
		        	//必须先有病人信息才能匹配
		    		if(PreSampleContentIndexInstance.getMateList().length==0){
		    			layer.msg('请先查询未采集的样本!',{icon:5});
		    			return false;
		    		}
		    		//先判断右侧的列表存不存在
		        	var isMate = PreSampleContentIndexInstance.isMateData(barcode);
		        	if(!isMate) {
		        		layer.msg('没有找到匹配的样本!',{icon:5});
		        		return false;
		        	}
		        	var isConstraintUpdate = true;//强制更新
                    var isUpdate = true;//更新
                    $("#"+me.config.domId+"-barCode").blur();
		        	PreSampleContentIndexInstance.onBarcode(barcode,isUpdate,isConstraintUpdate,function(list,TipMsg,alterModel){
				        if (TipMsg.length > 0)
					       layer.alert(TipMsg.join('<br>'), { icon: 0, anim: 0 });
					    if(list.length>0){
					    	me.addRow(list);
					    	if(!alterModel)layer.msg('条码号:'+barcode+'样本采集成功',{icon:6,time:2000});
	                        //采样确认打印清单
						    var isExect = PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0005');
	                        if(isExect=='1')PreSampleContentIndexInstance.onListPrint(list);
					    }
					});
		        }else{
		        	layer.msg('条码号不能为空,请扫码!',{icon:5});
		        }
	            return false;
	        }
	    });
	    //-参数-特定字段核收 操作
	    $("#"+me.config.domId+"-accept").on('keydown', function (event) {
	        if (event.keyCode == 13) {
	        	//查询和核收需先清空数据
	        	me.clearData();
	        	var value = $("#"+me.config.domId+"-accept").val();
	        	var receiveType = $("#"+me.config.domId+"-accept").attr('data-type');
	        	if(value){
	        	    PreSampleContentIndexInstance.onReceive(value,receiveType,function(list){
	        	    	$("#"+me.config.domId+"-accept").val('');
	        	    	$("#"+me.config.domId+"-accept").focus();
	        	    	PreSampleContentIndexInstance.mLoadData(list);
	        	    });
	        	}else{
	        		layer.msg('核收内容不能为空!',{icon:5});
	        	}
	            return false;
	        }
	    });
	    form.on('checkbox('+me.config.domId+'-show-search-toolbar)', function(data){
			me.showToolbar(data.elem.checked);
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
	/**大小改变 */
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
	//打印清单
    Class.prototype.onListPrint = function(){
    	var me = this;
    	  //选择行
	    var CheckedList = PreSampleContentIndexInstance.getCheckedList();
	    if(CheckedList.length==0){
	    	layer.msg('请勾选行数据',{ icon: 0 });
            return false;
	    }
	    var list = [];
	    for(var i in CheckedList){
	    	if(CheckedList[i].LisBarCodeFormVo_IsConfirm=="1"){ //已确认过的条码
	    		list.push(CheckedList[i]);
	    	}
	    }
	    if(list.length==0){
	    	layer.msg('请先采集确认再打印清单 ',{ icon: 0 });
            return false;
	    }
    	PreSampleContentIndexInstance.onListPrint(list);
    };
      //打包清单
    Class.prototype.packNoByBarCode= function(){
    	PreSampleContentIndexInstance.onpackNoByBarCode();
    };
    //新增行  list新增的数据行（如果已确认需打上确认标记再传进来）
     Class.prototype.addRow = function(list){
     	var me = this;
    	//新增数据行
		PreSampleContentIndexInstance.loadData(list);
		//清空扫码输入框数据
    	$("#"+me.config.domId+"-barCode").val('');
        $("#"+me.config.domId+"-barCode").focus();
        //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
	};
  
    //数据清空
    Class.prototype.clearData = function(){
    	var me = this;
    	PreSampleContentIndexInstance.clearData();
    	 //数量显示
    	$("#"+me.config.domId+"-num").text("数量:"+PreSampleContentIndexInstance.getNum());
    };
   
	//核心入口
	PreSampleGetherIndex.render = function(options){
		var me = new Class(options);
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//参数功能
	    PreSampleGetherBasicParamsInstance = PreSampleGetherBasicParams.render({nodetype:me.config.nodetype});
		//初始化功能参数
		PreSampleGetherBasicParamsInstance.init(function(){
			//列表字段
			LIST_COLS_INFO = PreSampleGetherBasicParamsInstance.get('Pre_OrderGether_DefaultPara_0013').split(',') || [];
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
	function afterUpateRevocation(data, barCode){
		//被撤销确认的条码号
		if (data && data[0]) {
			var obj = JSON.parse(data[0]);
			if (obj[barCode] == 'true') {
				layer.msg("撤销采集成功", { icon: 6, time: 2000 });
				PreSampleContentIndexInstance.onRevocation(barCode);
			} else if (obj[barCode] == 'false'){
				layer.alert("以下条码撤销失败，请检查样本状态：" + BarCodeConfirmError);
			}
		}
	}
	window.afterUpateRevocation = afterUpateRevocation;
	//暴露接口
	exports(MOD_NAME,PreSampleGetherIndex);
});