/**
 * @name：角色人员关系列表 
 * @author：liangyl
 * @version 2021-11-03
 */
layui.extend({ 
}).define(['uxutil','uxtable','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxtable = layui.uxtable,
		uxutil = layui.uxutil,
		form = layui.form,
		MOD_NAME = 'RoleList';
	//内部列表+表头dom
	var TABLE_DOM = [
	    '<div class="layui-form" >',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<div class="layui-inline"  style="height:25px">',
				  '<button type="button" id="{tableId}-btn" class="layui-btn layui-btn-xs"><i class="iconfont">&#xe713;</i>&nbsp;保存</button>', 
				'</div>',
			'</div>',
		'</div>',
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		 '<script type="text/html" id="switchTpl">',
            '<input type="checkbox" name="IsLinked" title="" lay-skin="primary" lay-filter="IsLinked" {{ d.IsLinked == "true" ? "checked" : "" }} >',
          '</script>', 
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//获取员工角色列表
	var GET_EMP_ROLE_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true';
	  //获取检验中权限列表数据
	var GET_LBRIGHT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true';
    //新增检验中权限/	
    var ADD_LBRIGHT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBRight";
    //删除检验中权限/	
    var DEL_LBRIGHT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBRight";
   
    var SECTIONID = null,EMPID=null,LINKDATA=[];
    
	//样本单列表
	var RoleList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null
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
			    {type: 'numbers',title: '行号',fixed: 'left'},
                {field:'IsLinked',title:'勾选',width:70,sort:false,align:'center', fixed: 'left',
                templet:'#switchTpl'},
				{field:'RBACEmpRoles_RBACRole_Id',title:'ID',width:150,sort:false,hide:true},
				{field:'RBACEmpRoles_RBACRole_CName',title:'角色名称',minWidth:150,flex:1,sort:false},			
				{field:'RBACEmpRoles_RBACRole_UseCode',title:'角色编码',width:120,sort:false},
				{field:'IsEdit',title:'是否有修改',width:120,sort:true,hide:true}
			]],
			text: {none: '暂无相关数据'}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,RoleList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,RoleList.tableConfig);
		
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
		var me = this;
		//保存
		$('#'+me.tableId+'-btn').on('click',function(){
			me.onSaveClick();
		});

		form.on('checkbox(IsLinked)', function (data) {
        	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		  	var tableCache = me.uxtable.table.cache[me.tableId];
		  	//改变后的数据
	        var rowObj = tableCache[dataindex].IsEdit;
	        if(rowObj)delete rowObj.IsLinked;
	        if(!rowObj)rowObj ={};
	        if(data.value)rowObj.IsLinked=data.elem.checked;
	    	layui.$.extend(me.uxtable.table.cache[me.tableId][dataindex],{['IsEdit'] : rowObj});
	    	layui.$.extend(me.uxtable.table.cache[me.tableId][dataindex],{['IsLinked'] : data.elem.checked});
	    });
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
	Class.prototype.onSearch = function(id){
		var me = this;
		EMPID = id
		var whereObj ={};
        me.loadData(EMPID,function(data){
    		for(var i=0;i<data.length;i++){
			   data[i].IsLinked = "true";
			}
        	me.uxtable.instance.reload({data:data});
        });
	};
    
	//数据加载
	Class.prototype.loadData = function(EmpID,callback){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_EMP_ROLE_LIST_URL,
			type:'get',
			async:false,
			data:{
				page:1,
				limit:1000,
				fields:me.getFields(),
				where: 'rbacemproles.HREmployee.Id in (' + EmpID+')',
				sort: JSON.stringify(me.tableConfig.defaultOrderBy)
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		});
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
		//还原关系设置
		me.changeRoleLink([],[],function(data){
		});
	};
	//获取修改过的行记录
    Class.prototype.getModifiedRecords =  function(){
    	var me = this,
    	    list=[];
    	//获取列表数据
    	var tableCache = me.uxtable.table.cache[me.tableId];
	    for(var i = 0;i<tableCache.length;i++){
	    	//找到修改过数据的行
	    	if(tableCache[i].IsEdit){
	    		var data = {};
				var arr = Object.keys(tableCache[i].IsEdit);
				if(arr.length != 0){
					list.push(tableCache[i]);
				}
	    	}
	    }
	    return list;
    };
    /**添加一条关系*/
	Class.prototype.addLink = function(rec,id){
		var me = this;
        var entity = {
			EmpID:EMPID,
        	Operator:uxutil.cookie.get(uxutil.cookie.map.USERNAME),
        	OperatorID:uxutil.cookie.get(uxutil.cookie.map.USERID),
            LBSection: { Id:SECTIONID, DataTimeStamp: [0,0,0,0,0,0,0,0] },
            RoleID:id
		};
		//显示遮罩层
		var config = {
			type: "POST",
			url: ADD_LBRIGHT_URL,
			data: JSON.stringify({entity:entity})
		};
		uxutil.server.ajax(config, function(data) {
			if (data.success) {
				me.saveCount++;
			} else {
				me.saveErrorCount++;
			}				
			me.onSaveEnd();
		});
	};
	/**删除一条关系*/
	Class.prototype.delLinkById = function(rec,id){
		var me = this;
		var url = DEL_LBRIGHT_URL + '?id=' + id;
		
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				me.saveCount++;
			}else{
				me.saveErrorCount++;
			}
			me.onSaveEnd();
		});
	};
	Class.prototype.onSaveEnd = function(){
		var me = this;
		if (me.saveCount + me.saveErrorCount == me.saveLength) {
			layer.closeAll('loading');//隐藏遮罩层
			if (me.saveErrorCount == 0){
				me.linkData(EMPID,SECTIONID);
				layer.msg('保存成功!',{ icon: 6, anim: 0 ,time:2000 });
			}else{
				layer.msg('保存失败!',{ icon: 5, anim: 0 });
			}
		}
	};
	Class.prototype.onSaveClick = function(EmpID,SectionID){
		var me = this;
		var records = me.getModifiedRecords();
		//显示遮罩
		if(records.length==0)return;
		var indexs=layer.load();
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		for(var i=0;i<records.length;i++){
			var rec = records[i];
			var id = rec.RBACEmpRoles_RBACRole_Id;
			var IsLinked = rec.IsLinked;
			var LinkId = rec.LinkId;
			if(LinkId && !IsLinked){
				me.delLinkById(rec,LinkId);
			}else{
				me.addLink(rec,id);
			}
		}
	};
	//获取关系数据
	Class.prototype.getLinkData = function(callback){
		var me = this;
		var loadIndex = layer.load();//开启加载层
		uxutil.server.ajax({
			url:GET_LBRIGHT_LIST_URL,
			type:'get',
			data:{
				page:1,
				limit:1000,
				fields:'LBRight_Id,LBRight_RoleID',
				where: 'lbright.EmpID in (' + EMPID+') and lbright.RoleID is not null and lbright.LBSection.Id in('+SECTIONID+")"
			}
		},function(data){
			layer.close(loadIndex);//关闭加载层
			if(data.success){
				callback((data.value || {}).list || []);
			}else{
				layer.msg(data.msg,{icon:5});
			}
		},true);
	};
	/**勾选关系赋值*/
	Class.prototype.changeRoleLink = function(list,linkList,callback){
		var me = this,
		    len =  list.length;
		var arr = [];
		for(var i=0;i<len;i++){
		   list[i].IsLinked = "false";
		   list[i].IsEdit="";
		   list[i].LinkId="";
           for(var j=0;j<linkList.length;j++){
           	    if(linkList[j].LBRight_RoleID == list[i].RBACEmpRoles_RBACRole_Id){
           	  	    list[i].IsLinked = "true";
           	  	    list[i].LinkId = linkList[j].LBRight_Id;
           	  	    break;
           	    }
           }
           arr.push(list[i]);
		}
		callback(arr);
	};
	//关系还原
	Class.prototype.linkData= function(id,sid){
		var me = this;
		EMPID =id;
		SECTIONID = sid
        var list = me.uxtable.table.cache[me.tableId];
		//获取关系数据
		me.getLinkData(function(linkList){
			//还原关系设置
			me.changeRoleLink(list,linkList,function(data){
				me.uxtable.instance.reload({
					data:data
				});
			});
		});
	};	
	//获取勾选的数组
	Class.prototype.getCheckedRoleIds = function(){
		var me = this;
		var checkedList = me.uxtable.table.checkStatus(me.tableId).data;
		var ids = [];
		for(var i in checkedList){
			ids.push(checkedList[i].RBACEmpRoles_RBACRole_Id);
		}
		return ids.join(',');
	};
		//保存，删除按钮禁用启用
	Class.prototype.isBtnEnable = function(bo){
		var me = this;
        if(bo){
        	$("#"+me.tableId+'-btn').removeClass("layui-btn-disabled").removeAttr('disabled',true);
        }else{
           $("#"+me.tableId+'-btn').addClass("layui-btn-disabled").attr('disabled',true);
        }
	};
	//核心入口
	RoleList.render = function(options){
		var me = new Class(options);
		
		if(!me.config.domId){
			window.console && console.error && console.error(MOD_NAME + "模块组件错误：参数config.domId缺失！");
			return me;
		}
		//初始化HTML
		me.initHtml();
		me.uxtable = uxtable.render(me.tableConfig);
		//监听事件
		me.initListeners();
		
		return me;
	};
	//暴露接口
	exports(MOD_NAME,RoleList);
});