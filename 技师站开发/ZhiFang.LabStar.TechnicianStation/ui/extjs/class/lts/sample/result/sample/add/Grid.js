/**
 * 已选择的检验项目
 * @author zhangda
 * @version 2020-04-17
 * @desc Jcall 2020-09-11修改代码部分内容
 */
Ext.define('Shell.class.lts.sample.result.sample.add.Grid',{
	extend:'Shell.ux.grid.Panel',
	requires:['Shell.ux.toolbar.Button'],
	title:'已选择项目列表',
	width:555,
	
	//获取数据服务路径
	selectUrl:'/ServerWCF/LabStarService.svc/LS_UDTO_DZQueryLisTestItemByHQL?isPlanish=true',
	//默认加载数据
	defaultLoad:true,
	//带分页栏
	hasPagingtoolbar:false,
	//是否默认选中数据
	autoSelect:true,
	//是否启用序号列
	hasRownumberer:true,
	//序号列宽度
	rowNumbererWidth:35,
	//后台排序
	remoteSort:false,
	//排序字段
	defaultOrderBy:[
		{property:'LisTestItem_PLBItem_DispOrder',direction:'ASC'},
		{property:'LisTestItem_LBItem_DispOrder',direction:'ASC'}
	],
	
	//全部数据（源数据+新增数据）
	allData:[],
	
	//检验单ID
	TestFormId:null,
	
	afterRender:function(){
		var me = this;
		me.callParent(arguments);
	},
	
    initComponent:function(){
        var me = this;
        
        //创建列
        me.columns = me.createGridColumns();
        //自定义按钮功能栏
		me.buttonToolbarItems = [{
			xtype:'textfield',fieldLabel:'已选择项目',itemId:'selectItem',
			labelWidth:75,width:280,emptyText:'名称、简称、快捷代码、拼音字头',
			listeners:{
				change:function(t,newVal,oldVal){
					me.onSearchStore(newVal);
					setTimeout(function(){t.focus()},100);
				}
			}
		}];
        
        me.callParent(arguments);
    },
    //创建数据列
	createGridColumns:function(){
		var me = this;
		
		var items = [{
			text:'主键ID',dataIndex:'LisTestItem_Id',isKey:true,hidden:true,hideable:false
		},{
			text:'组合项目ID',dataIndex:'LisTestItem_PLBItem_Id',hidden:true,hideable:false
		},{
			text:'组合项目名称',dataIndex:'LisTestItem_PLBItem_CName',width:100,
			renderer:me.isPItemRenderer
		},{
			text:'组合项目简称',dataIndex:'LisTestItem_PLBItem_SName',width:80,
			renderer:me.isPItemRenderer
		},{
			text:'组合项目快捷码',dataIndex: 'LisTestItem_PLBItem_Shortcode',width:80,hidden:true,
			renderer:me.isPItemRenderer
		},{
			text:'组合项目拼音字头',dataIndex:'LisTestItem_PLBItem_PinYinZiTou',width:80,hidden:true,
			renderer:me.isPItemRenderer
		},{
			text:'项目ID',dataIndex:'LisTestItem_LBItem_Id',hidden:true,hideable:false
		},{
			text:'项目名称',dataIndex:'LisTestItem_LBItem_CName',width:100,
			renderer:me.isPItemRenderer
		},{
			text:'项目简称',dataIndex:'LisTestItem_LBItem_SName',width:80,
			renderer:me.isPItemRenderer
		},{
			text:'项目快捷码',dataIndex:'LisTestItem_LBItem_Shortcode',width:80,hidden:true,
			renderer:me.isPItemRenderer
		},{
			text:'项目拼音字头',dataIndex:'LisTestItem_LBItem_PinYinZiTou',width:80,hidden:true,
			renderer:me.isPItemRenderer
		},{
			text:'医嘱项目',dataIndex:'LisTestItem_LBItem_IsOrderItem',width:70,
			renderer:function(v,metaData,record){
				if(String(v) == "true"){
					metaData.style = 'background-color:#7CE9BE;color:black';
					return '是';
				}else{
					metaData.style = 'color:red';
					return '否';
				}
			}
		},{
			text:'是否在用',dataIndex:'LisTestItem_DataAddTime',width:70,
			renderer:function(v,metaData,record){
				if(v){
					metaData.style = 'background-color:#ADE3F7';
					return '在用';
				}else{
					metaData.style = 'background-color:#BDFFDE';
					return '新增';
				}
			}
		},{
			text:'组合项目排序',dataIndex:'LisTestItem_PLBItem_DispOrder',width:90,hidden:true,hideable:false,type:'int'
		},{
			text:'项目排序',dataIndex:'LisTestItem_LBItem_DispOrder',width:90,hidden:true,hideable:false,type:'int'
		}];
		
		return items;
	},
	//组合项目的显示处理
    isPItemRenderer:function(v,metaData,record){
    	var ItemId = record.get("LisTestItem_LBItem_Id"),
			PItemId = record.get("LisTestItem_PLBItem_Id");
			
		if(ItemId != PItemId){
			metaData.style = 'background-color:#FFC3A5';
		}
		
		return v;
    },
	
	//获取带查询参数的URL
	getLoadUrl:function(){
		var me = this,
			url = me.callParent(arguments);
			
		url += '&TestFormId=' + me.TestFormId;
		
		return url;
	},
	//加载数据后
	onAfterLoad:function(records,successful){
		var me = this;
		
		me.allData = [];
		
		if(!me.errorInfo && (!records || records.length <= 0)){
			me.enableControl();//启用所有的操作功能
			me.fireEvent('afterLoad',[]);
			return;
		}
		
		me.callParent(arguments);
		
		me.store.each(function(re){
			me.allData.push(re.data);
		});
		me.fireEvent('afterLoad',me.allData);
	},
	//数据集查询
	onSearchStore:function(val){
		var me = this;
		
		if(!me.TestFormId){
			return;
		}
		
		var value = Ext.String.trim(val);
		if(value == ''){
			me.store.loadData(me.allData);
			if(me.allData.length > 0){
				me.getSelectionModel().select(me.allData.length-1);
			}
		}else{
			var data = [];
			Ext.Array.each(me.allData,function(str,index,array){
				if(str.LisTestItem_PLBItem_CName.indexOf(value) != -1 || 
				str.LisTestItem_PLBItem_SName.indexOf(value) != -1 || 
				str.LisTestItem_PLBItem_Shortcode.indexOf(value) != -1 || 
				str.LisTestItem_PLBItem_PinYinZiTou.indexOf(value) != -1 || 
				str.LisTestItem_LBItem_CName.indexOf(value) != -1 || 
				str.LisTestItem_LBItem_SName.indexOf(value) != -1 || 
				str.LisTestItem_LBItem_Shortcode.indexOf(value) != -1 || 
				str.LisTestItem_LBItem_PinYinZiTou.indexOf(value) != -1 ){
					data.push(str);
				}
				return true;
			});
		
			me.store.loadData(data);
			if(data.length > 0){
				me.getSelectionModel().select(data.length-1);
			}
		}
	}
});