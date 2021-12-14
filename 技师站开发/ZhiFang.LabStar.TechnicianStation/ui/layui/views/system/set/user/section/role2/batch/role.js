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
		MOD_NAME = 'BataRoleList';
	//内部列表+表头dom
	var TABLE_DOM = [
	    '<div class="layui-form" >',
			'<div class="layui-form-item" style="margin-bottom:0;">',
				'<div class="layui-inline"  style="height:25px">',
				  '<button type="button" id="{tableId}-btn" class="layui-btn layui-btn-xs"><i class="iconfont">&#xe713;</i>&nbsp;保存</button>', 
				'</div>',
				'<div class="layui-inline"  style="height:25px">',
				  '<button type="button" id="{tableId}-del-btn" class="layui-btn layui-btn-xs layui-bg-orange"><i class="layui-icon">&#xe640;</i>删除</button>', 
				'</div>',
			'</div>',
		'</div>',
		'<div class="{tableId}-table">',
			'<table class="layui-hide" id="{tableId}" lay-filter="{tableId}"></table>',
		'</div>',
		 '<script type="text/html" id="switchTpl">',
            '<input type="checkbox" name="IsLinked1" title="" lay-skin="primary" lay-filter="IsLinked1" {{ d.IsLinked1 == "true" ? "checked" : "" }} >',
          '</script>', 
		'<style>',
			'.layui-table-select{background-color:#5FB878;}',
		'</style>'
	];
	//获取员工角色列表
	var GET_EMP_ROLE_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?isPlanish=true';
	  //获取检验中权限列表数据
	var GET_LBRIGHT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true';
     //'删除员工小组数据权限  ---多选模式 的删除
	var DEL_DATA_RIGHT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelelteEmpSectionDataRight';
    //新增员工小组数据权限 ---多选模式 的保存
    var ADD_DATA_RIGHT_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddEmpSectionDataRight';
   
    var SECTIONID = null,EMPID=null,LINKDATA=[];
    
	//样本单列表
	var BataRoleList = {
		tableId:null,//列表ID
		tableToolbarId:null,//列表功能栏ID
		//对外参数
		config:{
			domId:null,
			height:null,
		    saveClick:function(){},
		    delClick:function(){}
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
                {field:'RBACEmpRoles_HREmployee_Id',title:'EmpID',width:150,sort:false,hide:true},
				{field:'RBACEmpRoles_RBACRole_Id',title:'ID',width:150,sort:false,hide:true},
				{field:'RBACEmpRoles_RBACRole_CName',title:'角色名称',minWidth:150,flex:1,sort:false},			
				{field:'RBACEmpRoles_RBACRole_UseCode',title:'角色编码',width:120,sort:false}
			]],
			text: {none: '暂无相关数据'}
		}
	};
	//构造器
	var Class = function(setings){  
		var me = this;
		me.config = $.extend({},me.config,BataRoleList.config,setings);
		me.tableConfig = $.extend({},me.tableConfig,BataRoleList.tableConfig);
		
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
	        var roleIDList = me.getCheckedRoleIds();
			if(!roleIDList){
				layer.msg('请先勾选角色',{icon:5});
				return false;
			}
			me.config.saveClick && me.config.saveClick(roleIDList);
		});
	    //删除
		$('#'+me.tableId+'-del-btn').on('click',function(){
			 var roleIDList = me.getCheckedRoleIds();
			if(!roleIDList){
				layer.msg('请先勾选角色',{icon:5});
				return false;
			}
			me.config.delClick && me.config.delClick(roleIDList);
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
            //并集去重复
            var list = [],arr=data;
            for(var i=0;i< data.length;i++){
            	var isExec = false;
            	var RoleId = data[i].RBACEmpRoles_RBACRole_Id;
            	for(var j=0;j<list.length;j++){
            		var  RoleId2 = list[j].RBACEmpRoles_RBACRole_Id;
            		if(RoleId == RoleId2){
            			isExec = true;
            			break;
            		}
            	}
            	if(!isExec)list.push(data[i]);
            }
        	me.uxtable.instance.reload({data:list});
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
				layer.msg(data.ErrorInfo,{icon:5});
			}
		});
	};
	//数据清空
	Class.prototype.clearData = function(){
		var me = this;
		me.uxtable.instance.reload({
			data:[]
		});
	};
	Class.prototype.onSaveClick = function(empIDList,sectionIDList,roleIDList){
		var me = this;
		var url = ADD_DATA_RIGHT_URL + '?empIDList=' + empIDList+'&sectionIDList='+sectionIDList+'&roleIDList='+roleIDList;
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				layer.msg("保存成功！", { icon: 6, anim: 0 ,time:2000});
			}else{
				layer.msg(data.ErrorInfo,{icon:5});
			}
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
	//删除小组
	Class.prototype.onDelClick = function(empIDList,sectionIDList,roleIDList){
		var me = this,
		    msg = "删除小组和对应小组的对应的角色?";
		if(!sectionIDList){
			layer.msg('请先勾选需要删除的小组',{icon:5});
			return false;
		}
		if(msg)msg+='<br/>建议取消本次删除';
		layer.confirm(msg, {
			icon: 3, title: '提示' ,
            btn: ['是:取消删除(建议)','否:执行删除'], //按钮
            yes: function (index) {
		       layer.close(index);
		    },
		    btn2:function(){
		    	me.delClick(empIDList,sectionIDList,roleIDList);
            }
       });
	};
	Class.prototype.delClick = function(empIDList,sectionIDList,roleIDList){
		var me = this;
		var url = DEL_DATA_RIGHT_URL + '?empIDList=' + empIDList+
		    '&sectionIDList='+sectionIDList+'&roleIDList='+roleIDList;
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				layer.msg("删除成功！", { icon: 6, anim: 0 ,time:2000});
				//
			}else{
				layer.msg(data.ErrorInfo, { icon: 5, anim: 6 });
			}
		});
	};
		//保存，删除按钮禁用启用
	Class.prototype.isBtnEnable = function(bo){
		var me = this;
        if(bo){
        	$("#"+me.tableId+'-btn').removeClass("layui-btn-disabled").removeAttr('disabled',true);
           	$("#"+me.tableId+'-del-btn').removeClass("layui-btn-disabled").removeAttr('disabled',true);
        }else{
           $("#"+me.tableId+'-btn').addClass("layui-btn-disabled").attr('disabled',true);
           $("#"+me.tableId+'-del-btn').addClass("layui-btn-disabled").attr('disabled',true);
        }
	};
	//核心入口
	BataRoleList.render = function(options){
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
	//暴露接口
	exports(MOD_NAME,BataRoleList);
});