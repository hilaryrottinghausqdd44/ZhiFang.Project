/**
 * @name：选择小组列表 
 * @author：liangyl
 * @version 2021-11-03
 */
layui.extend({
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		MOD_NAME = 'BataSectionsList';
	//内部列表+表头dom
	var TABLE_DOM = [
	    '<div class="layui-form" >',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<div class="layui-inline"  style="height:25px">',
				  '<button type="button" id="{tableId}-btn" class="layui-btn layui-btn-xs"><i class="iconfont">&#xe664;</i>&nbsp;选择小组</button>', 
				'</div>',
				'<div class="layui-inline" id="{tableId}-del-row" style="height:25px">',
				  '<button type="button" id="{tableId}-del-btn" class="layui-btn layui-btn-xs layui-bg-orange"><i class="layui-icon">&#xe640;</i>删除</button>', 
				'</div>',
			'</div>',
		'</div>',
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//获取检验中权限列表数据 -
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_QueryCommonSectionRightByEmpID?isPlanish=true';
	 //删除员工小组数据权限
	var DEL_URL = uxutil.path.ROOT  + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelelteEmpSectionDataRight';

	//员工ID
	var EmpID = null; 
	//员工姓名
	var EmpName = null;
	var table_ind = null;
	var DATA_LIST =[];
	
	//样本单列表
	var BataSectionsList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
			//行选择触发事件
			checkClickFun:function(){},
			done:function(){},
			sectionClickFun:function(){}
		},
		//内部列表参数
		tableConfig:{
			elem:null,
			size:'sm',//小尺寸的表格
			height:'full-110',
			where:{},
			page: false,
			limit: 5000,
			limits: [50,100, 200, 500, 1000, 1500],
		    cols:[[
	            {type: 'checkbox', fixed: 'left'},
				{field:'LBSection_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'LBSection_CName',title:'小组名称',minWidth:120,flex:1,sort:true}
			]],
			text: {none: '暂无相关数据'}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,BataSectionsList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,BataSectionsList.tableConfig);
		
		if(me.config.height){
			me.tableConfig.height = me.config.height;
		}
		me.tableId = me.config.domId + "-table";
		me.tableConfig.elem = "#" + me.tableId;
		//数据渲染完的回调
		me.tableConfig.done = function(res, curr, count){
			var bo = true;
			if(count==0)bo = false;
			me.isBtnEnable(bo);
			if(count>0)me.onClickFirstRow();
			me.config.done && me.config.done(res, curr, count);
		};
	};
	//初始化HTML
	Class.prototype.initHtml = function(){
		var me = this;
		var html = TABLE_DOM.join("").replace(/{tableId}/g,me.tableId).replace(/{tableToolbarId}/g,me.tableToolbarId);
		$('#' + me.config.domId).append(html);
	};
	//监听事件
	Class.prototype.initListeners = function(){
		var me = this,
		    iTime = null;
			//触发行单击事件
		me.uxtable.table.on('row(' + me.tableId + ')', function(obj){
			//标注选中样式
	        obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
	        	me.config.rowClickFun && me.config.rowClickFun(obj);
		});
		me.uxtable.table.on('checkbox(' + me.tableId + ')', function(obj){
            clearTimeout(iTime);
            iTime = setTimeout(function () {
            	me.config.checkClickFun && me.config.checkClickFun(me.getCheckedList());
            }, 500);
		});
		//选择小组
	    $('#'+me.tableId+'-btn').on('click',function(){
	    	getChildrenData();
	    	me.config.sectionClickFun && me.config.sectionClickFun();
//	    	me.OpenWin();
		});
		//删除员工小组数据权
	    $('#'+me.tableId+'-del-row').on('click',function(){
	    	var list = me.getCheckedList();
			if(list.length==0){
				layer.msg('请勾选需要删除小组',{icon:5});
				return false;
			}
	    	me.onDelClick();
		});
	};
		//默认选中第一行
	Class.prototype.onClickFirstRow = function(){
		var me = this;
		setTimeout(function () {
			$("#" + me.tableId + "+div").find('.layui-table-main tr[data-index="0"]').click();
		}, 0);
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
    //数据加载
	Class.prototype.onSearch = function(empIDList,EmpNameList){
		var me = this;
		EmpID = empIDList;
		EmpName = EmpNameList;
		me.loadData(empIDList);
	};
	//数据加载
	Class.prototype.loadData = function(empIDList){
		var me = this;
		me.uxtable.instance.reload({
			url: GET_LIST_URL,
			where:$.extend({},{},{
				empIDList:empIDList,
				fields:me.getFields()
			})
		});
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	//获取勾选的数组
	Class.prototype.getCheckedList = function(){
		var me = this;
		var checkedList = me.uxtable.table.checkStatus(me.tableId).data;
		return checkedList;
	};
	//弹出小组选择框
	Class.prototype.OpenWin = function(){
		var me = this;
		
		var win = $(window),
			maxWidth = win.width()-100,
			maxHeight = win.height()-80,
			width = maxWidth > 800 ? maxWidth : 800,
			height = maxHeight > 600 ? maxHeight : 600;
		var isF =true;
		layer.open({
			title:'选择小组',
			type:2,
			content: '../role2/batch/transfer/app.html',
			maxmin:true,
			toolbar:true,
			resize:true,
			area:[maxWidth+'px',maxHeight+'px'],
			success: function(layero, index){
	        },
	        end: function () {
	        	if(isF)me.loadData(EmpID);
            },
	        cancel: function (index, layero) {
	        	isF = false;
	        	parent.layer.closeAll('iframe');
            }
		});
	};
	
    //删除小组
	Class.prototype.onDelClick = function(){
		var me = this,
		    msg = "删除小组和对应小组的对应的角色?";
		if(msg)msg+='<br/>建议取消本次删除';
		layer.confirm(msg, {
			icon: 3, title: '提示' ,
            btn: ['是:取消删除(建议)','否:执行删除'], //按钮
            yes: function (index) {
		       layer.close(index);
		    },
		    btn2:function(){
		    	me.delLinkById();
            }
       });
	};
	/**删除一条关系*/
	Class.prototype.delLinkById = function(){
		var me = this;
		var list = me.getCheckedList();
		if(list.length==0){
			layer.msg('请勾选需要删除小组',{icon:5});
			return false;
		}
		var ids =[],names =[];
		for(var i in list){
			ids.push(list[i].LBSection_Id);
		}
		var url = DEL_URL + '?empIDList=' + EmpID+'&sectionIDList='+ids.join(',');
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				layer.msg('删除成功!',{ icon: 6, anim: 0 ,time:2000 });
				me.onSearch(EmpID,EmpName);
			}else{
				layer.msg(data.ErrorInfo,{ icon: 5, anim: 6 });
			}
		});
	};
			//删除按钮禁用启用
	Class.prototype.isBtnEnable = function(bo){
		var me = this;
        if(bo){
           	$("#"+me.tableId+'-del-btn').removeClass("layui-btn-disabled").removeAttr('disabled',true);
        }else{
           $("#"+me.tableId+'-del-btn').addClass("layui-btn-disabled").attr('disabled',true);
        }
	};
	//核心入口
	BataSectionsList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		me.uxtable.instance.reload({
			data:[]
		});
		//监听事件
		me.initListeners();
		
		return me;
	};
	function getChildrenData(){
		
		return {Ids:EmpID,Names:EmpName};
	};
	window.getChildrenData = getChildrenData;	
	//暴露接口
	exports(MOD_NAME,BataSectionsList);
});