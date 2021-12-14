/**
	@name：人员与站点类型与关系列表(用于人员与站点类型关系)
	@author：liangyl
	@version 2021-08-04
 */
layui.extend({
	uxtable: 'ux/table'
}).define(['uxutil','uxtable'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		uxtable = layui.uxtable;
	//获取站点类型和人员关系数据
	var GET_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeUserByHQL?isPlanish=true';
    //删除数据服务
	var DEL_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelBHostTypeUser';
     //获取所有站点类型
	var GET_HOSTTYPE_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeByHQL?isPlanish=true';
    var table_ind = null;
    var EMP_ENUM ={};
    
    var delErrorCount = 0,
		delCount = 0,
		delLength = 0;

	var EmpHostTypeList = {
		//参数配置
		config:{
            page: true,
			limit: 50,
			loading : true,
			cols:[[
				{type: 'checkbox', fixed: 'left'},
				{field:'BHostTypeUser_Id',title:'ID',width:150,sort:true,hide:true},
				{field:'BHostTypeUser_HostTypeName',title:'站点类型',minWidth:180,flex:1,templet: function (data) {
					var v = data.BHostTypeUser_HostTypeID;
					if(EMP_ENUM != null)v = EMP_ENUM[v];
                    return v;
                }},
				{field:'BHostTypeUser_HostTypeID',title:'员工ID',width:150,hide:true}
			]],
			text: {none: '暂无相关数据' }
		}
	};
	
	var Class = function(setings){
		var me = this;
		me.config = $.extend({
			parseData:function(res){//res即为原始返回的数据
				if(!res) return;
				var data = res.ResultDataValue ? $.parseJSON(res.ResultDataValue) : {};
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": data.list || []
				};
			},
			afterRender:function(that){
				var filter = $(that.config.elem).attr("lay-filter");
				if(filter){
					//监听行双击事件
					that.table.on('row(' + filter + ')', function(obj){
						//标注选中样式
	                    obj.tr.addClass('layui-table-click').siblings().removeClass('layui-table-click');
					});
					//监听工具条
					that.table.on('tool(' + filter + ')', function(obj){
					    var data = obj.data;
					    if(obj.event === 'copy'){
					    	me.onCopyClik();
					    }
					});
				}
			}
		},me.config,EmpHostTypeList.config,setings);
	};
	
	Class.pt = Class.prototype;
	//数据加载
	Class.pt.loadData = function(EMPID){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	    table_ind.EMPID = EMPID;
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_LIST_URL;
		var where = 'bhosttypeuser.EmpID='+table_ind.EMPID;
		var whereObj ={"where":where};
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(',')
			})
		});
	};
	Class.pt.initListeners= function(){
		var me = this;
		//按钮事件
		var active = {
			add: function() {//新增关系、
			    me.openAddLink();
			},
			copy: function() {//复制
				me.openCopy();
			},
			del: function() {//删除
				me.onDelClick();
			}
		};
		$('.UserHostType .layui-btn').on('click', function() {
			var type = $(this).data('type');
			active[type] ? active[type].call(this) : '';
		});
	};
	//删除
	Class.pt.onDelClick = function(){
		var me = this;
		var checkStatus = table_ind.table.checkStatus('emp_hosttype_table');
        var data = checkStatus.data;
        if(data.length==0){
        	layer.msg("请先选择行再删除");
			return false;
		}
        delErrorCount = 0;
		delCount = 0;
		delLength = data.length;
		
        layer.confirm('确定删除选中项?', { icon: 3, title: '提示' }, function (index) {
            for(var i=0;i<data.length;i++){
           	   me.delById(data[i].BHostTypeUser_Id,i);
            }
        });
	};
	Class.pt.delById = function(id,index){
		var me = this;
		setTimeout(function() {
   			uxutil.server.ajax({
   				url: DEL_URL+'?id=' + id
   			},function(data) {
				//隐藏遮罩层
				layer.closeAll('loading');
				if (data.success) {
				    delCount++;
				} else {
					delErrorCount++;
				}				
				if (delCount + delErrorCount == delLength) {
					if (delErrorCount == 0){
						layer.msg('刪除成功!',{icon:6,time:2000});
						table_ind.loadData(table_ind.EMPID);
					}else{
						layer.msg(data.ErrorInfo, { icon: 5});
					}
				}
			});
		}, 100 * index);
	};
	//弹出新增
	Class.pt.openAddLink= function(){
		var me = this;
	    var win = $(window),
			maxHeight = win.height()-80,
			height = maxHeight > 480 ? maxHeight : 480;
		layer.open({
            type: 2,
            area: ['800px', height+'px'],
            fixed: false,
            maxmin: false,
            title:'新增站点与人员关系',
            content: 'emphosttype/addlink.html?EMPID='+table_ind.EMPID
        });
	};
	//弹出复制
	Class.pt.openCopy= function(){
		var me = this;
		 var win = $(window),
			maxHeight = win.height()-80,
			height = maxHeight > 480 ? maxHeight : 480;
		layer.open({
            type: 2,
            area: ['440px', height+'px'],
            fixed: false,
            maxmin: false,
            title:'复制个人站点类型',
            content: 'emphosttype/copy.html?EMPID='+table_ind.EMPID
        });
	};
    //获取所有站点类型
	Class.pt.HostTypeList = function(callback){
		var fields = ['BHostType_Id','BHostType_CName'],
			url = GET_HOSTTYPE_LIST_URL;
		url += '&fields='+fields.join(',');
		
		uxutil.server.ajax({
			url:url,
			async:false
		},function(data){
			if(data){
				var list = (data.value || {}).list || [];
				callback(list);
			}else{
				layer.msg(data.ErrorInfo, { icon: 5});
			}
		});
	};
	//主入口
	EmpHostTypeList.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		table_ind = result;
		result.loadData = me.loadData;
		me.HostTypeList(function(list){
			for(var i=0;i<list.length;i++){
				EMP_ENUM[list[i].BHostType_Id] = list[i].BHostType_CName;
			}
		});
		me.initListeners();
		return result;
	};
	//新增保存成功后刷新
	function afterUpdate(){
		table_ind.loadData(table_ind.EMPID);
	}
	//传给子窗体(已选站点)
	function ChildHostTypeData(){
		var arr = table_ind.table.cache.emp_hosttype_table;
		return arr;
	}
	window.afterUpdate = afterUpdate;
	window.ChildHostTypeData= ChildHostTypeData;
	//暴露接口
	exports('EmpHostTypeList',EmpHostTypeList);
});