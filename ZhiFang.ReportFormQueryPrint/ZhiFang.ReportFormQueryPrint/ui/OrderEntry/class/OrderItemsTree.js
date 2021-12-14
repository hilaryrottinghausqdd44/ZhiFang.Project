/**
 * 申请项目树
 * @author Jcall
 * @version 2014-10-23
 */
Ext.define('Shell.OrderEntry.class.OrderItemsTree',{
	extend:'Shell.ux.panel.Tree',
	
	title:'可选申请项目',
	
	/**是否复选*/
	multiSelect:false,
	/**显示根节点*/
	rootVisible:false,
	/**根节点对象*/
	root:{text:'模板项目',expanded:true,leaf:false},
	
	/**叶子节点图标路径*/
    //leafIcon:null,
    /**非叶子节点图标路径*/
    //nLeafIcon:Shell.util.Path.uiPath + '/OrderEntry/images/project_templates.png',
	
	/**获取列表数据服务*/
	selectUrl:'/OrderService.svc/GetItemModelTree',
	/**HIS科室ID*/
	HisDeptNo:null,
	/**就诊类型*/
	SickTypeNo:null,
	/**需要的数据字段*/
	fields:['tid','text','expanded','leaf','icon','isItem','itemList','eName'],
	/**过滤数据列*/
	filterFields:['text','eName'],
	
	/**初始化面板属性*/
	initComponent:function(){
		var me = this;
		
		me.storeConfig = {
			proxy:{
				type:'ajax',
				url:Shell.util.Path.rootPath + me.selectUrl,
				extractResponseData:function(response){
					var result = Ext.JSON.decode(response.responseText),
						success = result.success;
					if(!success && me.showErrorInfo){Shell.util.Msg.showError(result.ErrorInfo);}
					
					result[me.defaultRootProperty] = me.changeTreeData(result[me.defaultRootProperty]);
						
					response.responseText = Ext.JSON.encode(result);
					
					return response;
				}
			}
		};
		
		me.listeners = {
			itemclick:function(treeview,record,item,index,e,eOpts) { 
			    //取消双击展开折叠菜单行为
			    //treeview.toggleOnDblClick = false;
				treeview.onItemDblClick(record,item,index);
			}
		};
		
		me.callParent(arguments);
	},
	
	/**获取带查询参数的URL*/
	getLoadUrl:function(){
		var me = this,
			url = Shell.util.Path.rootPath + me.selectUrl;
		
		url += "?hisDeptNo=" + me.HisDeptNo + "&sickTypeNo=" + me.SickTypeNo;// + "&fields=" + me.fields.join(",");
		
		return url;
	},
	
	onSearch:function(){
		this.load(null,true);
	},
	
	/**根据节点获取节点下的所有项目节点*/
	getChildrenItems:function(record){
		var me = this,
			list = [];
			
//		/*遍历子结点*/
//		var chd = function(node){
//			if(node.get('isItem')){
//				list.push({
//					tid:node.get('tid'),
//					text:node.get('text')
//				});
//			}
//			
//			if(node.isNode){
//				node.eachChild(function(child){
//					chd(child);
//				});
//			}
//		};
//		
//		chd(record);
			
		if(record.get('isItem')){
			list.push({
				tid:record.get('tid'),
				text:record.get('text'),
				itemList:record.get('itemList')
			});
		}
		
		return list;
	}
});