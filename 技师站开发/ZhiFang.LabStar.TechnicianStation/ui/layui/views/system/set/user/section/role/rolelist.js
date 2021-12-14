/**
	@name：角色列表
	@author：liangyl
	@version 2019-11-14
 */
layui.extend({
}).define(['uxutil','uxtable','table','form'],function(exports){
	"use strict";
	
	var $ = layui.$,
		uxutil = layui.uxutil,
		table=layui.table,
		form = layui.form,
		uxtable = layui.uxtable,
		ELEM_CELL = '.layui-table-cell';
	
		
	//获取角色列表数据
	var GET_ROLE_LIST_URL = uxutil.path.LIIP_ROOT + '/ServerWCF/RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?isPlanish=true';
    //获取检验中权限列表数据
	var GET_LBRIGHT_LIST_URL = uxutil.path.ROOT + '/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?isPlanish=true';
    //新增检验中权限/	
    var ADD_LBRIGHT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_AddLBRight";
    //删除检验中权限/	
    var DEL_LBRIGHT_URL = uxutil.path.ROOT + "/ServerWCF/LabStarBaseTableService.svc/LS_UDTO_DelLBRight";
 
    var SECTIONID = null,EMPID=null,LINKDATA=[];
    
	var roletable = {
		//参数配置
		config:{
            page: false,
			limit: 1000,
			loading : true,
			defaultLoad:true,
			data:[],
			defaultOrderBy:"[{property: 'RBACRole_DispOrder',direction: 'ASC'}]",
			cols:[[
//              {type: 'checkbox', fixed: 'left'},
			    {type: 'numbers',title: '行号',fixed: 'left'},
                {field:'IsLinked',title:'勾选',width:70,sort:false,align:'center', fixed: 'left',templet: '#switchTpl'},
                {field:'LinkId',title:'关系主键ID',width:150,hide:true},
				{field:'RBACRole_Id',title:'ID',width:150,sort:false,hide:true},
				{field:'RBACRole_CName',title:'角色名称',minWidth:150,flex:1,sort:false},			
				{field:'RBACRole_UseCode',title:'角色编码',width:120,sort:false},
				{field:'IsEdit',title:'是否有修改',width:120,sort:true,hide:true}

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
				var list = [];
				for(var i in data.list){
					data.list[i].IsLinked = "false";
					data.list[i].IsEdit="";
					list.push(me.changeData(data.list[i]));
				}
				return {
					"code": res.success ? 0 : 1, //解析接口状态
					"msg": res.ErrorInfo, //解析提示文本
					"count": data.count || 0, //解析数据长度
					"data": list || []
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
				}
			}
		},me.config,roletable.config,setings);
	};
	
	Class.pt = Class.prototype;
	
	//数据处理，需要时可重写
	Class.pt.changeData = function(data){
		return data;
	};
	//数据加载
	Class.pt.loadData = function(whereObj){
		var  me = this,
    		cols = me.config.cols[0],
			fields = [];
	     
		for(var i in cols){
			if(cols[i].field)fields.push(cols[i].field);
		}
		var url = GET_ROLE_LIST_URL;
		me.instance.reload({
			url:url,
			where:$.extend({},whereObj,{
				fields:fields.join(','),
				sort:me.config.defaultOrderBy
			})
		});
	};
	//主入口
	roletable.render = function(options){
		var me = new Class(options);
		var result = uxtable.render(me.config);
		
		result.loadData = me.loadData;
		if(me.config.defaultLoad){
			//加载数据
			result.loadData(me.config.where);
		}
        result.onSaveClick = me.onSaveClick;
        result.getLinkData = me.getLinkData;
        me.initListeners(result);
		return result;
	};
	
	Class.pt.onSaveClick = function(EmpID,SectionID){
		var me = this;
		SECTIONID = SectionID,EMPID=EmpID;
		var filter = $(me.config.elem).attr("lay-filter");
		var records = Class.pt.getModifiedRecords(me);
       
		//显示遮罩
		if(records.length==0)return;
		var indexs=layer.load(2);
		me.saveErrorCount = 0;
		me.saveCount = 0;
		me.saveLength = records.length;
		
		for(var i=0;i<records.length;i++){
			var rec = records[i];
			var id = rec.RBACRole_Id;
			var IsLinked = rec.IsLinked;
			var LinkId = rec.LinkId;
			if(LinkId && !IsLinked){
				Class.pt.delLinkById(rec,LinkId,me);
			}else{
				Class.pt.addLink(rec,id,me);
			}
		}
	};

	Class.pt.initListeners = function(result){
		var me = this;
		//修改行写入标记
		
		form.on('checkbox(IsLinked)', function (data) {
        	//这里是当选择一个下拉选项的时候 把选择的值赋值给表格的当前行的缓存数据 否则提交到后台的时候下拉框的值是空的
			var elem = data.othis.parents('tr');
		  	var dataindex = elem.attr("data-index");
		    var filter = $(me.config.elem).attr("lay-filter");
		  	var tableCache = table.cache[filter];
	        //改变后的数据
	        var rowObj = tableCache[dataindex].IsEdit;
	        if(rowObj) delete rowObj.IsLinked;
	        if(!rowObj)rowObj ={};
	  	    $.each(tableCache,function(index,value){
		       	if(value.LAY_TABLE_INDEX==dataindex){
		       		if(data.value)rowObj.IsLinked=data.elem.checked;
		       		value.IsEdit=rowObj;
		       		value.IsLinked = data.elem.checked;
		       	}
	        });  
	    });
	};
	/**获取关系数据*/
	Class.pt.getLinkData = function(EmpID,SetionID){
		var me = this;
		LINKDATA=[];
		Class.pt.clearData(me);
		//清空
		if(!EmpID ||  !SetionID)return;
	    EMPID = EmpID,
	    SECTIONID = SetionID;
	    var url = GET_LBRIGHT_LIST_URL+'&fields=LBRight_Id,LBRight_RoleID&where=lbright.EmpID='+EmpID+' and lbright.RoleID is not null and lbright.LBSection.Id='+SECTIONID; 
		uxutil.server.ajax({
			url:url
		},function(data){
			if(data.success){
				LINKDATA = data.value.list || [];
				Class.pt.changeRoleLink(me);
			}else{
				layer.msg(data.ErrorInfo,{icon:5});
			}
		});
	};
	
	/**勾选关系赋值*/
	Class.pt.changeRoleLink = function(that){
		var me = this;
		var    list = [],
		 	filter = $(that.config.elem).attr("lay-filter"),
            records= table.cache[filter],
			len = records.length;
		for(var i=0;i<len;i++){
		   records[i].IsLinked = "false";
		   records[i].IsEdit="";
		   records[i].LinkId="";
           for(var j=0;j<LINKDATA.length;j++){
           	    if(LINKDATA[j].LBRight_RoleID == records[i].RBACRole_Id){
           	  	    records[i].IsLinked = "true";
           	  	    records[i].LinkId = LINKDATA[j].LBRight_Id;
           	  	    break;
           	    }
           }
           list.push(records[i]);
		}
		table.reload(filter, {
            data: list
        });
	};
	//清空
	Class.pt.clearData = function(that){
		var me = this,
			filter = $(that.config.elem).attr("lay-filter");
       	LINKDATA = [];
		me.changeRoleLink(that);
	};

	/**添加一条关系*/
	Class.pt.addLink = function(rec,id,that){
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
				that.saveCount++;
			} else {
				that.saveErrorCount++;
			}				
			me.onSaveEnd(that);
		});
	};
	/**删除一条关系*/
	Class.pt.delLinkById = function(rec,id,that){
		var me = this;
		var url = DEL_LBRIGHT_URL + '?id=' + id;
		
		uxutil.server.ajax({
			url: url
		}, function(data) {
			if(data.success) {
				that.saveCount++;
			}else{
				that.saveErrorCount++;
			}
			me.onSaveEnd(that);
		});
	};
	Class.pt.onSaveEnd = function(that){
		var me = this;
		if (that.saveCount + that.saveErrorCount == that.saveLength) {
			layer.closeAll('loading');//隐藏遮罩层
			if (that.saveErrorCount == 0){
				layer.msg('保存成功!',{ icon: 6, anim: 0 ,time:2000 });
				that.getLinkData(EMPID,SECTIONID);
			}else{
				layer.msg('保存失败!',{ icon: 5, anim: 0 });
			}
		}
	};
	//获取修改过的行记录
    Class.pt.getModifiedRecords =  function(that){
    	var me = that,list=[];
    	//获取列表数据
    	var filter = $(me.config.elem).attr("lay-filter");
		var tableCache = table.cache[filter]; 
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
	//暴露接口
	exports('roletable',roletable);
});