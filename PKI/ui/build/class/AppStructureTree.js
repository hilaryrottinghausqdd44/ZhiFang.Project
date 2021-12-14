/**
 * 应用结构树类
 */
Ext.ns('Ext.build');
Ext.define('Ext.build.AppStructureTree',{
	extend:'Ext.tree.Panel',
	alias:'widget.appstructuretree',
	//============可配参数===========
	rootId:-1,
	rootText:'',
	useArrows:true,
	rootVisible:false,//是否显示根节点
	containerScroll:true,//是否支持滚动条 
	autoScroll:false,//内容溢出的时候是否产生滚动条
	getAppComRefListServerUrl:getRootPath()+'/ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsRefListById',
	//===============================
	
	initComponent:function(){
		var me = this;
		//根节点对象
		me.root = {
			text:me.rootText,
			id:me.rootId,
			leaf:false,
			expanded:true
		};
		//数据集合
		me.store = me.createStore();
		
		me.callParent(arguments);
	},
	/**
	 * 数据集
	 * @private
	 */
	createStore:function(){
		var me = this;
		
		var store = Ext.create('Ext.data.TreeStore',{
			fields:me.getTreeFields(),
			proxy:{
				type:'ajax',
				url:me.getTreeUrl(),
				extractResponseData:function(response){return me.changeData(response);}
			}
		});
		return store;
	},
	/**
	 * 数据转化
	 * @private
	 * @param {} response
	 * @return {}
	 */
	changeData:function(response){
		var me = this;
		var result = Ext.JSON.decode(response.responseText);
		if(!result.success){
    		alertError('获取应用结构数据失败！错误信息:'+result.ErrorInfo);
    	}else{
    		var list = [];
			var count = 0;
			
			if(result.ResultDataValue && result.ResultDataValue != ""){
	    		var ResultDataValue = Ext.JSON.decode(result.ResultDataValue);
		    	list = ResultDataValue.list;
		    	count = ResultDataValue.count;
	    	}
	    	
	    	var children = me.changeListToTree(list);
	    	
	    	var data = {
	    		success:true,
	    		children:children
	    	};
	    	
	    	response.responseText = Ext.JSON.encode(data);
    	}
    	return response;
	},
	/**
	 * 将列表格式的数据转化成树格式的数据
	 * @private
	 * @param {} list
	 * @return {}
	 */
	changeListToTree:function(list){
		var me = this;
		//递归生成结构树
		var insertNode = function(node,data){
			for(var i in data){
				if(data[i].AppComID == node.appId){
					var n = {};
					n.appId = data[i].RefAppComID;//应用ID
					n.appItemId = data[i].RefAppComIncID;//应用内部编码
					
					if(!node.children){
						node.children = [];
					}
					node.children.push(n);
				}
			}
			for(var i in node.children){
				insertNode(node.children[i],data);
			}
		}
		
		var rootNode = {
			appId:me.rootId,
			children:[]
		};
		
		insertNode(rootNode,list);
		
		return rootNode.children;
	},
	/**
	 * 获取数据字段
	 * @private
	 * @return {}
	 */
	getTreeFields:function(){
		var fields = [
			{name:'BTDAppComponentsRef_RefAppComID',type:'string'},//被引用应用ID
			{name:'BTDAppComponentsRef_RefAppComIncID',type:'string'}//被引用应用内部编号
		];
		return fields;
	},
	getTreeUrl:function(){
		var me = this;
		var url = me.getAppComRefListServerUrl;
		url += "?isPlanish=true&fields=BTDAppComponentsRef_RefAppComID,BTDAppComponentsRef_RefAppComIncID";
		return url;
	},
	/**
	 * 测试代码
	 */
	test:function(){
		var list = [
			{Id:1,AppComID:'1000',RefAppComID:'1001',RefAppComIncID:'item1'},
			{Id:2,AppComID:'1000',RefAppComID:'1002',RefAppComIncID:'item2'},
			{Id:3,AppComID:'1001',RefAppComID:'1003',RefAppComIncID:'item3'},
			{Id:4,AppComID:'1001',RefAppComID:'1004',RefAppComIncID:'item4'},
			{Id:5,AppComID:'1002',RefAppComID:'1005',RefAppComIncID:'item5'},
			{Id:6,AppComID:'1002',RefAppComID:'1006',RefAppComIncID:'item6'},
			{Id:7,AppComID:'1003',RefAppComID:'1007',RefAppComIncID:'item7'}
		];
		
		//1000是根节点
		var tree = [{
			appId:'1001',appItemId:'item1',children:[{
				appId:'1003',appItemId:'item3',children:[{
					appId:'1007',appItemId:'item7'
				}]
			},{
				appId:'1004',appItemId:'item4'
			}]
		},{
			appId:'1002',itemId:'item2',children:[{
				appId:'1005',appItemId:'item5'
			},{
				appId:'1006',appItemId:'item6'
			}]
		}];
		
		//逻辑
		var rootId = '1000';//跟节点ID
		var rootNode = {
			appId:rootId,
			children:[]
		};
		
		var insertNode = function(node,data){
			for(var i in data){
				if(data[i].AppComID == node.appId){
					var n = {};
					n.appId = data[i].RefAppComID;//应用ID
					n.appItemId = data[i].RefAppComIncID;//应用内部编码
					
					if(!node.children){
						node.children = [];
					}
					node.children.push(n);
				}
			}
			for(var i in node.children){
				insertNode(node.children[i],data);
			}
		}
		insertNode(rootNode,list);
		var a = rootNode;
	}
});